namespace Pools.PoolDecorator
{
    public abstract class FactoryDecorator<T> : IFactory<T>
    {
        protected readonly IFactory<T> Provider;

        public FactoryDecorator(IFactory<T> provider)
        {
            Provider = provider;
        }

        public abstract T Object();
    }
}