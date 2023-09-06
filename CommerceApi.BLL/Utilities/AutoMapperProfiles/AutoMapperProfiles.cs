using AutoMapper;
using CommerceApi.DAL.Entities;
using CommerceApi.DTO.DTOS;

namespace CommerceApi.BLL.Utilities
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Cart,  CartDto>();
            CreateMap<CartProduct, CartProductDto>();
            CreateMap<Product, ProductDto>();
            CreateMap<ProductReview, ProductReviewDto>();
            CreateMap<Review, ReviewDto>();
            CreateMap<User, UserDto>();
            CreateMap<WishList, WishListDto>();
            CreateMap<WishListProduct, WishListProductDto>();
        }

    }
}
