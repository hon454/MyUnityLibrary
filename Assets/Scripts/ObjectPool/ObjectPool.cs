using System;

namespace MyLibrary
{
    public sealed class ObjectPool<T> where T : new()
    {
        private int _growth = 20;
        private T[] _pool;
        private int _nextObjectIndex = 0;
        
        public ObjectPool(int size)
        {
            Resize(size, false);
        }

        public ObjectPool(int size, int growSize)
        {
            _growth = growSize;
            Resize(size, false);
        }

        public int Capacity => _pool.Length;

        public int AllocatedCount => _nextObjectIndex;

        public T AllocateObject()
        {
            T item = default(T);

            if (_nextObjectIndex >= _pool.Length)
            {
                if (_growth > 0)
                {
                    Resize(_pool.Length + _growth, true);
                }
                else
                {
                    return item;
                }
            }

            if (0 <= _nextObjectIndex && _nextObjectIndex < _pool.Length)
            {
                item = _pool[_nextObjectIndex];
                _nextObjectIndex++;
            }

            return item;
        }

        public void Release(T instance)
        {
            if (_nextObjectIndex > 0)
            {
                _nextObjectIndex--;
                _pool[_nextObjectIndex] = instance;
            }
        }

        public void Reset()
        {
            int length = _growth;
            if (_pool != null)
            {
                length = _pool.Length;
            }

            Resize(length, false);

            _nextObjectIndex = 0;
        }

        public void Resize(int size, bool preserve)
        {
            lock(this)
            {
                int count = 0;

                T[] newPool = new T[size];

                if (_pool != null && preserve)
                {
                    count = _pool.Length;
                    Array.Copy(_pool, newPool, Math.Min(count, size));
                }

                for (int i = count; i < size; i++)
                {
                    newPool[i] = new T();
                }

                _pool = newPool;
            }
        }
    }
}

