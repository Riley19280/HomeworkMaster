using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace HomeworkMaster.Views
{
	public class help : basePage
	{
		public help()
		{
			Title = "Help";

			Content = new StackLayout
			{
				Padding = new Thickness(25),
				Spacing = 10,
				Children = {
					new Label {
							Text = "Pressing the icon in the top left will open the menu. Select which mode you would like to use and continue from there.",
						FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label))
					},
					new Label {
							Text = "If you would like to request a subject addition you can do that",
						FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label))
					},
					new Button {
						Text = "here",
						HorizontalOptions = LayoutOptions.Center,
						Command = new Command (()=> {
							if (Device.OS != TargetPlatform.WinPhone){
							Device.OpenUri(new Uri(Constants.SurveyLink));
							} else {
							DisplayAlert("To Do","Not implemented yet","OK");
							};
						})

						}
					}
			};
		}
	}
}

