#define ICALL_TABLE_corlib 1

static int corlib_icall_indexes [] = {
220,
233,
234,
235,
236,
237,
238,
239,
240,
241,
244,
245,
246,
423,
424,
425,
453,
454,
455,
482,
483,
484,
601,
602,
603,
606,
644,
645,
646,
647,
648,
652,
654,
656,
658,
664,
672,
673,
674,
675,
676,
677,
678,
679,
680,
681,
682,
683,
684,
685,
686,
687,
688,
690,
691,
692,
693,
694,
695,
696,
793,
794,
795,
796,
797,
798,
799,
800,
801,
802,
803,
804,
805,
806,
807,
808,
809,
811,
812,
813,
814,
815,
816,
817,
879,
888,
889,
960,
967,
970,
972,
977,
978,
980,
981,
985,
986,
988,
989,
992,
993,
994,
997,
999,
1002,
1004,
1006,
1015,
1084,
1086,
1088,
1098,
1099,
1100,
1102,
1108,
1109,
1110,
1111,
1112,
1120,
1121,
1122,
1126,
1127,
1129,
1133,
1134,
1135,
1432,
1622,
1623,
10114,
10115,
10117,
10118,
10119,
10120,
10121,
10122,
10124,
10125,
10126,
10127,
10128,
10146,
10148,
10156,
10158,
10160,
10162,
10165,
10216,
10217,
10219,
10220,
10221,
10222,
10223,
10225,
10227,
11430,
11434,
11436,
11437,
11438,
11439,
11881,
11882,
11883,
11884,
11902,
11903,
11904,
11950,
12018,
12021,
12030,
12031,
12032,
12033,
12034,
12035,
12382,
12383,
12388,
12389,
12424,
12467,
12474,
12481,
12492,
12496,
12522,
12605,
12607,
12618,
12620,
12621,
12622,
12629,
12644,
12664,
12665,
12673,
12675,
12682,
12683,
12686,
12688,
12693,
12699,
12700,
12707,
12709,
12721,
12724,
12725,
12726,
12737,
12747,
12753,
12754,
12755,
12757,
12758,
12775,
12777,
12792,
12814,
12815,
12840,
12845,
12875,
12876,
13505,
13519,
13608,
13609,
13831,
13832,
13839,
13840,
13841,
13847,
13917,
14455,
14456,
14886,
14887,
14888,
14894,
14904,
15918,
15939,
15941,
15943,
};
void ves_icall_System_Array_InternalCreate (int,int,int,int,int);
int ves_icall_System_Array_GetCorElementTypeOfElementTypeInternal (int);
int ves_icall_System_Array_IsValueOfElementTypeInternal (int,int);
int ves_icall_System_Array_CanChangePrimitive (int,int,int);
int ves_icall_System_Array_FastCopy (int,int,int,int,int);
int ves_icall_System_Array_GetLengthInternal_raw (int,int,int);
int ves_icall_System_Array_GetLowerBoundInternal_raw (int,int,int);
void ves_icall_System_Array_GetGenericValue_icall (int,int,int);
void ves_icall_System_Array_GetValueImpl_raw (int,int,int,int);
void ves_icall_System_Array_SetGenericValue_icall (int,int,int);
void ves_icall_System_Array_SetValueImpl_raw (int,int,int,int);
void ves_icall_System_Array_InitializeInternal_raw (int,int);
void ves_icall_System_Array_SetValueRelaxedImpl_raw (int,int,int,int);
void ves_icall_System_Runtime_RuntimeImports_ZeroMemory (int,int);
void ves_icall_System_Runtime_RuntimeImports_Memmove (int,int,int);
void ves_icall_System_Buffer_BulkMoveWithWriteBarrier (int,int,int,int);
int ves_icall_System_Delegate_AllocDelegateLike_internal_raw (int,int);
int ves_icall_System_Delegate_CreateDelegate_internal_raw (int,int,int,int,int);
int ves_icall_System_Delegate_GetVirtualMethod_internal_raw (int,int);
void ves_icall_System_Enum_GetEnumValuesAndNames_raw (int,int,int,int);
int ves_icall_System_Enum_InternalGetCorElementType (int);
void ves_icall_System_Enum_InternalGetUnderlyingType_raw (int,int,int);
int ves_icall_System_Environment_get_ProcessorCount ();
int ves_icall_System_Environment_get_TickCount ();
int64_t ves_icall_System_Environment_get_TickCount64 ();
void ves_icall_System_Environment_FailFast_raw (int,int,int,int);
int ves_icall_System_GC_GetCollectionCount (int);
int ves_icall_System_GC_GetMaxGeneration ();
void ves_icall_System_GC_register_ephemeron_array_raw (int,int);
int ves_icall_System_GC_get_ephemeron_tombstone_raw (int);
int64_t ves_icall_System_GC_GetTotalAllocatedBytes_raw (int,int);
void ves_icall_System_GC_SuppressFinalize_raw (int,int);
void ves_icall_System_GC_ReRegisterForFinalize_raw (int,int);
void ves_icall_System_GC_GetGCMemoryInfo (int,int,int,int,int,int);
int ves_icall_System_GC_AllocPinnedArray_raw (int,int,int);
int ves_icall_System_Object_MemberwiseClone_raw (int,int);
double ves_icall_System_Math_Acos (double);
double ves_icall_System_Math_Acosh (double);
double ves_icall_System_Math_Asin (double);
double ves_icall_System_Math_Asinh (double);
double ves_icall_System_Math_Atan (double);
double ves_icall_System_Math_Atan2 (double,double);
double ves_icall_System_Math_Atanh (double);
double ves_icall_System_Math_Cbrt (double);
double ves_icall_System_Math_Ceiling (double);
double ves_icall_System_Math_Cos (double);
double ves_icall_System_Math_Cosh (double);
double ves_icall_System_Math_Exp (double);
double ves_icall_System_Math_Floor (double);
double ves_icall_System_Math_Log (double);
double ves_icall_System_Math_Log10 (double);
double ves_icall_System_Math_Pow (double,double);
double ves_icall_System_Math_Sin (double);
double ves_icall_System_Math_Sinh (double);
double ves_icall_System_Math_Sqrt (double);
double ves_icall_System_Math_Tan (double);
double ves_icall_System_Math_Tanh (double);
double ves_icall_System_Math_FusedMultiplyAdd (double,double,double);
double ves_icall_System_Math_Log2 (double);
double ves_icall_System_Math_ModF (double,int);
float ves_icall_System_MathF_Acos (float);
float ves_icall_System_MathF_Acosh (float);
float ves_icall_System_MathF_Asin (float);
float ves_icall_System_MathF_Asinh (float);
float ves_icall_System_MathF_Atan (float);
float ves_icall_System_MathF_Atan2 (float,float);
float ves_icall_System_MathF_Atanh (float);
float ves_icall_System_MathF_Cbrt (float);
float ves_icall_System_MathF_Ceiling (float);
float ves_icall_System_MathF_Cos (float);
float ves_icall_System_MathF_Cosh (float);
float ves_icall_System_MathF_Exp (float);
float ves_icall_System_MathF_Floor (float);
float ves_icall_System_MathF_Log (float);
float ves_icall_System_MathF_Log10 (float);
float ves_icall_System_MathF_Pow (float,float);
float ves_icall_System_MathF_Sin (float);
float ves_icall_System_MathF_Sinh (float);
float ves_icall_System_MathF_Sqrt (float);
float ves_icall_System_MathF_Tan (float);
float ves_icall_System_MathF_Tanh (float);
float ves_icall_System_MathF_FusedMultiplyAdd (float,float,float);
float ves_icall_System_MathF_Log2 (float);
float ves_icall_System_MathF_ModF (float,int);
int ves_icall_RuntimeMethodHandle_GetFunctionPointer_raw (int,int);
void ves_icall_RuntimeMethodHandle_ReboxFromNullable_raw (int,int,int);
void ves_icall_RuntimeMethodHandle_ReboxToNullable_raw (int,int,int,int);
int ves_icall_RuntimeType_GetCorrespondingInflatedMethod_raw (int,int,int);
void ves_icall_RuntimeType_make_array_type_raw (int,int,int,int);
void ves_icall_RuntimeType_make_byref_type_raw (int,int,int);
void ves_icall_RuntimeType_make_pointer_type_raw (int,int,int);
void ves_icall_RuntimeType_MakeGenericType_raw (int,int,int,int);
int ves_icall_RuntimeType_GetMethodsByName_native_raw (int,int,int,int,int);
int ves_icall_RuntimeType_GetPropertiesByName_native_raw (int,int,int,int,int);
int ves_icall_RuntimeType_GetConstructors_native_raw (int,int,int);
int ves_icall_System_RuntimeType_CreateInstanceInternal_raw (int,int);
void ves_icall_RuntimeType_GetDeclaringMethod_raw (int,int,int);
void ves_icall_System_RuntimeType_getFullName_raw (int,int,int,int,int);
void ves_icall_RuntimeType_GetGenericArgumentsInternal_raw (int,int,int,int);
int ves_icall_RuntimeType_GetGenericParameterPosition (int);
int ves_icall_RuntimeType_GetEvents_native_raw (int,int,int,int);
int ves_icall_RuntimeType_GetFields_native_raw (int,int,int,int,int);
void ves_icall_RuntimeType_GetInterfaces_raw (int,int,int);
int ves_icall_RuntimeType_GetNestedTypes_native_raw (int,int,int,int,int);
void ves_icall_RuntimeType_GetDeclaringType_raw (int,int,int);
void ves_icall_RuntimeType_GetName_raw (int,int,int);
void ves_icall_RuntimeType_GetNamespace_raw (int,int,int);
int ves_icall_RuntimeType_FunctionPointerReturnAndParameterTypes_raw (int,int);
int ves_icall_RuntimeTypeHandle_GetAttributes (int);
int ves_icall_RuntimeTypeHandle_GetMetadataToken_raw (int,int);
void ves_icall_RuntimeTypeHandle_GetGenericTypeDefinition_impl_raw (int,int,int);
int ves_icall_RuntimeTypeHandle_GetCorElementType (int);
int ves_icall_RuntimeTypeHandle_HasInstantiation (int);
int ves_icall_RuntimeTypeHandle_IsInstanceOfType_raw (int,int,int);
int ves_icall_RuntimeTypeHandle_HasReferences_raw (int,int);
int ves_icall_RuntimeTypeHandle_GetArrayRank_raw (int,int);
void ves_icall_RuntimeTypeHandle_GetAssembly_raw (int,int,int);
void ves_icall_RuntimeTypeHandle_GetElementType_raw (int,int,int);
void ves_icall_RuntimeTypeHandle_GetModule_raw (int,int,int);
void ves_icall_RuntimeTypeHandle_GetBaseType_raw (int,int,int);
int ves_icall_RuntimeTypeHandle_type_is_assignable_from_raw (int,int,int);
int ves_icall_RuntimeTypeHandle_IsGenericTypeDefinition (int);
int ves_icall_RuntimeTypeHandle_GetGenericParameterInfo_raw (int,int);
int ves_icall_RuntimeTypeHandle_is_subclass_of_raw (int,int,int);
int ves_icall_RuntimeTypeHandle_IsByRefLike_raw (int,int);
void ves_icall_System_RuntimeTypeHandle_internal_from_name_raw (int,int,int,int,int,int);
int ves_icall_System_String_FastAllocateString_raw (int,int);
int ves_icall_System_String_InternalIsInterned_raw (int,int);
int ves_icall_System_String_InternalIntern_raw (int,int);
int ves_icall_System_Type_internal_from_handle_raw (int,int);
int ves_icall_System_ValueType_InternalGetHashCode_raw (int,int,int);
int ves_icall_System_ValueType_Equals_raw (int,int,int,int);
int ves_icall_System_Threading_Interlocked_CompareExchange_Int (int,int,int);
void ves_icall_System_Threading_Interlocked_CompareExchange_Object (int,int,int,int);
int ves_icall_System_Threading_Interlocked_Decrement_Int (int);
int64_t ves_icall_System_Threading_Interlocked_Decrement_Long (int);
int ves_icall_System_Threading_Interlocked_Increment_Int (int);
int64_t ves_icall_System_Threading_Interlocked_Increment_Long (int);
int ves_icall_System_Threading_Interlocked_Exchange_Int (int,int);
void ves_icall_System_Threading_Interlocked_Exchange_Object (int,int,int);
int64_t ves_icall_System_Threading_Interlocked_CompareExchange_Long (int,int64_t,int64_t);
int64_t ves_icall_System_Threading_Interlocked_Exchange_Long (int,int64_t);
int64_t ves_icall_System_Threading_Interlocked_Read_Long (int);
int ves_icall_System_Threading_Interlocked_Add_Int (int,int);
int64_t ves_icall_System_Threading_Interlocked_Add_Long (int,int64_t);
void ves_icall_System_Threading_Monitor_Monitor_Enter_raw (int,int);
void mono_monitor_exit_icall_raw (int,int);
void ves_icall_System_Threading_Monitor_Monitor_pulse_raw (int,int);
void ves_icall_System_Threading_Monitor_Monitor_pulse_all_raw (int,int);
int ves_icall_System_Threading_Monitor_Monitor_wait_raw (int,int,int,int);
void ves_icall_System_Threading_Monitor_Monitor_try_enter_with_atomic_var_raw (int,int,int,int,int);
int64_t ves_icall_System_Threading_Monitor_Monitor_get_lock_contention_count ();
void ves_icall_System_Threading_Thread_InitInternal_raw (int,int);
int ves_icall_System_Threading_Thread_GetCurrentThread ();
void ves_icall_System_Threading_InternalThread_Thread_free_internal_raw (int,int);
int ves_icall_System_Threading_Thread_GetState_raw (int,int);
void ves_icall_System_Threading_Thread_SetState_raw (int,int,int);
void ves_icall_System_Threading_Thread_ClrState_raw (int,int,int);
void ves_icall_System_Threading_Thread_SetName_icall_raw (int,int,int,int);
int ves_icall_System_Threading_Thread_YieldInternal ();
void ves_icall_System_Threading_Thread_SetPriority_raw (int,int,int);
void ves_icall_System_Runtime_Loader_AssemblyLoadContext_PrepareForAssemblyLoadContextRelease_raw (int,int,int);
int ves_icall_System_Runtime_Loader_AssemblyLoadContext_GetLoadContextForAssembly_raw (int,int);
int ves_icall_System_Runtime_Loader_AssemblyLoadContext_InternalLoadFile_raw (int,int,int,int);
int ves_icall_System_Runtime_Loader_AssemblyLoadContext_InternalInitializeNativeALC_raw (int,int,int,int,int);
int ves_icall_System_Runtime_Loader_AssemblyLoadContext_InternalLoadFromStream_raw (int,int,int,int,int,int);
int ves_icall_System_Runtime_Loader_AssemblyLoadContext_InternalGetLoadedAssemblies_raw (int);
int ves_icall_System_GCHandle_InternalAlloc_raw (int,int,int);
void ves_icall_System_GCHandle_InternalFree_raw (int,int);
int ves_icall_System_GCHandle_InternalGet_raw (int,int);
void ves_icall_System_GCHandle_InternalSet_raw (int,int,int);
int ves_icall_System_Runtime_InteropServices_Marshal_GetLastPInvokeError ();
void ves_icall_System_Runtime_InteropServices_Marshal_SetLastPInvokeError (int);
void ves_icall_System_Runtime_InteropServices_Marshal_StructureToPtr_raw (int,int,int,int);
int ves_icall_System_Runtime_InteropServices_NativeLibrary_LoadByName_raw (int,int,int,int,int,int);
int ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_InternalGetHashCode_raw (int,int);
int ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_GetObjectValue_raw (int,int);
int ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_GetUninitializedObjectInternal_raw (int,int);
void ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_InitializeArray_raw (int,int,int);
int ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_GetSpanDataFrom_raw (int,int,int,int);
void ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_RunClassConstructor_raw (int,int);
int ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_SufficientExecutionStack ();
int ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_InternalBox_raw (int,int,int);
int ves_icall_System_Reflection_Assembly_GetExecutingAssembly_raw (int,int);
int ves_icall_System_Reflection_Assembly_GetEntryAssembly_raw (int);
int ves_icall_System_Reflection_Assembly_InternalLoad_raw (int,int,int,int);
int ves_icall_System_Reflection_Assembly_InternalGetType_raw (int,int,int,int,int,int);
int ves_icall_System_Reflection_AssemblyName_GetNativeName (int);
int ves_icall_MonoCustomAttrs_GetCustomAttributesInternal_raw (int,int,int,int);
int ves_icall_MonoCustomAttrs_GetCustomAttributesDataInternal_raw (int,int);
int ves_icall_MonoCustomAttrs_IsDefinedInternal_raw (int,int,int);
int ves_icall_System_Reflection_FieldInfo_internal_from_handle_type_raw (int,int,int);
int ves_icall_System_Reflection_FieldInfo_get_marshal_info_raw (int,int);
int ves_icall_System_Reflection_LoaderAllocatorScout_Destroy (int);
void ves_icall_System_Reflection_RuntimeAssembly_GetManifestResourceNames_raw (int,int,int);
void ves_icall_System_Reflection_RuntimeAssembly_GetExportedTypes_raw (int,int,int);
void ves_icall_System_Reflection_RuntimeAssembly_GetInfo_raw (int,int,int,int);
int ves_icall_System_Reflection_RuntimeAssembly_GetManifestResourceInternal_raw (int,int,int,int,int);
void ves_icall_System_Reflection_Assembly_GetManifestModuleInternal_raw (int,int,int);
void ves_icall_System_Reflection_RuntimeAssembly_GetModulesInternal_raw (int,int,int);
void ves_icall_System_Reflection_RuntimeCustomAttributeData_ResolveArgumentsInternal_raw (int,int,int,int,int,int,int);
void ves_icall_RuntimeEventInfo_get_event_info_raw (int,int,int);
int ves_icall_reflection_get_token_raw (int,int);
int ves_icall_System_Reflection_EventInfo_internal_from_handle_type_raw (int,int,int);
int ves_icall_RuntimeFieldInfo_ResolveType_raw (int,int);
int ves_icall_RuntimeFieldInfo_GetParentType_raw (int,int,int);
int ves_icall_RuntimeFieldInfo_GetFieldOffset_raw (int,int);
int ves_icall_RuntimeFieldInfo_GetValueInternal_raw (int,int,int);
void ves_icall_RuntimeFieldInfo_SetValueInternal_raw (int,int,int,int);
int ves_icall_RuntimeFieldInfo_GetRawConstantValue_raw (int,int);
int ves_icall_reflection_get_token_raw (int,int);
void ves_icall_get_method_info_raw (int,int,int);
int ves_icall_get_method_attributes (int);
int ves_icall_System_Reflection_MonoMethodInfo_get_parameter_info_raw (int,int,int);
int ves_icall_System_MonoMethodInfo_get_retval_marshal_raw (int,int);
int ves_icall_System_Reflection_RuntimeMethodInfo_GetMethodFromHandleInternalType_native_raw (int,int,int,int);
int ves_icall_RuntimeMethodInfo_get_name_raw (int,int);
int ves_icall_RuntimeMethodInfo_get_base_method_raw (int,int,int);
int ves_icall_reflection_get_token_raw (int,int);
int ves_icall_InternalInvoke_raw (int,int,int,int,int);
void ves_icall_RuntimeMethodInfo_GetPInvoke_raw (int,int,int,int,int);
int ves_icall_RuntimeMethodInfo_MakeGenericMethod_impl_raw (int,int,int);
int ves_icall_RuntimeMethodInfo_GetGenericArguments_raw (int,int);
int ves_icall_RuntimeMethodInfo_GetGenericMethodDefinition_raw (int,int);
int ves_icall_RuntimeMethodInfo_get_IsGenericMethodDefinition_raw (int,int);
int ves_icall_RuntimeMethodInfo_get_IsGenericMethod_raw (int,int);
void ves_icall_InvokeClassConstructor_raw (int,int);
int ves_icall_InternalInvoke_raw (int,int,int,int,int);
int ves_icall_reflection_get_token_raw (int,int);
void ves_icall_System_Reflection_RuntimeModule_GetGuidInternal_raw (int,int,int);
int ves_icall_System_Reflection_RuntimeModule_ResolveMethodToken_raw (int,int,int,int,int,int);
int ves_icall_RuntimeParameterInfo_GetTypeModifiers_raw (int,int,int,int,int,int);
void ves_icall_RuntimePropertyInfo_get_property_info_raw (int,int,int,int);
int ves_icall_reflection_get_token_raw (int,int);
int ves_icall_System_Reflection_RuntimePropertyInfo_internal_from_handle_type_raw (int,int,int);
int ves_icall_CustomAttributeBuilder_GetBlob_raw (int,int,int,int,int,int,int,int);
void ves_icall_DynamicMethod_create_dynamic_method_raw (int,int,int,int,int);
void ves_icall_AssemblyBuilder_basic_init_raw (int,int);
void ves_icall_AssemblyBuilder_UpdateNativeCustomAttributes_raw (int,int);
void ves_icall_ModuleBuilder_basic_init_raw (int,int);
void ves_icall_ModuleBuilder_set_wrappers_type_raw (int,int,int);
int ves_icall_ModuleBuilder_getUSIndex_raw (int,int,int);
int ves_icall_ModuleBuilder_getToken_raw (int,int,int,int);
int ves_icall_ModuleBuilder_getMethodToken_raw (int,int,int,int);
void ves_icall_ModuleBuilder_RegisterToken_raw (int,int,int,int);
int ves_icall_TypeBuilder_create_runtime_class_raw (int,int);
int ves_icall_System_IO_Stream_HasOverriddenBeginEndRead_raw (int,int);
int ves_icall_System_IO_Stream_HasOverriddenBeginEndWrite_raw (int,int);
int ves_icall_System_Diagnostics_Debugger_IsAttached_internal ();
int ves_icall_System_Diagnostics_Debugger_IsLogging ();
void ves_icall_System_Diagnostics_Debugger_Log (int,int,int);
int ves_icall_System_Diagnostics_StackFrame_GetFrameInfo (int,int,int,int,int,int,int,int);
void ves_icall_System_Diagnostics_StackTrace_GetTrace (int,int,int,int);
int ves_icall_Mono_RuntimeClassHandle_GetTypeFromClass (int);
void ves_icall_Mono_RuntimeGPtrArrayHandle_GPtrArrayFree (int);
int ves_icall_Mono_SafeStringMarshal_StringToUtf8 (int);
void ves_icall_Mono_SafeStringMarshal_GFree (int);
static void *corlib_icall_funcs [] = {
// token 220,
ves_icall_System_Array_InternalCreate,
// token 233,
ves_icall_System_Array_GetCorElementTypeOfElementTypeInternal,
// token 234,
ves_icall_System_Array_IsValueOfElementTypeInternal,
// token 235,
ves_icall_System_Array_CanChangePrimitive,
// token 236,
ves_icall_System_Array_FastCopy,
// token 237,
ves_icall_System_Array_GetLengthInternal_raw,
// token 238,
ves_icall_System_Array_GetLowerBoundInternal_raw,
// token 239,
ves_icall_System_Array_GetGenericValue_icall,
// token 240,
ves_icall_System_Array_GetValueImpl_raw,
// token 241,
ves_icall_System_Array_SetGenericValue_icall,
// token 244,
ves_icall_System_Array_SetValueImpl_raw,
// token 245,
ves_icall_System_Array_InitializeInternal_raw,
// token 246,
ves_icall_System_Array_SetValueRelaxedImpl_raw,
// token 423,
ves_icall_System_Runtime_RuntimeImports_ZeroMemory,
// token 424,
ves_icall_System_Runtime_RuntimeImports_Memmove,
// token 425,
ves_icall_System_Buffer_BulkMoveWithWriteBarrier,
// token 453,
ves_icall_System_Delegate_AllocDelegateLike_internal_raw,
// token 454,
ves_icall_System_Delegate_CreateDelegate_internal_raw,
// token 455,
ves_icall_System_Delegate_GetVirtualMethod_internal_raw,
// token 482,
ves_icall_System_Enum_GetEnumValuesAndNames_raw,
// token 483,
ves_icall_System_Enum_InternalGetCorElementType,
// token 484,
ves_icall_System_Enum_InternalGetUnderlyingType_raw,
// token 601,
ves_icall_System_Environment_get_ProcessorCount,
// token 602,
ves_icall_System_Environment_get_TickCount,
// token 603,
ves_icall_System_Environment_get_TickCount64,
// token 606,
ves_icall_System_Environment_FailFast_raw,
// token 644,
ves_icall_System_GC_GetCollectionCount,
// token 645,
ves_icall_System_GC_GetMaxGeneration,
// token 646,
ves_icall_System_GC_register_ephemeron_array_raw,
// token 647,
ves_icall_System_GC_get_ephemeron_tombstone_raw,
// token 648,
ves_icall_System_GC_GetTotalAllocatedBytes_raw,
// token 652,
ves_icall_System_GC_SuppressFinalize_raw,
// token 654,
ves_icall_System_GC_ReRegisterForFinalize_raw,
// token 656,
ves_icall_System_GC_GetGCMemoryInfo,
// token 658,
ves_icall_System_GC_AllocPinnedArray_raw,
// token 664,
ves_icall_System_Object_MemberwiseClone_raw,
// token 672,
ves_icall_System_Math_Acos,
// token 673,
ves_icall_System_Math_Acosh,
// token 674,
ves_icall_System_Math_Asin,
// token 675,
ves_icall_System_Math_Asinh,
// token 676,
ves_icall_System_Math_Atan,
// token 677,
ves_icall_System_Math_Atan2,
// token 678,
ves_icall_System_Math_Atanh,
// token 679,
ves_icall_System_Math_Cbrt,
// token 680,
ves_icall_System_Math_Ceiling,
// token 681,
ves_icall_System_Math_Cos,
// token 682,
ves_icall_System_Math_Cosh,
// token 683,
ves_icall_System_Math_Exp,
// token 684,
ves_icall_System_Math_Floor,
// token 685,
ves_icall_System_Math_Log,
// token 686,
ves_icall_System_Math_Log10,
// token 687,
ves_icall_System_Math_Pow,
// token 688,
ves_icall_System_Math_Sin,
// token 690,
ves_icall_System_Math_Sinh,
// token 691,
ves_icall_System_Math_Sqrt,
// token 692,
ves_icall_System_Math_Tan,
// token 693,
ves_icall_System_Math_Tanh,
// token 694,
ves_icall_System_Math_FusedMultiplyAdd,
// token 695,
ves_icall_System_Math_Log2,
// token 696,
ves_icall_System_Math_ModF,
// token 793,
ves_icall_System_MathF_Acos,
// token 794,
ves_icall_System_MathF_Acosh,
// token 795,
ves_icall_System_MathF_Asin,
// token 796,
ves_icall_System_MathF_Asinh,
// token 797,
ves_icall_System_MathF_Atan,
// token 798,
ves_icall_System_MathF_Atan2,
// token 799,
ves_icall_System_MathF_Atanh,
// token 800,
ves_icall_System_MathF_Cbrt,
// token 801,
ves_icall_System_MathF_Ceiling,
// token 802,
ves_icall_System_MathF_Cos,
// token 803,
ves_icall_System_MathF_Cosh,
// token 804,
ves_icall_System_MathF_Exp,
// token 805,
ves_icall_System_MathF_Floor,
// token 806,
ves_icall_System_MathF_Log,
// token 807,
ves_icall_System_MathF_Log10,
// token 808,
ves_icall_System_MathF_Pow,
// token 809,
ves_icall_System_MathF_Sin,
// token 811,
ves_icall_System_MathF_Sinh,
// token 812,
ves_icall_System_MathF_Sqrt,
// token 813,
ves_icall_System_MathF_Tan,
// token 814,
ves_icall_System_MathF_Tanh,
// token 815,
ves_icall_System_MathF_FusedMultiplyAdd,
// token 816,
ves_icall_System_MathF_Log2,
// token 817,
ves_icall_System_MathF_ModF,
// token 879,
ves_icall_RuntimeMethodHandle_GetFunctionPointer_raw,
// token 888,
ves_icall_RuntimeMethodHandle_ReboxFromNullable_raw,
// token 889,
ves_icall_RuntimeMethodHandle_ReboxToNullable_raw,
// token 960,
ves_icall_RuntimeType_GetCorrespondingInflatedMethod_raw,
// token 967,
ves_icall_RuntimeType_make_array_type_raw,
// token 970,
ves_icall_RuntimeType_make_byref_type_raw,
// token 972,
ves_icall_RuntimeType_make_pointer_type_raw,
// token 977,
ves_icall_RuntimeType_MakeGenericType_raw,
// token 978,
ves_icall_RuntimeType_GetMethodsByName_native_raw,
// token 980,
ves_icall_RuntimeType_GetPropertiesByName_native_raw,
// token 981,
ves_icall_RuntimeType_GetConstructors_native_raw,
// token 985,
ves_icall_System_RuntimeType_CreateInstanceInternal_raw,
// token 986,
ves_icall_RuntimeType_GetDeclaringMethod_raw,
// token 988,
ves_icall_System_RuntimeType_getFullName_raw,
// token 989,
ves_icall_RuntimeType_GetGenericArgumentsInternal_raw,
// token 992,
ves_icall_RuntimeType_GetGenericParameterPosition,
// token 993,
ves_icall_RuntimeType_GetEvents_native_raw,
// token 994,
ves_icall_RuntimeType_GetFields_native_raw,
// token 997,
ves_icall_RuntimeType_GetInterfaces_raw,
// token 999,
ves_icall_RuntimeType_GetNestedTypes_native_raw,
// token 1002,
ves_icall_RuntimeType_GetDeclaringType_raw,
// token 1004,
ves_icall_RuntimeType_GetName_raw,
// token 1006,
ves_icall_RuntimeType_GetNamespace_raw,
// token 1015,
ves_icall_RuntimeType_FunctionPointerReturnAndParameterTypes_raw,
// token 1084,
ves_icall_RuntimeTypeHandle_GetAttributes,
// token 1086,
ves_icall_RuntimeTypeHandle_GetMetadataToken_raw,
// token 1088,
ves_icall_RuntimeTypeHandle_GetGenericTypeDefinition_impl_raw,
// token 1098,
ves_icall_RuntimeTypeHandle_GetCorElementType,
// token 1099,
ves_icall_RuntimeTypeHandle_HasInstantiation,
// token 1100,
ves_icall_RuntimeTypeHandle_IsInstanceOfType_raw,
// token 1102,
ves_icall_RuntimeTypeHandle_HasReferences_raw,
// token 1108,
ves_icall_RuntimeTypeHandle_GetArrayRank_raw,
// token 1109,
ves_icall_RuntimeTypeHandle_GetAssembly_raw,
// token 1110,
ves_icall_RuntimeTypeHandle_GetElementType_raw,
// token 1111,
ves_icall_RuntimeTypeHandle_GetModule_raw,
// token 1112,
ves_icall_RuntimeTypeHandle_GetBaseType_raw,
// token 1120,
ves_icall_RuntimeTypeHandle_type_is_assignable_from_raw,
// token 1121,
ves_icall_RuntimeTypeHandle_IsGenericTypeDefinition,
// token 1122,
ves_icall_RuntimeTypeHandle_GetGenericParameterInfo_raw,
// token 1126,
ves_icall_RuntimeTypeHandle_is_subclass_of_raw,
// token 1127,
ves_icall_RuntimeTypeHandle_IsByRefLike_raw,
// token 1129,
ves_icall_System_RuntimeTypeHandle_internal_from_name_raw,
// token 1133,
ves_icall_System_String_FastAllocateString_raw,
// token 1134,
ves_icall_System_String_InternalIsInterned_raw,
// token 1135,
ves_icall_System_String_InternalIntern_raw,
// token 1432,
ves_icall_System_Type_internal_from_handle_raw,
// token 1622,
ves_icall_System_ValueType_InternalGetHashCode_raw,
// token 1623,
ves_icall_System_ValueType_Equals_raw,
// token 10114,
ves_icall_System_Threading_Interlocked_CompareExchange_Int,
// token 10115,
ves_icall_System_Threading_Interlocked_CompareExchange_Object,
// token 10117,
ves_icall_System_Threading_Interlocked_Decrement_Int,
// token 10118,
ves_icall_System_Threading_Interlocked_Decrement_Long,
// token 10119,
ves_icall_System_Threading_Interlocked_Increment_Int,
// token 10120,
ves_icall_System_Threading_Interlocked_Increment_Long,
// token 10121,
ves_icall_System_Threading_Interlocked_Exchange_Int,
// token 10122,
ves_icall_System_Threading_Interlocked_Exchange_Object,
// token 10124,
ves_icall_System_Threading_Interlocked_CompareExchange_Long,
// token 10125,
ves_icall_System_Threading_Interlocked_Exchange_Long,
// token 10126,
ves_icall_System_Threading_Interlocked_Read_Long,
// token 10127,
ves_icall_System_Threading_Interlocked_Add_Int,
// token 10128,
ves_icall_System_Threading_Interlocked_Add_Long,
// token 10146,
ves_icall_System_Threading_Monitor_Monitor_Enter_raw,
// token 10148,
mono_monitor_exit_icall_raw,
// token 10156,
ves_icall_System_Threading_Monitor_Monitor_pulse_raw,
// token 10158,
ves_icall_System_Threading_Monitor_Monitor_pulse_all_raw,
// token 10160,
ves_icall_System_Threading_Monitor_Monitor_wait_raw,
// token 10162,
ves_icall_System_Threading_Monitor_Monitor_try_enter_with_atomic_var_raw,
// token 10165,
ves_icall_System_Threading_Monitor_Monitor_get_lock_contention_count,
// token 10216,
ves_icall_System_Threading_Thread_InitInternal_raw,
// token 10217,
ves_icall_System_Threading_Thread_GetCurrentThread,
// token 10219,
ves_icall_System_Threading_InternalThread_Thread_free_internal_raw,
// token 10220,
ves_icall_System_Threading_Thread_GetState_raw,
// token 10221,
ves_icall_System_Threading_Thread_SetState_raw,
// token 10222,
ves_icall_System_Threading_Thread_ClrState_raw,
// token 10223,
ves_icall_System_Threading_Thread_SetName_icall_raw,
// token 10225,
ves_icall_System_Threading_Thread_YieldInternal,
// token 10227,
ves_icall_System_Threading_Thread_SetPriority_raw,
// token 11430,
ves_icall_System_Runtime_Loader_AssemblyLoadContext_PrepareForAssemblyLoadContextRelease_raw,
// token 11434,
ves_icall_System_Runtime_Loader_AssemblyLoadContext_GetLoadContextForAssembly_raw,
// token 11436,
ves_icall_System_Runtime_Loader_AssemblyLoadContext_InternalLoadFile_raw,
// token 11437,
ves_icall_System_Runtime_Loader_AssemblyLoadContext_InternalInitializeNativeALC_raw,
// token 11438,
ves_icall_System_Runtime_Loader_AssemblyLoadContext_InternalLoadFromStream_raw,
// token 11439,
ves_icall_System_Runtime_Loader_AssemblyLoadContext_InternalGetLoadedAssemblies_raw,
// token 11881,
ves_icall_System_GCHandle_InternalAlloc_raw,
// token 11882,
ves_icall_System_GCHandle_InternalFree_raw,
// token 11883,
ves_icall_System_GCHandle_InternalGet_raw,
// token 11884,
ves_icall_System_GCHandle_InternalSet_raw,
// token 11902,
ves_icall_System_Runtime_InteropServices_Marshal_GetLastPInvokeError,
// token 11903,
ves_icall_System_Runtime_InteropServices_Marshal_SetLastPInvokeError,
// token 11904,
ves_icall_System_Runtime_InteropServices_Marshal_StructureToPtr_raw,
// token 11950,
ves_icall_System_Runtime_InteropServices_NativeLibrary_LoadByName_raw,
// token 12018,
ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_InternalGetHashCode_raw,
// token 12021,
ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_GetObjectValue_raw,
// token 12030,
ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_GetUninitializedObjectInternal_raw,
// token 12031,
ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_InitializeArray_raw,
// token 12032,
ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_GetSpanDataFrom_raw,
// token 12033,
ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_RunClassConstructor_raw,
// token 12034,
ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_SufficientExecutionStack,
// token 12035,
ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_InternalBox_raw,
// token 12382,
ves_icall_System_Reflection_Assembly_GetExecutingAssembly_raw,
// token 12383,
ves_icall_System_Reflection_Assembly_GetEntryAssembly_raw,
// token 12388,
ves_icall_System_Reflection_Assembly_InternalLoad_raw,
// token 12389,
ves_icall_System_Reflection_Assembly_InternalGetType_raw,
// token 12424,
ves_icall_System_Reflection_AssemblyName_GetNativeName,
// token 12467,
ves_icall_MonoCustomAttrs_GetCustomAttributesInternal_raw,
// token 12474,
ves_icall_MonoCustomAttrs_GetCustomAttributesDataInternal_raw,
// token 12481,
ves_icall_MonoCustomAttrs_IsDefinedInternal_raw,
// token 12492,
ves_icall_System_Reflection_FieldInfo_internal_from_handle_type_raw,
// token 12496,
ves_icall_System_Reflection_FieldInfo_get_marshal_info_raw,
// token 12522,
ves_icall_System_Reflection_LoaderAllocatorScout_Destroy,
// token 12605,
ves_icall_System_Reflection_RuntimeAssembly_GetManifestResourceNames_raw,
// token 12607,
ves_icall_System_Reflection_RuntimeAssembly_GetExportedTypes_raw,
// token 12618,
ves_icall_System_Reflection_RuntimeAssembly_GetInfo_raw,
// token 12620,
ves_icall_System_Reflection_RuntimeAssembly_GetManifestResourceInternal_raw,
// token 12621,
ves_icall_System_Reflection_Assembly_GetManifestModuleInternal_raw,
// token 12622,
ves_icall_System_Reflection_RuntimeAssembly_GetModulesInternal_raw,
// token 12629,
ves_icall_System_Reflection_RuntimeCustomAttributeData_ResolveArgumentsInternal_raw,
// token 12644,
ves_icall_RuntimeEventInfo_get_event_info_raw,
// token 12664,
ves_icall_reflection_get_token_raw,
// token 12665,
ves_icall_System_Reflection_EventInfo_internal_from_handle_type_raw,
// token 12673,
ves_icall_RuntimeFieldInfo_ResolveType_raw,
// token 12675,
ves_icall_RuntimeFieldInfo_GetParentType_raw,
// token 12682,
ves_icall_RuntimeFieldInfo_GetFieldOffset_raw,
// token 12683,
ves_icall_RuntimeFieldInfo_GetValueInternal_raw,
// token 12686,
ves_icall_RuntimeFieldInfo_SetValueInternal_raw,
// token 12688,
ves_icall_RuntimeFieldInfo_GetRawConstantValue_raw,
// token 12693,
ves_icall_reflection_get_token_raw,
// token 12699,
ves_icall_get_method_info_raw,
// token 12700,
ves_icall_get_method_attributes,
// token 12707,
ves_icall_System_Reflection_MonoMethodInfo_get_parameter_info_raw,
// token 12709,
ves_icall_System_MonoMethodInfo_get_retval_marshal_raw,
// token 12721,
ves_icall_System_Reflection_RuntimeMethodInfo_GetMethodFromHandleInternalType_native_raw,
// token 12724,
ves_icall_RuntimeMethodInfo_get_name_raw,
// token 12725,
ves_icall_RuntimeMethodInfo_get_base_method_raw,
// token 12726,
ves_icall_reflection_get_token_raw,
// token 12737,
ves_icall_InternalInvoke_raw,
// token 12747,
ves_icall_RuntimeMethodInfo_GetPInvoke_raw,
// token 12753,
ves_icall_RuntimeMethodInfo_MakeGenericMethod_impl_raw,
// token 12754,
ves_icall_RuntimeMethodInfo_GetGenericArguments_raw,
// token 12755,
ves_icall_RuntimeMethodInfo_GetGenericMethodDefinition_raw,
// token 12757,
ves_icall_RuntimeMethodInfo_get_IsGenericMethodDefinition_raw,
// token 12758,
ves_icall_RuntimeMethodInfo_get_IsGenericMethod_raw,
// token 12775,
ves_icall_InvokeClassConstructor_raw,
// token 12777,
ves_icall_InternalInvoke_raw,
// token 12792,
ves_icall_reflection_get_token_raw,
// token 12814,
ves_icall_System_Reflection_RuntimeModule_GetGuidInternal_raw,
// token 12815,
ves_icall_System_Reflection_RuntimeModule_ResolveMethodToken_raw,
// token 12840,
ves_icall_RuntimeParameterInfo_GetTypeModifiers_raw,
// token 12845,
ves_icall_RuntimePropertyInfo_get_property_info_raw,
// token 12875,
ves_icall_reflection_get_token_raw,
// token 12876,
ves_icall_System_Reflection_RuntimePropertyInfo_internal_from_handle_type_raw,
// token 13505,
ves_icall_CustomAttributeBuilder_GetBlob_raw,
// token 13519,
ves_icall_DynamicMethod_create_dynamic_method_raw,
// token 13608,
ves_icall_AssemblyBuilder_basic_init_raw,
// token 13609,
ves_icall_AssemblyBuilder_UpdateNativeCustomAttributes_raw,
// token 13831,
ves_icall_ModuleBuilder_basic_init_raw,
// token 13832,
ves_icall_ModuleBuilder_set_wrappers_type_raw,
// token 13839,
ves_icall_ModuleBuilder_getUSIndex_raw,
// token 13840,
ves_icall_ModuleBuilder_getToken_raw,
// token 13841,
ves_icall_ModuleBuilder_getMethodToken_raw,
// token 13847,
ves_icall_ModuleBuilder_RegisterToken_raw,
// token 13917,
ves_icall_TypeBuilder_create_runtime_class_raw,
// token 14455,
ves_icall_System_IO_Stream_HasOverriddenBeginEndRead_raw,
// token 14456,
ves_icall_System_IO_Stream_HasOverriddenBeginEndWrite_raw,
// token 14886,
ves_icall_System_Diagnostics_Debugger_IsAttached_internal,
// token 14887,
ves_icall_System_Diagnostics_Debugger_IsLogging,
// token 14888,
ves_icall_System_Diagnostics_Debugger_Log,
// token 14894,
ves_icall_System_Diagnostics_StackFrame_GetFrameInfo,
// token 14904,
ves_icall_System_Diagnostics_StackTrace_GetTrace,
// token 15918,
ves_icall_Mono_RuntimeClassHandle_GetTypeFromClass,
// token 15939,
ves_icall_Mono_RuntimeGPtrArrayHandle_GPtrArrayFree,
// token 15941,
ves_icall_Mono_SafeStringMarshal_StringToUtf8,
// token 15943,
ves_icall_Mono_SafeStringMarshal_GFree,
};
static uint8_t corlib_icall_flags [] = {
0,
0,
0,
0,
0,
4,
4,
0,
4,
0,
4,
4,
4,
0,
0,
0,
4,
4,
4,
4,
0,
4,
0,
0,
0,
4,
0,
0,
4,
4,
4,
4,
4,
0,
4,
4,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
0,
4,
4,
4,
4,
4,
4,
4,
4,
0,
4,
4,
0,
0,
4,
4,
4,
4,
4,
4,
4,
4,
0,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
4,
4,
4,
4,
4,
4,
0,
4,
0,
4,
4,
4,
4,
4,
0,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
0,
0,
4,
4,
4,
4,
4,
4,
4,
4,
0,
4,
4,
4,
4,
4,
0,
4,
4,
4,
4,
4,
0,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
0,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
0,
0,
0,
0,
0,
0,
0,
0,
0,
};
