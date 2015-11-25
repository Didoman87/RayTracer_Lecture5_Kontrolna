/************************************************************
	Copyright (C) 2006-2013 by Hristo Lesev
	hristo.lesev@diadraw.com
	for educational purposes only, not for commercial use
************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RayTracer.Core;
using RayTracer.Math;
using System.Drawing;

namespace RayTracer.Renderers
{
    public class PixelSamplerOne : IPixelSampler
    {

        public PixelSamplerOne(IIntegrator integrator)
        {
            m_integrator = integrator;
        }

        public void setParams( Rectangle imageRect, Scene scene)
        {
            m_imageRect = imageRect;
            m_scene = scene;
        }
 
        public Color3 samplePixel(int pixX, int pixY, Rectangle region, Camera camera)
        {
            
            double rayA = (double)(pixX + region.X) / (double)m_imageRect.Width;
            double rayB = (double)(pixY + region.Y) / (double)m_imageRect.Height;

            Ray ray = camera.getRay(rayA, rayB);

			RayContext rayContext = RayContext.startFromPixel(ray, pixX, pixY, m_scene, m_integrator);
            
            m_scene.shade(rayContext);

            return rayContext.resultColor;
        }

        //private
        private IIntegrator m_integrator;
        private Rectangle m_imageRect;
        private Scene m_scene;

    }

}
