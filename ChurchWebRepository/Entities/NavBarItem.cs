namespace ChurchWebEntities
{
    public class NavBarItem
    {
        public int NavBarItemId { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string Name { get; set; }

        public NavBarItem()
        {
        }
    }
}
