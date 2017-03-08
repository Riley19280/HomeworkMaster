using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HomeworkMaster.Parsers
{
	class googleQuickBoxParser
	{
		public genericTermInfo OUTPUT = new genericTermInfo();
		string word;
		public googleQuickBoxParser(string HTML, string word) {

			this.word = word;

			string box = Regex.Match(HTML, @"<div class=""g"">((.|\n)*?)<div class=""g"">").ToString();

			string text = Regex.Match(box, @"<div class=._sPg.>(.*?)<\/div>").ToString();

			OUTPUT.term = word;
			OUTPUT.definition = RipTags(text);
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
	}
}
