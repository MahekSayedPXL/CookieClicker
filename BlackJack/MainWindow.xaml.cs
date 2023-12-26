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

namespace BlackJack
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private float passiveIncome = 0.0f;
        private DispatcherTimer incomeTimer;

        float score = 0;

        int cursorCost = 15;
        int grandmaCost = 10;
        int farmCost = 15;
        int mineCost = 20;
        int factoryCost = 25;

        int cursorCount = 0;
        int grandmaCount = 0;
        int farmCount = 0;
        int mineCount = 0;
        int factoryCount = 0;


        public MainWindow()
        {
            InitializeComponent();
            UpdateScoreText();
            UpdateButtonStatus();

            incomeTimer = new DispatcherTimer();
            incomeTimer.Tick += new EventHandler(IncomeTimer_Tick);
            incomeTimer.Interval = TimeSpan.FromMilliseconds(10);
            incomeTimer.Start();
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
                        cursorCount++;
                        lblNumberOfCursor.Content = cursorCount.ToString();
                        cursorCost = CalculateCost(cursorCount, cursorCost);
                        lblCursorCost.Content = cursorCost.ToString();
                    }
                    break;
                case "btnGrandma":
                    if (score >= grandmaCost)
                    {
                        score -= grandmaCost;
                        grandmaCount++;
                        lblNumberOfGrandma.Content = grandmaCount.ToString();
                        grandmaCost = CalculateCost(grandmaCount, grandmaCost);
                        lblGrandmaCost.Content = grandmaCost.ToString();
                    }
                    break;
                case "btnFarm":    
                    if (score >= farmCost)
                    {
                        score -= farmCost;
                        farmCount++;
                        lblNumberOfFarm.Content = farmCount.ToString();
                        farmCost = CalculateCost(farmCount, farmCost);
                        lblFarmCost.Content = farmCost.ToString();
                    }
                    break;
                case "btnMine":
                    if (score >= mineCost)
                    {
                        score -= mineCost;
                        mineCount++;
                        lblNumberOfMine.Content = mineCount.ToString();
                        mineCost = CalculateCost(mineCount, mineCost);
                        lblMineCost.Content = mineCost.ToString();
                    }
                    break;
                case "btnFactory":
                    if (score >= factoryCost)
                    {
                        score -= factoryCost;
                        factoryCount++;
                        lblNumberOfFactory.Content = factoryCount.ToString();
                        factoryCost = CalculateCost(factoryCount, factoryCost);
                        lblFactoryCost.Content = factoryCost.ToString();
                    }
                    break;
                default:
                    break;
            }
            
            UpdateScoreText();
            UpdateButtonStatus();
            UpdatePassiveIncome();
        }

        private int CalculateCost(int count, int basicCost)
        {
            double result = basicCost * Math.Pow(1.15, count);
            return (int)Math.Ceiling(result);
        }

        private void IncomeTimer_Tick(object sender, EventArgs e)
        {
            score += passiveIncome * 0.01f;
            score = (float)Math.Round(score, 2);

            UpdateScoreText();
            UpdateButtonStatus();
        }

        private void UpdatePassiveIncome()
        {
            passiveIncome = cursorCount * 0.001f +
                            grandmaCount * 0.01f +
                            farmCount * 0.08f +
                            mineCount * 0.47f +
                            factoryCount * 2.60f;
        }
    }
}
