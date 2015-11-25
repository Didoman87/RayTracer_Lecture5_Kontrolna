using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RayTracer.Textures;
using RayTracer.Core;
namespace RayTracer.Shaders
{
    public class TextureShader : Shader
    {
        public TextureImage texture;
        public TextureShader() { }
        public override void shade(RayContext rayContext)
        {
            rayContext.resultColor = texture.evalColor(rayContext);
        }
    }
}
