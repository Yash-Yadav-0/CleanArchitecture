
namespace CleanArchitecture.Application.Interfaces.RedisCache
{
    public interface ICacheableQuery
    {
        public string CacheKey { get; }
        public double CacheTime { get; }
    }
}
