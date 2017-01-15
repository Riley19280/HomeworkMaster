using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using HomeworkMaster.Droid;
using Android.Net;

[assembly: Xamarin.Forms.Dependency(typeof(NetworkAvailable))]
namespace HomeworkMaster.Droid
{

	public class NetworkAvailable : INetworkAvailable
	{
		public bool HasNetworkAccess()
		{
			Context c = Application.Context;
			ConnectivityManager cm = (ConnectivityManager)c.GetSystemService(Context.ConnectivityService);
			NetworkInfo netInfo = cm.ActiveNetworkInfo;
			return netInfo != null && netInfo.IsConnectedOrConnecting;
		}
	}
}
