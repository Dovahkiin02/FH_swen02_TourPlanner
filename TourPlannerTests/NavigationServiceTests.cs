using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlannerUi.Services;
using TourPlannerUi.ViewModels;

namespace TourPlannerTests {
    [TestFixture]
    public class NavigationServiceTests {
        private Mock<IViewModelFactory> _mockViewModelFactory;
        private NavigationService _navigationService;

        [SetUp]
        public void SetUp() {
            _mockViewModelFactory = new Mock<IViewModelFactory>();
            _navigationService = new NavigationService(() => _mockViewModelFactory.Object);
        }

        [Test]
        public void CurrentView_WhenCalled_ReturnsCurrentViewModel() {
            var expectedViewModel = new Mock<ViewModel>().Object;
            _mockViewModelFactory.Setup(v => v.Create<ViewModel>()).Returns(expectedViewModel);

            _navigationService.NavigateTo<ViewModel>();

            Assert.That(_navigationService.CurrentView, Is.EqualTo(expectedViewModel));
        }

        [Test]
        public void NavigateTo_WhenCalled_CreatesAndSetsCurrentViewModel() {
            var expectedViewModel = new Mock<ViewModel>().Object;
            _mockViewModelFactory.Setup(v => v.Create<ViewModel>()).Returns(expectedViewModel);

            _navigationService.NavigateTo<ViewModel>();

            _mockViewModelFactory.Verify(v => v.Create<ViewModel>(), Times.Once);
            Assert.That(_navigationService.CurrentView, Is.EqualTo(expectedViewModel));
        }

        [Test]
        public void NavigateTo_WhenCalledWithParameters_CreatesAndSetsCurrentViewModelWithParameters() {
            var expectedViewModel = new Mock<ViewModel>().Object;
            var parameters = new object[] { "param1", 123 };
            _mockViewModelFactory.Setup(v => v.Create<ViewModel>(parameters)).Returns(expectedViewModel);

            _navigationService.NavigateTo<ViewModel>(parameters);

            _mockViewModelFactory.Verify(v => v.Create<ViewModel>(parameters), Times.Once);
            Assert.That(_navigationService.CurrentView, Is.EqualTo(expectedViewModel));
        }

        [Test]
        public void NavigateTo_WhenCalledMultipleTimes_CreatesNewViewModelEachTime() {
            var expectedViewModel1 = new Mock<ViewModel>().Object;
            var expectedViewModel2 = new Mock<ViewModel>().Object;
            _mockViewModelFactory.SetupSequence(v => v.Create<ViewModel>())
                .Returns(expectedViewModel1)
                .Returns(expectedViewModel2);

            _navigationService.NavigateTo<ViewModel>();
            Assert.That(_navigationService.CurrentView, Is.EqualTo(expectedViewModel1));

            _navigationService.NavigateTo<ViewModel>();
            Assert.That(_navigationService.CurrentView, Is.EqualTo(expectedViewModel2));
        }

        [Test]
        public void NavigateTo_WhenCalledAfterSettingCurrentView_OverwritesCurrentView() {
            var initialViewModel = new Mock<ViewModel>().Object;
            var expectedViewModel = new Mock<ViewModel>().Object;
            _navigationService.NavigateTo<ViewModel>(initialViewModel);
            _mockViewModelFactory.Setup(v => v.Create<ViewModel>()).Returns(expectedViewModel);

            _navigationService.NavigateTo<ViewModel>();

            Assert.That(_navigationService.CurrentView, Is.EqualTo(expectedViewModel));
        }

        [Test]
        public void NavigateTo_WhenViewModelFactoryReturnsNull_KeepsCurrentView() {
            var initialViewModel = new Mock<ViewModel>().Object;
            _navigationService.NavigateTo<ViewModel>(initialViewModel);
            var callCount = 0;

            _mockViewModelFactory.Setup(v => v.Create<ViewModel>())
                .Returns(() => {
                    callCount++;
                    if (callCount == 1) {
                        return initialViewModel;
                    }

                    return null;
                });

            _navigationService.NavigateTo<ViewModel>();

            Assert.That(_navigationService.CurrentView, Is.EqualTo(initialViewModel));
        }
    }
}
