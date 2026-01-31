using MareneStore.Application.Dtos;
using System.Threading.Tasks;

namespace MareneStore.Application.Interfaces
{
    public interface IProductService
    {
        Task<ProductDto> CreateProductAsync(ProductDto product);
    }
}
