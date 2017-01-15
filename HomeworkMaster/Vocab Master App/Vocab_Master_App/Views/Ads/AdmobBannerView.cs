using System;
using Xamarin.Forms;

namespace HomeworkMaster.Views.Ads
{
	public class AdmobBannerView : View
	{
		public string AdUnitID { get; set; }

		public AdmobBannerView(string adUnitID)
		{
			AdUnitID = adUnitID;
		}
	}
}

