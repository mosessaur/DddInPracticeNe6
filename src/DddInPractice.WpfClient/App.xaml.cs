using System.Windows;
using DddInPractice.Data;

namespace DddInPractice.WpfClient;

public partial class App : Application
{
    private const string ConnectionString = @"Data Source=.\SqlExpress;Initial Catalog=DddInPractice; Integrated Security=true;";
    public App()
    {
        Initializer.Initialize(ConnectionString);
    }
}
