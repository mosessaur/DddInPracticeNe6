using DddInPractice.WpfClient.Management;

namespace DddInPractice.WpfClient.Common;

public class MainViewModel : ViewModelBase
{
    public DashboardViewModel Dashboard { get; }
    public MainViewModel()
    {
        Dashboard = new DashboardViewModel();
    }
}
