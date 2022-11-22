using API.Models.Entities;
using API.Models.Responses;
using AutoMapper;

namespace API.AutoMapper
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            #region Auth            
            CreateMap<User, LoginResponse>();
            #endregion            
        }
    }
}
