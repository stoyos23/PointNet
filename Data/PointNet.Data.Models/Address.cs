namespace PointNet.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using PointNet.Data.Common.Models;

    public class Address : BaseModel<int>
    {
        public string Country { get; set; }

        public string City { get; set; }

        public string Street { get; set; }

        public string HouseNumber { get; set; }
    }
}
