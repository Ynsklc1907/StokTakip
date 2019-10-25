using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMvc.Service.Abstract
{
    public interface IStockMovementService
    {
        bool Add(DTO.StockMovement stockMovement);
        
        List<DTO.StockMovement> GetAllStockMovements();
        DTO.StockMovement GetStockMovement(int id);
        bool UpdateStockMovement(DTO.StockMovement stockMovement);
    }
}
