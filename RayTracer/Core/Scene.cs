/************************************************************
	Copyright (C) 2006-2013 by Hristo Lesev
	hristo.lesev@diadraw.com
	for educational purposes only, not for commercial use
************************************************************/
using System;
using System.Collections.Generic;
using RayTracer.Math;
using RayTracer.Accelerators;

namespace RayTracer.Core
{
	/// <summary>
	/// Description of Scene.
	/// </summary>
	public class Scene {
		
		private List<GeomPrimitive> primitives;
		private List<Light>			lights;
		private Color3				backgroundColor;
		private Camera				camera;

        private AccBruteForceTracer m_bruteforceTracer;
        private Spatial             m_octree;
		private SkyDome				m_skyDome;

		public GlobalSettings		globalSettings;
		
		public Scene() {
			primitives = new List<GeomPrimitive>();
			backgroundColor = new Color3(0, 0, 0);
			lights = new List<Light>();
		}
		
		public void setCamera(Camera cam) {
			camera = cam;
		}
		
		public Camera getCamera() {
			return camera;
		}
		
		public List<Light> getLights() {
			return lights;
		}
		
		public void addLight(Light light) {
			lights.Add(light);
		}
		
		public void setBackgroundColor(Color3 bgColor) {
			backgroundColor.set(bgColor);
		}

		public void setBackgroundTexture(string imagePath) {
			m_skyDome = new SkyDome(imagePath);
		}
		
		public void addPrimitive(GeomPrimitive primitive) {
			primitives.Add(primitive);
		}
		
		public void removePrimitive(GeomPrimitive primitive) {
			primitives.Remove(primitive);
		}

		public void prepareForRender() 
		{
			m_bruteforceTracer = new AccBruteForceTracer( ref primitives );
		}

		public bool trace(RayContext rayContext) {
			return m_bruteforceTracer.trace( rayContext );
		}//trace

		public bool traceShadowRay(Ray shadowRay, RayContext origContext) {
			RayContext rayContext = RayContext.createNewContext(origContext, shadowRay);
			rayContext.ignorePrimitive = origContext.hitData.hitPrimitive;
			rayContext.isShadowRay = true;
			return trace(rayContext);
		}
		
		public void shade(RayContext rayContext) 
		{
			
			if( trace(rayContext) ) {
				
				if( null != rayContext.hitData.hitPrimitive ) {
					rayContext.hitData.hitPrimitive.shader.shade(rayContext);
					return;
				}
				
			}
			rayContext.resultColor.set( shadeBackground(rayContext) );
		}

		public Color3 shadeBackground(RayContext rayContext) {
			if (null != m_skyDome)
				return m_skyDome.getRadiance(rayContext);

			return backgroundColor;
		}
        
	}
}
