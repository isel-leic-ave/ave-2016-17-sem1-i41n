using Mapper.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Mapper
{
    public class MapperEmit<TSrc, TDest> : IMapper
    {
        private readonly Type klassSrc;
        private readonly Type klassDest;
        private readonly Dictionary<String, IMapping> props; // key: dest Prop and Value: src Prop

        public MapperEmit()
        {
            this.klassSrc = typeof(TSrc);
            this.klassDest = typeof(TDest);
            this.props = new Dictionary<String, IMapping>();

            foreach (PropertyInfo p in klassDest.GetProperties())
            {
                PropertyInfo pSrc = klassSrc.GetProperty(p.Name);
                if (pSrc != null && pSrc.PropertyType == p.PropertyType)
                {
                    props.Add(p.Name, MappingPropsBuidler.CreateMappingProps(pSrc, p));
                }
            }
        }

        public object Map(object src)
        {
            return this.Map((TSrc)src);
        }

        public TDest Map(TSrc src)
        {
            TDest dest = (TDest)Activator.CreateInstance(klassDest); // Assume a parameterless ctor
            foreach (IMapping m in props.Values)
            {
                m.Copy(src, dest);
            }
            return dest;
        }
        public TDest[] Map(TSrc[] src) {
            /*
            // 1. Criar o array destino
            TDest[] res = new TDest[src.Length];

            // 2. copiar do array src para o array destino + transformção dos items
            for (int i = 0; i < src.Length; i++)
            {
                res[i] = Map(src[i]);
            }
            // 3. retornar o array
            return res;
            */
            return src.Select(item => Map(item)).ToArray();
        }

        public MapperEmit<TSrc, TDest> Match(string nameFrom, string nameDest)
        {
            PropertyInfo pSrc = klassSrc.GetProperty(nameFrom);
            PropertyInfo pDest = klassDest.GetProperty(nameDest);
            if (pSrc.PropertyType == pDest.PropertyType)
                props.Add(nameDest, MappingPropsBuidler.CreateMappingProps(pSrc, pDest));
            else if (!pSrc.PropertyType.IsPrimitive && !pDest.PropertyType.IsPrimitive)
            {
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
            if (p != null)
            {
                if (p.PropertyType != value.GetType())
                    throw new ArgumentException("Property Type is not compatible with " + value.GetType());
                props.Remove(propName);
                props.Add(propName, new MappingConst(value, p));
            }
            return this;
        }
    }

}