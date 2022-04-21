using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace rings_and_x_s
{
    public partial class Fkepernyo : Form
    {
        Mezo m;
        AI ai;
        List<PictureBox> mezok;
        public Fkepernyo()
        {
            InitializeComponent();
        }
        private void BKezdes_Click(object sender, EventArgs e)
        {
            mezok = new List<PictureBox>();
            BKezdes.Visible = false;
            BKezdes.Location = new Point(10, 102);
            BKezdes.Text = "Újra kezdés";
            PKezelo.Controls.Add(BKezdes);
            PPalya.Controls.Clear();
            BKezdes.Visible = true;
            int meret = CBPalyaMerete.SelectedIndex + 3;
            int xKezdes = 1;
            int yKezdes;
            int width = Convert.ToInt32(PPalya.Width/meret);
            int height = Convert.ToInt32(PPalya.Height/meret);
            //MessageBox.Show(PPalya.Width + " " + PPalya.Height);
            string name;
            ai = new AI(meret);
            for (int i = 0; i <meret; i++)
            {
                yKezdes = 1;
                for (int j = 0; j < meret; j++)
                {
                    ai.Mezok.Add(0);
                    name = "" + i + "," + j + "";
                    ai.MezokNev.Add(""+name);
                    /*if (j == 0 && i == 0 || j == 2 && i == 2)
                    {
                        m = new Mezo(xKezdes, yKezdes, width, height, 0);
                        PPalya.Controls.Add(m.Keszit(1));
                        mezok.Add(m.Keszit(1));
                        yKezdes += height;
                    }
                    else if (j == 0 && i == 2 || j == 2 && i == 0)
                    {
                        m = new Mezo(xKezdes, yKezdes, width, height, 0);
                        PPalya.Controls.Add(m.Keszit(2));
                        mezok.Add(m.Keszit(2));
                        yKezdes += height;
                    }
                    else
                    {
                        m = new Mezo(xKezdes, yKezdes, width, height, 0);
                        PPalya.Controls.Add(m.Keszit(0));
                        mezok.Add(m.Keszit(0));
                        yKezdes += height;
                    }*/
                    m = new Mezo(xKezdes, yKezdes, width, height, 0, ai);
                    PPalya.Controls.Add(m.Keszit(0,name));
                    mezok.Add(m.Keszit(0,name));
                    yKezdes += height;
                }
                xKezdes += width;
            }
            /*ai = new AI(mezok, meret);
            int hely = ai.lepes();*/
        }


        

        /*void PBPalyaElemek_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                 MessageBox.Show("Left button clicked
            
        }*/

        /*private void MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                MessageBox.Show("ee");
                foreach (var item in mezok)
                {
                    MessageBox.Show("" + item.Image + "" + item.Location);
                }
            }
        }*/

        

        private void Fkepernyo_Load(object sender, EventArgs e)
        {
            CBPalyaMerete.SelectedIndex = 0;
        }
    }
}
