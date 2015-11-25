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
	
	public class GlobalSettings {
		
		public int imageWidth;
		public int imageHeight;

		public int bucketWidth;
		public int bucketHeight;

		public int maxTreadsCount;

		public GlobalSettings() {
			//set defaults
			imageWidth = 500;
			imageHeight = 500;

			bucketWidth = 64;
			bucketHeight = 64;

			maxTreadsCount = 5;
		}
	}

}
