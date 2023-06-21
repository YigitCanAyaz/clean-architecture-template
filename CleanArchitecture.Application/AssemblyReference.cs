using System.Reflection;

namespace CleanArchitecture.Application;

public static class AssemblyReference
{
    public static readonly Assembly assembly = typeof(Assembly).Assembly;
}
