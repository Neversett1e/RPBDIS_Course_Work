namespace WebApplication3
{
    public class PublisherFilterViewModel
    {
        public string Name { get; }
        public string City { get; }
        public string Address { get; }

        public PublisherFilterViewModel(string name, string city, string address)
        {
            Name = name;
            City = city;
            Address = address;
        }
    }
}

