/************************************************************
	Copyright (C) 2006-2013 by Hristo Lesev
	hristo.lesev@diadraw.com
	for educational purposes only, not for commercial use
************************************************************/
using System;
using RayTracer.Core;
using RayTracer.Math;

namespace RayTracer.Primitives
{
	/// <summary>
	/// Description of Triangle.
	/// </summary>
	public class Triangle : GeomPrimitive {
		
		public Vector3[] v;

		public Vector3[] tc;

		public Vector3[] vn; 

		public int shaderID;

		public Triangle() 
		{
			v = new Vector3[3];
			tc = new Vector3[3];
			vn = new Vector3[3];
			shaderID = 0;
		}

        public override BoundingBox GetBoundingBox()
        {
            var minX = v[0].x < v[1].x ? v[0].x : v[1].x;
            minX = minX < v[2].x ? minX : v[2].x;

            var minY = v[0].y < v[1].y ? v[0].y : v[1].y;
            minY = minY < v[2].y ? minY : v[2].y;

            var minZ = v[0].z < v[1].z ? v[0].z : v[1].z;
            minZ = minZ < v[2].z ? minZ : v[2].z;
            //--------------------------------------------
            var maxX = v[0].x > v[1].x ? v[0].x : v[1].x;
            maxX = maxX > v[2].x ? maxX : v[2].x;

            var maxY = v[0].y > v[1].y ? v[0].y : v[1].y;
            maxY = maxY > v[2].y ? maxY : v[2].y;

            var maxZ = v[0].z > v[1].z ? v[0].z : v[1].z;
            maxZ = maxZ > v[2].z ? maxZ : v[2].z;

            if ((minX - maxX) < 0.001)
            {
                minX -= 0.1;
                maxX += 0.1;
            }

            if ((minY - maxY) < 0.001)
            {
                minY -= 0.1;
                maxY += 0.1;
            }

            if ((minZ - maxZ) < 0.001)
            {
                minZ -= 0.1;
                maxZ += 0.1;
            }

            var min = new Vector3(minX, minY, minZ);
            var max = new Vector3(maxX, maxY, maxZ);

            return new BoundingBox(min, max);
        }
		
		public override bool intersect( Ray ray, IntersectionData hitData ) 
		{
            Vector3 e1, e2;
            e1 = v[1] - v[0];// e1 = edge v0 to v1
            e2 = v[2] - v[0];// e2 = edge v0 to v2
            Vector3 pvec = ray.dir ^ e2;// pvec = cross(dir,e2)
            double det = e1 * pvec;// determinant = dot(e1,pvec)
            if (det < 1e-6f && det > -1e-6f) return false; // if det close to zero -> ray lies in the plane
            double inv_det = 1.0f / det;// inverse determinant
            Vector3 tvec = ray.p - v[0];// distance v0 to ray origin
            double u = inv_det * (tvec * pvec);// u = dot(tvec,pvec) / det
            if (u < -1e-6f || u > (1.0f + 1e-6f)) return false;// if u outside triangle return
            Vector3 qvec = tvec ^ e1;// qvec = cross(tvec,e1)
            double t = inv_det * (e2 * qvec);// t = dot(e2,qvec) / det
            if (t < ray.mint || t > ray.maxt) return false;// return false if t is outside range
            double uvV = inv_det * (ray.dir * qvec);// v = dot(dir,qvec) / det
            if (uvV < -1e-6f || (u + uvV) > (1.0f + 1e-6f)) return false;// if v outside triangle return
            hitData.hitT = t;
            hitData.hitNormal = e1 ^ e2;
            hitData.hitNormal.normalize();
            hitData.textureUVW = ((1 - u - uvV) * tc[0]) + (u * tc[1]) + (uvV * tc[2]);
            hitData.shaderID = shaderID;
            return true;
		}
		
	}
}
