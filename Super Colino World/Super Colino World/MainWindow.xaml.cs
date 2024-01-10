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
        public static int tempsIndexJambes = 0;
        public static bool droite, gauche;
        private Joueur? joueur; 
        private Bullet[] bullets = new Bullet[64];
        public int nombreProjectile;
        private DispatcherTimer dispatcherTimer = new DispatcherTimer();
        public static readonly int FENETRE_HAUTEUR = 720;
        public static readonly int FENETRE_LARGEUR = 400;
        public static readonly int VITESSE_JOUEUR =(int) Math.Pow(2,3);
        public static readonly int VITESSE_SAUT_JOUEUR = 4;
        public static readonly int VITESSE_PROJECTILE = (int)Math.Pow(2, 4);
        private double vitessePlateforme = 1;
        private List<Rectangle> poubelle = new List<Rectangle>();



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
            BoiteDeCollision collisionSaut = new BoiteDeCollision((int)Canvas.GetLeft(CollisionJump), (int)Canvas.GetTop(CollisionJump),(int)CollisionJump.Width, (int)CollisionJump.Height);
            this.joueur = new Joueur(188,570, 200, 580, VITESSE_JOUEUR, VITESSE_SAUT_JOUEUR, collisionSaut);
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
            if (joueur.jambesActualisees)
            {
            }
            poubelle.Clear();
            Canvas.SetLeft(JoueurSprite, joueur.xCorps);
            Canvas.SetTop(JoueurSprite, joueur.yCorps);
            Canvas.SetLeft(JoueurBras, joueur.xBras);
            Canvas.SetTop(JoueurBras, joueur.yBras);
            Canvas.SetLeft(CollisionJump, joueur.boiteDeCollision.prendsX());
            Canvas.SetTop(CollisionJump, joueur.boiteDeCollision.prendsY());
            foreach (Bullet projectile in this.bullets)
            {
                if (projectile != null)
                {
                    projectile.Move();
                    Canvas.SetLeft(projectile.projectileImage, projectile.x);
                    Canvas.SetTop(projectile.projectileImage, projectile.y);
                }
            }

            foreach (Rectangle x in CanvasWPF.Children.OfType<Rectangle>())
            {
                if (x is Rectangle && (string)x.Tag == "plate-forme")
                {
                    //les plateformes descendent
                    Canvas.SetTop(x, Canvas.GetTop(x) + vitessePlateforme);

                    //on ajoute les plateformes qui quittent la fenêtre a la liste des éléments a supprimer
                    if (Canvas.GetTop(x) > ActualHeight + x.ActualHeight)
                    {
                        poubelle.Add(x);
                        //quand on supprime une plateforme on en recrée une
                        //GenPlateformes();
                    }

                    BoiteDeCollision plateForme = new BoiteDeCollision(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);
                    if (tempsSaut == 60 && plateForme.estEnCollisionAvec(this.joueur.boiteDeCollision))
                    {
                        tempsSaut = 0;
                        Trace.WriteLine(tempsSaut);
                    }
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
            double angle = Math.Atan2(b,a) * 180 / Math.PI;
            Bullet projectile = new Bullet((int)(Canvas.GetLeft(this.JoueurBras)+ new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Ressources/ArmeT1.png")).Width), (int)Canvas.GetTop(this.JoueurBras), Mouse.GetPosition(this.CanvasWPF).X, Mouse.GetPosition(this.CanvasWPF).Y, VITESSE_PROJECTILE); 
            Canvas.SetTop(projectile.projectileImage, Canvas.GetLeft(JoueurBras) + new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Ressources/ArmeT1.png")).Width);
            Canvas.SetLeft(projectile.projectileImage, Canvas.GetTop(JoueurBras));
            projectile.projectileImage.RenderTransform = new RotateTransform(angle, 5, 5);
            this.bullets[nombreProjectile] = projectile;
            this.nombreProjectile++;
            this.CanvasWPF.Children.Add(projectile.projectileImage);

        }
        private void CanvasMove(object sender, EventArgs e)
        {
            double a = (Mouse.GetPosition(this.CanvasWPF).X - joueur.xBras) + new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Ressources/ArmeT1.png")).Width;
            double b = (Mouse.GetPosition(this.CanvasWPF).Y - joueur.yBras);
            double angle = Math.Atan2(b,a) * 180 / Math.PI;
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
        public double xAleatoire()
        {
            Random rng = new Random();
            return (rng.NextDouble()%FenetreDeJeu.Width);
        }

        private void GenPlateformes()
        {
            Rectangle nouvellePlateforme = new Rectangle
            {
                Tag = "plate-forme",
                Height = 15,
                Width = 100,
                Fill = new SolidColorBrush(Colors.Yellow),
            };
            Panel.SetZIndex(nouvellePlateforme, -1);
            Canvas.SetLeft(nouvellePlateforme, xAleatoire());
            Canvas.SetBottom(nouvellePlateforme, 0);
            CanvasWPF.Children.Add(nouvellePlateforme);
        }
    }
}
