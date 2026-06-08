using LivePortfolio.Core.Models;


namespace LivePortfolio.Core.Services
{
    public interface IScoreCalculationService
    {
        double? GetFinalGameScore(IReadOnlyCollection<ReviewScoreInput> reviewScoreInputs);
    }
}
