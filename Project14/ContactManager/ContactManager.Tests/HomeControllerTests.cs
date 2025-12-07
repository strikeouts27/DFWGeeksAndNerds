using Xunit;
using Moq;
using ContactManager.Controllers;
using ContactManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;

namespace ContactManager.Tests
{
    public class HomeControllerTests
    {
        private readonly Mock<ILogger<HomeController>> _mockLogger;
        private readonly Mock<IContactRepository> _mockRepository;
        private readonly HomeController _controller;

        public HomeControllerTests()
        {
            _mockLogger = new Mock<ILogger<HomeController>>();
            _mockRepository = new Mock<IContactRepository>();
            _controller = new HomeController(_mockLogger.Object, _mockRepository.Object);
        }

        [Fact]
        public void Index_ReturnsViewResult_WithListOfContacts()
        {
            // Arrange
            var mockContacts = new List<Contact>
            {
                new Contact { ContactId = 1, FirstName = "John", LastName = "Doe", Email = "john@test.com", Phone = "555-1234" },
                new Contact { ContactId = 2, FirstName = "Jane", LastName = "Smith", Email = "jane@test.com", Phone = "555-5678" }
            };
            _mockRepository.Setup(repo => repo.GetAllContacts()).Returns(mockContacts);
            _mockRepository.Setup(repo => repo.Count()).Returns(mockContacts.Count);

            // Act
            var result = _controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Contact>>(viewResult.ViewData.Model);
            Assert.Equal(2, model.Count());
        }

        [Fact]
        public void Index_SetsContactCountInViewBag()
        {
            // Arrange
            var mockContacts = new List<Contact>
            {
                new Contact { ContactId = 1, FirstName = "John", LastName = "Doe", Email = "john@test.com", Phone = "555-1234" },
                new Contact { ContactId = 2, FirstName = "Jane", LastName = "Smith", Email = "jane@test.com", Phone = "555-5678" },
                new Contact { ContactId = 3, FirstName = "Bob", LastName = "Johnson", Email = "bob@test.com", Phone = "555-9012" }
            };
            _mockRepository.Setup(repo => repo.GetAllContacts()).Returns(mockContacts);
            _mockRepository.Setup(repo => repo.Count()).Returns(mockContacts.Count);

            // Act
            var result = _controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(3, viewResult.ViewData["ContactCount"]);
        }

        [Fact]
        public void Index_ReturnsViewResult_WithEmptyList_WhenNoContacts()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.GetAllContacts()).Returns(new List<Contact>());
            _mockRepository.Setup(repo => repo.Count()).Returns(0);

            // Act
            var result = _controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Contact>>(viewResult.ViewData.Model);
            Assert.Empty(model);
            Assert.Equal(0, viewResult.ViewData["ContactCount"]);
        }

        [Fact]
        public void Privacy_ReturnsViewResult()
        {
            // Act
            var result = _controller.Privacy();

            // Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Error_ReturnsViewResult_WithErrorViewModel()
        {
            // Arrange
            var httpContext = new DefaultHttpContext();
            _controller.ControllerContext = new ControllerContext()
            {
                HttpContext = httpContext
            };

            // Act
            var result = _controller.Error();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsType<ErrorViewModel>(viewResult.ViewData.Model);
        }

        [Fact]
        public void Constructor_ThrowsArgumentNullException_WhenLoggerIsNull()
        {
            // Arrange & Act & Assert
            Assert.Throws<ArgumentNullException>(() => 
                new HomeController(null!, _mockRepository.Object));
        }

        [Fact]
        public void Constructor_ThrowsArgumentNullException_WhenRepositoryIsNull()
        {
            // Arrange & Act & Assert
            Assert.Throws<ArgumentNullException>(() => 
                new HomeController(_mockLogger.Object, null!));
        }

        [Fact]
        public void Index_CallsGetAllContacts_Once()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.GetAllContacts()).Returns(new List<Contact>());
            _mockRepository.Setup(repo => repo.Count()).Returns(0);

            // Act
            _controller.Index();

            // Assert
            _mockRepository.Verify(repo => repo.GetAllContacts(), Times.Once);
        }

        [Fact]
        public void Index_CallsCount_Once()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.GetAllContacts()).Returns(new List<Contact>());
            _mockRepository.Setup(repo => repo.Count()).Returns(0);

            // Act
            _controller.Index();

            // Assert
            _mockRepository.Verify(repo => repo.Count(), Times.Once);
        }
    }
}
