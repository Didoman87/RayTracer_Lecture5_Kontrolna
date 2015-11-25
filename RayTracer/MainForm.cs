/************************************************************
	Copyright (C) 2006-2013 by Hristo Lesev
	hristo.lesev@diadraw.com
	for educational purposes only, not for commercial use
************************************************************/
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using RayTracer.Math;
using RayTracer.Core;
using System.Diagnostics;
using Amib.Threading;

namespace RayTracer
{

	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form {

		TextBoxTraceListener m_textBoxListener;

        private Display m_display;
		
		public MainForm() {
			InitializeComponent();
		}
		
		void RenderButtonClick(object sender, EventArgs e) 
		{
			OpenFileDialog openDialog = new OpenFileDialog();
			openDialog.DefaultExt = "scene";
			openDialog.Filter = "Scene files (*.scene)|*.scene|" + "All files|*.*";

			if (openDialog.ShowDialog() == DialogResult.OK) 
			{
				m_display = new Display();
				m_display.onImageUpdate += onImageUpdateHandler;

				SmartThreadPool smartThreadPool = new SmartThreadPool();

				RaytracerMainThread renderThread = new RaytracerMainThread(openDialog.FileName, m_display);
				smartThreadPool.QueueWorkItem(new WorkItemCallback(renderThread.ThreadPoolCallback));	
			}
		}

        void onImageUpdateHandler(object sender, EventArgs e)
        {
			GraphicsUnit units = GraphicsUnit.Pixel;
			RectangleF cloneRect = m_display.bitmap.GetBounds(ref units);
			System.Drawing.Imaging.PixelFormat format = m_display.bitmap.PixelFormat;
			Bitmap screenBitmap = m_display.bitmap.Clone(cloneRect, format);

			if (pictureBox.InvokeRequired) {
				pictureBox.Invoke(new MethodInvoker(
				delegate() {
					pictureBox.Image = screenBitmap;
				}));
			}
			else {
				pictureBox.Image = screenBitmap;
			}
        }

		private void MainForm_Load(object sender, EventArgs e) {
			m_textBoxListener = new TextBoxTraceListener(textBox1);
			Trace.Listeners.Add(m_textBoxListener);
		}

		private void saveImageButton_Click(object sender, EventArgs e) 
		{
			if (null == pictureBox.Image) {
				MessageBox.Show("You need to render something first!", "RayTracer says...", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}

			SaveFileDialog saveDialog = new SaveFileDialog();
			saveDialog.AddExtension = true;
			saveDialog.DefaultExt = "png";
			saveDialog.Filter = "PNG files (*.png)|*.png|" + "All files|*.*";
			if (saveDialog.ShowDialog() == DialogResult.OK) {
				pictureBox.Image.Save(saveDialog.FileName);
			}
		}
	}

	public class TextBoxTraceListener : TraceListener {
		private TextBox _target;
		private StringSendDelegate _invokeWrite;

		public TextBoxTraceListener(TextBox target) {
			_target = target;
			_invokeWrite = new StringSendDelegate(SendString);
		}

		public override void Write(string message) {
			_target.Invoke(_invokeWrite, new object[] { message });
		}

		public override void WriteLine(string message) {
			_target.Invoke(_invokeWrite, new object[] { message + Environment.NewLine });
		}

		private delegate void StringSendDelegate(string message);

		private void SendString(string message) {
			// No need to lock text box as this function will only 
			// ever be executed from the UI thread
			_target.Text += message;
		}
	}

	public class RaytracerMainThread {

		public RaytracerMainThread(string sceneFileName, Display display) {
			m_sceneFileName = sceneFileName;
			m_display = display;
		}

		private string m_sceneFileName;
		private Display m_display;

		public object ThreadPoolCallback(Object threadContext) {

			
			
			Debug.WriteLine("Loading scene... ");

			Stopwatch stopwatch = Stopwatch.StartNew();
			Raytracer raytracer = new Raytracer();
			raytracer.prepareScene(m_sceneFileName);
			stopwatch.Stop();

			Debug.WriteLine("Finished: loading scene [" + stopwatch.Elapsed.ToString()+"]");

			Debug.WriteLine("Start: rendering");

			stopwatch = Stopwatch.StartNew();
			raytracer.startRaytracing(m_display);



			stopwatch.Stop();

			Debug.WriteLine("Finished: rendering ["+ stopwatch.Elapsed.ToString()+"]");

			

			return null;
		}
	}
}
