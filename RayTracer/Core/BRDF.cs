/************************************************************
	Copyright (C) 2006-2013 by Hristo Lesev
	hristo.lesev@diadraw.com
	for educational purposes only, not for commercial use
************************************************************/
using System;
using RayTracer.Math;

namespace RayTracer.Core
{
    public enum BRDFTypes
    {
        Diffuse = 1,
        Specular = 2,
        Perfect = 4,
        Glossy = 8
    }

	/// <summary>
	/// Bidirectional reflection distribution function
	/// </summary>
	public abstract class BRDF {
		
		public Color3 filterColor;
		public Texture filterTexture;
		

		public BRDF() {
			filterColor = new Color3();
			filterTexture = null;
		}

        public virtual Color3 eval(RayContext rayContext, Vector3 wi, out double pdf)
        {
            pdf = 1.0;

			if (null != filterTexture)
				return filterTexture.evalColor(rayContext);

			return filterColor;
        }

		//return True if the sample ray is valid and False otherwise
        public abstract bool getSample(RayContext rayContext, double ru, double rv, out Vector3 wi, out double invPdf);

        public abstract BRDFTypes getType();

		public abstract bool isSingular();

        public bool matchTypes( BRDFTypes types )
        {
            BRDFTypes brdfType = this.getType();
            return (brdfType & types) == types; //this is exact match
			//return (brdfType & types) != 0; //this is: if brdfType contains some of the types
        }

		public Color3 getTransparency(RayContext rayContext) 
		{
			Color3 transparency = filterColor.getWhiteComplement();
			
			if (null != filterTexture)
				transparency = filterTexture.evalColor(rayContext).getWhiteComplement();

			return transparency;
		}

		public Color3 getFilterColor(RayContext rayContext) {
			if (null != filterTexture)
				return filterTexture.evalColor(rayContext);

			return filterColor;
		}

	}
}
