using AutoMapper;
using Dorm.BLL.Interfaces;
using Dorm.DAL.Interfaces;
using Dorm.Domain.DTO;
using Dorm.Domain.Entities.UserEF;
using Dorm.Domain.Responces;

namespace Dorm.BLL.Services
{
    public class StudentProfileService(IUsersRepository<UserEF> usersRepository, IMapper mapper)
        : IStudentProfileService
    {
        public async Task<BaseResponse<UserProfileDto>> GetById(int id)
        {
            var entity = await usersRepository.GetById(id);

            if (entity != null)
            {
                var userDto = mapper.Map<UserProfileDto>(entity);
                return new BaseResponse<UserProfileDto>(userDto, "Profile found");
            }

            return new BaseResponse<UserProfileDto>(null, "Profile not found");
        }

        public async Task<BaseResponse<IEnumerable<UserProfileDto>>> GetAll()
        {
            try
            {
                var entities = await usersRepository.GetAll();

                if (entities.Any())
                {
                    var users = mapper.Map<IEnumerable<UserProfileDto>>(entities);
                    return new BaseResponse<IEnumerable<UserProfileDto>>(users, "Success");
                }

                return new BaseResponse<IEnumerable<UserProfileDto>>(null, "No profiles");
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<UserProfileDto>>(null, $"{ex.Message}");
            }
        }
        
        public async Task<BaseResponse<UserProfileDto>> Edit(int id, UserProfileDto userDto)
        {
            try
            {
                var entity = await usersRepository.GetById(id);

                if (entity == null)
                    return new BaseResponse<UserProfileDto>(null, "Profile not found");

                entity = mapper.Map(userDto, entity);
                await usersRepository.Update(entity);
                

                var updatedUserDto = mapper.Map<UserProfileDto>(entity);
                
                return new BaseResponse<UserProfileDto>(
                    updatedUserDto  ?? throw new ArgumentNullException($"Why updateUserDto is null"), 
                    "Profile updated successfully");
            }
            catch (Exception ex)
            {
                return new BaseResponse<UserProfileDto>(null, $"Error updating profile: {ex.Message}");
            }
        }
        
        public async Task<BaseResponse<bool>> Delete(int id)
        {
            var entity = await usersRepository.GetById(id);

            if (entity != null)
            {
                return await usersRepository.Delete(entity) 
                    ? new BaseResponse<bool>(true, "Profile deleted successfully")
                    : new BaseResponse<bool>(false, "Profile was not deleted");
            }

            return new BaseResponse<bool>(false, "Profile does not exist with this id");
        }

    }
}
