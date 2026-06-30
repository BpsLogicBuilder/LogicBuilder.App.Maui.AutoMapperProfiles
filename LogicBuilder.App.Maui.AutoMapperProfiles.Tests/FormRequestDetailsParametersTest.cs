using AutoMapper;
using LogicBuilder.App.Maui.Forms.Configuration;
using LogicBuilder.App.Maui.Forms.Parameters;
using LogicBuilder.EntityFrameworkCore.Mapping;
using LogicBuilder.Forms.Parameters.Expansions;
using LogicBuilder.Forms.Parameters.Expressions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Abstractions;
using System.Diagnostics.CodeAnalysis;

namespace LogicBuilder.App.Maui.AutoMapperProfiles.Tests
{
    public class FormRequestDetailsParametersTest
    {
        static FormRequestDetailsParametersTest()
        {
            Initialize();
        }

        private static MapperConfiguration MapperConfiguration;
        private static IServiceProvider serviceProvider;

        [Fact]
        public void ConstructorShouldInitializeAllProperties()
        {
            // Arrange
            string getUrl = "api/Entity/GetEntity";
            string addUrl = "api/Student/Save";
            string updateUrl = "api/Student/Save";
            string deleteUrl = "api/Student/Delete";
            Type modelType = typeof(string);
            Type dataType = typeof(int);
            var filter = new FilterLambdaOperatorParameters(null!, null!, null!);
            var selectExpandDefinition = new SelectExpandDefinitionParameters([], null!);
            IMapper mapper = serviceProvider.GetRequiredService<IMapper>();

            // Act
            var parameters = new FormRequestDetailsParameters(
                getUrl: getUrl,
                addUrl: addUrl,
                updateUrl: updateUrl,
                deleteUrl: deleteUrl,
                modelType: modelType,
                dataType: dataType,
                filter: filter,
                selectExpandDefinition: selectExpandDefinition
            );
            var descriptor = mapper.Map<FormRequestDetailsDescriptor>(parameters);

            // Assert
            Assert.Equal(getUrl, descriptor.GetUrl);
            Assert.Equal(addUrl, descriptor.AddUrl);
            Assert.Equal(updateUrl, descriptor.UpdateUrl);
            Assert.Equal(deleteUrl, descriptor.DeleteUrl);
            Assert.Equal(modelType.AssemblyQualifiedName, descriptor.ModelType);
            Assert.Equal(dataType.AssemblyQualifiedName, descriptor.DataType);
            Assert.Equal(filter.ParameterName, descriptor.Filter!.ParameterName);
            Assert.Equal(selectExpandDefinition.Selects.Count, descriptor.SelectExpandDefinition!.Selects.Count);
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
