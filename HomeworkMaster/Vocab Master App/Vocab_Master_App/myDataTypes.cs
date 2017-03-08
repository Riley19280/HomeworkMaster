using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeworkMaster
{

	public class definitionWordInfo
	{
		public definitionWordInfo(string word, string def, string POS, string pronounc) {
			this.word = word;
			this.definition = def;
			this.partOfSpeech = POS;
			this.pronunciation = pronounc;

		}
		public definitionWordInfo() { }

		public string word { get; set; }
		public string definition { get; set; }
		public string partOfSpeech { get; set; }
		public string pronunciation { get; set; }
		public List<string> synonyms { get; set; } = new List<string>();
		public List<string> antonyms { get; set; } = new List<string>();

	}

	public class genericTermInfo {

		public string term { get; set; }
		public string definition { get; set; }

	}

	public class DateInfo{

		public DateInfo(string term, string date)
		{
			this.term = term;
			this.date = date;
		}
		public DateInfo() { }

		public string term { get; set; }
		public string date { get; set; }
}
}
