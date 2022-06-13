using System.Collections.Generic;
using Bars.Entities.Dto;

namespace Bars.Data.Interfaces
{
    public interface IDAContracts : IDAComponent
    {
        public List<Contract> GetList();
    }
}
