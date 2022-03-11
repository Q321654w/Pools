using UnityEngine;

namespace Pools.PoolDecorator
{
    public class EmptyObject : IPoolObject
    {
        public bool Active()
        {
            return false;
        }

        public void Enable()
        {
            Debug.Log("Enabled");
        }
    }
}