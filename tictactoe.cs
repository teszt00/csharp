using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TicTacToe_T
{
    /*A játéktáblán hozzunk létre kilenc nyomógomb (Button) futási időben, az ablak Load
eseményének bekövetkezésekor. Állítsuk be a gombok méretét, helyét és betűméretét.
A játékot két játékos játssza, tehát a gombokra kattintva felváltva rakjuk ki az X és O
jelet (természetesen csak akkor, ha még nincs rajta jel). Gondoljuk át, hogyan tudjuk a
lehető legegyszerűbben leellenőrizni, hogy nincs-e valamelyik jelből három darab
egymás mellett, alatt, vagy átlósan. Ha vége a játéknak, írjunk ki egy üzenetablakot az
eredménnyel (O nyert, X nyert, döntetelen).
    */

    public partial class Form1 : Form
    {
        private Button[,] nyomogombok = new Button[3, 3]; //ezzel csak tombot hoztunk letre, definialtuk, letrehoztuk, de nincsenek objektumok a tombben csak nullak
        private int szamlalo;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < 3; i++)//sor
            {
                for (int j = 0; j < 3; j++)//oszlop
                {
                    nyomogombok[i, j] = new Button();
                    nyomogombok[i, j].Size = new Size(100, 100);
                    //ne egymason legyenek a gombok
                    nyomogombok[i, j].Location = new Point(100 * j, 100 * i);
                    nyomogombok[i, j].Font = new Font("Arial", 65, FontStyle.Bold);//()/ba allva ctrl shift space a help
                    //esemeny definialasa a gombhoz /a gombraKattintas fv iras elkezdes utan, csak += hozzaadhatok fv-t
                    //nem tudunk kozvetlenul esemenyt meghivni (pl gombra kattintast), kell event handler
                    nyomogombok[i, j].Click += new EventHandler(GombraKattintas);

                    this.Controls.Add(nyomogombok[i, j]);
                }
            }
            //form merete pont akkora legyen mint a rajta levo gombok, ezt nem ciklusba
            this.ClientSize = new Size(300, 300);
            //ne lehessen atmeretezni az ablakot
            this.FormBorderStyle = FormBorderStyle.FixedSingle; //vilagoszold, ez egy felsorolt tipus, enum, nem osztaly
            //ne tudjam maximalizalni az ablakot
            //help ha ramegyek es f1, akkor kijon a microsoft hivatalos dokumentuma
            this.MaximizeBox = false;
        }

        //kattintas esemenyre megjelenjen a felirat, mindig visszateresi 
        //tipus void ha kattintos. az egyik tipusanak objectnek kell lennie, 
        //a masiknak event args osztalybol lennie
        //lehet ott mouse event args, key event args
        //objectet irjuk at nagy O-ra, tobb help jelenik meg, irjuk kicsi o-val
        //az event handler csak olyan metodust fogad el, ami object tipusu, buttont nem adhatok meg
        //amire kattintok, az a sender gomb, bar a tipusa az object.  
        private void GombraKattintas(object sender, EventArgs e)
        {
            //de tudom, hogy a sender az egy objectbol leszarmaztatott button, ezert 
            //letrehozok egy valtozot buttun tipusra es atcastolom
            //if(sender is Button){}if(sender is label){}....
            Button myPressedButton = (Button)sender;
            //ifet szamlaloval oldjuk meg, 0 alapertelm ertekkel, classban veszem fel

            //csak akkor tudjak x/et meg o/t kitenni, ha a konkret mezon meg nincs jel /ha meg nincs a textben semmi
            if (myPressedButton.Text == "")
            {
                szamlalo++;
                if (szamlalo % 2 == 1)
                {
                    myPressedButton.Text = "X";
                }
                else
                {
                    myPressedButton.Text = "O";
                }
                Ellenorzes();
            }
            //ellenorzes.ha kiteszek x/et vagy o-t, ellenorizze. de ne mindig, 
            //ha a felhasznalo kattintgat akkor ne. csak a jelkirakas utan ellenorizze
        }
        private void Ellenorzes()
        {
            string nyertes = "";
            //harom sor, harom oszlop, ket atlot
            for (int i = 0; i < 3; i++)
            {
                //i.sor ellenorzese
                if (nyomogombok[i, 0].Text == nyomogombok[i, 1].Text &&
                    nyomogombok[i, 1].Text == nyomogombok[i, 2].Text &&
                    nyomogombok[i, 0].Text != "") //*itt is barmelyiket valaszthatom
                {
                    nyertes = nyomogombok[i, 0].Text; //azert0, mert valasztok egyet a 0,1,2 kozul/barmelyiket valaszthatom *
                }
                //i. oszlop ellenorzese
                if (nyomogombok[0, i].Text == nyomogombok[1, i].Text &&
                   nyomogombok[1, i].Text == nyomogombok[2, i].Text &&
                   nyomogombok[0, i].Text != "")
                {
                    nyertes = nyomogombok[0, i].Text;
                }

            }
            //ket atlo ellenorzese nem kell ellenorizni h ures-e, mert akkor a sorokban oszlopokban atloban sem nyert biztosan senki
            if (nyomogombok[0, 0].Text == nyomogombok[1, 1].Text &&
               nyomogombok[1, 1].Text == nyomogombok[2, 2].Text)
            {
                nyertes = nyomogombok[0, 0].Text;
            }

            if (nyomogombok[0, 2].Text == nyomogombok[1, 1].Text &&
           nyomogombok[1, 1].Text == nyomogombok[2, 0].Text)
            {
                nyertes = nyomogombok[0, 2].Text;
            }

            //nyertes kiirasa
            if (nyertes != "")
            {
                MessageBox.Show(nyertes + " nyert!", "Jatek vege!", MessageBoxButtons.OK);
                JatekUjrainditasa();
            }
            //dontetlen is kell, ha a szamlalo egyenlo 9
            else if (szamlalo == 9)
            {
                MessageBox.Show("Dontetlen");
            }
        }
        private void JatekUjrainditasa()
        {
            szamlalo = 0;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    nyomogombok[i, j].Text = "";
                }
            }
        }
    }
}
