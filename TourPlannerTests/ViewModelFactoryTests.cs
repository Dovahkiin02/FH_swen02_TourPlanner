using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlannerUi.Models;
using TourPlannerUi.Services;
using TourPlannerUi.ViewModels;

namespace TourPlannerTests {
    [TestFixture]
    public class ViewModelFactoryTests {
        private IServiceProvider _serviceProvider;
        private IViewModelFactory _viewModelFactory;

        [SetUp]
        public void SetUp() {
            var services = new ServiceCollection();
            var navigationServiceMock = new Mock<INavigationService>();
            var tourModelMock = new Mock<ITourModel>();
            var tourLogModelMock = new Mock<ITourLogModel>();
            var generatePdfServiceMock = new Mock<IGeneratePdfService>();
            var dataIOServiceMock = new Mock<IDataIOService>();

            services.AddSingleton<INavigationService>(navigationServiceMock.Object);
            services.AddSingleton<ITourModel>(tourModelMock.Object);
            services.AddSingleton<ITourLogModel>(tourLogModelMock.Object);
            services.AddSingleton<IGeneratePdfService>(generatePdfServiceMock.Object);
            services.AddSingleton<IDataIOService>(dataIOServiceMock.Object);

            services.AddSingleton<TourListViewModel>();
            services.AddTransient<TourViewModel>();
            services.AddTransient<CreateAndEditTourViewModel>();
            services.AddTransient<EditTourLogViewModel>();

            _serviceProvider = services.BuildServiceProvider();
            _viewModelFactory = new ViewModelFactory(_serviceProvider);
        }

        [Test]
        public void Create_GivenTourListViewModel_ReturnsTourListViewModelInstance() {
            var viewModel = _viewModelFactory.Create<TourListViewModel>();

            Assert.That(viewModel, Is.Not.Null);
            Assert.That(viewModel, Is.InstanceOf<TourListViewModel>());
        }

        [Test]
        public void Create_GivenTourViewModel_ReturnsTourViewModelInstance() {
            var tour = new Tour();
            var viewModel = _viewModelFactory.Create<TourViewModel>(tour);

            Assert.That(viewModel, Is.Not.Null);
            Assert.That(viewModel, Is.InstanceOf<TourViewModel>());
        }

        [Test]
        public void Create_GivenCreateAndEditTourViewModel_ReturnsCreateAndEditTourViewModelInstance() {
            var viewModel = _viewModelFactory.Create<CreateAndEditTourViewModel>();

            Assert.That(viewModel, Is.Not.Null);
            Assert.That(viewModel, Is.InstanceOf<CreateAndEditTourViewModel>());
        }

        [Test]
        public void Create_GivenEditTourLogViewModel_ReturnsEditTourLogViewModelInstance() {
            var tour = new Tour();
            var viewModel = _viewModelFactory.Create<EditTourLogViewModel>(tour);

            Assert.That(viewModel, Is.Not.Null);
            Assert.That(viewModel, Is.InstanceOf<EditTourLogViewModel>());
        }
    }
}
