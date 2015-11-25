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
using System.Windows.Forms;

namespace RayTracer.Core
{
    
    public interface IImageSampler
    {
        void startTracing(Display display);

        void setScene(Scene scene);
    }
    
    public interface IPixelSampler
    {
        Color3 samplePixel(int pixX, int pixY, Rectangle region, Camera camera);

        void setParams(Rectangle imageRect, Scene scene);
    }

    public interface IIntegrator
    {
        Color3 illuminate(RayContext rayContext, BRDF brdf);
    }

    public interface IRenderer
    {
        void startRendering(Scene scene, Display display);
    }

	interface IBucketOrder 
	{
		void getBucketSequence(int w, int h, int nbw, int nbh, out Rectangle[] buckets);
	}

}
