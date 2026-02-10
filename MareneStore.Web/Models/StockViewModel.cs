using MareneStore.Application.Dtos;
using System.Collections.Generic;

namespace MareneStore.Web.Models
{
    public class StockViewModel
    {
        public List<StockItemViewModel> Items { get; set; }

        public static StockViewModel FromDto(List<StockDto> dtos)
        {
            return new StockViewModel
            {
                Items = dtos.ConvertAll(dto => new StockItemViewModel
                {
                    ProductName = dto.ProductName,
                    ProductQuantity = dto.ProductQuantity
                })
            };
        }

        public class StockItemViewModel
        {
            public string ProductName { get; set; }
            public int ProductQuantity { get; set; }
        }
    }
}