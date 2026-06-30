using AutoMapper;
using LogicBuilder.App.Maui.Forms.Configuration;
using LogicBuilder.App.Maui.Forms.Parameters;
using LogicBuilder.Forms.Parameters;

namespace LogicBuilder.App.Maui.AutoMapperProfiles
{
    public class CommandButtonProfile : Profile
    {
        public CommandButtonProfile()
        {
            CreateMap<ConnectorParameters, CommandButtonDescriptor>()
                .ForCtorParam("buttonIcon", opts => opts.MapFrom(src => src.ConnectorData == null ? "" : ((CommandButtonParameters)src.ConnectorData).ButtonIcon))
                .ForCtorParam("command", opts => opts.MapFrom(src => src.ConnectorData == null ? "" : ((CommandButtonParameters)src.ConnectorData).Command));
            CreateMap<CommandButtonParameters, CommandButtonDescriptor>()
                .ForCtorParam("id", opt => opt.MapFrom(src => 0))
                .ForCtorParam("shortString", opt => opt.MapFrom(src => ""))
                .ForCtorParam("longString", opt => opt.MapFrom(src => ""));
        }
    }
}
