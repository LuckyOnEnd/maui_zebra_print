using MauiApp3.Platforms.Android;
using MauiApp3.Services.Android;

namespace maui_zebra_print;

public partial class AppShell : Shell
{
	public AppShell()
	{
        DependencyService.Register<IConnectionManager, ConnectionManager>();

        InitializeComponent();
	}
}
