using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using HomeworkMaster;

namespace HomeworkMaster.Parsers
{
	public class GoogleParser
	{
		public string html;

		public definitionWordInfo OUTPUT = new definitionWordInfo();

		string word;

		public GoogleParser(string HTML, string word)
		{
			try
			{

				this.word = word;

				string box = Regex.Match(HTML, @"<div class=""g"">((.|\n)*?)<div class=""g"">").ToString();

				string speech = Regex.Match(box, @">\/(.*?)\/<").ToString().Remove(0, 2);
				speech = speech.Replace("/<", "");

				MatchCollection types = Regex.Matches(box, @"color:#666;padding:5px 0(.*?)<");
				string type = "";

				List<string> typeList = new List<string>();
				List<string> distTypeList = new List<string>();
				for (int i = 0; i < types.Count; i++)
				{
					string t = types[i].ToString().Replace("<", "").Remove(0, 26);
					typeList.Add(t);
				}
				//removing duplicates
				distTypeList = typeList.Distinct().ToList();

				for (int i = 0; i < distTypeList.Count; i++)
				{
					type += "• " + distTypeList[i] + "\n";
				}


				MatchCollection defs = Regex.Matches(box, @"<li(.*?)>(.*?)<");
				string definitions = "";

				for (int i = 0; i < defs.Count; i++)
				{
					string d = RipTags(defs[i].ToString());
					d = d.Replace("<", "");

					definitions += (i + 1).ToString() + ". " + d + "\n";
				}

				OUTPUT = new definitionWordInfo(word, definitions, type, speech);

				html += "<u><b>" + word + ": </b></u>" + "(" + type.Replace("\n", "/") + ")" + "<br>" + definitions.Replace("\n", "<br>") + "<br>";

			}
			catch
			{

			}
		}

		private string RipTags(string s)
		{
			s = Regex.Replace(s, "<(.|\n)*?>", "");
			s = Regex.Replace(s, "\n*\n", "\n");
			return s;
		}
	}
}
