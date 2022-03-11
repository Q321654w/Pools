namespace Pools.PoolDecorator
{
    public class ObjectFactory : IFactory<IPoolObject>
    {
        public IPoolObject Object()
        {
            return new EmptyObject();
        }
    }
}