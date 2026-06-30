using AutoMapper;
using LogicBuilder.App.Maui.Forms.Configuration;
using LogicBuilder.App.Maui.Forms.Parameters;
using LogicBuilder.EntityFrameworkCore.Mapping;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Abstractions;
using System.Diagnostics.CodeAnalysis;

namespace LogicBuilder.App.Maui.AutoMapperProfiles.Tests
{
    public class RequestDetailsParametersTest
    {
        static RequestDetailsParametersTest()
        {
            Initialize();
        }

        private static MapperConfiguration MapperConfiguration;
        private static IServiceProvider serviceProvider;

        [Fact]
        public void ConstructorShouldInitializeAllProperties()
        {
            // Arrange
            Type modelType = typeof(string);
            Type dataType = typeof(int);
            Type modelReturnType = typeof(decimal);
            Type dataReturnType = typeof(bool);
            string dataSourceUrl = "api/List/GetList";
            IMapper mapper = serviceProvider.GetRequiredService<IMapper>();

            // Act
            var parameters = new RequestDetailsParameters(
                modelType: modelType,
                dataType: dataType,
                modelReturnType: modelReturnType,
                dataReturnType: dataReturnType,
                dataSourceUrl: dataSourceUrl
            );
            var descriptor = mapper.Map<RequestDetailsDescriptor>(parameters);

            // Assert
            Assert.Equal(modelType.AssemblyQualifiedName, descriptor.ModelType);
            Assert.Equal(dataType.AssemblyQualifiedName, descriptor.DataType);
            Assert.Equal(modelReturnType.AssemblyQualifiedName, descriptor.ModelReturnType);
            Assert.Equal(dataReturnType.AssemblyQualifiedName, descriptor.DataReturnType);
            Assert.Equal(dataSourceUrl, descriptor.DataSourceUrl);
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
