using System;
using System.Collections.Generic;

namespace MADCA.Utility
{
    public static class MyMath
    {
        public static uint Gcd(uint a, uint b)
        {
            if (b == 0) { return a; }
            return Gcd(b, a % b);
        }

        public static int Lcm(int a, int b)
        {
            return (int)(a / Gcd((uint)a, (uint)b) * b);
        }

        /// <summary>
        /// 正剰余を計算します（割られる数が負でもOK）
        /// </summary>
        /// <param name="p"></param>
        /// <param name="q"></param>
        /// <returns></returns>
        public static float PositiveMod(float p, float q)
        {
            return (float)(p - Math.Floor(p / q) * q);
        }

        /// <summary>
        /// 正剰余を計算します（割られる数が負でもOK）
        /// </summary>
        /// <param name="p"></param>
        /// <param name="q"></param>
        /// <returns></returns>
        public static int PositiveMod(int p, int q)
        {
            return (int)(p - Math.Floor(p / (double)q) * q);
        }

        public static double DegToRad(double deg)
        {
            return deg * Math.PI / 180;
        }

        public static double RadToDeg(double rad)
        {
            return rad * 180 / Math.PI;
        }
    }

    public static class Misc
    {
        public static int IndexOf<T>(this IReadOnlyList<T> list, T target)
            where T : class
        {
            int result = -1;
            foreach(var item in list)
            {
                ++result;
                if (ReferenceEquals(item, target)) { return result; }
            }
            return result;
        }
    }
}
