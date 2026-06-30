using AutoMapper;
using LogicBuilder.App.Maui.Forms.Configuration.DataForm;
using LogicBuilder.App.Maui.Forms.Parameters;
using LogicBuilder.App.Maui.Forms.Parameters.DataForm;
using LogicBuilder.App.Maui.Forms.Parameters.Directives;
using LogicBuilder.App.Maui.Forms.Parameters.Validation;
using LogicBuilder.EntityFrameworkCore.Mapping;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Abstractions;
using System.Diagnostics.CodeAnalysis;

namespace LogicBuilder.App.Maui.AutoMapperProfiles.Tests.DataForm
{
    public class FormGroupArraySettingsParametersTest
    {
        static FormGroupArraySettingsParametersTest()
        {
            Initialize();
        }

        private static MapperConfiguration MapperConfiguration;
        private static IServiceProvider serviceProvider;

        [Fact]
        public void ConstructorShouldInitializeAllProperties()
        {
            // Arrange
            string field = "Addresses";
            var keyFields = new List<string> { "ID" };
            string title = "Title";
            string placeholder = "(Form Collection)";
            Type modelType = typeof(string);
            Type type = typeof(List<string>);
            string validFormControlText = "(Form)";
            string invalidFormControlText = "(Invalid Form)";
            var formsCollectionDisplayTemplate = new FormsCollectionDisplayTemplateParameters("HeaderTextDetailTemplate", []);
            var formGroupTemplate = new FormGroupTemplateParameters("PopupFormGroupTemplate");
            var fieldSettings = new List<IFormItemSettingsParameters>
            {
                new InputFieldControlSettingsParameters("Field1", "Title", "Placeholder", "{0}", typeof(string), new TextFieldTemplateParameters("TextTemplate"))
            };
            var validationMessages = new List<ValidationMessageParameters>
            {
                new("Field1", [])
            };
            var conditionalDirectives = new List<VariableDirectivesParameters>
            {
                new("Field1", [])
            };
            var headerBindings = new MultiBindingParameters("{0}", ["Field1"]);
            string? fieldTypeSource = "Contoso.Domain.Entities.Student";
            string? listElementTypeSource = "Contoso.Domain.Entities.Address";
            IMapper mapper = serviceProvider.GetRequiredService<IMapper>();

            // Act
            var parameters = new FormGroupArraySettingsParameters(
                field: field,
                keyFields: keyFields,
                title: title,
                placeholder: placeholder,
                modelType: modelType,
                type: type,
                validFormControlText: validFormControlText,
                invalidFormControlText: invalidFormControlText,
                formsCollectionDisplayTemplate: formsCollectionDisplayTemplate,
                formGroupTemplate: formGroupTemplate,
                fieldSettings: fieldSettings,
                validationMessages: validationMessages,
                conditionalDirectives: conditionalDirectives,
                headerBindings: headerBindings,
                fieldTypeSource: fieldTypeSource,
                listElementTypeSource: listElementTypeSource
            );
            var descriptor = mapper.Map<FormGroupArraySettingsDescriptor>(parameters);

            // Assert
            Assert.Equal(field, descriptor.Field);
            Assert.Equal(keyFields, descriptor.KeyFields);
            Assert.Equal(title, descriptor.Title);
            Assert.Equal(placeholder, descriptor.Placeholder);
            Assert.Equal(modelType.AssemblyQualifiedName, descriptor.ModelType);
            Assert.Equal(type.AssemblyQualifiedName, descriptor.Type);
            Assert.Equal(validFormControlText, descriptor.ValidFormControlText);
            Assert.Equal(invalidFormControlText, descriptor.InvalidFormControlText);
            Assert.Equal(formsCollectionDisplayTemplate.TemplateName, descriptor.FormsCollectionDisplayTemplate.TemplateName);
            Assert.Equal(formGroupTemplate.TemplateName, descriptor.FormGroupTemplate.TemplateName);
            Assert.Equal(((InputFieldControlSettingsParameters)fieldSettings[0]).Title, ((InputFieldControlSettingsDescriptor)descriptor.FieldSettings[0]).Title);
            Assert.Single(descriptor.ValidationMessages);
            Assert.Single(descriptor.ConditionalDirectives!);
            Assert.Equal(headerBindings.StringFormat, descriptor.HeaderBindings!.StringFormat);
            Assert.Equal(fieldTypeSource, descriptor.FieldTypeSource);
            Assert.Equal(listElementTypeSource, descriptor.ListElementTypeSource);
        }

        [Fact]
        public void ConstructorShouldInitializeAllPropertiesWhenValidationMessagesOrDirectivesHaveFieldsWithNullCollections()
        {
            // Arrange
            string field = "Addresses";
            var keyFields = new List<string> { "ID" };
            string title = "Title";
            string placeholder = "(Form Collection)";
            Type modelType = typeof(string);
            Type type = typeof(List<string>);
            string validFormControlText = "(Form)";
            string invalidFormControlText = "(Invalid Form)";
            var formsCollectionDisplayTemplate = new FormsCollectionDisplayTemplateParameters("HeaderTextDetailTemplate", []);
            var formGroupTemplate = new FormGroupTemplateParameters("PopupFormGroupTemplate");
            var fieldSettings = new List<IFormItemSettingsParameters>
            {
                new InputFieldControlSettingsParameters("Field1", "Title", "Placeholder", "{0}", typeof(string), new TextFieldTemplateParameters("TextTemplate"))
            };
            var validationMessages = new List<ValidationMessageParameters>
            {
                new("Field1", null!)
            };
            var conditionalDirectives = new List<VariableDirectivesParameters>
            {
                new("Field1", null!)
            };
            var headerBindings = new MultiBindingParameters("{0}", ["Field1"]);
            string? fieldTypeSource = "Contoso.Domain.Entities.Student";
            string? listElementTypeSource = "Contoso.Domain.Entities.Address";
            IMapper mapper = serviceProvider.GetRequiredService<IMapper>();

            // Act
            var parameters = new FormGroupArraySettingsParameters(
                field: field,
                keyFields: keyFields,
                title: title,
                placeholder: placeholder,
                modelType: modelType,
                type: type,
                validFormControlText: validFormControlText,
                invalidFormControlText: invalidFormControlText,
                formsCollectionDisplayTemplate: formsCollectionDisplayTemplate,
                formGroupTemplate: formGroupTemplate,
                fieldSettings: fieldSettings,
                validationMessages: validationMessages,
                conditionalDirectives: conditionalDirectives,
                headerBindings: headerBindings,
                fieldTypeSource: fieldTypeSource,
                listElementTypeSource: listElementTypeSource
            );
            var descriptor = mapper.Map<FormGroupArraySettingsDescriptor>(parameters);

            // Assert
            Assert.Equal(field, descriptor.Field);
            Assert.Equal(keyFields, descriptor.KeyFields);
            Assert.Equal(title, descriptor.Title);
            Assert.Equal(placeholder, descriptor.Placeholder);
            Assert.Equal(modelType.AssemblyQualifiedName, descriptor.ModelType);
            Assert.Equal(type.AssemblyQualifiedName, descriptor.Type);
            Assert.Equal(validFormControlText, descriptor.ValidFormControlText);
            Assert.Equal(invalidFormControlText, descriptor.InvalidFormControlText);
            Assert.Equal(formsCollectionDisplayTemplate.TemplateName, descriptor.FormsCollectionDisplayTemplate.TemplateName);
            Assert.Equal(formGroupTemplate.TemplateName, descriptor.FormGroupTemplate.TemplateName);
            Assert.Equal(((InputFieldControlSettingsParameters)fieldSettings[0]).Title, ((InputFieldControlSettingsDescriptor)descriptor.FieldSettings[0]).Title);
            Assert.Single(descriptor.ValidationMessages);
            Assert.Single(descriptor.ConditionalDirectives!);
            Assert.Equal(headerBindings.StringFormat, descriptor.HeaderBindings!.StringFormat);
            Assert.Equal(fieldTypeSource, descriptor.FieldTypeSource);
            Assert.Equal(listElementTypeSource, descriptor.ListElementTypeSource);
        }

        [Fact]
        public void ConstructorShouldInitializeAllPropertiesWhenConditionalDirectivesIsNull()
        {
            // Arrange
            string field = "Addresses";
            var keyFields = new List<string> { "ID" };
            string title = "Title";
            string placeholder = "(Form Collection)";
            Type modelType = typeof(string);
            Type type = typeof(List<string>);
            string validFormControlText = "(Form)";
            string invalidFormControlText = "(Invalid Form)";
            var formsCollectionDisplayTemplate = new FormsCollectionDisplayTemplateParameters("HeaderTextDetailTemplate", []);
            var formGroupTemplate = new FormGroupTemplateParameters("PopupFormGroupTemplate");
            var fieldSettings = new List<IFormItemSettingsParameters>
            {
                new InputFieldControlSettingsParameters("Field1", "Title", "Placeholder", "{0}", typeof(string), new TextFieldTemplateParameters("TextTemplate"))
            };
            var validationMessages = new List<ValidationMessageParameters>
            {
                new("Field1", [])
            };

            var headerBindings = new MultiBindingParameters("{0}", ["Field1"]);
            string? fieldTypeSource = "Contoso.Domain.Entities.Student";
            string? listElementTypeSource = "Contoso.Domain.Entities.Address";
            IMapper mapper = serviceProvider.GetRequiredService<IMapper>();

            // Act
            var parameters = new FormGroupArraySettingsParameters(
                field: field,
                keyFields: keyFields,
                title: title,
                placeholder: placeholder,
                modelType: modelType,
                type: type,
                validFormControlText: validFormControlText,
                invalidFormControlText: invalidFormControlText,
                formsCollectionDisplayTemplate: formsCollectionDisplayTemplate,
                formGroupTemplate: formGroupTemplate,
                fieldSettings: fieldSettings,
                validationMessages: validationMessages,
                conditionalDirectives: null,
                headerBindings: headerBindings,
                fieldTypeSource: fieldTypeSource,
                listElementTypeSource: listElementTypeSource
            );
            var descriptor = mapper.Map<FormGroupArraySettingsDescriptor>(parameters);

            // Assert
            Assert.Equal(field, descriptor.Field);
            Assert.Equal(keyFields, descriptor.KeyFields);
            Assert.Equal(title, descriptor.Title);
            Assert.Equal(placeholder, descriptor.Placeholder);
            Assert.Equal(modelType.AssemblyQualifiedName, descriptor.ModelType);
            Assert.Equal(type.AssemblyQualifiedName, descriptor.Type);
            Assert.Equal(validFormControlText, descriptor.ValidFormControlText);
            Assert.Equal(invalidFormControlText, descriptor.InvalidFormControlText);
            Assert.Equal(formsCollectionDisplayTemplate.TemplateName, descriptor.FormsCollectionDisplayTemplate.TemplateName);
            Assert.Equal(formGroupTemplate.TemplateName, descriptor.FormGroupTemplate.TemplateName);
            Assert.Equal(((InputFieldControlSettingsParameters)fieldSettings[0]).Title, ((InputFieldControlSettingsDescriptor)descriptor.FieldSettings[0]).Title);
            Assert.Single(descriptor.ValidationMessages);
            Assert.Empty(descriptor.ConditionalDirectives!);
            Assert.Equal(headerBindings.StringFormat, descriptor.HeaderBindings!.StringFormat);
            Assert.Equal(fieldTypeSource, descriptor.FieldTypeSource);
            Assert.Equal(listElementTypeSource, descriptor.ListElementTypeSource);
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
