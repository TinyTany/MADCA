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
    public sealed class ScoreBox : RectangleBox<Score.Score>
    {
        private ScoreBox() { }

        public ScoreBox(Score.Score score, RectangleF region, Func<Score.Score, RectangleF> regionSetter)
            : base(score, region) 
        {
            score.Changed += (s) =>
            {
                Region = regionSetter(s);
            };
        }
    }
}
