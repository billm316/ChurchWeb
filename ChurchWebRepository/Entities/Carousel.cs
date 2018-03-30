using System.Collections.Generic;

namespace ChurchWebEntities
{
    public class Carousel
    {
        public Carousel()
        {
            Items = new List<CarouselItem>();
        }
        public string DivId { get; set; }
        public List<CarouselItem> Items { get; set; }
    }
}
