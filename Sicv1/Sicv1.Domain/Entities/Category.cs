using System;

namespace Sicv1.Domain.Entities
{
    public class Category : BaseClass
    {
        public int POSITION { get; set; }
        public string BARCODE{ get; set; }
        public string BARCODE_FORMAT{ get; set; }
        public string TYPE_CODE { get; set; }
        public bool ISCOUPON { get; set; }
        public int ID_COMPANY { get; set; }
        public int SHOW_IMAGE { get; set; }
        public string CAT_URL { get; set; }
        ///***********************************************************************//
        /// campo(s) para el store procedure: 'SP_CATEGORIES_LIST_BY_IDUSER'      //
        public string NAME_COMPANY { get; set; } = string.Empty;                  //
        public string LOGO { get; set; } = string.Empty;                          //
        public decimal SCORE { get; set; }                                        //
        public int TOTAL_RATING { get; set; }                                     //
        ///***********************************************************************//
        /// campo(s) para el store procedure: 'SP_CATEGORIES_CODE_QR_LIST'        //
        public string CODE_QR { get; set; } = string.Empty;                       //
        public string PHONE { get; set; } = string.Empty;                         //
        public string NAME { get; set; } = string.Empty;                          //
        public string LASTNAME { get; set; } = string.Empty;                      //
        public DateTime DATE_USE { get; set; }                                    //
        public string PERCENTAGE { get; set; } = string.Empty;                    //
        public string FULL_NAME { get; set; } = string.Empty;                     //
        ///***********************************************************************//
        /// campo(s) para el store procedure: 'SP_CATEGORIES_LIST_BY_IDCOMPANY'***//
        public int ID_PARENT2 { get; set; }                                       //
        public int ID_PARENT3 { get; set; }                                       //
                                                                                  
        public bool HAVE_ACCESS_SI { get; set; }
        public bool HAVE_ACCESS_NO { get; set; }
        public int ID_HAVE_ACCESS_SI { get; set; }
        public int ID_HAVE_ACCESS_NO { get; set; }
        ///***********************************************************************//
        public int ID_PARENT { get; set; }
        public string TITLE { get; set; } = string.Empty;
        public string SUBTITLE { get; set; } = string.Empty;
        public string DESCRIPTION { get; set; } = string.Empty;
        public string CONDITIONS { get; set; } = string.Empty;
        public string TO { get; set; } = string.Empty;
        public string COLOR { get; set; } = string.Empty;
        public string ICON { get; set; } = string.Empty;
        public string ALIAS { get; set; } = string.Empty;
        public decimal PRICE { get; set; }
        public string IMAGE_FIRST { get; set; } = string.Empty;
        public string IMAGE_SECOND { get; set; } = string.Empty;
        public DateTime START_DATE { get; set; } = Convert.ToDateTime("1901-01-01");
        public DateTime END_DATE { get; set; } = Convert.ToDateTime("1901-01-01");
        public string TYPE { get; set; } = string.Empty;
        public string DOCUMENT { get; set; }
        public int FK_ROLE { get; set; }

        public string ALLIANCE { get; set; } = string.Empty;
        public bool USED { get; set; }
        public int APPROVED_ID_USER { get; set; }

        public int LIFEMILES_PARTICIPATES_CAMPAIGN { get; set; }
        public int NUMBER_MILES { get; set; }
        public int COMPANY_HAVE_LIFEMILES { get; set; }


        public string CODE_LIFEMILES { get; set; }
        public int QUANTITY { get; set; }
        public string SEGMENT{ get; set; }

        public string URL_LINK { get; set; }

    }
}
