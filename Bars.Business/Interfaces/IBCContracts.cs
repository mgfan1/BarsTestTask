using System.Collections.Generic;
using Bars.Entities.Dto;
using Bars.Infrasctucture.Entities;

namespace Bars.Business.Interfaces
{
    public interface IBCContracts : IBusinessComponent
    {
        public OperationResult<List<Contract>> GetList();
    }
}
