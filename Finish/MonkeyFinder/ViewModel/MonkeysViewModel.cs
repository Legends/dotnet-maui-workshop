﻿using MonkeyFinder.Services;

namespace MonkeyFinder.ViewModel;

public partial class MonkeysViewModel : BaseViewModel
{
	public ObservableCollection<Monkey> Monkeys { get; } = new();
	MonkeyService monkeyService;
	IConnectivity connectivity;
	IGeolocation geolocation;
	IShellService shellService;

	public MonkeysViewModel(MonkeyService monkeyService,
							IConnectivity connectivity,
							IGeolocation geolocation,
							IShellService shellService)
	{
		Title = "Monkey Finder";
		this.monkeyService = monkeyService;
		this.connectivity = connectivity;
		this.geolocation = geolocation;
		this.shellService = shellService;
	}

	[RelayCommand]
	async Task GoToDetails(Monkey monkey)
	{
		if (monkey == null)
			return;

		await this.shellService.GoToAsync<DetailsPage>("Monkey", monkey);
	}

	[ObservableProperty]
	bool isRefreshing;

	/// <summary>
	/// GetMonkeysCommand
	/// </summary>
	/// <returns></returns>
	[RelayCommand]
	async Task GetMonkeysAsync()
	{
		if (IsBusy)
			return;

		try
		{
			if (connectivity.NetworkAccess != NetworkAccess.Internet)
			{
				await shellService.ShowMessage("No connectivity!",
					$"Please check internet and try again.", "OK");
				return;
			}

			IsBusy = true;
			var monkeys = await monkeyService.GetMonkeys();

			if (Monkeys.Count != 0)
				Monkeys.Clear();

			foreach (var monkey in monkeys)
				Monkeys.Add(monkey);

		}
		catch (Exception ex)
		{
			Debug.WriteLine($"Unable to get monkeys: {ex.Message}");
			await shellService.ShowMessage("Error!", ex.Message, "OK");
		}
		finally
		{
			IsBusy = false;
			IsRefreshing = false;
		}

	}

	[RelayCommand]
	async Task GetClosestMonkey()
	{
		if (IsBusy || Monkeys.Count == 0)
			return;

		try
		{
			// Get cached location, else get real location.
			var location = await geolocation.GetLastKnownLocationAsync();
			if (location == null)
			{
				location = await geolocation.GetLocationAsync(new GeolocationRequest
				{
					DesiredAccuracy = GeolocationAccuracy.Medium,
					Timeout = TimeSpan.FromSeconds(30)
				});
			}

			// Find closest monkey to us
			var first = Monkeys.OrderBy(m => location.CalculateDistance(
				new Location(m.Latitude, m.Longitude), DistanceUnits.Miles))
				.FirstOrDefault();

			await shellService.ShowMessage("", first.Name + " " + first.Location, "OK");

		}
		catch (Exception ex)
		{
			Debug.WriteLine($"Unable to query location: {ex.Message}");
			await shellService.ShowMessage("Error!", ex.Message, "OK");
		}
	}
}
