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
    class Plateforme
    {
		private int Zindex = -1;
		public Rectangle PlateformeImage;
		public int x, y, hauteur, largeur, vitesse;

		public Plateforme(int x, int y, int largeur, int hauteur, int vitesse)
		{
			this.hauteur = hauteur;
			this.largeur = largeur;
			this.x = x;
			this.y = y;
			this.vitesse = vitesse;
			PlateformeImage = new Rectangle()
			{
				Height = (int)new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Ressources/Plateforme.png")).Height,
				Width = (int)new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Ressources/Plateforme.png")).Width,
				Fill = new ImageBrush( new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Ressources/Plateforme.png")))
            };
        }

		public void SeDeplace()
		{
            this.y += vitesse;
        }
	}
}
