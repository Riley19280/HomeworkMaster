using HomeworkMaster.Parsers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HomeworkMaster
{
	public class downloadMgr
	{
		public async Task<definitionWordInfo> getWordDefinition(string word)
		{
			definitionWordInfo WI = new definitionWordInfo();

			string httpText = "";

			bool retried = false;

			switch (MANAGER.SearchType)
			{
				case MANAGER.SEARCHTYPE.GOOGLE:
					httpText = await new HttpClient().GetStringAsync(Constants.GoogleGetWord + word);
					googleParse(httpText, word, WI);
					//checking that we got results
					if (string.IsNullOrWhiteSpace(WI.definition) && !retried)
					{
						try
						{
							httpText = await new HttpClient().GetStringAsync(Constants.DictionaryGetWord + word);
						}
						catch (Exception)
						{

						}
						dictionaryParse(httpText, word, WI);
						retried = true;
					}
					break;
				case MANAGER.SEARCHTYPE.DICTIONARY:
					try
					{
						httpText = await new HttpClient().GetStringAsync(Constants.DictionaryGetWord + word);
					}
					catch (Exception) { }
					dictionaryParse(httpText, word, WI);
					//checking that we got results
					if (string.IsNullOrWhiteSpace(WI.definition) && !retried)
					{
						httpText = await new HttpClient().GetStringAsync(Constants.GoogleGetWord + word);
						googleParse(httpText, word, WI);
						retried = true;
					}
					break;
				case MANAGER.SEARCHTYPE.WIKIPEDIA:
					httpText = await new HttpClient().GetStringAsync(Constants.WikipediaGetword + word);
					wikipediaParse(httpText, word, WI);
					break;
			}

			if (retried && string.IsNullOrWhiteSpace(WI.definition))
				WI.definition = "Word not found!";

			if (MANAGER.GetSynonyms)
			{
				string t = "";
				try
				{
					t = await new HttpClient().GetStringAsync(Constants.ThesarusGetSynAnt + word);
				}
				catch (Exception) { }
				ThesaurusParser tp = new ThesaurusParser(t);
				if (tp.syns.Count > 0)
				{ WI.synonyms = tp.syns; }
				else
				{ WI.synonyms.Add("No synonyms found"); };

				if (tp.ants.Count > 0)
				{ WI.antonyms = tp.ants; }
				else
				{ WI.antonyms.Add("No antonyms found"); };

			}

			return WI;
		}

		public async Task<apushTermInfo> getAPUSHDefinition(string term)
		{
			apushTermInfo AT = new apushTermInfo();

			string httpText = "";

			switch (MANAGER.Mode)
			{
				case MANAGER.MODE.APUSH:
					httpText = await new HttpClient().GetStringAsync(Constants.GoogleSearch + term.Replace(" ", "+")+"+APUSH");
					SearchParser_Quizlet qp = new SearchParser_Quizlet(httpText, term);
					AT.term = WebUtility.HtmlDecode(qp.OUTPUT.title);
					AT.definition = WebUtility.HtmlDecode(qp.OUTPUT.text);
					break;
				default:
					break;
			}

			return AT;
		}

		public async Task<DateInfo> getDate(string term) {
			string httpText = await new HttpClient().GetStringAsync(Constants.GoogleSearch + term.Replace(" ", "+") + "+date");
			dateParser dp = new dateParser(httpText,term);
			if(string.IsNullOrWhiteSpace(dp.OUTPUT.date)){ dp.OUTPUT.date = "Not found!"; } 
			return dp.OUTPUT;
		}

		#region definition parsers
		void googleParse(string httpText, string word, definitionWordInfo WI)
		{
			GoogleParser gp = new GoogleParser(httpText, word);
			WI.word = word;
			WI.definition = WebUtility.HtmlDecode(gp.OUTPUT.definition);
			WI.partOfSpeech = WebUtility.HtmlDecode(gp.OUTPUT.partOfSpeech);
			WI.pronunciation = WebUtility.HtmlDecode(gp.OUTPUT.pronunciation);
		}

		void dictionaryParse(string httpText, string word, definitionWordInfo WI)
		{
			DictionaryParser dp = new DictionaryParser(httpText);
			WI.word = word;
			WI.definition = WebUtility.HtmlDecode(dp.OUTPUT.definition);
			WI.partOfSpeech = WebUtility.HtmlDecode(dp.OUTPUT.partOfSpeech);
		}

		void wikipediaParse(string httpText, string word, definitionWordInfo WI)
		{
			WikipediaParser wp = new WikipediaParser(httpText);
			WI.word = word;
			WI.definition = WebUtility.HtmlDecode(wp.OUTPUT.definition);
		}

		#endregion
	}
}
