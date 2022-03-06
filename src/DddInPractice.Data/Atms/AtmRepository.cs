using DddInPractice.Data.Common;
using DddInPractice.Domain.BoundedContext.Atms.AtmAggregate;
using DddInPractice.Domain.BoundedContext.Atms.Dto;
using Microsoft.EntityFrameworkCore;

namespace DddInPractice.Data.Atms;
public class AtmRepository : RepositoryBase<Atm>
{
    public IReadOnlyList<AtmDto> GetAtmList()
    {
        using var dbContext = DataContextFactory.CreateDataContext();
        return dbContext.Atm.AsNoTracking()
            .Select(x => new AtmDto(x.Id, x.MoneyInside.Amount))
            .ToList();
    }
}
