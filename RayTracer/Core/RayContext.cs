/************************************************************
	Copyright (C) 2006-2013 by Hristo Lesev
	hristo.lesev@diadraw.com
	for educational purposes only, not for commercial use
************************************************************/
using System;
using RayTracer.Math;

namespace RayTracer.Core
{
	/// <summary>
	/// Description of RayContext.
	/// </summary>
	public class RayContext {
		
		//Input Ray params
		public Ray ray; //the ray that will be traced
		public GeomPrimitive ignorePrimitive; //this primitive wont be intersected with the ray
		
		//Intersection (hit) Info
		public IntersectionData hitData;
		
		
		//Shading Result
		public Color3 resultColor;
		
		public int traceLevel;
        public int traceLevelGI;
		
		public Scene scene;

        public RandomContext m_rndContext;

        public int randomIndex;

        public IIntegrator m_renderer;

		public int ignoreID;

		public bool isShadowRay;
		
		public RayContext() {
			traceLevel = 0;
			ignoreID = -1;

			isShadowRay = false;
		}
		
		public RayContext(Ray _ray) {
			ray = new Ray(_ray);
			traceLevel = 0;
            traceLevelGI = 0;
			ignoreID = -1;
		}
		
		public Color3 evalLight(BRDF brdf) {
            return m_renderer.illuminate(this, brdf);
		}

        public double getRandom(int dim, int id)
        {
            return m_rndContext.getRandom(id, dim);
        }

		public static RayContext startFromPixel(Ray startRay, int pixX, int pixY, Scene _scene, IIntegrator integrator) {
			RayContext newContext = new RayContext(startRay);
			newContext.scene = _scene;
			newContext.m_renderer = integrator;
			newContext.m_rndContext = new RandomContext(2, 17 * pixX + 73 * pixY);

			return newContext;
		}
		
		public static RayContext createNewContext(RayContext previousContext, Ray newRay) {
			RayContext newContext = new RayContext(newRay);
			newContext.traceLevel += previousContext.traceLevel + 1;
			newContext.scene = previousContext.scene;
			newContext.ignorePrimitive = previousContext.hitData.hitPrimitive;

            newContext.m_renderer = previousContext.m_renderer;
			newContext.ignoreID = previousContext.ignoreID;

            newContext.m_rndContext = previousContext.m_rndContext.createNew(previousContext.randomIndex, 2);
			return newContext;
		}

        public static RayContext createNewContextGI(RayContext previousContext, Ray newRay) {
            RayContext newContext = createNewContext(previousContext, newRay);
            newContext.traceLevelGI += previousContext.traceLevelGI + 1;
            return newContext;
        }
		
	}
}
