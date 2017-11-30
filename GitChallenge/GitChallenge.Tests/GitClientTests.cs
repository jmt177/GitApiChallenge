using System;
using Moq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GitChallenge.Tests
{
    [TestClass]
    public class GitClientTests
    {
        private Mock<IGitClient> _gitClient;

        [TestInitialize]
        public void Setup()
        {
            _gitClient = new Mock<IGitClient>();
        }

        [TestMethod]
        public void GetUserFollowers_HasNoFollowers()
        {
            // Arrange
            SetupFor_GetUserFollowers_HasNoFollowers();
            var expectedResult = new List<GitHubUser>();

            // Act
            var testResult = _gitClient.Object.GetUserFollowers("test");

            // Assert
            Assert.IsTrue(expectedResult.Count == testResult.Count);
        }

        private void SetupFor_GetUserFollowers_HasNoFollowers()
        {
            _gitClient.Setup(v => v.GetUserFollowers(It.IsAny<string>())).Returns(new List<GitHubUser>());
        }
    }
}
