using HomeworkMaster;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Vocab_Master_App.iOS
{
	class NetworkAvailable : INetworkAvailable
	{
		public bool HasNetworkAccess()
		{
			if (Reachability.Reachability.IsHostReachable("http://google.com"))
				return true;
			else
				return false;
		}
	}
}