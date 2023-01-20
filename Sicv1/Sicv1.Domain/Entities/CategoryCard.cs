namespace Sicv1.Domain.Entities
{
    public class CategoryCard : BaseClass
    {
        public int ID_CATEGORY { get; set; }
        public string HAVE_CARD { get; set; } = string.Empty;
        public bool HAVE_ACCESS { get; set; }
        public string BARCODE{ get; set; }
        public string BARCODE_FORMAT{ get; set; }
    }
}
