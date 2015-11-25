/************************************************************
	Copyright (C) 2006-2013 by Hristo Lesev
	hristo.lesev@diadraw.com
	for educational purposes only, not for commercial use
************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RayTracer.Math;
using RayTracer.Core;

namespace RayTracer.Primitives {

	public class TriangleMesh 
	{
		public Vector3[] v;
		public int[] f;
		public Vector3[] tc;
		public Vector3[] vn;
		public int[] faceShaderIDs;

		private int m_numFaces;
		private int m_numVerts;
		private int m_vertsPerFace;

		public bool invertNormals;

		public TriangleMesh() {
			invertNormals = false;
		}

		public int numVerts {
			get{
				return m_numVerts;
			} 

			set{
				m_numVerts = value; 
				v = new Vector3[m_numVerts];
				
			}
		}

		public int numFaces {
			get {
				return m_numFaces;
			}

			set {
				m_numFaces = value;
				f = new int[m_numFaces * m_vertsPerFace];
				tc = new Vector3[m_numFaces * m_vertsPerFace];
				faceShaderIDs = new int[m_numFaces];
				vn = new Vector3[m_numFaces * m_vertsPerFace];
			}
		}

		public int vertsPerFace {
			get {
				return m_vertsPerFace;
			}

			set {
				m_vertsPerFace = value;
			}
		}
		
		
		public void compileMeshToScene(Scene scene, Shader shader) {
			int faceID = 0;
			for (int i = 0; i < numFaces * m_vertsPerFace; i += m_vertsPerFace) {
				
				Triangle tri = new Triangle();
				tri.v[0].set(v[ f[i+0] ]);
	            tri.v[1].set(v[ f[i+1] ]);
	            tri.v[2].set(v[ f[i+2] ]);

				tri.tc[0].set(tc[i + 0]);
				tri.tc[1].set(tc[i + 1]);
				tri.tc[2].set(tc[i + 2]);

				tri.vn[0].set(vn[i + 0]);
				tri.vn[1].set(vn[i + 1]);
				tri.vn[2].set(vn[i + 2]);

				tri.shaderID = faceShaderIDs[faceID];

	            tri.setShader(shader);
	            scene.addPrimitive(tri);

				faceID++;
			}
		}
	}

}
