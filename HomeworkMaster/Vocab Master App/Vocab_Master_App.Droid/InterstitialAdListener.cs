using Android.Gms.Ads;
using HomeworkMaster.Views.Ads;
using Xamarin.Forms;

namespace Xamarin.Admob.Android
{
	public class InterstitialAdListener : AdListener
    {
        readonly InterstitialAd _ad;

        public InterstitialAdListener(InterstitialAd ad)
        {
            _ad = ad;
        }

		public override void OnAdLoaded()
        {
            base.OnAdLoaded();

        }

		public override void OnAdOpened()
		{
			base.OnAdOpened();

		}

		public override void OnAdClosed()
		{
			base.OnAdClosed();
			DependencyService.Get<IAdmobInterstitial>().Request(HomeworkMaster.Constants.InterstitialID);
		}
		
		public override void OnAdLeftApplication()
		{
			base.OnAdLeftApplication();

		}
	}
}

