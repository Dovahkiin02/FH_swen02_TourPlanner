using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TourPlannerUi.Models;
using TourPlannerUi.Services;
using TourPlannerUi.ViewModels;

namespace TourPlannerTests {
    [TestFixture]
    public class CreateAndEditTourViewModelTests {
        private Mock<INavigationService> _mockNavigationService;
        private Mock<ITourModel> _mockTourModel;
        private CreateAndEditTourViewModel _viewModel;
        private Tour _tour;

        [SetUp]
        public void SetUp() {
            _mockNavigationService = new Mock<INavigationService>();
            _mockTourModel = new Mock<ITourModel>();
            _tour = new Tour { Id = 1 };
            _viewModel = new CreateAndEditTourViewModel(_tour, _mockNavigationService.Object, _mockTourModel.Object);
        }

        [Test]
        public void OnCancel_WhenCalled_NavigatesToTourViewModel() {
            _viewModel.CancelCommand.Execute(null);

            _mockNavigationService.Verify(s => s.NavigateTo<TourViewModel>(It.IsAny<Tour>()), Times.Once);
        }

        [Test]
        public void OnCancel_WhenCalled_NavigatesToTourViewModelWithUnchangedTour() {
            var tour = new Tour();
            _viewModel = new CreateAndEditTourViewModel(tour, _mockNavigationService.Object, _mockTourModel.Object);

            _viewModel.CancelCommand.Execute(null);

            _mockNavigationService.Verify(n => n.NavigateTo<TourViewModel>(tour), Times.Once);
        }
    }
}
