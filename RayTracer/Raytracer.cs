/************************************************************
	Copyright (C) 2006-2013 by Hristo Lesev
	hristo.lesev@diadraw.com
	for educational purposes only, not for commercial use
************************************************************/
using System;
using System.Drawing;
using System.Windows.Forms;
using RayTracer.Core;
using RayTracer.Math;
using RayTracer.Primitives;
using RayTracer.Shaders;
using RayTracer.Cameras;
using RayTracer.Lights;
using RayTracer.Parsers;
using System.Threading;
using System.Collections.Generic;
using RayTracer.Renderers;
using System.IO;
using Microsoft.CSharp;
using System.CodeDom.Compiler;

namespace RayTracer
{
	/// <summary>
	/// Description of Raytracer.
	/// </summary>
	public class Raytracer {
		
		public Scene scene;
		
		public Raytracer() {
			scene = new Scene();
		}
		
		public void prepareScene( string sceneFileName ) {

			scene.globalSettings = new GlobalSettings();
			scene.setBackgroundColor( new Color3(0, 0, 0) );

			SceneLoaderPython sceneLoader = new SceneLoaderPython();
			sceneLoader.setSceneFile(sceneFileName);
			sceneLoader.loadScene(scene);
		}

		public void startRaytracing( Display display ) 
        {
			scene.setBackgroundColor(new Color3(0.6, 0.6, 0.6));

            try
            {
				IntegratorMain integrator = new IntegratorMain();
				PixelSamplerOne pixelSampler = new PixelSamplerOne(integrator);
				ImageSamplerBuckets imageSampler = new ImageSamplerBuckets(pixelSampler);
				RendrerBase renderer = new RendrerBase(imageSampler);

                renderer.startRendering( scene, display);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
            }

		}//startRaytracing

        
	}
}
