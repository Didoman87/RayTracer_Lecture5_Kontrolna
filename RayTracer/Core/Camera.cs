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
	/// Description of Camera.
	/// </summary>
	public abstract class Camera {
		
		public Vector3 pos;
		
		public Camera() {
			pos = new Vector3();
		}
		
		// x and y in [0,1]
		public abstract Ray getRay(double x, double y);
	}
}
