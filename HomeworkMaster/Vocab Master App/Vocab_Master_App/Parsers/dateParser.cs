using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HomeworkMaster.Parsers
{
	public class dateParser
	{
		public DateInfo OUTPUT;
		
		public dateParser(string html, string term)
		{
			
			string s = "";
			try
			{
				s = Regex.Match(html, @"<span class=""_m3b"">(.*?)<").ToString();
				s = RipTags(s);
				s = s.Remove(s.IndexOf("<"), 1);
			}
			catch (Exception e)
			{
				Debug.WriteLine(e.Message);
				Debug.WriteLine(e.StackTrace);
			}
			OUTPUT = new DateInfo(term, WebUtility.HtmlDecode(s));
		}


		private string RipTags(string s)
		{
			s = Regex.Replace(s, @"<style>(.|\n)*?<\/style>", string.Empty);
			s = Regex.Replace(s, @"<script(.|\n)*?>(.|\n)*?<\/script>", string.Empty);
			s = Regex.Replace(s, "<(.|\n)*?>", "");
			s = Regex.Replace(s, "\n*\n", "\n");
			return s;
		}
	}
}
