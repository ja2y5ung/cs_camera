using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace camera_test
{
    public partial class Form1 : Form
    {
        private VideoCapture video;
        private Thread thread;
        private Bitmap bitmap;
        private bool videothread = true;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            video = new VideoCapture(0);
            thread = new Thread(new ThreadStart(videoThread));
            thread.Start();

        }

        private void videoThread()
        {
            Mat mat = new Mat();
            while(videothread)
            {
                video.Read(mat);

                if (bitmap != null)
                    bitmap.Dispose();
                bitmap = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(mat);
                pictureBox1.Image = bitmap;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            videothread = false;
            thread.Abort();
        }
    }
}

//1.프로세스메모리가 증가되는현상
//- 비트맵 리소스를 생성하고 해제하지 않았다
//2. 프로그램이 죽지않은 현상은
//- thread가 종료되지 않았기 때문이다.