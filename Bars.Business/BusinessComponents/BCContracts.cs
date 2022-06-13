using System;
using System.Collections.Generic;
using Bars.Business.Extensions;
using Bars.Business.Interfaces;
using Bars.Entities.Dto;
using Bars.Entities.Interfaces;
using Bars.Infrasctucture.Entities;

namespace Bars.Business.BusinessComponents
{
    internal class BCContracts : BCComponentBase, IBCContracts
    {
        public OperationResult<List<Contract>> GetList()
        {
            var getContractsFunc = (Func<IContractsService, List<Contract>>)(channel => channel.GetContracts());
            return getContractsFunc.WcfInvoke();
        }
    }
}
