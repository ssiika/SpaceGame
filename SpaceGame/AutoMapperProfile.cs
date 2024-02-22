namespace SpaceGame
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() 
        { 
            CreateMap<User, GetUserDto>();
            CreateMap<AddUserDto, User>();
        }  
    }
}
