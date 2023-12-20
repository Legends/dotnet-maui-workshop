namespace MonkeyFinder.View;

public partial class MainPage : ContentPage
{
	MonkeysViewModel vm;
	public MainPage(MonkeysViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
		vm = viewModel;
		//this.Appearing
	}

	protected override void OnAppearing()
	{
		base.OnAppearing();

		if (DeviceInfo.Current.Platform == DevicePlatform.WinUI) // Windows only
		{
			Dispatcher.Dispatch(() =>
			{
				ToolbarItems.Add(new ToolbarItem("REFRESH", null, () =>
				{
					// Handle Windows toolbar item click
					vm.GetMonkeysCommand.Execute(null);
				}));
			});
		}
	}

}

