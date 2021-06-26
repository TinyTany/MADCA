using MADCA.Utility;
using System;

namespace MADCA.Core.Data
{
    /// <summary>
    /// ノーツの時間位置を表現するクラス
    /// 実際はただの分数を表現するクラス
    /// </summary>
    public sealed class TimingPosition : IEquatable<TimingPosition>, IComparable<TimingPosition>
    {
        /// <summary>
        /// 小節の分割数
        /// </summary>
        public uint DivValue { get; private set; }

        /// <summary>
        /// divValueによる分割単位の個数
        /// [0, divValue)
        /// </summary>
        public int CntValue { get; private set; }

        /// <summary>
        /// 小数で表現した絶対位置
        /// </summary>
        public double BarRatio
        {
            get
            {
                if (DivValue == 0)
                {
                    // いちいち無限大とかを表現する必要もないので分母が0なら非数ということで...
                    System.Diagnostics.Debug.Assert(false, "分母が0です");
                    return double.NaN;
                }
                return CntValue / (double)DivValue;
            }
        }

        /// <summary>
        /// 有効な値かを調べる（分母が0でないか）
        /// </summary>
        public bool IsValid => DivValue != 0;

        public TimingPosition(uint div, int cnt)
        {
            DivValue = div;
            CntValue = cnt;
            Normalize();
            // NOTE:TODO: バリデーションチェックはどうしようか（そもそも必要か？）
        }

        public TimingPosition(TimingPosition other)
            : this(other.DivValue, other.CntValue) { }

        /// <summary>
        /// 通分
        /// </summary>
        private void Normalize()
        {
            var gcd = Utility.MyMath.Gcd(DivValue, CntValue.ToUInt());
            if (gcd == 0) { return; }
            DivValue /= gcd;
            CntValue /= (int)gcd;
        }

        #region 加減算オペレーターオーバーロード
        public static TimingPosition operator+(TimingPosition lhs, TimingPosition rhs)
        {
            if (!lhs.IsValid || !rhs.IsValid)
            {
                // どっちかがInvalidだったら計算結果はInvalidなものが返る
                return new TimingPosition(0, 0);
            }
            var newDiv = MyMath.Lcm((int)lhs.DivValue, (int)rhs.DivValue);
            var newCnt = (int)(newDiv / lhs.DivValue * lhs.CntValue + newDiv / rhs.DivValue * rhs.CntValue);
            return new TimingPosition(newDiv.ToUInt(), newCnt);
        }

        public static TimingPosition operator-(TimingPosition lhs, TimingPosition rhs)
        {
            return (lhs + new TimingPosition(rhs.DivValue, -rhs.CntValue));
        }

        public static TimingPosition operator*(TimingPosition lhs, TimingPosition rhs)
        {
            var newDiv = lhs.DivValue * rhs.DivValue;
            var newCnt = lhs.CntValue * rhs.CntValue;
            return new TimingPosition(newDiv, newCnt);
        }

        public static TimingPosition operator/(TimingPosition lhs, TimingPosition rhs)
        {
            var cnt = rhs.CntValue < 0 ? rhs.DivValue * -1 : rhs.DivValue;
            return lhs * new TimingPosition(rhs.CntValue.ToUInt(), (int)cnt);
        }
        #endregion

        #region IEquatable, IComparable実装と比較演算子オーバーロード
        public override bool Equals(object obj)
        {
            if (obj is null || GetType() != obj.GetType())
            {
                return false;
            }
            var other = (TimingPosition)obj;
            return Equals(other);
        }

        public bool Equals(TimingPosition other)
        {
            return CntValue * other.DivValue == other.CntValue * DivValue;
        }

        public override int GetHashCode()
        {
            var hashCode = -845787673;
            hashCode = hashCode * -1521134295 + BarRatio.GetHashCode();
            return hashCode;
        }

        public int CompareTo(TimingPosition other)
        {
            if (other.BarRatio < BarRatio) { return 1; }
            if (other.BarRatio > BarRatio) { return -1; }
            return 0;
        }

        public static bool operator ==(TimingPosition left, TimingPosition right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(TimingPosition left, TimingPosition right)
        {
            return !(left == right);
        }

        public static bool operator <(TimingPosition left, TimingPosition right)
        {
            return left is null ? right is object : left.CompareTo(right) < 0;
        }

        public static bool operator <=(TimingPosition left, TimingPosition right)
        {
            return left is null || left.CompareTo(right) <= 0;
        }

        public static bool operator >(TimingPosition left, TimingPosition right)
        {
            return left is object && left.CompareTo(right) > 0;
        }

        public static bool operator >=(TimingPosition left, TimingPosition right)
        {
            return left is null ? right is null : left.CompareTo(right) >= 0;
        }
        #endregion
    }
}
