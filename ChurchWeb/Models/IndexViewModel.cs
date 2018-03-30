using System.Collections.Generic;
using ChurchWebEntities;

namespace ChurchWeb.Models
{
    public class IndexViewModel : LayoutViewModel
    {
        public List<CarouselItem> CarouselItems { get; set; }
    }
}
