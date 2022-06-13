using System.Collections.Generic;
using System.ServiceModel;
using Bars.Entities.Dto;

namespace Bars.Interfaces
{
    [ServiceContract]
    public interface IServiceContracts : IService
    {
        [OperationContract]
        public List<Contract> GetContracts();
    }
}
