using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Mapping;
using AutoMapper;
using Domain.Common.Dto.User;
using Domain.Common.Enum;
using Domain.Common.Helper;
using Domain.Entities;
using Repository;

namespace Application.Services.Service
{
    public class UserService
    {
        IRepositories<User> _repository;
        public IMapper _mapper { get; }

        public UserService(IRepositories<User> repository, IMapper mapper)
        {
            this._repository = repository;
            this._mapper = mapper;
        }

        public async Task<ApiResponse<bool>> AddService(UserDto add, CancellationToken cancellationToken)
        {
            var user = await _repository.ExistsAsync(x => x.Email == add.Email);
            if (user)
            {
                return new ApiResponse<bool>(ResponseStatusEnum.BadRequest, false, Message.EmailExistErrorMessage);
            }

            var map = ObjectMapper.Mapper.Map<User>(add);
            await _repository.AddAsync(map, cancellationToken, true);

            return new ApiResponse<bool>(ResponseStatusEnum.Success, true, Message.SuccessfullMessage);
        }

        public async Task<ApiResponse<bool>> EditService(UserDto edit, CancellationToken cancellationToken)
        {
            var emailExist = await _repository.ExistsAsync(x => x.Email == edit.Email && x.Id != edit.Id);
            if (emailExist)
            {
                return new ApiResponse<bool>(ResponseStatusEnum.BadRequest, false, Message.EmailExistErrorMessage);
            }
            var user = ObjectMapper.Mapper.Map<User>(edit);
            await _repository.UpdateAsync(user, cancellationToken, true);

            return new ApiResponse<bool>(ResponseStatusEnum.Success, true, Message.SuccessfullMessage);
        }

        public async Task<ApiResponse<bool>> DeleteService(int userId, CancellationToken cancellationToken)
        {
            var user = await _repository.ExistsAsync(x => x.Id == userId);
            if (!user)
            {
                return new ApiResponse<bool>(ResponseStatusEnum.NotFound, false, Message.NotFoundErrorMessage);
            }
            await _repository.DeleteAsync(x => x.Id == userId, cancellationToken, true);
            return new ApiResponse<bool>(ResponseStatusEnum.Success, true, Message.SuccessfullMessage);
        }

        public async Task<ApiResponse<List<UserDto>>> GetService(CancellationToken cancellationToken)
        {
            var result = await _repository.GetAsync(cancellationToken);
            var map = ObjectMapper.Mapper.Map<List<UserDto>>(result);

            return new ApiResponse<List<UserDto>>(ResponseStatusEnum.Success, map, Message.SuccessfullMessage);
        }

        public async Task<ApiResponse<UserDto>> GetByIdService(int userId, CancellationToken cancellationToken)
        {
            var result = await _repository.GetByIdAsync(cancellationToken, userId);
            if (result == null)
            {
                return new ApiResponse<UserDto>(ResponseStatusEnum.NotFound, null, Message.NotFoundErrorMessage);
            }
            var map = ObjectMapper.Mapper.Map<UserDto>(result);

            return new ApiResponse<UserDto>(ResponseStatusEnum.Success, map, Message.SuccessfullMessage);
        }
    }
}