using LivePortfolio.Core.Models;

namespace LivePortfolio.Core.Services
{
    public class ScoreCalculationService : IScoreCalculationService
    {
        private const double BaseWeight = 0.25;
        private const double BaseTrustRating = 0.5;
        private const double WeightCoefficient = 10.0;

        public double? GetFinalGameScore(IReadOnlyCollection<ReviewScoreInput> reviewScoreInputs)
        {
            ArgumentNullException.ThrowIfNull(reviewScoreInputs);

            if (reviewScoreInputs.Count == 0)
            {
                return null;
            }

            double weightSum = 0;
            double weightedScoreSum = 0;

            foreach (var input in reviewScoreInputs)
            {
                ValidateInput(input);

                var weight = GetReviewScoreWeight(input);

                weightSum += weight;
                weightedScoreSum += input.ReviewScore * weight;
            }

            return weightedScoreSum / weightSum;
        }

        private static double GetReviewScoreWeight(ReviewScoreInput input)
        {
            var voterCount = input.VoterCount;
            var trustRating = input.TrustRating;

            var denominator = voterCount + WeightCoefficient;

            var defaultTrustPart = (WeightCoefficient / denominator) * Math.Pow(BaseTrustRating, 2);
            var actualTrustPart = (voterCount / denominator) * Math.Pow(trustRating, 2);

            return BaseWeight + defaultTrustPart + actualTrustPart;
        }

        private static void ValidateInput(ReviewScoreInput input)
        {
            ArgumentNullException.ThrowIfNull(input);

            if (input.VoterCount < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(input.VoterCount), "VoterCount cannot be negative.");
            }

            if (input.ReviewScore < 0 || input.ReviewScore > 100)
            {
                throw new ArgumentOutOfRangeException(nameof(input.ReviewScore), "ReviewScore must be between 0 and 100.");
            }

            if (input.TrustRating < 0 || input.TrustRating > 1)
            {
                throw new ArgumentOutOfRangeException(nameof(input.TrustRating), "TrustRating must be between 0 and 1.");
            }
        }
    }
}
