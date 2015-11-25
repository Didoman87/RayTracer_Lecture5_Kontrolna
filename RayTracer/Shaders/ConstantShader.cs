/************************************************************
	Copyright (C) 2006-2013 by Hristo Lesev
	hristo.lesev@diadraw.com
	for educational purposes only, not for commercial use
************************************************************/
using System;
using RayTracer.Math;
using RayTracer.Core;
using RayTracer.BRDFs;

namespace RayTracer.Shaders
{
	/// <summary>
	/// Description of ConstantShader.
	/// </summary>
	public class ConstantShader : Shader {
		
		public Color3 color;
		
		public ConstantShader() 
		{
			//Default Implementation
			color = new Color3(0.8, 0.3, 0);
		}
		
		public override void shade(RayContext rayContext) 
		{
            rayContext.resultColor = color;
		}
		
	}
}
