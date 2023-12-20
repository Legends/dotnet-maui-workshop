using Microsoft.Extensions.Logging;

namespace MonkeyFinder.ViewModel;

[QueryProperty(nameof(Monkey), "Monkey")]
public partial class MonkeyDetailsViewModel : BaseViewModel
{
	IMap map;
	ILogger<MonkeyDetailsViewModel> logger;
	public MonkeyDetailsViewModel(IMap map, ILogger<MonkeyDetailsViewModel> loggerInst)
	{
		logger = loggerInst;
		this.map = map;
	}

	[ObservableProperty]
	Monkey monkey;

	[RelayCommand]
	async Task OpenMap()
	{
		try
		{
			await map.OpenAsync(Monkey.Latitude, Monkey.Longitude, new MapLaunchOptions
			{
				Name = Monkey.Name,
				NavigationMode = NavigationMode.None
			});
		}
		catch (Exception ex)
		{
			logger.LogError($"Unable to launch maps: {ex.Message}");
			await Shell.Current.DisplayAlert("Error, no Maps app!", ex.Message, "OK");
		}
	}
}
