using AutoMapper;
using LogicBuilder.App.Maui.Forms.Configuration;
using LogicBuilder.App.Maui.Forms.Parameters;
using LogicBuilder.App.Maui.Forms.Parameters.Bindings;
using LogicBuilder.EntityFrameworkCore.Mapping;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Abstractions;
using System.Diagnostics.CodeAnalysis;

namespace LogicBuilder.App.Maui.AutoMapperProfiles.Tests
{
    public class FormsCollectionDisplayTemplateParametersTest
    {
        static FormsCollectionDisplayTemplateParametersTest()
        {
            Initialize();
        }

        private static MapperConfiguration MapperConfiguration;
        private static IServiceProvider serviceProvider;

        [Fact]
        public void ConstructorShouldInitializeAllProperties()
        {
            // Arrange
            string templateName = "HeaderTextDetailTemplate";
            var bindings = new List<ItemBindingParameters>
            {
                new TextItemBindingParameters("Header", "Name", "Header Text", "{0}", new TextFieldTemplateParameters("TextTemplate")),
                new TextItemBindingParameters("Text", "Description", "Description Text", "{0}", new TextFieldTemplateParameters("TextTemplate"))
            };
            IMapper mapper = serviceProvider.GetRequiredService<IMapper>();

            // Act
            var parameters = new FormsCollectionDisplayTemplateParameters(
                templateName: templateName,
                bindings: bindings
            );
            var descriptor = mapper.Map<FormsCollectionDisplayTemplateDescriptor>(parameters);

            // Assert
            Assert.Equal(templateName, descriptor.TemplateName);
            Assert.Equal(2, descriptor.Bindings.Count);
            Assert.Equal(bindings[0].Name, descriptor.Bindings["Header"].Name);
            Assert.Equal(bindings[1].Name, descriptor.Bindings["Text"].Name);
        }

        #region Helpers
        [MemberNotNull(nameof(MapperConfiguration))]
        [MemberNotNull(nameof(serviceProvider))]
        private static void Initialize()
        {
            MapperConfiguration ??= new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CommandButtonProfile>();
                cfg.AddProfile<FormsParameterToFormsDescriptorMappingProfile>();
                cfg.AddProfile<ExpansionParameterToDescriptorMappingProfile>();
                cfg.AddProfile<ExpressionParameterToDescriptorMappingProfile>();
            }, NullLoggerFactory.Instance);

            serviceProvider ??= new ServiceCollection()
                .AddSingleton<AutoMapper.IConfigurationProvider>
                (
                    MapperConfiguration
                )
                .AddTransient<IMapper>(sp => new Mapper(sp.GetRequiredService<AutoMapper.IConfigurationProvider>(), sp.GetService))
                .BuildServiceProvider();
        }
        #endregion Helpers
    }
}
