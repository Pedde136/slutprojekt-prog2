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
        double score; //Håller reda på din score
        int gravity = 8; // Sätter gravity till positiv så att fågeln ska falla
        bool gameOver; //Sant eller falskt att spelet är över
        Rect FlappyBirdHitbox; //Lagrar höjden, bredden och platsen för fågeln vilket innebär att vi vet när han träffar röret. Är en hitbox.

        public MainWindow()
        {
            InitializeComponent();

            gameTimer.Tick += MainEventTimer;
            gameTimer.Interval = TimeSpan.FromMilliseconds(20);//20 anger hur snabbt fågeln flyger.
            StartGame();
        }

        private void MainEventTimer(object sender, EventArgs e)
        {
            txtScore.Content = "Score: " + score; //Här visas och läggs poängen till

            FlappyBirdHitbox = new Rect(Canvas.GetLeft(FlappyBird), Canvas.GetTop(FlappyBird), FlappyBird.Width - 5, FlappyBird.Height); //Betämmer fågelns hitbox

            Canvas.SetTop(FlappyBird, Canvas.GetTop(FlappyBird) + gravity); //Här lägger vi till gravitation.

            if (Canvas.GetTop(FlappyBird) < -10 || Canvas.GetTop(FlappyBird) > 458) //Om fågeln träffar någon border så förlorar man.
            {
                EndGame();
            }

            foreach (var x in MyCanvas.Children.OfType<Image>()) //ger rören och målnen rörlighet
            {
                if ((string)x.Tag == "obs1" || (string)x.Tag == "obs2" || (string)x.Tag == "obs3") 
                {
                    Canvas.SetLeft(x, Canvas.GetLeft(x) - 5); //Pipespeed

                    if (Canvas.GetLeft(x) < -100) //generarar om alla pipes efter att de har passerat fågeln.
                    {
                        Canvas.SetLeft(x, 800);

                        score += 0.5; //ett rör är värt 0.5 poäng, och därmed att man flyger över 2st så får man 1 poäng efter varje par.
                    }

                    Rect pipeHitBox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);// Här anges hitbox för alla rör

                    if (FlappyBirdHitbox.IntersectsWith(pipeHitBox))// Om fågeln träffar rören så förlorar man.
                    {
                        EndGame();
                    }
                }

                if ((string)x.Tag == "cloud")
                {
                    Canvas.SetLeft(x, Canvas.GetLeft(x) - 2); //Anger molnens fart

                    if (Canvas.GetLeft(x) < -250)//Om målnet åker förbi vår canvas så genereras den om igen åt höger.
                    {
                        Canvas.SetLeft(x, 550);
                    }
                }
            }
        }

        private void KeyIsDown(object sender, KeyEventArgs e) //hur fågeln går upp (flyger)
        {
            if(e.Key == Key.Space) //Binder space till att fågeln ska flyga upp.
            {
                FlappyBird.RenderTransform = new RotateTransform(-20, FlappyBird.Width / 2, FlappyBird.Height / 2 );// rotation för fågeln
                gravity = -8; //istället för att fågeln ska gå ner går den upp då man ändrar på graviteten.
            }

            if (e.Key == Key.R && gameOver == true) //Om man trycker på R och spelet är över, så kan man börja om igen
            {
                StartGame();
            }

        }

        private void KeyIsUp(object sender, KeyEventArgs e) // Om man inte trycker på space så vinklas fågeln nedåt.
        {
            FlappyBird.RenderTransform = new RotateTransform(5, FlappyBird.Width / 2, FlappyBird.Height / 2);
            gravity = 8;
        }

        private void StartGame() //Startar spelet med default värden
        {

            MyCanvas.Focus(); //Håller canvasen i fokus 

            int temp = 300; 

            score = 0; //start poängen är 0 

            gameOver = false;
            Canvas.SetTop(FlappyBird, 190); //Startvärdet på fågeln

            foreach (var x in MyCanvas.Children.OfType<Image>()) //gör så att flyttning mellan bilderna som lagts in kan ske
            {

                if ((string)x.Tag == "obs1") //Om det är obs1 ska den vara på position 500. 
                    {
                    Canvas.SetLeft(x, 500);
                    }

                if ((string)x.Tag == "obs2") //Om det är obs2 ska den vara på position 800
                {
                    Canvas.SetLeft(x, 800);
                    }

                if ((string)x.Tag == "obs3") //Om det är obs3 ska den vara på position 1100
                {
                    Canvas.SetLeft(x, 1100);
                }


                if ((string)x.Tag == "cloud") //anger molnens startposition
                {
                    Canvas.SetLeft(x, 300 + temp);
                    temp = 800;
                }

            }
            gameTimer.Start(); 
        }

        private void EndGame() //spelet avslutas
        {
            gameTimer.Stop();
            gameOver = true;
            txtScore.Content += "  Game Over! Press 'R' to try again.";
        }
    }
}
