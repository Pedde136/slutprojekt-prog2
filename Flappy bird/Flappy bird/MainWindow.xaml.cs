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

namespace Flappy_bird
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        DispatcherTimer gameTimer = new DispatcherTimer();
        //Håller reda på din score
        double score;
        // Sätter gravity till positiv så att fågeln ska falla
        int gravity = 8;
        //Sant eller falskt att spelet är över
        bool gameOver;
        //Lagrar höjden, bredden och platsen för fågeln vilket innebär att vi vet när han träffar röret. Är en hitbox.
        Rect FlappyBirdHitbox;

        public MainWindow()
        {
            InitializeComponent();

            gameTimer.Tick += MainEventTimer;
            //20 anger hur snabbt fågeln flyger.
            gameTimer.Interval = TimeSpan.FromMilliseconds(20);
            StartGame();
        }

        /// <summary>
        /// Själva spelet
        /// </summary>
        private void MainEventTimer(object sender, EventArgs e)
        {
            //Här visas och läggs poängen till
            txtScore.Content = "Score: " + score;
            //Betämmer fågelns hitbox
            FlappyBirdHitbox = new Rect(Canvas.GetLeft(FlappyBird), Canvas.GetTop(FlappyBird), FlappyBird.Width - 5, FlappyBird.Height);
            //Här lägger vi till gravitation.
            Canvas.SetTop(FlappyBird, Canvas.GetTop(FlappyBird) + gravity);

            //Om fågeln träffar någon border så förlorar man.
            if (Canvas.GetTop(FlappyBird) < -10 || Canvas.GetTop(FlappyBird) > 458) 
            {
                EndGame();
            }

            //ger rören och målnen rörlighet
            foreach (var x in MyCanvas.Children.OfType<Image>())
            {
                if ((string)x.Tag == "obs1" || (string)x.Tag == "obs2" || (string)x.Tag == "obs3") 
                {
                    //Pipespeed
                    Canvas.SetLeft(x, Canvas.GetLeft(x) - 5);

                    //generarar om alla pipes efter att de har passerat fågeln.
                    if (Canvas.GetLeft(x) < -100) 
                    {
                        Canvas.SetLeft(x, 800);

                        //ett rör är värt 0.5 poäng, och därmed att man flyger över 2st så får man 1 poäng efter varje par.
                        score += 0.5; 
                    }

                    // Här anges hitbox för alla rör
                    Rect pipeHitBox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);

                    // Om fågeln träffar rören så förlorar man.
                    if (FlappyBirdHitbox.IntersectsWith(pipeHitBox))
                    {
                        EndGame();
                    }
                }

                if ((string)x.Tag == "cloud")
                {
                    //Anger molnens fart
                    Canvas.SetLeft(x, Canvas.GetLeft(x) - 2);

                    //Om målnet åker förbi vår canvas så genereras den om igen åt höger.
                    if (Canvas.GetLeft(x) < -250)
                    {
                        Canvas.SetLeft(x, 550);
                    }
                }
            }
        }

        /// <summary>
        /// Fågeln flyger upp när man trycker på space
        /// </summary>
        private void KeyIsDown(object sender, KeyEventArgs e) 
        {
            //Binder space till att fågeln ska flyga upp.
            if (e.Key == Key.Space) 
            {
                // rotation för fågeln
                FlappyBird.RenderTransform = new RotateTransform(-20, FlappyBird.Width / 2, FlappyBird.Height / 2 );
                //istället för att fågeln ska gå ner går den upp då man ändrar på graviteten.
                gravity = -8; 
            }

            //Om man trycker på R och spelet är över, så kan man börja om igen
            if (e.Key == Key.R && gameOver == true) 
            {
                StartGame();
            }

        }

        /// <summary>
        /// Om man inte trycker på något åker fågeln nedåt
        /// </summary>
        private void KeyIsUp(object sender, KeyEventArgs e) 
        {
            FlappyBird.RenderTransform = new RotateTransform(5, FlappyBird.Width / 2, FlappyBird.Height / 2);
            gravity = 8;
        }

        /// <summary>
        /// Startar spelet med default värden
        /// </summary>
        private void StartGame() 
        {
            //Håller canvasen i fokus
            MyCanvas.Focus(); 

            int temp = 300;

            //start poängen är 0
            score = 0;  

            gameOver = false;
            //Startvärdet på fågeln
            Canvas.SetTop(FlappyBird, 190);
            //gör så att flyttning mellan bilderna som lagts in kan ske
            foreach (var x in MyCanvas.Children.OfType<Image>()) 
            {
                //Om det är obs1 ska den vara på position 500.
                if ((string)x.Tag == "obs1") 
                    {
                    Canvas.SetLeft(x, 500);
                    }
                //Om det är obs2 ska den vara på position 800
                if ((string)x.Tag == "obs2") 
                {
                    Canvas.SetLeft(x, 800);
                    }
                //Om det är obs3 ska den vara på position 1100
                if ((string)x.Tag == "obs3")
                {
                    Canvas.SetLeft(x, 1100);
                }

                //anger molnens startposition
                if ((string)x.Tag == "cloud") 
                {
                    Canvas.SetLeft(x, 300 + temp);
                    temp = 800;
                }

            }
            gameTimer.Start(); 
        }

        /// <summary>
        /// //spelet avslutas
        /// </summary>
        private void EndGame() 
        {
            gameTimer.Stop();
            gameOver = true;
            txtScore.Content += "  Game Over! Press 'R' to try again.";
        }
    }
}
