using Android.Gms.Ads;
using Xamarin.Admob.Android;
using Android.App;
using HomeworkMaster.Droid.Views.Ads;
using HomeworkMaster.Views.Ads;

[assembly: Xamarin.Forms.Dependency(typeof(AdmobInterstitial))]

namespace HomeworkMaster.Droid.Views.Ads
{
	public class AdmobInterstitial : IAdmobInterstitial
	{
		InterstitialAd _ad;
		bool reqAd = true;

		public void Request(string adUnit)
		{
			if (false)
				if (reqAd == true && MANAGER.ENABLEADS)
				{
					_ad = new InterstitialAd(Application.Context);
					_ad.AdUnitId = adUnit;

					var intlistener = new InterstitialAdListener(_ad);
					intlistener.OnAdLoaded();
					intlistener.OnAdOpened();
					intlistener.OnAdClosed();
					intlistener.OnAdLeftApplication();


					_ad.AdListener = intlistener;

					var requestbuilder = new AdRequest.Builder().AddTestDevice("CD0D9F143549110573451D43B4DA9859");
					requestbuilder.AddTestDevice("77B08A4FA88832D62AF6BBDB31038A05");
					_ad.LoadAd(requestbuilder.Build());
					reqAd = false;
				}
		}

		public void Show()
		{
			if (_ad != null)
				if (_ad.IsLoaded && MANAGER.ENABLEADS)
				{
					_ad.Show();
					reqAd = true;
				}
		}
	}
}

