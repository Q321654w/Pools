using System.Collections.Generic;

namespace Pools.PoolDecorator
{
    public class Pool<T> : FactoryDecorator<T> where T : IPoolObject
    {
        private readonly Queue<T> _inactiveObjects;
        private readonly List<T> _activeObjects;

        public Pool(IFactory<T> provider) : this(provider, new Queue<T>(), new List<T>())
        {
        }

        public Pool(IFactory<T> provider, Queue<T> inactiveObjects, List<T> activeObjects) :
            base(provider)
        {
            _inactiveObjects = inactiveObjects;
            _activeObjects = activeObjects;
        }

        public override T Object()
        {
            var inactiveObject = HasInactiveObjects() ? _inactiveObjects.Dequeue() : InactiveObject();

            inactiveObject.Enable();

            return inactiveObject;
        }

        private bool HasInactiveObjects()
        {
            return _inactiveObjects.Count > 0;
        }

        private T InactiveObject()
        {
            RecalculateInactiveObjects();

            return HasInactiveObjects() ? _inactiveObjects.Dequeue() : Provider.Object();
        }

        private void RecalculateInactiveObjects()
        {
            var lastIndex = _activeObjects.Count - 1;
            var lastObject = _activeObjects[lastIndex];

            for (var i = lastIndex - 1; i >= 0; i--)
            {
                var currentObject = _activeObjects[i];

                if (currentObject.Active())
                    continue;

                _activeObjects[i] = lastObject;
                _activeObjects.RemoveAt(lastIndex);

                _inactiveObjects.Enqueue(currentObject);

                lastIndex = _activeObjects.Count - 1;
                lastObject = _activeObjects[lastIndex];
            }
        }
    }
}