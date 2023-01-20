namespace Sicv1.Presentation.Models
{
    public class PopupConfigurationViewModel
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public string LinkImage { get; set; }
        public bool IsLinkeable { get; set; }
        public bool IsActive { get; set; }
        public int UpdateUser { get; set; }

        public string TermsBanner { get; set; }
        public string LinkBanner { get; set; }
        public bool IsActiveBanner { get; set; }





    }
}