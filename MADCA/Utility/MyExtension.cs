using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MADCA.Utility
{
    public static class MyExtension
    {
        public static T Find<T>(this IReadOnlyList<T> list, Predicate<T> p)
            where T : class
        {
            foreach (var item in list)
            {
                if (p.Invoke(item))
                {
                    return item;
                }
            }
            return null;
        }

        public static T FindLast<T>(this IReadOnlyList<T> list, Predicate<T> p)
            where T : class
        {
            foreach (var item in list.Reverse())
            {
                if (p.Invoke(item))
                {
                    return item;
                }
            }
            return null;
        }
    }
}
