﻿using NUnit.Framework;
using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTestExample.Controllers;

namespace UnitTestExample.Test
{
    public class AccountControllerTestFixture
    {
        [
            Test,
            TestCase("abcd", "false"),
            TestCase("abcd@gmail.com", "true")
            
            
        ]
        public void TestValidateEmail(string email, bool expectedResult)
        {
            //Arrange
            var accountController = new AccountController();

            //Act
            var actualResult = accountController.ValidateEmail(email);

            //Assert
            Assert.AreEqual(expectedResult, actualResult);

        }

        [
            Test,
            TestCase("abcd1234", "false"),
            TestCase("Abcd", "false"),
            TestCase("Abcd1234", "true")
        ]
        public void TestValidatePassword(string password, bool expectedResult)
        {
            //Arrange
            var accountController = new AccountController();

            //Act
            var actualResult = accountController.ValidatePassword(password);

            //Assert
            Assert.AreEqual(expectedResult, actualResult);

        }

        public void TestRegisterHappyPath(string email, string password)
        {
            // Arrange
            var accountServiceMock = new Mock<IAccountManager>(MockBehavior.Strict);
            accountServiceMock
                .Setup(m => m.CreateAccount(It.IsAny<Account>()))
                .Returns<Account>(a => a);
            var accountController = new AccountController();
            accountController.AccountManager = accountServiceMock.Object;

            // Act
            var actualResult = accountController.Register(email, password);

            // Assert
            Assert.AreEqual(email, actualResult.Email);
            Assert.AreEqual(password, actualResult.Password);
            Assert.AreNotEqual(Guid.Empty, actualResult.ID);
            accountServiceMock.Verify(m => m.CreateAccount(actualResult), Times.Once);
        }


        [
            Test,
            TestCase("irf@uni-corvinus", "Abcd1234"),
            TestCase("irf.uni-corvinus.hu", "Abcd1234"),
            TestCase("irf@uni-corvinus.hu", "abcd1234"),
            TestCase("irf@uni-corvinus.hu", "ABCD1234"),
            TestCase("irf@uni-corvinus.hu", "abcdABCD"),
            TestCase("irf@uni-corvinus.hu", "Ab1234"),
        ]
        public void TestRegisterValidateException(string email, string password)
        {
            // Arrange
            var accountController = new AccountController();

            // Act
            try
            {
                var actualResult = accountController.Register(email, password);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOf<ValidationException>(ex);
            }

            // Assert
        }
    }
}
