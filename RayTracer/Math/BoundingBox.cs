using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RayTracer.Math
{
    public struct BoundingBox
    {
        public Vector3 Min;// { get; set; }
        public Vector3 Max;// { get; set; }

        public BoundingBox(Vector3 min, Vector3 max)
            : this()
        {
            Min = min;
            Max = max;
        }

        public double this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0: { return Min.x; }
                    case 1: { return Min.y; }
                    case 2: { return Min.z; }
                    case 3: { return Max.x; }
                    case 4: { return Max.y; }
                    case 5: { return Max.z; }
                    default: throw new ArgumentException("try to access index outside [0..5]", "index");
                }
            }
            set
            {
                switch (index)
                {
                    case 0: { Min.x = value; break; }
                    case 1: { Min.y = value; break; }
                    case 2: { Min.z = value; break; }
                    case 3: { Max.x = value; break; }
                    case 4: { Max.y = value; break; }
                    case 5: { Max.z = value; break; }

                    default: throw new ArgumentException("try to access index outside [0..5]", "index");
                }
            }
        }

        public override string ToString()
        {
            return string.Format("Min: ({0}, {1}, {2})\nMax: ({3}, {4}, {5})\n", Min.x, Min.y, Min.z, Max.x, Max.y, Max.z);
        }
    }
}
