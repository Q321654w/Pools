namespace Pool.PoolDecorator
{
    public interface IObjectProvider<T>
    {
        T GetInactiveObject();
    }
}