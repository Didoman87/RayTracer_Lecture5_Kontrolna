/************************************************************
	Copyright (C) 2006-2013 by Hristo Lesev
	hristo.lesev@diadraw.com
	for educational purposes only, not for commercial use
************************************************************/
using System;

namespace RayTracer.Core
{
	/// <summary>
	/// Description of Shader.
	/// </summary>
	public abstract class Shader : RenderObject {
		
		public Shader() {
		}
		
		public abstract void shade(RayContext rayContext);


		public virtual void frameBegin() {}

		public virtual void frameEnd() {}
	}
}
