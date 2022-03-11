using System.Collections.Generic;

namespace Pools.DefaultPools
{
    public class Pool<T> where T : IPoolObject
    {
        private readonly Queue<T> _inactiveObjects = new Queue<T>();

        public void AddToPool(T obj)
        {
            _inactiveObjects.Enqueue(obj);
            obj.Disable();
        }

        public void ReturnToPool(T obj)
        {
            _inactiveObjects.Enqueue(obj);
            obj.Disable();
        }

        public bool HasInactiveObjects()
        {
            return _inactiveObjects.Count > 0;
        }

        public T GetInactiveObject()
        {
            var inactiveObject = _inactiveObjects.Dequeue();
            inactiveObject.Enable();

            return inactiveObject;
        }
    }
}