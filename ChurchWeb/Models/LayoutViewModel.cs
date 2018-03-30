using System.Collections.Generic;
using ChurchWebEntities;

namespace ChurchWeb.Models
{
    public class LayoutViewModel
    {
        public List<NavBarItem> NavBarItems { get; set; }

        public LayoutViewModel()
        {
            NavBarItems = new List<NavBarItem>();
        }
    }
}
