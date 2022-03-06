using System.Windows;

namespace DddInPractice.WpfClient.Common;

public partial class CustomWindow : Window
{
    public CustomWindow(ViewModel viewModel)
    {
        InitializeComponent();

        //Owner = Application.Current.MainWindow;
        DataContext = viewModel;
    }
}
