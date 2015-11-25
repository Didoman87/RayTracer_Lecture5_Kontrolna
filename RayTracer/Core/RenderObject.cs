/************************************************************
	Copyright (C) 2006-2013 by Hristo Lesev
	hristo.lesev@diadraw.com
	for educational purposes only, not for commercial use
************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RayTracer.Core {

	interface RenderObject {
		void frameBegin();
		void frameEnd();
	}

}
