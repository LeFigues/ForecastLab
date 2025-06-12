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
645,
646,
647,
648,
649,
653,
655,
657,
659,
665,
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
689,
691,
692,
693,
694,
695,
696,
697,
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
810,
812,
813,
814,
815,
816,
817,
818,
880,
889,
890,
961,
968,
971,
973,
978,
979,
981,
982,
986,
988,
989,
991,
992,
995,
996,
997,
1000,
1002,
1005,
1007,
1009,
1018,
1087,
1089,
1091,
1101,
1102,
1103,
1105,
1111,
1112,
1113,
1114,
1115,
1123,
1124,
1125,
1129,
1130,
1132,
1136,
1137,
1138,
1435,
1626,
1627,
10322,
10323,
10325,
10326,
10327,
10328,
10329,
10330,
10332,
10333,
10334,
10335,
10336,
10354,
10356,
10364,
10366,
10368,
10370,
10373,
10424,
10425,
10427,
10428,
10429,
10430,
10431,
10433,
10435,
11643,
11647,
11649,
11650,
11651,
11652,
12094,
12095,
12096,
12097,
12115,
12116,
12117,
12164,
12232,
12235,
12244,
12245,
12246,
12247,
12248,
12249,
12597,
12598,
12599,
12604,
12605,
12640,
12683,
12690,
12697,
12708,
12712,
12738,
12821,
12823,
12834,
12836,
12837,
12838,
12845,
12860,
12880,
12881,
12889,
12891,
12898,
12899,
12902,
12904,
12909,
12915,
12916,
12923,
12925,
12937,
12940,
12941,
12942,
12953,
12963,
12969,
12970,
12971,
12973,
12974,
12991,
12993,
13008,
13030,
13031,
13056,
13061,
13092,
13093,
13725,
13739,
13828,
13829,
14051,
14052,
14059,
14060,
14061,
14067,
14138,
14685,
14686,
15132,
15133,
15134,
15140,
15150,
16167,
16188,
16190,
16192,
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
void ves_icall_RuntimeType_GetInterfaceMapData_raw (int,int,int,int,int);
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
int ves_icall_System_Reflection_Assembly_GetCallingAssembly_raw (int);
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
// token 645,
ves_icall_System_GC_GetCollectionCount,
// token 646,
ves_icall_System_GC_GetMaxGeneration,
// token 647,
ves_icall_System_GC_register_ephemeron_array_raw,
// token 648,
ves_icall_System_GC_get_ephemeron_tombstone_raw,
// token 649,
ves_icall_System_GC_GetTotalAllocatedBytes_raw,
// token 653,
ves_icall_System_GC_SuppressFinalize_raw,
// token 655,
ves_icall_System_GC_ReRegisterForFinalize_raw,
// token 657,
ves_icall_System_GC_GetGCMemoryInfo,
// token 659,
ves_icall_System_GC_AllocPinnedArray_raw,
// token 665,
ves_icall_System_Object_MemberwiseClone_raw,
// token 673,
ves_icall_System_Math_Acos,
// token 674,
ves_icall_System_Math_Acosh,
// token 675,
ves_icall_System_Math_Asin,
// token 676,
ves_icall_System_Math_Asinh,
// token 677,
ves_icall_System_Math_Atan,
// token 678,
ves_icall_System_Math_Atan2,
// token 679,
ves_icall_System_Math_Atanh,
// token 680,
ves_icall_System_Math_Cbrt,
// token 681,
ves_icall_System_Math_Ceiling,
// token 682,
ves_icall_System_Math_Cos,
// token 683,
ves_icall_System_Math_Cosh,
// token 684,
ves_icall_System_Math_Exp,
// token 685,
ves_icall_System_Math_Floor,
// token 686,
ves_icall_System_Math_Log,
// token 687,
ves_icall_System_Math_Log10,
// token 688,
ves_icall_System_Math_Pow,
// token 689,
ves_icall_System_Math_Sin,
// token 691,
ves_icall_System_Math_Sinh,
// token 692,
ves_icall_System_Math_Sqrt,
// token 693,
ves_icall_System_Math_Tan,
// token 694,
ves_icall_System_Math_Tanh,
// token 695,
ves_icall_System_Math_FusedMultiplyAdd,
// token 696,
ves_icall_System_Math_Log2,
// token 697,
ves_icall_System_Math_ModF,
// token 794,
ves_icall_System_MathF_Acos,
// token 795,
ves_icall_System_MathF_Acosh,
// token 796,
ves_icall_System_MathF_Asin,
// token 797,
ves_icall_System_MathF_Asinh,
// token 798,
ves_icall_System_MathF_Atan,
// token 799,
ves_icall_System_MathF_Atan2,
// token 800,
ves_icall_System_MathF_Atanh,
// token 801,
ves_icall_System_MathF_Cbrt,
// token 802,
ves_icall_System_MathF_Ceiling,
// token 803,
ves_icall_System_MathF_Cos,
// token 804,
ves_icall_System_MathF_Cosh,
// token 805,
ves_icall_System_MathF_Exp,
// token 806,
ves_icall_System_MathF_Floor,
// token 807,
ves_icall_System_MathF_Log,
// token 808,
ves_icall_System_MathF_Log10,
// token 809,
ves_icall_System_MathF_Pow,
// token 810,
ves_icall_System_MathF_Sin,
// token 812,
ves_icall_System_MathF_Sinh,
// token 813,
ves_icall_System_MathF_Sqrt,
// token 814,
ves_icall_System_MathF_Tan,
// token 815,
ves_icall_System_MathF_Tanh,
// token 816,
ves_icall_System_MathF_FusedMultiplyAdd,
// token 817,
ves_icall_System_MathF_Log2,
// token 818,
ves_icall_System_MathF_ModF,
// token 880,
ves_icall_RuntimeMethodHandle_GetFunctionPointer_raw,
// token 889,
ves_icall_RuntimeMethodHandle_ReboxFromNullable_raw,
// token 890,
ves_icall_RuntimeMethodHandle_ReboxToNullable_raw,
// token 961,
ves_icall_RuntimeType_GetCorrespondingInflatedMethod_raw,
// token 968,
ves_icall_RuntimeType_make_array_type_raw,
// token 971,
ves_icall_RuntimeType_make_byref_type_raw,
// token 973,
ves_icall_RuntimeType_make_pointer_type_raw,
// token 978,
ves_icall_RuntimeType_MakeGenericType_raw,
// token 979,
ves_icall_RuntimeType_GetMethodsByName_native_raw,
// token 981,
ves_icall_RuntimeType_GetPropertiesByName_native_raw,
// token 982,
ves_icall_RuntimeType_GetConstructors_native_raw,
// token 986,
ves_icall_RuntimeType_GetInterfaceMapData_raw,
// token 988,
ves_icall_System_RuntimeType_CreateInstanceInternal_raw,
// token 989,
ves_icall_RuntimeType_GetDeclaringMethod_raw,
// token 991,
ves_icall_System_RuntimeType_getFullName_raw,
// token 992,
ves_icall_RuntimeType_GetGenericArgumentsInternal_raw,
// token 995,
ves_icall_RuntimeType_GetGenericParameterPosition,
// token 996,
ves_icall_RuntimeType_GetEvents_native_raw,
// token 997,
ves_icall_RuntimeType_GetFields_native_raw,
// token 1000,
ves_icall_RuntimeType_GetInterfaces_raw,
// token 1002,
ves_icall_RuntimeType_GetNestedTypes_native_raw,
// token 1005,
ves_icall_RuntimeType_GetDeclaringType_raw,
// token 1007,
ves_icall_RuntimeType_GetName_raw,
// token 1009,
ves_icall_RuntimeType_GetNamespace_raw,
// token 1018,
ves_icall_RuntimeType_FunctionPointerReturnAndParameterTypes_raw,
// token 1087,
ves_icall_RuntimeTypeHandle_GetAttributes,
// token 1089,
ves_icall_RuntimeTypeHandle_GetMetadataToken_raw,
// token 1091,
ves_icall_RuntimeTypeHandle_GetGenericTypeDefinition_impl_raw,
// token 1101,
ves_icall_RuntimeTypeHandle_GetCorElementType,
// token 1102,
ves_icall_RuntimeTypeHandle_HasInstantiation,
// token 1103,
ves_icall_RuntimeTypeHandle_IsInstanceOfType_raw,
// token 1105,
ves_icall_RuntimeTypeHandle_HasReferences_raw,
// token 1111,
ves_icall_RuntimeTypeHandle_GetArrayRank_raw,
// token 1112,
ves_icall_RuntimeTypeHandle_GetAssembly_raw,
// token 1113,
ves_icall_RuntimeTypeHandle_GetElementType_raw,
// token 1114,
ves_icall_RuntimeTypeHandle_GetModule_raw,
// token 1115,
ves_icall_RuntimeTypeHandle_GetBaseType_raw,
// token 1123,
ves_icall_RuntimeTypeHandle_type_is_assignable_from_raw,
// token 1124,
ves_icall_RuntimeTypeHandle_IsGenericTypeDefinition,
// token 1125,
ves_icall_RuntimeTypeHandle_GetGenericParameterInfo_raw,
// token 1129,
ves_icall_RuntimeTypeHandle_is_subclass_of_raw,
// token 1130,
ves_icall_RuntimeTypeHandle_IsByRefLike_raw,
// token 1132,
ves_icall_System_RuntimeTypeHandle_internal_from_name_raw,
// token 1136,
ves_icall_System_String_FastAllocateString_raw,
// token 1137,
ves_icall_System_String_InternalIsInterned_raw,
// token 1138,
ves_icall_System_String_InternalIntern_raw,
// token 1435,
ves_icall_System_Type_internal_from_handle_raw,
// token 1626,
ves_icall_System_ValueType_InternalGetHashCode_raw,
// token 1627,
ves_icall_System_ValueType_Equals_raw,
// token 10322,
ves_icall_System_Threading_Interlocked_CompareExchange_Int,
// token 10323,
ves_icall_System_Threading_Interlocked_CompareExchange_Object,
// token 10325,
ves_icall_System_Threading_Interlocked_Decrement_Int,
// token 10326,
ves_icall_System_Threading_Interlocked_Decrement_Long,
// token 10327,
ves_icall_System_Threading_Interlocked_Increment_Int,
// token 10328,
ves_icall_System_Threading_Interlocked_Increment_Long,
// token 10329,
ves_icall_System_Threading_Interlocked_Exchange_Int,
// token 10330,
ves_icall_System_Threading_Interlocked_Exchange_Object,
// token 10332,
ves_icall_System_Threading_Interlocked_CompareExchange_Long,
// token 10333,
ves_icall_System_Threading_Interlocked_Exchange_Long,
// token 10334,
ves_icall_System_Threading_Interlocked_Read_Long,
// token 10335,
ves_icall_System_Threading_Interlocked_Add_Int,
// token 10336,
ves_icall_System_Threading_Interlocked_Add_Long,
// token 10354,
ves_icall_System_Threading_Monitor_Monitor_Enter_raw,
// token 10356,
mono_monitor_exit_icall_raw,
// token 10364,
ves_icall_System_Threading_Monitor_Monitor_pulse_raw,
// token 10366,
ves_icall_System_Threading_Monitor_Monitor_pulse_all_raw,
// token 10368,
ves_icall_System_Threading_Monitor_Monitor_wait_raw,
// token 10370,
ves_icall_System_Threading_Monitor_Monitor_try_enter_with_atomic_var_raw,
// token 10373,
ves_icall_System_Threading_Monitor_Monitor_get_lock_contention_count,
// token 10424,
ves_icall_System_Threading_Thread_InitInternal_raw,
// token 10425,
ves_icall_System_Threading_Thread_GetCurrentThread,
// token 10427,
ves_icall_System_Threading_InternalThread_Thread_free_internal_raw,
// token 10428,
ves_icall_System_Threading_Thread_GetState_raw,
// token 10429,
ves_icall_System_Threading_Thread_SetState_raw,
// token 10430,
ves_icall_System_Threading_Thread_ClrState_raw,
// token 10431,
ves_icall_System_Threading_Thread_SetName_icall_raw,
// token 10433,
ves_icall_System_Threading_Thread_YieldInternal,
// token 10435,
ves_icall_System_Threading_Thread_SetPriority_raw,
// token 11643,
ves_icall_System_Runtime_Loader_AssemblyLoadContext_PrepareForAssemblyLoadContextRelease_raw,
// token 11647,
ves_icall_System_Runtime_Loader_AssemblyLoadContext_GetLoadContextForAssembly_raw,
// token 11649,
ves_icall_System_Runtime_Loader_AssemblyLoadContext_InternalLoadFile_raw,
// token 11650,
ves_icall_System_Runtime_Loader_AssemblyLoadContext_InternalInitializeNativeALC_raw,
// token 11651,
ves_icall_System_Runtime_Loader_AssemblyLoadContext_InternalLoadFromStream_raw,
// token 11652,
ves_icall_System_Runtime_Loader_AssemblyLoadContext_InternalGetLoadedAssemblies_raw,
// token 12094,
ves_icall_System_GCHandle_InternalAlloc_raw,
// token 12095,
ves_icall_System_GCHandle_InternalFree_raw,
// token 12096,
ves_icall_System_GCHandle_InternalGet_raw,
// token 12097,
ves_icall_System_GCHandle_InternalSet_raw,
// token 12115,
ves_icall_System_Runtime_InteropServices_Marshal_GetLastPInvokeError,
// token 12116,
ves_icall_System_Runtime_InteropServices_Marshal_SetLastPInvokeError,
// token 12117,
ves_icall_System_Runtime_InteropServices_Marshal_StructureToPtr_raw,
// token 12164,
ves_icall_System_Runtime_InteropServices_NativeLibrary_LoadByName_raw,
// token 12232,
ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_InternalGetHashCode_raw,
// token 12235,
ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_GetObjectValue_raw,
// token 12244,
ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_GetUninitializedObjectInternal_raw,
// token 12245,
ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_InitializeArray_raw,
// token 12246,
ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_GetSpanDataFrom_raw,
// token 12247,
ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_RunClassConstructor_raw,
// token 12248,
ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_SufficientExecutionStack,
// token 12249,
ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_InternalBox_raw,
// token 12597,
ves_icall_System_Reflection_Assembly_GetExecutingAssembly_raw,
// token 12598,
ves_icall_System_Reflection_Assembly_GetCallingAssembly_raw,
// token 12599,
ves_icall_System_Reflection_Assembly_GetEntryAssembly_raw,
// token 12604,
ves_icall_System_Reflection_Assembly_InternalLoad_raw,
// token 12605,
ves_icall_System_Reflection_Assembly_InternalGetType_raw,
// token 12640,
ves_icall_System_Reflection_AssemblyName_GetNativeName,
// token 12683,
ves_icall_MonoCustomAttrs_GetCustomAttributesInternal_raw,
// token 12690,
ves_icall_MonoCustomAttrs_GetCustomAttributesDataInternal_raw,
// token 12697,
ves_icall_MonoCustomAttrs_IsDefinedInternal_raw,
// token 12708,
ves_icall_System_Reflection_FieldInfo_internal_from_handle_type_raw,
// token 12712,
ves_icall_System_Reflection_FieldInfo_get_marshal_info_raw,
// token 12738,
ves_icall_System_Reflection_LoaderAllocatorScout_Destroy,
// token 12821,
ves_icall_System_Reflection_RuntimeAssembly_GetManifestResourceNames_raw,
// token 12823,
ves_icall_System_Reflection_RuntimeAssembly_GetExportedTypes_raw,
// token 12834,
ves_icall_System_Reflection_RuntimeAssembly_GetInfo_raw,
// token 12836,
ves_icall_System_Reflection_RuntimeAssembly_GetManifestResourceInternal_raw,
// token 12837,
ves_icall_System_Reflection_Assembly_GetManifestModuleInternal_raw,
// token 12838,
ves_icall_System_Reflection_RuntimeAssembly_GetModulesInternal_raw,
// token 12845,
ves_icall_System_Reflection_RuntimeCustomAttributeData_ResolveArgumentsInternal_raw,
// token 12860,
ves_icall_RuntimeEventInfo_get_event_info_raw,
// token 12880,
ves_icall_reflection_get_token_raw,
// token 12881,
ves_icall_System_Reflection_EventInfo_internal_from_handle_type_raw,
// token 12889,
ves_icall_RuntimeFieldInfo_ResolveType_raw,
// token 12891,
ves_icall_RuntimeFieldInfo_GetParentType_raw,
// token 12898,
ves_icall_RuntimeFieldInfo_GetFieldOffset_raw,
// token 12899,
ves_icall_RuntimeFieldInfo_GetValueInternal_raw,
// token 12902,
ves_icall_RuntimeFieldInfo_SetValueInternal_raw,
// token 12904,
ves_icall_RuntimeFieldInfo_GetRawConstantValue_raw,
// token 12909,
ves_icall_reflection_get_token_raw,
// token 12915,
ves_icall_get_method_info_raw,
// token 12916,
ves_icall_get_method_attributes,
// token 12923,
ves_icall_System_Reflection_MonoMethodInfo_get_parameter_info_raw,
// token 12925,
ves_icall_System_MonoMethodInfo_get_retval_marshal_raw,
// token 12937,
ves_icall_System_Reflection_RuntimeMethodInfo_GetMethodFromHandleInternalType_native_raw,
// token 12940,
ves_icall_RuntimeMethodInfo_get_name_raw,
// token 12941,
ves_icall_RuntimeMethodInfo_get_base_method_raw,
// token 12942,
ves_icall_reflection_get_token_raw,
// token 12953,
ves_icall_InternalInvoke_raw,
// token 12963,
ves_icall_RuntimeMethodInfo_GetPInvoke_raw,
// token 12969,
ves_icall_RuntimeMethodInfo_MakeGenericMethod_impl_raw,
// token 12970,
ves_icall_RuntimeMethodInfo_GetGenericArguments_raw,
// token 12971,
ves_icall_RuntimeMethodInfo_GetGenericMethodDefinition_raw,
// token 12973,
ves_icall_RuntimeMethodInfo_get_IsGenericMethodDefinition_raw,
// token 12974,
ves_icall_RuntimeMethodInfo_get_IsGenericMethod_raw,
// token 12991,
ves_icall_InvokeClassConstructor_raw,
// token 12993,
ves_icall_InternalInvoke_raw,
// token 13008,
ves_icall_reflection_get_token_raw,
// token 13030,
ves_icall_System_Reflection_RuntimeModule_GetGuidInternal_raw,
// token 13031,
ves_icall_System_Reflection_RuntimeModule_ResolveMethodToken_raw,
// token 13056,
ves_icall_RuntimeParameterInfo_GetTypeModifiers_raw,
// token 13061,
ves_icall_RuntimePropertyInfo_get_property_info_raw,
// token 13092,
ves_icall_reflection_get_token_raw,
// token 13093,
ves_icall_System_Reflection_RuntimePropertyInfo_internal_from_handle_type_raw,
// token 13725,
ves_icall_CustomAttributeBuilder_GetBlob_raw,
// token 13739,
ves_icall_DynamicMethod_create_dynamic_method_raw,
// token 13828,
ves_icall_AssemblyBuilder_basic_init_raw,
// token 13829,
ves_icall_AssemblyBuilder_UpdateNativeCustomAttributes_raw,
// token 14051,
ves_icall_ModuleBuilder_basic_init_raw,
// token 14052,
ves_icall_ModuleBuilder_set_wrappers_type_raw,
// token 14059,
ves_icall_ModuleBuilder_getUSIndex_raw,
// token 14060,
ves_icall_ModuleBuilder_getToken_raw,
// token 14061,
ves_icall_ModuleBuilder_getMethodToken_raw,
// token 14067,
ves_icall_ModuleBuilder_RegisterToken_raw,
// token 14138,
ves_icall_TypeBuilder_create_runtime_class_raw,
// token 14685,
ves_icall_System_IO_Stream_HasOverriddenBeginEndRead_raw,
// token 14686,
ves_icall_System_IO_Stream_HasOverriddenBeginEndWrite_raw,
// token 15132,
ves_icall_System_Diagnostics_Debugger_IsAttached_internal,
// token 15133,
ves_icall_System_Diagnostics_Debugger_IsLogging,
// token 15134,
ves_icall_System_Diagnostics_Debugger_Log,
// token 15140,
ves_icall_System_Diagnostics_StackFrame_GetFrameInfo,
// token 15150,
ves_icall_System_Diagnostics_StackTrace_GetTrace,
// token 16167,
ves_icall_Mono_RuntimeClassHandle_GetTypeFromClass,
// token 16188,
ves_icall_Mono_RuntimeGPtrArrayHandle_GPtrArrayFree,
// token 16190,
ves_icall_Mono_SafeStringMarshal_StringToUtf8,
// token 16192,
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
