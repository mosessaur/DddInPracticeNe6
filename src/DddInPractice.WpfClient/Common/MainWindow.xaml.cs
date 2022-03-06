using System.Windows;

namespace DddInPractice.WpfClient.Common;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        DataContext = new MainViewModel();
    }
}
