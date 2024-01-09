using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Super_Colino_World
{
    internal class Joueur
    {
        public int xCorps;
        public int yCorps;
        public int xBras;
        public int yBras;
        private int taille;
        private int vitesse;
        public int angleBras = 0;
        public static readonly int LARGEUR = 50;
        public static readonly int LONGUEUR = 25; 
        public int Vitesse
        {
            get { return this.vitesse; }
            set { this.vitesse = value; }
        }

        public Joueur(int x, int y, int brasx, int brasy, int vitesseCotes) {
            this.xCorps = x;
            this.yCorps = y; 
            this.xBras = brasx;
            this.yBras = brasy; 
            this.Vitesse = vitesseCotes;
        }
        
        public void InitPlayer()
        {
        }
        public void Move()
        {
            if (MainWindow.droite)
            {
                this.xCorps += this.Vitesse;
                this.xBras += this.Vitesse;
            }
            if(MainWindow.gauche)
            {
                this.xCorps -= this.Vitesse;
                this.xBras -= this.Vitesse;


            }
            if (MainWindow.haut)
            {

            }
            if (this.xCorps < 0 - Joueur.LARGEUR)
            {
                this.xCorps=MainWindow.FENETRE_LARGEUR;
                this.xBras = MainWindow.FENETRE_LARGEUR;

            }
            if (this.xCorps>MainWindow.FENETRE_LARGEUR)
            {
                this.xCorps = -Joueur.LARGEUR;
                this.xBras = -Joueur.LARGEUR;
            }

        }
    }
}
