using HomeworkMaster.Views.Ads;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace HomeworkMaster.Views.Date
{
	public class dateView : basePage
	{
		public dateView(DateInfo dateInfo)
		{
			Content = new StackLayout
			{
				Children = {
					new StackLayout
						{
							Padding = new Thickness(25),
							Spacing = 10,
							Children = {
								new Label {
									Text = dateInfo.term,
									FontSize = Device.GetNamedSize(NamedSize.Large,typeof(Label))
								},
								new Label {
									Text = dateInfo.date,
									FontSize = Device.GetNamedSize(NamedSize.Medium,typeof(Label))
								},
							}

					},
					MANAGER.ENABLEADS ? new StackLayout
					{
						VerticalOptions = LayoutOptions.End,
						Children = {
							new AdmobBannerView(Constants.BannerID) {
								VerticalOptions = LayoutOptions.End
							}
						}
					} : new StackLayout { }
				}

			};
		}
	}
}
	

