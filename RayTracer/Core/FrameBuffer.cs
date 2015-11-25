/************************************************************
	Copyright (C) 2006-2013 by Hristo Lesev
	hristo.lesev@diadraw.com
	for educational purposes only, not for commercial use
************************************************************/
using System;
using System.Collections;
using System.Drawing;
using RayTracer.Math;
using System.Collections.Generic;

namespace RayTracer.Core
{

    public class ImageSample
    {
        public double x;
        public double y;
        public Color3 color;
    }

    public struct Pixel
    {
        public int x;
        public int y;

        private List<ImageSample> m_samples;

        public void addSample(ImageSample sample)
        {
            if (null == m_samples)
                m_samples = new List<ImageSample>();

            m_samples.Add(sample);
        }

        public Color3 getColor()
        {
            Color3 sumCol = new Color3();
           
            if (null == m_samples)
                return sumCol;

            if (m_samples.Count<1)
                return sumCol;

            foreach (ImageSample sample in m_samples)
            {
                sumCol += sample.color;
            }

            sumCol *= 1.0 / (double)m_samples.Count;

            //if (Double.IsNaN(sumCol.getIntensity()))
            //    return new Color3();

            return sumCol;
        }
    }

	/// <summary>
	/// Description of FrameBuffer.
	/// </summary>
	public class FrameBuffer {
		
		private int width;
		private int height;
		private Color3[] colorBuffer;
		private int fromY;

        private Pixel[] pixelBuffer;
		
		public FrameBuffer(int _width, int _height) {
			width = _width;
			height = _height;
			
			fromY = 0;
			
			colorBuffer = new Color3[width*height];
            pixelBuffer = new Pixel[width * height];
			
			clearColorBuffer();
		}

        public void addSample(int x, int y, ImageSample sample)
        {
            int offset = y * width + x;
            pixelBuffer[offset].addSample(sample);
        }

		public void toBitmap(Bitmap bitmap) {

            colorBuffer = new Color3[width * height];

            int colBuffCounter = 0;
            foreach (Pixel pixel in pixelBuffer)
            {
                colorBuffer[colBuffCounter++] = pixel.getColor();
            }

			fromY = 0;
			toBitmap(height, bitmap);
		}
		
		public void toBitmap(int toY, Bitmap bitmap) {
			
			for(int y=fromY; y<toY; ++y)
				for(int x=0; x<width; ++x) {
					int offset = y*width + x;
					bitmap.SetPixel(x, y, 
				                Color.FromArgb(255,
				                               tonemapComponentExp(colorBuffer[offset].r),
				                               tonemapComponentExp(colorBuffer[offset].g),
				                               tonemapComponentExp(colorBuffer[offset].b)
				                              )
					               );
				}
			fromY = toY;
		}
		
		private double clamp(double x, double min, double max) {
		    if (x > max)
		        return max;
		    if (x > min)
		        return x;
		    return min;
		}
		
		private double expose(double light, double exposure) {
			return 1.0 - System.Math.Exp(-light * exposure);
		}
		
		private int tonemapComponentExp( double v ) {
			int ig = (int) (expose(v, 1.0) * 255.0 + 0.5); 
			ig = (int)clamp(ig, 0, 255);
			return ig;
		}
		
		private void clearColorBuffer() 
        {
		}
		
	}
}
