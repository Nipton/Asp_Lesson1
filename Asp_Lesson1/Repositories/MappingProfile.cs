using Asp_Lesson1.Models;
using Asp_Lesson1.Models.DTO;
using AutoMapper;

namespace Asp_Lesson1.Repositories
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<Product, ProductModel>(MemberList.Destination).ReverseMap();
            CreateMap<ProductGroup, ProductGroupModel>(MemberList.Destination).ReverseMap();
            CreateMap<Store, StoreModel>(MemberList.Destination).ReverseMap();
        }
    }
}
