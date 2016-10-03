using System;
using System.Reflection;
using System.Collections.Generic;

class App {

    public static void Main() {
        ICollection<Type> ts = SelectTypes("RestSharp.dll", typeof(IComparable));
        PrintTypes(ts);
    }

    public static void PrintTypes(ICollection<Type> ts) {
        foreach(Type t in ts){
            Console.Write(t.Name + " ----|> "+ t.BaseType);
            foreach(Type itf in t.GetInterfaces()) {
                Console.Write(", " + itf.Name);
            }
            Console.WriteLine();
            DumpMethods(t);
        }
        Console.WriteLine("It has " + ts.Count + " types");
    }
    
    public static ICollection<Type> SelectTypes(string path, Type baseItf) {
        Assembly asm = Assembly.LoadFrom(path);
        ICollection<Type> ts = new HashSet<Type>(); 
        foreach(Type t in asm.GetTypes()){
            if(t.IsPublic) {
                foreach(Type itf in t.GetInterfaces()) {
                    if(itf == baseItf) {
                        ts.Add(t);
                        break;
                    }
                }
            }
        }
        return ts;
    }
    
    public static void DumpMethods(Type t) {
        // MemberInfo[] ms = t.GetMembers(); // Membros Publicos
        MethodInfo[] ms = t.GetMethods(BindingFlags.Instance | BindingFlags.Public);
        foreach(MethodInfo m in ms){
            Console.Write("   - " + m.Name + "(");
            foreach(ParameterInfo p in m.GetParameters()) {
                Console.Write(p.ParameterType + " " + p.Name);
            }
            Console.WriteLine(")");
        }
    }
}