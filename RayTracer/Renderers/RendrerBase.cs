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
using System.Drawing;


namespace RayTracer.Renderers
{
    public class RendrerBase :IRenderer
    {

        private IImageSampler   m_imageSampler;

        public RendrerBase(IImageSampler imageSampler)
        {
            m_imageSampler = imageSampler;
        }


        #region IRenderer Members

        public void startRendering(Scene scene, Display display)
        {
            scene.prepareForRender();

            m_imageSampler.setScene(scene);

            m_imageSampler.startTracing(display);
        }

        #endregion
    }
}
