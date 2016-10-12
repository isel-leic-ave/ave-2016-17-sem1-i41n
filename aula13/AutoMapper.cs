using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

public class AutoMapper{
    public static Mapper<KSrc, KDest> Build<KSrc, KDest>()
    {
        return new Mapper<KSrc, KDest>();
    }

    public static IMapper Build(Type klassSrc, Type klassDest)
    {
        return (IMapper) typeof(AutoMapper)
            .GetMethod("Build", Type.EmptyTypes)
            .MakeGenericMethod(klassSrc, klassDest)
            .Invoke(null, null);
    }
}
   
interface IMapping
{
    void Map(object src, object dest);
}

public interface IMapper 
{
    object Map(object src);
}

public class Mapper<TSrc, TDest> :IMapper
{
    private readonly Type klassSrc;
    private readonly Type klassDest;
    private readonly Dictionary<string, IMapping> mappings;
    
    public Mapper() {
        this.klassSrc = typeof(TSrc);
        this.klassDest = typeof(TDest);
        this.mappings = BuildMappings(klassSrc, klassDest);
    }

    private static Dictionary<string, IMapping> BuildMappings(Type klassSrc, Type klassDest) {
        var dic = new Dictionary<string, IMapping>();
        var props = klassDest.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        foreach (var p in props)
        {
            var pSrc = klassSrc.GetProperty(p.Name);
            if (pSrc != null)
            {
                if (pSrc.PropertyType == p.PropertyType)
                    dic.Add(p.Name, new MappingProps(pSrc, p));
                else {
                    if (!pSrc.PropertyType.IsPrimitive && !p.PropertyType.IsPrimitive) {
                        dic.Add(p.Name, new MappingEntities(pSrc, p));
                    }
                }
            }
        }
        return dic;
    }

    public object Map(object src)
    {
        return Map((TSrc)src);
    }

    public TDest Map(TSrc src)
    {
        TDest dest = Activator.CreateInstance<TDest>();
        return mappings
            .Values
            .Aggregate(dest, (acc, mapper) => { mapper.Map(src, acc); return acc; });
    }

    public Mapper<TSrc, TDest> Match(string nameFrom, string nameDest) {
        PropertyInfo pDest = klassDest.GetProperty(nameDest);
        PropertyInfo pSrc = klassSrc.GetProperty(nameFrom);
        if (pDest != null && pSrc != null && pDest.PropertyType == pSrc.PropertyType)
            mappings[pDest.Name] = new MappingProps(pSrc, pDest);
        return this;
    }

    public Mapper<TSrc, TDest> IgnoreMember(string propName)
    {
        return this;
    }

    public Mapper<TSrc, TDest> To(string propName, object value)
    {
        return this;
    }

}

class MappingProps : IMapping
{
    private PropertyInfo pSrc;
    private PropertyInfo p;

    public MappingProps(PropertyInfo pSrc, PropertyInfo p)
    {
        this.pSrc = pSrc;
        this.p = p;
    }

    public void Map(object src, object dest)
    {
        p.SetValue(dest, pSrc.GetValue(src));
    }
}

class MappingEntities : IMapping
{
    private readonly IMapper m;
    private readonly PropertyInfo pSrc;
    private readonly PropertyInfo p;

    public MappingEntities(PropertyInfo pSrc, PropertyInfo p)
    {
        this.m = AutoMapper.Build(pSrc.PropertyType, p.PropertyType);
        this.pSrc = pSrc;
        this.p = p;
    }

    public void Map(object src, object dest)
    {
        object srcVal = pSrc.GetValue(src);
        object destVal = m.Map(srcVal);
        p.SetValue(dest, destVal);
    }
}