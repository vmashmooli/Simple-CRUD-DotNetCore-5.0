using System;
using AutoMapper;
using Domain.Common.Dto.User;
using Domain.Entities;

namespace Application.Mapping
{
    public static class ObjectMapper
    {
        private static readonly Lazy<IMapper> Lazy = new Lazy<IMapper>(() =>
        {
            var config = new MapperConfiguration(cfg =>
              {
                  cfg.ShouldMapProperty = p => p.GetMethod.IsPublic || p.GetMethod.IsAssembly;
                  cfg.AddProfile<AutoMappperProfile>();
              });
            var mapper = config.CreateMapper();
            return mapper;
        });
        public static IMapper Mapper => Lazy.Value;
    }

    public class AutoMappperProfile : Profile
    {
        public AutoMappperProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
        }
    }
}