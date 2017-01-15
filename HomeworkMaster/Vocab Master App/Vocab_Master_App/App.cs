using HomeworkMaster.Views.Menu;

using Xamarin.Forms;

namespace HomeworkMaster
{
	public class App : Application
	{
		public static Page mainPage;

		public App()
		{
			#region Style
			var contentPageStyle = new Style(typeof(ContentPage))
			{
				Setters = {
				new Setter { Property = ContentPage.BackgroundColorProperty, Value = Constants.palette.primary },
				}
			};
			var labelStyle = new Style(typeof(Label))
			{
				Setters = {
				new Setter { Property = Label.TextColorProperty, Value = Constants.palette.primary_text },
				}
			};
			var editorStyle = new Style(typeof(Editor))
			{
				Setters = {
				new Setter { Property = Editor.TextColorProperty, Value = Constants.palette.primary_text },
				new Setter { Property = Editor.BackgroundColorProperty, Value = Constants.palette.primary_light },
				}
			};
			var buttonStyle = new Style(typeof(Button))
			{
				Setters = {
				new Setter { Property = Button.TextColorProperty, Value = Constants.palette.primary_text },
				new Setter { Property = Button.BackgroundColorProperty, Value = Constants.palette.primary_light },
				}
			};
			var switchStyle = new Style(typeof(Switch))
			{
				Setters = {
				new Setter { Property = Switch.BackgroundColorProperty, Value = Constants.palette.primary_light },
				}
			};

			Resources = new ResourceDictionary();
			Resources.Add("contentPageStyle", contentPageStyle);
			Resources.Add("labelStyle", labelStyle);
			Resources.Add("editorStyle", editorStyle);




			#endregion

			// The root page of your application
			mainPage =new RootPage(); /*new NavigationPage(new defStartPage())
			{
				BarBackgroundColor = Constants.palette.primary_dark,
				BarTextColor = Constants.palette.icons,
				Title = "QUICK DEFINITIONS",

			};*/
			MainPage = mainPage;

		}

		protected override void OnStart()
		{
			// Handle when your app starts
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}
	}
}
