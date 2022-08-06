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
        Panel palya;
        Button kezdesGob;
        public Fkepernyo()
        {
            InitializeComponent();
            palya = PPalya;
            kezdesGob = BKezdes;
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
            if (CBPalyaMerete.SelectedIndex == -1)
            {
                MessageBox.Show("Nem lehet kézzel be irni karaktert");
                meret = 3;
            }
            int xKezdes = 1;
            int yKezdes;
            int width = Convert.ToInt32(PPalya.Width/meret);
            int height = Convert.ToInt32(PPalya.Height/meret);
            //MessageBox.Show(PPalya.Width + " " + PPalya.Height);
            string name;
            ai = new AI(meret, m, mezok, palya,kezdesGob);
            List <Mezo> mezokSeged = new List<Mezo>();
            for (int i = 0; i <meret; i++)
            {
                yKezdes = 1;
                for (int j = 0; j < meret; j++)
                {
                    ai.MezokErteke.Add(0);
                    name = "" + i + "," + j + "";
                    ai.MezokNev.Add(""+name);
                    m = new Mezo(xKezdes, yKezdes, width, height, ai);
                    var mezo = m.Keszit(name);
                    PPalya.Controls.Add(mezo);
                    mezok.Add(mezo);
                    yKezdes += height;
                }
                xKezdes += width;
            }
            int nyereshezSzuksegesJelekSzama = 0;

            if (meret < 4)
            {
                nyereshezSzuksegesJelekSzama = 3;
            }
            else if (meret < 8)
            {
                nyereshezSzuksegesJelekSzama = 4;
            }
            else
            {
                nyereshezSzuksegesJelekSzama = 5;
            }
            MessageBox.Show("a nyeréshaz "+nyereshezSzuksegesJelekSzama+" kör kell");
        }


        private void Fkepernyo_Load(object sender, EventArgs e)
        {
            CBPalyaMerete.SelectedIndex = 0;
        }
    }
}
