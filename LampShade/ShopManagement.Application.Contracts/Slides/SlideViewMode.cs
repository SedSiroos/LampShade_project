namespace ShopManagement.Application.Contracts.Slides
{
    public class SlideViewMode
    {
        public long Id { get; set; }
        public string Picture { get; set; }
        public string Title { get; set; }
        public string Heading { get; set; }
        public bool IsRemove { get; set; }
        public string CreationDate { get; set; }
    }
}