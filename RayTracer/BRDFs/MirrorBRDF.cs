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
	/// Description of MirrorBRDF.
	/// </summary>
	public class MirrorBRDF : BRDF {
		
		public MirrorBRDF() {
		}

        public override bool getSample(RayContext rayContext, double ru, double rv, out Vector3 wi, out double invPdf)
        {
			//в каква посока гледа наблюдателят (входящ лъч)
			Vector3 viewDir = rayContext.ray.dir;
			//faceforward се грижи нормалата винаги да е от страната на входящия лъч 
			Vector3 normal = MathUtils.faceforward(rayContext.hitData.hitNormal, viewDir);

			//намираме посоката на отразения лъч (изходящ лъч)
			wi = viewDir - (normal * (normal * viewDir) * 2.0);
			wi.normalize();

			invPdf = 0;
			return true;
        }

		public override bool isSingular() {
			return true;
		}

        public override BRDFTypes getType()
        {
            return BRDFTypes.Specular | BRDFTypes.Perfect;
        }
		
	}
}
