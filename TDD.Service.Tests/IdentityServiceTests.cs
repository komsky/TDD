using Moq;
using NUnit.Framework;
using System;
using TDD.Repository.Interfaces;
using FluentAssertions;

namespace TDD.Service.Tests
{
    [TestFixture]
    public class IdentityServiceTests
    {
        IdentityService _sut;
        Mock<IRepository> _mockinery;
        [SetUp]
        public void Setup()
        {
            _mockinery = new Mock<IRepository>();
            _mockinery.Setup(x => x.IsUsedPassword(It.IsAny<String>())).Returns(false);

            _sut = new IdentityService(_mockinery.Object);
        }

        [Test]
        public void PasswordValidateShouldNotAcceptLessThan6Characters([Values("1", "12", "123", "1234", "12345")]string password)
        {
            //act
            _sut.Should().NotBeNull();
            _sut.Should().BeOfType<IdentityService>();
            bool result = _sut.ValidatePassword(password);
            //assert
            Assert.IsFalse(result);
        }

        [Test]
        public void ValidatePasswordShouldReturnFalseForBadPasswords(
            [Values("INVALID", //missing small, digit, special
                    "invalid", //missing cap, digit, special
                    "Invalid", //digit, special
                    "Invalid1" //missing special
            )]
        string password)
        {
            bool result = _sut.ValidatePassword(password);
            Assert.IsFalse(result);
        }

        [Test]
        public void ValidatePasswordShouldReturnTrueForCorrectPasswords(
            [Values("Valid1#", //missing small, digit, special
                    "COMplex1!2@" //missing cap, digit, special
            )]
        string password)
        {
            bool result = _sut.ValidatePassword(password);
            Assert.IsTrue(result);
        }

        [Test]
        public void ValidatePasswordShouldCheckForOldPasswordClash()
        {
            
            _sut.ValidatePassword("Valid1#");
            
            //make sure repository checkPassword was called
            _mockinery.Verify(x => x.IsUsedPassword(It.IsAny<string>()));
        }

    }
}
