/************************************************************
	Copyright (C) 2006-2013 by Hristo Lesev
	hristo.lesev@diadraw.com
	for educational purposes only, not for commercial use
************************************************************/
using System;

namespace RayTracer.Math
{
	/// <summary>
	/// Class Matrix3 representing 3x3 matrix and its operations.
	/// </summary>
	public class Matrix3 {
		
		private Vector3[] m;
		
		public Matrix3() {
			m = new Vector3[3];
			setIdentity();
		}
		
		public void setIdentity() {
			m[0].set(1.0f, 0.0f, 0.0f);
			m[1].set(0.0f, 1.0f, 0.0f);
			m[2].set(0.0f, 0.0f, 1.0f);
		}
		
		public void setRow(int row, Vector3 a) {
			m[0][row] = a.x;
			m[1][row] = a.y;
			m[2][row] = a.z;
		}
		
		public Matrix3 getInverted() {
			Matrix3 res = new Matrix3();
			double det = m[0] * ( m[1] ^ m[2] );
			//if(det==0.0f) //error
	
			res.setRow(0, (m[1]^m[2]) / det);
			res.setRow(1, (m[2]^m[0]) / det);
			res.setRow(2, (m[0]^m[1]) / det);
			return res;
		}
		
		//transform a vector
		//DOES NOT translate it
		public Vector3 transformVector( Vector3 v ) {
			Vector3 rv = new Vector3();
			
			rv.x = m[0]*v;
			rv.y = m[1]*v;
			rv.z = m[2]*v;
			
			return rv;
		}
		
		//from given normal vector
		//calculate two orthogonal vectors in tangent plane
		public void computeTangentBasis(Vector3 n) {
			Vector3 vU, vV;
	
			vU = n ^ (new Vector3(0.643782, 0.98432, 0.324632));
	
			if (vU.getLenghtSqr() < 0.00001)
				vU = n ^ (new Vector3(0.432902, 0.43223, 0.908953));
	
			vV = n ^ vU;
	
			vU.normalize();
			vV.normalize();
	
			setRow(0, vU);
			setRow(1, vV);
			setRow(2, n );
		}
		
	}
}
