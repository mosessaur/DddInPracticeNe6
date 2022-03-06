using System.Windows;

namespace DddInPractice.WpfClient.Common;

public partial class CustomWindow : Window
{
    public CustomWindow(ViewModelBase viewModel)
    {
        InitializeComponent();

        //Owner = Application.Current.MainWindow;
        DataContext = viewModel;
    }
}
