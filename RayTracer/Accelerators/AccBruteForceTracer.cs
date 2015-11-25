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

namespace RayTracer.Accelerators
{
    class AccBruteForceTracer
    {

        private List<GeomPrimitive> m_primitives;

        public AccBruteForceTracer(ref List<GeomPrimitive> primitives)
        {
            m_primitives = primitives;
        }

        public bool trace(RayContext rayContext)
        {
            double maxt = rayContext.ray.maxt;
            bool haveIntersection = false;

            foreach (GeomPrimitive primitive in m_primitives)
            {

                if (rayContext.ignorePrimitive == primitive)
                    continue; //skip this primitive

                IntersectionData hitData = new IntersectionData();

                if (primitive.intersect(rayContext.ray, hitData))
                {

                    if (maxt > hitData.hitT && hitData.hitT > rayContext.ray.mint)
                    {
                        maxt = hitData.hitT;

                        rayContext.hitData = hitData;
                        rayContext.hitData.hitPrimitive = primitive;
                        haveIntersection = true;
                    }

                    if (haveIntersection)
                    {
                        rayContext.hitData.hitPos = rayContext.ray.p + rayContext.ray.dir * rayContext.hitData.hitT;
                    }
                }
            }//foreach
            return haveIntersection;
        }//trace


    }
}
