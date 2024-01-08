using System;
using System.Collections.Generic;
using System.Linq;
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
        public static bool droite, gauche, haut;
        private Joueur? joueur;
        private DispatcherTimer dispatcherTimer = new DispatcherTimer();


        public MainWindow()
        {
            InitializeComponent();
            Init(); 
        }
        public void Init()
        {
            CanvasWPF.Focus();  
            this.joueur = new Joueur(10,5);
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
            Canvas.SetLeft(JoueurSprite, joueur.x);
            Canvas.SetTop(JoueurSprite, joueur.y);
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
