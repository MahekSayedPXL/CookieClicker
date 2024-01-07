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
    /// ------------------------------------------------
    /// Description: Documentatie Cookie Clicker project
    /// ------------------------------------------------

    /// <summary>
    /// Represents the main window of the cookie-clicker game
    /// </summary>
    public partial class MainWindow : Window
    {
        private double passiveIncome = 0.0f;
        //Timer for updating the passive income
        private DispatcherTimer incomeTimer;

        private Random random = new Random();
        //timer for managing the appearance of golden cookies
        private DispatcherTimer goldenCookieTimer;
        //Indicating whether a golden cookie is currently active
        private bool isGoldenCookieActive = false;

        double score = 999990;

        int cursorCost = 15;
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

        double cursorSpeed = 0.1;
        double grandmaSpeed = 1;
        double farmSpeed = 8;
        double mineSpeed = 41;
        double factorySpeed = 260;
        double bankSpeed = 1400;
        double templeSpeed = 7800;

        double cursorMultiplier = 1;
        double grandmaMultiplier = 1;
        double farmMultiplier = 1;
        double mineMultiplier = 1;
        double factoryMultiplier = 1;
        double bankMultiplier = 1;
        double templeMultiplier = 1;

        double cursorBonusCost = 0;
        double grandmaBonusCost = 0;
        double farmBonusCost = 0;
        double mineBonusCost = 0;
        double factoryBonusCost = 0;
        double bankBonusCost = 0;
        double templeBonusCost = 0;

        //Quests to achieve in the game
        private List<Quest> quests;

        /// <summary>
        /// Represents a quest in the game
        /// </summary>
        public class Quest
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public Func<bool> IsCompleted { get; set; }
            public string CompletionMessage { get; set; }
            public bool IsCompletionMessageShown { get; set; }

            public string QuestName { get; set; }
            public string Notification { get; set; }
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

            //bonusManager = new BonusManager(this);

            // Initialize quests for the game
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
                    CompletionMessage = "Je bankimperium domineert de financiële wereld. Het dorp is nu een welvarende stad dankzij jou."
                },
                new Quest
                {
                    Name = "Temple of Cookies",
                    Description = "Koop 5 tempels",
                    IsCompleted = () => bankCount >= 50,
                    CompletionMessage = "Je hebt nu een tempelgebied. Mensen maken bedevaarten om je heilige cookies te proeven."
                },
                new Quest
                {
                    Name = "Farmland Frenzy",
                    Description = "Koop 30 boerderijen",
                    IsCompleted = () => farmCount >= 30,
                    CompletionMessage = "Je boerderijen zijn een ware frenzy geworden. Het dorp wordt gevoed door je overvloedige oogst."
                },
                new Quest
                {
                    Name = "Cookie Celeb",
                    Description = "Bereik 5,000 cookies per seconde.",
                    IsCompleted = () => passiveIncome >= 5000,
                    CompletionMessage = "Je bent een beroemdheid geworden. Paparazzi volgen je elke beweging, allemaal vanwege je onweerstaanbare cookies."
                },
                new Quest
                {
                    Name = "Quantum Factories",
                    Description = "Koop 100 factories",
                    IsCompleted = () => factoryCount >= 100,
                    CompletionMessage = "Je fabrieken werken nu op een quantumniveau. Productiviteit is niet te bevatten."
                },
                new Quest
                {
                    Name = "Epic Farmland Uprising",
                    Description = "Koop 100 boerderijen",
                    IsCompleted = () => farmCount >= 100,
                    CompletionMessage = "Je boerderijen hebben een epische opstand veroorzaakt. Het dorp is getuige van een landbouwhervorming dankzij jou."
                },
                new Quest
                {
                    Name = "Cookie Nebula",
                    Description = "Bereik 10,000 cookies per seconde",
                    IsCompleted = () => passiveIncome >= 10000,
                    CompletionMessage = "Je cookies zijn nu een hemels fenomeen. Mensen beweren dat ze sterrenstelsels proeven bij elke hap."
                },
                new Quest
                {
                    Name = "Grandma Revolution",
                    Description = "Koop 100 grandmas",
                    IsCompleted = () => grandmaCount >= 100,
                    CompletionMessage = "Grootmoeders hebben een revolutie ontketend. Ze eisen koekjesgelijkheid voor iedereen."
                },
                new Quest
                {
                    Name = "Global Banking Network",
                    Description = "Koop 200 banken",
                    IsCompleted = () => bankCount >= 200,
                    CompletionMessage = "Je hebt een wereldwijd bankennetwerk opgezet. Financiële stabiliteit is nu jouw nalatenschap."
                },
                new Quest
                {
                    Name = "Enlightened Temples",
                    Description = "Koop 20 tempels",
                    IsCompleted = () => templeCount >= 20,
                    CompletionMessage = "Je tempels stralen verlichting uit. Het dorp ziet je als een spirituele leider van de cookie-cultus."
                },
                new Quest
                {
                    Name = "Cookie Galactic Overlord",
                    Description = "Bereik 50,000 cookies per seconde",
                    IsCompleted = () => passiveIncome >= 50000,
                    CompletionMessage = "Je bent nu de heerser van de cookie-galaxie. Wezens van andere werelden smeken om je geheime cookie recept."
                },
                new Quest
                {
                    Name = "Epic Cursor Uprising",
                    Description = "Koop 50 cursors",
                    IsCompleted = () => cursorCount >= 50,
                    CompletionMessage = "Je cursors hebben een epische opstand veroorzaakt. "
                },
                new Quest
                {
                    Name = "Eternal Cookie Emperor",
                    Description = "Bereik 1 miljoen cookies per seconde.",
                    IsCompleted = () => passiveIncome >= 1000000,
                    CompletionMessage = "Je bent nu de eeuwige cookie keizer. Zelfs tijd kan je niet stoppen."
                },
                new Quest
                {
                    Name = "Sacred Temples of Eternity",
                    Description = "Koop 100 tempels",
                    IsCompleted = () => templeCount >= 100,
                    CompletionMessage = "Je tempels zijn nu heiligdommen van eeuwigheid. Het dorp vereert je als de hoeder van tijdloze koekjeswijsheid."
                }
            };
        }

        /// <summary>
        /// Event handler for the cookie-clicking action.
        /// </summary>
        /// <param name="sender">The object that triggered the event.</param>
        /// <param name="e">Mouse button event arguments.</param>
        private void CookiePress_MouseUp(object sender, MouseButtonEventArgs e)
        {
            score += 1;
            if (imgCookie.ActualWidth < 260)
            {
                imgCookie.Width += 10;
            }

            if (imgCookie.ActualHeight < 260)
            {
                imgCookie.Height += 10;
            }

            UpdateScoreText();
            UpdateButtonStatus();
        }

        /// <summary>
        /// Event handler for the cookie-losing action.
        /// </summary>
        /// <param name="sender">The object that triggered the event.</param>
        /// <param name="e">Mouse button event arguments.</param>
        private void CookieLose_MouseDown(object sender, MouseButtonEventArgs e)
        {

            if (imgCookie.ActualWidth > 250)
            {
                imgCookie.Width -= 10;
            }

            if (imgCookie.ActualHeight > 250)
            {
                imgCookie.Height -= 10;
            }
        }

        /// <summary>
        /// Updates the text displaying the current score based on the score value.
        /// </summary>
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

        /// <summary>
        /// <para>
        /// Calculates the total cookies collected
        /// </para>
        /// <para>
        /// Updates button visibility and status based on the current score
        /// </para>
        /// <para>
        /// Manages the visibility of bonus buttons.
        /// </para>
        /// </summary>
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

        /// <summary>
        /// <para>
        /// Handles the click event of the "Buy" button for different game elements
        /// </para>
        /// </summary>
        /// <param name="sender">The object that triggered the event.</param>
        /// <param name="e">Routed event arguments.</param>
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
            UpdateBonusButtonStatus();
        }

        /// <summary>
        /// Calculates the cost of purchasing additional units of a game element based on its count and basic cost.
        /// </summary>
        /// <param name="count">The current count of the game element.</param>
        /// <param name="basicCost">The basic cost of the game element.</param>
        /// <returns>The updated cost after calculation.</returns>
        private int CalculateCost(int count, int basicCost)
        {
            double result = basicCost * Math.Pow(1.15, count);
            return (int)Math.Ceiling(result);
        }

        /// <summary>
        /// <para>
        /// Updates the passive income, score, score text, button status, and checks and displays completed quests
        /// </para>
        /// <para>
        /// Event handler for the income timer tick.
        /// </para>
        /// </summary>
        /// <param name="sender">The object that triggered the event.</param>
        /// <param name="e">Event arguments.</param>
        private void IncomeTimer_Tick(object sender, EventArgs e)
        {
            UpdatePassiveIncome();
            score += passiveIncome * 0.01f;

            UpdateScoreText();
            UpdateButtonStatus();

            foreach (var quest in quests)
            {
                if (!quest.IsCompleted() || quest.IsCompletionMessageShown) continue;

                AddQuestToPopup(quest.Name, quest.CompletionMessage);

                MessageBox.Show(quest.CompletionMessage, "Quest Completed", MessageBoxButton.OK, MessageBoxImage.Information);
                quest.IsCompletionMessageShown = true;
            }
        }

        /// <summary>
        /// <para>
        /// Updates the passive income based on the counts of different game elements
        /// </para>
        /// </summary>
        private void UpdatePassiveIncome()
        {
            passiveIncome = cursorCount * cursorSpeed +
                                  grandmaCount * grandmaSpeed +
                                  farmCount * farmSpeed +
                                  mineCount * mineSpeed +
                                  factoryCount * factorySpeed +
                                  bankCount * bankSpeed +
                                  templeCount * templeSpeed;

            //passiveIncome = (float)Math.Round(passiveIncome, 2);
            lblCookiesPerSec.Content = passiveIncome.ToString();
        }

        /// <summary>
        /// Formats a large number by converting it into a string with the corresponding term (million, billion, etc.).
        /// </summary>
        /// <param name="number">The number to be formatted.</param>
        /// <returns>The formatted string representing the large number.</returns>
        private string NameLargeNumber(double number)
        {
            string[] terms = { "", "", "Miljoen", "Miljard", "Biljoen", "Biljard", "Triljoen" };

            int termsIndex = 0;
            while (number >= 1000 && termsIndex < terms.Length - 1)
            {
                number /= 1000;
                termsIndex++;
            }

            string formattedNumber = $"{number:N3}";

            // Extract the first three digits of the remainder
            int remainder = (int)(number % 1000);
            string remainderString = remainder.ToString().PadLeft(3, '0');

            // Concatenate the formatted number with the remainder and the corresponding term
            string largeNumber = $"{formattedNumber} {terms[termsIndex]}";
            return largeNumber;
        }

        /// <summary>
        /// Formats a number by inserting spaces for better readability.
        /// </summary>
        /// <param name="number">The number to be formatted.</param>
        /// <returns>The formatted string with spaces for better readability.</returns>
        private string NumberWithSpace(double number)
        {
            string spaceNumber = $"{number:N0}".Replace(".", " ");
            return spaceNumber;
        }

        /// <summary>
        /// Represents an input dialog window with a text box, OK, and Cancel buttons.
        /// </summary>
        public class InputDialog : Window
        {
            private TextBox txtInput;
            private Button btnOK;
            private Button btnCancel;

            /// <summary>
            /// Gets the input value provided by the user.
            /// </summary>
            public string InputValue { get; private set; }

            /// <summary>
            /// Initializes a new instance of the InputDialog class.
            /// </summary>
            /// <param name="title">The title of the input dialog window.</param>
            /// <param name="prompt">The prompt displayed to the user.</param>
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

        /// <summary>
        /// Event handler for changing the bakery name on mouse click.
        /// </summary>
        /// <param name="sender">The object that triggered the event.</param>
        /// <param name="e">Mouse button event arguments.</param>
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

        /// <summary>
        /// Buys an item in the game and adds its image to the appropriate WrapPanel.
        /// </summary>
        /// <param name="imagePath">The image path of the item.</param>
        /// <param name="category">The category of the item.</param>
        private void BuyItem(string imagePath, string category)
        {
            Image newItemImage = new Image
            {
                Source = new BitmapImage(new Uri(imagePath, UriKind.Relative)),
                Width = 30,
                Height = 30
            };

            // Determine the appropriate WrapPanel based on the category
            WrapPanel targetWrapPanel = GetWrapPanelByCategory(category);

            // Add the new item image to the determined WrapPanel
            targetWrapPanel.Children.Add(newItemImage);
        }

        /// <summary>
        /// Gets the corresponding WrapPanel based on the item category.
        /// </summary>
        /// <param name="category">The category of the item.</param>
        /// <returns>The WrapPanel associated with the specified category.</returns>
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

        /// <summary>
        /// <para>
        /// Event handler for the Golden Cookie timer tick event.
        /// </para>
        /// <para>
        /// Spawns a Golden Cookie with a 30% chance if one is not already active
        /// </para>
        /// </summary>
        /// <param name="sender">The object that triggered the event.</param>
        /// <param name="e">Event arguments.</param>
        private void GoldenCookieTimer_Tick(object sender, EventArgs e)
        {
            if (random.NextDouble() < 0.3 && !isGoldenCookieActive)
            {
                SpawnGoldenCookie();
            }
        }

        /// <summary>
        /// Spawns a Golden Cookie on the canvas with a timer for automatic disappearance.
        /// </summary>
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

        /// <summary>
        /// <para>
        /// Event handler for the Golden Cookie image mouse-up event
        /// </para>
        /// <para>
        /// Removes the Golden Cookie, grants bonus score, and updates UI elements
        /// </para>
        /// </summary>
        /// <param name="sender">The object that triggered the event.</param>
        /// <param name="e">Mouse button event arguments.</param>
        private void GoldenCookieImage_MouseUp(object sender, MouseButtonEventArgs e)
        {
            mainCanvas.Children.Remove((UIElement)sender);
            isGoldenCookieActive = false;

            score += passiveIncome * 15 * 60;

            UpdateScoreText();
            UpdateButtonStatus();
        }

        /// <summary>
        /// <para>
        /// Event handler for the "Quests" button click event.
        /// </para>
        /// <para>
        /// Toggles the visibility of the quest popup.
        /// </para>
        /// </summary>
        /// <param name="sender">The object that triggered the event.</param>
        /// <param name="e">Event arguments.</param>
        private void btnQuests_Click(object sender, RoutedEventArgs e)
        {
            questPopup.IsOpen = !questPopup.IsOpen;
        }

        /// <summary>
        /// Adds a completed quest to the quest history popup.
        /// </summary>
        /// <param name="questName">The name of the completed quest.</param>
        /// <param name="notification">The notification message for the completed quest.</param>
        private void AddQuestToPopup(string questName, string notification)
        {
            Quest completedQuest = new Quest
            {
                QuestName = questName,
                Notification = notification
            };

            lstQuestHistoryPopup.Items.Add(completedQuest);

        }

        /// <summary>
        /// Updates the visibility and enablement of bonus buttons based on the total cookies collected.
        /// </summary>
        private void UpdateBonusButtonStatus()
        {
           double totalCookiesCollected = score + (cursorCount * cursorCost) +
                                  (grandmaCount * grandmaCost) + (farmCount * farmCost) +
                                  (mineCount * mineCost) + (factoryCount * factoryCost) +
                                  (bankCount * bankCost) + (templeCount * templeCost);


            // Enable and visible bonus buttons based on score
          
            if (score >= cursorBonusCost && cursorCount >= 1)
            {
                btnCursorBonus.IsEnabled = true;
            }
            if (score >= grandmaBonusCost)
            {
                btnGrandmaBonus.IsEnabled = true;
            }
            if (score >= farmBonusCost)
            {
                btnFarmBonus.IsEnabled = true;
            }
            if (score >= mineBonusCost)
            {
                btnMineBonus.IsEnabled = true;
            }
            if (score >= factoryBonusCost)
            {
                btnFactoryBonus.IsEnabled = true;
            }
            if (score >= bankBonusCost)
            {
                btnBankBonus.IsEnabled = true;
            }
            if (score >= templeBonusCost)
            {
                btnTempleBonus.IsEnabled = true;
            }

            btnCursorBonus.Visibility = Visibility.Hidden;
            btnGrandmaBonus.Visibility = Visibility.Hidden;
            btnFarmBonus.Visibility = Visibility.Hidden;
            btnMineBonus.Visibility = Visibility.Hidden;
            btnFactoryBonus.Visibility = Visibility.Hidden;
            btnBankBonus.Visibility = Visibility.Hidden;
            btnTempleBonus.Visibility = Visibility.Hidden;

            if (totalCookiesCollected >= cursorCost)
            {
                btnCursorBonus.Visibility = Visibility.Visible;
            }
            if (totalCookiesCollected >= grandmaCost)
            {
                btnGrandmaBonus.Visibility = Visibility.Visible;
            }
            if (totalCookiesCollected >= farmCost)
            {
                btnFarmBonus.Visibility = Visibility.Visible;
            }
            if (totalCookiesCollected >= mineCost)
            {
                btnMineBonus.Visibility = Visibility.Visible;
            }
            if (totalCookiesCollected >= factoryCost)
            {
                btnFactoryBonus.Visibility = Visibility.Visible;
            }
            if (totalCookiesCollected >= bankCost)
            {
                btnBankBonus.Visibility = Visibility.Visible;
            }
            if (totalCookiesCollected >= templeCost)
            {
                btnTempleBonus.Visibility = Visibility.Visible;
            }
        }

        /// <summary>
        /// Updates the bonus multiplier, speed, and the UI label for the bonus.
        /// </summary>
        /// <param name="multiplierBonus">The bonus multiplier to be updated.</param>
        /// <param name="lblBonusMultiplier">The label displaying the bonus multiplier.</param>
        /// <param name="speed">The speed value to be updated.</param>
        /// <returns>Returns void.</returns>
        private void BonusMultiplierUpdate(ref double multiplierBonus, Label lblBonusMultiplier, ref double speed)
        {
            speed *= 2;

            multiplierBonus *= 2;
            lblBonusMultiplier.Content = "x" + multiplierBonus;
        }

        /// <summary>
        /// Calculates the cost of a bonus based on the base cost and bonus multiplier.
        /// </summary>
        /// <param name="baseCost">The base cost of the item.</param>
        /// <param name="multiplierBonus">The bonus multiplier affecting the cost.</param>
        /// <returns>The calculated bonus cost.</returns>
        private double CalculateBonusCost(int baseCost, double bonusCost, double multiplierBonus)
        {
            if (multiplierBonus == 1)
            {
                bonusCost = baseCost * 100;
            }
            else if (multiplierBonus == 2)
            {
                bonusCost = baseCost * 500;
            }
            else
            {
                bonusCost *= 10;
            }

            return bonusCost;
        }

        // <summary>
        /// Handles the click event of the bonus buttons, applies bonus multipliers, deducts the cost, and updates the UI.
        /// </summary>
        /// <param name="sender">The object that triggered the event.</param>
        /// <param name="e">Routed event arguments.</param>
        /// <returns>Returns void.</returns>
        private void btnBonus_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = (Button)sender;


            switch (clickedButton.Name)
            {
                case "btnCursorBonus":
                    cursorBonusCost = CalculateBonusCost(cursorCost, cursorBonusCost, cursorMultiplier);

                    if (score > cursorBonusCost)
                    {
                        BonusMultiplierUpdate(ref cursorMultiplier, lblCursorMultiplier, ref cursorSpeed);
                        score -= cursorBonusCost;
                        lblCursorBonusCost.Content = cursorBonusCost.ToString();
                    }
                    break;

                case "btnGrandmaBonus":
                    grandmaBonusCost = CalculateBonusCost(grandmaCost, grandmaBonusCost, grandmaMultiplier);

                    if (score > grandmaBonusCost)
                    {
                        BonusMultiplierUpdate(ref grandmaMultiplier, lblGrandmaMultiplier, ref grandmaSpeed);
                        score -= grandmaBonusCost;
                        lblGrandmaBonusCost.Content = grandmaBonusCost.ToString();
                    }
                    break;

                case "btnFarmBonus":
                    farmBonusCost = CalculateBonusCost(farmCost, farmBonusCost, farmMultiplier);

                    if (score > farmBonusCost)
                    {
                        BonusMultiplierUpdate(ref farmMultiplier, lblFarmMultiplier, ref farmSpeed);
                        score -= farmBonusCost;
                        lblFarmBonusCost.Content = farmBonusCost.ToString();
                    }
                    break;

                case "btnMineBonus":
                    mineBonusCost = CalculateBonusCost(mineCost, mineBonusCost, mineMultiplier);

                    if (score > mineBonusCost)
                    {
                        BonusMultiplierUpdate(ref mineMultiplier, lblMineMultiplier, ref mineSpeed);
                        score -= mineBonusCost;
                        lblMineBonusCost.Content = mineBonusCost.ToString();
                    }
                    break;

                case "btnFactoryBonus":
                    factoryBonusCost = CalculateBonusCost(factoryCost, farmBonusCost, factoryMultiplier);

                    if (score > factoryBonusCost)
                    {
                        BonusMultiplierUpdate(ref factoryMultiplier, lblFactoryMultiplier, ref factorySpeed);
                        score -= factoryBonusCost;
                        lblFactoryBonusCost.Content = factoryBonusCost.ToString();
                    }
                    break;

                case "btnBankBonus":
                    bankBonusCost = CalculateBonusCost(bankCost, bankBonusCost, bankMultiplier);

                    if (score > bankBonusCost)
                    {
                        BonusMultiplierUpdate(ref bankMultiplier, lblBankMultiplier, ref bankSpeed);
                        score -= bankBonusCost;
                        lblBankBonusCost.Content = bankBonusCost.ToString();
                    }
                    break;

                case "btnTempleBonus":
                    templeBonusCost = CalculateBonusCost(templeCost, templeBonusCost, templeMultiplier);

                    if (score > templeBonusCost)
                    {
                        BonusMultiplierUpdate(ref templeMultiplier, lblTempleMultiplier, ref templeSpeed);
                        score -= templeBonusCost;
                        lblTempleBonusCost.Content = templeBonusCost.ToString();
                    }
                    break;

                default:
                    break;
            }

            UpdateBonusButtonStatus();
            UpdateScoreText();
        }

    }
}           
