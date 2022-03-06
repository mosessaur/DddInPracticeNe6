using DddInPractice.Data.Common;
using DddInPractice.Domain.BoundedContext.SnackMachines.Dto;
using DddInPractice.Domain.BoundedContext.SnackMachines.SnackMachineAggregate;
using Microsoft.EntityFrameworkCore;

namespace DddInPractice.Data.SnackMachines;

public class SnackMachineRepository : RepositoryBase<SnackMachine>
{
    public IReadOnlyList<SnackMachineDto> GetSnackMachineList()
    {
        using var dbContext = DataContextFactory.CreateDataContext();
        return dbContext.SnackMachines.AsNoTracking()
            .Select(x => new SnackMachineDto(x.Id, x.MoneyInside.Amount))
            .ToList();
    }
}
