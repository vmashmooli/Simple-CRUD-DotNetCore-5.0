using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Services.Service;
using AutoMapper;
using Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Repository;
using Shouldly;
using Test.UserServiceTest.TestCase;
using Xunit;

[assembly: CollectionBehavior(DisableTestParallelization = true)]
namespace Test.UserServiceTest
{

    public class UserServiceTest
    {
        UserService _service;
        Mock<IRepositories<User>> _repository;
        CancellationTokenSource _source;
        Mock<IMapper> _mapper;

        public UserServiceTest()
        {
            this._repository = new Mock<IRepositories<User>>();
            this._mapper = new Mock<IMapper>();
            this._service = new UserService(_repository.Object, _mapper.Object);
            this._source = new CancellationTokenSource();
        }

        [Fact]
        public async Task AddUser_WithDublicateEmail_Test()
        {
            // Arrange
            var request = (new UserTestCase()).Add_User_With_DublicateEmail_Request();
            var existResult = true;
            _repository.Setup(x => x.ExistsAsync(x => x.Email == request.Email)).ReturnsAsync(() => existResult);
            // Act
            var result = await _service.AddService(request, _source.Token);
            // Assert
            result.Result.ShouldBe(false);
        }

        [Fact]
        public async Task AddUser_CorrectRequest_Test()
        {
            // Arrange
            var request = (new UserTestCase()).Add_User_With_Correct_Request();
            var existResult = false;
            var user = new User();
            _repository.Setup(x => x.ExistsAsync(x => x.Email == request.Email)).ReturnsAsync(() => existResult);
            _repository.Setup(x => x.AddAsync(user, _source.Token, true));
            // Act
            var result = await _service.AddService(request, _source.Token);
            // Assert
            result.Result.ShouldBe(true);
        }

        [Fact]
        public async Task EditUser_Without_UserId_Test()
        {
            // Arrange
            var request = (new UserTestCase()).Edit_User_Without_UserId_Request();
            var existResult = true;
            _repository.Setup(x => x.FindAsync(x => x.Id == request.Id, _source.Token)).ReturnsAsync(() => null);
            _repository.Setup(x => x.ExistsAsync(x => x.Email == request.Email && x.Id != request.Id)).ReturnsAsync(() => existResult);
            // Act
            var result = await _service.EditService(request, _source.Token);
            // Assert
            result.Result.ShouldBe(false);
        }

        [Fact]
        public async Task EditUser_CorrectRequest_Test()
        {
            // Arrange
            var request = (new UserTestCase()).Edit_User_With_Correct_Request();
            var existResult = false;
            var user = new User();
            _repository.Setup(x => x.FindAsync(x => x.Id == request.Id, _source.Token)).ReturnsAsync(() => user);
            _repository.Setup(x => x.ExistsAsync(x => x.Email == request.Email && x.Id != request.Id)).ReturnsAsync(() => existResult);
            _repository.Setup(x => x.UpdateAsync(user, _source.Token, true));
            // Act
            var result = await _service.EditService(request, _source.Token);
            // Assert
            result.Result.ShouldBe(true);
        }

        [Fact]
        public async Task DeleteUser_CorrectRequest_Test()
        {
            // Arrange
            var existResult = true;
            var userId = 1;
            _repository.Setup(x => x.ExistsAsync(x => x.Id == userId)).ReturnsAsync(() => existResult);
            _repository.Setup(x => x.DeleteAsync(x => x.Id == userId, _source.Token, true));
            // Act
            var result = await _service.DeleteService(userId, _source.Token);
            // Assert
            result.Result.ShouldBe(true);
        }

        [Fact]
        public async Task DeleteUser_NotFoundRequest_Test()
        {
            // Arrange
            var existResult = false;
            var userId = 1;
            _repository.Setup(x => x.ExistsAsync(x => x.Id == userId)).ReturnsAsync(() => existResult);
            _repository.Setup(x => x.DeleteAsync(x => x.Id == userId, _source.Token, true));
            // Act
            var result = await _service.DeleteService(userId, _source.Token);
            // Assert
            result.Result.ShouldBe(false);
        }

        [Fact]
        public async Task GetUserById_NotFoundRequest_Test()
        {
            // Arrange
            var userId = 1;
            _repository.Setup(x => x.GetByIdAsync(_source.Token, userId)).ReturnsAsync(() => null);
            // Act
            var result = await _service.GetByIdService(userId, _source.Token);
            // Assert
            result.Result.ShouldBe(null);
        }

        [Fact]
        public async Task GetUserById_CorrectRequest_Test()
        {
            // Arrange
            var userId = 1;
            var user = new User();
            _repository.Setup(x => x.GetByIdAsync(_source.Token, userId)).ReturnsAsync(() => user);
            // Act
            var result = await _service.GetByIdService(userId, _source.Token);
            // Assert
            result.Result.ShouldNotBeNull();
        }
    }
}
