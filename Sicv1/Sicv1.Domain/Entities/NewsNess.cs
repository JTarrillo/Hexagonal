using System;
namespace Sicv1.Domain.Entities
{
    public class NewsNess : BaseClass
    {
        public int ID_CATEGORY { get; set; }
        public string CATEGORY { get; set; }
        public int ID_TYPE_NEWNESS { get; set; }
        public string TYPE_NEWNESS { get; set; }
        public string URL { get; set; }
        public bool VIDEO { get; set; }
        public string TITLE { get; set; }
        public string DATE_PUBLISH { get; set; }
        public string DESCRIPTION { get; set; }
        public string IMAGE_FIRST { get; set; }
        public string DATE_EXPIRATION { get; set; }
        public string DATE_PUBLICATION { get; set; }
        public bool EXPIRE { get; set; }
        //public bool ISVIDEO { get; set; }

        
    }
}
