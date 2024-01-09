using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Super_Colino_World
{
    internal class Joueur
    {
        public int x = 188;
        public int y= 570;
        private int taille;
        private int vitesse;

        public static readonly int LARGEUR = 50;
        public static readonly int LONGUEUR = 25; 
        public int Vitesse
        {
            get { return this.vitesse; }
            set { this.vitesse = value; }
        }

        public Joueur(int vitesseCotes) {
            this.Vitesse = vitesseCotes;
        }
        
        public void InitPlayer()
        {
            
        }
        public void Move()
        {
            if (MainWindow.droite)
            {
                this.x += this.Vitesse;
            }
            if(MainWindow.gauche)
            {
                this.x -= this.Vitesse;

            }
            if (MainWindow.haut)
            {

            }
            if (this.x < 0 - Joueur.LARGEUR)
            {
                this.x=MainWindow.FENETRE_LARGEUR;  
            }
            if(this.x>MainWindow.FENETRE_LARGEUR)
            {
                this.x = -Joueur.LARGEUR;
            }

        }
    }
}
