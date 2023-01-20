namespace Sicv1.Domain.Entities
{
    public class Parameter : BaseClass
    {
     
        public string NAME { get; set; }
        public string DESCRIPTION { get; set; }

        public int ESTADO { get; set; }
        public int SAVE_OR_UPDATE { get; set; }

        public int COMPANY_ID { get; set; }
        public string VALUE { get; set; }

        public int ID_PADRE { get; set; }



    }
}