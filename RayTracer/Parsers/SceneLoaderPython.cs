/************************************************************
	Copyright (C) 2006-2013 by Hristo Lesev
	hristo.lesev@diadraw.com
	for educational purposes only, not for commercial use
************************************************************/
using System;
using System.Collections.Generic;
using IronPython.Hosting;
using IronPython.Runtime;
using Microsoft.Scripting;
using Microsoft.Scripting.Hosting;
using RayTracer.Core;

namespace RayTracer.Parsers
{
    class SceneLoaderPython : SceneLoaderBase
    {
        public virtual void loadScene(Core.Scene scene)
        {
            startPythonEngine();
            m_scope.SetVariable("scene", scene);
            m_scope.SetVariable("m_materials", m_materials);
            m_scope.SetVariable("loader", this);
			m_scope.SetVariable("basePath", m_basePath);
            CompileSourceAndExecute();

			foreach (KeyValuePair<string, Shader> pair in m_materials) {
				pair.Value.frameBegin();
			}
        }

        public void setSceneFile( string fileName )
        {
            m_fileName = fileName;
			m_basePath = System.IO.Path.GetDirectoryName(m_fileName);
        }

        private void startPythonEngine()
        {
            m_engine = Python.CreateEngine();
            m_runtime = m_engine.Runtime;
            m_scope = m_engine.CreateScope();

            m_runtime.LoadAssembly(GetType().Assembly);
        }

        private void CompileSourceAndExecute( )
        {
            ScriptSource source = m_engine.CreateScriptSourceFromFile( m_fileName, System.Text.Encoding.Unicode );
            CompiledCode compiled = source.Compile();
            compiled.Execute(m_scope);
        }

        private ScriptEngine    m_engine;
        private ScriptRuntime   m_runtime;
        private ScriptScope     m_scope;
        private string          m_fileName;
		private string			m_basePath;
    }
}
