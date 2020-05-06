using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
