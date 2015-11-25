/************************************************************
	Copyright (C) 2006-2013 by Hristo Lesev
	hristo.lesev@diadraw.com
	for educational purposes only, not for commercial use
************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RayTracer.Math;
using RayTracer.Core;

namespace RayTracer.Textures {

	public class TextureChecker 
	{
		public int m_numU;
		public int m_numV;

		private Color3 evalChecker(double u, double v) 
		{
            return new Color3();
		}

        public Color3 evalColor(RayContext rayContext)
        {
			return evalChecker(rayContext.hitData.textureUVW.x, rayContext.hitData.textureUVW.y);
        }
	}

}
