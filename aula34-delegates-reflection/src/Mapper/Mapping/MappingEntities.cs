using System;
using System.Collections.Generic;
using System.Reflection;

namespace Mapper.Mapping
{
    class MappingEntities : IMapping
    {
        readonly PropertyInfo pSrc;
        readonly PropertyInfo pDest;
        readonly IMapper mapper;

        public MappingEntities(PropertyInfo pSrc, PropertyInfo pDest, Dictionary<Type, IMapper> mappers)
        {
            this.pSrc = pSrc;
            this.pDest = pDest;
            if (mappers != null && mappers.ContainsKey(pSrc.PropertyType))
                this.mapper = mappers[pSrc.PropertyType];
            else
                this.mapper = AutoMapperEmit.Build(pSrc.PropertyType, pDest.PropertyType);
        }
        public void Copy(object src, object dest)
        {
            object val = pSrc.GetValue(src);
            val = mapper.Map(val);
            pDest.SetValue(dest, val);
        }
    }
}