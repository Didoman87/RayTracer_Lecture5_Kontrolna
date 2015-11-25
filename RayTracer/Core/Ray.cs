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
	/// Description of Ray.
	/// </summary>
	public class Ray {
		
		public Vector3 p; //start point
		public Vector3 dir; //direction
		
		public double mint;
		public double maxt;
		
		public Ray() {
			p = new Vector3(0, 0, 0);
			dir = new Vector3(0, 0, 1);
			mint = 0.0;
			maxt = double.MaxValue;
		}
		
		public Ray(Ray r) {
			p = new Vector3(r.p);
			dir = new Vector3(r.dir);
			mint = r.mint;
			maxt = r.maxt;
		}
		
	}
}
