namespace Pool.PoolDecorator
{
    public abstract class ObjectProviderDecorator<T> : IObjectProvider<T>
    {
        protected IObjectProvider<T> Provider;

        public ObjectProviderDecorator(IObjectProvider<T> provider)
        {
            Provider = provider;
        }

        public abstract T GetInactiveObject();
    }
}