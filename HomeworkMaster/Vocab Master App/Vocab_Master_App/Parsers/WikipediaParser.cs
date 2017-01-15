using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using HomeworkMaster;

namespace HomeworkMaster.Parsers
{
	class WikipediaParser
	{
		public definitionWordInfo OUTPUT = new definitionWordInfo();

		public WikipediaParser(string HTML)
		{
			OUTPUT.definition = RipTags(Regex.Match(HTML, @"<P>((.|\n)*?)(<\/P>)").ToString());
		}

		private string RipTags(string s)
		{
			s = Regex.Replace(s, "<(.|\n)*?>", "");
			s = Regex.Replace(s, "\n*\n", "\n");
			return s;
		}
	}
}
