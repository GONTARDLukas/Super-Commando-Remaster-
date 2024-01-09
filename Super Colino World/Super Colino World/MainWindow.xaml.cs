using Microsoft.Windows.Themes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Super_Colino_World
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static int tempsSaut = 0;
        public static bool droite, gauche;
        private Joueur? joueur;
        private Bullet[] bullets = new Bullet[64];
        private DispatcherTimer dispatcherTimer = new DispatcherTimer();
        public static readonly int FENETRE_HAUTEUR = 720;
        public static readonly int FENETRE_LARGEUR = 400;
        public static readonly int VITESSE_JOUEUR =(int) Math.Pow(2,3);
        public static readonly int VITESSE_SAUT_JOUEUR = 4;
        public static readonly int VITESSE_PROJECTILE = (int)Math.Pow(2, 4);


        public MainWindow()
        {
            InitializeComponent();
           
            Init(); 
        }
        public void Init()
        {
            CanvasWPF.Focus();
            JoueurBras.Fill = new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Ressources/ArmeT1.png")));
            JoueurBras.Height = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Ressources/ArmeT1.png")).Height;
            JoueurBras.Width = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Ressources/ArmeT1.png")).Width;

            this.joueur = new Joueur(188,570, 200, 580, VITESSE_JOUEUR, VITESSE_SAUT_JOUEUR);
            // lie le timer du répartiteur à un événement appelé moteur de jeu gameengine
            dispatcherTimer.Tick += BoucleJeu;
            // rafraissement toutes les 16 milliseconds
            dispatcherTimer.Interval = TimeSpan.FromMilliseconds(16);
            // lancement du timer
            dispatcherTimer.Start();
        }
        public void BoucleJeu(object sender, EventArgs e)
        {
            joueur.Move();
            Canvas.SetLeft(JoueurSprite, joueur.xCorps);
            Canvas.SetTop(JoueurSprite, joueur.yCorps);
            Canvas.SetLeft(JoueurBras, joueur.xBras);
            Canvas.SetTop(JoueurBras, joueur.yBras);
            foreach (Bullet projectile in this.bullets)
            {
                if (projectile != null)
                {
                    projectile.Move();
                    Canvas.SetLeft(projectile.projectileImage, projectile.x);
                    Canvas.SetTop(projectile.projectileImage, projectile.y);
                }
            }
        }
        private void CanvasKeyIsDown(object sender, KeyEventArgs e)
        {
            switch(e.Key){
                case Key.Left:
                    MainWindow.gauche = true; 
                    break;
                case Key.Right:
                    MainWindow.droite = true;
                    break;

            }
        }
        private void CanvasUp(object sender, EventArgs e)
        {

        }
        private void CanvasDown(object sender, EventArgs e)
        {
            double a = (Mouse.GetPosition(this.CanvasWPF).X - Canvas.GetLeft(JoueurBras)) + new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Ressources/ArmeT1.png")).Width;
            double b = (Mouse.GetPosition(this.CanvasWPF).Y - Canvas.GetTop(JoueurBras));
            double angle = Math.Atan(a/b) * 180 / 3.14159;
            Bullet projectile = new Bullet((int)(Canvas.GetLeft(this.JoueurBras)+ new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Ressources/ArmeT1.png")).Width), (int)Canvas.GetTop(this.JoueurBras), VITESSE_PROJECTILE,b/a); 
            this.CanvasWPF.Children.Add(projectile.projectileImage);
            Canvas.SetTop(projectile.projectileImage, Canvas.GetLeft(JoueurBras) + new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Ressources/ArmeT1.png")).Width);
            Canvas.SetLeft(projectile.projectileImage, Canvas.GetTop(JoueurBras));
            this.bullets[bullets.Length - 1]=projectile;
        }
        private void CanvasMove(object sender, EventArgs e)
        {
            double a = (Mouse.GetPosition(this.CanvasWPF).X - joueur.xBras) + new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Ressources/ArmeT1.png")).Width;
            double b = (Mouse.GetPosition(this.CanvasWPF).Y - joueur.yBras);
            double c = Math.Sqrt(Math.Pow(a, 2) + Math.Pow(b, 2));
            //double angle=Math.Asin(b/c) * 180 / Math.PI;
            double angle = Math.Atan(a/b) * 180 / 3.14159;

            // Trace.WriteLine(angle);
            this.JoueurBras.RenderTransform = new RotateTransform(angle,5,5);
        }
        private void CanvasKeyIsUp(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Left:
                    MainWindow.gauche = false;
                    break;
                case Key.Right:
                    MainWindow.droite = false;
                    break;
            }
        }
    }
}
