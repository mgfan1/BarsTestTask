using Bars.Data.DAComponents;
using Bars.Data.DataAccess;
using Bars.Data.Interfaces;
using Bars.Infrasctucture.Components;
using Bars.Infrasctucture.Interfaces;

namespace Bars.Data.Builders
{
    internal class DAFacadeServicesBuilder : DAObjectBuilder
    {
        public override void Build(ComponentsStorage<IComponent> model)
        {
            SimpleRegister<IConnectionContainer, ConnectionContainer>();
            SimpleRegister<IDAContracts, DAContracts>();
        }
    }
}
