﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GenFx {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("GenFx.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Component is not initialized..
        /// </summary>
        internal static string ComponentNotInitialized {
            get {
                return ResourceManager.GetString("ComponentNotInitialized", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Crossover Operator.
        /// </summary>
        internal static string CrossoverCommonName {
            get {
                return ResourceManager.GetString("CrossoverCommonName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Elitism Strategy.
        /// </summary>
        internal static string ElitismCommonName {
            get {
                return ResourceManager.GetString("ElitismCommonName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Entity.
        /// </summary>
        internal static string EntityCommonName {
            get {
                return ResourceManager.GetString("EntityCommonName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The &apos;{0}&apos; method must be called before the algorithm can be executed..
        /// </summary>
        internal static string ErrorMsg_AlgorithmNotInitialized {
            get {
                return ResourceManager.GetString("ErrorMsg_AlgorithmNotInitialized", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Property &apos;{0}&apos; not found on type &apos;{1}&apos;..
        /// </summary>
        internal static string ErrorMsg_ComponentConfigurationPropertyNotFound {
            get {
                return ResourceManager.GetString("ErrorMsg_ComponentConfigurationPropertyNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Property &apos;{0}&apos; is defined as a configuration property but has no setter. A setter must be defined on this property..
        /// </summary>
        internal static string ErrorMsg_ConfigurationPropertyHasNoSetter {
            get {
                return ResourceManager.GetString("ErrorMsg_ConfigurationPropertyHasNoSetter", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Property &apos;{0}.{1}&apos; must have both a getter and setter since it is adorned with &apos;{2}&apos;..
        /// </summary>
        internal static string ErrorMsg_ConfigurationPropertyNoGetterNoSetter {
            get {
                return ResourceManager.GetString("ErrorMsg_ConfigurationPropertyNoGetterNoSetter", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The object returned from &apos;{0}.{1}&apos; must not be null..
        /// </summary>
        internal static string ErrorMsg_CreateNewComponentNull {
            get {
                return ResourceManager.GetString("ErrorMsg_CreateNewComponentNull", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The object returned from &apos;{0}.{1}&apos; is of an incorrect type.
        ///Expected type: &apos;{0}&apos;
        ///Actual type: &apos;{2}&apos;.
        /// </summary>
        internal static string ErrorMsg_CreateNewComponentWrongType {
            get {
                return ResourceManager.GetString("ErrorMsg_CreateNewComponentWrongType", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to A cycle was detected in the dependency graph associated with metric type &apos;{0}&apos;.  This occurs when metrics depend upon each other either directly or indirectly.  Remove this dependency..
        /// </summary>
        internal static string ErrorMsg_CycleInMetricDependencyGraph {
            get {
                return ResourceManager.GetString("ErrorMsg_CycleInMetricDependencyGraph", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Object of type &apos;{0}&apos; already exists in collection..
        /// </summary>
        internal static string ErrorMsg_DuplicateConfiguration {
            get {
                return ResourceManager.GetString("ErrorMsg_DuplicateConfiguration", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Entity list cannot be empty..
        /// </summary>
        internal static string ErrorMsg_EntityListEmpty {
            get {
                return ResourceManager.GetString("ErrorMsg_EntityListEmpty", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &apos;{0}&apos; is not an enum type..
        /// </summary>
        internal static string ErrorMsg_EnumValidator_NotEnumType {
            get {
                return ResourceManager.GetString("ErrorMsg_EnumValidator_NotEnumType", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to An error occurred while instantiating component associated with configuration &apos;{0}&apos;.\n\nException detail: {1}.
        /// </summary>
        internal static string ErrorMsg_ErrorCreatingComponent {
            get {
                return ResourceManager.GetString("ErrorMsg_ErrorCreatingComponent", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The type &apos;{0}&apos; is not derived from &apos;{1}&apos;..
        /// </summary>
        internal static string ErrorMsg_ExternalValidator_InvalidTargetType {
            get {
                return ResourceManager.GetString("ErrorMsg_ExternalValidator_InvalidTargetType", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to A public property named &apos;{0}&apos; does not exist on type &apos;{1}&apos;..
        /// </summary>
        internal static string ErrorMsg_ExternalValidator_PropertyDoesNotExist {
            get {
                return ResourceManager.GetString("ErrorMsg_ExternalValidator_PropertyDoesNotExist", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Type must derive from &apos;{0}&apos;..
        /// </summary>
        internal static string ErrorMsg_IncorrectDerivedType {
            get {
                return ResourceManager.GetString("ErrorMsg_IncorrectDerivedType", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Property &apos;{0}&apos; must be equal to &apos;{1}&apos;..
        /// </summary>
        internal static string ErrorMsg_InvalidBooleanProperty {
            get {
                return ResourceManager.GetString("ErrorMsg_InvalidBooleanProperty", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Configuration property &apos;{0}&apos; must be a value of type Double and between {1} ({2}) and {3} ({4})..
        /// </summary>
        internal static string ErrorMsg_InvalidDoubleProperty {
            get {
                return ResourceManager.GetString("ErrorMsg_InvalidDoubleProperty", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to When the minimum and maximum values are equal, they must also both be set to be inclusive..
        /// </summary>
        internal static string ErrorMsg_InvalidDoubleProperty_EqualMinMaxButNotInclusive {
            get {
                return ResourceManager.GetString("ErrorMsg_InvalidDoubleProperty_EqualMinMaxButNotInclusive", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The {0} value is invalid.  Valid values are {1}..
        /// </summary>
        internal static string ErrorMsg_InvalidEnum {
            get {
                return ResourceManager.GetString("ErrorMsg_InvalidEnum", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Generation index must be greater than or equal to zero..
        /// </summary>
        internal static string ErrorMsg_InvalidGenerationIndex {
            get {
                return ResourceManager.GetString("ErrorMsg_InvalidGenerationIndex", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Configuration property &apos;{0}&apos; must be an integral value between {1} and {2}.
        ///.
        /// </summary>
        internal static string ErrorMsg_InvalidIntegerProperty {
            get {
                return ResourceManager.GetString("ErrorMsg_InvalidIntegerProperty", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The minimum value cannot be greater than the maximum value..
        /// </summary>
        internal static string ErrorMsg_InvalidMinMaxRange {
            get {
                return ResourceManager.GetString("ErrorMsg_InvalidMinMaxRange", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Population index must be greater than or equal to zero..
        /// </summary>
        internal static string ErrorMsg_InvalidPopulationIndex {
            get {
                return ResourceManager.GetString("ErrorMsg_InvalidPopulationIndex", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Configuration property &apos;{0}&apos; must be equal to {1}..
        /// </summary>
        internal static string ErrorMsg_InvalidProperty_Exact {
            get {
                return ResourceManager.GetString("ErrorMsg_InvalidProperty_Exact", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Type must be derived from &apos;{0}&apos;..
        /// </summary>
        internal static string ErrorMsg_InvalidType {
            get {
                return ResourceManager.GetString("ErrorMsg_InvalidType", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The {0} declared the configuration value as valid but the value could not be converted to the target type &apos;{1}&apos;.  Either fix the implementation of the validator or implement a {2}..
        /// </summary>
        internal static string ErrorMsg_IsValidButCannotConvert {
            get {
                return ResourceManager.GetString("ErrorMsg_IsValidButCannotConvert", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to List item &apos;{0}&apos; cannot be compared because it does not implement System.IComparable..
        /// </summary>
        internal static string ErrorMsg_ListItemNotComparable {
            get {
                return ResourceManager.GetString("ErrorMsg_ListItemNotComparable", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The &apos;{0}&apos; type requires a {1} of type &apos;{2}&apos; but the algorithm is not configured with that type..
        /// </summary>
        internal static string ErrorMsg_NoRequiredConfigurableType {
            get {
                return ResourceManager.GetString("ErrorMsg_NoRequiredConfigurableType", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The result of invoking method &apos;{0}.{1}&apos; is null which is an invalid value.  It must return a non-null value..
        /// </summary>
        internal static string ErrorMsg_NullReturnValue {
            get {
                return ResourceManager.GetString("ErrorMsg_NullReturnValue", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Object must be of type &apos;{0}&apos;..
        /// </summary>
        internal static string ErrorMsg_ObjectIsWrongType {
            get {
                return ResourceManager.GetString("ErrorMsg_ObjectIsWrongType", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The specified component must either by of type &apos;{0}&apos; or &apos;{0}&apos; and have a reference to a non-null &apos;{0}&apos;..
        /// </summary>
        internal static string ErrorMsg_RequiredComponentValidator_NoAlgorithm {
            get {
                return ResourceManager.GetString("ErrorMsg_RequiredComponentValidator_NoAlgorithm", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Configuration property &apos;{0}&apos; must be set..
        /// </summary>
        internal static string ErrorMsg_RequiredPropertyNotSet {
            get {
                return ResourceManager.GetString("ErrorMsg_RequiredPropertyNotSet", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to String value cannot be null or empty..
        /// </summary>
        internal static string ErrorMsg_StringNullOrEmpty {
            get {
                return ResourceManager.GetString("ErrorMsg_StringNullOrEmpty", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to exclusive.
        /// </summary>
        internal static string ExclusiveLabel {
            get {
                return ResourceManager.GetString("ExclusiveLabel", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Fitness Evaluator.
        /// </summary>
        internal static string FitnessEvaluatorCommonName {
            get {
                return ResourceManager.GetString("FitnessEvaluatorCommonName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Fitness Scaling Strategy.
        /// </summary>
        internal static string FitnessScalingCommonName {
            get {
                return ResourceManager.GetString("FitnessScalingCommonName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Genetic Algorithm.
        /// </summary>
        internal static string GeneticAlgorithmCommonName {
            get {
                return ResourceManager.GetString("GeneticAlgorithmCommonName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to inclusive.
        /// </summary>
        internal static string InclusiveLabel {
            get {
                return ResourceManager.GetString("InclusiveLabel", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Metric.
        /// </summary>
        internal static string MetricCommonName {
            get {
                return ResourceManager.GetString("MetricCommonName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Mutation Operator.
        /// </summary>
        internal static string MutationCommonName {
            get {
                return ResourceManager.GetString("MutationCommonName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Plugin.
        /// </summary>
        internal static string PluginCommonName {
            get {
                return ResourceManager.GetString("PluginCommonName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Population.
        /// </summary>
        internal static string PopulationCommonName {
            get {
                return ResourceManager.GetString("PopulationCommonName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Selection Operator.
        /// </summary>
        internal static string SelectionCommonName {
            get {
                return ResourceManager.GetString("SelectionCommonName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Terminator.
        /// </summary>
        internal static string TerminatorCommonName {
            get {
                return ResourceManager.GetString("TerminatorCommonName", resourceCulture);
            }
        }
    }
}
