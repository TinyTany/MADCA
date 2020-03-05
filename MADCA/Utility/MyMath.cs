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
}
