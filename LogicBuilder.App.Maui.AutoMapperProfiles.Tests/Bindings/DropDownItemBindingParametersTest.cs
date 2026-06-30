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
    public class DropDownItemBindingParametersTest
    {
        static DropDownItemBindingParametersTest()
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
            string property = "Name";
            string title = "Select Item";
            string stringFormat = "{0}";
            var dropDownTemplate = new DropDownTemplateParameters(
                templateName: "PickerTemplate",
                titleText: "Title",
                textField: "Text",
                valueField: "Value",
                loadingIndicatorText: "loading",
                textAndValueSelector: new SelectorLambdaOperatorParameters(null!, null!, null!),
                requestDetails: new RequestDetailsParameters(null!, null!, null!, null!, null!),
                reloadItemsFlowName: null
            );
            bool requiresReload = false;
            string fieldTypeSource = "Contoso.Domain.Entities.Customer";
            IMapper mapper = serviceProvider.GetRequiredService<IMapper>();

            // Act
            var parameters = new DropDownItemBindingParameters(
                name: name,
                property: property,
                title: title,
                stringFormat: stringFormat,
                dropDownTemplate: dropDownTemplate,
                requiresReload: requiresReload,
                fieldTypeSource: fieldTypeSource
            );
            var descriptor = mapper.Map<DropDownItemBindingDescriptor>(parameters);

            // Assert
            Assert.Equal(name, descriptor.Name);
            Assert.Equal(property, descriptor.Property);
            Assert.Equal(title, descriptor.Title);
            Assert.Equal(stringFormat, descriptor.StringFormat);
            Assert.Equal(dropDownTemplate.TemplateName, descriptor.DropDownTemplate.TemplateName);
            Assert.Equal(requiresReload, descriptor.RequiresReload);
            Assert.Equal(fieldTypeSource, descriptor.FieldTypeSource);
        }

        [Fact]
        public void ConstructorShouldInitializeWithNullFieldTypeSource()
        {
            // Arrange
            string name = "Text";
            string property = "Description";
            string title = "Item Description";
            string stringFormat = "Value: {0}";
            var dropDownTemplate = new DropDownTemplateParameters(
                templateName: "PickerTemplate",
                titleText: "Title",
                textField: "Text",
                valueField: "Value",
                loadingIndicatorText: "loading",
                textAndValueSelector: new SelectorLambdaOperatorParameters(null!, null!, null!),
                requestDetails: new RequestDetailsParameters(null!, null!, null!, null!, null!),
                reloadItemsFlowName: null
            );
            bool requiresReload = true;
            IMapper mapper = serviceProvider.GetRequiredService<IMapper>();

            // Act
            var parameters = new DropDownItemBindingParameters(
                name: name,
                property: property,
                title: title,
                stringFormat: stringFormat,
                dropDownTemplate: dropDownTemplate,
                requiresReload: requiresReload
            );
            var descriptor = mapper.Map<DropDownItemBindingDescriptor>(parameters);

            // Assert
            Assert.Equal(name, descriptor.Name);
            Assert.Equal(property, descriptor.Property);
            Assert.Equal(title, descriptor.Title);
            Assert.Equal(stringFormat, descriptor.StringFormat);
            Assert.Equal(dropDownTemplate.TemplateName, descriptor.DropDownTemplate.TemplateName);
            Assert.Equal(requiresReload, descriptor.RequiresReload);
            Assert.Null(descriptor.FieldTypeSource);
        }

        [Fact]
        public void ConstructorShouldInitializeWithRequiresReloadTrue()
        {
            // Arrange
            string name = "Detail";
            string property = "Category";
            string title = "Category";
            string stringFormat = "{0}";
            var dropDownTemplate = new DropDownTemplateParameters(
                templateName: "PickerTemplate",
                titleText: "Title",
                textField: "Text",
                valueField: "Value",
                loadingIndicatorText: "loading",
                textAndValueSelector: new SelectorLambdaOperatorParameters(null!, null!, null!),
                requestDetails: new RequestDetailsParameters(null!, null!, null!, null!, null!),
                reloadItemsFlowName: "ReloadCategories"
            );
            bool requiresReload = true;
            string fieldTypeSource = "Contoso.Domain.Entities.Product";
            IMapper mapper = serviceProvider.GetRequiredService<IMapper>();

            // Act
            var parameters = new DropDownItemBindingParameters(
                name: name,
                property: property,
                title: title,
                stringFormat: stringFormat,
                dropDownTemplate: dropDownTemplate,
                requiresReload: requiresReload,
                fieldTypeSource: fieldTypeSource
            );
            var descriptor = mapper.Map<DropDownItemBindingDescriptor>(parameters);

            // Assert
            Assert.True(descriptor.RequiresReload);
            Assert.Equal("ReloadCategories", descriptor.DropDownTemplate.ReloadItemsFlowName);
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