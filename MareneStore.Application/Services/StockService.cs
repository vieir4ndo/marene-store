using MareneStore.Application.Dtos;
using MareneStore.Application.Interfaces;
using MareneStore.Domain.Entities;
using MareneStore.Domain.Exceptions;
using MareneStore.Infrastructure.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MareneStore.Application.Services
{
    public class StockService : IStockService
    {
        private readonly IStockRepository _repository;

        public StockService(IStockRepository repository)
        {
            _repository = repository;
        }
        public async Task<StockDto> CreateStockAsync(Guid productId)
        {
            if (await _repository.GetByProductIdAsync(productId) != null)
            {
                return null;
            }
            var stock = new Stock(productId);
            await _repository.AddAsync(stock);
            return StockDto.FromEntity(stock);
        }

        public async Task<(List<StockDto>, int)> GetAllAsync(int pageNumber, int pageSize)
        {
            var stocks = await _repository.GetAllAsync(pageNumber, pageSize);
            var totalCount = await _repository.GetTotalCountAsync();

            var outputStocks = new List<StockDto>();

            foreach (var s in stocks)
            {
                outputStocks.Add(StockDto.FromEntity(s));
            }

            return (outputStocks, totalCount);
        }

        public async Task RemoveByProductIdAsync(Guid productId)
        {
            var stock = await _repository.GetByProductIdAsync(productId);
            if (stock == null)
            {
                throw new NotFoundException();
            }

            _repository.Remove(stock);
            await _repository.SaveChangesAsync();
        }
    }
}
