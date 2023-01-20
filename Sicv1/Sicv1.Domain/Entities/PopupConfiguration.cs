namespace Sicv1.Domain.Entities
{
    public class PopupConfiguration : BaseClass
    {
        public string URL { get; set; }
        public string DESCRIPTION { get; set; }
        public string LINK_IMAGE { get; set; }
        public bool IS_LINKEABLE { get; set; }
        public bool IS_ACTIVE { get; set; }


        public string LINK_IMAGE_BANNER { get; set; }
        public string TERMS_CONDITION_BANNER { get; set; }
        public bool IS_ACTIVE_BANNER { get; set; }

    }
}
