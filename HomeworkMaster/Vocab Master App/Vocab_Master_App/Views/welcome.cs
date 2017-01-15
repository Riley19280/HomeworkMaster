using HomeworkMaster.Views.Ads;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace HomeworkMaster.Views
{
	public class welcome : basePage
	{
		public welcome()
		{
			Title = "Welcome";

			Content = new StackLayout
			{
				Padding = new Thickness(25),
				Spacing = 10,
				Children = {					
					new Label {
						Text = "Welcome!",
						FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label))
					},
					new Label {
						Text = "Pressing the icon in the top left will open the menu. Select which mode you would like to use and continue from there.",
						FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label))
					},
						MANAGER.ENABLEADS ? new StackLayout {
						VerticalOptions = LayoutOptions.End,
						Children = {
							new AdmobBannerView(Constants.BannerID) {
								VerticalOptions = LayoutOptions.End
							}
						}
					}:new StackLayout { }

				}
			};
		}
	}
}
