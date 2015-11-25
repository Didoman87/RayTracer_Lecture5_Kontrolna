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
	/// Description of PhongBRDF.
	/// </summary>
	public class PhongBRDF : BRDF {
		
		private double glossy;
		private int numRays;
		
		public PhongBRDF(double _glossy, int _numRays) {
			glossy = 1.0/System.Math.Pow(1.0-_glossy, 3.5)-1.0;
			numRays = _numRays;
		}

		
		public override Color3 eval(RayContext rayContext, Vector3 wi, out double pdf) 
		{
			Vector3 viewDir = rayContext.ray.dir;
			Vector3 normal = rayContext.hitData.hitNormal;
			Vector3 reflectDir = viewDir - (normal * (normal * viewDir) * 2.0);
            reflectDir.normalize();

            double cosA = wi * reflectDir;

			double k = 0.0f;

            if (cosA > 0.0f)
                k = System.Math.Pow(cosA, glossy) * (glossy + 1.0f) * (2.0 / System.Math.PI);

			pdf = k;

            return filterColor *k; 
		}
		
		
		public override bool getSample(RayContext rayContext, double ru, double rv, out Vector3 wi, out double invPdf)
        {	
			Vector3 viewDir = rayContext.ray.dir;
			Vector3 normal = rayContext.hitData.hitNormal;
			
			Vector3 reflectDir = viewDir - (normal * (normal * viewDir) * 2.0);
			reflectDir.normalize();

			Matrix3 shadingMatrix = new Matrix3();
            shadingMatrix.computeTangentBasis(reflectDir);

			double pdf = 1;
            invPdf = 1;

			wi = MathUtils.getSpecularDir(ru, rv, glossy, out pdf);

			wi = shadingMatrix.transformVector(wi);
			wi.normalize();

            if (pdf < 0.0001)
                return false;

			invPdf = 1.0 / pdf;

			double cosT = rayContext.hitData.hitNormal * wi;
			if (cosT < 0.0)
				return false;

			return true;	
        }
		
		public override bool isSingular() 
		{
			return false;
		}

        public override BRDFTypes getType()
        {
            return BRDFTypes.Specular | BRDFTypes.Glossy;
        }
		
	}
}
