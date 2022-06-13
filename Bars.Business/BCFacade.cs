using Bars.Business.Interfaces;
using Bars.Infrasctucture.Components;

namespace Bars.Business
{
    public class BCFacade : ComponentsContainer<IBusinessComponent>, IBusinessComponent
    {
        private static BCFacade _instanceValue;
        public static BCFacade Instance => _instanceValue ??= new BCFacade();

        public TBcComponent GetBcComponent<TBcComponent>() where TBcComponent : IBusinessComponent
        {
            return Get<TBcComponent>();
        }
    }
}