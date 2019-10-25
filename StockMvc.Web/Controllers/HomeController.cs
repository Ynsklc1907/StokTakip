using StockMvc.Service.Abstract;
using System.Web.Mvc;

namespace StockMvc.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductService _productService;
        private readonly IStockMovementService _stockMovementService;

        public HomeController(IProductService productService,IStockMovementService stockMovementService)
        {
            _productService = productService;
            _stockMovementService = stockMovementService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult AddProduct(DTO.Product product)
        {
            var result = _productService.Add(product);
            TempData["message"] = result ? "Kaydedildi." : "Hata Oluştu";
            return RedirectToAction("Contact");
        }

        public ActionResult StockMovementList(DTO.StockMovement stockMovement)
        {
           return View(_stockMovementService.GetAllStockMovements());

        }
        public ActionResult StockMovementAdd(DTO.StockMovement stockMovement)
        {
            ViewData["StockMovements"] = _stockMovementService.GetAllStockMovements();
            return View();

        }

        



    }
}