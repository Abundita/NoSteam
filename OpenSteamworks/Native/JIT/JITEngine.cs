﻿using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Linq;

namespace OpenSteamworks.Native.JIT
{
    class JITEngineException : Exception
    {
        public JITEngineException(string message) : base(message) { }
    }

    public static class JITEngine
    {
        private static ModuleBuilder moduleBuilder;

        static JITEngine()
        {
            //TODO: re-add AssemblyBuilderAccess.RunAndSave when it is implemented
            AssemblyBuilder assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(new AssemblyName("OpenSteamworksJIT"), AssemblyBuilderAccess.Run);

#if DEBUG
            Type daType = typeof(DebuggableAttribute);
            ConstructorInfo? daCtor = daType.GetConstructor(new Type[] { typeof(DebuggableAttribute.DebuggingModes) });
            if (daCtor != null) {
                CustomAttributeBuilder daBuilder = new CustomAttributeBuilder(daCtor, new object[] { 
                DebuggableAttribute.DebuggingModes.DisableOptimizations | 
                DebuggableAttribute.DebuggingModes.Default });
                assemblyBuilder.SetCustomAttribute(daBuilder);
            }
#endif

            moduleBuilder = assemblyBuilder.DefineDynamicModule("OpenSteamworksJIT");
        }

        private static void EmitPrettyLoad(ILGenerator ilgen, int index)
        {
            switch (index)
            {
                case 0: ilgen.Emit(OpCodes.Ldarg_0); break;
                case 1: ilgen.Emit(OpCodes.Ldarg_1); break;
                case 2: ilgen.Emit(OpCodes.Ldarg_2); break;
                case 3: ilgen.Emit(OpCodes.Ldarg_3); break;
                default: ilgen.Emit(OpCodes.Ldarg_S, (byte)index); break;
            }
        }

        private static void EmitPrettyLoadLocal(ILGenerator ilgen, int index)
        {
            switch (index)
            {
                case 0: ilgen.Emit(OpCodes.Ldloc_0); break;
                case 1: ilgen.Emit(OpCodes.Ldloc_1); break;
                case 2: ilgen.Emit(OpCodes.Ldloc_2); break;
                case 3: ilgen.Emit(OpCodes.Ldloc_3); break;
                default: ilgen.Emit(OpCodes.Ldloc_S, (byte)index); break;
            }
        }

        private static void EmitPrettyStoreLocal(ILGenerator ilgen, int index)
        {
            switch (index)
            {
                case 0: ilgen.Emit(OpCodes.Stloc_0); break;
                case 1: ilgen.Emit(OpCodes.Stloc_1); break;
                case 2: ilgen.Emit(OpCodes.Stloc_2); break;
                case 3: ilgen.Emit(OpCodes.Stloc_3); break;
                default: ilgen.Emit(OpCodes.Stloc_S, (byte)index); break;
            }
        }

        private static void EmitPlatformLoad(ILGenerator ilgen, IntPtr pointer)
        {
            switch (IntPtr.Size)
            {
                case 4: ilgen.Emit(OpCodes.Ldc_I4, (int)pointer.ToInt32()); break;
                case 8: ilgen.Emit(OpCodes.Ldc_I8, (long)pointer.ToInt64()); break;
                default: throw new JITEngineException("Bad IntPtr size");
            }

            ilgen.Emit(OpCodes.Conv_I);
        }

        public static TClass GenerateClass<TClass>(IntPtr classptr) where TClass : class
        {
            if (classptr == IntPtr.Zero)
            {
                throw new JITEngineException("GenerateClass called with NULL ptr");
            }

            IntPtr vtable_ptr = Marshal.ReadIntPtr(classptr);

            Type targetInterface = typeof(TClass);

            TypeBuilder builder = moduleBuilder.DefineType(targetInterface.Name + "_" + (IntPtr.Size * 8).ToString(),
                                                    TypeAttributes.Class, null, new Type[] { targetInterface });
            
            
            FieldBuilder fbuilder = builder.DefineField("ObjectAddress", typeof(IntPtr), FieldAttributes.Public);

            ClassJITInfo classInfo = new(targetInterface);

            for (int i = 0; i < classInfo.Methods.Count; i++)
            {
                IntPtr vtableMethod = Marshal.ReadIntPtr(vtable_ptr, IntPtr.Size * classInfo.Methods[i].VTableSlot);
                MethodJITInfo methodInfo = classInfo.Methods[i];
                EmitClassMethod(methodInfo, classptr, vtableMethod, builder, fbuilder);
            }

            Type implClass = builder.CreateType();
            Object? instClass = Activator.CreateInstance(implClass);
            if (instClass == null) {
                throw new JITEngineException("Failed to CreateInstance of implClass");
            }

            FieldInfo? addressField = implClass.GetField("ObjectAddress", BindingFlags.Public | BindingFlags.Instance);
            if (addressField == null) {
                throw new JITEngineException("ObjectAddress field wasn't defined");
            }

            addressField.SetValue(instClass, classptr);

            return (TClass)instClass;
        }

        private static int generatedClasses = 0;
        public static TClass GenerateUniqueClass<TClass>(IntPtr classptr) where TClass : class 
        {
            if (classptr == IntPtr.Zero)
            {
                throw new JITEngineException("GenerateClass called with NULL ptr");
            }

            IntPtr vtable_ptr = Marshal.ReadIntPtr(classptr);

            Type targetInterface = typeof(TClass);

            TypeBuilder builder = moduleBuilder.DefineType(targetInterface.Name + "_" + (IntPtr.Size * 8).ToString() + "_" + generatedClasses.ToString(),
                                                    TypeAttributes.Class, null, new Type[] { targetInterface });
            generatedClasses++;
            
            FieldBuilder fbuilder = builder.DefineField("ObjectAddress", typeof(IntPtr), FieldAttributes.Public);

            ClassJITInfo classInfo = new(targetInterface);

            for (int i = 0; i < classInfo.Methods.Count; i++)
            {
                IntPtr vtableMethod = Marshal.ReadIntPtr(vtable_ptr, IntPtr.Size * classInfo.Methods[i].VTableSlot);
                MethodJITInfo methodInfo = classInfo.Methods[i];
                EmitClassMethod(methodInfo, classptr, vtableMethod, builder, fbuilder);
            }

            Type implClass = builder.CreateType();
            Object? instClass = Activator.CreateInstance(implClass);
            if (instClass == null) {
                throw new JITEngineException("Failed to CreateInstance of implClass");
            }

            FieldInfo? addressField = implClass.GetField("ObjectAddress", BindingFlags.Public | BindingFlags.Instance);
            if (addressField == null) {
                throw new JITEngineException("ObjectAddress field wasn't defined");
            }

            addressField.SetValue(instClass, classptr);

            return (TClass)instClass;
        }

        private class MethodState
        {
            public struct RefArgLocal
            {
                public LocalBuilder builder;
                public int argIndex;
                public Type paramType;
            }

            public MethodState()
            {
                MethodArgs = new List<Type>();
                NativeArgs = new List<Type>();
                unmanagedMemory = new List<LocalBuilder>();
                refargLocals = new List<RefArgLocal>();
            }

            public List<Type> MethodArgs;
            public Type? MethodReturn;

            public List<Type> NativeArgs;
            public Type? NativeReturn;

            public bool ReturnTypeByStack;
            public LocalBuilder? localReturn;

            public List<LocalBuilder> unmanagedMemory;
            public List<RefArgLocal> refargLocals;
        }

        private static void EmitClassMethod(MethodJITInfo method, IntPtr objectptr, IntPtr methodptr, TypeBuilder builder, FieldBuilder addressAssistant)
        {
            MethodState state = new MethodState();

            state.NativeArgs.Add(typeof(IntPtr)); // thisptr

            state.ReturnTypeByStack = method.ReturnType.DetermineProps();

            if (state.ReturnTypeByStack)
            {
                // ref to the native return type
                state.NativeArgs.Add(method.ReturnType.NativeType.MakeByRefType());
                state.NativeReturn = null;
            }
            else if (method.ReturnType.IsStringClass)
            {
                // special case for strings, we will marshal it in ourselves
                state.NativeReturn = typeof(IntPtr);
            }
            else
            {
                state.NativeReturn = method.ReturnType.NativeType;
            }

            state.MethodReturn = method.ReturnType.Type;

            foreach (TypeJITInfo typeInfo in method.Args)
            {
                // populate MethodArgs and NativeArgs now
                typeInfo.DetermineProps();

                state.MethodArgs.Add(typeInfo.Type);

                if (typeInfo.IsStringClass)
                {
                    // we need to specially marshal strings
                    state.NativeArgs.Add(typeof(IntPtr));
                }
                else
                if (!typeInfo.IsParams)
                {
                    if (typeInfo.IsByRef && typeInfo.NativeType.IsValueType)
                    {
                        state.NativeArgs.Add(typeInfo.NativeType.MakeByRefType());
                    }
                    else
                    {
                        state.NativeArgs.Add(typeInfo.NativeType);
                    }
                }
            }
            var paramInfos = method.MethodInfo.GetParameters();
            MethodBuilder mbuilder = builder.DefineMethod(method.Name, MethodAttributes.Public | MethodAttributes.HideBySig | MethodAttributes.NewSlot | MethodAttributes.Virtual, CallingConventions.HasThis);
            if (method.ReturnType.IsGeneric)
            {
                // create generic param
                GenericTypeParameterBuilder[] gtypeParameters;
                gtypeParameters = mbuilder.DefineGenericParameters(new string[] { "TClass" });

                state.MethodReturn = gtypeParameters[0];
                gtypeParameters[0].SetGenericParameterAttributes(GenericParameterAttributes.ReferenceTypeConstraint);
            }

            mbuilder.SetSignature(state.MethodReturn, method.MethodInfo.ReturnParameter.GetRequiredCustomModifiers(), method.MethodInfo.ReturnParameter.GetOptionalCustomModifiers(), state.MethodArgs.ToArray(), paramInfos.Select(pi => pi.GetRequiredCustomModifiers()).ToArray(), paramInfos.Select(pi => pi.GetOptionalCustomModifiers()).ToArray());

            builder.DefineMethodOverride(mbuilder, method.MethodInfo);

            ILGenerator ilgen = mbuilder.GetILGenerator();

            // load object pointer
            EmitPlatformLoad(ilgen, objectptr);

            if (state.ReturnTypeByStack)
            {
                // allocate local to hold the return
                state.localReturn = ilgen.DeclareLocal(method.ReturnType.NativeType, true);

                //BLOCKED: Add this when this function is reimplemented in .NET Core
                // It's only debugging info, but who needs that anyway? C/C# autogen work flawlessly always, right?
                //state.localReturn.SetLocalSymInfo("nativeReturnPlaceholder");

                ilgen.Emit(OpCodes.Ldloca_S, state.localReturn.LocalIndex);
            }

            int argindex = 0;
            foreach (TypeJITInfo typeInfo in method.Args)
            {
                argindex++;

                // perform any conversions necessary
                if (typeInfo.NativeType != typeInfo.Type && typeInfo.IsByRef)
                {
                    LocalBuilder localArg = ilgen.DeclareLocal(typeInfo.NativeType);
                    //BLOCKED: Add this when this function is reimplemented in .NET Core
                    //localArg.SetLocalSymInfo("byrefarg" + argindex);

                    var helper = new MethodState.RefArgLocal();
                    helper.builder = localArg;
                    helper.argIndex = argindex;
                    helper.paramType = typeInfo.PierceType;

                    state.refargLocals.Add(helper);
                    ilgen.Emit(OpCodes.Ldloca_S, localArg);
                }
                else if (typeInfo.NativeType != typeInfo.Type && !typeInfo.Type.IsEnum)
                {
                    EmitPrettyLoad(ilgen, argindex);
                    ilgen.EmitCall(OpCodes.Call, typeInfo.Type.GetMethod("GetValue"), null);
                }
                else
                {
                    EmitPrettyLoad(ilgen, argindex);
                }

                if ((typeInfo.IsStringClass || typeInfo.IsParams) && method.HasParams)
                {
                    if (!typeInfo.IsParams)
                        continue;

                    ilgen.EmitCall(OpCodes.Call, typeof(String).GetMethod("Format", BindingFlags.Public | BindingFlags.Static, null, new Type[] { typeof(string), typeof(object[]) }, null), null);
                }

                if (typeInfo.IsStringClass || typeInfo.IsParams)
                {
                    LocalBuilder localString = ilgen.DeclareLocal(typeof(GCHandle));
                    //BLOCKED: Add this when this function is reimplemented in .NET Core
                    //localString.SetLocalSymInfo("nativeString" + argindex);

                    state.unmanagedMemory.Add(localString);

                    // we need to specially marshal strings
                    ilgen.Emit(OpCodes.Ldloca, localString.LocalIndex);
                    ilgen.EmitCall(OpCodes.Call, typeof(InteropHelp).GetMethod("EncodeUTF8String"), null);
                }
                else if (typeInfo.IsCreatableClass)
                {
                    // if this argument is a class we understand: get the object pointer
                    ilgen.Emit(OpCodes.Ldfld, addressAssistant);
                }
            }

            if (method.ReturnType.IsGeneric)
            {
                ilgen.Emit(OpCodes.Ldtoken, method.ReturnType.Type);
                ilgen.EmitCall(OpCodes.Call, typeof(Type).GetMethod("GetTypeFromHandle", BindingFlags.Static | BindingFlags.Public), null);
            }

            // load vtable method pointer
            EmitPlatformLoad(ilgen, methodptr);

            CallingConvention ccv = CallingConvention.ThisCall;

            if (method.HasParams)
                ccv = CallingConvention.Cdecl;

            if (state.NativeReturn == typeof(bool))
                state.NativeReturn = typeof(byte);

            ilgen.EmitCalli(OpCodes.Calli, ccv, state.NativeReturn, state.NativeArgs.ToArray());

            // populate byref args
            foreach (var local in state.refargLocals)
            {
                EmitPrettyLoad(ilgen, local.argIndex);
                EmitPrettyLoadLocal(ilgen, local.builder.LocalIndex);
                ilgen.Emit(OpCodes.Newobj, local.paramType.GetConstructor(new Type[] { local.builder.LocalType }));
                ilgen.Emit(OpCodes.Stind_Ref);
            }

            // clean up unmanaged memory
            foreach (LocalBuilder localbuilder in state.unmanagedMemory)
            {
                ilgen.Emit(OpCodes.Ldloca, localbuilder.LocalIndex);
                ilgen.EmitCall(OpCodes.Call, typeof(InteropHelp).GetMethod("FreeString"), null);
            }

            if (state.ReturnTypeByStack)
            {
                EmitPrettyLoadLocal(ilgen, state.localReturn.LocalIndex);

                // reconstruct return type
                if (state.localReturn.LocalType != state.MethodReturn)
                {
                    ilgen.Emit(OpCodes.Newobj, state.MethodReturn.GetConstructor(new Type[] { state.localReturn.LocalType }));
                }
            }
            else if (method.ReturnType.IsCreatableClass)
            {
                if (method.ReturnType.IsGeneric)
                {
                    ilgen.EmitCall(OpCodes.Call, typeof(JITEngine).GetMethod("GenerateClass", BindingFlags.Static | BindingFlags.Public), null);
                }
                else if (method.ReturnType.IsDelegate)
                {
                    ilgen.Emit(OpCodes.Ldtoken, method.ReturnType.Type);
                    ilgen.EmitCall(OpCodes.Call, typeof(Type).GetMethod("GetTypeFromHandle"), null);
                    ilgen.EmitCall(OpCodes.Call, typeof(Marshal).GetMethod("GetDelegateForFunctionPointer", BindingFlags.Static | BindingFlags.Public), null);
                    ilgen.Emit(OpCodes.Castclass, method.ReturnType.Type);
                }
                else
                {
                    ilgen.EmitCall(OpCodes.Call, typeof(JITEngine).GetMethod("GenerateClass", BindingFlags.Static | BindingFlags.Public).MakeGenericMethod(method.ReturnType.Type), null);
                }
            }
            else if (method.ReturnType.IsStringClass)
            {
                // marshal string return
                ilgen.EmitCall(OpCodes.Call, typeof(InteropHelp).GetMethod("DecodeUTF8String"), null);
            }

            ilgen.Emit(OpCodes.Ret);
        }
    }
}
