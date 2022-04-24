using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace rings_and_x_s
{
    class AI
    {
        Mezo m;
        List<int> mezokErteke;
        List<string> mezokNev;
        List<PictureBox> mezok;
        int palyaMerete;
        Panel palya;
        public AI(int meret, Mezo m, List<PictureBox> mezoAdatok, Panel PPalya)
        {
            this.palyaMerete = meret;
            mezokErteke = new List<int>();
            MezokNev = new List<string>();
            this.Mezok = new List<PictureBox>();
            this.Mezok = mezoAdatok;
            palya = PPalya;
        }
        public List<int> MezokErteke { get => mezokErteke; set => mezokErteke = value; }
        public List<string> MezokNev { get => mezokNev; set => mezokNev = value; }
        public List<PictureBox> Mezok { get => mezok; set => mezok = value; }

        public string joHely(string nev,Image kep)
        {
            string[] seged;

            seged = nev.Split(',');

            int x = Convert.ToInt32(seged[0]);
            int y = Convert.ToInt32(seged[1]);

            if (kep == null)
            {
                var greenCircle = new Bitmap(rings_and_x_s.Properties.Resources.green_circle);
                kep = greenCircle;
                int xSeged = x *palyaMerete;

                int hely = y + xSeged;
                //MessageBox.Show("" + mezokNev[hely]);
                
                if (mezokErteke[hely] == 0)
                {
                    
                    mezokErteke[hely] = 1;
                    return mezokNev[hely];
                }
            }
            return null;
        }

        public void ujraRajzol(string nev, int kep)
        {
            palya.Controls.Clear();
            foreach (var item in mezok)
            {
                if (item.Name == nev)
	            {
                    var greenCircle = new Bitmap(rings_and_x_s.Properties.Resources.green_circle);
                    var redX = new Bitmap(rings_and_x_s.Properties.Resources.red_x);
                    if (kep == 1)
                    {
                        item.Image = greenCircle;
                    }
                    else if (kep == 2)
                    {
                        item.Image = redX;
                    }
	            }
                palya.Controls.Add(item);
            }

        }
    }
}
        /*List<PictureBox> mezok;
        int palyMerete;

        public AI(List<PictureBox> mezok, int palyMerete)
        {
            this.mezok = mezok;
            this.palyMerete = palyMerete;
        }

        public List<PictureBox> Mezok { get => mezok; set => mezok = value; }
        public int PalyMerete { get => palyMerete; set => palyMerete = value; }

        public int lepes()
        {
            int hely = -1;
            var palya = new PictureBox[palyMerete, palyMerete];
            int listaElemSzamlalo = 0;
            int[,] kiertekeles = new int[palyMerete, palyMerete];
            for (int i = 0; i < palyMerete; i++)
            {
                for (int j = 0; j < palyMerete; j++)
                {
                    palya[i, j] = mezok[listaElemSzamlalo];

                    listaElemSzamlalo++;
                    kiertekeles[i, j] = -5;
                }
            }
            for (int i = 0; i < palyMerete; i++)
            {
                for (int j = 0; j < palyMerete; j++)
                {
                    if (palya[i, j].Image == null)
                    {
                        bool jobbSzomszed = false;
                        bool alsoSzomszed = false;
                        bool balSzomszed = false;
                        bool felsoSzomszed = false;
                        if (j > 0)
                        {
                            balSzomszed = palya[i, j - 1].Image == null;
                        }

                        if (i > 0)
                        {
                            alsoSzomszed = palya[i - 1, j].Image == null;
                        }
                        if (j < palyMerete-1)
                        {
                            jobbSzomszed = palya[i, j + 1].Image == null;
                        }
                        if (i < palyMerete-1)
                        {
                            felsoSzomszed = palya[i + 1, j].Image == null;
                        }

                        if (i-1 < 0 || j - 1 < 0 || i  > palyMerete-2 || j > palyMerete-2)
                        {
                            bool iKissebb = i - 1 < 0;
                            bool jKissebb = j - 1 < 0;
                            bool iNagyobb = i + 2 > palyMerete;
                            bool jNagyobb = j + 2 > palyMerete;
                        /*MessageBox.Show(" " + i + " " + j + "" + "\n" +
                                "i - 1 < 0" + ": " + iKissebb + "\n" +
                                "j - 1 < 0" + ": " + jKissebb + "\n" +
                                "i + 1 > palyMeret" + ": " + iNagyobb + "\n" +
                                "j + 1 > palyMerete" + ": " + jNagyobb); */
                        /*if (iKissebb && jKissebb)
                        {
                            if (felsoSzomszed && jobbSzomszed)
                            {
                                kiertekeles[i, j] = 0;
                            }
                        } 
                        else if (iKissebb && jNagyobb)
                        {
                            if (felsoSzomszed && balSzomszed)
                            {
                                kiertekeles[i, j] = 0;
                            }
                        }
                        else if (iNagyobb && jKissebb)
                        {
                            if (alsoSzomszed && jobbSzomszed)
                            {
                                kiertekeles[i, j] = 0;
                            }
                        }
                        else if (iNagyobb && jNagyobb)
                        {
                            if (alsoSzomszed && balSzomszed)
                            {
                                kiertekeles[i, j] = 0;
                            }
                        }
                        else if (iNagyobb)
                        {
                            if (jobbSzomszed && alsoSzomszed && balSzomszed)
                            {
                                kiertekeles[i, j] = 0;
                            }
                        }
                        else if (iKissebb)
                        {
                            if (jobbSzomszed && felsoSzomszed && balSzomszed)
                            {
                                kiertekeles[i, j] = 0;
                            }
                        }
                        else if (jKissebb)
                        {
                            if (alsoSzomszed && jobbSzomszed && felsoSzomszed)
                            {
                                kiertekeles[i, j] = 0;
                            }
                        }
                        else if (jNagyobb)
                        {
                            if (alsoSzomszed && balSzomszed && felsoSzomszed)
                            {
                                kiertekeles[i, j] = 0;
                            }
                        }
                        else
                        {
                            if (alsoSzomszed && felsoSzomszed && balSzomszed && jobbSzomszed)
                            {
                                kiertekeles[i, j] = 0;
                            }
                        }
                    }
                    else
                    {
                    var greenCircle = new Bitmap(rings_and_x_s.Properties.Resources.green_circle);
                    var redX = new Bitmap(rings_and_x_s.Properties.Resources.red_x);
                    var array1 = new Byte[0];
                    Array.Clear(array1, 0, array1.Length);
                    array1 = ImageToByteArray(palya[i, j].Image);
                    var array2 = ImageToByteArray(Properties.Resources.red_x);
                    var array3 = ImageToByteArray(Properties.Resources.green_circle);


                    bool isSame = array1.Length == array2.Length;

                    if (isSame)
                    for (int index = 0; index < array1.Length; index++)
                        if (array1[index] != array3[index])
                        {

                            isSame = false;
                            MessageBox.Show("" + index);
                            //break;
                        }

                        if (isSame)
                        {
                            palya[i, j].Image = greenCircle;
                            kiertekeles[i, j] = -2;
                        }
                        else
                        {
                            palya[i, j].Image = redX;
                            kiertekeles[i, j] = -1;
                        }
                    }
                }
            }

            for (int i = 0; i < palyMerete; i++)
            {
                for (int j = 0; j < palyMerete; j++)
                {
                    MessageBox.Show(" " + kiertekeles[i, j] + "; "+i+" "+j);
                }
             }
             for (int i = 0; i < palyMerete*2; i++)
             {
                for (int j = 0; j < palyMerete*2; j++)
                {
                    MessageBox.Show(" "+kiertekeles[i, j]+"; ");
                    if (palyMerete == j)
                    {
                        MessageBox.Show(" " + palya[i, j] + "; ");
                    }   
                }
            }
            return hely;
        }

        public byte[] ImageToByteArray(System.Drawing.Image imageIn)
        {
            ImageConverter _imageConverter = new ImageConverter();
            byte[] xByte = (byte[])_imageConverter.ConvertTo(imageIn, typeof(byte[]));
            return xByte;
        }*/
