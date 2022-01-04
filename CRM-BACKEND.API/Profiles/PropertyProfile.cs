using AutoMapper;
using Entities.Models.DataTableCreation;
using System.Data;

namespace CRM_BACKEND.API.AutoMapperConfigure
{
    public class PropertyProfile : Profile
    {
        public PropertyProfile()
        {
            CreateMap<DataColumn, Property>()
                .ForMember(dest =>
                    dest.FieldName, opt => opt.MapFrom(src => src.ColumnName))
                .ForMember(dest =>
                    dest.IsNullable, opt => opt.MapFrom(src => src.AllowDBNull))
                .ForMember(dest =>
                    dest.Size, opt => opt.MapFrom(src => src.MaxLength))
                .ForMember(dest => dest.DataType, opt => opt.Ignore())
                .ForMember(dest => dest.Constraint, opt => opt.Ignore());
        }
    }
}
