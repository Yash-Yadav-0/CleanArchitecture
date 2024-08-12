using CleanArchitecture.Application.Features.Products.Commands.CreateProduct;
using CleanArchitecture.Application.Features.Products.Exceptions;
using CleanArchitecture.Application.Features.Products.Rules;
using CleanArchitecture.Application.Interfaces.AutoMapper;
using CleanArchitecture.Application.Interfaces.Repositories;
using CleanArchitecture.Application.Interfaces.Storage;
using CleanArchitecture.Application.Interfaces.UnitOfWorks;
using CleanArchitecture.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Moq;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using static Xunit.Assert;

namespace CleanArchitecture.Application.Tests.Features.Products.Commands
{
    public class CreateProductCommandHandlerTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IProductRules> _mockProductRules;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<IHttpContextAccessor> _mockHttpContextAccessor;
        private readonly Mock<UserManager<User>> _mockUserManager;
        private readonly Mock<ILocalStorage> _mockLocalStorage;
        private readonly Mock<RoleManager<Role>> _mockRoleManager;
        private readonly CreateProductCommandHandler _handler;

        public CreateProductCommandHandlerTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockProductRules = new Mock<IProductRules>();
            _mockMapper = new Mock<IMapper>();
            _mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            _mockUserManager = new Mock<UserManager<User>>(MockBehavior.Strict, null, null, null, null, null, null, null, null);
            _mockLocalStorage = new Mock<ILocalStorage>();
            _mockRoleManager = new Mock<RoleManager<Role>>(MockBehavior.Strict, null, null, null, null);

            _handler = new CreateProductCommandHandler(
                _mockUnitOfWork.Object,
                _mockProductRules.Object,
                _mockMapper.Object,
                _mockHttpContextAccessor.Object,
                _mockUserManager.Object,
                _mockLocalStorage.Object,
                _mockRoleManager.Object
            );
        }

        [Fact]
        public async Task Handle_ValidRequest_ShouldCreateProduct()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var request = new CreateProductCommandRequest
            {
                Title = "New Product",
                Description = "Product Description",
                Price = 100.00m,
                Discount = 10.00m,
                BrandId = 1,
                CategortIds = new List<int> { 1, 2 },
                Images = null // Assuming you have a list of IFormFile for testing
            };

            var httpContext = new DefaultHttpContext();
            httpContext.User = new ClaimsPrincipal(new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                new Claim(ClaimTypes.Role, "ADMIN")
            }, "test"));

            _mockHttpContextAccessor.Setup(x => x.HttpContext).Returns(httpContext);
            _mockUserManager.Setup(x => x.FindByIdAsync(userId.ToString())).ReturnsAsync(new User());
            _mockUserManager.Setup(x => x.IsInRoleAsync(It.IsAny<User>(), "ADMIN")).ReturnsAsync(true);

            var mockProductRepo = new Mock<IReadRepository<Product>>();
            var mockWriteProductRepo = new Mock<IWriteRepository<Product>>();
            var mockWriteProductsCategoriesRepo = new Mock<IWriteRepository<ProductsCategories>>();
            var mockLocalStorage = new Mock<ILocalStorage>();

            _mockUnitOfWork.Setup(uow => uow.readRepository<Product>()).Returns(mockProductRepo.Object);
            _mockUnitOfWork.Setup(uow => uow.writeRepository<Product>()).Returns(mockWriteProductRepo.Object);
            _mockUnitOfWork.Setup(uow => uow.writeRepository<ProductsCategories>()).Returns(mockWriteProductsCategoriesRepo.Object);
            _mockUnitOfWork.Setup(uow => uow.SaveChangeAsync()).ReturnsAsync(1);

            mockProductRepo.Setup(x => x.Find(It.IsAny<Expression<Func<Product, bool>>>(), false)).ReturnsAsync(new List<Product>().AsQueryable());
            mockWriteProductRepo.Setup(x => x.AddAsync(It.IsAny<Product>())).Returns(Task.CompletedTask);
            mockWriteProductsCategoriesRepo.Setup(x => x.AddAsync(It.IsAny<ProductsCategories>())).Returns(Task.CompletedTask);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            NotNull(result);
            _mockUserManager.Verify(x => x.FindByIdAsync(userId.ToString()), Times.Once);
            _mockUserManager.Verify(x => x.IsInRoleAsync(It.IsAny<User>(), "ADMIN"), Times.Once);
            mockProductRepo.Verify(x => x.Find(It.IsAny<Expression<Func<Product, bool>>>(), false), Times.Once);
            mockWriteProductRepo.Verify(x => x.AddAsync(It.IsAny<Product>()), Times.Once);
            mockWriteProductsCategoriesRepo.Verify(x => x.AddAsync(It.IsAny<ProductsCategories>()), Times.Exactly(request.CategortIds.Count));
        }
    }
}
