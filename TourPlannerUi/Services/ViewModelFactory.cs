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

        public ViewModelFactory() {
        }

        public ViewModel Create<TViewModel>(object? param = null) where TViewModel : ViewModel {
            var viewModelType = typeof(TViewModel);
            var paramType = param?.GetType();

            try {
                // Try to get a cached constructor
                if (!constructorCache.TryGetValue((viewModelType, paramType), out var constructor)) {
                    // If we have a parameter, we try to find a matching constructor
                    if (param != null) {
                        constructor = viewModelType.GetConstructor(new[] { paramType });
                        if (constructor == null) {
                            throw new InvalidOperationException($"No matching constructor found for ViewModel type {viewModelType.FullName} with a parameter of type {paramType.FullName}");
                        }
                    } else {
                        // If we don't have a parameter, or couldn't find a matching constructor, try to find a parameterless constructor
                        constructor = viewModelType.GetConstructor(Type.EmptyTypes);
                        if (constructor == null) {
                            throw new InvalidOperationException($"No parameterless constructor found for ViewModel type {viewModelType.FullName}");
                        }
                    }

                    // Cache the constructor for next time
                    constructorCache[(viewModelType, paramType)] = constructor;
                }

                return (ViewModel)constructor.Invoke(param != null ? new[] { param } : null);
            } catch (Exception ex) {
                throw new InvalidOperationException($"Error creating ViewModel of type {viewModelType.FullName}", ex);
            }
        }
    }
}
