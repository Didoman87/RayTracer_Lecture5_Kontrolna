/************************************************************
	Copyright (C) 2006-2013 by Hristo Lesev
	hristo.lesev@diadraw.com
	for educational purposes only, not for commercial use
************************************************************/
using System;
using RayTracer.Core;
using RayTracer.Math;

namespace RayTracer.Cameras
{
	/// <summary>
	/// Description of Pinhole.
	/// </summary>
	public class Pinhole : Camera {
		
		public Vector3 forward;
		public Vector3 up;
		public Vector3 right;
		public double fov;
		public double aspect;
		
		public Pinhole() {
			forward = new Vector3();
			up = new Vector3();
			right = new Vector3();
			
			aspect = 1;
			fov = 45;
		}
		
		public override Ray getRay(double x, double y) {
			double au = System.Math.Tan(  0.0174532925*((fov*0.5)) );//
			double av = au / aspect;
			
			double du = -au + (2.0 * au * x);
			double dv = -av + (2.0 * av * (1-y));
	
			Ray ray = new Ray();
			ray.p.set(pos);
		
			Vector3 rayDir = new Vector3(du, dv, -1.0f);
			
			ray.dir.x = right.x*rayDir.x + up.x*rayDir.y + forward.x*rayDir.z;
			ray.dir.y = right.y*rayDir.x + up.y*rayDir.y + forward.y*rayDir.z;
			ray.dir.z = right.z*rayDir.x + up.z*rayDir.y + forward.z*rayDir.z;
			
			return ray;
		}
		
	}
}
