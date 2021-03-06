﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

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

        public static AutoMapperBuilder Load(string path)
        {
            return new AutoMapperBuilder(path);
        }

        public class AutoMapperBuilder
        {
            private string path;

            public AutoMapperBuilder(string path)
            {
                this.path = path;
            }

            public MapperEmit<TSrc, TDest> Build<TSrc, TDest>()
            {
                return new MapperEmit<TSrc, TDest>(LoadMappers(path));
            }
        }

        /// <returns>Dictionary<Type, IMapper>  Where key is the type of the Src and the value is a Mapper</returns>
        public static Dictionary<Type, IMapper> LoadMappers(string path) {
            Dictionary<Type, IMapper> res = new Dictionary<Type, IMapper>();
            IEnumerable<MethodInfo> ms = Assembly
                .LoadFrom(path)                  // Assembly
                .GetTypes()                      // Type[]
                .SelectMany(t => t.GetMethods()) // IEnumerable<MethodInfo>
                .Where(IsMapperMethod);          // IEnumerable<MethodInfo>
            foreach (MethodInfo m in ms)
            {
                res.Add(m.GetParameters()[0].ParameterType, new MapperForMethod(m));
            }
            return res; 
        }
        public static bool IsMapperMethod(MethodInfo m)
        {
            if (!m.IsStatic || m.ReturnType == typeof(void)) return false;
            return
                Attribute.IsDefined(m, typeof(MapperAttribute))
                && m.GetParameters().Length == 1;
        }

        class MapperForMethod : IMapper
        {
            private Func<object, object> handler;

            public MapperForMethod(MethodInfo m)
            {
                this.handler = (Func<object, object>) Delegate.CreateDelegate(
                    typeof(Func<object, object>),
                    m);
            }

            public object Map(object src)
            {
                return handler(src);
            }
        }
    }
}
