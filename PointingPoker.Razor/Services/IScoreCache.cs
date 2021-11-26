using PointingPoker.Razor.ViewModels;

namespace PointingPoker.Razor.Services;

public interface IScoreCache
{
    void Add(string key, IEnumerable<ScoreViewModel> scores);
    
    IEnumerable<ScoreViewModel> All(string key);
    
    void Update(string key, IEnumerable<ScoreViewModel> scores);
    
    void Remove(string key);
}
