namespace Sicv1.Presentation.Models
{
    public class CompanyViewModel
    {
        public int CompanyId { get; set; }
        public string Nombre { get; set; }
        public string NAME { get; set; }
        public string Logo { get; set; }
        public string Ruc { get; set; }
        //public string RUC { get; set; }
        public string Url { get; set; }
        public string Telefono { get; set; }
        public int CreatedUser { get; set; }
        public int StatusOriginalImage { get; set; }
        public string ImageBase64 { get; set; }
        public string FileName { get; set; }
        public bool Status { get; set; }
        public int LIFEMILES_PARTICIPATES_CAMPAIGN { get; set; }
        public int NUMBER_MILES { get; set; }
        ///******************************************************************//
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Direction { get; set; }

        public string Country { get; set; }
    }
}