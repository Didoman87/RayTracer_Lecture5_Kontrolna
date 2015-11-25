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

namespace RayTracer.Parsers {

	public interface ISceneLoader 
	{
		Shader findMaterial(string mtlName);
		void loadScene(Scene scene);
	}
}
