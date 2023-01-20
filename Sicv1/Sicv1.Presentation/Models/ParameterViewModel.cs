namespace Sicv1.Presentation.Models
{
    public class ParameterViewModel
    {

        public int ID { get; set; }
        public string NAME { get; set; }
        public string DESCRIPTION { get; set; }

        public int ESTADO { get; set; }
        public int UPDATED_USER { get; set; }

        public int COMPANY_ID { get; set; }
        public string VALUE { get; set; }

        public bool STATUS { get; set; }

        public int SAVE_OR_UPDATE { get; set; }
        public int ID_PADRE { get; set; }

    }
}