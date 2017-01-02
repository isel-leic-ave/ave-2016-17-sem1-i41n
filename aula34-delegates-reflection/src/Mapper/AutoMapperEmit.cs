using System;

namespace Mapper
{
    public class AutoMapperEmit
    {
        public static MapperEmit<TSrc, TDest> Build<TSrc, TDest>()
        {
            return new MapperEmit<TSrc, TDest>();
        }

        public static IMapper Build(Type klassSrc, Type klassDest)
        {
            return (IMapper)typeof(AutoMapperEmit)
                .GetMethod("Build", Type.EmptyTypes)
                .MakeGenericMethod(klassSrc, klassDest)
                .Invoke(null, null);
        }
    }
}
