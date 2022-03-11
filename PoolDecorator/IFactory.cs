namespace Pools.PoolDecorator
{
    public interface IFactory<out T>
    {
        T Object();
    }
}