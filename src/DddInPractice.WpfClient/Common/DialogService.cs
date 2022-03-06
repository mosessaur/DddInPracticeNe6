namespace DddInPractice.WpfClient.Common;

public class DialogService
{
    public bool? ShowDialog(ViewModel viewModel)
    {
        CustomWindow window = new CustomWindow(viewModel);
        return window.ShowDialog();
    }
}
