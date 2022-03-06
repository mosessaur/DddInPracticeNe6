using DddInPractice.Domain.BoundedContext.Atms.AtmAggregate;
using FluentAssertions;
using static DddInPractice.Domain.SharedKernel.Money;
using Xunit;

namespace DddInPractice.Domain.Tests.BoundedContext.Atms.AtmAggregate;
public class AtmSpecs
{
    private const int DecimalPrecision = 2;
    [Fact]
    public void TakeMoney_exchanges_money_with_commission()
    {
        var atm = new Atm();
        atm.LoadMoney(Dollar);

        atm.TakeMoney(1m);

        atm.MoneyInside.Amount.Should().Be(0m);
        atm.MoneyCharged.Should().Be(1.01m);
    }

    [Fact]
    public void TakeMoney_apply_at_least_one_cent_commission()
    {
        var atm = new Atm();
        atm.LoadMoney(Cent);

        atm.TakeMoney(0.01m);

        atm.MoneyCharged.Should().BeApproximately(0.02m, DecimalPrecision);
    }

    [Fact]
    public void TakeMoney_rounds_commission_up_to_the_next_cent()
    {
        var atm = new Atm();
        atm.LoadMoney(Dollar + TenCent);

        atm.TakeMoney(1.1m);

        atm.MoneyCharged.Should().BeApproximately(1.12m, DecimalPrecision);
    }
}
