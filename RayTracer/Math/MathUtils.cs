/************************************************************
	Copyright (C) 2006-2013 by Hristo Lesev
	hristo.lesev@diadraw.com
	for educational purposes only, not for commercial use
************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RayTracer.Math {
	
	public class MathUtils {
		
		public static double frac(double x) 
		{
			return x < 0 ? x - (int) x + 1 : x - (int) x;
		}

		public static Vector3 faceforward(Vector3 v, Vector3 right) 
		{
			if ((right * v) < 0) 
				return v; 
			else 
				return -v;
		}

		public static Vector3 getDiffuseDir(double u, double v, out double prob) 
		{
			double thetaSin = System.Math.Sqrt(u);
			double thetaCos = System.Math.Sqrt(1.0f - u);
			double phi = System.Math.PI * 2.0 * v;
			prob = thetaSin * 2.0;
			return new Vector3(System.Math.Cos(phi) * thetaCos, System.Math.Sin(phi) * thetaCos, thetaSin);
		}

		public static Vector3 getSpecularDir(double u, double v, double n, out double pdf) 
		{
			double thetaSin = System.Math.Pow(u, 1.0f / (n + 1.0f));
			double thetaCos = System.Math.Sqrt(1.0f - thetaSin * thetaSin);
			double phi = 2.0 * System.Math.PI * v;
			pdf = (n + 1.0f) * System.Math.Pow(u, n / (n + 1.0f));
			return new Vector3(System.Math.Cos(phi) * thetaCos, System.Math.Sin(phi) * thetaCos, thetaSin);
		}

	}

}
