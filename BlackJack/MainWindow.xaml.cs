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

        private Random random = new Random();
        private DispatcherTimer goldenCookieTimer;
        private bool isGoldenCookieActive = false;

        double score = 0;

        int cursorCost = 1;
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

        private List<Quest> quests;

        public class Quest
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public Func<bool> IsCompleted { get; set; }
            public string CompletionMessage { get; set; }
            public bool IsCompletionMessageShown { get; set; } 
        }

        public MainWindow()
        {
            InitializeComponent();
            UpdateScoreText();
            UpdateButtonStatus();

            incomeTimer = new DispatcherTimer();
            incomeTimer.Tick += IncomeTimer_Tick;
            incomeTimer.Interval = TimeSpan.FromMilliseconds(10);
            incomeTimer.Start();

            goldenCookieTimer = new DispatcherTimer();
            goldenCookieTimer.Tick += GoldenCookieTimer_Tick;
            goldenCookieTimer.Interval = TimeSpan.FromMinutes(1);
            goldenCookieTimer.Start();

            quests = new List<Quest>
            {
                new Quest
                {
                    Name = "Cookie Fields Attract Attention",
                    Description = "Bereik 20 cookies per seconde",
                    IsCompleted = () => passiveIncome >= 5,
                    CompletionMessage = "Je winkel begint op gang te komen en je trekt volk van heel het dorp naar je bakkerij."
                },
                new Quest
                {
                    Name = "Grandma Bingo Night",
                    Description = "Koop 10 grandmas",
                    IsCompleted = () => grandmaCount >= 10,
                    CompletionMessage = "In de avond organiseer je bingo-spellen om grootmoeders van heel het dorp te trekken naar je bakkerij. De gratis werkkracht is een succesvol businessmodel."
                },
                new Quest
                {
                    Name = "Cookie Field Notoriety",
                    Description = "Bereik 100 cookies per seconde",
                    IsCompleted = () => passiveIncome >= 100,
                    CompletionMessage = "Je cookie velden zijn berucht. Je verschijnt dagelijks in de krant over je befaamde cookie planten die je in je akkers teelt."
                },
                new Quest
                {
                    Name = "Automated Factories",
                    Description = "Koop 10 factories",
                    IsCompleted = () => factoryCount >= 10,
                    CompletionMessage = "The flesh is weak. Je zet alle arbeiders aan de deur. Vanaf vandaag zijn je fabrieken volledig geautomatiseerd."
                },
                new Quest
                {
                    Name = "Cookie Tycoon Rising",
                    Description = "Bereik 500 cookies per seconde",
                    IsCompleted = () => passiveIncome >= 500,
                    CompletionMessage = "Het dorp spreekt over je als de Cookie Tycoon. De plaatselijke economie draait volledig om jouw bakkerij."
                },
                new Quest
                {
                    Name = "Mining Bonanza",
                    Description = "Koop 20 mijnen",
                    IsCompleted = () => mineCount >= 20,
                    CompletionMessage = "Je mijnactiviteiten zijn een bonanza geworden. Het dorp is afhankelijk van je waardevolle mijnproducten."
                },
                new Quest
                {
                    Name = "Banking Empire",
                    Description = "Koop 50 banken",
                    IsCompleted = () => bankCount >= 50,
                    CompletionMessage = "Je mijnactiviteiten zijn een bonanza geworden. Het dorp is afhankelijk van je waardevolle mijnproducten."
                },
            };
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
            double totalCookiesCollected = score + (cursorCount * cursorCost) +
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

                        BuyItem("/cursor-removebg-preview (1).png", "Cursor");
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

                        BuyItem("/grandma-removebg-preview.png", "Grandma");
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

                        BuyItem("/farm-removebg-preview (1).png", "Farm");
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

                        BuyItem("/mine-removebg-preview.png", "Mine");
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
                        BuyItem("/factory-removebg-preview.png", "Factory");
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

                        BuyItem("/bank-removebg-preview.png", "Bank");
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

                        BuyItem("/temple-removebg-preview.png", "Temple");
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

            foreach (var quest in quests)
            {
                if (!quest.IsCompleted() || quest.IsCompletionMessageShown) continue;

                MessageBox.Show(quest.CompletionMessage, "Quest Completed", MessageBoxButton.OK, MessageBoxImage.Information);
                quest.IsCompletionMessageShown = true;
            }
        }

        private void UpdatePassiveIncome()
        {
            passiveIncome = cursorCount * 0.1f +
                                  grandmaCount * 1f +
                                  farmCount * 8f +
                                  mineCount * 47f +
                                  factoryCount * 260f +
                                  bankCount * 1400f +
                                  templeCount * 7800f;

            //passiveIncome = (float)Math.Round(passiveIncome, 2);
            lblCookiesPerSec.Content = passiveIncome.ToString();
        }

        private string NameLargeNumber(double number)
        {
            string[] terms = { "", "Miljoen", "Miljard", "Biljoen", "Biljard", "Triljoen" };

            int termsIndex = 0;
            while (number >= 1000 && termsIndex < terms.Length - 1)
            {
                number /= 1000;
                termsIndex++;
            }

            string largeNumber = $"{number:N3} {terms[termsIndex]}";
            return largeNumber;
        }

        private string NumberWithSpace(double number)
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

        private void BuyItem(string imagePath, string category)
        {
            Image newItemImage = new Image
            {
                Source = new BitmapImage(new Uri(imagePath, UriKind.Relative))
                //Width = 30,
                //Height = 30
            };

            // Determine the appropriate WrapPanel based on the category
            WrapPanel targetWrapPanel = GetWrapPanelByCategory(category);

            // Add the new item image to the determined WrapPanel
            targetWrapPanel.Children.Add(newItemImage);
        }

        private WrapPanel GetWrapPanelByCategory(string category)
        {
            // Choose the correct WrapPanel based on the category
            switch (category)
            {
                case "Cursor":
                    return wrapPanelCursor;
                case "Grandma":
                    return wrapPanelGrandma;
                case "Farm":
                    return wrapPanelFarm;
                case "Mine":
                    return wrapPanelMine;
                case "Factory":
                    return wrapPanelFactory;
                case "Bank":
                    return wrapPanelBank;
                case "Temple":
                    return wrapPanelTemple;
                default:
                    // You can handle a default case or raise an error based on your requirements
                    throw new ArgumentException($"Unknown category: {category}");
            }
        }

        private void GoldenCookieTimer_Tick(object sender, EventArgs e)
        {
            if (random.NextDouble() < 0.3 && !isGoldenCookieActive)
            {
                SpawnGoldenCookie();
            }
        }

        private void SpawnGoldenCookie()
        {
            isGoldenCookieActive = true;

            Image goldenCookieImage = new Image
            {
                Source = new BitmapImage(new Uri("/golden_cookie-removebg-preview.png", UriKind.Relative)),
                Width = 50,
                Height = 50
            };

            Canvas.SetLeft(goldenCookieImage, random.Next((int)this.ActualWidth - 50));
            Canvas.SetTop(goldenCookieImage, random.Next((int)this.ActualHeight - 50));

            mainCanvas.Children.Add(goldenCookieImage);

            goldenCookieImage.MouseUp += GoldenCookieImage_MouseUp;

            DispatcherTimer disappearTimer = new DispatcherTimer();
            disappearTimer.Tick += (s, e) =>
            {
                mainCanvas.Children.Remove(goldenCookieImage);
                isGoldenCookieActive = false;
            };
            disappearTimer.Interval = TimeSpan.FromSeconds(15);
            disappearTimer.Start();
        }

        private void GoldenCookieImage_MouseUp(object sender, MouseButtonEventArgs e)
        {
            mainCanvas.Children.Remove((UIElement)sender);
            isGoldenCookieActive = false;

            score += passiveIncome * 15 * 60;

            UpdateScoreText();
            UpdateButtonStatus();
        }


    }
}
