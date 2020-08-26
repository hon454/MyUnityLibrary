using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MyUnityLibrary.Utilities
{
    public static class ReflectionUtility
    {
        public static IEnumerable<TBaseType> GetEnumerableOfSubTypes<TBaseType>(params object[] constructorArgs) where TBaseType : class
        {
            return GetSubTypes<TBaseType>()
                .Select(subType => (TBaseType) Activator.CreateInstance(subType, constructorArgs))
                .ToList();
        }

        public static IEnumerable<Type> GetSubTypes<TBaseType>() where TBaseType : class
        {
            var assembly = Assembly.GetAssembly(typeof(TBaseType));
            var subTypes = assembly.GetTypes()
                .Where(t => t.IsSubclassOf(typeof(TBaseType)) && !t.IsAbstract && t.IsClass);
            return subTypes;
        }
    }
}
