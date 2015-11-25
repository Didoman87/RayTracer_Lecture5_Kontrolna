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

namespace RayTracer.Core {

	public class ImageBitmap {

		public ImageBitmap(int w, int h) {

			m_width = w;
			m_height = h;
			m_color = new Color3 [w*h];
		}

		public int Width {
			get { return m_width; }
		}

		public int Height {
			get { return m_height; }
		}

		public Color3 getColor(int x, int y) {
			return m_color[x + y * m_width];
		}

		public void setColor(int x, int y, Color3 col) {
			lock (thisLock) {
				m_color[x + y * m_width] = col;
			}
		}

		private int m_width;
		private int m_height;
		private static Object thisLock = new Object();

		private Color3 [] m_color;
	}

}
