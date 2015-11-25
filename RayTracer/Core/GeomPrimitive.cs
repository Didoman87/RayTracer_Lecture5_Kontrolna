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
	/// Description of GeomPrimitive.
	/// </summary>
    [Serializable]
	public abstract class GeomPrimitive {
		
		public Shader shader;
		
		public GeomPrimitive() {
			shader = null;
		}
		
		public abstract bool intersect( Ray ray, IntersectionData hitData );
		
		public void setShader(Shader _shader) {
			shader = _shader;
		}
		
		public Shader getShader() {
			return shader;
		}

        public abstract BoundingBox GetBoundingBox();
	}
}
