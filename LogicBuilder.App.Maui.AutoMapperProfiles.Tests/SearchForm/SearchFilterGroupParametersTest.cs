using AutoMapper;
using LogicBuilder.App.Maui.Forms.Configuration.SearchForm;
using LogicBuilder.App.Maui.Forms.Parameters.SearchForm;
using LogicBuilder.EntityFrameworkCore.Mapping;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Abstractions;
using System.Diagnostics.CodeAnalysis;

namespace LogicBuilder.App.Maui.AutoMapperProfiles.Tests.SearchForm
{
    public class SearchFilterGroupParametersTest
    {
        static SearchFilterGroupParametersTest()
        {
            Initialize();
        }

        private static MapperConfiguration MapperConfiguration;
        private static IServiceProvider serviceProvider;

        [Fact]
        public void ConstructorShouldInitializeAllProperties()
        {
            // Arrange
            ICollection<ISearchFilterParameters> filters =
            [
                new SearchFilterParameters("FirstName"),
                new SearchFilterParameters("LastName")
            ];
            IMapper mapper = serviceProvider.GetRequiredService<IMapper>();

            // Act
            var parameters = new SearchFilterGroupParameters(
                filters: filters
            );
            var descriptor = mapper.Map<SearchFilterGroupDescriptor>(parameters);

            // Assert
            Assert.Equal(((SearchFilterParameters)filters.First()).Field, ((SearchFilterDescriptor)descriptor.Filters.First()).Field);
            Assert.Equal(2, descriptor.Filters.Count);
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
