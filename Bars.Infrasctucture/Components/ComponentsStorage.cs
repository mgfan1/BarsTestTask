using System;
using System.Collections.Generic;

namespace Bars.Infrasctucture.Components
{
    public class ComponentsStorage<TBaseType>
    {
        private const string WrongComponentTypeExMessage = "Component registration failed, wrong component type";
        private const string AlreadyRegisteredExMessage = "Component registration failed, component has been already registered";
        private readonly Dictionary<Type, TBaseType> _componentsValue;

        #region Methods

        public ComponentsStorage()
        {
            _componentsValue = new Dictionary<Type, TBaseType>();
        }

        public void Register<T>(TBaseType component) where T : TBaseType
        {
            if (!(component is T))
            {
                throw new InvalidOperationException(WrongComponentTypeExMessage);
            }

            if (_componentsValue.ContainsKey(typeof(T)))
            {
                throw new InvalidOperationException(AlreadyRegisteredExMessage);
            }

            _componentsValue.Add(typeof(T), component);
        }

        public bool Exists<T>() where T : TBaseType
        {
            return _componentsValue.ContainsKey(typeof(T));
        }

        public T Get<T>() where T : TBaseType
        {
            return (T)_componentsValue[typeof(T)];
        }

        #endregion
    }
}