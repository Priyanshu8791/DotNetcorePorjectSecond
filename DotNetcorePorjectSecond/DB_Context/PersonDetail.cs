using System;
using System.Collections.Generic;

#nullable disable

namespace DotNetcorePorjectSecond.DB_Context
{
    public partial class PersonDetail
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
    }
}
