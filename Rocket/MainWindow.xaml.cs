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

namespace Rocket
{
    public class Vector
    {
        public double X;
        public double Y;
        public double Rot;
    }


/// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Vector Position;
        private Vector Velocity;

        private DateTime previousTick;

        private DispatcherTimer TickTimer;

        private bool LeftKeyPressed;
        private bool RightKeyPressed;
        private bool UpKeyPressed;

        public MainWindow()
        {
            InitializeComponent();

            Position = new Vector();
            Velocity = new Vector();

            Position.X = Canvas.GetLeft(RocketImage);
            Position.Y = Canvas.GetTop(RocketImage);
            Position.Rot = 0;

            Velocity.X = 0;
            Velocity.Y = 0;
            Velocity.Rot = 0;

            previousTick = DateTime.Now;

            TickTimer = new DispatcherTimer();
            TickTimer.Interval = TimeSpan.FromSeconds(0.01);
            TickTimer.Tick += Tick;
            TickTimer.Start();
        }

        public void Tick(object o, EventArgs e)
        {
            DateTime currentTick = DateTime.Now;
            double timeStep = (currentTick - previousTick).TotalSeconds;

            if (LeftKeyPressed)
            {
                Velocity.Rot -= 400 * timeStep;
            }
            if (RightKeyPressed)
            {
                Velocity.Rot += 400 * timeStep;
            }
            if (UpKeyPressed)
            {
                Velocity.X += 400 * Math.Sin(Position.Rot * Math.PI / 180) * timeStep;
                Velocity.Y -= 400 * Math.Cos(Position.Rot * Math.PI / 180) * timeStep;
            }

            Position.X = Position.X + Velocity.X * timeStep;
            Position.Y = Position.Y + Velocity.Y * timeStep;
            Position.Rot = Position.Rot + Velocity.Rot * timeStep;

            Canvas.SetLeft(RocketImage, Position.X);
            Canvas.SetTop(RocketImage, Position.Y);
            RocketImage.RenderTransform = new RotateTransform(Position.Rot);

            previousTick = currentTick;
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left)
            {
                LeftKeyPressed = true;
            }
            else if (e.Key == Key.Right)
            {
                RightKeyPressed = true;
            }
            else if (e.Key == Key.Up)
            {
                UpKeyPressed = true;
            }
        }

        private void Window_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left)
            {
                LeftKeyPressed = false;
            }
            else if (e.Key == Key.Right)
            {
                RightKeyPressed = false;
            }
            else if (e.Key == Key.Up)
            {
                UpKeyPressed = false;
            }

        }
    }
}
