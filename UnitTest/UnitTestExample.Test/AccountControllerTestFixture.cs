using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using UnitTestExample.Abstractions;
using UnitTestExample.Controllers;
using UnitTestExample.Entities;

namespace UnitTestExample.Test
{
    public class AccountControllerTestFixture
    {
        [
            Test,
            TestCase("abcd", false),
            TestCase("abcd@xyz.com", true),
            TestCase("irf@uni-corvinus", false),
            TestCase("irf.uni-corvinus.hu", false),
            TestCase("irf@uni-corvinus.hu", true)
        ]
        public void TestValidateEmail(string email, bool expectedResult)
        {
            // Arrange
            var accountController = new AccountController();

            // Act
            var result = accountController.ValidateEmail(email);

            // Assert
            Assert.AreEqual(result, expectedResult);

        }
        [
            Test,
            TestCase("abcd123", false),
            TestCase("ABCG123", false),
            TestCase("abcdABCD", false),
            TestCase("abCD12", false),
            TestCase("aBcd123", true),
            TestCase("abcdEFG123", true)
        ]
        public void TestValidatePassword(string password, bool expectedResult)
        {
            // Arrange
            var accountController = new AccountController();

            // Act
            var result = accountController.ValidatePassword(password);

            // Assert
            Assert.AreEqual(expectedResult, result);

        }
        [
            Test,
            TestCase("irf@uni-corvinus.hu", "Abcd1234"),
            TestCase("irf@uni-corvinus.hu", "Abcd1234567")

        ]
        public void TestRegisterHappyPath(string email, string password)
        {
            // Arrange
            var accountManagerMock = new Mock <IAccountManager>(MockBehavior.Strict);
            accountManagerMock
                .Setup(m.CreateAccount(It.IsAny<Account>()))
                .Returns.<Account> (a => a);
            var accountController = new AccountController();
            accountController.AccountManager = accountServiceMock.Object;
            // Act
            var result = accountController.Register(email, password);
            accountController.AccountManager.Accounts.Contains(result);

            // Assert
            Assert.AreEqual(email, result.Email);
            Assert.AreEqual(password, result.Password);
            Assert.AreNotEqual(Guid.Empty, result.ID);
            accountServiceMock.Verify(m => m.CreateAccount(actualResult), Times.Once);


        }

        [   Test,
            TestCase("irf@uni-corvinus.hu", "Abcd1234"),
            TestCase("irf@uni-corvinus.hu", "Abcd1234567"),
            TestCase("abcd123", false),
            TestCase("ABCG123", false),
            TestCase("abcdABCD", false),
            TestCase("abCD12", false),
            TestCase("aBcd123", true),
            TestCase("abcdEFG123", true)
        ]

        public void TestRegisterValidateException(string email, string password)
        {
            // Arrange
            var accountController = new AccountController();

            // Act


            // Assert
            try
            {
                accountController.Register(email, password);
                Assert.Fail();
            }
            catch (Exception ex)
            {

                Assert.IsInstanceOf<ValidationException>(ex);
            }


        }
    }
}
