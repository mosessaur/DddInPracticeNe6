using DddInPractice.Data;
using DddInPractice.Domain;

namespace DddInPractice.WpfClient.Common;

public class MainViewModel : ViewModel
{
    public MainViewModel()
    {
        var snackMachineRepository = new SnackMachineRepository();

        var snackMachine = snackMachineRepository.GetById(1L);

        if (snackMachine is null)
            return;

        var viewModel = new SnackMachineViewModel(snackMachine);
        _dialogService.ShowDialog(viewModel);
    }
}
