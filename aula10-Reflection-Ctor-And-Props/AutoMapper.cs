using System;
using System.Collections.Generic;
using System.Reflection;

public class AutoMapper {
    public static Mapper<TSrc, TDest> Build<TSrc, TDest>() {
        return new Mapper<TSrc, TDest>();
    }
}

public class Mapper<TSrc, TDest>
{
    private readonly Type klassSrc;
    private readonly Type klassDest;
    private readonly Dictionary<PropertyInfo, PropertyInfo> props; // key: dest Prop and Value: src Prop
    private readonly Dictionary<PropertyInfo, object> propConstants; // key: dest Prop
    // private readonly Dictionary<PropertyInfo, Generator> propConstants; // key: dest Prop
    
    public Mapper() {
        this.klassSrc = typeof(TSrc);
        this.klassDest = typeof(TDest);
        this.props = new Dictionary<PropertyInfo, PropertyInfo>();
        foreach(PropertyInfo p in klassDest.GetProperties()) {
            PropertyInfo pSrc = klassSrc.GetProperty(p.Name);
            if(pSrc != null && pSrc.PropertyType == p.PropertyType) {
                props.Add(p, pSrc);
            }
        }
    }

    public TDest Map(TSrc src)
    {
        TDest dest = (TDest) Activator.CreateInstance(klassDest); // Assume a parameterless ctor
        foreach(KeyValuePair<PropertyInfo, PropertyInfo> kvp in props)
        {
            object srcVal = kvp.Value.GetValue(src);
            kvp.Key.SetValue(dest, srcVal);
        }
        return dest;
        
    }

    public Mapper<TSrc, TDest> Match(string nameFrom, string nameDest) {
        return this;
    }

    public Mapper<TSrc, TDest> IgnoreMember(string propName)
    {
        PropertyInfo p = klassDest.GetProperty(propName);
        if(p != null){
            props.Remove(p);
        }
        return this;
    }

    public Mapper<TSrc, TDest> To(string propName, object value)
    {
        return this;
    }
}
