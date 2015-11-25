/************************************************************
	Copyright (C) 2006-2013 by Hristo Lesev
	hristo.lesev@diadraw.com
	for educational purposes only, not for commercial use
************************************************************/
using System;

namespace RayTracer.Core
{
	/// <summary>
	/// Description of Light.
	/// </summary>
	public abstract class Light {
		public Light() {
		}

        public abstract bool isSingular();
		public abstract LightSample getSample(RayContext rayContext, double ru, double rv);
	}
}
