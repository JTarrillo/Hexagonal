using System;

namespace Sicv1.Presentation.Models
{
    public class NewsnessViewModel
    {
		public int ID { get; set; }
		public string NAME_COUNTRY { get; set; }
		public int ID_CATEGORY { get; set; }
		public int ID_TYPE_NEWNESS { get; set; }
		public string URL { get; set; }
		public bool ISVIDEO { get; set; }
		public string TITLE { get; set; }
		public string DESCRIPTION { get; set; }
		public string IMAGE_FIRST { get; set; }
		public bool EXPIRE { get; set; }
		public string DATE_EXPIRATION { get; set; }
		public string DATE_PUBLICATION { get; set; }
		public string IMAGEBASE64 { get; set; }
		public string HDFILENAME { get; set; }


	}
}