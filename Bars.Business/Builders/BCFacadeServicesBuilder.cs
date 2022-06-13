using Bars.Business.BusinessComponents;
using Bars.Business.Interfaces;
using Bars.Infrasctucture.Components;
using Bars.Infrasctucture.Interfaces;

namespace Bars.Business.Builders
{
    internal class BCFacadeServicesBuilder : BCObjectBuilder
    {
        public override void Build(ComponentsStorage<IComponent> model)
        {
            SimpleRegister<IBCContracts, BCContracts>();
        }
    }
}
