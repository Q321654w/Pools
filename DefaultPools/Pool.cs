using System.Collections.Generic;
using Collections;
using Collections.Defaults;
using Collections.Implementations;

namespace Pools.DefaultPools
{
    public class Pool<T> where T : IPoolObject
    {
        private readonly ISpecifyCollection<T> _inactiveObjects;

        public Pool() : this(new DefaultQueue<T>(new Queue<T>(), new DefaultFrameworkValue<T>()))
        {
        }

        public Pool(ISpecifyCollection<T> inactiveObjects)
        {
            _inactiveObjects = inactiveObjects;
        }

        public void AddToPool(T obj)
        {
            _inactiveObjects.With(obj);
            obj.Disable();
        }

        public void ReturnToPool(T obj)
        {
            _inactiveObjects.With(obj);
            obj.Disable();
        }

        public bool HasInactiveObjects()
        {
            return _inactiveObjects.Find().Success;
        }

        public T GetInactiveObject()
        {
            var inactiveObject = _inactiveObjects.Find().Content;
            inactiveObject.Enable();

            return inactiveObject;
        }
    }
}