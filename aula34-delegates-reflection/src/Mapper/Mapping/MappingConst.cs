using System.Reflection;

namespace Mapper.Mapping
{
    class MappingConst : IMapping
    {
        readonly object value;
        readonly PropertyInfo pDest;
        public MappingConst(object value, PropertyInfo pDest)
        {
            this.value = value;
            this.pDest = pDest;
        }
        public void Copy(object src, object dest)
        {
            pDest.SetValue(dest, value);
        }
    }
}