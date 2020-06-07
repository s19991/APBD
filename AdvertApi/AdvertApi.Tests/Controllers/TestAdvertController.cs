using NUnit.Framework;
using AdvertApi.Controllers;
using AdvertApi.DTO.Requests;
using Microsoft.AspNetCore.Mvc;

namespace AdvertApi.Tests.Controllers
{
    [TestFixture]
    public class TestAdvertController
    {
        private AdvertController _advertController;
            
        [SetUp]
        public void Setup()
        {
            _advertController = new AdvertController();
        }

        [Test]
        public void TestUserRegistered()
        {
            // todo https://stackoverflow.com/questions/41292919/unit-testing-controller-methods-which-return-iactionresult
            var request = new RegisterUserRequest
            {
                
            };
            var result = _advertController.RegisterUser(request) as ObjectResult;
            Assert.IsNotNull(result);
            Assert.True(result is OkObjectResult);
        }
    }
}