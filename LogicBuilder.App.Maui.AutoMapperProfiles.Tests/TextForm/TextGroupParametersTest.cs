using AutoMapper;
using LogicBuilder.App.Maui.Forms.Configuration.TextForm;
using LogicBuilder.App.Maui.Forms.Parameters.TextForm;
using LogicBuilder.EntityFrameworkCore.Mapping;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Abstractions;
using System.Diagnostics.CodeAnalysis;

namespace LogicBuilder.App.Maui.AutoMapperProfiles.Tests.TextForm
{
    public class TextGroupParametersTest
    {
        static TextGroupParametersTest()
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
            var labels = new List<ILabelItemParameters>
            {
                new LabelItemParameters("Label text")
            };
            IMapper mapper = serviceProvider.GetRequiredService<IMapper>();

            // Act
            var parameters = new TextGroupParameters(
                title: title,
                labels: labels
            );
            var descriptor = mapper.Map<TextGroupDescriptor>(parameters);

            // Assert
            Assert.Equal(title, descriptor.Title);
            Assert.Equal(((LabelItemParameters)labels[0]).Text, ((LabelItemDescriptor)descriptor.Labels[0]).Text);
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
