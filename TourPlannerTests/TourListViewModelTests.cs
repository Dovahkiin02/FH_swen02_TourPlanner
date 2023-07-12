using Moq;
using System.Collections.ObjectModel;
using System.Net;
using TourPlannerUi.Models;
using TourPlannerUi.Services;
using TourPlannerUi.ViewModels;

namespace TourPlannerTests {
    [TestFixture]
    public class TourListViewModelTests {
        private Mock<INavigationService> _mockNavigationService;
        private Mock<ITourModel> _mockTourModel;
        private Mock<IGeneratePdfService> _mockGeneratePdfService;
        private Mock<IDataIOService> _mockDataIOService;
        private Mock<TourLogModel> _mockTourLogModel;
        private TourListViewModel _viewModel;

        [SetUp]
        public void Init() {
            _mockNavigationService = new Mock<INavigationService>();
            _mockTourModel = new Mock<ITourModel>();
            _mockTourLogModel = new Mock<TourLogModel>();
            _mockGeneratePdfService = new Mock<IGeneratePdfService>();
            _mockDataIOService = new Mock<IDataIOService>();

            _mockTourModel.Setup(t => t.TourList).Returns(new ObservableCollection<Tour>());

            _viewModel = new TourListViewModel(_mockNavigationService.Object,
                                               _mockTourModel.Object,
                                               _mockTourLogModel.Object,
                                               _mockGeneratePdfService.Object,
                                               _mockDataIOService.Object);
        }

        [Test]
        public void OnCreateTour_WhenCalled_NavigatesToCreateAndEditTourViewModel() {
            _viewModel.CreateTourCommand.Execute(null);

            _mockNavigationService.Verify(s => s.NavigateTo<CreateAndEditTourViewModel>(), Times.Once);
        }

        [Test]
        public async Task OnDeleteTour_WhenCalled_RemovesTourAndLoadsToursAsync() {
            var tour = new Tour { Id = 1 };
            _mockTourModel.Setup(m => m.RemoveTourAsync(tour.Id)).ReturnsAsync(HttpStatusCode.OK);

            _viewModel.DeleteTourCommand.Execute(tour);

            _mockTourModel.Verify(m => m.RemoveTourAsync(tour.Id), Times.Once);
            _mockTourModel.Verify(m => m.LoadToursAsync(), Times.Exactly(2)); 
        }

        [Test]
        public void OnEditTour_WhenCalledWithTour_NavigatesToCorrectViewModel() {
            var tour = new Tour { Id = 1 };

            _viewModel.EditTourCommand.Execute(tour);

            _mockNavigationService.Verify(s => s.NavigateTo<CreateAndEditTourViewModel>(tour), Times.Once);
            Assert.That(tour, Is.EqualTo(_viewModel.SelectedTour));
        }

        [Test]
        public void SearchText_WhenSet_FiltersTours() {
            var tour1 = new Tour { Id = 1, Name = "Tour1", Description = "Description1", From = "Place1", To = "Place2" };
            var tour2 = new Tour { Id = 2, Name = "Tour2", Description = "Description2", From = "Place3", To = "Place4" };
            var tours = new List<Tour> { tour1, tour2 };
            _mockTourModel.Setup(m => m.TourList).Returns(new ObservableCollection<Tour>(tours));
            _mockTourModel.Setup(m => m.UnfilteredTourList).Returns(tours);

            _viewModel.SearchText = "Tour1";

            CollectionAssert.AreEqual(new List<Tour> { tour1 }, _viewModel.TourList);
        }

    }
}