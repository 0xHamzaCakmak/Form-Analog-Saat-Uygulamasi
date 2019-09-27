using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Form_Analog_Saat
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Timer t = new Timer();
        int Genişlik = 300, Yukseklik = 300, Saniyeibre = 140, Dakikaibre = 110, Saatibre = 80;

        private void timer1_Tick(object sender, EventArgs e)
        {
            g = Graphics.FromImage(bmp);
            int ss = DateTime.Now.Second;
            int mm = DateTime.Now.Minute;
            int hh = DateTime.Now.Hour;

            int[] ibrekoordinat = new int[2];
            g.Clear(Color.White);
            g.DrawEllipse(new Pen(Color.Black, 1F), 0, 0, Yukseklik, Genişlik);

            g.DrawString("12", new Font("Arial", 12), Brushes.Black, new PointF(140, 2));
            g.DrawString("1", new Font("Arial", 12), Brushes.Black, new PointF(210, 20));
            g.DrawString("2", new Font("Arial", 12), Brushes.Black, new PointF(260, 75));
            g.DrawString("3", new Font("Arial", 12), Brushes.Black, new PointF(286, 140));
            g.DrawString("6", new Font("Arial", 12), Brushes.Black, new PointF(142, 282));
            g.DrawString("9", new Font("Arial", 12), Brushes.Black, new PointF(0, 140));

            ibrekoordinat = mscoord(ss, Saniyeibre);
            g.DrawLine(new Pen(Color.Red, 1F), new Point(OrtaX, OrtaY), new Point(ibrekoordinat[0], ibrekoordinat[1]));

            ibrekoordinat = mscoord(mm, Dakikaibre);
            g.DrawLine(new Pen(Color.Orange, 2F), new Point(OrtaX, OrtaY), new Point(ibrekoordinat[0], ibrekoordinat[1]));

            ibrekoordinat = hrcoord(hh % 12,mm, Saatibre);
            g.DrawLine(new Pen(Color.ForestGreen, 3F), new Point(OrtaX, OrtaY), new Point(ibrekoordinat[0], ibrekoordinat[1]));

            pictureBox1.Image = bmp;this.Text = "Saat " + hh + ":" + mm + ":" + ss;
            g.Dispose();
        }
        private int[] mscoord(int saniyedeger, int saataci)
        {
            int[] coord = new int[2];
            saniyedeger *= 6;
            if(saniyedeger>=0&& saniyedeger <= 180)
            {
                coord[0] = OrtaX + (int)(saataci * Math.Sin(Math.PI * saniyedeger / 180));
                coord[1]= OrtaY - (int)(saataci * Math.Cos(Math.PI * saniyedeger / 180));
            }
            else
            {
                coord[0] = OrtaX - (int)(saataci * -Math.Sin(Math.PI * saniyedeger / 180));
                coord[1] = OrtaY - (int)(saataci * Math.Cos(Math.PI * saniyedeger / 180));
            }
            return coord;
        }
        int[] coord = new int[2];
        private int[] hrcoord(int saatdeger,int dakikadeger, int saataci)
        {
            int val = (int)((saatdeger * 30) + (dakikadeger * 0.5));
            if (val >= 0 && val <= 180)
            {
                coord[0] = OrtaX + (int)(saataci * Math.Sin(Math.PI * val / 180));
                coord[1] = OrtaY - (int)(saataci * Math.Cos(Math.PI * val / 180));
            }
            else
            {
                coord[0] = OrtaX - (int)(saataci * -Math.Sin(Math.PI * val / 180));
                coord[1] = OrtaY - (int)(saataci * Math.Sin(Math.PI * val / 180));
            }
            return coord;
        }


        int OrtaX, OrtaY;
        Bitmap bmp;
        Graphics g;
        private void Form1_Load(object sender, EventArgs e)
        {
            bmp = new Bitmap(Genişlik + 1, Yukseklik + 1);
            OrtaX = Genişlik / 2;
            OrtaY = Yukseklik / 2;
            this.BackColor = Color.White;
            t.Interval = 1000;
            t.Tick += new EventHandler(this.timer1_Tick);
            t.Start();
        }
    }
}
