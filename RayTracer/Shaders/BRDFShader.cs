using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RayTracer.Textures;
using RayTracer.Core;

namespace RayTracer.Shaders
{
    public class BRDFShader : Shader
    {
        public BRDF m_brdf;
        public BRDFShader() { }
        public override void shade(RayContext rayContext)
        {
            rayContext.resultColor = rayContext.evalLight(m_brdf);
        }
    }
}
