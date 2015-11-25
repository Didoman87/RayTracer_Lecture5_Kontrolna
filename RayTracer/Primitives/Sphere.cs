/************************************************************
	Copyright (C) 2006-2013 by Hristo Lesev
	hristo.lesev@diadraw.com
	for educational purposes only, not for commercial use
************************************************************/
using System;
using RayTracer.Math;
using RayTracer.Core;

namespace RayTracer.Primitives
{
	/// <summary>
	/// Description of Sphere.
	/// </summary>
	public class Sphere : GeomPrimitive {
		
		public Vector3 center;
		public double radius;
		
		public Sphere() {
			center = new Vector3();
			radius = 1;
		}

        public override BoundingBox GetBoundingBox()
        {
            var min = new Vector3(center.x - radius, center.y - radius, center.z - radius);
            var max = new Vector3(center.x + radius, center.y + radius, center.z + radius);
            return new BoundingBox(min, max);
        }
		
		public override bool intersect( Ray ray, IntersectionData hitData ) 
        {
            
            
            Vector3 d = ray.dir;
            Vector3 p = ray.p;
            //лъчът преминава в локални координати на сферата
            p = p - center;

            double A = d * d;
            double B = 2 * (d * p);
            double C = (p * p) - (radius * radius);

            //връщаме лъча в коодинати на сцената
            p = p + center;

            double D = (B * B) - (4 * A * C);

            if (D < 0)
                return false;
            else
                D = System.Math.Sqrt(D);

            double t0 = (-B - D) / (2 * A);
            double t1 = (-B + D) / (2 * A);

            if (t1 < t0)
                t0 = t1;

            hitData.hitT = t0;
            Vector3 wp = p + d * t0;
            hitData.hitPos = wp;
            hitData.hitNormal = wp - center;
            hitData.hitNormal.normalize();

            double u = (System.Math.PI + System.Math.Atan2(wp.z - center.z, wp.x - center.x)) / (System.Math.PI * 2);
            double v = 1.0 - ((System.Math.PI / 2) + System.Math.Asin((wp.y - center.y) / radius)) / System.Math.PI;

            hitData.textureUVW = new Vector3(u, v, 0);

            return true;
         
		}
		
	}
}
