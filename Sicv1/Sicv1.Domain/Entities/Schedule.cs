namespace Sicv1.Domain.Entities
{
    public class Schedule : BaseClass
    {
        public int ID_USER { get; set; }
        public int ID_CATEGORY { get; set; }
        public string PHONE { get; set; }
        public string CONTACT_NAME { get; set; }
        public string CALL_HOUR { get; set; }
        ///******************************************************************//
        /// campo(s) para el store procedure: 'SP_SCHEDULE_CALL_LIST'        //
        public int ID_CUPON { get; set; }                                    //
        public string CUPON { get; set; }                                    //
        public int ID_COMPANY { get; set; }                                  //
        public string COMPANY { get; set; }                                  //
        public string USERNAME { get; set; }                                 //
        public string DOCUMENT { get; set; }                                 //
        public string NAME { get; set; }                                     //
        public string LASTNAME { get; set; }                                 //
        ///******************************************************************//
    }
}