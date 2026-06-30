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
    public class MultiSelectTemplateParametersTest
    {
        static MultiSelectTemplateParametersTest()
        {
            Initialize();
        }

        private static MapperConfiguration MapperConfiguration;
        private static IServiceProvider serviceProvider;

        [Fact]
        public void ConstructorShouldInitializeAllProperties()
        {
            // Arrange
            string templateName = "MultiSelectTemplate";
            string placeholderText = "(Courses)";
            string textField = "Text";
            string valueField = "Value";
            Type modelType = typeof(string);
            string loadingIndicatorText = "Loading ...";
            var textAndValueSelector = new SelectorLambdaOperatorParameters(null!, null!, null!);
            var requestDetails = new RequestDetailsParameters(null!, null!, null!, null!, null!);
            string? fieldTypeSource = "Contoso.Domain.Entities.Course";
            IMapper mapper = serviceProvider.GetRequiredService<IMapper>();

            // Act
            var parameters = new MultiSelectTemplateParameters(
                templateName: templateName,
                placeholderText: placeholderText,
                textField: textField,
                valueField: valueField,
                modelType: modelType,
                loadingIndicatorText: loadingIndicatorText,
                textAndValueSelector: textAndValueSelector,
                requestDetails: requestDetails,
                fieldTypeSource: fieldTypeSource
            );
            var descriptor = mapper.Map<MultiSelectTemplateDescriptor>(parameters);

            // Assert
            Assert.Equal(templateName, descriptor.TemplateName);
            Assert.Equal(placeholderText, descriptor.PlaceholderText);
            Assert.Equal(textField, descriptor.TextField);
            Assert.Equal(valueField, descriptor.ValueField);
            Assert.Equal(modelType.AssemblyQualifiedName, descriptor.ModelType);
            Assert.Equal(loadingIndicatorText, descriptor.LoadingIndicatorText);
            Assert.Equal(textAndValueSelector.ParameterName, descriptor.TextAndValueSelector.ParameterName);
            Assert.Equal(requestDetails.DataSourceUrl, descriptor.RequestDetails.DataSourceUrl);
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
