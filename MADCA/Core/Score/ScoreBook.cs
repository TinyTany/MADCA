﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MADCA.Core.Score
{
    public sealed class ScoreBook
    {
        private readonly List<Score> scores;

        public ScoreBook()
        {
            scores = new List<Score>();
        }

        public bool AddScoreLast(Score score)
        {
            if (scores.Contains(score)) { return false; }
            scores.Add(score);
            return true;
        }

        public bool RemoveScore(Score score)
        {
            return scores.Remove(score);
        }
    }
}
