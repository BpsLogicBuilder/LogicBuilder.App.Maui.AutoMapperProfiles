using AutoMapper;
using LogicBuilder.App.Maui.Forms.Configuration;
using LogicBuilder.App.Maui.Forms.Parameters;
using LogicBuilder.EntityFrameworkCore.Mapping;
using LogicBuilder.Forms.Parameters.Expressions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Abstractions;
using System.Diagnostics.CodeAnalysis;

namespace LogicBuilder.App.Maui.AutoMapperProfiles.Tests
{
    public class DropDownTemplateParametersTest
    {
        static DropDownTemplateParametersTest()
        {
            Initialize();
        }

        private static MapperConfiguration MapperConfiguration;
        private static IServiceProvider serviceProvider;

        [Fact]
        public void ConstructorShouldInitializeAllProperties()
        {
            // Arrange
            string templateName = "PickerTemplate";
            string titleText = "Select Item";
            string textField = "Text";
            string valueField = "Value";
            string loadingIndicatorText = "Loading ...";
            var textAndValueSelector = new SelectorLambdaOperatorParameters(null!, null!, null!);
            var requestDetails = new RequestDetailsParameters(null!, null!, null!, null!, null!);
            string? reloadItemsFlowName = "ReloadFlow";
            string? fieldTypeSource = "Contoso.Domain.Entities.Customer";
            IMapper mapper = serviceProvider.GetRequiredService<IMapper>();

            // Act
            var parameters = new DropDownTemplateParameters(
                templateName: templateName,
                titleText: titleText,
                textField: textField,
                valueField: valueField,
                loadingIndicatorText: loadingIndicatorText,
                textAndValueSelector: textAndValueSelector,
                requestDetails: requestDetails,
                reloadItemsFlowName: reloadItemsFlowName,
                fieldTypeSource: fieldTypeSource
            );
            var descriptor = mapper.Map<DropDownTemplateDescriptor>(parameters);

            // Assert
            Assert.Equal(templateName, descriptor.TemplateName);
            Assert.Equal(titleText, descriptor.TitleText);
            Assert.Equal(textField, descriptor.TextField);
            Assert.Equal(valueField, descriptor.ValueField);
            Assert.Equal(loadingIndicatorText, descriptor.LoadingIndicatorText);
            Assert.Equal(textAndValueSelector.ParameterName, descriptor.TextAndValueSelector.ParameterName);
            Assert.Equal(requestDetails.DataSourceUrl, descriptor.RequestDetails.DataSourceUrl);
            Assert.Equal(reloadItemsFlowName, descriptor.ReloadItemsFlowName);
            Assert.Equal(fieldTypeSource, descriptor.FieldTypeSource);
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
