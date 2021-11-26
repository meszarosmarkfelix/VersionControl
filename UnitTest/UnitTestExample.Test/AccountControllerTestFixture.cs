using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using UnitTestExample.Controllers;

namespace UnitTestExample.Test
{
    public class AccountControllerTestFixture
    {
        [
            Test,
            TestCase("abcd",false),
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
            TestCase("abcd123",false),
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
            Assert.AreEqual(result, expectedResult);

        }
    }
}
