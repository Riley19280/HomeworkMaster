using HomeworkMaster.Views.Ads;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace HomeworkMaster.Views.APUSH
{
	public class APUSHTermView : basePage
	{
		public APUSHTermView(genericTermInfo info)
		{
			Title = info.term;

			Content = new StackLayout
			{
				Children = {
					new StackLayout {
						Padding = new Thickness(25),
						Spacing = 10,
						Children = {
							new Label {
								Text = info.term,
								FontSize = Device.GetNamedSize(NamedSize.Large,typeof(Label))
							},
							new ScrollView {
								VerticalOptions = LayoutOptions.FillAndExpand,
								HorizontalOptions = LayoutOptions.FillAndExpand,

								Content = new Label {
									Text = info.definition,
									FontSize = Device.GetNamedSize(NamedSize.Medium,typeof(Label))
								  },

								},
							new Button {
								Text = "More Info",
								HorizontalOptions = LayoutOptions.Center,
								Command = new Command(()=> {
									Device.OpenUri(new Uri( Constants.GoogleSearch + info.term.Replace(" ","+")));
								})
								
							}
							},

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
