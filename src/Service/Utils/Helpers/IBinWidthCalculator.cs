using Sdk.Core.Entities;
using System.Collections.Generic;

namespace Service.Utils.Helpers
{
    public interface IBinWidthCalculator
    {
        decimal CalculateMinBinWidth(List<ProductEntity> products);
    }
}
