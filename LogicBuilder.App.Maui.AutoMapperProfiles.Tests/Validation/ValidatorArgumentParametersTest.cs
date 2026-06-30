using AutoMapper;
using LogicBuilder.App.Maui.Forms.Configuration.Validation;
using LogicBuilder.App.Maui.Forms.Parameters.Validation;
using LogicBuilder.EntityFrameworkCore.Mapping;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Abstractions;
using System.Diagnostics.CodeAnalysis;

namespace LogicBuilder.App.Maui.AutoMapperProfiles.Tests.Validation
{
    public class ValidatorArgumentParametersTest
    {
        static ValidatorArgumentParametersTest()
        {
            Initialize();
        }

        private static MapperConfiguration MapperConfiguration;
        private static IServiceProvider serviceProvider;

        [Fact]
        public void ConstructorShouldInitializeAllProperties()
        {
            // Arrange
            string name = "min";
            object value = 5;
            Type type = typeof(int);
            IMapper mapper = serviceProvider.GetRequiredService<IMapper>();

            // Act
            var parameters = new ValidatorArgumentParameters(
                name: name,
                value: value,
                type: type
            );
            var descriptor = mapper.Map<ValidatorArgumentDescriptor>(parameters);

            // Assert
            Assert.Equal(name, descriptor.Name);
            Assert.Equal(value, descriptor.Value);
            Assert.Equal(type.AssemblyQualifiedName, descriptor.Type);
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
