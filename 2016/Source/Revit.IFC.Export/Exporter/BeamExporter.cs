﻿//
// BIM IFC library: this library works with Autodesk(R) Revit(R) to export IFC files containing model geometry.
// Copyright (C) 2012-2016  Autodesk, Inc.
// 
// This library is free software; you can redistribute it and/or
// modify it under the terms of the GNU Lesser General Public
// License as published by the Free Software Foundation; either
// version 2.1 of the License, or (at your option) any later version.
//
// This library is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
// Lesser General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with this library; if not, write to the Free Software
// Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301  USA
//

using System;
using System.Collections.Generic;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.IFC;
using Revit.IFC.Export.Utility;
using Revit.IFC.Export.Toolkit;
using Revit.IFC.Export.Exporter.PropertySet;
using Revit.IFC.Common.Utility;

namespace Revit.IFC.Export.Exporter
{
   /// <summary>
   /// Provides methods to export beams.
   /// </summary>
   class BeamExporter
   {
      /// <summary>
      /// A structure to contain information about the defining axis of a beam.
      /// </summary>
      private class BeamAxisInfo
      {
         /// <summary>
         /// The default constructor.
         /// </summary>
         public BeamAxisInfo()
         {
            Axis = null;
            LCSAsTransform = null;
            LCSAsPlane = null;
            AxisDirection = null;
            AxisNormal = null;
         }

         /// <summary>
         /// The curve that represents the beam axis.
         /// </summary>
         public Curve Axis { get; set; }

         /// <summary>
         /// The local coordinate system of the beam used for IFC export as a plane.
         /// </summary>
         public Plane LCSAsPlane { get; set; }

         /// <summary>
         /// The local coordinate system of the beam used for IFC export as a transform.
         /// </summary>
         public Transform LCSAsTransform { get; set; }

         /// <summary>
         /// The tangent to the axis at the start parameter of the axis curve.
         /// </summary>
         public XYZ AxisDirection { get; set; }

         /// <summary>
         /// The normal to the axis at the start parameter of the axis curve.
         /// </summary>
         public XYZ AxisNormal { get; set; }
      }

      /// <summary>
      /// A structure to contain the body representation of the beam, if it can be expressed as an extrusion, potentially with clippings and openings.
      /// </summary>
      private class BeamBodyAsExtrusionInfo
      {
         /// <summary>
         /// The default constructor.
         /// </summary>
         public BeamBodyAsExtrusionInfo()
         {
            RepresentationHandle = null;
            Materials = null;
            Slope = 0.0;
            DontExport = false;
         }

         /// <summary>
         /// The IFC handle representing the created extrusion, potentially with clippings.
         /// </summary>
         public IFCAnyHandle RepresentationHandle { get; set; }

         /// <summary>
         /// The set of material ids for the beam.  This should usually only contain one material id. (Probably will be deprecated, replaced with MaterialProfile)
         /// </summary>
         public ICollection<ElementId> Materials { get; set; }

         /// <summary>
         /// The material profile set for the extruded Beam
         /// </summary>
         public MaterialAndProfile materialAndProfile { get; set; }

         /// <summary>
         /// The calculated slope of the beam along its axis, relative to the XY plane.
         /// </summary>
         public double Slope { get; set; }

         /// <summary>
         /// True if the beam has no geometry to export, and as such attempts to export should stop.
         /// </summary>
         public bool DontExport { get; set; }
      }

      /// <summary>
      /// Get information about the beam axis, if possible.
      /// </summary>
      /// <param name="element">The beam element.</param>
      /// <returns>The BeamAxisInfo structure, or null if the beam has no axis, or it is not a Line or Arc.</returns>
      private static BeamAxisInfo GetBeamAxisTransform(Element element)
      {
         BeamAxisInfo axisInfo = null;

         Transform orientTrf = Transform.Identity;
         XYZ beamDirection = null;
         XYZ projDir = null;
         Curve curve = null;
         Plane plane = null;

         LocationCurve locCurve = element.Location as LocationCurve;
         bool canExportAxis = (locCurve != null);

         if (canExportAxis)
         {
            curve = locCurve.Curve;
            if (curve is Line)
            {
               Line line = curve as Line;
               XYZ planeY, planeOrig;
               planeOrig = line.GetEndPoint(0);
               beamDirection = line.Direction;
               beamDirection = beamDirection.Normalize();
               if (Math.Abs(beamDirection.Z) < 0.707)  // approx 1.0/sqrt(2.0)
               {
                  planeY = XYZ.BasisZ.CrossProduct(beamDirection);
               }
               else
               {
                  planeY = XYZ.BasisX.CrossProduct(beamDirection);
               }
               planeY = planeY.Normalize();
               projDir = beamDirection.CrossProduct(planeY);
               plane = new Plane(beamDirection, planeY, planeOrig);
               orientTrf.BasisX = beamDirection; orientTrf.BasisY = planeY; orientTrf.BasisZ = projDir; orientTrf.Origin = planeOrig;
            }
            else if (curve is Arc)
            {
               XYZ yDir, center;
               Arc arc = curve as Arc;
               beamDirection = arc.XDirection; yDir = arc.YDirection; projDir = arc.Normal; center = arc.Center;
               beamDirection = beamDirection.Normalize();
               yDir = yDir.Normalize();
               if (!MathUtil.IsAlmostZero(beamDirection.DotProduct(yDir)))
               {
                  // ensure that beamDirection and yDir are orthogonal
                  yDir = projDir.CrossProduct(beamDirection);
                  yDir = yDir.Normalize();
               }
               plane = new Plane(beamDirection, yDir, center);
               orientTrf.BasisX = beamDirection; orientTrf.BasisY = yDir; orientTrf.BasisZ = projDir; orientTrf.Origin = center;
            }
            else
               canExportAxis = false;
         }

         if (canExportAxis)
         {
            axisInfo = new BeamAxisInfo();
            axisInfo.Axis = curve;
            axisInfo.AxisDirection = beamDirection;
            axisInfo.AxisNormal = projDir;
            axisInfo.LCSAsPlane = plane;
            axisInfo.LCSAsTransform = orientTrf;
         }

         return axisInfo;
      }

      /// <summary>
      /// Create the handle corresponding to the "Axis" IfcRepresentation for a beam, if possible.
      /// </summary>
      /// <param name="exporterIFC">The ExporterIFC class.</param>
      /// <param name="element">The beam element.</param>
      /// <param name="catId">The beam category id.</param>
      /// <param name="axisInfo">The optional beam axis information.</param>
      /// <param name="offsetTransform">The optional offset transform applied to the "Body" representation.</param>
      /// <returns>The handle, or null if not created.</returns>
      private static IFCAnyHandle CreateBeamAxis(ExporterIFC exporterIFC, Element element, ElementId catId, BeamAxisInfo axisInfo, Transform offsetTransform)
      {
         if (axisInfo == null)
            return null;

         Curve curve = axisInfo.Axis;
         XYZ projDir = axisInfo.AxisNormal;
         Plane plane = axisInfo.LCSAsPlane;

         XYZ curveOffset = XYZ.Zero;
         if (offsetTransform != null)
            curveOffset = -UnitUtil.UnscaleLength(offsetTransform.Origin);
         else
         {
            // Note that we do not have to have any scaling adjustment here, since the curve origin is in the 
            // same internal coordinate system as the curve.
            curveOffset = -plane.Origin;
         }

         Plane offsetPlane = new Plane(plane.XVec, plane.YVec, XYZ.Zero);
         IFCGeometryInfo info = IFCGeometryInfo.CreateCurveGeometryInfo(exporterIFC, offsetPlane, projDir, false);
         ExporterIFCUtils.CollectGeometryInfo(exporterIFC, info, curve, curveOffset, true);

         IList<IFCAnyHandle> axis_items = info.GetCurves();

         if (axis_items.Count > 0)
         {
            string identifierOpt = "Axis";	// This is by IFC2x2+ convention.
            string representationTypeOpt = "Curve2D";  // This is by IFC2x2+ convention.
            IFCAnyHandle axisRep = RepresentationUtil.CreateShapeRepresentation(exporterIFC, element, catId, exporterIFC.Get3DContextHandle(identifierOpt),
               identifierOpt, representationTypeOpt, axis_items);
            return axisRep;
         }

         return null;
      }

      /// <summary>
      /// Create the "Body" IfcRepresentation for a beam if it is representable by an extrusion, possibly with clippings and openings.
      /// </summary>
      /// <param name="exporterIFC">The exporterIFC class.</param>
      /// <param name="element">The beam element.T</param>
      /// <param name="catId">The category id.</param>
      /// <param name="geomObjects">The list of solids and meshes representing the beam's geometry.
      /// <param name="axisInfo">The beam axis information.</param>
      /// <returns>The BeamBodyAsExtrusionInfo class which contains the created handle (if any) and other information, or null.</returns>
      private static BeamBodyAsExtrusionInfo CreateBeamGeometryAsExtrusion(ExporterIFC exporterIFC, Element element, ElementId catId,
          IList<GeometryObject> geomObjects, BeamAxisInfo axisInfo)
      {
         // If we have a beam with a Linear location line that only has one solid geometry,
         // we will try to use the ExtrusionAnalyzer to generate an extrusion with 0 or more clippings.
         // This code is currently limited in that it will not process beams with openings, so we
         // use other methods below if this one fails.
         if (geomObjects == null || geomObjects.Count != 1 || (!(geomObjects[0] is Solid)) || axisInfo == null || !(axisInfo.Axis is Line))
            return null;

         Solid solid = geomObjects[0] as Solid;

         BeamBodyAsExtrusionInfo info = new BeamBodyAsExtrusionInfo();
         info.DontExport = false;
         info.Materials = new HashSet<ElementId>();
         info.Slope = 0.0;

         Transform orientTrf = axisInfo.LCSAsTransform;

         bool completelyClipped;
         XYZ beamDirection = orientTrf.BasisX;
         XYZ planeXVec = orientTrf.BasisY.Normalize();
         XYZ planeYVec = orientTrf.BasisZ.Normalize();

         MaterialAndProfile materialAndProfile = null;
         IFCAnyHandle footPrintHnd = null;

         Plane beamExtrusionPlane = new Plane(planeXVec, planeYVec, orientTrf.Origin);
         info.RepresentationHandle = ExtrusionExporter.CreateExtrusionWithClipping(exporterIFC, element,
          catId, solid, beamExtrusionPlane, beamDirection, null, out completelyClipped,
          out footPrintHnd, out materialAndProfile, addInfo: GenerateAdditionalInfo.GenerateProfileDef);
         if (completelyClipped)
         {
            info.DontExport = true;
            return null;
         }

         if (!IFCAnyHandleUtil.IsNullOrHasNoValue(info.RepresentationHandle))
         {
            // This is used by the BeamSlopeCalculator.  This should probably be generated automatically by
            // CreateExtrusionWithClipping.
            IFCExtrusionBasis bestAxis = (Math.Abs(beamDirection[0]) > Math.Abs(beamDirection[1])) ?
                IFCExtrusionBasis.BasisX : IFCExtrusionBasis.BasisY;
            info.Slope = GeometryUtil.GetSimpleExtrusionSlope(beamDirection, bestAxis);
            ElementId materialId = BodyExporter.GetBestMaterialIdFromGeometryOrParameter(solid, exporterIFC, element);
            if (materialId != ElementId.InvalidElementId)
               info.Materials.Add(materialId);
         }

         if (materialAndProfile != null)
            info.materialAndProfile = materialAndProfile;

         return info;
      }

      /// <summary>
      /// Determines the beam geometry to export after removing invisible geometry.
      /// </summary>
      /// <param name="exporterIFC">The ExporterIFC object.</param>
      /// <param name="element">The beam element to be exported.</param>
      /// <param name="geometryElement">The geometry element that contains the beam geometry.</param>
      /// <param name="dontExport">An output value that says that the element shouldn't be exported at all.</param>
      private static IList<GeometryObject> BeamGeometryToExport(ExporterIFC exporterIFC, Element element,
         GeometryElement geometryElement, out bool dontExport)
      {
         dontExport = true;
         if (element == null || geometryElement == null)
            return null;

         IList<GeometryObject> visibleGeomObjects = new List<GeometryObject>();
         {
            SolidMeshGeometryInfo solidMeshInfo = GeometryUtil.GetSplitSolidMeshGeometry(geometryElement);

            IList<Solid> solids = solidMeshInfo.GetSolids();
            IList<Mesh> meshes = solidMeshInfo.GetMeshes();

            visibleGeomObjects = FamilyExporterUtil.RemoveInvisibleSolidsAndMeshes(element.Document, exporterIFC, solids, meshes);

            // If we found solids and meshes, and they are all invisible, don't export the beam.
            // If we didn't find solids and meshes, we won't export the beam with ExportBeamAsStandardElement, but will allow the generic
            // family export routine to work.
            if ((visibleGeomObjects == null || visibleGeomObjects.Count == 0) && (solids.Count > 0 || meshes.Count > 0))
               return null;
         }

         dontExport = false;
         return visibleGeomObjects;
      }

      /// <summary>
      /// Creates a new IfcBeamType and relates it to the current element.
      /// </summary>
      /// <param name="exporterIFC">The exporter.</param>
      /// <param name="wrapper">The ProductWrapper class.</param>
      /// <param name="elementHandle">The element handle.</param>
      /// <param name="element">The element.</param>
      /// <param name="overrideMaterialId">The material id used for the element type.</param>
      public static void ExportBeamType(ExporterIFC exporterIFC, ProductWrapper wrapper, IFCAnyHandle elementHandle, Element element, string predefinedType)
      {
         if (elementHandle == null || element == null)
            return;

         Document doc = element.Document;
         ElementId typeElemId = element.GetTypeId();
         Element elementType = doc.GetElement(typeElemId);
         if (elementType == null)
            return;

         IFCAnyHandle beamType = ExporterCacheManager.ElementToHandleCache.Find(typeElemId);
         if (!IFCAnyHandleUtil.IsNullOrHasNoValue(beamType))
         {
            ExporterCacheManager.TypeRelationsCache.Add(beamType, elementHandle);
            return;
         }

         string elemGUID = GUIDUtil.CreateGUID(elementType);
         string elemName = NamingUtil.GetNameOverride(elementType, NamingUtil.GetIFCName(elementType));
         string elemDesc = NamingUtil.GetDescriptionOverride(elementType, null);
         string elemTag = NamingUtil.GetTagOverride(elementType, NamingUtil.CreateIFCElementId(elementType));
         string elemApplicableOccurence = NamingUtil.GetOverrideStringValue(elementType, "IfcApplicableOccurence", null);
         string elemElementType = NamingUtil.GetOverrideStringValue(elementType, "IfcElementType", null);

         // Property sets will be set later.
         beamType = IFCInstanceExporter.CreateBeamType(exporterIFC.GetFile(), elemGUID, ExporterCacheManager.OwnerHistoryHandle,
            elemName, elemDesc, elemApplicableOccurence, null, null, elemTag, elemElementType, GetBeamType(elementType, predefinedType));

         wrapper.RegisterHandleWithElementType(elementType as ElementType, beamType, null);

         ExporterCacheManager.TypeRelationsCache.Add(beamType, elementHandle);
         ExporterCacheManager.ElementToHandleCache.Register(typeElemId, beamType);
      }

      /// <summary>
      /// Exports a beam to IFC beam if it has an axis representation and only one Solid as its geometry, ideally as an extrusion, potentially with clippings and openings.
      /// </summary>
      /// <param name="exporterIFC">The ExporterIFC object.</param>
      /// <param name="element">The element to be exported.</param>
      /// <param name="geometryElement">The geometry element.</param>
      /// <param name="productWrapper">The ProductWrapper.</param>
      /// <param name="dontExport">An output value that says that the element shouldn't be exported at all.</param>
      /// <returns>The created handle.</returns>
      /// <remarks>In the original implementation, the ExportBeam function would export each beam as its own individual geometry (that is, not use representation maps).
      /// For non-standard beams, this could result in massive IFC files.  Now, we use the ExportBeamAsStandardElement function and limit its scope, and instead
      /// resort to the standard FamilyInstanceExporter.ExportFamilyInstanceAsMappedItem for more complicated objects categorized as beams.  This has the following pros and cons:
      /// Pro: possiblity for massively reduced file sizes for files containing repeated complex beam families
      /// Con: some beams that may have had an "Axis" representation before will no longer have them, although this possibility is minimized.
      /// Con: some beams that have 1 Solid and an axis, but that Solid will be heavily faceted, won't be helped by this improvement.
      /// It is intended that we phase out this routine entirely and instead teach ExportFamilyInstanceAsMappedItem how to sometimes export the Axis representation for beams.</remarks>
      public static IFCAnyHandle ExportBeamAsStandardElement(ExporterIFC exporterIFC,
         Element element, GeometryElement geometryElement, ProductWrapper productWrapper, out bool dontExport)
      {
         dontExport = true;
         IList<GeometryObject> geomObjects = BeamGeometryToExport(exporterIFC, element, geometryElement, out dontExport);
         if (dontExport)
            return null;

         IFCAnyHandle beam = null;
         IFCFile file = exporterIFC.GetFile();
         MaterialAndProfile materialAndProfile = null;
         IFCAnyHandle materialProfileSet = null;

         using (IFCTransaction transaction = new IFCTransaction(file))
         {
            BeamAxisInfo axisInfo = GetBeamAxisTransform(element);
            bool canExportAxis = (axisInfo != null);

            Curve curve = canExportAxis ? axisInfo.Axis : null;
            XYZ beamDirection = canExportAxis ? axisInfo.AxisDirection : null;
            Transform orientTrf = canExportAxis ? axisInfo.LCSAsTransform : null;

            using (PlacementSetter setter = PlacementSetter.Create(exporterIFC, element, null, orientTrf))
            {
               IFCAnyHandle localPlacement = setter.LocalPlacement;
               using (IFCExtrusionCreationData extrusionCreationData = new IFCExtrusionCreationData())
               {
                  extrusionCreationData.SetLocalPlacement(localPlacement);
                  if (canExportAxis && (orientTrf.BasisX != null))
                  {
                     extrusionCreationData.CustomAxis = beamDirection;
                     extrusionCreationData.PossibleExtrusionAxes = IFCExtrusionAxes.TryCustom;
                  }
                  else
                     extrusionCreationData.PossibleExtrusionAxes = IFCExtrusionAxes.TryXY;

                  ElementId catId = CategoryUtil.GetSafeCategoryId(element);

                  // There may be an offset to make the local coordinate system
                  // be near the origin.  This offset will be used to move the axis to the new LCS.
                  Transform offsetTransform = null;

                  // The list of materials in the solids or meshes.
                  ICollection<ElementId> materialIds = null;

                  // The representation handle generated from one of the methods below.
                  BeamBodyAsExtrusionInfo extrusionInfo = CreateBeamGeometryAsExtrusion(exporterIFC, element, catId, geomObjects, axisInfo);
                  if (extrusionInfo != null && extrusionInfo.DontExport)
                  {
                     dontExport = true;
                     return null;
                  }

                  IFCAnyHandle repHnd = (extrusionInfo != null) ? extrusionInfo.RepresentationHandle : null;

                  if (!IFCAnyHandleUtil.IsNullOrHasNoValue(repHnd))
                  {
                     materialIds = extrusionInfo.Materials;
                     extrusionCreationData.Slope = extrusionInfo.Slope;
                  }
                  else
                  {
                     // Here is where we limit the scope of how complex a case we will still try to export as a standard element.
                     // This is explicitly added so that many curved beams that can be represented by a reasonable facetation because of the
                     // SweptSolidExporter can still have an Axis representation.
                     BodyData bodyData = null;

                     BodyExporterOptions bodyExporterOptions = new BodyExporterOptions(true, ExportOptionsCache.ExportTessellationLevel.ExtraLow);
                     bodyExporterOptions.CollectMaterialAndProfile = true;

                     if (geomObjects != null && geomObjects.Count == 1 && geomObjects[0] is Solid)
                     {
                        bodyData = BodyExporter.ExportBody(exporterIFC, element, catId, ElementId.InvalidElementId,
                    geomObjects[0], bodyExporterOptions, extrusionCreationData);

                        repHnd = bodyData.RepresentationHnd;
                        materialIds = bodyData.MaterialIds;
                        offsetTransform = bodyData.OffsetTransform;
                        materialAndProfile = bodyData.materialAndProfile;
                     }
                  }

                  if (IFCAnyHandleUtil.IsNullOrHasNoValue(repHnd))
                  {
                     extrusionCreationData.ClearOpenings();
                     return null;
                  }

                  // This code doesn't quite work yet, as it always creates a BeamType and doesn't deal with axes correctly.  Redo or remove.
                  // Note that there is a call CreateBeamType below that does the right thing below.

                  //IFCAnyHandle typeStyle = null;
                  //FamilyInstance familyInstance = element as FamilyInstance;
                  //if (familyInstance != null)
                  //{
                  //FamilySymbol originalFamilySymbol = ExporterIFCUtils.GetOriginalSymbol(familyInstance);
                  //FamilySymbol familySymbol = familyInstance.Symbol;
                  //if (originalFamilySymbol != null && familySymbol != null)
                  //{
                  //IFCAnyHandle ownerHistory = ExporterCacheManager.OwnerHistoryHandle;

                  //string familyName = familySymbol.Name;
                  //string revitObjectType = familyName;
                  //string symId = NamingUtil.CreateIFCElementId(originalFamilySymbol);
                  //string guid = GUIDUtil.CreateGUID(originalFamilySymbol);

                  //string gentypeName = NamingUtil.GetNameOverride(familySymbol, revitObjectType);
                  //string gentypeDescription = NamingUtil.GetDescriptionOverride(familySymbol, null);
                  //string gentypeApplicableOccurrence = NamingUtil.GetOverrideStringValue(familySymbol, "IfcApplicableOccurrence", null);
                  //string gentypeTag = NamingUtil.GetTagOverride(familySymbol, symId);
                  //string gentypeElementType = NamingUtil.GetOverrideStringValue(familySymbol, "IfcElementType", revitObjectType);

                  //HashSet<IFCAnyHandle> propertySets = new HashSet<IFCAnyHandle>();

                  //IList<IFCAnyHandle> repMapsHnd = new List<IFCAnyHandle>();

                  //string beamType = "Beam";
                  //typeStyle = IFCInstanceExporter.CreateBeamType(file, guid, ownerHistory, gentypeName,
                  //gentypeDescription, gentypeApplicableOccurrence, propertySets, repMapsHnd, gentypeTag,
                  //gentypeElementType, GetBeamType(familyInstance, beamType));

                  //if (materialAndProfile != null)
                  //{
                  //materialProfileSet = CategoryUtil.GetOrCreateMaterialSet(exporterIFC, familySymbol, typeStyle, materialAndProfile);
                  //CategoryUtil.CreateMaterialAssociation(exporterIFC, familySymbol, typeStyle, materialAndProfile);
                  //}
                  //}
                  //}

                  IList<IFCAnyHandle> representations = new List<IFCAnyHandle>();

                  IFCAnyHandle axisRep = CreateBeamAxis(exporterIFC, element, catId, axisInfo, offsetTransform);
                  if (!IFCAnyHandleUtil.IsNullOrHasNoValue(axisRep))
                     representations.Add(axisRep);
                  representations.Add(repHnd);

                  Transform boundingBoxTrf = (offsetTransform == null) ? Transform.Identity : offsetTransform.Inverse;
                  IFCAnyHandle boundingBoxRep = BoundingBoxExporter.ExportBoundingBox(exporterIFC, geometryElement, boundingBoxTrf);
                  if (boundingBoxRep != null)
                     representations.Add(boundingBoxRep);

                  IFCAnyHandle prodRep = IFCInstanceExporter.CreateProductDefinitionShape(file, null, null, representations);

                  string instanceGUID = GUIDUtil.CreateGUID(element);
                  string instanceName = NamingUtil.GetNameOverride(element, NamingUtil.GetIFCName(element));
                  string instanceDescription = NamingUtil.GetDescriptionOverride(element, null);
                  string instanceObjectType = NamingUtil.GetObjectTypeOverride(element, NamingUtil.CreateIFCObjectName(exporterIFC, element));
                  string instanceTag = NamingUtil.GetTagOverride(element, NamingUtil.CreateIFCElementId(element));
                  string preDefinedType = "BEAM";     // Default predefined type for Beam
                  preDefinedType = IFCValidateEntry.GetValidIFCType(element, preDefinedType);

                  beam = IFCInstanceExporter.CreateBeam(file, instanceGUID, ExporterCacheManager.OwnerHistoryHandle,
                      instanceName, instanceDescription, instanceObjectType, extrusionCreationData.GetLocalPlacement(), prodRep, instanceTag, preDefinedType);

                  IFCAnyHandle mpSetUsage;
                  if (materialProfileSet != null)
                     mpSetUsage = IFCInstanceExporter.CreateMaterialProfileSetUsage(file, materialProfileSet, null, null);

                  productWrapper.AddElement(element, beam, setter, extrusionCreationData, true);

                  ExportBeamType(exporterIFC, productWrapper, beam, element, preDefinedType);

                  OpeningUtil.CreateOpeningsIfNecessary(beam, element, extrusionCreationData, offsetTransform, exporterIFC,
                      extrusionCreationData.GetLocalPlacement(), setter, productWrapper);

                  FamilyTypeInfo typeInfo = new FamilyTypeInfo();
                  typeInfo.ScaledDepth = extrusionCreationData.ScaledLength;
                  typeInfo.ScaledArea = extrusionCreationData.ScaledArea;
                  typeInfo.ScaledInnerPerimeter = extrusionCreationData.ScaledInnerPerimeter;
                  typeInfo.ScaledOuterPerimeter = extrusionCreationData.ScaledOuterPerimeter;
                  PropertyUtil.CreateBeamColumnBaseQuantities(exporterIFC, beam, element, typeInfo, null);

                  if (materialIds.Count != 0)
                     CategoryUtil.CreateMaterialAssociation(exporterIFC, beam, materialIds);

                  // Register the beam's IFC handle for later use by truss and beam system export.
                  ExporterCacheManager.ElementToHandleCache.Register(element.Id, beam);

                  // TODO: To create IfcBeamType, IfcMaterialProfileSet, Usage
               }
            }

            transaction.Commit();
            return beam;
         }
      }

      static IFCBeamType GetBeamType(Element element, string beamType)
      {
         string value = null;
         if (ParameterUtil.GetStringValueFromElementOrSymbol(element, "IfcType", out value) == null)
            value = beamType;

         if (String.IsNullOrEmpty(value))
            return IFCBeamType.Beam;

         string newValue = NamingUtil.RemoveSpacesAndUnderscores(value);

         if (String.Compare(newValue, "USERDEFINED", true) == 0)
            return IFCBeamType.UserDefined;

         return IFCBeamType.Beam;
      }
   }
}