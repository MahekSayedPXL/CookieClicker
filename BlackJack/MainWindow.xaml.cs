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

namespace BlackJack
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        float score = 0;

        int cursorCost = 5;
        int grandmaCost = 10;
        int farmCost = 15;
        int mineCost = 20;
        int factoryCost = 25;

        public MainWindow()
        {
            InitializeComponent();
            UpdateScoreText();
            UpdateButtonStatus();
        }

        private void CookiePress_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (imgCookie.Width < 260)
            {
                imgCookie.Width += 10;
            }

            if (imgCookie.Height < 260)
            {
                imgCookie.Height += 10;
            }

            UpdateScoreText();
            UpdateButtonStatus();
        }

        private void CookieLose_MouseDown(object sender, MouseButtonEventArgs e)
        {
            score += 1;
            if (imgCookie.Width > 250)
            {
                imgCookie.Width -= 10;
            }

            if (imgCookie.Height > 250)
            {
                imgCookie.Height -= 10;
            }
        }

        private void UpdateScoreText()
        {
            lblScore.Content = score.ToString();
        }

        private void UpdateButtonStatus()
        {
            btnCursor.IsEnabled = false;
            btnGrandma.IsEnabled = false;
            btnFarm.IsEnabled = false;
            btnMine.IsEnabled = false;
            btnFactory.IsEnabled = false;

            // Enable buttons based on score
            if (score >= cursorCost)
            {
                btnCursor.IsEnabled = true;
            }
            if (score >= grandmaCost)
            {
                btnGrandma.IsEnabled = true;
            }
            if (score >= farmCost)
            {
                btnFarm.IsEnabled = true;
            }
            if (score >= mineCost)
            {
                btnMine.IsEnabled = true;
            }
            if (score >= factoryCost)
            {
                btnFactory.IsEnabled = true;
            }
        }

        private void btnBuy_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = (Button)sender;

            switch (clickedButton.Name)
            {
                case "btnCursor":
                    if (score >= cursorCost)
                    {
                        score -= cursorCost;
                    }
                    break;
                case "btnGrandma":
                    if (score >= grandmaCost)
                    {
                        score -= grandmaCost;
                    }
                    break;
                case "btnFarm":    
                    if (score >= farmCost)
                    {
                        score -= farmCost;
                    }
                    break;
                case "btnMine":
                    if (score >= mineCost)
                    {
                        score -= mineCost;
                    }
                    break;
                case "btnFactory":
                    if (score >= factoryCost)
                    {
                        score -= factoryCost;
                    }
                    break;
                default:
                    break;
            }
            
            UpdateScoreText();
            UpdateButtonStatus();
        }
    }
}
