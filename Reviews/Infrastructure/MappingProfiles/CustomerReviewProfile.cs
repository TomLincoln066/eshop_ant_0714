using ApplicationCore.Entities;
using ApplicationCore.Models;
using AutoMapper;

namespace Infrastructure.MappingProfiles;

public class CustomerReviewProfile: Profile
{
    public CustomerReviewProfile()
    {
        CreateMap<CustomerReview, CustomerReviewResponseModel>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => (ReviewStatus)src.Status));

        CreateMap<CustomerReviewRequestModel, CustomerReview>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => ReviewStatus.Pending));
        
    }
}