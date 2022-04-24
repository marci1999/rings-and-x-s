using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
    
namespace rings_and_x_s
{
    class Mezo : PictureBox
    {
        AI ai;
        public PictureBox PBPalyaElemek;

        public Mezo(int x, int y, int width, int height, AI ai)
        {
            this.ai = ai;
            X = x;
            Y = y;
            OWidth = width;
            OHeight = height;
        }

        
        public int X { get; set; }
        public int Y { get; set; }
        public int OWidth { get; set; }
        public int OHeight { get; set; }
        public int Kep { get; set; }

        public PictureBox Keszit(int kep, string nev)
        {
            PBPalyaElemek = new PictureBox();
            PBPalyaElemek.Name = nev;
            PBPalyaElemek.Width = this.OWidth;
            PBPalyaElemek.Height = this.OHeight;
            PBPalyaElemek.Location = new Point(this.X, this.Y);
            var greenCircle = new Bitmap(rings_and_x_s.Properties.Resources.green_circle);
            var redX = new Bitmap(rings_and_x_s.Properties.Resources.red_x);
            if (kep == 1)
            {

                PBPalyaElemek.Image = greenCircle;
            }
            else if (kep == 2)
            {
                PBPalyaElemek.Image = redX;
            }
            PBPalyaElemek.SizeMode = PictureBoxSizeMode.Zoom;
            PBPalyaElemek.MouseClick += new MouseEventHandler(PBPalyaElemek_MouseClick);
            PBPalyaElemek.BorderStyle = BorderStyle.Fixed3D;
            Controls.Add(PBPalyaElemek);
            return PBPalyaElemek;
        }

        //public PictureBox

        /*var greenCircle = new Bitmap(rings_and_x_s.Properties.Resources.green_circle);
        var redX = new Bitmap(rings_and_x_s.Properties.Resources.red_x);
        if (this.Kep == 1)
        {
            PBPalyaElemek.Image = greenCircle;
        }
        else if (this.Kep == 2)
        {
            PBPalyaElemek.Image = redX;
        }*/

        public void PBPalyaElemek_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
               string helyNeve = ai.joHely(PBPalyaElemek.Name, PBPalyaElemek.Image);
                if (helyNeve != null)
                {
                    ai.ujraRajzol(helyNeve, 1);
                }
                /*var greenCircle = new Bitmap(rings_and_x_s.Properties.Resources.green_circle);
                PBPalyaElemek.Image = greenCircle;*/
                //PBPalyaElemek.ImageLocation = (0,0);
                //PBPalyaElemek.Refresh();
                //PBPalyaElemek.Visible = true;
            }
            //MessageBox.Show(""+this.Location+" ; "+this.Image);
        }

        //public MouseEventHandler PBPalyaElemek_MouseClick { get; }
    }
}

