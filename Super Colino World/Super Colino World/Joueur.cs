using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Super_Colino_World
{
    internal class Joueur
    {
        public int xCorps;
        public int yCorps;
        public int xBras;
        public int yBras;
        public int xJambes;
        public int yJambes;
        public int jambesCommandoIndex=0;
        public bool jambesActualisees = false;
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

        public Joueur(int x, int y, int xBras, int yBras, int xJambes, int yJambes, int vitesseCotes, int vitesseSaut, BoiteDeCollision boiteDeCollision) {
            this.xCorps = x;
            this.yCorps = y; 
            this.xBras = xBras;
            this.yBras = yBras; 
            this.xJambes = xJambes;
            this.yJambes = yJambes;
            this.Vitesse = vitesseCotes;
            this.VitesseSaut = vitesseSaut;
            this.boiteDeCollision = boiteDeCollision;   
        }
        
        public void InitPlayer()
        {
        }


        public void Move()
        {
            MainWindow.tempsIndexJambes++;
            if (MainWindow.tempsIndexJambes >= 5)
            {
                MainWindow.tempsIndexJambes = 0;
                jambesCommandoIndex+= jambesCommandoIndex+1<8 ? 1 : 0;
                this.jambesActualisees = true;
            }
            else
            {
                this.jambesActualisees = false;
            }
            if (jambesCommandoIndex >= 8)
            {
                jambesCommandoIndex = 0;
            }
            if (MainWindow.droite)
            {
                this.xCorps += this.Vitesse;
                this.xBras += this.Vitesse;
                this.xJambes += this.Vitesse;

                this.boiteDeCollision.metX(this.boiteDeCollision.prendsX() + this.Vitesse);
            }
            if(MainWindow.gauche)
            {
                this.xCorps -= this.Vitesse;
                this.xBras -= this.Vitesse;
                this.xJambes -= this.Vitesse;

                this.boiteDeCollision.metX(this.boiteDeCollision.prendsX() - this.Vitesse);



            }
            if (MainWindow.saut && MainWindow.tempsSaut < 60)
            {

                this.yCorps -= this.VitesseSaut;
                this.yBras -= this.VitesseSaut;
                this.yJambes -= this.VitesseSaut;

                this.boiteDeCollision.metY(this.boiteDeCollision.prendsY() - this.VitesseSaut);
                MainWindow.tempsSaut ++;
            }else // le perso chute jusqu'au game over ou jusqu'a atteindre une plateforme
            {
                this.yCorps += this.VitesseSaut;
                this.yBras += this.VitesseSaut;
                this.yJambes += this.VitesseSaut;

                this.boiteDeCollision.metY(this.boiteDeCollision.prendsY() + this.VitesseSaut); 
            }
            if (this.xCorps < 0 - Joueur.LARGEUR)
            {
                this.xCorps=MainWindow.FENETRE_LARGEUR;
                this.xBras = MainWindow.FENETRE_LARGEUR;
                this.xJambes = MainWindow.FENETRE_LARGEUR;
                this.boiteDeCollision.metX(MainWindow.FENETRE_LARGEUR);


            }
            if (this.xCorps>MainWindow.FENETRE_LARGEUR)
            {
                this.xCorps = -Joueur.LARGEUR;
                this.xBras = -Joueur.LARGEUR;
                this.xJambes = -Joueur.LARGEUR;

                this.boiteDeCollision.metX(-Joueur.LARGEUR);

            }

        }
        public void tourneGauche()
        {

        }
    }
}
