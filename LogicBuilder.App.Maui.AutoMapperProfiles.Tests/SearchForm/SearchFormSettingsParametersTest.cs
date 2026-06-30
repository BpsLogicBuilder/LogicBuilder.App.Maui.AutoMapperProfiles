using AutoMapper;
using LogicBuilder.App.Maui.Forms.Configuration.SearchForm;
using LogicBuilder.App.Maui.Forms.Parameters;
using LogicBuilder.App.Maui.Forms.Parameters.Bindings;
using LogicBuilder.App.Maui.Forms.Parameters.SearchForm;
using LogicBuilder.EntityFrameworkCore.Mapping;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Abstractions;
using System.Diagnostics.CodeAnalysis;

namespace LogicBuilder.App.Maui.AutoMapperProfiles.Tests.SearchForm
{
    public class SearchFormSettingsParametersTest
    {
        static SearchFormSettingsParametersTest()
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
            Type modelType = typeof(string);
            string loadingIndicatorText = "Loading ...";
            string itemTemplateName = "HeaderTextDetailTemplate";
            string filterPlaceholder = "Filter";
            string createPagingSelectorFlowName = "paging_selector_models";
            var bindings = new List<ItemBindingParameters>
            {
                new TextItemBindingParameters("Header", "Name", "Header Text", "{0}", new TextFieldTemplateParameters("TextTemplate"))
            };
            var requestDetails = new RequestDetailsParameters(null!, null!, null!, null!, null!);
            IMapper mapper = serviceProvider.GetRequiredService<IMapper>();

            // Act
            var parameters = new SearchFormSettingsParameters(
                title: title,
                modelType: modelType,
                loadingIndicatorText: loadingIndicatorText,
                itemTemplateName: itemTemplateName,
                filterPlaceholder: filterPlaceholder,
                createPagingSelectorFlowName: createPagingSelectorFlowName,
                bindings: bindings,
                requestDetails: requestDetails
            );
            var descriptor = mapper.Map<SearchFormSettingsDescriptor>(parameters);

            // Assert
            Assert.Equal(title, descriptor.Title);
            Assert.Equal(modelType.AssemblyQualifiedName, descriptor.ModelType);
            Assert.Equal(loadingIndicatorText, descriptor.LoadingIndicatorText);
            Assert.Equal(itemTemplateName, descriptor.ItemTemplateName);
            Assert.Equal(filterPlaceholder, descriptor.FilterPlaceholder);
            Assert.Equal(createPagingSelectorFlowName, descriptor.CreatePagingSelectorFlowName);
            Assert.Single(descriptor.Bindings);
            Assert.Equal(requestDetails.DataSourceUrl, descriptor.RequestDetails.DataSourceUrl);
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
