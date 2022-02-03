using AutoMapper;
using QueueTriggerDI.Context.DTO;
using QueueTriggerDI.Context.Entities;

namespace QueueTriggerDI.Context.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BookDto, Book>()
                .ForMember(dst => dst.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dst => dst.Author, opt => opt.MapFrom(src => src.Author))
                .ReverseMap();

            CreateMap<BookDto, BookContent>()
                .ForMember(dst => dst.BookId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dst => dst.Content, opt => opt.MapFrom(src => src.Content))
                .ReverseMap();
        }
    }
}
