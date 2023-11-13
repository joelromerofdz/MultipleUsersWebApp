using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Province
    {
        public int Id { get; set; }
        public string ProvinceName { get; set; }
        public int CountryId { get; set; }
    }
}
