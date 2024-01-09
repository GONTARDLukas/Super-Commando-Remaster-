using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Media3D;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Diagnostics;
using System.Windows.Media.Imaging;

namespace Super_Colino_World
{
    internal class Bullet
    {
        public int x, y, vitesse;
        public double ratio; 
        public Rectangle projectileImage;
        public Bullet(int x, int y, int vitesse, double ratio) {
            this.ratio = ratio;
            Trace.WriteLine(ratio);
            this.x = x;
            this.y = y;
            this.vitesse = vitesse;
            projectileImage = new Rectangle()
            {
            Height = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Ressources/ArmeT1.png")).Height/4,
            Width = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Ressources/ArmeT1.png")).Width/4,
            Fill = new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Ressources/ProjectileT1.png")))
            };


        }
        public void Move() {
            this.x += (int)((1-this.ratio) * vitesse);
            this.y += (int)( this.ratio * vitesse); 
        }
    }
}
