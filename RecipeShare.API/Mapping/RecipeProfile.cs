using AutoMapper;
using RecipeShare.Application.DTOs;
using RecipeShare.Domain.Entities;

namespace RecipeShare.API.Mapping;

public class RecipeProfile : Profile
{
    public RecipeProfile() 
    {
        CreateMap<RecipeDto, Recipe>().
            ForMember(dest => dest.Id, opt => opt.Ignore()).
            ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
        CreateMap<RecipeUpdateDto, Recipe>().
            ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
        CreateMap<Recipe, RecipeDto>();


    }
}
