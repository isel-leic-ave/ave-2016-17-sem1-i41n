using System;
using System.Reflection;

[assembly: Red ]
[module: Green ]
public class App 
{
    static void Main() {
        Attribute[] atts = (Attribute[]) Assembly
            .LoadFrom("Demo02.exe")
            .GetCustomAttributes(typeof(RedAttribute));
        Console.WriteLine(atts.Length);
    }
}

[AttributeUsage(AttributeTargets.Assembly)]
public sealed class RedAttribute : System.Attribute { }

[AttributeUsage(AttributeTargets.Module)]
public sealed class GreenAttribute : System.Attribute { }

[AttributeUsage(AttributeTargets.Class)]
public sealed class BlueAttribute : System.Attribute { }
public sealed class YellowAttribute : System.Attribute { }
public sealed class CyanAttribute : System.Attribute { }
public sealed class MagentaAttribute : System.Attribute { }
public sealed class BlackAttribute : System.Attribute { }

[type: Blue ]
[ Yellow ]
public sealed class Widget
{
  [return: Cyan ]
  [ Black ]
  public int Splat([Magenta] int x ) {return 0;}
}

