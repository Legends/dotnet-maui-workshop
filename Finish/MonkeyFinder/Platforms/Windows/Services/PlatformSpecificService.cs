using MonkeyFinder.Interfaces;
using MonkeyFinder.Platforms.Windows.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly: Microsoft.Maui.Controls.Dependency(typeof(PlatformSpecificService))]
namespace MonkeyFinder.Platforms.Windows.Services
{
	public partial class PlatformSpecificService : IPlatformSpecificService
	{
		public void DoSomething()
		{
			Debug.WriteLine("#############   Windows SERVICE called   ###############");
		}
	}
}
