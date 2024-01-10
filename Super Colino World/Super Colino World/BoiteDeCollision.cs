using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Super_Colino_World
{
    internal class BoiteDeCollision
    {
        public int x, y;
        public Rect rectangle; 
        public BoiteDeCollision(int x, int y, int longueur, int largeur)
        {
            this.x = x; this.y = y;
            this.rectangle = new Rect(x, y, longueur, largeur); 
        }
        public bool estEnCollisionAvec(Rect rectangle)
        {
            return this.rectangle.IntersectsWith(rectangle);
        }
        public bool estEnCollisionAvec(BoiteDeCollision rectangle)
        {
            return this.rectangle.IntersectsWith(rectangle.rectangle);
        }

    }
}
