using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using HomeworkMaster;

namespace HomeworkMaster.Parsers
{
	class DictionaryParser
	{
		public definitionWordInfo OUTPUT = new definitionWordInfo();

		public DictionaryParser(string HTML)
		{
			try
			{

				#region definition
				string sourceBox1 = Regex.Match(HTML, @"<div class=""(source-box)( oneClick-area)?"">((.|\n)*?)<div class=""tail-wrapper"">").ToString();

				string definition = "";
				foreach (var item in Regex.Matches(sourceBox1, @"<section class=.def-pbk ce-spot((.|\n)*?)<\/section>"))
				{
					definition += RipTags(Regex.Replace(item.ToString(), @"<span class=""dbox-example"">((\n|.)*?)<\/span>", ""));
				}
				Debug.WriteLine(HTML);
				definition = definition.Remove(0, definition.IndexOf('1'));
				definition = Regex.Replace(definition, "\n", "");
				definition = Regex.Replace(definition, "[ ]{2,}", " ");

				MatchCollection dm = Regex.Matches(definition, @"[0-9]\.");

				List<string> dm2 = new List<string>();
				foreach (var item in dm)
				{
					if (!string.IsNullOrWhiteSpace(item.ToString()))
					{
						dm2.Add(item.ToString());
					}
				}

				for (int i = 0; i < dm2.Count; i++)
					definition = definition.Replace((i + 1).ToString(), "\n" + (i + 1).ToString());

				#endregion

				#region type
				string type = "";
				foreach (var item in Regex.Matches(sourceBox1, @"<header class=""luna-data-header"">((.|\n)*?)<\/span>"))
				{
					type += RipTags(item.ToString());
				}
				type = Regex.Replace(type, "[ ]{2,}", " ");
				#endregion

				OUTPUT.definition = definition;
				OUTPUT.partOfSpeech = type;
			}
			catch (Exception)
			{


			}

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
