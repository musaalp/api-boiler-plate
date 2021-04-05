using MediatR;
using System.Collections.Generic;

namespace Data.Orders.GetProductsByUnitTypes
{
    public class GetProductsByUnitTypesDataRequest : IRequest<GetProductsByUnitTypesDataResponse>
    {
        public List<string> UnitTypes { get; set; }
    }
}
