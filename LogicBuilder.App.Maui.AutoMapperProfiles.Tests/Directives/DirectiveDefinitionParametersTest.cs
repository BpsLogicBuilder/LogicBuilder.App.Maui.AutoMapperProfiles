using AutoMapper;
using LogicBuilder.App.Maui.Forms.Configuration.Directives;
using LogicBuilder.App.Maui.Forms.Parameters.Directives;
using LogicBuilder.EntityFrameworkCore.Mapping;
using LogicBuilder.Expressions.Utils.ExpressionDescriptors;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Abstractions;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace LogicBuilder.App.Maui.AutoMapperProfiles.Tests.Directives
{
    public class DirectiveDefinitionParametersTest
    {
        static DirectiveDefinitionParametersTest()
        {
            Initialize();
        }

        private static MapperConfiguration MapperConfiguration;
        private static IServiceProvider serviceProvider;

        [Fact]
        public void ConstructorShouldInitializeAllProperties()
        {
            // Arrange
            string className = "HideIf";
            string functionName = "Check";
            var arguments = new List<DirectiveArgumentParameters>
            {
                new("arg1", "value1", typeof(string))
            };
            IMapper mapper = serviceProvider.GetRequiredService<IMapper>();

            // Act
            var parameters = new DirectiveDefinitionParameters(
                className: className,
                functionName: functionName,
                arguments: arguments
            );
            var descriptor = mapper.Map<DirectiveDefinitionDescriptor>(parameters);

            // Assert
            Assert.Equal(className, descriptor.ClassName);
            Assert.Equal(functionName, descriptor.FunctionName);
            Assert.Single(descriptor.Arguments!);
            Assert.Equal("arg1", descriptor.Arguments!["arg1"].Name);
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
