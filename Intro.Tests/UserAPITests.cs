using Intro.Models.DTO;
using Intro.Models.Model;
using Intro.WebApi.Repositories;
using Intro.WebApi.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Intro.Tests
{
    [TestClass]
    public class UserAPITests
    {
        public UserServices _userService;
        public Mock<IRepository<UserTitle>> _userTitleRepository;
        public Mock<IRepository<UserType>> _userTypeRepository;

        /// <summary>
        /// Setups Test instance.
        /// </summary>
        [TestInitialize]
        public void Setup()
        {
            _userTitleRepository = new Mock<IRepository<UserTitle>>();
            _userTypeRepository = new Mock<IRepository<UserType>>();
            _userService = new UserServices(_userTitleRepository.Object, _userTypeRepository.Object);
        }

        /// <summary>
        /// Test Delete user method from service.
        /// Delete method must change isActive value to false.
        /// </summary>
        [TestMethod]
        public void AssertUserIsDeleted_Success()
        {
            #region Create User Entity
            User user = new User
            {
                Id = 1,
                Name = "John",
                Surname = "Doe",
                BirthDate = System.DateTime.Now,
                EmailAddress = "test@test.gr",
                UserTitle = new UserTitle
                {
                    Id=1,
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
            #endregion

            user = _userService.DeleteUser(user);

            Assert.AreEqual(user.IsActive, false);
        }

        /// <summary>
        /// Tests Create user entity method from service.
        /// 
        /// </summary>
        [TestMethod]
        public void TestAddUser_AssertSuccess()
        {
            UserDTO dto = new UserDTO
            {
                Name = "John",
                Surname = "Doe",
                BirthDate = System.DateTime.Now,
                EmailAddress = "test@mail.gr",
                UserTitleDescription = "Title Description",
                UserTypeDescription = "Type Description",
                UserTypeCode = "TS"
            };

            User user = _userService.CreateUserEntity(dto);

            Assert.AreEqual(dto.Name,user.Name);
            Assert.AreEqual(dto.UserTitleDescription, user.UserTitle.Description);
            Assert.AreEqual(user.IsActive,true);
        }

        /// <summary>
        /// Tests edit user entity method from service.
        /// </summary>
        [TestMethod]
        public void TestEditUser_assertSuccess()
        {
            #region Create User Entity and DTO
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

            UserDTO dto = new UserDTO
            {
                Id = 1,
                Name = "John",
                Surname = "Doe",
                BirthDate = System.DateTime.Now,
                EmailAddress = "test@mail.gr",
                UserTitleDescription = "Title Description",
                UserTypeDescription = "Type Description",
                UserTypeCode = "TS"
            };
            #endregion

            user = _userService.EditUserAction(user, dto);

            Assert.AreEqual(user.Name, dto.Name);
            Assert.AreEqual(user.EmailAddress, dto.EmailAddress);
        }

    }
}
