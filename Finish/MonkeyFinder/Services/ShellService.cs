using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonkeyFinder.Services
{
	public interface IShellService
	{
		Task GoToAsync<TPage>(string propName, object obj, bool animate = true);
		Task ShowMessage(string titel, string msg, string ok, string cancel = null);
	}

	public class ShellService : IShellService
	{
		public ShellService() { }

		public async Task ShowMessage(string titel, string msg, string ok, string cancel = null)
		{

			if (!string.IsNullOrEmpty(ok) && !string.IsNullOrEmpty(cancel))
			{
				await Shell.Current.DisplayAlert(titel, msg, ok, cancel);
			}

			await Shell.Current.DisplayAlert(titel, msg, ok);
		}

		public async Task GoToAsync<TPage>(string propName, object obj, bool animate = true)
		{

			await Shell.Current.GoToAsync(typeof(TPage).Name, animate, new Dictionary<string, object>
		{
			{propName, obj }
		});
		}
	}


}
