using DddInPractice.Data.Common;
using DddInPractice.Data.Management;

namespace DddInPractice.Data;
public static class Initializer
{
    public static void Initialize(string connectionString)
    {
        if (string.IsNullOrWhiteSpace(connectionString))
            throw new ArgumentException("Connection string cannot be null or empty", nameof(connectionString));

        DataContextFactory.Init(connectionString);
        HeadOfficeInstance.Init();
        DomainEventDispatcher.Init();
    }
}
