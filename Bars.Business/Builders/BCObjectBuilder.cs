using Bars.Business.Interfaces;
using Bars.Infrasctucture.Builders;

namespace Bars.Business.Builders
{
    internal abstract class BCObjectBuilder : ObjectModelBuilder
    {
        protected TClass SimpleRegister<TInterface, TClass>()
            where TClass : IBusinessComponent, new()
            where TInterface : IBusinessComponent
        {
            var instance = new TClass();
            BCFacade.Instance.Register<TInterface>(instance);
            return instance;
        }
    }
}