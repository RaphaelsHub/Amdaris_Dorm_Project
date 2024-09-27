using AutoMapper;
using Dorm.DAL.Interfaces;
using Dorm.Domain.DTO;
using Dorm.Domain.Entities.UserEF;
using Dorm.Domain.Responces;
using Dorm.Server.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dorm.BLL.Services
{
    public class StudentProfileService : IStudentProfileService
    {
        private readonly IUsersRepository<UserEF> _usersRepository;
        private readonly IMapper _mapper;

        public StudentProfileService(IUsersRepository<UserEF> usersRepository, IMapper mapper)
        {
            _usersRepository = usersRepository;
            _mapper = mapper;
        }

        public async Task<TestsResponse<UserProfileDto>> Create(UserProfileDto userDto)
        {
            try
            {
                var entity = await _usersRepository.GetById(userDto.UserId);

                if (entity == null)
                {
                    return new TestsResponse<UserProfileDto>(null, "User not found");
                }

                entity = _mapper.Map(userDto,entity);

                var createdEntity = await _usersRepository.Update(entity); 

                if (createdEntity != null)
                {
                    userDto = _mapper.Map<UserProfileDto>(createdEntity);
                    return new TestsResponse<UserProfileDto>(userDto, "Profile created successfully");
                }

                return new TestsResponse<UserProfileDto>(null, "Error creating profile");
            }
            catch (Exception ex)
            {
                return new TestsResponse<UserProfileDto>(null, $"Exception: {ex.Message}");
            }
        }

        public async Task<TestsResponse<bool>> Delete(int id)
        {
            var entity = await _usersRepository.GetById(id);

            if (entity != null)
            {
                await _usersRepository.Delete(entity);
                return new TestsResponse<bool>(true, "Profile deleted successfully");
            }

            return new TestsResponse<bool>(false, "Profile does not exist");
        }

        public async Task<TestsResponse<UserProfileDto>> Edit(int id, UserProfileDto userDto)
        {
            try
            {
                var entity = await _usersRepository.GetById(id);

                if (entity == null)
                {
                    return new TestsResponse<UserProfileDto>(null, "Profile not found");
                }

                entity = _mapper.Map(userDto, entity);
                await _usersRepository.Update(entity);

                var updatedUserDto = _mapper.Map<UserProfileDto>(entity);
                return new TestsResponse<UserProfileDto>(updatedUserDto, "Profile updated successfully");
            }
            catch (Exception ex)
            {
                return new TestsResponse<UserProfileDto>(null, $"Error updating profile: {ex.Message}");
            }
        }

        public async Task<TestsResponse<UserProfileDto>> GetById(int id)
        {
            var entity = await _usersRepository.GetById(id);

            if (entity != null)
            {
                var userDto = _mapper.Map<UserProfileDto>(entity);
                return new TestsResponse<UserProfileDto>(userDto, "Profile found");
            }

            return new TestsResponse<UserProfileDto>(null, "Profile not found");
        }

        public async Task<TestsResponse<IEnumerable<UserProfileDto>>> GetAll()
        {
            try
            {
                var entities = await _usersRepository.GetAll();

                if (entities != null && entities.Any())
                {
                    var userDtos = _mapper.Map<IEnumerable<UserProfileDto>>(entities);
                    return new TestsResponse<IEnumerable<UserProfileDto>>(userDtos, "Success");
                }

                return new TestsResponse<IEnumerable<UserProfileDto>>(null, "No profiles");
            }
            catch (Exception ex)
            {
                return new TestsResponse<IEnumerable<UserProfileDto>>(null, $"{ex.Message}");
            }
        }
    }
}
