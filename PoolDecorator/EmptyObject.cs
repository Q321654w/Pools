namespace Pool.PoolDecorator
{
    public class EmptyObject : IPoolObject
    {
        public bool Active()
        {
            return false;
        }

        public void Enable()
        {
            
        }
    }
}