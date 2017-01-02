using System;
using System.Reflection;
using System.Reflection.Emit;

namespace Mapper.Mapping
{
    class MappingPropsBuidler
    {
        public static IMapping CreateMappingProps(PropertyInfo pSrc, PropertyInfo pDest)
        {
            string asmName = "Dyn" + pSrc.Name + "To" + pDest.Name;
            AssemblyBuilder asm = CreateAsm(asmName);
            ModuleBuilder mb = asm.DefineDynamicModule(asmName, asmName + ".dll");
            TypeBuilder tb = mb.DefineType(
                asmName,
                TypeAttributes.Public,
                typeof(object),
                new Type[] { typeof(IMapping) });

            MethodBuilder copyBuilder = tb.DefineMethod(
                "Copy",
                MethodAttributes.Virtual | MethodAttributes.Public | MethodAttributes.ReuseSlot,
                typeof(void),
                new Type[] { typeof(object), typeof(object) }
            );
            CreateCopyMethod(copyBuilder, pSrc, pDest);
            // Finish the type.
            Type t = tb.CreateType();
            asm.Save(asmName + ".dll");
            return (IMapping)Activator.CreateInstance(t);
        }
        private static void CreateCopyMethod(MethodBuilder copyBuilder, PropertyInfo pSrc, PropertyInfo pDest)
        {
            ILGenerator il = copyBuilder.GetILGenerator();
            il.Emit(OpCodes.Ldarg_2);
            il.Emit(OpCodes.Ldarg_1);
            il.Emit(OpCodes.Call, pSrc.GetGetMethod());
            il.Emit(OpCodes.Call, pDest.GetSetMethod());
            il.Emit(OpCodes.Ret);
        }

        private static AssemblyBuilder CreateAsm(string name)
        {
            AssemblyName aName = new AssemblyName(name);
            AssemblyBuilder ab =
                AppDomain.CurrentDomain.DefineDynamicAssembly(
                    aName,
                    AssemblyBuilderAccess.RunAndSave);
            return ab;
        }
    }
}