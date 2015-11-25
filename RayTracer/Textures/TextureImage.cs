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
using System.Drawing;
using RayTracer.Core;

namespace RayTracer.Textures {
	
    public class TextureImage : Texture
	{
		private ImageBitmap m_imageBitmap;

		public void loadImage(string fileName) 
		{
			Bitmap bmp = new Bitmap(fileName);

			
			double constTo01 = 0.00392156862;

			m_imageBitmap = new ImageBitmap(bmp.Width, bmp.Height);

			for( int x=0; x<bmp.Width; ++x )
				for (int y = 0; y < bmp.Height; ++y) {
					Color c = bmp.GetPixel(x, y);
					m_imageBitmap.setColor(x, y, new Color3(c.R * constTo01, c.G * constTo01, c.B * constTo01));
				}
			
		}

		private Color3 evalColorBilinear(double x, double y) {
				x = MathUtils.frac(x);
				y = MathUtils.frac(y);
				double dx = x * (m_imageBitmap.Width - 1);
				double dy = y * (m_imageBitmap.Height - 1);
				int ix0 = (int)dx;
				int iy0 = (int)dy;
				int ix1 = (ix0 + 1) % m_imageBitmap.Width;
				int iy1 = (iy0 + 1) % m_imageBitmap.Height;
			
				double u = dx - ix0;
				double v = dy - iy0;
				u = u * u * (3.0f - (2.0f * u));
				v = v * v * (3.0f - (2.0f * v));
				double k00 = (1.0f - u) * (1.0f - v);

				Color3 c00 = m_imageBitmap.getColor(ix0, iy0);
				double k01 = (1.0f - u) * v;
				Color3 c01 = m_imageBitmap.getColor(ix0, iy1);
				double k10 = u * (1.0f - v);
				Color3 c10 = m_imageBitmap.getColor(ix1, iy0);
				double k11 = u * v;
				Color3 c11 = m_imageBitmap.getColor(ix1, iy1);

				Color3 c = k00 * c00;
				c += k01 * c01;
				c += k10 * c10;
				c += k11 * c11;

				return c;
		}


		public int getWidth() {
			return m_imageBitmap.Width;
		}

		public int getHeight() {
			return m_imageBitmap.Height;
		}

		

        public Color3 evalColor(RayContext rayContext)
        {
			return evalColorBilinear(rayContext.hitData.textureUVW.x, rayContext.hitData.textureUVW.y);
        }
    }
}
