using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

public class AutoMapperEmit {
    public static MapperEmit<TSrc, TDest> Build<TSrc, TDest>() {
        return new MapperEmit<TSrc, TDest>();
    }
    
    public static IMapper Build(Type klassSrc, Type klassDest)
    {
        return (IMapper) typeof(AutoMapperEmit)
            .GetMethod("Build", Type.EmptyTypes)
            .MakeGenericMethod(klassSrc, klassDest)
            .Invoke(null, null);
    }
}

public interface IMapping{ void Copy(object src, object dest); }

class MappingConst : IMapping {
    readonly object value;
    readonly PropertyInfo pDest;
    public MappingConst(object value, PropertyInfo pDest) {
        this.value = value;
        this.pDest = pDest;
    }
    public void Copy(object src, object dest){
        pDest.SetValue(dest, value);
    }
}


class MappingPropsBuidler
{
    public static IMapping CreateMappingProps(PropertyInfo pSrc, PropertyInfo pDest)
    {
        string asmName = "Dyn" + pSrc.Name + "To" + pDest.Name;
        AssemblyBuilder  asm = CreateAsm(asmName);
        ModuleBuilder mb = asm.DefineDynamicModule(asmName, asmName + ".dll");
        TypeBuilder tb = mb.DefineType(
            asmName, 
            TypeAttributes.Public,
            typeof(object),
            new Type[] {typeof(IMapping)});
        
        MethodBuilder copyBuilder = tb.DefineMethod(
            "Copy",
            MethodAttributes.Virtual | MethodAttributes.Public | MethodAttributes.ReuseSlot,
            typeof(void),
            new Type[]{typeof(object), typeof(object)}
        );
        CreateCopyMethod(copyBuilder, pSrc, pDest);
        // Finish the type.
        Type t = tb.CreateType();
        asm.Save(asmName + ".dll");
        return (IMapping) Activator.CreateInstance(t);
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
    
    private static AssemblyBuilder CreateAsm(string name) {
        AssemblyName aName = new AssemblyName(name);
        AssemblyBuilder ab = 
            AppDomain.CurrentDomain.DefineDynamicAssembly(
                aName, 
                AssemblyBuilderAccess.RunAndSave);
        return ab;
    }
}

class MappingEntities : IMapping {
    readonly PropertyInfo pSrc;
    readonly PropertyInfo pDest;
    readonly IMapper mapper;
    public MappingEntities( PropertyInfo pSrc, PropertyInfo pDest) {
        this.pSrc = pSrc;
        this.pDest = pDest;
        this.mapper = AutoMapperEmit.Build(pSrc.PropertyType, pDest.PropertyType);
    }
    public void Copy(object src, object dest){
        object val = pSrc.GetValue(src);
        val = mapper.Map(val);
        pDest.SetValue(dest, val);
    }
}

public interface IMapper {
    object Map(object src);
}

public class MapperEmit<TSrc, TDest> : IMapper
{
    private readonly Type klassSrc;
    private readonly Type klassDest;
    private readonly Dictionary<String, IMapping> props; // key: dest Prop and Value: src Prop
    
    public MapperEmit() {
        this.klassSrc = typeof(TSrc);
        this.klassDest = typeof(TDest);
        this.props = new Dictionary<String, IMapping>();
        
        foreach(PropertyInfo p in klassDest.GetProperties()) {
            PropertyInfo pSrc = klassSrc.GetProperty(p.Name);
            if(pSrc != null && pSrc.PropertyType == p.PropertyType) {
                props.Add(p.Name, MappingPropsBuidler.CreateMappingProps(pSrc, p));
            }
        }
    }

    public object Map(object src){
        return this.Map((TSrc) src);
    }
    
    public TDest Map(TSrc src)
    {
        TDest dest = (TDest) Activator.CreateInstance(klassDest); // Assume a parameterless ctor
        foreach(IMapping m in props.Values)
        {
            m.Copy(src, dest);
        }
        return dest;
        
    }

    public MapperEmit<TSrc, TDest> Match(string nameFrom, string nameDest) {
        PropertyInfo pSrc = klassSrc.GetProperty(nameFrom);
        PropertyInfo pDest = klassDest.GetProperty(nameDest);
        if(pSrc.PropertyType ==  pDest.PropertyType)
            props.Add(nameDest, MappingPropsBuidler.CreateMappingProps(pSrc, pDest));
        else if (!pSrc.PropertyType.IsPrimitive &&  !pDest.PropertyType.IsPrimitive) {
            props.Add(nameDest, new MappingEntities(pSrc, pDest));
        }
        return this;
    }

    public MapperEmit<TSrc, TDest> IgnoreMember(string propName)
    {
        props.Remove(propName);
        return this;
    }

    public MapperEmit<TSrc, TDest> To(string propName, object value)
    {
        PropertyInfo p = klassDest.GetProperty(propName);
        if(p != null) {
            if(p.PropertyType != value.GetType())
                throw new ArgumentException("Property Type is not compatible with " + value.GetType());
            props.Remove(propName);
            props.Add(propName, new MappingConst(value, p));
        }
        return this;
    }
}
