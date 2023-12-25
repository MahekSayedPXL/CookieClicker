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
        int score = 0;

        public MainWindow()
        {
            InitializeComponent();
            UpdateScoreText();
        }

        private void CookiePress_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (imgCookie.Width < ActualWidth)
            {
                imgCookie.Width += 10;
            }

            if (imgCookie.Height < ActualWidth)
            {
                imgCookie.Height += 10;
            }
            UpdateScoreText();
        }

        private void CookieLose_MouseDown(object sender, MouseButtonEventArgs e)
        {
            score += 1;
            if (imgCookie.Width > ActualWidth)
            {
                imgCookie.Width -= 10;
            }

            if (imgCookie.Height > ActualWidth)
            {
                imgCookie.Height -= 10;
            }
        }

        private void UpdateScoreText()
        {
            lblScore.Content = score.ToString();
        }
    }
}
