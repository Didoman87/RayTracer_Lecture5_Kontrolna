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

namespace RayTracer.Core
{
    public interface Texture
    {
        Color3 evalColor(RayContext rayContext);
    }
}
