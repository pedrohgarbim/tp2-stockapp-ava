using StockApp.Domain.Entities;
using StockApp.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockApp.Application.Services
{
    public class ProductImportService
    {
        private readonly IProductRepository _productRepository;

        public ProductImportService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task ImportProductAsync(Stream fileStream)
        {
            using (var reader = new StreamReader(fileStream))
            {
                while (!reader.EndOfStream)
                {
                    var line = await reader.ReadLineAsync();
                    var values = line.Split(',');

                    var product = new Product(

                        name: values[0],
                        description: values[1],
                        price: decimal.Parse(values[2], CultureInfo.InvariantCulture),
                        stock: int.Parse(values[3]),
                        image: values[4]

                    );

                    product.CategoryId = int.Parse(values[5]);

                    await _productRepository.AddAsync(product);
                }
            }
        }
    }
}
