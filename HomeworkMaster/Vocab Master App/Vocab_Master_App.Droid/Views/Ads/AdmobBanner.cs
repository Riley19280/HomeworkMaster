using Xamarin.Forms;
using Xamarin.Admob.Android;
using Xamarin.Forms.Platform.Android;
using Android.Gms.Ads;
using HomeworkMaster.Droid.Views.Ads;
using HomeworkMaster.Views.Ads;

[assembly: ExportRenderer(typeof(AdmobBannerView), typeof(AdmobBanner))]

namespace HomeworkMaster.Droid.Views.Ads
{
    public class AdmobBanner : ViewRenderer
    {
        /// <summary>
        /// Used for registration with dependency service
        /// </summary>
        public static void Init()
        {
        }

        /// <summary>
        /// reload the view and hit up google admob 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnElementChanged(ElementChangedEventArgs<View> e)
        {
            base.OnElementChanged(e);

            //convert the element to the control we want
            var adMobElement = Element as AdmobBannerView;

            if ((adMobElement != null) && (e.OldElement == null))
            {
                var ad = new AdView(Context);
                ad.AdSize = AdSize.Banner;
                ad.AdUnitId = adMobElement.AdUnitID;
                var requestbuilder = new AdRequest.Builder().AddTestDevice("CD0D9F143549110573451D43B4DA9859");
				requestbuilder.AddTestDevice("77B08A4FA88832D62AF6BBDB31038A05");
				ad.LoadAd(requestbuilder.Build());
                SetNativeControl(ad);
            }
        }
    }
}


