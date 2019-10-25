using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMvc.Service.Abstract
{
    public interface IProductService
    {
        bool Add(DTO.Product product);
        
        List<DTO.Product> GetAllProducts();
        DTO.Product GetProduct(int id);
        bool UpdateProduct(DTO.Product product);
    }
}
