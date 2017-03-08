using HomeworkMaster.Views.Ads;
using HomeworkMaster.Views.APUSH;
using HomeworkMaster.Views.Date;
using HomeworkMaster.Views.Definitions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xamarin.Forms;

namespace HomeworkMaster.Views
{
	public class wordListView : basePage
	{
		StackLayout BASESTACK;
		ListView listView;
		ActivityIndicator ai = new ActivityIndicator() {Color = Constants.palette.secondary_text };
		public wordListView()
		{
			Title = "My Terms";

			#region List View
			listView = new ListView
			{
				// Source of data items.


				RowHeight = 65,
				// Define template for displaying each item.
				// (Argument of DataTemplate constructor is called for 
				//      each item; it must return a Cell derivative.)
				ItemTemplate = new DataTemplate(() =>
				{
					// Create views with bindings for displaying each property.
					Label wordLabel = new Label
					{
						FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
					}, defLabel = new Label
					{
						FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
					};
					if (MANAGER.Mode == MANAGER.MODE.DICTIONARY)
					{
						wordLabel.SetBinding(Label.TextProperty, "word");
						defLabel.SetBinding(Label.TextProperty, "definition");
					}
					else if (MANAGER.Mode == MANAGER.MODE.APUSH || MANAGER.Mode == MANAGER.MODE.APPSYCH)
					{
						wordLabel.SetBinding(Label.TextProperty, "term");
						defLabel.SetBinding(Label.TextProperty, "definition");
					}
					else if (MANAGER.Mode == MANAGER.MODE.DATE)
					{
						wordLabel.SetBinding(Label.TextProperty, "term");
						defLabel.SetBinding(Label.TextProperty, "date");
					}

					// Return an assembled ViewCell.
					return new ViewCell
					{

						View = new StackLayout
						{

							Padding = new Thickness(5, 5, 5, 0),
							Children =
								{
									new StackLayout
									{
										VerticalOptions = LayoutOptions.Center,
										Spacing = 0,
										Children =
										{
											wordLabel,
											defLabel,

										}
									}
								}
						}
					};
				})
			};
			#endregion

			listView.ItemSelected += OnItemSelected;

			BASESTACK = new StackLayout
			{
				Children = {
					listView,
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

			Content = BASESTACK;
		}

		async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
		{
			if (listView.SelectedItem != null)
			{
				switch (MANAGER.Mode)
				{
					case MANAGER.MODE.DICTIONARY:
						var wordView = new defWordView(listView.SelectedItem as definitionWordInfo);
						listView.SelectedItem = null;
						await Navigation.PushAsync(wordView);

						break;
					case MANAGER.MODE.APUSH:
						var termView = new APUSHTermView(listView.SelectedItem as genericTermInfo);
						listView.SelectedItem = null;
						await Navigation.PushAsync(termView);
						break;
					case MANAGER.MODE.APPSYCH:
						var iView = new APUSHTermView(listView.SelectedItem as genericTermInfo);
						listView.SelectedItem = null;
						await Navigation.PushAsync(iView);
						break;
					case MANAGER.MODE.DATE:
						var dateView = new dateView(listView.SelectedItem as DateInfo);
						listView.SelectedItem = null;
						await Navigation.PushAsync(dateView);
						break;
				}

			}
		}


		protected async override void OnAppearing()
		{
			base.OnAppearing();
			{
				if (MANAGER.wordListNeedsRefresh)
				{


					BASESTACK.Children.Insert(0, ai);
					ai.IsRunning = true;
					ai.IsEnabled = true;

					try
					{
						switch (MANAGER.Mode)
						{
							case MANAGER.MODE.DICTIONARY:
								List<definitionWordInfo> wordsInfo = new List<definitionWordInfo>();
								for (int i = 0; i < MANAGER.words.Count; i++)
								{
									downloadMgr downloadMgr = new downloadMgr();

									definitionWordInfo w = await downloadMgr.getWordDefinition(MANAGER.words[i]);
									wordsInfo.Add(w);

								}
								listView.ItemsSource = wordsInfo;
								break;
							case MANAGER.MODE.APUSH:
								List<genericTermInfo> termInfo = new List<genericTermInfo>();
								for (int i = 0; i < MANAGER.words.Count; i++)
								{
									downloadMgr downloadMgr = new downloadMgr();

									genericTermInfo w = await downloadMgr.getAPUSHDefinition(MANAGER.words[i]);
									termInfo.Add(w);

								}
								listView.ItemsSource = termInfo;
								break;
							case MANAGER.MODE.APPSYCH:
								List<genericTermInfo> ti = new List<genericTermInfo>();
								for (int i = 0; i < MANAGER.words.Count; i++)
								{
									downloadMgr downloadMgr = new downloadMgr();

									genericTermInfo w = await downloadMgr.getPsychDefinition(MANAGER.words[i]);
									ti.Add(w);

								}
								listView.ItemsSource = ti;
								break;
							case MANAGER.MODE.DATE:
								List<DateInfo> dateInfo = new List<DateInfo>();
								for (int i = 0; i < MANAGER.words.Count; i++)
								{
									downloadMgr downloadMgr = new downloadMgr();

									DateInfo w = await downloadMgr.getDate(MANAGER.words[i]);
									dateInfo.Add(w);
								}
								listView.ItemsSource = dateInfo;
								break;
						}						

					}
					catch (Exception e)
					{
						Debug.WriteLine(e.Message);
						Debug.WriteLine(e.StackTrace);
					}

					BASESTACK.Children.Remove(ai);
					ai.IsEnabled = false;
					ai.IsRunning = false;
					MANAGER.wordListNeedsRefresh = false;

				}
			}
		}
	}

}
