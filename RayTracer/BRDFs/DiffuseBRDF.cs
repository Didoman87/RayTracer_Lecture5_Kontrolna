/************************************************************
	Copyright (C) 2006-2013 by Hristo Lesev
	hristo.lesev@diadraw.com
	for educational purposes only, not for commercial use
************************************************************/
using System;
using RayTracer.Core;
using RayTracer.Math;

namespace RayTracer.BRDFs
{
	/// <summary>
	/// Description of DiffuseBRDF.
	/// </summary>
	public class DiffuseBRDF : BRDF {
		
		public DiffuseBRDF() {
		}     
        
        public override Color3 eval(RayContext rayContext, Vector3 wi, out double pdf) 
        {
            pdf = System.Math.Max( rayContext.hitData.hitNormal * wi, 0.0) * (1.0 / System.Math.PI);

			if (null != filterTexture)
				return filterTexture.evalColor(rayContext);

			return filterColor;
		}
        
        public override bool getSample(RayContext rayContext, double ru, double rv, out Vector3 wi, out double invPdf)
        {
            invPdf = 1;
            wi = new Vector3();
            return false;
        }

		public override bool isSingular() {
			return false;
		}

        public override BRDFTypes getType()
        {
            return BRDFTypes.Diffuse;
        }
		
	}
}
