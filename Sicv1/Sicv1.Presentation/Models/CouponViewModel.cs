using System;

namespace Sicv1.Presentation.Models
{
    public class CouponViewModel
    {
        public string imageBase64 { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Conditions { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Percentage { get; set; }
        public int hdCategoryId { get; set; }
        public string hdFileName { get; set; } 
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public int CreatedUser { get; set; }
        public int CompanyId { get; set; }
        public int IdParent { get; set; }
        public int StatusOriginalImage { get; set; }
        public string Type { get; set; }
        public string URL_LINK { get; set; }
        
        public int LIFEMILES_PARTICIPATES_CAMPAIGN { get; set; }
        public int NUMBER_MILES { get; set; }

        public string BARCODE { get; set; }
        public string BARCODE_FORMAT{ get; set; }
        public string TYPE_CODE { get; set; }
        public string SEGMENT { get; set; }
        public int SHOW_IMAGE { get; set; }
        public string CAT_URL { get; set; }
    }
}