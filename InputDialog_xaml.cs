using System.Windows;

public partial class InputDialog : Window
{
    public string InputValue { get; set; }

    public InputDialog(string title, string prompt)
    {
        InitializeComponent();
        Title = title;
        lblPrompt.Content = prompt;
    }

    private void btnOK_Click(object sender, RoutedEventArgs e)
    {
        InputValue = txtInput.Text;
        DialogResult = true;
    }

    private void btnCancel_Click(object sender, RoutedEventArgs e)
    {
        DialogResult = false;
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
