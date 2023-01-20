using System;

namespace Sicv1.Domain.Entities
{
    public class BranchOffices : BaseClass
    {
        public int ID_COMPANY { get; set; }
        public string NAME { get; set; }
        public string DESCRIPTION { get; set; }
        public string PHONE { get; set; }
        public string LATITUDE { get; set; }
        public string LONGITUDE { get; set; }
        public string DIRECTION { get; set; }
        public string COUNTRY { get; set; }
    }
}