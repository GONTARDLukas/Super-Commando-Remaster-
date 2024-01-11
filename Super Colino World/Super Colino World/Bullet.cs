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
        public double x, y, mouseX, mouseY, vitesse;
        public double ratio; 
        public Rectangle projectileImage;
        public Bullet(int x, int y, double mouseX, double mouseY, int vitesse) {
            this.ratio = ratio;
            Trace.WriteLine(ratio);
            this.x = x;
            this.y = y;
            this.mouseX = mouseX-x;
            this.mouseY = mouseY-y;
            this.vitesse = vitesse;
            projectileImage = new Rectangle()
            {
            Height = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Ressources/ProjectileT1.png")).Height,
            Width = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Ressources/ProjectileT1.png")).Width,
            Fill = new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Ressources/ProjectileT1.png")))
            };


        }
        public void Move() {
            double distance = Math.Sqrt(Math.Pow(mouseX, 2) + Math.Pow(mouseY, 2));
            double velociteX = mouseX / distance * vitesse;
            double velociteY = mouseY / distance * vitesse;

            this.x += velociteX;
            this.y += velociteY;
        }
    }
}
