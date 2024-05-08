using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Common.Dto.User;
using Test.Helper;

namespace Test.UserServiceTest.TestCase
{
    public class UserTestCase
    {
        public UserDto Add_User_With_DublicateEmail_Request()
        {
            return new UserDto()
            {
                Email = "user2@example.com",
                Name = "test user Add"
            };
        }

        public UserDto Add_User_With_Correct_Request()
        {
            return new UserDto()
            {
                Email = StringHelper.RandomStringWithGuid(5) + "@gmail.com",
                Name = "test2 user Add"
            };
        }

        public UserDto Edit_User_Without_UserId_Request()
        {
            return new UserDto()
            {
                Id = 0,
                Email = StringHelper.RandomStringWithGuid(5) + "@gmail.com",
                Name = "test3 user Add"
            };
        }

        public UserDto Edit_User_With_Correct_Request()
        {
            return new UserDto()
            {
                Id = 1,
                Email = StringHelper.RandomStringWithGuid(5) + "@gmail.com",
                Name = "test1 test edit"
            };
        }
    }
}