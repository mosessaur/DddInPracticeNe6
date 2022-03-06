using DddInPractice.Data.Atms;
using DddInPractice.Data.SnackMachines;
using DddInPractice.Domain.BoundedContext.Atms.AtmAggregate;
using DddInPractice.WpfClient.Atms;
using DddInPractice.WpfClient.SnackMachines;

namespace DddInPractice.WpfClient.Common;

public class MainViewModel : ViewModelBase
{
    public MainViewModel()
    {
        //ShowSnackMachine();
        ShowAtm();
    }

    private void ShowSnackMachine()
    {
        var snackMachineRepository = new SnackMachineRepository();
        var snackMachine = snackMachineRepository.GetById(1L);

        if (snackMachine is null)
            return;

        var viewModel = new SnackMachineViewModel(snackMachine);
        _dialogService.ShowDialog(viewModel);
    }

    private void ShowAtm()
    {
        var atmRepository = new AtmRepository();
        Atm? atm = atmRepository.GetById(1);
        
        if (atm is null)
            return;
        var viewModel = new AtmViewModel(atm);
        _dialogService.ShowDialog(viewModel);
    }
}
