using AutoMapper;
using LogicBuilder.App.Maui.Forms.Configuration.TextForm;
using LogicBuilder.App.Maui.Forms.Parameters.TextForm;
using LogicBuilder.EntityFrameworkCore.Mapping;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Abstractions;
using System.Diagnostics.CodeAnalysis;

namespace LogicBuilder.App.Maui.AutoMapperProfiles.Tests.TextForm
{
    public class TextFormSettingsParametersTest
    {
        static TextFormSettingsParametersTest()
        {
            Initialize();
        }

        private static MapperConfiguration MapperConfiguration;
        private static IServiceProvider serviceProvider;

        [Fact]
        public void ConstructorShouldInitializeAllProperties()
        {
            // Arrange
            string title = "Title";
            var textGroups = new List<TextGroupParameters>
            {
                new("Group 1", [])
            };
            IMapper mapper = serviceProvider.GetRequiredService<IMapper>();

            // Act
            var parameters = new TextFormSettingsParameters(
                title: title,
                textGroups: textGroups
            );
            var descriptor = mapper.Map<TextFormSettingsDescriptor>(parameters);

            // Assert
            Assert.Equal(title, descriptor.Title);
            Assert.Equal(textGroups[0].Title, descriptor.TextGroups[0].Title);
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
