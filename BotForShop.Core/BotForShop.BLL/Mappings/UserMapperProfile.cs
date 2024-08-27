using AutoMapper;
using BotForShop.Core.Dtos;
using BotForShop.Core.InputModels;
using BotForShop.Core.OutputModels;


namespace BotForShop.BLL.Mappings
{
    public class UserMapperProfile : Profile
    {
        public UserMapperProfile()
        {
            CreateMap<UserInputModel, UserDto>();//ReverseMap() => с этим сможет работать в обратную сторону но formember не работает тогда
            CreateMap<UserDto, UserOutputModel>();
            //.ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));  // if name != name
        }



    }
}
