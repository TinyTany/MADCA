using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MADCA.Core.Data
{
    public class TimingPosition : IEquatable<TimingPosition>
    {
        /// <summary>
        /// 小節番号
        /// 1スタート
        /// </summary>
        public int BarIndex { get; private set; }

        /// <summary>
        /// 小節の分割数
        /// </summary>
        public int DivValue { get; private set; }

        /// <summary>
        /// divValueによる分割単位の個数
        /// [0, divValue)
        /// </summary>
        public int CntValue { get; private set; }

        /// <summary>
        /// 小節上の位置の比率
        /// [0, 1)
        /// </summary>
        public double BarRatio
        {
            get
            {
                if (DivValue <= 0 || CntValue < 0)
                {
                    return 0;
                }
                return CntValue / (double)DivValue;
            }
        }

        public TimingPosition(int bar, int div, int cnt)
        {
            BarIndex = bar;
            DivValue = div;
            CntValue = cnt;
            // NOTE: バリデーションチェック
            {
                if (BarIndex <= 0) { BarIndex = 1; }
                if (DivValue <= 0) { DivValue = 1; }
                if (CntValue < 0) { CntValue = 0; }
                if (BarRatio < 0 || 1 <= BarRatio)
                {
                    CntValue = 0;
                    DivValue = 1;
                }
            }
        }

        #region IEquatable実装と演算子オーバーロード
        public override bool Equals(object obj)
        {
            if (obj is null || GetType() != obj.GetType())
            {
                return false;
            }
            var other = (TimingPosition)obj;
            return (BarIndex == other.BarIndex && BarRatio == other.BarRatio);
        }

        public bool Equals(TimingPosition other)
        {
            return BarIndex == other.BarIndex &&
                   BarRatio == other.BarRatio;
        }

        public override int GetHashCode()
        {
            var hashCode = -845787673;
            hashCode = hashCode * -1521134295 + BarIndex.GetHashCode();
            hashCode = hashCode * -1521134295 + BarRatio.GetHashCode();
            return hashCode;
        }

        public static bool operator ==(TimingPosition left, TimingPosition right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(TimingPosition left, TimingPosition right)
        {
            return !(left == right);
        }
        #endregion
    }
}
