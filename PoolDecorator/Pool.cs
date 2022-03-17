using System.Collections.Generic;
using Collections;
using Collections.Defaults;
using Collections.Implementations;
using Collections.Predicates.Common;

namespace Pools.PoolDecorator
{
    public class Pool<T> : FactoryDecorator<T> where T : IPoolObject, IEqualsWithParameter<T>
    {
        private readonly ISpecifyCollection<T> _inactiveObjects;
        private readonly ICustomCollection<T> _activeObjects;

        public Pool(IFactory<T> provider) : this(provider,
            new DefaultQueue<T>(new Queue<T>(), new DefaultFrameworkValue<T>()),
            new DefaultList<T>(new List<T>(), new DefaultFrameworkValue<T>()))
        {
        }

        public Pool(IFactory<T> provider, ISpecifyCollection<T> inactiveObjects, ICustomCollection<T> activeObjects) :
            base(provider)
        {
            _inactiveObjects = inactiveObjects;
            _activeObjects = activeObjects;
        }

        public override T Object()
        {
            var inactiveObject = HasInactiveObjects() ? _inactiveObjects.Find().Content : InactiveObject();

            inactiveObject.Enable();

            return inactiveObject;
        }

        private bool HasInactiveObjects()
        {
            return _inactiveObjects.Find().Success;
        }

        private T InactiveObject()
        {
            RecalculateInactiveObjects();

            return HasInactiveObjects() ? _inactiveObjects.Find().Content : Provider.Object();
        }

        private void RecalculateInactiveObjects()
        {
            var lastIndex = _activeObjects.Count() - 1;

            for (var i = lastIndex; i >= 0; i--)
            {
                var currentObject = _activeObjects.Element(i);

                if (currentObject.Active())
                    continue;

                _activeObjects.WithOut(currentObject);
                _inactiveObjects.With(currentObject);
            }
        }
    }
}