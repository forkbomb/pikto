﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Emgu.CV;
using Emgu.CV.Structure;
using System.Runtime.InteropServices;
using System.Windows.Threading;

namespace Pikto
{
    class Camera
    {
        private Capture _capture;
        public event EventHandler<CameraEventArgs> TimeElapsed;
        private DispatcherTimer timer;
        Image<Bgr, Byte> img;

        public Camera()
        {
            _capture = new Capture();
            timer = new DispatcherTimer();
            timer.Interval = System.TimeSpan.FromMilliseconds(25);
            timer.Tick += new EventHandler(TimerHandler);
            timer.Start();

        }

        private void TimerHandler(object sender, EventArgs e)
        {
            img = _capture.QueryFrame();
            if (TimeElapsed != null)
            {
                TimeElapsed(this, new CameraEventArgs(img));
            }
        }


        [DllImport("gdi32")]
        private static extern int DeleteObject(IntPtr o);

        /// <summary>
        /// Convert an IImage to a WPF BitmapSource. The result can be used in the Set Property of Image.Source
        /// </summary>
        /// <param name="image">The Emgu CV Image</param>
        /// <returns>The equivalent BitmapSource</returns>
        public static BitmapSource ToBitmapSource(IImage image)
        {
            using (System.Drawing.Bitmap source = image.Bitmap)
            {
                IntPtr ptr = source.GetHbitmap(); //obtain the Hbitmap

                BitmapSource bs = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                    ptr,
                    IntPtr.Zero,
                    Int32Rect.Empty,
                    System.Windows.Media.Imaging.BitmapSizeOptions.FromEmptyOptions());

                DeleteObject(ptr); //release the HBitmap
                return bs;
            }
        }
    }
}