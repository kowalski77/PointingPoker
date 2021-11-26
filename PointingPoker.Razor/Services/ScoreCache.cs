using Microsoft.Extensions.Caching.Memory;
using PointingPoker.Razor.ViewModels;

namespace PointingPoker.Razor.Services;

public class ScoreCache : IScoreCache
{
    private readonly MemoryCache cache = new(new MemoryCacheOptions {SizeLimit = 1024});

    public void Add(string key, IEnumerable<ScoreViewModel> scores)
    {
        this.cache.Set(key, scores, new MemoryCacheEntryOptions {Size = 1});
    }

    public IEnumerable<ScoreViewModel> Get(string key)
    {
        return this.cache.Get<IEnumerable<ScoreViewModel>>(key);
    }

    public void Update(string key, IEnumerable<ScoreViewModel> scores)
    {
        this.cache.Remove(key);
        this.cache.Set(key, scores, new MemoryCacheEntryOptions {Size = 1});
    }

    public void Remove(string key)
    {
        this.cache.Remove(key);
    }
}
