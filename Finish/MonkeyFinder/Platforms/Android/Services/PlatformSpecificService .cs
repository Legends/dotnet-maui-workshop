using MonkeyFinder.Interfaces;
using MonkeyFinder.Platforms.Android.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly: Microsoft.Maui.Controls.Dependency(typeof(PlatformSpecificService))]
namespace MonkeyFinder.Platforms.Android.Services
{
	public partial class PlatformSpecificService : IPlatformSpecificService
	{
		public void DoSomething()
		{
			// Platform-specific implementation for Android
			Debug.WriteLine("#############   ANDROID SERVICE called   ###############");
		}
	}
}
