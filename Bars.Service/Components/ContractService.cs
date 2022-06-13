using System.Collections.Generic;
using Bars.Data;
using Bars.Data.Interfaces;
using Bars.Entities.Dto;
using Bars.Entities.Interfaces;

namespace Bars.Service.Components
{
    public class ContractService : IContractsService
    {
        private IDAContracts DaContracts => DAFacade.Instance.GetDaComponent<IDAContracts>();

        public List<Contract> GetContracts()
        {
            return DaContracts.GetList();
        }
    }
}
