namespace Pool.PoolDecorator
{
    public class ObjectFactory : IObjectProvider<IPoolObject>
    {
        public IPoolObject GetInactiveObject()
        {
            return new EmptyObject();
        }
    }
}