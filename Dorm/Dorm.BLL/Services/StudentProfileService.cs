using AutoMapper;
using Dorm.BLL.Interfaces;
using Dorm.DAL.Interfaces;
using Dorm.Domain.DTO;
using Dorm.Domain.Entities.UserEF;
using Dorm.Domain.Responces;

namespace Dorm.BLL.Services
{
    /// <summary>
    /// Service for handling student profile operations.
    /// </summary>
    public class StudentProfileService : IStudentProfileService
    {
        private readonly IUsersRepository<UserEF> _usersRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="StudentProfileService"/> class.
        /// </summary>
        /// <param name="usersRepository">The user repository.</param>
        /// <param name="mapper">The mapper.</param>
        public StudentProfileService(IUsersRepository<UserEF> usersRepository, IMapper mapper)
        {
            _usersRepository = usersRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets a user profile by ID.
        /// </summary>
        /// <param name="id">The user ID.</param>
        /// <returns>A response containing the user profile DTO.</returns>
        public async Task<BaseResponse<UserProfileDto>> GetById(int id)
        {
            var entity = await _usersRepository.GetById(id);

            if (entity != null)
            {
                var userDto = _mapper.Map<UserProfileDto>(entity);
                return new BaseResponse<UserProfileDto>(userDto, "Profile found");
            }

            return new BaseResponse<UserProfileDto>(null, "Profile not found");
        }

        /// <summary>
        /// Gets all user profiles.
        /// </summary>
        /// <returns>A response containing a list of user profile DTOs.</returns>
        public async Task<BaseResponse<IEnumerable<UserProfileDto>>> GetAll()
        {
            try
            {
                var entities = await _usersRepository.GetAll();

                if (entities.Any())
                {
                    var users = _mapper.Map<IEnumerable<UserProfileDto>>(entities);
                    return new BaseResponse<IEnumerable<UserProfileDto>>(users, "Success");
                }

                return new BaseResponse<IEnumerable<UserProfileDto>>(null, "No profiles");
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<UserProfileDto>>(null, $"{ex.Message}");
            }
        }

        /// <summary>
        /// Edits a user profile.
        /// </summary>
        /// <param name="id">The user ID.</param>
        /// <param name="userDto">The user profile DTO.</param>
        /// <returns>A response containing the updated user profile DTO.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the updated user profile DTO is null.</exception>
        public async Task<BaseResponse<UserProfileDto>> Edit(int id, UserProfileDto userDto)
        {
            try
            {
                var entity = await _usersRepository.GetById(id);

                if (entity == null)
                    return new BaseResponse<UserProfileDto>(
                        null,
                        "Profile not found"
                    );

                entity = _mapper.Map(userDto, entity);
                await _usersRepository.Update(entity);

                var updatedUserDto = _mapper.Map<UserProfileDto>(entity);

                return new BaseResponse<UserProfileDto>(
                    updatedUserDto ?? throw new ArgumentNullException(
                        $"UpdateUserDto is null"),
                    "Profile updated successfully"
                );
            }
            catch (Exception ex)
            {
                return new BaseResponse<UserProfileDto>(null, $"Error updating profile: {ex.Message}");
            }
        }

        /// <summary>
        /// Deletes a user profile.
        /// </summary>
        /// <param name="id">The user ID.</param>
        /// <returns>A response indicating whether the profile was deleted successfully.</returns>
        public async Task<BaseResponse<bool>> Delete(int id)
        {
            var entity = await _usersRepository.GetById(id);

            if (entity != null)
            {
                return await _usersRepository.Delete(entity) 
                    ? new BaseResponse<bool>(true, "Profile deleted successfully")
                    : new BaseResponse<bool>(false, "Profile was not deleted");
            }

            return new BaseResponse<bool>(false, "Profile does not exist with this id");
        }
    }
}