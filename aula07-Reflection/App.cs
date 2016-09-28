using System;
using System.Reflection;
using System.Collections.Generic;

class App {
    static void Main() {
        Dump("RestSharp.dll");
    }
    static void Dump(string path) {
        Assembly asm = Assembly.LoadFrom(path);
        Console.WriteLine(asm.FullName);
        
        Type[] ts = asm.GetTypes();
        List<Type> typesFromRestsharp  = new List<Type>();
        for(int i = 0; i < ts.Length; i++) {
            if(ts[i].Namespace != null && ts[i].Namespace.Equals("RestSharp") && !ts[i].Name.Contains("<>"))
                typesFromRestsharp.Add(ts[i]);
        }
       
        // for(int i = 0; i < typesFromRestsharp.Count; i++) {
        //    Type klass = typesFromRestsharp[i];
        
        Type rest = SearchType(typesFromRestsharp, "IRestClient");
        
        Console.WriteLine("Types from Restsharp namespace = " + typesFromRestsharp.Count);
        Console.WriteLine("Implementation of IRestClient = " + rest.FullName);
        DumpMethods(rest);
    }
    static void DumpFields(Type klass) {
        // Print the fields name and type
    }
    static void DumpMethods(Type klass) {
        MethodInfo [] ms = klass.GetMethods(); // Membros Publicos, de Instância e Estáticos
        int total = 0;
        for(int i = 0; i < ms.Length; i++) {
            total++;
            Console.Write(ms[i].Name + "(");
            ParameterInfo[] args = ms[i].GetParameters();
            for(int j = 0; j < args.Length; j++) {
                Console.Write(args[j].ParameterType.Name + " ");
                Console.Write(args[j].Name); // Não existe em Java. O nome dos params não é gravado na metadata
                if(j <= (args.Length - 1)) Console.Write(", ");
            }
            Console.WriteLine(")");
        }   
        Console.WriteLine("Nr of public methods = " + total);
    }
    
    static void DumpMembers(Type klass) {
        // MemberInfo [] ms = klass.GetMembers(); // Membros Publicos, de Isntância e Estáticos
        MemberInfo [] ms = klass.GetMembers(
            BindingFlags.NonPublic | 
            BindingFlags.Public |
            BindingFlags.Static | 
            BindingFlags.Instance 
        ); 
        int total = 0;
        for(int i = 0; i < ms.Length; i++) {
            total++;
            Console.WriteLine(ms[i].Name + " -- " + ms[i].MemberType);
        }   
        Console.WriteLine("Nr of public members = " + total);
    }
    
    static Type SearchType(List<Type> src, String itf) {
        foreach(Type klass in src) {
            Type [] itfs = klass.GetInterfaces();
            for(int i = 0; i < itfs.Length; i++) {
                if(itfs[i].Name.Equals(itf)) {
                    return itfs[i];
                }
            }   
        }
        throw new ArgumentException("There is no Type for interface argument: " + itf);
    }
    static void DumpType(Type klass) {
        Console.Write(klass.FullName);
        string supertype = klass.BaseType == null ? "Object" : klass.BaseType.Name;
        Console.Write(" ----|> " + supertype);
        Type [] itfs = klass.GetInterfaces();
        for(int i = 0; i < itfs.Length; i++) {
            Console.Write(", " + itfs[i].Name);
        }
        Console.WriteLine();
    }
    
}
