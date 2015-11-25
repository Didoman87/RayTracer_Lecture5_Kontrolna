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
    public class IntegratorDirectLight : IIntegrator
    {
        #region IIntegrator Members

        public Color3 illuminate(RayContext rayContext, BRDF brdf)
        {
            //вземаме списък с всички светлини в сцената
            List<Light> lights = rayContext.scene.getLights();
            //в sumColor ще сумираме тяхната енергия 
            Color3 sumColor = new Color3();
            foreach (Light light in lights)
            {
                Color3 color = new Color3();
                //задаваме максималната бройка лъчи
                //с които ще проследяваме към всяка светлина 
                int numSamples = 25;
                int curSample = 0; //номер на текущ лъч
                LightSample lightSample;

                rayContext.randomIndex = 0;
                
                do
                {
                    curSample++;
                    //генерирме двойка случайни числа (ru, rv)
                    double ru = rayContext.getRandom(0, curSample);
                    double rv = rayContext.getRandom(1, curSample);
                    //искаме от светлината да върне сонда за сянка
                    lightSample = light.getSample(rayContext, ru, rv);

                    rayContext.randomIndex++;

                    if (null == lightSample)
                        continue;
                    //пресмятаме косинуса между нормалата на повърхността и лъча към лампата
                    double cosT = rayContext.hitData.hitNormal * (lightSample.shadowRay.dir);
                    if (cosT < 0.00001) cosT = 0.0;

                    //проверяваме за засенчване
                    if (rayContext.scene.traceShadowRay(lightSample.shadowRay, rayContext))
                        continue;

                    double pdf = 1.0;
                    //пресмятаме, каква част от светлината ще отрази BRFD-a
                    Color3 brdfCol = brdf.eval(rayContext, lightSample.shadowRay.dir, out pdf);

                    color += brdfCol * pdf * lightSample.color * cosT;

                    //ако светлината е точкова ни е нужен само един лъч
                    if (light.isSingular())
                        break;

                } while (curSample < numSamples);

                if (curSample < 1)
                    curSample = 1;

                //сумираме резултата от всички сампъли
                color *= 1.0 / (double)curSample;
                sumColor += color;
            }

            int lightsCount = lights.Count;
            if (lightsCount < 1)
                lightsCount = 1;

            //сумираме енергията от всички светлини
            sumColor *= 1.0 / (double)lightsCount;
            return sumColor;
        }

        #endregion
    }
}
