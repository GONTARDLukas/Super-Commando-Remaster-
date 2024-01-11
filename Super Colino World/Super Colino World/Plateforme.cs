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
		private int largeur;
		public Rectangle PlateformeImage;

		public int Largeur
		{
			get { return this.largeur; }
			set { this.largeur = value; }
		}

		private int hauteur;

		public int Hauteur
		{
			get { return this.hauteur; }
			set { this.hauteur = value; }
		}

		private int x;

		public int X
		{
			get { return this.x; }
			set { this.x = value; }
		}

		public Plateforme(int hauteur, int largeur, int x)
		{
			this.hauteur = hauteur;
			this.largeur = largeur;
			this.x = x;
			PlateformeImage = new Rectangle()
			{
				Height = (int)new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Ressources/Plateforme.png")).Height,
				Width = (int)new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Ressources/Plateforme.png")).Width,
				Fill = new ImageBrush( new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Ressources/Plateforme.png")))
            };
        }

	}
}
