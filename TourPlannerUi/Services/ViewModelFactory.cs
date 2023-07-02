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
        T Create<T>(params object[] parameters) where T : ViewModel;
    }
    public class ViewModelFactory : IViewModelFactory {
        private readonly IServiceProvider _serviceProvider;

        public ViewModelFactory(IServiceProvider serviceProvider) {
            _serviceProvider = serviceProvider;
        }

        public T Create<T>(params object[] parameters) where T : ViewModel {
            return ActivatorUtilities.CreateInstance<T>(_serviceProvider, parameters);
        }
    }
}
