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
using RayTracer.Shaders;

namespace RayTracer.Parsers {
	
	public class SceneLoaderBase : ISceneLoader
	{

		public SceneLoaderBase() {
			m_materials = new Dictionary<string, Shader>();
		}

		public virtual void loadScene(Core.Scene scene) {
			throw new NotImplementedException();
		}

		public virtual Core.Shader findMaterial(string mtlName) {
			if (m_materials.ContainsKey(mtlName))
				return m_materials[mtlName];
			else
				return new ConstantShader();
		}

        public virtual string debugPrint(string message)
        {
            System.Diagnostics.Debug.WriteLine(message);
            return "";
        }

		protected Dictionary<string, Shader> m_materials;
	}

}
