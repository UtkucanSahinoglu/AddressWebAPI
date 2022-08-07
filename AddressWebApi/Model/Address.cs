using System.Collections.Generic;


namespace AddressWebApi.Model
{
    public class Address
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string OpenAddress { get; set; }
        public virtual User user { get; set; }

    }
}
