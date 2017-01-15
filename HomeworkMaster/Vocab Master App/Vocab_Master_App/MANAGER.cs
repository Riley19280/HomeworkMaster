using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//DependencyService.Get<IAdMobInterstitial>().Show(AdUnitId);
namespace HomeworkMaster
{
	public static class MANAGER
	{
		public static bool wordListNeedsRefresh = false;
		
		public static bool ENABLEADS = true;

		public enum SEARCHTYPE
		{
			GOOGLE,DICTIONARY,WIKIPEDIA

		}

		public static SEARCHTYPE SearchType = SEARCHTYPE.GOOGLE;

		public enum MODE { DICTIONARY,APUSH,DATE }

		public static MODE Mode = MODE.DICTIONARY;

		public static bool GetSynonyms = false;

		public static List<string> words = new List<string>();

	}
}
