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
        private int vitesseCotes, vitesseHaut;

        public int VitesseCotes
        {
            get { return this.vitesseCotes; }
            set { this.vitesseCotes = value; }
        }

        public Joueur(int vitesseCotes, int vitesseHaut) {
            this.vitesseHaut = vitesseHaut;
            this.vitesseCotes = vitesseCotes;
        }
        public void InitPlayer()
        {
            
        }
        public void Move()
        {
            if (MainWindow.droite)
            {
                this.x += vitesseCotes;
            }
            if(MainWindow.gauche)
            {
                this.x -= vitesseCotes;

            }
            if (MainWindow.haut)
            {
                this.y += vitesseHaut;

            }

        }
    }
}
