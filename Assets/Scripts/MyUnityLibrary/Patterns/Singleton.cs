using System;

namespace MyUnityLibrary.Patterns
{
    /// <summary>
    /// 반드시 상속받은 개체에 sealed 키워드를 통해 상속을 금지하고, private 생성자를 선언하여 개체 생성을 금지해야 한다.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class Singleton<T> where T : Singleton<T>
    {
        protected Singleton() {}

        public static T Instance { get; } = Create();
        
        private static T Create() 
        {
            Type t = typeof(T);
            var flags = System.Reflection.BindingFlags.Instance |
                        System.Reflection.BindingFlags.NonPublic;
            var constructor = t.GetConstructor(flags, null, Type.EmptyTypes, null);
            var instance = constructor?.Invoke(null);
            return instance as T;
        }
    }
}
