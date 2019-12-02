using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace MADCA.Core.MaterialBox
{
    public abstract class RectangleBox<T> where T : class
    {
        public T Instance { get; protected set; }
        public RectangleF Region { get; protected set; }

        protected RectangleBox() { }

        protected RectangleBox(T instance, RectangleF region)
        {
            Instance = instance;
            Region = region;
        }
    }
}
