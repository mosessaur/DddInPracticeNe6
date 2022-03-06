using DddInPractice.Data.Atms;
using DddInPractice.Domain.BoundedContext.Atms;
using DddInPractice.Domain.BoundedContext.Atms.AtmAggregate;
using DddInPractice.Domain.SharedKernel;
using DddInPractice.WpfClient.Common;

namespace DddInPractice.WpfClient.Atms;
public class AtmViewModel : ViewModelBase
{
    private readonly Atm _atm;
    private readonly AtmRepository _atmRepository;
    private readonly PaymentGateway _paymentGateway;
    private string? _message;

    public override string Caption => "ATM";
    public Money MoneyInside => _atm.MoneyInside;
    public string MoneyCharged => _atm.MoneyCharged.ToString("C2");
    public string? Message
    {
        get { return _message; }
        private set
        {
            _message = value;
            Notify();
        }
    }
    public Command<decimal> TakeMoneyCommand { get; private set; }

    public AtmViewModel(Atm atm)
    {
        _atm = atm;
        _atmRepository = new AtmRepository();
        _paymentGateway = new PaymentGateway();
        TakeMoneyCommand = new Command<decimal>(amount => amount > 0, TakeMoney);
    }

    private void TakeMoney(decimal amount)
    {
        string error = _atm.CanTakeMoney(amount);
        if (error != string.Empty)
        {
            NotifyClient(error);
            return;
        }

        decimal amountWithCommission = Atm.CalculateAmountWithCommission(amount);
        _paymentGateway.ChargePayment(amountWithCommission);
        _atm.TakeMoney(amount);
        _atmRepository.Save(_atm);

        NotifyClient("You have taken " + amount.ToString("C2"));
    }

    private void NotifyClient(string message)
    {
        Message = message;
        Notify(nameof(MoneyInside));
        Notify(nameof(MoneyCharged));
    }
}
