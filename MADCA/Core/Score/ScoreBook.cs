using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MADCA.Core.Score
{
    public sealed class ScoreBook
    {
        private readonly List<Score> scores;

        public IReadOnlyList<IReadOnlyScore> Scores => scores;

        public ScoreBook()
        {
            scores = new List<Score>();
        }

        public bool AddScoreLast(Score score)
        {
            if (scores.Contains(score)) { return false; }
            if (!scores.Any())
            {
                score.TimingBegin = new Data.TimingPosition(1, 0);
            }
            else
            {
                var last = scores.Last();
                score.TimingBegin = last.TimingEnd;
            }
            scores.Add(score);
            return true;
        }

        public bool RemoveScore(Score score)
        {
            var index = scores.IndexOf(score);
            if (index < 0) { return false; }
            scores.Remove(score);
            foreach(var item in scores.Skip(index))
            {
                item.OnChanged();
                item.TimingBegin -= new Data.TimingPosition(score.BeatDen, (int)score.BeatNum);
            }
            return true;
        }
    }
}
