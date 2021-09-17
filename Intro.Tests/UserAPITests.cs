using AutoMapper;
using Intro.Models.DTO;
using Intro.Models.Model;
using Intro.WebApi;
using Intro.WebApi.Repositories.Interfaces;
using Intro.WebApi.Services;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Intro.Tests
{
    [TestClass]
    public class UserAPITests
    {
        public UserServices _userService;

        public Mock<IUserRepository> _userRepository;
        public Mock<IUserTitleRepository> _userTitleRepository;
        public Mock<IUserTypeRepository> _usertTypeRepository;
        public Mock<ILogger<UserServices>> _logger;
        public IMapper _mapper;

        /// <summary>
        /// Setups Test instance.
        /// </summary>
        [TestInitialize]
        public void Setup()
        {
            // Mock logger and repositories
            _logger = new Mock<ILogger<UserServices>>();
            _userRepository = new Mock<IUserRepository>();
            _userTitleRepository = new Mock<IUserTitleRepository>();
            _usertTypeRepository = new Mock<IUserTypeRepository>();

            // Configure Automapper
            var userMappingConfig = new MapperConfiguration(c =>
            {
                c.AddProfile(new UserMappingProfile());
            });
            _mapper = userMappingConfig.CreateMapper();

            _userService = new UserServices(_logger.Object, _userRepository.Object, _mapper);
        }

        /// <summary>
        /// Test Delete user method from service.
        /// Delete method must change isActive value to false.
        /// </summary>
        [TestMethod]
        public void AssertUserIsDeleted_Success()
        {
            // TODO regions within methods should be avoided
            User user = new User
            {
                Id = 1,
                Name = "John",
                Surname = "Doe",
                BirthDate = System.DateTime.Now,
                EmailAddress = "test@test.gr",
                UserTitle = new UserTitle
                {
                    Id = 1,
                    Description = "Title Description",
                },
                UserType = new UserType
                {
                    Id = 1,
                    Description = "Type Description",
                    Code = "TS"
                },
                IsActive = true
            };

            var deletedUserEntity = _userService.DeleteUserAction(user);

            Assert.AreEqual(deletedUserEntity.IsActive, false);
        }

        /// <summary>
        /// Tests Create user entity method from service.
        ///
        /// </summary>
        [TestMethod]
        public void TestAddUser_AssertSuccess()
        {
            CreateEditUserDTO dto = new CreateEditUserDTO
            {
                Name = "John",
                Surname = "Doe",
                BirthDate = System.DateTime.Now,
                EmailAddress = "test@mail.gr",
                UserTitleId = 5,
                UserTypeId = 5
            };

            User user = _userService.CreateUserAction(dto);

            Assert.AreEqual(dto.Name, user.Name);
            Assert.AreEqual(user.IsActive, true);
        }

        /// <summary>
        /// Tests edit user entity method from service.
        /// </summary>
        [TestMethod]
        public void TestEditUser_assertSuccess()
        {
            User user = new User
            {
                Id = 1,
                Name = "John",
                Surname = "Doe",
                BirthDate = System.DateTime.Now,
                EmailAddress = "test@test.gr",
                UserTitle = new UserTitle
                {
                    Id = 1,
                    Description = "Title Description",
                },
                UserType = new UserType
                {
                    Id = 1,
                    Description = "Type Description",
                    Code = "TS"
                },
                IsActive = true
            };

            CreateEditUserDTO dto = new CreateEditUserDTO
            {
                Id = 1,
                Name = "John",
                Surname = "Doe",
                BirthDate = System.DateTime.Now,
                EmailAddress = "test@mail.gr",
                UserTitleId = 1,
                UserTypeId = 1
            };

            user = _userService.EditUserAction(user, dto);

            Assert.AreEqual(user.Name, dto.Name);
            Assert.AreEqual(user.EmailAddress, dto.EmailAddress);
        }
    }
}