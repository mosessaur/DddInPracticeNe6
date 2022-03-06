namespace DddInPractice.WpfClient.Common;

public class DialogService
{
    public bool? ShowDialog(ViewModelBase viewModel)
    {
        CustomWindow window = new CustomWindow(viewModel);
        return window.ShowDialog();
    }
}
