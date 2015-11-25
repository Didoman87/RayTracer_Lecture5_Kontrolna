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
	/// Description of GlassBRDF.
	/// </summary>
	public class GlassBRDF : BRDF {

        public double m_ior;

		public GlassBRDF() {
            m_ior = 1.0;
		}	

        public override bool getSample(RayContext rayContext, double ru, double rv, out Vector3 wi, out double invPdf)
        {
			Vector3 viewDir = rayContext.ray.dir;
			Vector3 normal = rayContext.hitData.hitNormal;
			double ior = 1.0 / m_ior;

			double thetaView = viewDir * normal;
            double thetaT = 1.0 - (ior * ior) * (1.0 - thetaView * thetaView);

            wi = new Vector3(0, 0, 0);
            invPdf = 0;
            if (thetaT < 0.0f) // проверка за пълно вътрешно отражение
                return false;

            wi = ior * viewDir - (ior * thetaView + System.Math.Sqrt(thetaT)) * normal;

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
