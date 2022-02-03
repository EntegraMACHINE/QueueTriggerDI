using AutoMapper;
using Azure.Data.Tables;
using QueueTriggerDI.Tables.DTO;
using QueueTriggerDI.Tables.Entities;

namespace QueueTriggerDI.Tables.Mapping
{
    public class TableMappingProfile : Profile
    {
        public TableMappingProfile()
        {
            CreateMap<BookEntityDto, BookEntity>()
                .ForMember(x => x.PartitionKey, o => o.MapFrom(s => s.Id.ToString().Substring(0, s.Id.ToString().Length / 2)))
                .ForMember(x => x.RowKey, o => o.MapFrom(s => s.Id.ToString().Substring(s.Id.ToString().Length / 2, s.Id.ToString().Length - s.Id.ToString().Length / 2)))
                .ForMember(x => x.Name, o => o.MapFrom(s => s.Name))
                .ForMember(x => x.Author, o => o.MapFrom(s => s.Author));

            CreateMap<BookEntity, BookEntityDto>()
                .ForMember(x => x.Id, o => o.MapFrom(s => s.PartitionKey + s.RowKey))
                .ForMember(x => x.Name, o => o.MapFrom(s => s.Name))
                .ForMember(x => x.Author, o => o.MapFrom(s => s.Author))
                .ForSourceMember(x => x.Timestamp, o => o.DoNotValidate())
                .ForSourceMember(x => x.ETag, o => o.DoNotValidate());
        }
    }
}
