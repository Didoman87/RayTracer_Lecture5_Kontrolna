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
using RayTracer.Math;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;
using System.Diagnostics;
using Amib.Threading;

namespace RayTracer.Renderers
{
    public class ImageSamplerBuckets : IImageSampler
    {
		private Scene m_scene;
		private Rectangle m_imageRect;
		private IPixelSampler m_pixelSampler;

		public ImageSamplerBuckets(IPixelSampler pixelSampler) 
		{
			m_pixelSampler = pixelSampler;
		}

		public void setScene(Scene scene) {
			m_scene = scene;
		}

        public void startTracing(Display display)
        {
			display.imageBegin(m_scene.globalSettings.imageWidth, m_scene.globalSettings.imageHeight);
            this.startRaytracingThreading( display );
        }
		
        private void startRaytracingThreading(Display display)
        {

			Rectangle imgPlaneRect = new Rectangle(0, 0, m_scene.globalSettings.imageWidth, m_scene.globalSettings.imageHeight);

            int cellWidth = m_scene.globalSettings.bucketWidth;
            int cellHeight = m_scene.globalSettings.bucketHeight;

            List<Rectangle> bucketsRect = new List<Rectangle>();
            List<Bitmap> bucketsBmp = new List<Bitmap>();


            int maxRows = (int)System.Math.Ceiling((float)imgPlaneRect.Height / (float)cellHeight);
            int maxCol = (int)System.Math.Ceiling((float)imgPlaneRect.Width / (float)cellWidth);

            if ((maxRows % 2) > 0)
                maxRows++;

            if ((maxCol % 2) > 0)
                maxCol++;

			ManualResetEvent[] doneEvents = new ManualResetEvent[maxRows*maxCol];
			int bucketID = 0;

			// Create an instance of the Smart Thread Pool
			SmartThreadPool smartThreadPool = new SmartThreadPool();
			smartThreadPool.MaxThreads = m_scene.globalSettings.maxTreadsCount;
			
			Rectangle[] buckets;
			IBucketOrder bucketOrderer = new RowBucketOrder();
			bucketOrderer.getBucketSequence(cellWidth, cellHeight, maxCol, maxRows, out buckets);

			foreach (var bucketRect in buckets) {
                doneEvents[bucketID] = new ManualResetEvent(false);
				BucketWorker f = new BucketWorker(imgPlaneRect, bucketRect, display, m_pixelSampler, m_scene, doneEvents[bucketID]);
				smartThreadPool.QueueWorkItem(new WorkItemCallback(f.ThreadPoolCallback), bucketID);
				bucketID++;
			}


			smartThreadPool.WaitForIdle();
        }
    }


	public class BucketWorker {

        public BucketWorker(Rectangle imgPlaneRect, Rectangle bucketRect, Display display, IPixelSampler pixelSampler, Scene scene, ManualResetEvent doneEvent)
        {
			_doneEvent = doneEvent;

			m_pixelSampler = pixelSampler;
			m_scene = scene;
			m_imgPlaneRect = imgPlaneRect;
			m_bucketRect = bucketRect;
			m_bucketRect.Intersect(imgPlaneRect);
            m_display = display;
		}

        int m_threadIndex;

		// Wrapper method for use with thread pool.
		public object ThreadPoolCallback(Object threadContext) {
            m_threadIndex = (int)threadContext;
			renderBucketThread(m_imgPlaneRect, m_bucketRect);
			_doneEvent.Set();

			return null;
		}

		public void renderBucketThread(Rectangle imgPlaneRect, Rectangle bucketRect) {
            m_display.imagePrepare(bucketRect, m_threadIndex);

			FrameBuffer frameBuffer = startSamplingRegion(bucketRect);

            m_display.imageUpdate(bucketRect, frameBuffer);
		}
       
		public FrameBuffer startSamplingRegion(Rectangle region) {
			m_pixelSampler.setParams(m_imgPlaneRect, m_scene);

			FrameBuffer frameBuffer = new FrameBuffer(region.Width, region.Height);

			Camera camera = m_scene.getCamera();

			for (int y = 0; y < region.Height; ++y) {
				for (int x = 0; x < region.Width; ++x) {
                    ImageSample sample = new ImageSample();
                    sample.x = x;
                    sample.y = y;
                    sample.color = m_pixelSampler.samplePixel(x, y, region, camera);
                    frameBuffer.addSample(x, y, sample);
				}//x
			}//y

			return frameBuffer;
		}

		private IPixelSampler m_pixelSampler;
		private Scene m_scene;
		private Rectangle m_imgPlaneRect; 
		private	Rectangle m_bucketRect; 
        private Display m_display;

		private ManualResetEvent _doneEvent;
	}

	

	public class RowBucketOrder : IBucketOrder {

		public void getBucketSequence(int w, int h, int nbw, int nbh, out Rectangle[] buckets) {
			buckets = new Rectangle[nbw * nbh];
			for (int i = 0; i < nbw * nbh; i++) {
				int by = i / nbw;
				int bx = i % nbw;
				if ((by & 1) == 1)
					bx = nbw - 1 - bx;
				buckets[i].X = bx * w;
				buckets[i].Y = by * h;
				buckets[i].Width = w;
				buckets[i].Height = h;
			}
		}

	}

	public class ColumnBucketOrder : IBucketOrder {
		
		public void getBucketSequence(int w, int h, int nbw, int nbh, out Rectangle[] buckets) 
		{
			buckets = new Rectangle[nbw * nbh];
			for (int i = 0; i < nbw * nbh; i++) {
				int bx = i / nbh;
				int by = i % nbh;
				if ((bx & 1) == 1)
					by = nbh - 1 - by;
				buckets[i].X = bx * w;
				buckets[i].Y = by * h;
				buckets[i].Width = w;
				buckets[i].Height = h;
			}
		}

	}

	public class SpiralBucketOrder : IBucketOrder {

		public void getBucketSequence(int w, int h, int nbw, int nbh, out Rectangle[] buckets) 
		{
			buckets = new Rectangle[nbw * nbh];

			for (int i = 0; i < nbw * nbh; i++) {
				int bx, by;
				int center = (System.Math.Min(nbw, nbh) - 1) / 2;
				int nx = nbw;
				int ny = nbh;
				while (i < (nx * ny)) {
					nx--;
					ny--;
				}
				int nxny = nx * ny;
				int minnxny = System.Math.Min(nx, ny);
				if ((minnxny & 1) == 1) {
					if (i <= (nxny + ny)) {
						bx = nx - minnxny / 2;
						by = -minnxny / 2 + i - nxny;
					}
					else {
						bx = nx - minnxny / 2 - (i - (nxny + ny));
						by = ny - minnxny / 2;
					}
				}
				else {
					if (i <= (nxny + ny)) {
						bx = -minnxny / 2;
						by = ny - minnxny / 2 - (i - nxny);
					}
					else {
						bx = -minnxny / 2 + (i - (nxny + ny));
						by = -minnxny / 2;
					}
				}
				buckets[i].X = (bx + center) * w;
				buckets[i].Y = (by + center) * h;
				buckets[i].Width = w;
				buckets[i].Height = h;
			}

		}
	}


}
