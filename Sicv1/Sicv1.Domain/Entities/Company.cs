namespace Sicv1.Domain.Entities
{
    public class Company : BaseClass
    {
        public string NAME { get; set; }
        public string ALIAS { get; set; }
        public string DESCRIPTION { get; set; }
        public string LOGO { get; set; }
        public string RUC { get; set; }
        public string URL { get; set; }
        public string PHONE { get; set; }
        ///******************************************************************//
        /// campo(s) para el store procedure: 'SP_COMPANIES_LIST_BY_IDUSER'  //
        public int ID_USER { get; set; }                                     //
        public int FK_ROLE { get; set; }                                     //
        ///******************************************************************//
        public int LIFEMILES_PARTICIPATES_CAMPAIGN { get; set; }
        public int NUMBER_MILES { get; set; }
        public int CompanyHaveLifeMiles { get; set; }
        ///******************************************************************//
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Direction { get; set; }

        public string Country { get; set; }
        public int ESTADO { get; set; }
    }

}