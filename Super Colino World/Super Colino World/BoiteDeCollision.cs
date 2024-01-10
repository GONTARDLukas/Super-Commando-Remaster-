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
        private double x, y;
        public Rect rectangle; 
        public BoiteDeCollision(double x, double y, double longueur, double largeur)
        {
            this.x = x; this.y = y;
            this.rectangle = new Rect(x, y, longueur, largeur); 
        }
        public void metX(double x)
        {
            this.x = x;
            this.rectangle.X = x;
        }
        public void metY(double y)
        {
            this.y = y;
            this.rectangle.Y = y;
        }
        public double prendsX()
        {
            return this.rectangle.X;    
        }
        public double prendsY()
        {
            return this.rectangle.Y;
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
