using Bars.Data.Interfaces;
using Bars.Infrasctucture.Builders;

namespace Bars.Data.Builders
{
    internal abstract class DAObjectBuilder : ObjectModelBuilder
    {
        protected TClass SimpleRegister<TInterface, TClass>()
            where TClass : IDAComponent, new()
            where TInterface : IDAComponent
        {
            var instance = new TClass();
            DAFacade.Instance.Register<TInterface>(instance);
            return instance;
        }
    }
}