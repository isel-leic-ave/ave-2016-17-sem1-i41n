using System;
using System.Collections.Generic;
using System.Reflection;

public class AutoMapper {
    public static Mapper<TSrc, TDest> Build<TSrc, TDest>() {
        return new Mapper<TSrc, TDest>();
    }
    
    public static IMapper Build(Type klassSrc, Type klassDest)
    {
        return (IMapper) typeof(AutoMapper)
            .GetMethod("Build", Type.EmptyTypes)
            .MakeGenericMethod(klassSrc, klassDest)
            .Invoke(null, null);
    }
}

interface IMapping{ void Copy(object src, object dest); }

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

class MappingProps : IMapping {
    readonly PropertyInfo pSrc;
    readonly PropertyInfo pDest;
    public MappingProps( PropertyInfo pSrc, PropertyInfo pDest) {
        this.pSrc = pSrc;
        this.pDest = pDest;
    }
    public void Copy(object src, object dest){
        pDest.SetValue(dest, pSrc.GetValue(src));
    }
}

class MappingEntities : IMapping {
    readonly PropertyInfo pSrc;
    readonly PropertyInfo pDest;
    readonly IMapper mapper;
    public MappingEntities( PropertyInfo pSrc, PropertyInfo pDest) {
        this.pSrc = pSrc;
        this.pDest = pDest;
        this.mapper = AutoMapper.Build(pSrc.PropertyType, pDest.PropertyType);
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

public class Mapper<TSrc, TDest> : IMapper
{
    private readonly Type klassSrc;
    private readonly Type klassDest;
    private readonly Dictionary<String, IMapping> props; // key: dest Prop and Value: src Prop
    
    public Mapper() {
        this.klassSrc = typeof(TSrc);
        this.klassDest = typeof(TDest);
        this.props = new Dictionary<String, IMapping>();
        
        foreach(PropertyInfo p in klassDest.GetProperties()) {
            PropertyInfo pSrc = klassSrc.GetProperty(p.Name);
            if(pSrc != null && pSrc.PropertyType == p.PropertyType) {
                props.Add(p.Name, new MappingProps(pSrc, p));
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

    public Mapper<TSrc, TDest> Match(string nameFrom, string nameDest) {
        PropertyInfo pSrc = klassSrc.GetProperty(nameFrom);
        PropertyInfo pDest = klassDest.GetProperty(nameDest);
        if(pSrc.PropertyType ==  pDest.PropertyType)
            props.Add(nameDest, new MappingProps(pSrc, pDest));
        else if (!pSrc.PropertyType.IsPrimitive &&  !pDest.PropertyType.IsPrimitive) {
            props.Add(nameDest, new MappingEntities(pSrc, pDest));
        }
        return this;
    }

    public Mapper<TSrc, TDest> IgnoreMember(string propName)
    {
        props.Remove(propName);
        return this;
    }

    public Mapper<TSrc, TDest> To(string propName, object value)
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
