/************************************************************
	Copyright (C) 2006-2013 by Hristo Lesev
	hristo.lesev@diadraw.com
	for educational purposes only, not for commercial use
************************************************************/
using System;

namespace RayTracer.Math
{
	/// <summary>
	/// Description of Color3.
	/// </summary>
	public struct Color3 {
		
		public double r;
		public double g;
		public double b;
		
		#region Constructors
		public Color3(double _r, double _g, double _b) {
			this.r = _r;
			this.g = _g;
			this.b = _b;
		}
		
		public Color3( Color3 color ) {
			this.r = color.r;
			this.g = color.g;
			this.b = color.b;
		}
		#endregion Constructors
		
		#region Setters
		public void set(double _r, double _g, double _b) {
			this.r = _r;
			this.g = _g;
			this.b = _b;
		}
		
		public void set( Color3 color ) {
			this.r = color.r;
			this.g = color.g;
			this.b = color.b;
		}
		
		public void makeBlack() { r=g=b=0.0; }
		public void makeWhite() { r=g=b=1.0; }
		#endregion Setters
		
		//public bool isBlack(void) const { return (r<1e-12f && g<1e-12f && b<1e-12f); }
		
		#region Color to Color Operations
		
		public static Color3 operator+(Color3 c1) {
			return
		    (
		    	new Color3
		        	(
		            	+c1.r,
		                +c1.g,
		                +c1.b
		            )
		    );
		}//c1+c2
		
		public static Color3 operator-(Color3 c1) {
			return
		    (
		    	new Color3
		        	(
		            	-c1.r,
		                -c1.g,
		                -c1.b
		            )
		    );
		}//c1+c2
		
		public static Color3 operator+(Color3 c1, Color3 c2) {
			return
		    (
		    	new Color3
		        	(
		            	c1.r + c2.r,
		                c1.g + c2.g,
		                c1.b + c2.b
		            )
		    );
		}//c1+c2
		
		public static Color3 operator-(Color3 c1, Color3 c2) {
			return
		    (
		    	new Color3
		        	(
		            	c1.r - c2.r,
		                c1.g - c2.g,
		                c1.b - c2.b
		            )
		    );
		}//c1-c2
		
		public static Color3 operator*(Color3 c1, Color3 c2) {
			return
		    (
		    	new Color3
		        	(
		            	c1.r * c2.r,
		                c1.g * c2.g,
		                c1.b * c2.b
		            )
		    );
		}//c1*c2
		
		public static Color3 operator/(Color3 c1, Color3 c2) {
			return
		    (
		    	new Color3
		        	(
		            	c1.r / c2.r,
		                c1.g / c2.g,
		                c1.b / c2.b
		            )
		    );
		}//c1/c2
		#endregion Color to Color Operations
		
		#region Color to Scalar Operations
		public static Color3 operator+(Color3 c1, double s2) {
			return
		    (
		    	new Color3
		        	(
		            	c1.r + s2,
		                c1.g + s2,
		                c1.b + s2
		            )
		    );
		}
		
		public static Color3 operator-(Color3 c1, double s2) {
			return
		    (
		    	new Color3
		        	(
		            	c1.r - s2,
		                c1.g - s2,
		                c1.b - s2
		            )
		    );
		}
		
		public static Color3 operator*(Color3 c1, double s2) {
			return
		    (
		    	new Color3
		        	(
		            	c1.r * s2,
		                c1.g * s2,
		                c1.b * s2
		            )
		    );
		}
		
		public static Color3 operator/(Color3 c1, double s2) {
			return
		    (
		    	new Color3
		        	(
		            	c1.r / s2,
		                c1.g / s2,
		                c1.b / s2
		            )
		    );
		}
		
		public static Color3 operator+(double s1, Color3 c2){
			return c2 + s1;
		}
		
		public static Color3 operator-(double s1, Color3 c2){
			return c2 - s1;
		}
		
		public static Color3 operator*(double s1, Color3 c2){
			return c2 * s1;
		}
		
		public static Color3 operator/(double s1, Color3 c2){
			return c2 / s1;
		}
		#endregion Color to Scalar Operations
		
		public override string ToString() {
			return String.Format("({0}, {1}, {2})", r, g, b);
		}
		
		public double getIntensity() {
			return (r+g+b) * 0.33333;
		}

		public Color3 getWhiteComplement() 
		{
			return new Color3(System.Math.Max(0.0, 1.0 - r), System.Math.Max(0.0, 1.0 - g), System.Math.Max(0.0, 1.0 - b) );
		}
		
	}
}
