using Xunit;
using Moq;
using ContactManager.Controllers;
using ContactManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Http;

namespace ContactManager.Tests
{
    public class ContactControllerTests
    {
        private readonly Mock<IContactRepository> _mockRepository;
        private readonly ContactController _controller;

        public ContactControllerTests()
        {
            _mockRepository = new Mock<IContactRepository>();
            _controller = new ContactController(_mockRepository.Object);
            
            // Setup TempData for testing
            var httpContext = new DefaultHttpContext();
            var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());
            _controller.TempData = tempData;
        }

        #region Index Tests

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

            // Act
            var result = _controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Contact>>(viewResult.ViewData.Model);
            Assert.Equal(2, model.Count());
        }

        [Fact]
        public void Index_ReturnsEmptyList_WhenNoContacts()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.GetAllContacts()).Returns(new List<Contact>());

            // Act
            var result = _controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Contact>>(viewResult.ViewData.Model);
            Assert.Empty(model);
        }

        #endregion

        #region Details Tests

        [Fact]
        public void Details_ReturnsViewResult_WithContact_WhenContactExists()
        {
            // Arrange
            var contact = new Contact { ContactId = 1, FirstName = "John", LastName = "Doe", Email = "john@test.com", Phone = "555-1234" };
            _mockRepository.Setup(repo => repo.GetContactById(1)).Returns(contact);

            // Act
            var result = _controller.Details(1);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<Contact>(viewResult.ViewData.Model);
            Assert.Equal(1, model.ContactId);
            Assert.Equal("John", model.FirstName);
        }

        [Fact]
        public void Details_ReturnsNotFound_WhenContactDoesNotExist()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.GetContactById(It.IsAny<int>())).Returns((Contact?)null);

            // Act
            var result = _controller.Details(999);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Details_ReturnsNotFound_WhenIdIsZero()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.GetContactById(0)).Returns((Contact?)null);

            // Act
            var result = _controller.Details(0);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Details_ReturnsNotFound_WhenIdIsNegative()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.GetContactById(-1)).Returns((Contact?)null);

            // Act
            var result = _controller.Details(-1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        #endregion

        #region Add Tests (GET)

        [Fact]
        public void Add_Get_ReturnsViewResult()
        {
            // Act
            var result = _controller.Add();

            // Assert
            Assert.IsType<ViewResult>(result);
        }

        #endregion

        #region Add Tests (POST)

        [Fact]
        public void Add_Post_RedirectsToIndex_WhenModelIsValid()
        {
            // Arrange
            var contact = new Contact 
            { 
                FirstName = "John", 
                LastName = "Doe", 
                Email = "john@test.com", 
                Phone = "555-1234" 
            };
            _mockRepository.Setup(repo => repo.AddContact(It.IsAny<Contact>()));

            // Act
            var result = _controller.Add(contact);

            // Assert
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectResult.ActionName);
        }

        [Fact]
        public void Add_Post_CallsAddContact_WhenModelIsValid()
        {
            // Arrange
            var contact = new Contact 
            { 
                FirstName = "John", 
                LastName = "Doe", 
                Email = "john@test.com", 
                Phone = "555-1234" 
            };

            // Act
            _controller.Add(contact);

            // Assert
            _mockRepository.Verify(repo => repo.AddContact(It.IsAny<Contact>()), Times.Once);
        }

        [Fact]
        public void Add_Post_SetsTempDataSuccessMessage_WhenModelIsValid()
        {
            // Arrange
            var contact = new Contact 
            { 
                FirstName = "John", 
                LastName = "Doe", 
                Email = "john@test.com", 
                Phone = "555-1234" 
            };

            // Act
            _controller.Add(contact);

            // Assert
            Assert.NotNull(_controller.TempData["SuccessMessage"]);
            Assert.Contains("John Doe", _controller.TempData["SuccessMessage"]?.ToString());
            Assert.Contains("successfully added", _controller.TempData["SuccessMessage"]?.ToString());
        }

        [Fact]
        public void Add_Post_ReturnsViewWithModel_WhenModelIsInvalid()
        {
            // Arrange
            var contact = new Contact { FirstName = "John" }; // Missing required fields
            _controller.ModelState.AddModelError("LastName", "Required");

            // Act
            var result = _controller.Add(contact);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<Contact>(viewResult.ViewData.Model);
            Assert.Equal("John", model.FirstName);
        }

        [Fact]
        public void Add_Post_DoesNotCallAddContact_WhenModelIsInvalid()
        {
            // Arrange
            var contact = new Contact { FirstName = "John" };
            _controller.ModelState.AddModelError("LastName", "Required");

            // Act
            _controller.Add(contact);

            // Assert
            _mockRepository.Verify(repo => repo.AddContact(It.IsAny<Contact>()), Times.Never);
        }

        #endregion

        #region Edit Tests (GET)

        [Fact]
        public void Edit_Get_ReturnsViewResult_WithContact_WhenContactExists()
        {
            // Arrange
            var contact = new Contact { ContactId = 1, FirstName = "John", LastName = "Doe", Email = "john@test.com", Phone = "555-1234" };
            _mockRepository.Setup(repo => repo.GetContactById(1)).Returns(contact);

            // Act
            var result = _controller.Edit(1);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<Contact>(viewResult.ViewData.Model);
            Assert.Equal(1, model.ContactId);
        }

        [Fact]
        public void Edit_Get_ReturnsNotFound_WhenContactDoesNotExist()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.GetContactById(It.IsAny<int>())).Returns((Contact?)null);

            // Act
            var result = _controller.Edit(999);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        #endregion

        #region Edit Tests (POST)

        [Fact]
        public void Edit_Post_RedirectsToIndex_WhenModelIsValid()
        {
            // Arrange
            var contact = new Contact 
            { 
                ContactId = 1,
                FirstName = "John", 
                LastName = "Doe", 
                Email = "john@test.com", 
                Phone = "555-1234" 
            };
            _mockRepository.Setup(repo => repo.UpdateContact(It.IsAny<Contact>()));

            // Act
            var result = _controller.Edit(contact);

            // Assert
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectResult.ActionName);
        }

        [Fact]
        public void Edit_Post_CallsUpdateContact_WhenModelIsValid()
        {
            // Arrange
            var contact = new Contact 
            { 
                ContactId = 1,
                FirstName = "John", 
                LastName = "Doe", 
                Email = "john@test.com", 
                Phone = "555-1234" 
            };

            // Act
            _controller.Edit(contact);

            // Assert
            _mockRepository.Verify(repo => repo.UpdateContact(It.IsAny<Contact>()), Times.Once);
        }

        [Fact]
        public void Edit_Post_SetsTempDataSuccessMessage_WhenModelIsValid()
        {
            // Arrange
            var contact = new Contact 
            { 
                ContactId = 1,
                FirstName = "John", 
                LastName = "Doe", 
                Email = "john@test.com", 
                Phone = "555-1234" 
            };

            // Act
            _controller.Edit(contact);

            // Assert
            Assert.NotNull(_controller.TempData["SuccessMessage"]);
            Assert.Contains("John Doe", _controller.TempData["SuccessMessage"]?.ToString());
            Assert.Contains("successfully updated", _controller.TempData["SuccessMessage"]?.ToString());
        }

        [Fact]
        public void Edit_Post_ReturnsViewWithModel_WhenModelIsInvalid()
        {
            // Arrange
            var contact = new Contact { ContactId = 1, FirstName = "John" };
            _controller.ModelState.AddModelError("LastName", "Required");

            // Act
            var result = _controller.Edit(contact);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<Contact>(viewResult.ViewData.Model);
            Assert.Equal(1, model.ContactId);
        }

        [Fact]
        public void Edit_Post_DoesNotCallUpdateContact_WhenModelIsInvalid()
        {
            // Arrange
            var contact = new Contact { ContactId = 1, FirstName = "John" };
            _controller.ModelState.AddModelError("LastName", "Required");

            // Act
            _controller.Edit(contact);

            // Assert
            _mockRepository.Verify(repo => repo.UpdateContact(It.IsAny<Contact>()), Times.Never);
        }

        #endregion

        #region Delete Tests (GET)

        [Fact]
        public void Delete_Get_ReturnsViewResult_WithContact_WhenContactExists()
        {
            // Arrange
            var contact = new Contact { ContactId = 1, FirstName = "John", LastName = "Doe", Email = "john@test.com", Phone = "555-1234" };
            _mockRepository.Setup(repo => repo.GetContactById(1)).Returns(contact);

            // Act
            var result = _controller.Delete(1);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<Contact>(viewResult.ViewData.Model);
            Assert.Equal(1, model.ContactId);
        }

        [Fact]
        public void Delete_Get_ReturnsNotFound_WhenContactDoesNotExist()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.GetContactById(It.IsAny<int>())).Returns((Contact?)null);

            // Act
            var result = _controller.Delete(999);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        #endregion

        #region Delete Tests (POST)

        [Fact]
        public void DeleteConfirmed_RedirectsToIndex_WhenContactExists()
        {
            // Arrange
            var contact = new Contact 
            { 
                ContactId = 1, 
                FirstName = "John", 
                LastName = "Doe", 
                Email = "john@test.com", 
                Phone = "555-1234" 
            };
            _mockRepository.Setup(repo => repo.GetContactById(1)).Returns(contact);
            _mockRepository.Setup(repo => repo.DeleteContact(1));

            // Act
            var result = _controller.DeleteConfirmed(1);

            // Assert
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectResult.ActionName);
        }

        [Fact]
        public void DeleteConfirmed_CallsDeleteContact_WhenContactExists()
        {
            // Arrange
            var contact = new Contact 
            { 
                ContactId = 1, 
                FirstName = "John", 
                LastName = "Doe", 
                Email = "john@test.com", 
                Phone = "555-1234" 
            };
            _mockRepository.Setup(repo => repo.GetContactById(1)).Returns(contact);

            // Act
            _controller.DeleteConfirmed(1);

            // Assert
            _mockRepository.Verify(repo => repo.DeleteContact(1), Times.Once);
        }

        [Fact]
        public void DeleteConfirmed_SetsTempDataSuccessMessage_WhenContactExists()
        {
            // Arrange
            var contact = new Contact 
            { 
                ContactId = 1, 
                FirstName = "John", 
                LastName = "Doe", 
                Email = "john@test.com", 
                Phone = "555-1234" 
            };
            _mockRepository.Setup(repo => repo.GetContactById(1)).Returns(contact);

            // Act
            _controller.DeleteConfirmed(1);

            // Assert
            Assert.NotNull(_controller.TempData["SuccessMessage"]);
            Assert.Contains("John Doe", _controller.TempData["SuccessMessage"]?.ToString());
            Assert.Contains("successfully deleted", _controller.TempData["SuccessMessage"]?.ToString());
        }

        [Fact]
        public void DeleteConfirmed_RedirectsToIndex_WhenContactDoesNotExist()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.GetContactById(999)).Returns((Contact?)null);

            // Act
            var result = _controller.DeleteConfirmed(999);

            // Assert
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectResult.ActionName);
        }

        [Fact]
        public void DeleteConfirmed_DoesNotCallDeleteContact_WhenContactDoesNotExist()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.GetContactById(999)).Returns((Contact?)null);

            // Act
            _controller.DeleteConfirmed(999);

            // Assert
            _mockRepository.Verify(repo => repo.DeleteContact(It.IsAny<int>()), Times.Never);
        }

        #endregion

        #region Constructor Tests

        [Fact]
        public void Constructor_ThrowsArgumentNullException_WhenRepositoryIsNull()
        {
            // Arrange & Act & Assert
            Assert.Throws<ArgumentNullException>(() => new ContactController(null!));
        }

        #endregion

        #region Edge Case Tests

        [Fact]
        public void Add_Post_HandlesContactWithNullOrganization()
        {
            // Arrange
            var contact = new Contact 
            { 
                FirstName = "John", 
                LastName = "Doe", 
                Email = "john@test.com", 
                Phone = "555-1234",
                Organization = null
            };

            // Act
            var result = _controller.Add(contact);

            // Assert
            _mockRepository.Verify(repo => repo.AddContact(It.IsAny<Contact>()), Times.Once);
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectResult.ActionName);
        }

        [Fact]
        public void Edit_Post_HandlesContactWithAllFieldsUpdated()
        {
            // Arrange
            var contact = new Contact 
            { 
                ContactId = 1,
                FirstName = "Jane", 
                LastName = "Smith", 
                Email = "jane@test.com", 
                Phone = "555-9999",
                Organization = "New Corp"
            };

            // Act
            var result = _controller.Edit(contact);

            // Assert
            _mockRepository.Verify(repo => repo.UpdateContact(It.Is<Contact>(c => 
                c.FirstName == "Jane" && 
                c.LastName == "Smith" && 
                c.Email == "jane@test.com" && 
                c.Phone == "555-9999" &&
                c.Organization == "New Corp"
            )), Times.Once);
        }

        [Fact]
        public void Index_HandlesLargeNumberOfContacts()
        {
            // Arrange
            var contacts = Enumerable.Range(1, 100).Select(i => new Contact
            {
                ContactId = i,
                FirstName = $"First{i}",
                LastName = $"Last{i}",
                Email = $"user{i}@test.com",
                Phone = $"555-{i:D4}"
            }).ToList();
            _mockRepository.Setup(repo => repo.GetAllContacts()).Returns(contacts);

            // Act
            var result = _controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Contact>>(viewResult.ViewData.Model);
            Assert.Equal(100, model.Count());
        }

        #endregion
    }
}
