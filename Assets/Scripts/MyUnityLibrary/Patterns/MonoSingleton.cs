using System;
using UnityEngine;

namespace MyUnityLibrary.Patterns
{
    /// <summary>
    /// 반드시 상속받은 개체에 sealed 키워드를 통해 상속을 금지하고, private 생성자를 선언하여 개체 생성을 금지해야 한다.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
    {
        private static T _instance;
        private static object _lock = new object();
        private static bool _isAlive = true;
        
        public static T Instance
        {
            get
            {
                if (!_isAlive)
                {
                    Debug.LogWarning("[Singleton] Instance '" + typeof(T) +
                                     "' already destroyed on application quit." +
                                     " Won't create again - returning null.");
                    return null;
                }
                
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        T[] objects = FindObjectsOfType<T>();

                        if (objects.Length > 0)
                        {
                            _instance = objects[0];
                        }

                        if (objects.Length > 1)
                        {
                            Debug.LogError("[Singleton] Something went really wrong " +
                                           " - there should never be more than 1 singleton!" +
                                           " Reopening the scene might fix it.");
                            for (int i = 1; i < objects.Length; ++i)
                            {
                                Destroy(objects[i].gameObject);
                            }
                            return _instance;
                        }
                        
                        if (_instance == null)
                        {
                            GameObject go = new GameObject();
                            go.name = typeof(T).Name;
                            go.hideFlags = HideFlags.DontSave;
                            _instance = go.AddComponent<T>();
                            
                            Debug.Log("[Singleton] An instance of " + typeof(T) +
                                      " is needed in the scene, so '" + go.name +
                                      "' was created with DontDestroyOnLoad.");
                        }
                        else
                        {
                            Debug.Log("[Singleton] Using instance already created: " + _instance.gameObject.name);
                        }
                        
                        DontDestroyOnLoad(_instance.gameObject);
                    }
                    return _instance;
                }
            }
        }

        protected MonoSingleton() {}

        protected void OnDestroy()
        {
            _isAlive = false;
        }

        protected void OnApplicationQuit()
        {
            _isAlive = false;
        }
    }
}