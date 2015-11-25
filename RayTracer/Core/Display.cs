/************************************************************
	Copyright (C) 2006-2013 by Hristo Lesev
	hristo.lesev@diadraw.com
	for educational purposes only, not for commercial use
************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading;

namespace RayTracer.Core
{
    public class Display
    {
        public event EventHandler onImageUpdate;
        public Bitmap bitmap;
        private static object lockObject = new object();

        private static AutoResetEvent event_1 = new AutoResetEvent(true);

        public void imageBegin(int w, int h)
        {
            bitmap = new Bitmap(w, h);
        }

        public void imageEnd()
        {

        }

        public void imageUpdate(Rectangle regionRect, FrameBuffer frameBuffer)
        {
            Bitmap bucketBmp = new Bitmap(regionRect.Width, regionRect.Height);
            frameBuffer.toBitmap(bucketBmp);

            lock (lockObject)
            {

                CopyRegionIntoImage(bucketBmp, new Rectangle(0, 0, regionRect.Width, regionRect.Height), ref bitmap, regionRect);

				fireImageUpdateEvent();
            }
            
        }

        public void imagePrepare( Rectangle regionRect, int id )
        {

            lock (lockObject) 
            {

                using (Graphics grD = Graphics.FromImage(bitmap)) 
                {
                    Rectangle tileRect = new Rectangle(regionRect.Location, new Size(regionRect.Width - 1, regionRect.Height - 1));
                    grD.DrawRectangle(new Pen(Color.Black), tileRect);
                    grD.DrawString(id.ToString(), new Font("Arial", 12.0f), new SolidBrush(Color.Black), (float)(regionRect.X + regionRect.Width * 0.5), (float)(regionRect.Y + regionRect.Height * 0.5));
                }
                
				fireImageUpdateEvent();
            }
            
        }

        private void CopyRegionIntoImage(Bitmap srcBitmap, Rectangle srcRegion, ref Bitmap destBitmap, Rectangle destRegion)
        {
            using (Graphics grD = Graphics.FromImage(destBitmap))
            {
                grD.DrawImage(srcBitmap, destRegion, srcRegion, GraphicsUnit.Pixel);
            }
        }

        private void fireImageUpdateEvent()
        {
            if (null != onImageUpdate)
                onImageUpdate(this, new EventArgs());
        }

    }
}
