using Bars.Infrasctucture.Components;
using Bars.Infrasctucture.Interfaces;

namespace Bars.Infrasctucture.Builders
{
    public abstract class ObjectModelBuilder
    {
        public abstract void Build(ComponentsStorage<IComponent> model);
    }
}