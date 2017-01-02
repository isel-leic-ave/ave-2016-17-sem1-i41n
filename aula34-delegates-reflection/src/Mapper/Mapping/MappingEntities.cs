using System.Reflection;

namespace Mapper.Mapping
{
    class MappingEntities : IMapping
    {
        readonly PropertyInfo pSrc;
        readonly PropertyInfo pDest;
        readonly IMapper mapper;
        public MappingEntities(PropertyInfo pSrc, PropertyInfo pDest)
        {
            this.pSrc = pSrc;
            this.pDest = pDest;
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