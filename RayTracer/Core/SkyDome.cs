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
using RayTracer.Textures;

namespace RayTracer.Core {
	public class SkyDome 
	{
		private TextureImage m_skyTexture;

		public SkyDome(string imagePath) {
			m_skyTexture = new TextureImage();
			m_skyTexture.loadImage(imagePath);
		}

		public Color3 getRadiance(RayContext rayContext)
		{
			Vector3 d = new Vector3();
			d.set( rayContext.ray.dir );
	
			double len = System.Math.Sqrt(d.x*d.x + d.y*d.y);
			double r = (len<0.0001) ? 0.0f : System.Math.Acos(-d.z)/(2.0f*System.Math.PI*len);
			double u = r*d.x + 0.5f;
			double v = 0.5f - r*d.y;
			
			int w = m_skyTexture.getWidth(), h = m_skyTexture.getHeight();

			double x = System.Math.Min(System.Math.Max(u, 0), 1);
			double y = System.Math.Min(System.Math.Max(v, 0), 1);

            Color3 c = new Color3();//m_skyTexture.evalColor(x, y);

			//System.Diagnostics.Debug.WriteLine(x.ToString()+" "+y.ToString());
			return c;
		}
	}
}
