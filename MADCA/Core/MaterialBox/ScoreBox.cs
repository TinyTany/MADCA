using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MADCA.Core.Score;
using System.Drawing;

namespace MADCA.Core.MaterialBox
{
    /// <summary>
    /// 小節をGUIで操作・表示するためのクラス
    /// </summary>
    public sealed class ScoreBox
    {
        public Score.Score Score { get; private set; }
        public RectangleF Region { get; private set; }

        private ScoreBox() { }

        public ScoreBox(Score.Score score, RectangleF region)
        {
            Score = score;
            Region = region;
        }
    }
}
