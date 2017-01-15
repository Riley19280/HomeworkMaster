using HomeworkMaster.Views.Ads;

using Xamarin.Forms;

namespace HomeworkMaster.Views.Definitions
{
	public class defOptions : basePage
	{
		public defOptions()
		{
			Title = "OPTIONS";

			#region picker
			Picker picker = new Picker
			{
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				Title = "Select search type",
				
			};

			picker.Items.Add("Goolge");//index 0
			picker.Items.Add("Dictionary.com");//index 1
			//picker.Items.Add("Wikipedia.com");//index 2

			picker.SelectedIndex = 0;

			picker.SelectedIndexChanged += (sender, eventArgs) =>
			{
				MANAGER.wordListNeedsRefresh = true;

				switch (picker.SelectedIndex)
				{
					case 0:
						MANAGER.SearchType = MANAGER.SEARCHTYPE.GOOGLE;
						break;
					case 1:
						MANAGER.SearchType = MANAGER.SEARCHTYPE.DICTIONARY;
						break;
					case 2:
						MANAGER.SearchType = MANAGER.SEARCHTYPE.WIKIPEDIA;
						break;
				}
			};

			#endregion

			#region switch
			Switch sw = new Switch
			{
				HorizontalOptions = LayoutOptions.CenterAndExpand,

			};
			sw.Toggled += (sender, eventArgs) =>
			{
				MANAGER.wordListNeedsRefresh = true;

				if (eventArgs.Value)//is true
					MANAGER.GetSynonyms = true;
				else
					MANAGER.GetSynonyms = false;

			};

			#endregion

			Content = new StackLayout
			{				
				Children = {
					new StackLayout {
						Padding = new Thickness(25),
						VerticalOptions = LayoutOptions.FillAndExpand,
						Children = {
							new StackLayout {
								Orientation = StackOrientation.Horizontal,
								Children = {
									new Label {
										Text ="Search Type",
										FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label))
									},
									picker,
								}
							},
								new StackLayout {
									Orientation = StackOrientation.Horizontal,
									Children = {
										new Label {
											HorizontalOptions = LayoutOptions.StartAndExpand,
											Text = "Find synonyms and antynoms: ",
											FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label))
										},
										sw,
									}
								},

								new Button {
										HorizontalOptions = LayoutOptions.CenterAndExpand,
										VerticalOptions = LayoutOptions.EndAndExpand,
										Text = "NEXT >",
										Command = new Command(() => {

											DependencyService.Get<IAdmobInterstitial>().Show();
											Navigation.PushAsync(new wordListView());

										})
								},
						}
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
