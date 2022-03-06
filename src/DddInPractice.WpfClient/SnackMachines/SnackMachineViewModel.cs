using System;
using System.Collections.Generic;
using System.Linq;
using DddInPractice.Data.SnackMachines;
using DddInPractice.Domain.BoundedContext.SnackMachines.SnackMachineAggregate;
using DddInPractice.Domain.SharedKernel;
using DddInPractice.WpfClient.Common;

namespace DddInPractice.WpfClient.SnackMachines;

public class SnackMachineViewModel : ViewModelBase
{
    private readonly SnackMachine _snackMachine;
    private readonly SnackMachineRepository _snackMachineRepository;

    public override string Caption => "Snack Machine";
    // ReSharper disable once SpecifyACultureInStringConversionExplicitly
    public string MoneyInTransaction => _snackMachine.MoneyInTransaction.ToString();
    public Money MoneyInside => _snackMachine.MoneyInside;

    public IReadOnlyList<SnackPileViewModel> Piles
    {
        get
        {
            return _snackMachine.GetAllSnackPiles()
                .Select(x => new SnackPileViewModel(x))
                .ToList();
        }
    }

    private string _message = "";
    public string Message
    {
        get { return _message; }
        private set
        {
            _message = value;
            Notify();
        }
    }

    public Command InsertCentCommand { get; }
    public Command InsertTenCentCommand { get; }
    public Command InsertQuarterCommand { get; }
    public Command InsertDollarCommand { get; }
    public Command InsertFiveDollarCommand { get; }
    public Command InsertTwentyDollarCommand { get; }
    public Command ReturnMoneyCommand { get; }
    public Command<string> BuySnackCommand { get; }

    public SnackMachineViewModel(SnackMachine snackMachine)
    {
        _snackMachine = snackMachine;
        _snackMachineRepository = new SnackMachineRepository();
        InsertCentCommand = new Command(() => InsertMoney(Money.Cent));
        InsertTenCentCommand = new Command(() => InsertMoney(Money.TenCent));
        InsertQuarterCommand = new Command(() => InsertMoney(Money.Quarter));
        InsertDollarCommand = new Command(() => InsertMoney(Money.Dollar));
        InsertFiveDollarCommand = new Command(() => InsertMoney(Money.FiveDollar));
        InsertTwentyDollarCommand = new Command(() => InsertMoney(Money.TwentyDollar));
        ReturnMoneyCommand = new Command(ReturnMoney);
        BuySnackCommand = new Command<string>(BuySnack);
    }

    private void BuySnack(string? positionString)
    {
        if (string.IsNullOrWhiteSpace(positionString))
            throw new ArgumentException($"{positionString} cannot be null or empty", nameof(positionString));

        int position = int.Parse(positionString);

        string error = _snackMachine.CanBuySnack(position);
        if (error != string.Empty)
        {
            NotifyClient(error);
            return;
        }

        _snackMachine.BuySnack(position);
        _snackMachineRepository.Save(_snackMachine);
        NotifyClient("You have bought a snack");
    }

    private void ReturnMoney()
    {
        _snackMachine.ReturnMoney();
        NotifyClient("Money was returned");
    }

    private void InsertMoney(Money coinOrNote)
    {
        _snackMachine.InsertMoney(coinOrNote);
        NotifyClient("You have inserted: " + coinOrNote);
    }

    private void NotifyClient(string message)
    {
        Message = message;
        Notify(nameof(MoneyInTransaction));
        Notify(nameof(MoneyInside));
    }
}
