using MADCA.Core.Data;
using MADCA.Core.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using JsonObject = System.Collections.Generic.Dictionary<string, dynamic>;

namespace MADCA.Core.Score
{
    public sealed class ScoreBook : IExchangeable
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
                score.TimingBegin = new TimingPosition(1, 0);
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
                item.TimingBegin -= new TimingPosition(score.BeatDen, (int)score.BeatNum);
            }
            return true;
        }

        public TimingPosition Size => scores.Last().TimingEnd;

        public JsonObject Exchange()
        {
            return new JsonObject()
            {
                ["Scores"] = scores.Select(s => s.Exchange()).ToList()
            };
        }

        public void Exchange(JsonObject json)
        {
            scores.Clear();
            foreach(var s in json["Scores"])
            {
                var tmp = new Score(1, 1);
                tmp.Exchange(s);
                scores.Add(tmp);
            }
        }
    }
}
