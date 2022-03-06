using DddInPractice.Domain.BoundedContext.Management.HeadOfficeAggregate;

namespace DddInPractice.Data.Management;
public static class HeadOfficeInstance
{
    private const long HeadOfficeId = 1L;

    public static HeadOffice? Instance { get; private set; }

    public static void Init()
    {
        var headOfficeRepository = new HeadOfficeRepository();
        Instance = headOfficeRepository.GetById(HeadOfficeId);
    }
}
