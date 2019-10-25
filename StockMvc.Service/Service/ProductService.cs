using NLog;
using StockMvc.Data.Abstract;
using StockMvc.DTO;
using StockMvc.Service.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMvc.Service.Service
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Data.Entity.Product> _productRepository;
        private readonly ILogger _logger;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _logger = LogManager.GetLogger("ProductService");
            _unitOfWork = unitOfWork;
            _productRepository = _unitOfWork.Repository<Data.Entity.Product>();
        }

        public bool Add(DTO.Product product)
        {
            try
            {
                _productRepository.Add(new Data.Entity.Product
                {
                    Brand = product.Brand,
                    CreatedAt = DateTime.Now,
                    Description = product.Description,
                    IsDeleted = false,
                    Model = product.Model,
                    ModifiedAt = DateTime.Now,
                    Name = product.Name,
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

       

        public void Delete(Product product, int id)
        {
            throw new NotImplementedException();
        }

        public List<DTO.Product> GetAllProducts()
        {
            try
            {
                return _productRepository.Where().Select(s => new DTO.Product
                {
                    Brand = s.Brand,
                    CreatedAt = s.CreatedAt,
                    Description=s.Description,

                }).ToList();
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex);
                return null;
            }
        }

        public DTO.Product GetProduct(int id)
        {
            try
            {
                return _productRepository.Where(k => k.ProductId == id).Select(s => new DTO.Product
                {
                    Brand = s.Brand,
                     ModifiedAt=s.ModifiedAt,
                     Model=s.Model,
                     IsDeleted=s.IsDeleted,
                     CreatedAt=s.CreatedAt,
                     Description=s.Description,
                     Name=s.Name,
                     ProductId=s.ProductId


                }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex);
                return null;
            }
        }

        public bool UpdateProduct(DTO.Product product)
        {
            try
            {
                _productRepository.Update(new Data.Entity.Product
                {
                    Brand = product.Brand,
                    ProductId = product.ProductId,
                    Name = product.Name,
                    Description = product.Description,
                    CreatedAt = product.CreatedAt,
                    IsDeleted = product.IsDeleted,
                    Model = product.Model,
                    ModifiedAt = product.ModifiedAt
                    
                   

                    

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
