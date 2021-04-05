using Sdk.Core.Entities;
using Sdk.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.Utils.Helpers
{
    public class BinWdithCalculator : IBinWidthCalculator
    {
        private const int MugProductStackCount = 4;

        public decimal CalculateMinBinWidth(List<ProductEntity> products)
        {
            if (products == null)
                throw new ArgumentNullException(nameof(products));

            decimal totalMinBinWidth = products
                .Where(p => p.UnitType != ProductTypes.Mug.ToString())
                .Sum(p => p.Quantity * p.UnitSize);

            var mugProduct = products
                .FirstOrDefault(p => p.UnitType == ProductTypes.Mug.ToString());

            return totalMinBinWidth + CalculateMugProductsMinBinWidth(mugProduct);
        }

        private decimal CalculateMugProductsMinBinWidth(ProductEntity mugProduct)
        {
            return mugProduct != null
                 ? (mugProduct.Quantity / MugProductStackCount * mugProduct.UnitSize) +
                   (mugProduct.Quantity % MugProductStackCount != 0 ? mugProduct.UnitSize : 0)
                 : 0;
        }
    }

    public static class UnitTypeExtension
    {
        public static IEnumerable<T> Filter<T>(this IEnumerable<T> items, Predicate<T> predicate)
        {
            if (items == null)
                yield return default(T);

            foreach (var item in items)
            {
                if (predicate(item))
                    yield return item;
            }
        }
    }
}
