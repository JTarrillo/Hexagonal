namespace Sicv1.Domain.Entities
{
    public class Ubigeo
    {
        public int ID { get; set; }
        public string UBIGEO { get; set; }
        public string DEPARTAMENT { get; set; }
        public string PROVINCE { get; set; }
        public string DISTRICT { get; set; }

        /// <summary>
        /// SP_UBIGEO_LIST_DPTO
        /// </summary>
        public string COD_DPTO { get; set; }
        public string NOM_DPTO { get; set; }

        /// <summary>
        /// SP_UBIGEO_LIST_PROV
        /// </summary>
        public string COD_PROV { get; set; }
        public string NOM_PROV { get; set; }

        /// <summary>
        /// SP_UBIGEO_LIST_DIST
        /// </summary>
        public string COD_DIST { get; set; }
        public string NOM_DIST { get; set; }
    }
}
