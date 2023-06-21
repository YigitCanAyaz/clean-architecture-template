using System.Reflection;

namespace CleanArchitecture.Persistence;

public static class AssemblyReference
{
    public static readonly Assembly assembly = typeof(AssemblyReference).Assembly;
}
