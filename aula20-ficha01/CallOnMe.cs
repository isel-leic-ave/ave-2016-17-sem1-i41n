using System;
using System.Collections.Generic;
using System.Reflection;


public class App
{
    public static void Main() {
        
    }
    
    public static Caller CallOneMe(Type klass)
    {
        List<CallerHandler> handlers = new List<CallerHandler>();
        object target = Activator.CreateInstance(klass);
        foreach (var m in klass.GetMethods(
            BindingFlags.Public |
            BindingFlags.NonPublic |
            BindingFlags.Instance |
            BindingFlags.Static))
        {
            ParameterInfo[] args = m.GetParameters();
            if (m.ReturnType == typeof(void) && args.Length == 1 && args[0].ParameterType == typeof(long))
            {
                handlers.Add(new CallerHandler(target, m));
            }
        }
        return new Caller(handlers);
    }
}

public class Caller
{
    private List<CallerHandler> handlers;
    private long start;
    public Caller(List<CallerHandler> handlers)
    {
        this.handlers = handlers;
        this.start = DateTime.Now.Ticks;
    }
    public void Call()
    {
        if (handlers.Count == 0) return;
        long curr = DateTime.Now.Ticks;
        for (int i = 0; i < handlers.Count; i++)
        {
            long diff = curr - start;
            if (diff < handlers[i].Due)
                handlers[i].Call(diff);
            else { 
                handlers.RemoveAt(i);
                i--;
            }
        }
    }
}
public class CallerHandler 
{
    private MethodInfo m;
    private object target;

    public long Due { get; set; }

    public CallerHandler(object target, MethodInfo m)
    {
        this.target = target;
        this.m = m;
        DueAfterAttribute attr = m.GetCustomAttribute<DueAfterAttribute>();
        Due = attr != null ? attr.Due : long.MaxValue;
    }

    internal void Call(long diff)
    {
        m.Invoke(target, new object[] { diff });
    }
}

public class DueAfterAttribute : Attribute 
{
    public long Due { get; set; }
    public DueAfterAttribute(long due)
    {
        this.Due = due;
    }
}