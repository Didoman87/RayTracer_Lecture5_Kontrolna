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
    public class IntegratorMain : IIntegrator
    {

        private IIntegrator m_directLightIntegrator;
        private IIntegrator m_specularIntegrator;
        private IIntegrator m_diffuseIntegrator;
        private int m_maxTraceLevel;

        public IntegratorMain()
        {
            m_directLightIntegrator = new IntegratorDirectLight();
            m_specularIntegrator = new IntegratorSpecular();
            //m_diffuseIntegrator = new IntegratorDiffuse();
            m_maxTraceLevel = 5;
        }

        #region IIntegrator Members

        public Color3 illuminate(RayContext rayContext, BRDF brdf)
        {
            Color3 finalColor = new Color3();

            BRDFTypes brdfType = brdf.getType();

            if (brdf.matchTypes(BRDFTypes.Diffuse) || brdf.matchTypes(BRDFTypes.Glossy))
            {
                if (null != m_directLightIntegrator)
                    finalColor += m_directLightIntegrator.illuminate(rayContext, brdf);

                if (brdf.matchTypes(BRDFTypes.Diffuse) && null != m_diffuseIntegrator)
                    finalColor += m_diffuseIntegrator.illuminate(rayContext, brdf);
            }

            if (brdf.matchTypes(BRDFTypes.Specular))
            {
                if (rayContext.traceLevel < m_maxTraceLevel)
                    if (null != m_specularIntegrator)
                        finalColor += m_specularIntegrator.illuminate(rayContext, brdf);
            }

            return finalColor;
        }

        #endregion
    }
}
