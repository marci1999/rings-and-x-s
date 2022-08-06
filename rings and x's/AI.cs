using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace rings_and_x_s
{
    class AI
    {
        // -4 == zőld kör
        // -5 == piros x
        Mezo m;
        List<int> mezokErteke;
        List<string> mezokNev;
        List<PictureBox> mezok;
        int[,] kiErtekeles;
        int palyaMerete;
        Panel palya;
        Button kezdesGob;
        private int hely = -1;
        int xAKozelben = 0;
        int korAKozelben = 0;


        public AI(int meret, Mezo m, List<PictureBox> mezoAdatok, Panel PPalya, Button BKezdes)
        {
            this.palyaMerete = meret;
            mezokErteke = new List<int>();
            MezokNev = new List<string>();
            this.Mezok = new List<PictureBox>();
            this.Mezok = mezoAdatok;
            kiErtekeles = new int[palyaMerete, palyaMerete];
            palya = PPalya;
            kezdesGob = BKezdes;
        }
        public List<int> MezokErteke { get => mezokErteke; set => mezokErteke = value; }
        public List<string> MezokNev { get => mezokNev; set => mezokNev = value; }
        public List<PictureBox> Mezok { get => mezok; set => mezok = value; }

        public string joHely(string nev, Image kep)
        {
            string[] seged;

            seged = nev.Split(',');

            int x = Convert.ToInt32(seged[0]);
            int y = Convert.ToInt32(seged[1]);

            if (kep == null)
            {
                int xSeged = x * palyaMerete;

                int hely = y + xSeged;
                //MessageBox.Show("" + mezokNev[hely]);

                if (mezokErteke[hely] == 0)
                {
                    var greenCircle = new Bitmap(rings_and_x_s.Properties.Resources.green_circle);
                    kep = greenCircle;
                    mezokErteke[hely] = -4;
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
                    if (kep == 1)
                    {
                        var greenCircle = new Bitmap(rings_and_x_s.Properties.Resources.green_circle);
                        item.Image = greenCircle;
                    }
                    else if (kep == 2)
                    {
                        var redX = new Bitmap(rings_and_x_s.Properties.Resources.red_x);
                    item.Image = redX;
                    }
                }
                palya.Controls.Add(item);
            }

        }

        public void aIValaszol()
        {

            bool teleVan = true;
            for (int i = 0; i < palyaMerete; i++)
            {
                for (int j = 0; j < palyaMerete; j++)
                {
                    int iSeged = i * palyaMerete;

                    hely = j + iSeged;

                    kiErtekeles[i, j] = mezokErteke[hely];
                }
            }
            int kitEllenorzok = -4;
            if (nyertValaki(kitEllenorzok))
            {
                jatekVege(kitEllenorzok);
                return;
            }
            kitEllenorzok = -5;
            for (int i = 0; i < palyaMerete; i++)
            {
                for (int j = 0; j < palyaMerete; j++)
                {
                    if (kiErtekeles[i,j] == 0)
                    {
                        szomszedokEllenorzese(szomsedosMezok(i, j), i, j, -4);
                        if (hely == -1)
                        {
                            int iSeged = i * palyaMerete;

                            hely = j + iSeged;
                            if (mezokErteke[hely] == 0)
                            {
                                mezokErteke[hely] = -5;
                                //red x
                                ujraRajzol(mezokNev[hely], 2);
                                kiErtekeles[i, j] = -5;
                                if (nyertValaki(kitEllenorzok))
                                {
                                    jatekVege(kitEllenorzok);
                                    return;
                                }
                                if (dontetlen())
                                {
                                    MessageBox.Show("az állás dontetlen");
                                    mezokErteke.Clear();
                                    palya.Controls.Clear();
                                }
                                return;
                            }
                        }
                        szomszedokEllenorzese(szomsedosMezok(i, j), i, j, -5);
                        if (hely == -1)
                        {
                            int iSeged = i * palyaMerete;

                            hely = j + iSeged;
                            if (mezokErteke[hely] == 0)
                            {
                                mezokErteke[hely] = -5;
                                //green circle
                                ujraRajzol(mezokNev[hely], 2);
                                kiErtekeles[i, j] = -5;
                                if (nyertValaki(kitEllenorzok))
                                {
                                    jatekVege(kitEllenorzok);
                                    return;
                                }
                                if (dontetlen())
                                {
                                    MessageBox.Show("az állás dontetlen");
                                    mezokErteke.Clear();
                                    palya.Controls.Clear();
                                }
                                return;
                            }
                        }
                        if (korAKozelben > xAKozelben)
                        {
                            kiErtekeles[i, j] = korAKozelben;
                        }
                        else
                        {
                            kiErtekeles[i, j] = xAKozelben;
                        }
                        xAKozelben = 0;
                        korAKozelben = 0;
                    }
                }
            }
            if (dontetlen())
            {
                MessageBox.Show("az állás dontetlen");
                mezokErteke.Clear();
                palya.Controls.Clear();
                return;
            }
            string xHelye = xLerakasa();
            string[] seged;

            seged = xHelye.Split(',');

            int x = Convert.ToInt32(seged[0]);
            int y = Convert.ToInt32(seged[1]);
            kiErtekeles[x, y] = -5;
            int xSeged = x * palyaMerete;
            hely = y + xSeged;
            mezokErteke[hely] = -5;
            ujraRajzol(mezokNev[hely], 2);
            if (nyertValaki(kitEllenorzok))
            {
                jatekVege(kitEllenorzok);
                return;
            }
            if(dontetlen())
            {
                MessageBox.Show("az állás dontetlen");
                mezokErteke.Clear();
                palya.Controls.Clear();
            }
        }
        private bool dontetlen()
        {
            bool teleVan = true;
            for (int i = 0; i < mezokErteke.Count;i++)
            {
                if (mezokErteke[i] == 0)
                {
                    teleVan = false;
                }
            }
            return teleVan;
        }
        private string xLerakasa()
        {
            int x = -1;
            int y = -1;
            int kiválasztottÉrték = -1;
            do
            {
                int hasonélitasiAlap = -1;
                for (int i = 0; i < palyaMerete; i++)
                {
                    for (int j = 0; j < palyaMerete; j++)
                    {
                        if (kiErtekeles[i, j] != 0)
                        {
                            if (kiErtekeles[i, j] > hasonélitasiAlap)
                            {
                                x = i;
                                y = j;
                                hasonélitasiAlap = kiErtekeles[i, j];
                            }
                            else if (kiErtekeles[i, j] == hasonélitasiAlap)
                            {
                                Random r = new Random();
                                int yesOrNo = r.Next(0, 2);
                                if (yesOrNo == 1)
                                {
                                    x = i;
                                    y = j;
                                }
                                hasonélitasiAlap = kiErtekeles[i, j];
                            }
                        }
                    }
                }
                int xSeged = x * palyaMerete;

                hely = y + xSeged;
                kiválasztottÉrték = mezokErteke[hely];
            } while (mezokErteke[hely] != 0);
            return ("" + x + "," + y + "");
        }
        private void mezoVizsgalata(int iVector, int jVector, int i,int j, int nyeresEllenorzo)
        {
            bool iMinus = iVector < 0;
            bool JMinus = jVector < 0;
            bool iPlus = iVector > 0;
            bool jPlus = jVector > 0;
            kiErtekeles[i, j] = nyeresEllenorzo;
            int hanyJel = 0;
            if (palyaMerete > 4)
            {
                if (nyertValaki(nyeresEllenorzo))
                {
                    if (nyeresEllenorzo == 4)
                    {
                        kiErtekeles[i, j] = -5;
                        //red x
                    }
                    else
                    {
                        kiErtekeles[i, j] = -4;
                        //green circle
                    }
                    
                    if (!((i == 0 || i == (palyaMerete - 1)) || (j == 0 || j == (palyaMerete - 1))))
                    {
                        if (nyertValaki(-1))
                        {
                            hely = -1;
                            kiErtekeles[i, j] = 10;
                            return;
                        }
                    }
                }
                else
                {
                    if (!((palyaMerete < 8 && (i + (iVector * 2) < 0 || j + (jVector * 2) < 0 || i + (iVector * 2) > palyaMerete-1 || j + (jVector * 2) > palyaMerete-1)) || 
                        (palyaMerete >= 8 && (i + (iVector * 3) < 0 || j + (jVector * 3) < 0 || i + (iVector * 3) > palyaMerete-1|| j + (jVector * 3) > palyaMerete-1)) ||
                                            (i + (iVector * -1) < 0 || j + (jVector * -1) < 0 || i + (iVector * -1) > palyaMerete - 1 || j + (jVector * -1) > palyaMerete - 1)))
                    {
                        if (palyaMerete < 8 && (kiErtekeles[i + (iVector * 2), j + (jVector * 2)] == nyeresEllenorzo))
                        {
                            hely = -1;
                            kiErtekeles[i, j] = 10;
                            return;
                        }
                        else if (palyaMerete >= 8 && (kiErtekeles[i + (iVector * 2), j + (jVector * 2)] == nyeresEllenorzo) && (kiErtekeles[i + (iVector * 3), j + (jVector * 3)] == nyeresEllenorzo))
                        {
                            hely = -1;
                            kiErtekeles[i, j] = 10;
                            return;
                        }
                        else if (palyaMerete < 8 && (kiErtekeles[i + (iVector * -1), j + (jVector * -1)] == nyeresEllenorzo))
                        {
                            hely = -1;
                            kiErtekeles[i, j] = 10;
                            return;
                        }
                        else if (palyaMerete >= 8 && (kiErtekeles[i + (iVector * -1), j + (jVector * -1)] == nyeresEllenorzo) && (kiErtekeles[i + (iVector * 2), j + (jVector * 2)] == -nyeresEllenorzo))
                        {
                            hely = -1;
                            kiErtekeles[i, j] = 10;
                            return;
                        }
                    }
                }
            }
            else if (palyaMerete <= 4)
            {
                if (nyertValaki(nyeresEllenorzo))
                {
                    hely = -1;
                    kiErtekeles[i, j] = 10;
                    return;
                }
            }
            if (nyeresEllenorzo == -4)
            {
                korAKozelben++;
            }
            else
            {
                xAKozelben++;
            }
            kiErtekeles[i, j] = 0;
                /*
                 try 
                        {	      
                            
                            

                        }   
                        catch (Exception)
                        {
                            MessageBox.Show ("ok müködik");
                        }
                        if ((i == 0 || i == (palyaMerete - 1)) || (j == 0 || j == (palyaMerete - 1)))
                        {
                            if (palyaMerete < 8)
                            {
                               // kiInduloJ * -1
                            }
                            else
                            {

                            }
                        }
                    
                 */
                /*
                vagy le kell ellneorizni hogy a  nyeres iranzaban meg egy jelet lehelyezve nyer e a jatekos.
                i: le kell helyezni az elso ideiglenes jel helyere egy x-et.
                n: megjeloles hogy szomszedos egy korre, x - el es eltavolitjuk az ideiglenes jeleket.*/
        }
        private void szomszedokEllenorzese(string szomszedok,int i, int j, int nyeresEllenorzo)
        {
            string[] seged = szomszedok.Split('@');
            string[] iplusJplus = seged[0].Split(';');
            string[] iMinusJMinus = seged[1].Split(';');
            int iPlus = 0;
            int jplus = 0;
            int iMinus = 0;
            int jMinus = 0;
            if (iplusJplus[0] == "+1")
            {
                iPlus = 1;
            }
            if (iplusJplus[1] == "+1")
            {
                jplus = 1;
            }
            if (iMinusJMinus[0] == "-1")
            {
                iMinus = -1;
            }
            if (iMinusJMinus[1] == "-1")
            {
                jMinus = -1;
            }



            if (iPlus != 0 && jplus != 0)
            {
                if (kiErtekeles[i + 1, j + 1] == nyeresEllenorzo)
                {
                    mezoVizsgalata(+ 1, + 1, i, j, nyeresEllenorzo);
                }
            }
            if (iMinus != 0 && jMinus != 0)
            {
                if (kiErtekeles[i - 1, j - 1] == nyeresEllenorzo)
                {
                    mezoVizsgalata(- 1, - 1, i, j, nyeresEllenorzo);
                }
            }
            if (iMinus != 0 && jplus != 0)
            {
                if (kiErtekeles[i - 1, j + 1] == nyeresEllenorzo)
                {
                    mezoVizsgalata(- 1, + 1,i,j, nyeresEllenorzo);
                }
            }
            if (iPlus != 0 && jMinus != 0)
            {
                if (kiErtekeles[i + 1, j - 1] == nyeresEllenorzo)
                {
                    mezoVizsgalata(+ 1, - 1, i, j, nyeresEllenorzo);
                }
            }
            if (iPlus != 0)
            {
                if (kiErtekeles[i + 1,j] == nyeresEllenorzo)
                {
                    mezoVizsgalata(+ 1, 0, i, j, nyeresEllenorzo);
                }
            }
            if (iMinus != 0)
            {
                 if (kiErtekeles[i - 1, j] == nyeresEllenorzo)
                {
                    mezoVizsgalata(- 1, 0, i, j, nyeresEllenorzo);
                }
            }
            if (jplus != 0)
            {
                if (kiErtekeles[i, j + 1] == nyeresEllenorzo)
                {
                    mezoVizsgalata(0, + 1, i, j, nyeresEllenorzo);
                }
            }
            if (jMinus != 0)
            {
                if (kiErtekeles[i, j - 1] == nyeresEllenorzo)
                {

                    mezoVizsgalata(0, - 1, i, j, nyeresEllenorzo);
                }
            }
        }
        private string szomsedosMezok(int i, int j)
        {
            string szomszedok = "";
            bool iKissebb = i - 1 < 0;
            bool jKissebb = j - 1 < 0;
            bool iNagyobb = i + 2 > palyaMerete;
            bool jNagyobb = j + 2 > palyaMerete;
            if (iKissebb && jKissebb)
            {
                szomszedok += "+1;+1@0;0";
            }
            else if (jNagyobb && iKissebb)
            {
                szomszedok += "+1;0@0;-1";
            }
            else if (iNagyobb && jNagyobb)
            {
                szomszedok += "0;0@-1;-1";
            }
            else if (jKissebb && iNagyobb)
            {
                szomszedok += "0;+1@-1;0";
            }
            else if (iNagyobb)
            {
                szomszedok += "0;+1@-1;-1";
            }
            else if (jNagyobb)
            {
                szomszedok += "+1;0@-1;-1";
            }
            else if (iKissebb)
            {
                szomszedok += "+1;+1@0;-1";
            }
            else if (jKissebb)
            {
                szomszedok += "+1;+1@-1;0";
            }
            else
            {
                szomszedok += "+1;+1@-1;-1";
            }
            return szomszedok;
        }
        private bool nyertValaki(int kitEllenorzok)
        {
            int nyereshezSzuksegesJelekSzama = 0;

            if (palyaMerete < 4)
            {
                nyereshezSzuksegesJelekSzama = 3;
            }
            else if (palyaMerete < 8)
            {
                nyereshezSzuksegesJelekSzama = 4;
            }
            else
            {
                nyereshezSzuksegesJelekSzama = 5;
            }

            for (int i = 0; i < palyaMerete; i++)
            {
                for (int j = 0; j < palyaMerete; j++)
                {
                    if (kitEllenorzok == -1)
                    {
                        if (i + nyereshezSzuksegesJelekSzama <= palyaMerete)
                        {
                            if (kiErtekeles[i + nyereshezSzuksegesJelekSzama, j] == (palyaMerete - 1))
                            {
                                return true;
                            }
                        }
                        if (i - (nyereshezSzuksegesJelekSzama - 1) >= 0)
                        {
                            if (kiErtekeles[i - nyereshezSzuksegesJelekSzama, j] == 0)
                            {
                                return true;
                            }
                        }
                        if (j + nyereshezSzuksegesJelekSzama <= palyaMerete)
                        {
                            if (kiErtekeles[i, j + nyereshezSzuksegesJelekSzama] == (palyaMerete - 1))
                            {
                                return true;
                            }
                        }
                        if (j - (nyereshezSzuksegesJelekSzama - 1) >= 0)
                        {
                            if (kiErtekeles[i, j - nyereshezSzuksegesJelekSzama] == 0)
                            {
                                return true;
                            }
                        }
                        if (j + (nyereshezSzuksegesJelekSzama) <= palyaMerete && i + (nyereshezSzuksegesJelekSzama) <= palyaMerete)
                        {
                            if (kiErtekeles[i + nyereshezSzuksegesJelekSzama, j + nyereshezSzuksegesJelekSzama] == (palyaMerete - 1))
                            {
                                return true;
                            }
                        }
                        if (i - (nyereshezSzuksegesJelekSzama - 1) >= 0 && i - (nyereshezSzuksegesJelekSzama - 1) >= 0)
                        {
                            if (kiErtekeles[i - nyereshezSzuksegesJelekSzama, j - nyereshezSzuksegesJelekSzama] == 0)
                            {
                                return true;
                            }
                        }
                        if (i - (nyereshezSzuksegesJelekSzama - 1) >= 0 && j + (nyereshezSzuksegesJelekSzama) <= palyaMerete)
                        {
                            if (kiErtekeles[i - nyereshezSzuksegesJelekSzama, j + nyereshezSzuksegesJelekSzama] == 0 || kiErtekeles[i - nyereshezSzuksegesJelekSzama, j + nyereshezSzuksegesJelekSzama] == (palyaMerete - 1))
                            {
                                return true;
                            }
                        }
                        if (i + (nyereshezSzuksegesJelekSzama) <= palyaMerete && j - (nyereshezSzuksegesJelekSzama - 1) >= 0)
                        {
                            if (kiErtekeles[i + nyereshezSzuksegesJelekSzama, j - nyereshezSzuksegesJelekSzama] == 0 || kiErtekeles[i + nyereshezSzuksegesJelekSzama, j - nyereshezSzuksegesJelekSzama] == (palyaMerete - 1))
                            {
                                return true;
                            }
                        }
                        return false;
                    }
                    if (kiErtekeles[i, j] == kitEllenorzok)
                    {
                        if (i + nyereshezSzuksegesJelekSzama <= palyaMerete && miKellANyereshez(nyereshezSzuksegesJelekSzama, "+i", i, j, kitEllenorzok))
                        {
                            return true;
                        }
                        if (j + nyereshezSzuksegesJelekSzama <= palyaMerete && miKellANyereshez(nyereshezSzuksegesJelekSzama, "+j", i, j, kitEllenorzok))
                        {
                            return true;
                        }
                        if (j + (nyereshezSzuksegesJelekSzama) <= palyaMerete && i + (nyereshezSzuksegesJelekSzama) <= palyaMerete && miKellANyereshez(nyereshezSzuksegesJelekSzama, "+i+j", i, j, kitEllenorzok))
                        {
                            return true;
                        }
                        if (i - (nyereshezSzuksegesJelekSzama - 1) >= 0 && j + (nyereshezSzuksegesJelekSzama) <= palyaMerete && miKellANyereshez(nyereshezSzuksegesJelekSzama, "-i+j", i, j, kitEllenorzok))
                        {
                            return true;
                        }

                    }
                }
            }
            return false;
        }
        private bool miKellANyereshez(int nyereshezSzuksegesJelekSzama, string irany, int i, int j, int kitEllenorzok)
        {
            if (nyereshezSzuksegesJelekSzama == 3)
            {
                if (irany == "+i")
                {
                    if (kiErtekeles[i + 1, j] == kitEllenorzok && kiErtekeles[i + 2, j] == kitEllenorzok)
                    {
                        return true;
                    }
                }
                if (irany == "+j")
                {
                    if (kiErtekeles[i, j + 1] == kitEllenorzok && kiErtekeles[i, j + 2] == kitEllenorzok)
                    {
                        return true;
                    }
                }
                if (irany == "+i+j")
                {
                    if (kiErtekeles[i + 1, j + 1] == kitEllenorzok && kiErtekeles[i + 2, j + 2] == kitEllenorzok)
                    {
                        return true;
                    }
                }
                if (irany == "-i+j")
                {
                    if (kiErtekeles[i - 1, j + 1] == kitEllenorzok && kiErtekeles[i - 2, j + 2] == kitEllenorzok)
                    {
                        return true;
                    }
                }
            }
            else if (nyereshezSzuksegesJelekSzama == 4)
            {
                if (irany == "+i")
                {
                    if (kiErtekeles[i + 1, j] == kitEllenorzok && kiErtekeles[i + 2, j] == kitEllenorzok && kiErtekeles[i + 3, j] == kitEllenorzok)
                    {
                        return true;
                    }
                }
                if (irany == "+j")
                {
                    if (kiErtekeles[i, j + 1] == kitEllenorzok && kiErtekeles[i, j + 2] == kitEllenorzok && kiErtekeles[i, j + 3] == kitEllenorzok)
                    {
                        return true;
                    }
                }
                if (irany == "+i+j")
                {
                    if (kiErtekeles[i + 1, j + 1] == kitEllenorzok && kiErtekeles[i + 2, j + 2] == kitEllenorzok && kiErtekeles[i + 3, j + 3] == kitEllenorzok)
                    {
                        return true;
                    }
                }
                if (irany == "-i+j")
                {
                    if (kiErtekeles[i - 1, j + 1] == kitEllenorzok && kiErtekeles[i - 2, j + 2] == kitEllenorzok && kiErtekeles[i - 3, j + 3] == kitEllenorzok)
                    {
                        return true;
                    }
                }
            }
            else if (nyereshezSzuksegesJelekSzama == 5)
            {
                if (kitEllenorzok == -1)
                {

                }
                if (irany == "+i")
                {
                    if (kiErtekeles[i + 1, j] == kitEllenorzok && kiErtekeles[i + 2, j] == kitEllenorzok && kiErtekeles[i + 3, j] == kitEllenorzok && kiErtekeles[i + 4, j] == kitEllenorzok)
                    {
                        return true;
                    }
                }
                if (irany == "+j")
                {
                    if (kiErtekeles[i, j + 1] == kitEllenorzok && kiErtekeles[i, j + 2] == kitEllenorzok && kiErtekeles[i, j + 3] == kitEllenorzok && kiErtekeles[i, j + 4] == kitEllenorzok)
                    {
                        return true;
                    }
                }
                if (irany == "+i+j")
                {
                    if (kiErtekeles[i + 1, j + 1] == kitEllenorzok && kiErtekeles[i + 2, j + 2] == kitEllenorzok && kiErtekeles[i + 3, j + 3] == kitEllenorzok && kiErtekeles[i + 4, j + 4] == kitEllenorzok)
                    {
                        return true;
                    }
                }
                if (irany == "-i+j")
                {
                    if (kiErtekeles[i - 1, j + 1] == kitEllenorzok && kiErtekeles[i - 2, j + 2] == kitEllenorzok && kiErtekeles[i - 3, j + 3] == kitEllenorzok && kiErtekeles[i - 4, j + 4] == kitEllenorzok)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        private void jatekVege(int kiNyert)
        {
            if (kiNyert == -4)
            {
                MessageBox.Show("Gratulálok ön nyert");
            }
            else
            {
                MessageBox.Show("sajnálom ön vesztett");
            }
            kezdesGob.Visible = false;
            kezdesGob.Location = new Point(223, 44);
            kezdesGob.Text = "Kezdés";
            palya.Controls.Clear();
            palya.Controls.Add(kezdesGob);
            kezdesGob.Visible = true;
        }
    }
}
/*kitEllenorzok
 
megnezni merre vznak tole a tobbbi mezok.
-------------------------------------------
megnezni hogy a korulotte levo mezokon van e kor vgy x.
-------------------------------------------
n: nem kell ezel a mezovel foglalkozni.
-------------------------------------------
i: ellenorizni hogy ha ide rakna valki ez x-et, vagy egy kort, akkor azzal lehet e nyerni h igen a gep rokjon ide egy x-et:

1. lehelyezni egy idegilenes jelet, es csinalni egy hogyomanyos nyeres ellenorzest.
i: ha a palya merete 3 es az ideiglenes jel iranyaban felett erint az ideiglenes jel helyer kerul egy x.
vagy ha a palya merete 3 akorr az ideiglenes jel helyer kerul egy x.
vagy le kell ellneorizni hogy a  nyeres iranzaban meg egy jelet lehelyezve nyer e a jatekos.
i: le kell helyezni az elso ideiglenes jel helyere egy x-et.
n: megjeloles hogy szomszedos egy korre, x-el es eltavolitjuk az ideiglenes jeleket.

a jelolesek kozul valaszt egyett a gep.

 */



/*

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

}
}
}
}*/