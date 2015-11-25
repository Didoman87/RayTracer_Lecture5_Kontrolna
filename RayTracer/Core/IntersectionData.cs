/************************************************************
	Copyright (C) 2006-2013 by Hristo Lesev
	hristo.lesev@diadraw.com
	for educational purposes only, not for commercial use
************************************************************/
using System;
using RayTracer.Math;

namespace RayTracer.Core
{
	/// <summary>
	/// Description of IntersectionData.
	/// </summary>
	public class IntersectionData {
		
		public double			hitT; //parameter along the ray
		public Vector3 			hitPos; //point in world coordinates
		public Vector3 			hitNormal;
		public Vector3 			hitUV;
		public GeomPrimitive 	hitPrimitive; //reference to the primitive
        public bool             hasIntersection;
		public Vector3			textureUVW;
		public int				shaderID;
		
		public IntersectionData() {
			hitT = double.MaxValue;
			hitPos = new Vector3();
			hitNormal = new Vector3();
			hitUV = new Vector3();
			hitPrimitive = null;
            hasIntersection = false;
			textureUVW = new Vector3();
			shaderID = 0;
		}
		
	}
}
