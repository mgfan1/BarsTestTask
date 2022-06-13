using Bars.Business.Interfaces;
using Bars.Infrasctucture.Components;

namespace Bars.Business.BusinessComponents
{
    public abstract class BCComponentBase : ComponentsContainer<IBusinessComponent>, IBusinessComponent { }
}