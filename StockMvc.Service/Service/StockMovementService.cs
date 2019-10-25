using NLog;
using StockMvc.Data.Abstract;

using StockMvc.Service.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMvc.Service.Service
{
    public class StockMovementService : IStockMovementService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Data.Entity.StockMovement> _stockMovementRepository;
        private readonly ILogger _logger;

        public StockMovementService(IUnitOfWork unitOfWork)
        {
            _logger = LogManager.GetLogger("StockMovementService");
            _unitOfWork = unitOfWork;
            _stockMovementRepository = _unitOfWork.Repository<Data.Entity.StockMovement>();
        }

        public bool Add(DTO.StockMovement stockMovement)
        {
            try
            {
                _stockMovementRepository.Add(new Data.Entity.StockMovement
                {
                    CreatedAt = stockMovement.CreatedAt,
                    Description = stockMovement.Description,
                    Quantity = stockMovement.Quantity,
                    ModifiedAt = stockMovement.ModifiedAt,
                    Price = stockMovement.Price,
                    

                   
                });
                _unitOfWork.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex);
                return false;
            }
        }
        


        public List<DTO.StockMovement> GetAllStockMovements()
        {
            try
            {
                return _stockMovementRepository.Where().Select(s => new DTO.StockMovement
                {
                    CreatedAt = s.CreatedAt,
                    Description = s.Description,
                    Quantity = s.Quantity,
                    ModifiedAt = s.ModifiedAt,
                    Price = s.Price,
                    ProductId = s.ProductId,
                    StockMovementId = s.StockMovementId,
                    


                }).ToList();
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex);
                return null;
            }
        }

        public DTO.StockMovement GetStockMovement(int id)
        {
            try
            {
                return _stockMovementRepository.Where(k => k.StockMovementId == id).Select(s => new DTO.StockMovement
                {
                    CreatedAt = s.CreatedAt,
                    Description = s.Description,
                    Quantity = s.Quantity,
                    ModifiedAt = s.ModifiedAt,
                    Price = s.Price,
                    ProductId=s.ProductId,
                    StockMovementId=s.StockMovementId

                }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex);
                return null;
            }
        }

        public bool UpdateStockMovement(DTO.StockMovement stockMovement)
        {
            try
            {
                _stockMovementRepository.Update(new Data.Entity.StockMovement
                {
                    CreatedAt = stockMovement.CreatedAt,
                    Description = stockMovement.Description,
                    Quantity = stockMovement.Quantity,
                    ModifiedAt = stockMovement.ModifiedAt,
                    Price = stockMovement.Price,
                    




                });

                _unitOfWork.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex);
                // null
                return false;
            }
        }
    }
}
