using AutoMapper;
using LogicBuilder.App.Maui.Forms.Configuration.Bindings;
using LogicBuilder.App.Maui.Forms.Parameters;
using LogicBuilder.App.Maui.Forms.Parameters.Bindings;
using LogicBuilder.EntityFrameworkCore.Mapping;
using LogicBuilder.Forms.Parameters.Expressions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Abstractions;
using System.Diagnostics.CodeAnalysis;

namespace LogicBuilder.App.Maui.AutoMapperProfiles.Tests.Bindings
{
    public class MultiSelectItemBindingParametersTest
    {
        static MultiSelectItemBindingParametersTest()
        {
            Initialize();
        }

        private static MapperConfiguration MapperConfiguration;
        private static IServiceProvider serviceProvider;

        [Fact]
        public void ConstructorShouldInitializeAllProperties()
        {
            // Arrange
            string name = "Header";
            string property = "SelectedItems";
            string title = "Select Items";
            string stringFormat = "{0}";
            var keyFields = new List<string> { "Id" };
            var multiSelectTemplate = new MultiSelectTemplateParameters(
                templateName: "MultiSelectTemplate",
                placeholderText: "Title",
                textField: "Text",
                valueField: "Value",
                modelType: typeof(string),
                loadingIndicatorText: "loading",
                textAndValueSelector: new SelectorLambdaOperatorParameters(null!, null!, null!),
                requestDetails: new RequestDetailsParameters(null!, null!, null!, null!, null!),
                fieldTypeSource: null
            );
            string fieldTypeSource = "Contoso.Domain.Entities.Order";
            string listElementTypeSource = "Contoso.Domain.Entities.Item";
            IMapper mapper = serviceProvider.GetRequiredService<IMapper>();

            // Act
            var parameters = new MultiSelectItemBindingParameters(
                name: name,
                property: property,
                title: title,
                stringFormat: stringFormat,
                keyFields: keyFields,
                multiSelectTemplate: multiSelectTemplate,
                fieldTypeSource: fieldTypeSource,
                listElementTypeSource: listElementTypeSource
            );
            var descriptor = mapper.Map<MultiSelectItemBindingDescriptor>(parameters);

            // Assert
            Assert.Equal(name, descriptor.Name);
            Assert.Equal(property, descriptor.Property);
            Assert.Equal(title, descriptor.Title);
            Assert.Equal(stringFormat, descriptor.StringFormat);
            Assert.Equal(keyFields, descriptor.KeyFields);
            Assert.Equal(multiSelectTemplate.TemplateName, descriptor.MultiSelectTemplate.TemplateName);
            Assert.Equal(fieldTypeSource, descriptor.FieldTypeSource);
            Assert.Equal(listElementTypeSource, descriptor.ListElementTypeSource);
        }

        [Fact]
        public void ConstructorShouldInitializeWithNullFieldTypeSource()
        {
            // Arrange
            string name = "Text";
            string property = "Categories";
            string title = "Categories";
            string stringFormat = "Selected: {0}";
            var keyFields = new List<string> { "CategoryId" };
            var multiSelectTemplate = new MultiSelectTemplateParameters(
                templateName: "MultiSelectTemplate",
                placeholderText: "Title",
                textField: "Text",
                valueField: "Value",
                modelType: typeof(string),
                loadingIndicatorText: "loading",
                textAndValueSelector: new SelectorLambdaOperatorParameters(null!, null!, null!),
                requestDetails: new RequestDetailsParameters(null!, null!, null!, null!, null!),
                fieldTypeSource: null
            );
            string listElementTypeSource = "Contoso.Domain.Entities.Category";
            IMapper mapper = serviceProvider.GetRequiredService<IMapper>();

            // Act
            var parameters = new MultiSelectItemBindingParameters(
                name: name,
                property: property,
                title: title,
                stringFormat: stringFormat,
                keyFields: keyFields,
                multiSelectTemplate: multiSelectTemplate,
                listElementTypeSource: listElementTypeSource
            );
            var descriptor = mapper.Map<MultiSelectItemBindingDescriptor>(parameters);

            // Assert
            Assert.Equal(name, descriptor.Name);
            Assert.Equal(property, descriptor.Property);
            Assert.Equal(title, descriptor.Title);
            Assert.Equal(stringFormat, descriptor.StringFormat);
            Assert.Equal(keyFields, descriptor.KeyFields);
            Assert.Equal(multiSelectTemplate.TemplateName, descriptor.MultiSelectTemplate.TemplateName);
            Assert.Null(descriptor.FieldTypeSource);
            Assert.Equal(listElementTypeSource, descriptor.ListElementTypeSource);
        }

        [Fact]
        public void ConstructorShouldInitializeWithCompositeKey()
        {
            // Arrange
            string name = "Detail";
            string property = "AssignedRoles";
            string title = "Roles";
            string stringFormat = "{0}";
            var keyFields = new List<string> { "UserId", "RoleId" };
            var multiSelectTemplate = new MultiSelectTemplateParameters(
                templateName: "MultiSelectTemplate",
                placeholderText: "Title",
                textField: "Text",
                valueField: "Value",
                modelType: typeof(string),
                loadingIndicatorText: "loading",
                textAndValueSelector: new SelectorLambdaOperatorParameters(null!, null!, null!),
                requestDetails: new RequestDetailsParameters(null!, null!, null!, null!, null!),
                fieldTypeSource: null
            );
            string fieldTypeSource = "Contoso.Domain.Entities.User";
            string listElementTypeSource = "Contoso.Domain.Entities.Role";
            IMapper mapper = serviceProvider.GetRequiredService<IMapper>();

            // Act
            var parameters = new MultiSelectItemBindingParameters(
                name: name,
                property: property,
                title: title,
                stringFormat: stringFormat,
                keyFields: keyFields,
                multiSelectTemplate: multiSelectTemplate,
                fieldTypeSource: fieldTypeSource,
                listElementTypeSource: listElementTypeSource
            );
            var descriptor = mapper.Map<MultiSelectItemBindingDescriptor>(parameters);

            // Assert
            Assert.Equal(2, descriptor.KeyFields.Count);
            Assert.Contains("UserId", descriptor.KeyFields);
            Assert.Contains("RoleId", descriptor.KeyFields);
        }

        [Fact]
        public void ConstructorShouldInitializeWithDefaultListElementTypeSource()
        {
            // Arrange
            string name = "Header";
            string property = "Tags";
            string title = "Tags";
            string stringFormat = "{0}";
            var keyFields = new List<string> { "Id" };
            var multiSelectTemplate = new MultiSelectTemplateParameters(
                templateName: "MultiSelectTemplate",
                placeholderText: "Title",
                textField: "Text",
                valueField: "Value",
                modelType: typeof(string),
                loadingIndicatorText: "loading",
                textAndValueSelector: new SelectorLambdaOperatorParameters(null!, null!, null!),
                requestDetails: new RequestDetailsParameters(null!, null!, null!, null!, null!),
                fieldTypeSource: null
            );
            IMapper mapper = serviceProvider.GetRequiredService<IMapper>();

            // Act
            var parameters = new MultiSelectItemBindingParameters(
                name: name,
                property: property,
                title: title,
                stringFormat: stringFormat,
                keyFields: keyFields,
                multiSelectTemplate: multiSelectTemplate
            );
            var descriptor = mapper.Map<MultiSelectItemBindingDescriptor>(parameters);

            // Assert
            Assert.Equal("Enrollment.Domain.Entities", descriptor.ListElementTypeSource);
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