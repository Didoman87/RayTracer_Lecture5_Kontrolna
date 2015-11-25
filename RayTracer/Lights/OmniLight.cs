/************************************************************
	Copyright (C) 2006-2013 by Hristo Lesev
	hristo.lesev@diadraw.com
	for educational purposes only, not for commercial use
************************************************************/
using System;
using RayTracer.Core;
using RayTracer.Math;

namespace RayTracer.Lights
{
	/// <summary>
	/// Description of OmniLight.
	/// </summary>
	public class OmniLight : Light {
		
		public Vector3 pos;
		public Color3 color;
		public double power;
		
		public OmniLight() 
		{
			pos = new Vector3();
			color = new Color3();
			power = 1.0;
		}

        public override bool isSingular()
        {
            return true;
        }

        public override LightSample getSample(RayContext rayContext, double ru, double rv)
        {
            //намираме векрор между двете точки
            Vector3 dir = pos - rayContext.hitData.hitPos;
            //определяме каква част от сферата (пространствен ъгъл) покрива точката
            double solidAngle = (1.0 / (4.0 * System.Math.PI * dir.getLenghtSqr()));
            //максимална дистанция за търсене на пресичания
            double maxt = dir.getLenght(); dir.normalize();
            //ако светлината се намира под повърхността от която е точката,
            //няма смисъл да пресмятаме нищо
            if (dir * (rayContext.hitData.hitNormal) <= 0)
                return null;
            //създаваме "сонда за сянка" и попълваме
            LightSample sample = new LightSample();
            sample.shadowRay = new Ray();
            sample.shadowRay.p.set(rayContext.hitData.hitPos);
            sample.shadowRay.dir.set(dir);
            sample.shadowRay.maxt = maxt;
            //задаваме енергията (изпратена през пространствения ъгъл)
            sample.color = color * (power * solidAngle);
            return sample;
        }

		
		
	}
}
