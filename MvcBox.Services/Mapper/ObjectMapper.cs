using AutoMapper;
using Entities.Models.Imitator;
using Entities.ViewModels.ImitatorViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace MvcBox.Services.Mapper
{
    public class ObjectMapper
    {
        private static readonly Lazy<IMapper> Lazy = new Lazy<IMapper>(() =>
        {
            var config = new MapperConfiguration(cfg =>
            {
                // This line ensures that internal properties are also mapped over.
                cfg.ShouldMapProperty = p => p.GetMethod.IsPublic || p.GetMethod.IsAssembly;
                cfg.AddProfile<AspnetRunDtoMapper>();
            });
            var mapper = config.CreateMapper();
            return mapper;
        });
        public static IMapper Mapper => Lazy.Value;
    }

    public class AspnetRunDtoMapper : Profile
    {
        public AspnetRunDtoMapper()
        {
            CreateMap<Owner, UserModel>()
                .ReverseMap();

            CreateMap<Owner, UserShortModel>()
               .ReverseMap();

            CreateMap<Device, DeviceModel>()
              .ReverseMap();

            CreateMap<Device, DeviceShortModel>()
              .ReverseMap();


            //  CreateMap<RatingRow, RatingRowModel>()
            //.ReverseMap();

            //  CreateMap<LinkToDocument, LinkToDocumentModel>()
            //  .ReverseMap();


            //  CreateMap<SectionRow, SectionRowModel>()
            //  .ReverseMap();


            //  CreateMap<UserDailyDataRow, UserDailyDataRowModel>().ReverseMap();
        }
    }
}
