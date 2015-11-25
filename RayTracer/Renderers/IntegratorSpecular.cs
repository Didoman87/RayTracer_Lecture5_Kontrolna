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
    public class IntegratorSpecular : IIntegrator
    {
        #region IIntegrator Members

        public Color3 illuminate(RayContext rayContext, BRDF brdf)
        {
			Ray newRay = new Ray();

			double invPdf = 0;

			//във finalColor ще сумираме резултатния цвят
			Color3 finalColor = new Color3();

			//задаваме максималната бройка лъчи
			//с които ще проследяваме грапави отражения
			int numSamples = 10;
			int curSample = 0;  //номер на текущ лъч

			for (curSample = 0; curSample < numSamples; curSample++)
			{
				//генерирме двойка случайни числа (ru, rv)
				double ru = rayContext.getRandom(0, curSample);
				double rv = rayContext.getRandom(1, curSample);

				//искаме brdf да конструира лъч използвайки (ru, rv) 
				bool isValidSample = brdf.getSample(rayContext, ru, rv, out newRay.dir, out invPdf);

				if (!isValidSample)
					continue;

				//конструираме нов лъч от текущата точка
				//и го обвиваме във собствен контекст
				newRay.p = rayContext.hitData.hitPos;
				RayContext newContext = RayContext.createNewContext(rayContext, newRay);
				//извикваме функцията shade, която проследява 
				//лъча в сцената и го осветява
				rayContext.scene.shade(newContext);

				double cosT = rayContext.hitData.hitNormal * (newRay.dir);
				if (cosT < 0.0) cosT *= -1.0;

				double pdf;

				Color3 w = new Color3(1, 1, 1);
				//пресмятаме, каква част от светлината ще отрази BRFD-a
				w = brdf.eval(rayContext, newRay.dir, out pdf);

				if (!brdf.isSingular())
				{
					w *= cosT * invPdf;
				}
				//добавяме енергията на новия лъч
				finalColor += newContext.resultColor * w;

                //if (Double.IsNaN(finalColor.getIntensity()))
                //    break;

				if (brdf.isSingular())
					break;
			}

			//сумираме енергията от всички лъчи
			if (curSample > 0)
				finalColor *= 1.0 / (double)curSample;

			return finalColor;
        }

        #endregion
    }
}
