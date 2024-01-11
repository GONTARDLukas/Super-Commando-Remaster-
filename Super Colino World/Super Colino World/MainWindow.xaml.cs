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
        public static int tempsGenPlat = 0;
        public static int tempsIndexJambes = 0;
        public static bool droite, gauche, saut ,echap;
        private Joueur? joueur; 
        private Bullet[] bullets = new Bullet[64];
        private List<Plateforme> plateformes = new List<Plateforme>();
        public int nombreProjectile;
        public int nombrePlateformes;

        private DispatcherTimer dispatcherTimer = new DispatcherTimer();
        public static readonly int FENETRE_HAUTEUR = 720;
        public static readonly int FENETRE_LARGEUR = 590;
        public static readonly int VITESSE_JOUEUR =(int) Math.Pow(2,3);
        public static readonly int VITESSE_SAUT_JOUEUR = 5;
        public static readonly int VITESSE_PROJECTILE = (int)Math.Pow(2, 4);
        private double vitessePlateforme = 2;
        private int vitesseSaut = VITESSE_SAUT_JOUEUR;
        private List<Rectangle> poubelle = new List<Rectangle>();



        public MainWindow()
        {
            InitializeComponent();
            Init(); 
        }
        public void Init()
        {
            CanvasWPF.Focus();
            JoueurBras.Fill = new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Ressources/ArmeT1Droite.png")));
            JoueurBras.Height = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Ressources/ArmeT1Droite.png")).Height;
            JoueurBras.Width = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Ressources/ArmeT1Droite.png")).Width;
            JoueurJambes.Fill = new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Ressources/CommandoJambesDefautDroite.png")));
            JoueurJambes.Height = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Ressources/CommandoJambesDefautDroite.png")).Height;
            JoueurJambes.Width = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Ressources/CommandoJambesDefautDroite.png")).Width;
            JoueurTronc.Fill = new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Ressources/CommandoCorpsDroite.png")));
            JoueurTronc.Height = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Ressources/CommandoCorpsDroite.png")).Height;
            JoueurTronc.Width = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Ressources/CommandoCorpsDroite.png")).Width;
            BoiteDeCollision collisionSaut = new BoiteDeCollision((int)Canvas.GetLeft(CollisionJump), (int)Canvas.GetTop(CollisionJump),(int)CollisionJump.Width, (int)CollisionJump.Height);
            this.joueur = new Joueur(188,570, 197, 587, 188, 603, VITESSE_JOUEUR, VITESSE_SAUT_JOUEUR /* souce du problème de la vitesse de chute du joueur */ , collisionSaut);
            InitPlateformes();
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
            GenPlateformes();
            if (joueur.jambesActualisees)
            {
            }
            Canvas.SetLeft(JoueurTronc, joueur.xCorps);
            Canvas.SetTop(JoueurTronc, joueur.yCorps);
            Canvas.SetLeft(JoueurBras, joueur.xBras);
            Canvas.SetTop(JoueurBras, joueur.yBras);
            Canvas.SetLeft(JoueurJambes, joueur.xJambes);
            Canvas.SetTop(JoueurJambes, joueur.yJambes);
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

            
                for (int i = 0; i < plateformes.Count; i++)
                {
                    Plateforme plateforme = plateformes[i];
                    plateforme.SeDeplace();
                    Canvas.SetTop(plateforme.PlateformeImage, plateforme.y);
                    BoiteDeCollision plateForme = new BoiteDeCollision(plateforme.x, plateforme.y, plateforme.largeur, plateforme.hauteur);
                    if (tempsSaut == 60 && plateForme.estEnCollisionAvec(this.joueur.boiteDeCollision))
                    {
                        tempsSaut = 0;
                        saut = false;
                        Trace.WriteLine(tempsSaut);

                    }
                    if (plateforme.y > ActualHeight + plateforme.hauteur)
                    {
                        plateformes.Remove(plateforme);
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
                case Key.Escape:
                    MenuPause();
                    break;
                case Key.Space:
                    MainWindow.saut = true;
                    break;
            }
        }
        private void CanvasUp(object sender, EventArgs e)
        {

        }
        private void CanvasDown(object sender, EventArgs e)
        {
            double a = (Mouse.GetPosition(this.CanvasWPF).X - Canvas.GetLeft(JoueurBras)) + new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Ressources/ArmeT1Droite.png")).Width;
            double b = (Mouse.GetPosition(this.CanvasWPF).Y - Canvas.GetTop(JoueurBras));
            double angle = Math.Atan2(b,a) * 180 / Math.PI;
            Bullet projectile = new Bullet((int)(Canvas.GetLeft(this.JoueurBras)+ new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Ressources/ProjectileT1.png")).Width), (int)Canvas.GetTop(this.JoueurBras), Mouse.GetPosition(this.CanvasWPF).X, Mouse.GetPosition(this.CanvasWPF).Y, VITESSE_PROJECTILE); 
            Canvas.SetTop(projectile.projectileImage, Canvas.GetLeft(JoueurBras) + new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Ressources/ProjectileT1.png")).Width);
            Canvas.SetLeft(projectile.projectileImage, Canvas.GetTop(JoueurBras));
            projectile.projectileImage.RenderTransform = new RotateTransform(angle, 5, 5);
            this.bullets[nombreProjectile] = projectile;
            this.nombreProjectile++;
            this.CanvasWPF.Children.Add(projectile.projectileImage);

        }
        private void CanvasMove(object sender, EventArgs e)
        {
            double a = (Mouse.GetPosition(this.CanvasWPF).X - joueur.xBras) + new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Ressources/ArmeT1Droite.png")).Width;
            double b = (Mouse.GetPosition(this.CanvasWPF).Y - joueur.yBras);
            double angle = Math.Atan2(b,a) * 180 / Math.PI;
            if (!(angle>0 && angle<90 || angle<0 && angle>-90))
            {
                JoueurBras.Fill = new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Ressources/ArmeT1Gauche.png")));
                JoueurBras.Height = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Ressources/ArmeT1Gauche.png")).Height;
                JoueurBras.Width = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Ressources/ArmeT1Gauche.png")).Width;
                JoueurJambes.Fill = new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Ressources/CommandoJambesDefautGauche.png")));
                JoueurJambes.Height = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Ressources/CommandoJambesDefautGauche.png")).Height;
                JoueurJambes.Width = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Ressources/CommandoJambesDefautGauche.png")).Width;

                if (angle > -180 && angle < 0)
                {
                    JoueurTronc.Fill = new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Ressources/CommandoCorpsHautGauche.png")));
                    JoueurTronc.Height = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Ressources/CommandoCorpsHautGauche.png")).Height;
                    JoueurTronc.Width = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Ressources/CommandoCorpsHautGauche.png")).Width;
                } else
                {
                    JoueurTronc.Fill = new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Ressources/CommandoCorpsGauche.png")));
                    JoueurTronc.Height = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Ressources/CommandoCorpsGauche.png")).Height;
                    JoueurTronc.Width = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Ressources/CommandoCorpsGauche.png")).Width;
                }
            } else
            {
                JoueurBras.Fill = new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Ressources/ArmeT1Droite.png")));
                JoueurBras.Height = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Ressources/ArmeT1Droite.png")).Height;
                JoueurBras.Width = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Ressources/ArmeT1Droite.png")).Width;
                JoueurJambes.Fill = new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Ressources/CommandoJambesDefautDroite.png")));
                JoueurJambes.Height = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Ressources/CommandoJambesDefautDroite.png")).Height;
                JoueurJambes.Width = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Ressources/CommandoJambesDefautDroite.png")).Width;
                if (angle < 30 && angle > 0)
                {
                    JoueurTronc.Fill = new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Ressources/CommandoCorpsHautDroite.png")));
                    JoueurTronc.Height = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Ressources/CommandoCorpsHautDroite.png")).Height;
                    JoueurTronc.Width = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Ressources/CommandoCorpsHautDroite.png")).Width;
                } else
                {
                    JoueurTronc.Fill = new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Ressources/CommandoCorpsDroite.png")));
                    JoueurTronc.Height = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Ressources/CommandoCorpsDroite.png")).Height;
                    JoueurTronc.Width = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Ressources/CommandoCorpsDroite.png")).Width;
                }

            }

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
        public int xAleatoire()
        {
            Random rng = new Random();
            return (rng.Next(FENETRE_LARGEUR-100));
        }

        private void GenPlateformes()
        {
            if (tempsGenPlat == 50)
            {
                Plateforme nouvellePlateforme = new Plateforme(xAleatoire(),0,100,15,(int)vitessePlateforme);
                Canvas.SetLeft(nouvellePlateforme.PlateformeImage, nouvellePlateforme.x);
                Canvas.SetBottom(nouvellePlateforme.PlateformeImage, 0);
                CanvasWPF.Children.Add(nouvellePlateforme.PlateformeImage);
                plateformes.Add( nouvellePlateforme);
                tempsGenPlat = 0;
            }else tempsGenPlat++;
        }

        private void InitPlateformes()
        {
            Plateforme premierePlateforme = new Plateforme((joueur.xCorps - 40), 35, 100, 15, (int)vitessePlateforme);
            Canvas.SetLeft(premierePlateforme.PlateformeImage, premierePlateforme.x);
            Canvas.SetBottom(premierePlateforme.PlateformeImage, 35);
            CanvasWPF.Children.Add(premierePlateforme.PlateformeImage);
            plateformes.Add(premierePlateforme);
            for (int i = 1; i < 7; i++)
            {
                Plateforme nouvellePlateforme = new Plateforme(xAleatoire(), 700 - i*100, 100, 15, (int)vitessePlateforme);
                Canvas.SetLeft(nouvellePlateforme.PlateformeImage, nouvellePlateforme.x);
                Canvas.SetBottom(nouvellePlateforme.PlateformeImage, i*100);
                CanvasWPF.Children.Add(nouvellePlateforme.PlateformeImage);
                plateformes.Add(nouvellePlateforme);
            }
        }

        private void MenuPause()
        {
            if (!echap)
            {
                dispatcherTimer.Stop();
                Menupause.Visibility = Visibility.Visible;
                echap = true;
            }
            else
            {
                Menupause.Visibility = Visibility.Hidden;
                dispatcherTimer.Start();
                echap = false;
            }
        }
    }
}
