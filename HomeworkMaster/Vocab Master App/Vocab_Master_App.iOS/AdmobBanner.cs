using Xamarin.Forms;
using Xamarin.Admob.iOS;
using Xamarin.Forms.Platform.iOS;
using GoogleAdMobAds;
using MonoTouch.UIKit;
using Xamarin.Admob.Abstractions;

[assembly: ExportRenderer(typeof(AdmobBannerView), typeof(AdmobBanner))]

namespace Xamarin.Admob.iOS
{
    public class AdmobBanner : ViewRenderer
    {
        GADBannerView adView;
        bool viewOnScreen;

        protected override void OnElementChanged(ElementChangedEventArgs<View> e)
        {
            base.OnElementChanged(e);

            var adMobElement = Element as AdmobBannerView;

            if (null != adMobElement)
            {
                adView = new GADBannerView(GADAdSizeCons.Banner)
                {
                    AdUnitID = adMobElement.AdUnitID,
                    RootViewController = UIApplication.SharedApplication.Windows[0].RootViewController
                };

                adView.AdReceived += (sender, args) =>
                {
                    if (!viewOnScreen)
                        AddSubview(adView);
                    viewOnScreen = true;
                };

                adView.LoadRequest(GADRequest.Request);
                SetNativeControl(adView);
            }                
        }
    }
	/*    public class CustomAdViewRenderer : ViewRenderer
    {
        const string AdmobID = "My-Unit-ID";

        GADBannerView adView;
        bool viewOnScreen = false;

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.View> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement == null)
                return;

            if (e.OldElement == null)
            {
                adView = new GADBannerView(size: GADAdSizeCons.SmartBannerPortrait)//, origin: new PointF(0, 0))
                    {
                        AdUnitID = AdmobID,
                        RootViewController = UIApplication.SharedApplication.Windows[0].RootViewController
                    };

                adView.DidReceiveAd += (sender, args) =>
                    {
                        if (!viewOnScreen) this.AddSubview(adView);
                        viewOnScreen = true;
                    };

                GADRequest request = GADRequest.Request;

                #if DEBUG
                request.TestDevices = new string [] { GADRequest.GAD_SIMULATOR_ID };
                #endif

                adView.LoadRequest(request);
                base.SetNativeControl(adView);
            }
        }
    }
*/
}
