using System.Collections.Generic;
using System.ServiceModel;
using Bars.Entities.Dto;
using Bars.Infrasctucture.Interfaces;

namespace Bars.Entities.Interfaces
{
    [ServiceContract]
    public interface IContractsService : IService
    {
        [OperationContract]
        public List<Contract> GetContracts();
    }
}
