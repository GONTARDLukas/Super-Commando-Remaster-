using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Super_Colino_World
{
    internal class Joueur
    {
        public int x, y;
        public int taille;
        public int vitesseCotes, vitesseHaut; 

        public Joueur(int vitesseCotes, int vitesseHaut) {
            this.vitesseHaut = vitesseHaut;
            this.vitesseCotes = vitesseCotes;
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
                this.y += vitesseCotes;

            }

        }
    }
}
