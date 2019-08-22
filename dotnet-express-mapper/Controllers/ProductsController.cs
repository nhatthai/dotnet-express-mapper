using dotnet_express_mapper.Models;
using dotnet_express_mapper.Services;
using ExpressMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace dotnet_express_mapper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ProductService _productService;

        public ProductsController(ProductService productService)
        {
            _productService = productService;
        }

        // GET api/products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductViewModel>>> Get()
        {
            IEnumerable<Product> products = await _productService.GetProducts();
            IEnumerable<ProductViewModel> productViewModels = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(products);
            return new ObjectResult(productViewModels);
        }

        // GET api/products/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> Get(int id)
        {
            object product = await _productService.GetProduct(id);
            if (product == null)
                return new NotFoundResult();
            return new OkObjectResult(product);
        }

        // POST api/products
        [HttpPost]
        public async Task<ActionResult<Product>> Post([FromBody] ProductViewModel productViewModel)
        {
            Product product = Mapper.Map<ProductViewModel, Product>(productViewModel);
            await _productService.Create(product);
            return new OkObjectResult(product);
        }

        // PUT api/products/1
        [HttpPut("{id}")]
        public async Task<ActionResult<Product>> Put(int id, [FromBody] ProductViewModel productViewModel)
        {
            var productFromDb = await _productService.GetProduct(id);

            if (productFromDb == null)
                return new NotFoundResult();

            Product product = Mapper.Map<ProductViewModel, Product>(productViewModel);
            product.Id = productFromDb.Id;
            await _productService.UpdateProductAsync(product);

            return new OkObjectResult(product);
        }
    }


}