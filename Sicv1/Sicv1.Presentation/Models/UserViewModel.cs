namespace Sicv1.Presentation.Models
{
    public class UserViewModel
    {

        public int ID { get; set; }
        public string NAME { get; set; }
        public string LASTNAME_P { get; set; }
        public string LASTNAME_M { get; set; }

        public string USERNAME { get; set; }
        public string PASSWORD { get; set; } = string.Empty;
        public string TYPE_DOCUMENT { get; set; }
        public string DOCUMENT { get; set; }

        public string PHONE1 { get; set; }
        public string GENDER { get; set; }

        public string EMAIL { get; set; }
        public string ADDRESS { get; set; }
        public string UBIGEO { get; set; }
        public int FK_ROLE { get; set; }

        public string CARD { get; set; }
        public int ESTADO { get; set; }
        public int UPDATED_USER { get; set; }

        public int COMPANY_ID { get; set; }

        public string BIRTHDAY { get; set; }
        public bool STATUS { get; set; }
    }
}