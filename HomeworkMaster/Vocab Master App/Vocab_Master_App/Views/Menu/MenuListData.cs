using System;
using Xamarin.Forms;
using System.Collections.Generic;
using HomeworkMaster.Views;
using HomeworkMaster.Views.APUSH;
using HomeworkMaster.Views.APPsych;

namespace HomeworkMaster.Views.Menu
{

	public class MenuListData : List<MenuItem>
	{
		public MenuListData ()
		{
			this.Add (new MenuItem () { 
				Title = "Definitions", 
				//IconSource = "contacts.png", 
				TargetType = typeof(defStartPage)
			});

			this.Add (new MenuItem () { 
				Title = "APUSH", 
				//IconSource = "leads.png", 
				TargetType = typeof(APUSHStartPage)
			});

			this.Add(new MenuItem()
			{
				Title = "Psychology",
				//IconSource = "leads.png", 
				TargetType = typeof(APPsychStartPage)
			});

			this.Add(new MenuItem()
			{
				Title = "Dates",
				//IconSource = "leads.png", 
				TargetType = typeof(dateStartpage)
			});

			this.Add(new MenuItem()
			{
				Title = "Help",
				//IconSource = "leads.png", 
				TargetType = typeof(help)
			});


		}
	}
}