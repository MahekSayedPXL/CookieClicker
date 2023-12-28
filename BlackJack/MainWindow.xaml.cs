using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        float score = 0f;

        int cursorCost = 5;
        int grandmaCost = 10;
        int farmCost = 15;
        int mineCost = 20;
        int factoryCost = 25;
        int bankCost = 30;
        int templeCost = 35;

        int cursorCount = 0;
        int grandmaCount = 0;
        int farmCount = 0;
        int mineCount = 0;
        int factoryCount = 0;
        int bankCount = 0;
        int templeCount = 0;

        public MainWindow()
        {
            InitializeComponent();
            UpdateScoreText();
            UpdateButtonStatus();

            incomeTimer = new DispatcherTimer();
            incomeTimer.Tick += IncomeTimer_Tick;
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
            if (score < 1000)
            {
                lblScore.Content = score;
            }
            if (score < 1000000)
            {
                string spaceNumber = NumberWithSpace(score);
                lblScore.Content = spaceNumber;
            }
            else
            {
                string largeScore = NameLargeNumber(score);
                lblScore.Content = largeScore;
            }
        }

        private void UpdateButtonStatus()
        {
            float totalCookiesCollected = score + (cursorCount * cursorCost) +
                                   (grandmaCount * grandmaCost) + (farmCount * farmCost) +
                                   (mineCount * mineCost) + (factoryCount * factoryCost) +
                                   (bankCount * bankCost) + (templeCount * templeCost);
            
            btnCursor.IsEnabled = false;
            btnGrandma.IsEnabled = false;
            btnFarm.IsEnabled = false;
            btnMine.IsEnabled = false;
            btnFactory.IsEnabled = false;
            btnBank.IsEnabled = false;
            btnTemple.IsEnabled = false;

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
            if (score >= bankCost)
            {
                btnBank.IsEnabled = true;
            }
            if (score >= templeCost)
            {
                btnTemple.IsEnabled = true;
            }

            btnCursor.Visibility = Visibility.Hidden;
            btnGrandma.Visibility = Visibility.Hidden;
            btnFarm.Visibility = Visibility.Hidden;
            btnMine.Visibility = Visibility.Hidden;
            btnFactory.Visibility = Visibility.Hidden;
            btnBank.Visibility = Visibility.Hidden;
            btnTemple.Visibility = Visibility.Hidden;

            // Visible buttons based on score
            if (totalCookiesCollected >= cursorCost)
            {
                btnCursor.Visibility = Visibility.Visible;
            }
            if (totalCookiesCollected >= grandmaCost)
            {
                btnGrandma.Visibility = Visibility.Visible;
            }
            if (totalCookiesCollected >= farmCost)
            {
                btnFarm.Visibility = Visibility.Visible;
            }
            if (totalCookiesCollected >= mineCost)
            {
                btnMine.Visibility = Visibility.Visible;
            }
            if (totalCookiesCollected >= factoryCost)
            {
                btnFactory.Visibility = Visibility.Visible;
            }
            if (totalCookiesCollected >= bankCost)
            {
                btnBank.Visibility = Visibility.Visible;
            }
            if (totalCookiesCollected >= templeCost)
            {
                btnTemple.Visibility = Visibility.Visible;
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
                        UpdatePassiveIncome();
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
                        UpdatePassiveIncome();
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
                        UpdatePassiveIncome();
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
                        UpdatePassiveIncome();
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
                        UpdatePassiveIncome();
                    }
                    break;
                case "btnBank":
                    if (score >= bankCost)
                    {
                        score -= bankCost;
                        bankCount++;
                        lblNumberOfBank.Content = bankCount.ToString();
                        bankCost = CalculateCost(bankCount, bankCost);
                        lblBankCost.Content = bankCost.ToString();
                        UpdatePassiveIncome();
                    }
                    break;
                case "btnTemple":
                    if (score >= templeCost)
                    {
                        score -= templeCost;
                        templeCount++;
                        lblNumberOfTemple.Content = templeCount.ToString();
                        templeCost = CalculateCost(templeCount, templeCost);
                        lblTempleCost.Content = templeCost.ToString();
                        UpdatePassiveIncome();
                    }
                    break;
                default:
                    break;
            }
            
            UpdateScoreText();
            UpdateButtonStatus();
        }

        private int CalculateCost(int count, int basicCost)
        {
            double result = basicCost * Math.Pow(1.15, count);
            return (int)Math.Ceiling(result);
        }

        private void IncomeTimer_Tick(object sender, EventArgs e)
        {
            UpdatePassiveIncome();
            score += passiveIncome * 0.01f;

            UpdateScoreText();
            UpdateButtonStatus();
        }

        private void UpdatePassiveIncome()
        {
            passiveIncome = cursorCount * 0.001f +
                                  grandmaCount * 0.01f +
                                  farmCount * 0.08f +
                                  mineCount * 0.47f +
                                  factoryCount * 2.60f +
                                  bankCount * 14f +
                                  templeCount * 78f;

            //passiveIncome = (float)Math.Round(passiveIncome, 2);

            lblCookiesPerSec.Content = passiveIncome.ToString();
        }

        private string NameLargeNumber(float number)
        {
            string[] terms = {"", "Miljoen", "Miljard", "Biljoen", "Biljard", "Triljoen"};

            int termsIndex = 0;
            while (number >= 1000000 && termsIndex < terms.Length - 1)
            {
                number /= 1000000;
                termsIndex++;
            }

            string largeNumber = $"{number:N3} {terms[termsIndex]}";
            return largeNumber;
        }

        private string NumberWithSpace(float number)
        {
            string spaceNumber = $"{number:N0}".Replace(".", " ");
            return spaceNumber;
        }

        public class InputDialog : Window
        {
            private TextBox txtInput;
            private Button btnOK;
            private Button btnCancel;

            public string InputValue { get; private set; }

            public InputDialog(string title, string prompt)
            {
                Title = title;
                Width = 300;
                Height = 150;

                Grid grid = new Grid();

                txtInput = new TextBox { Margin = new Thickness(10, 30, 0, 0), Width = 200 };
                btnOK = new Button { Content = "OK", Width = 75, Height = 25, Margin = new Thickness(5) };
                btnCancel = new Button { Content = "Cancel", Width = 75, Height = 25, Margin = new Thickness(5) };

                btnOK.Click += (sender, e) =>
                {
                    InputValue = txtInput.Text;
                    DialogResult = true;
                };

                btnCancel.Click += (sender, e) => DialogResult = false;

                grid.Children.Add(new Label { Content = prompt, Margin = new Thickness(10) });
                grid.Children.Add(txtInput);
                grid.Children.Add(new StackPanel
                {
                    Orientation = Orientation.Horizontal,
                    HorizontalAlignment = HorizontalAlignment.Right,
                    VerticalAlignment = VerticalAlignment.Bottom,
                    Children = { btnOK, btnCancel }
                });

                Content = grid;
            }
        }

        private void BakeryName_MouseUp(object sender, MouseButtonEventArgs e)
        {
            InputDialog dialog = new InputDialog("Change Bakery Name", "Enter a new bakery name:");

            if (dialog.ShowDialog() == true)
            {
                string newBakeryName = dialog.InputValue.Trim();

                if (!string.IsNullOrWhiteSpace(newBakeryName))
                {
                    lblBakeryName.Content = newBakeryName;
                    MessageBox.Show("Bakery name changed successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Invalid bakery name. Please enter a valid name.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}

