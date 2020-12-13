using System;
using System.Collections.Generic;
using UnityEngine;
using HeathCo.GameServices;

namespace HeathCo.ServiceLocator
{
    /// <summary>
    /// Simple service locator for <see cref="IGameService"/> instances.
    /// </summary>
    public class ServiceLocator
    {
        private ServiceLocator() { }

        /// <summary>
        /// currently registered services.
        /// </summary>
        private readonly Dictionary<Type, IGameService> _services = new Dictionary<Type, IGameService>();

        /// <summary>
        /// Singleton instance of the service locator class.
        /// </summary>
        private static ServiceLocator _instance = null;

        /// <summary>
        /// Allows access to the service locator instance.
        /// </summary>
        public static ServiceLocator Instance
        {
            get
            {
                if (_instance == null)
				{
                    _instance = new ServiceLocator();
				}
                return _instance;
			}
        }

        /// <summary>
        /// Gets the service instance of the given type.
        /// </summary>
        /// <typeparam name="T">The type of the service to lookup.</typeparam>
        /// <returns>The service instance for the passed-in type.</returns>
        public T Get<T>() where T : IGameService
        {
            Type key = typeof(T);
            if (!_services.ContainsKey(key))
            {
                Debug.LogError($"{key} not registered with {GetType().Name}");
                throw new InvalidOperationException();
            }

            return (T)_services[key];
        }

        /// <summary>
        /// Registers the service with the current service locator.
        /// </summary>
        /// <typeparam name="T">Service type.</typeparam>
        /// <param name="service">Service instance.</param>
        public void Register<T>(T service) where T : IGameService
        {
            Type key = typeof(T);
            if (_services.ContainsKey(key))
            {
                Debug.LogError($"Attempted to register service of type {key} which is already registered with the {GetType().Name}.");
                return;
            }

            _services.Add(key, service);
        }

        /// <summary>
        /// Unregisters the service from the current service locator.
        /// </summary>
        /// <typeparam name="T">Service type.</typeparam>
        public void Unregister<T>() where T : IGameService
        {
            Type key = typeof(T);
            if (!_services.ContainsKey(key))
            {
                Debug.LogError($"Attempted to unregister service of type {key} which is not registered with the {GetType().Name}.");
                return;
            }

            _services.Remove(key);
        }
    }
}