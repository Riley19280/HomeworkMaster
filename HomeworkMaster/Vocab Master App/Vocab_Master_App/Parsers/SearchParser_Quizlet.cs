using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Net.Http;

namespace HomeworkMaster.Parsers
{
	class SearchParser_Quizlet
	{

		public class quizletEntry
		{

			public quizletEntry(string titl, string def, string html)
			{
				title = titl;
				text = def;
				this.html = html;
				value = 0;
			}
			public string html;
			public string title;
			public string text;
			public int value;
		}

		public quizletEntry OUTPUT;

		public string searchTerm = "";
		List<string> termList = new List<string>();

		string quizletHTML = "";

		public SearchParser_Quizlet(string HTML, string term)
		{

			searchTerm = term;

			MatchCollection m = Regex.Matches(HTML, @"url\?q=https?:\/\/quizlet.com\/(.*?)\/&");//the google search result class

			//TODO: check to make sure it is quizzlet
			string url = m[0].ToString().Remove(0,6);//the url in the search result

			url = url.Remove(url.Length - 1, 1);


			var task = Task.Run(async () => { quizletHTML = await new HttpClient().GetStringAsync(url); });
			task.Wait();

			

			MatchCollection quzentries = Regex.Matches(quizletHTML, @"<div class=.SetPage-term.(.*?)<\/div><\/div><\/div><\/div><\/div><\/div><\/div>");

			List<quizletEntry> entries = new List<quizletEntry>();

			foreach (Match s in quzentries)
			{
				entries.Add(new quizletEntry(RipTags(Regex.Match(s.ToString(), @"<div class=.SetPageTerm-wordText(.*?<\/div>)").ToString()), RipTags(Regex.Match(s.ToString(), @"<div class=.SetPageTerm-definitionText.>(.*?<\/div>)").ToString()), s.ToString()));
			}

			quizletEntry final = new quizletEntry("Not Found", "Not Found", "Not Found");//the final results

			foreach (string s in searchTerm.Split(' '))
				termList.Add(s);

			//assigning point values
			foreach (quizletEntry s in entries)
			{
				foreach (string t in termList)
				{
					if (s.title.ToLower().Contains(t.ToLower()))
						s.value += 2;
					if (s.text.ToLower().Contains(t.ToLower()))
						s.value += 1;
				}

				if (s.value > final.value)
					final = s;
			}

			OUTPUT = final;
			//OUTPUT = m[0].ToString();

		}

		private string RipTags(string s)
		{
			s = Regex.Replace(s, @"<style>(.|\n)*?<\/style>", string.Empty);
			s = Regex.Replace(s, @"<script(.|\n)*?>(.|\n)*?<\/script>", string.Empty);
			s = Regex.Replace(s, "\\n", "");
			s = Regex.Replace(s, "\\t", "");
			s = Regex.Replace(s, "<(.|\n)*?>", "");
			s = Regex.Replace(s, "\n*\n", "\n");
			return s;
		}

		private string ReplaceNewLine(string s)
		{
			s = Regex.Replace(s, @"\r\n?|\n", "");
			return s;
		}
	}
}
