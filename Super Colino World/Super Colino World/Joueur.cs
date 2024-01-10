using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
        private int vitesse,vitesseSaut;
        public int angleBras = 0;
        public BoiteDeCollision boiteDeCollision;
        public static readonly int LARGEUR = 50;
        public static readonly int LONGUEUR = 25; 
        public int Vitesse
        {
            get { return this.vitesse; }
            set { this.vitesse = value; }
        }

        public int VitesseSaut
        {
            get { return this.vitesseSaut; }
            set { this.vitesseSaut = value; }
        }

        public Joueur(int x, int y, int brasx, int brasy, int vitesseCotes, int vitesseSaut, BoiteDeCollision boiteDeCollision) {
            this.xCorps = x;
            this.yCorps = y; 
            this.xBras = brasx;
            this.yBras = brasy; 
            this.Vitesse = vitesseCotes;
            this.VitesseSaut = vitesseSaut;
            this.boiteDeCollision = boiteDeCollision;   
        }
        
        public void InitPlayer()
        {
        }

        public void Sauter()
        {
            if (MainWindow.ascension)
                this.y += vitesseHaut;
            else this.y -= vitesseCotes;
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
            if (MainWindow.tempsSaut < 60)
            {
                this.yCorps -= this.VitesseSaut;
                this.yBras -= this.VitesseSaut;
                MainWindow.tempsSaut ++;
            }else // le perso chute jusqu'au game over ou jusqu'a atteindre une plateforme
            {
                this.yCorps += this.VitesseSaut;
                this.yBras += this.VitesseSaut;
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
