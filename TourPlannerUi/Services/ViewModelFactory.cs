using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TourPlannerUi.ViewModels;

namespace TourPlannerUi.Services {
    public interface IViewModelFactory {
        public ViewModel Create<TViewModel>(object? param = null) where TViewModel : ViewModel;
    }
    public class ViewModelFactory : IViewModelFactory {
        private readonly ConcurrentDictionary<(Type viewModelType, Type paramType), ConstructorInfo> constructorCache =
            new ConcurrentDictionary<(Type viewModelType, Type paramType), ConstructorInfo>();

        private readonly IServiceProvider _serviceProvider;

        public ViewModelFactory(IServiceProvider serviceProvider) {
            _serviceProvider = serviceProvider;
        }

        public ViewModel Create<TViewModel>(object? param = null) where TViewModel : ViewModel {
            var viewModelType = typeof(TViewModel);
            var paramType = param?.GetType();

            try {
                if (!constructorCache.TryGetValue((viewModelType, paramType), out var constructor)) {
                    var constructors = viewModelType.GetConstructors();

                    constructor = constructors.FirstOrDefault();

                    if (constructor == null) {
                        throw new InvalidOperationException($"No suitable constructor found for ViewModel type {viewModelType.FullName}");
                    }

                    constructorCache[(viewModelType, paramType)] = constructor;
                }

                // Use the service provider to get required services for the constructor
                var args = constructor.GetParameters()
                    .Select(p => _serviceProvider.GetService(p.ParameterType) ?? param)
                    .ToArray();

                return (ViewModel)constructor.Invoke(args);
            } catch (Exception ex) {
                throw new InvalidOperationException($"Error creating ViewModel of type {viewModelType.FullName}", ex);
            }
        }
    }
}
