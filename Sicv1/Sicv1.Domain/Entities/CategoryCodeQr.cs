using System;

namespace Sicv1.Domain.Entities
{
    public class CategoryCodeQr : BaseClass
    {
        public int ID_CATEGORY { get; set; }
        public int ID_USER { get; set; }
        public string CODE_QR { get; set; }
        public string VIA { get; set; }
        public string TYPE { get; set; }
        public new int CREATED_USER { get; set; }

        /// <summary>
        /// use SP_CATEGORIES_HISTORICAL_DETAIL
        /// </summary>
        public int COUPON_ID { get; set; }
        public DateTime REDEMPTION_DATE { get; set; }
        public string TITLE { get; set; } = string.Empty;
        public string DESCRIPTION { get; set; } = string.Empty;
        public string ALLIANCE_NAME { get; set; } = string.Empty;
        public string USERNAME { get; set; } = string.Empty;
        public string LEVEL_3 { get; set; } = string.Empty;
        public string LEVEL_2 { get; set; } = string.Empty;
        public string LEVEL_1 { get; set; } = string.Empty;
        public int NUMBER_OF_TIMES_REDEEMED { get; set; }
        public string EXCHANGE_USER { get; set; } = string.Empty;
        public string ALLIANCE_USER { get; set; } = string.Empty;

        /// <summary>
        /// use SP_CATEGORIES_CODE_QR_CHART_COUNT_COUPONS_BY_DATE
        /// </summary>
        public int num_cupones { get; set; }
        public string fecha { get; set; } = string.Empty;

        /// <summary>
        /// use SP_CATEGORIES_CODE_QR_UPDATE
        /// </summary>
        public int APPROVED_ID_USER { get; set; }
    }
}