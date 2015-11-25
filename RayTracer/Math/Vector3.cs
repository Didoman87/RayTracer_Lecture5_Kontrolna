/************************************************************
	Copyright (C) 2006-2013 by Hristo Lesev
	hristo.lesev@diadraw.com
	for educational purposes only, not for commercial use
************************************************************/
using System;

namespace RayTracer.Math
{
	/// <summary>
	/// Description of Vector3.
	/// </summary>
	public struct Vector3 {
		
		public double x;
		public double y;
		public double z;
		
		#region Constructors
		public Vector3(double _x, double _y, double _z) {
			this.x = _x;
			this.y = _y;
			this.z = _z;
		}
		
		public Vector3( Vector3 v ) {
			this.x = v.x;
			this.y = v.y;
			this.z = v.z;
		}
		#endregion Constructors
		
		#region Setters
		public void set(double _x, double _y, double _z) {
			this.x = _x;
			this.y = _y;
			this.z = _z;
		}
		
		public void set( Vector3 v ) {
			this.x = v.x;
			this.y = v.y;
			this.z = v.z;
		}
		#endregion Setters
		
		#region Vector to Vector Operations
		
		public static Vector3 operator+(Vector3 v1) {
			return
		    (
		    	new Vector3
		        	(
		            	+v1.x,
		                +v1.y,
		                +v1.z
		            )
		    );
		}
		
		public static Vector3 operator-(Vector3 v1) {
			return
		    (
		    	new Vector3
		        	(
		            	-v1.x,
		                -v1.y,
		                -v1.z
		            )
		    );
		}
		
		public static Vector3 operator+(Vector3 v1, Vector3 v2) {
			return
		    (
		    	new Vector3
		        	(
		            	v1.x + v2.x,
		                v1.y + v2.y,
		                v1.z + v2.z
		            )
		    );
		}
		
		public static Vector3 operator-(Vector3 v1, Vector3 v2) {
			return
		    (
		    	new Vector3
		        	(
		            	v1.x - v2.x,
		                v1.y - v2.y,
		                v1.z - v2.z
		            )
		    );
		}
		
		//DOT product
		public static double operator*(Vector3 v1, Vector3 v2) {
			return
		    (
	            v1.x * v2.x +
	            v1.y * v2.y +
	            v1.z * v2.z
		            
		    );
		}
		
		//CROSS product
		public static Vector3 operator^(Vector3 v1, Vector3 v2) {
			return
		    (
				new Vector3( v1.y*v2.z-v1.z*v2.y, v1.z*v2.x-v1.x*v2.z, v1.x*v2.y-v1.y*v2.x )
		    );
		}
		
		#endregion
		
		#region Vector to Scalar Operations
		public static Vector3 operator+(Vector3 v1, double s2) {
			return
		    (
		    	new Vector3
		        	(
		            	v1.x + s2,
		                v1.y + s2,
		                v1.z + s2
		            )
		    );
		}
		
		public static Vector3 operator-(Vector3 v1, double s2) {
			return
		    (
		    	new Vector3
		        	(
		            	v1.x - s2,
		                v1.y - s2,
		                v1.z - s2
		            )
		    );
		}
		
		public static Vector3 operator*(Vector3 v1, double s2) {
			return
		    (
		    	new Vector3
		        	(
		            	v1.x * s2,
		                v1.y * s2,
		                v1.z * s2
		            )
		    );
		}
		
		public static Vector3 operator/(Vector3 v1, double s2) {
			double s = 1.0 / s2;
			return
		    (
		    	new Vector3
		        	(
		            	v1.x * s,
		                v1.y * s,
		                v1.z * s
		            )
		    );
		}
		
		public static Vector3 operator+(double s1, Vector3 v2) {
			return v2 + s1;
		}
		
		public static Vector3 operator-(double s1, Vector3 v2) {
			return v2 - s1;
		}
		
		public static Vector3 operator*(double s1, Vector3 v2) {
			return v2 * s1;
		}
		
		public static Vector3 operator/(double s1, Vector3 v2) {
			return v2 / s1;
		}
		
		#endregion
		
		#region Vector methods
		
		public double getLenght() {
			return System.Math.Sqrt( x*x + y*y + z*z );
		}
		
		public double getLenghtSqr() {
			return ( x*x + y*y + z*z );
		}
		
		public void normalize() {
			double len = getLenght();
			if (len>1e-12f) {
				x /= len;
				y /= len;
				z /= len;
			}
		}
		
		public Vector3 getNormalized() {
			Vector3 vec = new Vector3( this.x, this.y, this.z );
			vec.normalize();
			return vec;
		}
		
		#endregion
		
		public double this[ int index ]
		{
		   get
		   {
		      switch (index)
		      {
		         case 0: {return x; }
		         case 1: {return y; }
		         case 2: {return z; }
		         default: throw new ArgumentException("try to access index outside [0.2]", "index");
		     }
		   }
		   set
		   {
		       switch (index)
		       {
		          case 0: {x = value; break;}
		          case 1: {y = value; break;}
		          case 2: {z = value; break;}
		          default: throw new ArgumentException("try to access index outside [0.2]", "index");
		      }
		   }
		}

		
	}
}
