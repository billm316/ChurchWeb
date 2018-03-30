namespace ChurchWebEntities
{
    public class CarouselItem
    {
        public int CarouselItemId { get; set; }
        public int SortOrder { get; set; }
        public string SourceImage { get; set; }
        public string AltImageString { get; set; }
        public string Link { get; set; }
        public string LinkHeading { get; set; }
        public string LinkName { get; set; }

        public CarouselItem()
        {
        }
    }
}
