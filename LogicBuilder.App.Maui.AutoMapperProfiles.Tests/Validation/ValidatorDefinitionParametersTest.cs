using AutoMapper;
using LogicBuilder.App.Maui.Forms.Configuration.Validation;
using LogicBuilder.App.Maui.Forms.Parameters.Validation;
using LogicBuilder.EntityFrameworkCore.Mapping;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Abstractions;
using System.Diagnostics.CodeAnalysis;

namespace LogicBuilder.App.Maui.AutoMapperProfiles.Tests.Validation
{
    public class ValidatorDefinitionParametersTest
    {
        static ValidatorDefinitionParametersTest()
        {
            Initialize();
        }

        private static MapperConfiguration MapperConfiguration;
        private static IServiceProvider serviceProvider;

        [Fact]
        public void ConstructorShouldInitializeAllProperties()
        {
            // Arrange
            string className = "RequiredRule";
            string functionName = "Check";
            var arguments = new List<ValidatorArgumentParameters>
            {
                new("min", 5, typeof(int))
            };
            IMapper mapper = serviceProvider.GetRequiredService<IMapper>();

            // Act
            var parameters = new ValidatorDefinitionParameters(
                className: className,
                functionName: functionName,
                arguments: arguments
            );
            var descriptor = mapper.Map<ValidatorDefinitionDescriptor>(parameters);

            // Assert
            Assert.Equal(className, descriptor.ClassName);
            Assert.Equal(functionName, descriptor.FunctionName);
            Assert.Single(descriptor.Arguments!);
            Assert.Equal("min", descriptor.Arguments!["min"].Name);
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
