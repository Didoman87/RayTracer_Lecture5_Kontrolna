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
	/// Description of LightSample.
	/// </summary>
	public class LightSample {
		
		public Color3 color;
		public Ray shadowRay;
			
		public LightSample() {
			color = new Color3();
			shadowRay = new Ray();
		}
	}
}
