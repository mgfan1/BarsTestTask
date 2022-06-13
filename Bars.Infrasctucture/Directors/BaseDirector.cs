using System.Collections.Generic;
using Bars.Infrasctucture.Builders;
using Bars.Infrasctucture.Components;
using Bars.Infrasctucture.Interfaces;

namespace Bars.Infrasctucture.Directors
{
    public abstract class BaseDirector
    {
        protected internal virtual List<ObjectModelBuilder> Builders { get; }

        protected BaseDirector()
        {
            Builders = new List<ObjectModelBuilder>();
        }

        public void Register(ObjectModelBuilder builder)
        {
            Builders.Add(builder);
        }

        protected internal void InvokeBuild(ComponentsStorage<IComponent> model)
        {
            foreach (var builder in Builders)
            {
                builder.Build(model);
            }
        }
    }
}