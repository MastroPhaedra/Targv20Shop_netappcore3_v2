using System;
using System.Threading.Tasks;
using Targv20Shop.Core.Domain;
using Targv20Shop.Core.Dtos;

namespace Targv20Shop.Core.ServiceInterface
{
    public interface IProductService : IApplicationService
    {
        Task<Product> Delete(Guid id);

        Task<Product> Add(ProductDto dto);

        Task<Product> Edit(Guid id);

        Task<Product> Update(ProductDto dto);
    }
}
