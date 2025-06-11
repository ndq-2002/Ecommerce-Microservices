using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Products.Domain.ModelMetas
{
    public class ProductMeta
    {
        public string CompanyId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string CategoryId { get; set; }
        public bool IsActive { get; set; }
        public string ConcurrencyStamp { get; set; }
    }
}
