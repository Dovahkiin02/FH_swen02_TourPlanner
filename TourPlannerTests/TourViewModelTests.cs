using Moq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TourPlannerUi.Models;
using TourPlannerUi.Services;
using TourPlannerUi.ViewModels;

namespace TourPlannerTests {
    [TestFixture]
    public class TourViewModelTests {
        private Mock<INavigationService> _mockNavigationService;
        private Mock<ITourLogModel> _mockTourLogModel;
        private Mock<ITourModel> _mockTourModel;
        private Mock<IGeneratePdfService> _mockGeneratePdfService;
        private TourViewModel _viewModel;

        [SetUp]
        public void SetUp() {
            _mockNavigationService = new Mock<INavigationService>();
            _mockTourLogModel = new Mock<ITourLogModel>();
            _mockTourModel = new Mock<ITourModel>();
            _mockGeneratePdfService = new Mock<IGeneratePdfService>();
            _viewModel = new TourViewModel(
                new Tour { Id = 1 },
                _mockTourLogModel.Object,
                _mockNavigationService.Object,
                _mockGeneratePdfService.Object,
                _mockTourModel.Object
            );
        }

        [Test]
        public void OnEdit_WhenCalled_NavigatesToEditTourLogViewModel() {
            var tourLog = new TourLog { Id = Guid.NewGuid() };

            _viewModel.EditCommand.Execute(tourLog);

            _mockNavigationService.Verify(s => s.NavigateTo<EditTourLogViewModel>(tourLog, It.IsAny<Tour>()), Times.Once);
        }

        [Test]
        public void OnDelete_WhenCalled_RemovesTourLogAndLoadsTourLogsAsync() {
            var tourLog = new TourLog { Id = Guid.NewGuid() };
            _mockTourLogModel.Setup(m => m.RemoveTourLogAsync(tourLog.Id)).ReturnsAsync(HttpStatusCode.OK);

            _viewModel.DeleteCommand.Execute(tourLog);

            _mockTourLogModel.Verify(m => m.RemoveTourLogAsync(tourLog.Id), Times.Once);
            _mockTourLogModel.Verify(m => m.LoadTourLogsAsync(It.IsAny<int>()), Times.Exactly(2));
        }

        [Test]
        public void OnCreate_WhenCalled_NavigatesToEditTourLogViewModel() {
            _viewModel.CreateCommand.Execute(null);

            _mockNavigationService.Verify(s => s.NavigateTo<EditTourLogViewModel>(It.IsAny<Tour>()), Times.Once);
        }

        [Test]
        public void OnGeneratePdf_WhenCalled_CallsGeneratePdfService() {
            _viewModel.GeneratePdfCommand.Execute(null);

            _mockGeneratePdfService.Verify(s => s.create(It.IsAny<Tour>()), Times.Once);
        }
    }
}
