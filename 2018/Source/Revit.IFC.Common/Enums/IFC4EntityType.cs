﻿//
// BIM IFC export alternate UI library: this library works with Autodesk(R) Revit(R) to provide an alternate user interface for the export of IFC files from Revit.
// Copyright (C) 2013  Autodesk, Inc.
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Revit.IFC.Common.Enums.IFC4
{
   /// <summary>
   /// IFC entity types (updated to IFC4-Add2)
   /// </summary>
   public enum IFCEntityType
   {
      IfcActionRequest,
      IfcActor,
      IfcActorRole,
      IfcActuator,
      IfcActuatorType,
      IfcAddress,
      IfcAdvancedBrep,
      IfcAdvancedBrepWithVoids,
      IfcAdvancedFace,
      IfcAirTerminal,
      IfcAirTerminalBox,
      IfcAirTerminalBoxType,
      IfcAirTerminalType,
      IfcAirToAirHeatRecovery,
      IfcAirToAirHeatRecoveryType,
      IfcAlarm,
      IfcAlarmType,
      IfcAnnotation,
      IfcAnnotationFillArea,
      IfcApplication,
      IfcAppliedValue,
      IfcApproval,
      IfcApprovalRelationship,
      IfcArbitraryClosedProfileDef,
      IfcArbitraryOpenProfileDef,
      IfcArbitraryProfileDefWithVoids,
      IfcAsset,
      IfcAsymmetricIShapeProfileDef,
      IfcAudioVisualAppliance,
      IfcAudioVisualApplianceType,
      IfcAxis1Placement,
      IfcAxis2Placement2D,
      IfcAxis2Placement3D,
      IfcBeam,
      IfcBeamStandardCase,
      IfcBeamType,
      IfcBlobTexture,
      IfcBlock,
      IfcBoiler,
      IfcBoilerType,
      IfcBooleanClippingResult,
      IfcBooleanResult,
      IfcBoundaryCondition,
      IfcBoundaryCurve,
      IfcBoundaryEdgeCondition,
      IfcBoundaryFaceCondition,
      IfcBoundaryNodeCondition,
      IfcBoundaryNodeConditionWarping,
      IfcBoundedCurve,
      IfcBoundedSurface,
      IfcBoundingBox,
      IfcBoxedHalfSpace,
      IfcBSplineCurve,
      IfcBSplineCurveWithKnots,
      IfcBSplineSurface,
      IfcBSplineSurfaceWithKnots,
      IfcBuilding,
      IfcBuildingElement,
      IfcBuildingElementPart,
      IfcBuildingElementPartType,
      IfcBuildingElementProxy,
      IfcBuildingElementProxyType,
      IfcBuildingElementType,
      IfcBuildingStorey,
      IfcBuildingSystem,
      IfcBurner,
      IfcBurnerType,
      IfcCableCarrierFitting,
      IfcCableCarrierFittingType,
      IfcCableCarrierSegment,
      IfcCableCarrierSegmentType,
      IfcCableFitting,
      IfcCableFittingType,
      IfcCableSegment,
      IfcCableSegmentType,
      IfcCartesianPoint,
      IfcCartesianPointList,
      IfcCartesianPointList2D,
      IfcCartesianPointList3D,
      IfcCartesianTransformationOperator,
      IfcCartesianTransformationOperator2D,
      IfcCartesianTransformationOperator2DnonUniform,
      IfcCartesianTransformationOperator3D,
      IfcCartesianTransformationOperator3DnonUniform,
      IfcCenterLineProfileDef,
      IfcChiller,
      IfcChillerType,
      IfcChimney,
      IfcChimneyType,
      IfcCircle,
      IfcCircleHollowProfileDef,
      IfcCircleProfileDef,
      IfcCivilElement,
      IfcCivilElementType,
      IfcClassification,
      IfcClassificationReference,
      IfcClosedShell,
      IfcCoil,
      IfcCoilType,
      IfcColourRgb,
      IfcColourRgbList,
      IfcColourSpecification,
      IfcColumn,
      IfcColumnStandardCase,
      IfcColumnType,
      IfcCommunicationsAppliance,
      IfcCommunicationsApplianceType,
      IfcComplexProperty,
      IfcComplexPropertyTemplate,
      IfcCompositeCurve,
      IfcCompositeCurveOnSurface,
      IfcCompositeCurveSegment,
      IfcCompositeProfileDef,
      IfcCompressor,
      IfcCompressorType,
      IfcCondenser,
      IfcCondenserType,
      IfcConic,
      IfcConnectedFaceSet,
      IfcConnectionCurveGeometry,
      IfcConnectionGeometry,
      IfcConnectionPointEccentricity,
      IfcConnectionPointGeometry,
      IfcConnectionSurfaceGeometry,
      IfcConnectionVolumeGeometry,
      IfcConstraint,
      IfcConstructionEquipmentResource,
      IfcConstructionEquipmentResourceType,
      IfcConstructionMaterialResource,
      IfcConstructionMaterialResourceType,
      IfcConstructionProductResource,
      IfcConstructionProductResourceType,
      IfcConstructionResource,
      IfcConstructionResourceType,
      IfcContext,
      IfcContextDependentUnit,
      IfcControl,
      IfcController,
      IfcControllerType,
      IfcConversionBasedUnit,
      IfcConversionBasedUnitWithOffset,
      IfcCooledBeam,
      IfcCooledBeamType,
      IfcCoolingTower,
      IfcCoolingTowerType,
      IfcCoordinateOperation,
      IfcCoordinateReferenceSystem,
      IfcCostItem,
      IfcCostSchedule,
      IfcCostValue,
      IfcCovering,
      IfcCoveringType,
      IfcCrewResource,
      IfcCrewResourceType,
      IfcCsgPrimitive3D,
      IfcCsgSolid,
      IfcCShapeProfileDef,
      IfcCurrencyRelationship,
      IfcCurtainWall,
      IfcCurtainWallType,
      IfcCurve,
      IfcCurveBoundedPlane,
      IfcCurveBoundedSurface,
      IfcCurveStyle,
      IfcCurveStyleFont,
      IfcCurveStyleFontAndScaling,
      IfcCurveStyleFontPattern,
      IfcCylindricalSurface,
      IfcDamper,
      IfcDamperType,
      IfcDerivedProfileDef,
      IfcDerivedUnit,
      IfcDerivedUnitElement,
      IfcDimensionalExponents,
      IfcDirection,
      IfcDiscreteAccessory,
      IfcDiscreteAccessoryType,
      IfcDistributionChamberElement,
      IfcDistributionChamberElementType,
      IfcDistributionCircuit,
      IfcDistributionControlElement,
      IfcDistributionControlElementType,
      IfcDistributionElement,
      IfcDistributionElementType,
      IfcDistributionFlowElement,
      IfcDistributionFlowElementType,
      IfcDistributionPort,
      IfcDistributionSystem,
      IfcDocumentInformation,
      IfcDocumentInformationRelationship,
      IfcDocumentReference,
      IfcDoor,
      IfcDoorLiningProperties,
      IfcDoorPanelProperties,
      IfcDoorStandardCase,
      IfcDoorStyle,
      IfcDoorType,
      IfcDraughtingPreDefinedColour,
      IfcDraughtingPreDefinedCurveFont,
      IfcDuctFitting,
      IfcDuctFittingType,
      IfcDuctSegment,
      IfcDuctSegmentType,
      IfcDuctSilencer,
      IfcDuctSilencerType,
      IfcEdge,
      IfcEdgeCurve,
      IfcEdgeLoop,
      IfcElectricAppliance,
      IfcElectricApplianceType,
      IfcElectricDistributionBoard,
      IfcElectricDistributionBoardType,
      IfcElectricFlowStorageDevice,
      IfcElectricFlowStorageDeviceType,
      IfcElectricGenerator,
      IfcElectricGeneratorType,
      IfcElectricMotor,
      IfcElectricMotorType,
      IfcElectricTimeControl,
      IfcElectricTimeControlType,
      IfcElement,
      IfcElementarySurface,
      IfcElementAssembly,
      IfcElementAssemblyType,
      IfcElementComponent,
      IfcElementComponentType,
      IfcElementQuantity,
      IfcElementType,
      IfcEllipse,
      IfcEllipseProfileDef,
      IfcEnergyConversionDevice,
      IfcEnergyConversionDeviceType,
      IfcEngine,
      IfcEngineType,
      IfcEvaporativeCooler,
      IfcEvaporativeCoolerType,
      IfcEvaporator,
      IfcEvaporatorType,
      IfcEvent,
      IfcEventTime,
      IfcEventType,
      IfcExtendedProperties,
      IfcExternalInformation,
      IfcExternallyDefinedHatchStyle,
      IfcExternallyDefinedSurfaceStyle,
      IfcExternallyDefinedTextFont,
      IfcExternalReference,
      IfcExternalReferenceRelationship,
      IfcExternalSpatialElement,
      IfcExternalSpatialStructureElement,
      IfcExtrudedAreaSolid,
      IfcExtrudedAreaSolidTapered,
      IfcFace,
      IfcFaceBasedSurfaceModel,
      IfcFaceBound,
      IfcFaceOuterBound,
      IfcFaceSurface,
      IfcFacetedBrep,
      IfcFacetedBrepWithVoids,
      IfcFailureConnectionCondition,
      IfcFan,
      IfcFanType,
      IfcFastener,
      IfcFastenerType,
      IfcFeatureElement,
      IfcFeatureElementAddition,
      IfcFeatureElementSubtraction,
      IfcFillAreaStyle,
      IfcFillAreaStyleHatching,
      IfcFillAreaStyleTiles,
      IfcFilter,
      IfcFilterType,
      IfcFireSuppressionTerminal,
      IfcFireSuppressionTerminalType,
      IfcFixedReferenceSweptAreaSolid,
      IfcFlowController,
      IfcFlowControllerType,
      IfcFlowFitting,
      IfcFlowFittingType,
      IfcFlowInstrument,
      IfcFlowInstrumentType,
      IfcFlowMeter,
      IfcFlowMeterType,
      IfcFlowMovingDevice,
      IfcFlowMovingDeviceType,
      IfcFlowSegment,
      IfcFlowSegmentType,
      IfcFlowStorageDevice,
      IfcFlowStorageDeviceType,
      IfcFlowTerminal,
      IfcFlowTerminalType,
      IfcFlowTreatmentDevice,
      IfcFlowTreatmentDeviceType,
      IfcFooting,
      IfcFootingType,
      IfcFurnishingElement,
      IfcFurnishingElementType,
      IfcFurniture,
      IfcFurnitureType,
      IfcGeographicElement,
      IfcGeographicElementType,
      IfcGeometricCurveSet,
      IfcGeometricRepresentationContext,
      IfcGeometricRepresentationItem,
      IfcGeometricRepresentationSubContext,
      IfcGeometricSet,
      IfcGrid,
      IfcGridAxis,
      IfcGridPlacement,
      IfcGroup,
      IfcHalfSpaceSolid,
      IfcHeatExchanger,
      IfcHeatExchangerType,
      IfcHumidifier,
      IfcHumidifierType,
      IfcImageTexture,
      IfcIndexedColourMap,
      IfcIndexedPolyCurve,
      IfcIndexedPolygonalFace,
      IfcIndexedPolygonalFaceWithVoids,
      IfcIndexedTextureMap,
      IfcIndexedTriangleTextureMap,
      IfcInterceptor,
      IfcInterceptorType,
      IfcIntersectionCurve,
      IfcInventory,
      IfcIrregularTimeSeries,
      IfcIrregularTimeSeriesValue,
      IfcIShapeProfileDef,
      IfcJunctionBox,
      IfcJunctionBoxType,
      IfcLaborResource,
      IfcLaborResourceType,
      IfcLagTime,
      IfcLamp,
      IfcLampType,
      IfcLibraryInformation,
      IfcLibraryReference,
      IfcLightDistributionData,
      IfcLightFixture,
      IfcLightFixtureType,
      IfcLightIntensityDistribution,
      IfcLightSource,
      IfcLightSourceAmbient,
      IfcLightSourceDirectional,
      IfcLightSourceGoniometric,
      IfcLightSourcePositional,
      IfcLightSourceSpot,
      IfcLine,
      IfcLocalPlacement,
      IfcLoop,
      IfcLShapeProfileDef,
      IfcManifoldSolidBrep,
      IfcMapConversion,
      IfcMappedItem,
      IfcMaterial,
      IfcMaterialClassificationRelationship,
      IfcMaterialConstituent,
      IfcMaterialConstituentSet,
      IfcMaterialDefinition,
      IfcMaterialDefinitionRepresentation,
      IfcMaterialLayer,
      IfcMaterialLayerSet,
      IfcMaterialLayerSetUsage,
      IfcMaterialLayerWithOffsets,
      IfcMaterialList,
      IfcMaterialProfile,
      IfcMaterialProfileSet,
      IfcMaterialProfileSetUsage,
      IfcMaterialProfileSetUsageTapering,
      IfcMaterialProfileWithOffsets,
      IfcMaterialProperties,
      IfcMaterialRelationship,
      IfcMaterialUsageDefinition,
      IfcMeasureWithUnit,
      IfcMechanicalFastener,
      IfcMechanicalFastenerType,
      IfcMedicalDevice,
      IfcMedicalDeviceType,
      IfcMember,
      IfcMemberStandardCase,
      IfcMemberType,
      IfcMetric,
      IfcMirroredProfileDef,
      IfcMonetaryUnit,
      IfcMotorConnection,
      IfcMotorConnectionType,
      IfcNamedUnit,
      IfcObject,
      IfcObjectDefinition,
      IfcObjective,
      IfcObjectPlacement,
      IfcOccupant,
      IfcOffsetCurve2D,
      IfcOffsetCurve3D,
      IfcOpeningElement,
      IfcOpeningStandardCase,
      IfcOpenShell,
      IfcOrganization,
      IfcOrganizationRelationship,
      IfcOrientedEdge,
      IfcOuterBoundaryCurve,
      IfcOutlet,
      IfcOutletType,
      IfcOwnerHistory,
      IfcParameterizedProfileDef,
      IfcPath,
      IfcPcurve,
      IfcPerformanceHistory,
      IfcPermeableCoveringProperties,
      IfcPermit,
      IfcPerson,
      IfcPersonAndOrganization,
      IfcPhysicalComplexQuantity,
      IfcPhysicalQuantity,
      IfcPhysicalSimpleQuantity,
      IfcPile,
      IfcPileType,
      IfcPipeFitting,
      IfcPipeFittingType,
      IfcPipeSegment,
      IfcPipeSegmentType,
      IfcPixelTexture,
      IfcPlacement,
      IfcPlanarBox,
      IfcPlanarExtent,
      IfcPlane,
      IfcPlate,
      IfcPlateStandardCase,
      IfcPlateType,
      IfcPoint,
      IfcPointOnCurve,
      IfcPointOnSurface,
      IfcPolygonalBoundedHalfSpace,
      IfcPolygonalFaceSet,
      IfcPolyline,
      IfcPolyLoop,
      IfcPort,
      IfcPostalAddress,
      IfcPreDefinedColour,
      IfcPreDefinedCurveFont,
      IfcPreDefinedItem,
      IfcPreDefinedProperties,
      IfcPreDefinedPropertySet,
      IfcPreDefinedTextFont,
      IfcPresentationItem,
      IfcPresentationLayerAssignment,
      IfcPresentationLayerWithStyle,
      IfcPresentationStyle,
      IfcPresentationStyleAssignment,
      IfcProcedure,
      IfcProcedureType,
      IfcProcess,
      IfcProduct,
      IfcProductDefinitionShape,
      IfcProductRepresentation,
      IfcProfileDef,
      IfcProfileProperties,
      IfcProject,
      IfcProjectedCRS,
      IfcProjectionElement,
      IfcProjectLibrary,
      IfcProjectOrder,
      IfcProperty,
      IfcPropertyAbstraction,
      IfcPropertyBoundedValue,
      IfcPropertyDefinition,
      IfcPropertyDependencyRelationship,
      IfcPropertyEnumeratedValue,
      IfcPropertyEnumeration,
      IfcPropertyListValue,
      IfcPropertyReferenceValue,
      IfcPropertySet,
      IfcPropertySetDefinition,
      IfcPropertySetTemplate,
      IfcPropertySingleValue,
      IfcPropertyTableValue,
      IfcPropertyTemplate,
      IfcPropertyTemplateDefinition,
      IfcProtectiveDevice,
      IfcProtectiveDeviceTrippingUnit,
      IfcProtectiveDeviceTrippingUnitType,
      IfcProtectiveDeviceType,
      IfcProxy,
      IfcPump,
      IfcPumpType,
      IfcQuantityArea,
      IfcQuantityCount,
      IfcQuantityLength,
      IfcQuantitySet,
      IfcQuantityTime,
      IfcQuantityVolume,
      IfcQuantityWeight,
      IfcRailing,
      IfcRailingType,
      IfcRamp,
      IfcRampFlight,
      IfcRampFlightType,
      IfcRampType,
      IfcRationalBSplineCurveWithKnots,
      IfcRationalBSplineSurfaceWithKnots,
      IfcRectangleHollowProfileDef,
      IfcRectangleProfileDef,
      IfcRectangularPyramid,
      IfcRectangularTrimmedSurface,
      IfcRecurrencePattern,
      IfcReference,
      IfcRegularTimeSeries,
      IfcReinforcementBarProperties,
      IfcReinforcementDefinitionProperties,
      IfcReinforcingBar,
      IfcReinforcingBarType,
      IfcReinforcingElement,
      IfcReinforcingElementType,
      IfcReinforcingMesh,
      IfcReinforcingMeshType,
      IfcRelAggregates,
      IfcRelAssigns,
      IfcRelAssignsToActor,
      IfcRelAssignsToControl,
      IfcRelAssignsToGroup,
      IfcRelAssignsToGroupByFactor,
      IfcRelAssignsToProcess,
      IfcRelAssignsToProduct,
      IfcRelAssignsToResource,
      IfcRelAssociates,
      IfcRelAssociatesApproval,
      IfcRelAssociatesClassification,
      IfcRelAssociatesConstraint,
      IfcRelAssociatesDocument,
      IfcRelAssociatesLibrary,
      IfcRelAssociatesMaterial,
      IfcRelationship,
      IfcRelConnects,
      IfcRelConnectsElements,
      IfcRelConnectsPathElements,
      IfcRelConnectsPorts,
      IfcRelConnectsPortToElement,
      IfcRelConnectsStructuralActivity,
      IfcRelConnectsStructuralMember,
      IfcRelConnectsWithEccentricity,
      IfcRelConnectsWithRealizingElements,
      IfcRelContainedInSpatialStructure,
      IfcRelCoversBldgElements,
      IfcRelCoversSpaces,
      IfcRelDeclares,
      IfcRelDecomposes,
      IfcRelDefines,
      IfcRelDefinesByObject,
      IfcRelDefinesByProperties,
      IfcRelDefinesByTemplate,
      IfcRelDefinesByType,
      IfcRelFillsElement,
      IfcRelFlowControlElements,
      IfcRelInterferesElements,
      IfcRelNests,
      IfcRelProjectsElement,
      IfcRelReferencedInSpatialStructure,
      IfcRelSequence,
      IfcRelServicesBuildings,
      IfcRelSpaceBoundary,
      IfcRelSpaceBoundary1stLevel,
      IfcRelSpaceBoundary2ndLevel,
      IfcRelVoidsElement,
      IfcReparametrisedCompositeCurveSegment,
      IfcRepresentation,
      IfcRepresentationContext,
      IfcRepresentationItem,
      IfcRepresentationMap,
      IfcResource,
      IfcResourceApprovalRelationship,
      IfcResourceConstraintRelationship,
      IfcResourceLevelRelationship,
      IfcResourceTime,
      IfcRevolvedAreaSolid,
      IfcRevolvedAreaSolidTapered,
      IfcRightCircularCone,
      IfcRightCircularCylinder,
      IfcRoof,
      IfcRoofType,
      IfcRoot,
      IfcRoundedRectangleProfileDef,
      IfcSanitaryTerminal,
      IfcSanitaryTerminalType,
      IfcSchedulingTime,
      IfcSeamCurve,
      IfcSectionedSpine,
      IfcSectionProperties,
      IfcSectionReinforcementProperties,
      IfcSensor,
      IfcSensorType,
      IfcShadingDevice,
      IfcShadingDeviceType,
      IfcShapeAspect,
      IfcShapeModel,
      IfcShapeRepresentation,
      IfcShellBasedSurfaceModel,
      IfcSimpleProperty,
      IfcSimplePropertyTemplate,
      IfcSite,
      IfcSIUnit,
      IfcSlab,
      IfcSlabElementedCase,
      IfcSlabStandardCase,
      IfcSlabType,
      IfcSlippageConnectionCondition,
      IfcSolarDevice,
      IfcSolarDeviceType,
      IfcSolidModel,
      IfcSpace,
      IfcSpaceHeater,
      IfcSpaceHeaterType,
      IfcSpaceType,
      IfcSpatialElement,
      IfcSpatialElementType,
      IfcSpatialStructureElement,
      IfcSpatialStructureElementType,
      IfcSpatialZone,
      IfcSpatialZoneType,
      IfcSphere,
      IfcSphericalSurface,
      IfcStackTerminal,
      IfcStackTerminalType,
      IfcStair,
      IfcStairFlight,
      IfcStairFlightType,
      IfcStairType,
      IfcStructuralAction,
      IfcStructuralActivity,
      IfcStructuralAnalysisModel,
      IfcStructuralConnection,
      IfcStructuralConnectionCondition,
      IfcStructuralCurveAction,
      IfcStructuralCurveConnection,
      IfcStructuralCurveMember,
      IfcStructuralCurveMemberVarying,
      IfcStructuralCurveReaction,
      IfcStructuralItem,
      IfcStructuralLinearAction,
      IfcStructuralLoad,
      IfcStructuralLoadCase,
      IfcStructuralLoadConfiguration,
      IfcStructuralLoadGroup,
      IfcStructuralLoadLinearForce,
      IfcStructuralLoadOrResult,
      IfcStructuralLoadPlanarForce,
      IfcStructuralLoadSingleDisplacement,
      IfcStructuralLoadSingleDisplacementDistortion,
      IfcStructuralLoadSingleForce,
      IfcStructuralLoadSingleForceWarping,
      IfcStructuralLoadStatic,
      IfcStructuralLoadTemperature,
      IfcStructuralMember,
      IfcStructuralPlanarAction,
      IfcStructuralPointAction,
      IfcStructuralPointConnection,
      IfcStructuralPointReaction,
      IfcStructuralReaction,
      IfcStructuralResultGroup,
      IfcStructuralSurfaceAction,
      IfcStructuralSurfaceConnection,
      IfcStructuralSurfaceMember,
      IfcStructuralSurfaceMemberVarying,
      IfcStructuralSurfaceReaction,
      IfcStyledItem,
      IfcStyledRepresentation,
      IfcStyleModel,
      IfcSubContractResource,
      IfcSubContractResourceType,
      IfcSubedge,
      IfcSurface,
      IfcSurfaceCurve,
      IfcSurfaceCurveSweptAreaSolid,
      IfcSurfaceFeature,
      IfcSurfaceOfLinearExtrusion,
      IfcSurfaceOfRevolution,
      IfcSurfaceReinforcementArea,
      IfcSurfaceStyle,
      IfcSurfaceStyleLighting,
      IfcSurfaceStyleRefraction,
      IfcSurfaceStyleRendering,
      IfcSurfaceStyleShading,
      IfcSurfaceStyleWithTextures,
      IfcSurfaceTexture,
      IfcSweptAreaSolid,
      IfcSweptDiskSolid,
      IfcSweptDiskSolidPolygonal,
      IfcSweptSurface,
      IfcSwitchingDevice,
      IfcSwitchingDeviceType,
      IfcSystem,
      IfcSystemFurnitureElement,
      IfcSystemFurnitureElementType,
      IfcTable,
      IfcTableColumn,
      IfcTableRow,
      IfcTank,
      IfcTankType,
      IfcTask,
      IfcTaskTime,
      IfcTaskTimeRecurring,
      IfcTaskType,
      IfcTelecomAddress,
      IfcTendon,
      IfcTendonAnchor,
      IfcTendonAnchorType,
      IfcTendonType,
      IfcTessellatedFaceSet,
      IfcTessellatedItem,
      IfcTextLiteral,
      IfcTextLiteralWithExtent,
      IfcTextStyle,
      IfcTextStyleFontModel,
      IfcTextStyleForDefinedFont,
      IfcTextStyleTextModel,
      IfcTextureCoordinate,
      IfcTextureCoordinateGenerator,
      IfcTextureMap,
      IfcTextureVertex,
      IfcTextureVertexList,
      IfcTimePeriod,
      IfcTimeSeries,
      IfcTimeSeriesValue,
      IfcTopologicalRepresentationItem,
      IfcTopologyRepresentation,
      IfcToroidalSurface,
      IfcTransformer,
      IfcTransformerType,
      IfcTransportElement,
      IfcTransportElementType,
      IfcTrapeziumProfileDef,
      IfcTriangulatedFaceSet,
      IfcTrimmedCurve,
      IfcTShapeProfileDef,
      IfcTubeBundle,
      IfcTubeBundleType,
      IfcTypeObject,
      IfcTypeProcess,
      IfcTypeProduct,
      IfcTypeResource,
      IfcUnitaryControlElement,
      IfcUnitaryControlElementType,
      IfcUnitaryEquipment,
      IfcUnitaryEquipmentType,
      IfcUnitAssignment,
      IfcUShapeProfileDef,
      IfcValve,
      IfcValveType,
      IfcVector,
      IfcVertex,
      IfcVertexLoop,
      IfcVertexPoint,
      IfcVibrationIsolator,
      IfcVibrationIsolatorType,
      IfcVirtualElement,
      IfcVirtualGridIntersection,
      IfcVoidingFeature,
      IfcWall,
      IfcWallElementedCase,
      IfcWallStandardCase,
      IfcWallType,
      IfcWasteTerminal,
      IfcWasteTerminalType,
      IfcWindow,
      IfcWindowLiningProperties,
      IfcWindowPanelProperties,
      IfcWindowStandardCase,
      IfcWindowStyle,
      IfcWindowType,
      IfcWorkCalendar,
      IfcWorkControl,
      IfcWorkPlan,
      IfcWorkSchedule,
      IfcWorkTime,
      IfcZone,
      IfcZShapeProfileDef,
      Unknown
   }
}
