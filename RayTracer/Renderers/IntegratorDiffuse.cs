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

namespace RayTracer.Renderers
{
    public class IntegratorDiffuse : IIntegrator
    {

        #region IIntegrator Members

        public Color3 illuminate(RayContext rayContext, BRDF brdf)
        {
            return new Color3();
        }

        #endregion
    }
}
