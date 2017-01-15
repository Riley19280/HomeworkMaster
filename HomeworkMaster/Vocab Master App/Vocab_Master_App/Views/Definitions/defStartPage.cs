using HomeworkMaster.Views.Ads;
using HomeworkMaster.Views.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace HomeworkMaster.Views
{
	public class defStartPage : basePage
	{

		public defStartPage()
		{
			Title = "DEFINITIONS";

			MANAGER.Mode = MANAGER.MODE.DICTIONARY;

			string editorText = "Enter Words, one per line";
			Editor editor = new Editor
			{
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.FillAndExpand,
				Text = editorText,
				Style = (Style)Application.Current.Resources["editorStyle"]

			};
			editor.Focused += (sender, eventArgs) =>
			{
				editor.Text = editor.Text.Replace(editorText, string.Empty);
			};


			Button btnNext = new Button
			{
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				Text = "NEXT >"
			};


			btnNext.Clicked += (sender, eventArgs) =>
			{
				if (DependencyService.Get<INetworkAvailable>().HasNetworkAccess())
				{
					if (!editor.Text.Contains(editorText))
					{
						if (!string.IsNullOrWhiteSpace(editor.Text))
						{
							DependencyService.Get<IAdmobInterstitial>().Request(Constants.InterstitialID);
							MANAGER.wordListNeedsRefresh = true;
							List<string> words = new List<string>();
							words = editor.Text.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries).ToList();
							for (int i = 0; i < words.Count; i++)
							{
								if (string.IsNullOrWhiteSpace(words[i]))
								{
									words.RemoveAt(i);
								}
							}
							MANAGER.words = words;
							Navigation.PushAsync(new defOptions());
						}
						else
							DisplayAlert("Enter Words", "Enter some words before continuing", "OK");

					}
					else
						DisplayAlert("Enter Words", "Enter some words before continuing", "OK");
				}
				else
					DisplayAlert("No internet access", "No internet access detected. Please connect to the internet before continuing", "OK");
			};


			Content = new StackLayout
			{

				Children = {
					new StackLayout {
						Padding = new Thickness(25),
						VerticalOptions = LayoutOptions.FillAndExpand,
						Children = {
							editor,
							btnNext,

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
