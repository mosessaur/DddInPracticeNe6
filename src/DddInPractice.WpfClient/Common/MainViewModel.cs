using DddInPractice.Domain;

namespace DddInPractice.WpfClient.Common;

public class MainViewModel : ViewModel
{
    public MainViewModel()
    {
        SnackMachine? snackMachine;
        using (var dbContext = DataContextFactory.CreateDataContext())
        {
            snackMachine = dbContext.SnackMachines.Find(1L);
        }
        if (snackMachine is null)
            return;

        var viewModel = new SnackMachineViewModel(snackMachine);
        _dialogService.ShowDialog(viewModel);
    }
}
