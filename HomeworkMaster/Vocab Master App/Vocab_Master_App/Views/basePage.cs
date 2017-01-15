using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace HomeworkMaster.Views
{
	public class basePage : ContentPage
	{
		public basePage()
		{
			Style = (Style)Application.Current.Resources["contentPageStyle"];
			Resources = new ResourceDictionary();
		}
	}
}
