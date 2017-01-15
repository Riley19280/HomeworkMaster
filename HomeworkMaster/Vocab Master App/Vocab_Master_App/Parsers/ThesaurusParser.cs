using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace HomeworkMaster.Parsers
{
	public class ThesaurusParser
	{

		public List<string> syns { get; private set; } = new List<string>();
		public List<string> ants { get; private set; } = new List<string>();


		public ThesaurusParser(string HTML)
		{

			#region parse
			//relevancy-block((.|\n)*?)<\/ul>
			string regexSyn = @"relevancy-block((.|\n)*?)<\/ul>((.|\n)*?)<\/div>";
			string regexAnt = @"container-info antonyms((.|\n)*?)<\/ul>((.|\n)*?)<\/div>";


			Match msyn = Regex.Match(HTML, regexSyn);
			Match mant = Regex.Match(HTML, regexAnt);

			string regex2 = @"<span class=""text"">((.|\n)*?)<\/span>";

			foreach (Match m in Regex.Matches(msyn.ToString(), regex2))
			{
				Match ma = Regex.Match(m.ToString(), regex2);

				string h = ma.ToString().Remove(0, 19);
				h = h.Remove(h.IndexOf("<"));
				syns.Add(h);

			}

			foreach (Match m in Regex.Matches(mant.ToString(), regex2))
			{
				Match ma = Regex.Match(m.ToString(), regex2);

				string h = ma.ToString().Remove(0, 19);
				h = h.Remove(h.IndexOf("<"));
				ants.Add(h);

			}
			#endregion
		}
	}
}
