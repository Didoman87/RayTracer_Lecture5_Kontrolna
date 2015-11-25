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
    public class RandomContext
    {
        public int m_index;
        public int m_dimension;

        public double qmcD0I;
        public double qmcD1I;

        public RandomContext()
        {
        }

        public RandomContext(int d, int i)
        {
            m_index = i;
            m_dimension = d;
            init();
        }

        public RandomContext createNew(/*RandomContext rc,*/ int id, int newDims)
        {
            RandomContext newRc = new RandomContext();
            newRc.m_index = this.m_index + id;
            newRc.m_dimension = this.m_dimension + newDims;
            newRc.init();

            return newRc;
        }

        public void init()
        {
            qmcD0I = QMC.rnd(m_dimension, m_index);
            qmcD1I = QMC.rnd(m_dimension + 1, m_index);
        }

        public double getRandom(int j, int dim) {
            switch (dim) {
                case 0:
                    return QMC.mod1(qmcD0I + QMC.rnd(0, j));
                case 1:
                    return QMC.mod1(qmcD1I + QMC.rnd(1, j));
                default:
                    return QMC.mod1(QMC.rnd(m_dimension + dim, m_index) + QMC.rnd(dim, j));
            }
        }

    }
}
