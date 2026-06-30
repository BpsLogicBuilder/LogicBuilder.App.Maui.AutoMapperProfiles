using AutoMapper;
using LogicBuilder.App.Maui.Forms.Configuration.DataForm;
using LogicBuilder.App.Maui.Forms.Parameters;
using LogicBuilder.App.Maui.Forms.Parameters.DataForm;
using LogicBuilder.App.Maui.Forms.Parameters.Directives;
using LogicBuilder.App.Maui.Forms.Parameters.Validation;
using LogicBuilder.EntityFrameworkCore.Mapping;
using LogicBuilder.Expressions.Utils.ExpressionDescriptors;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace LogicBuilder.App.Maui.AutoMapperProfiles.Tests.DataForm
{
    public class FormGroupSettingsParametersTest
    {
        static FormGroupSettingsParametersTest()
        {
            Initialize();
        }

        private static MapperConfiguration MapperConfiguration;
        private static IServiceProvider serviceProvider;

        [Fact]
        public void ConstructorShouldInitializeAllProperties()
        {
            // Arrange
            string field = "StudentAddress";
            string title = "Title";
            string validFormControlText = "(Form)";
            string invalidFormControlText = "(Invalid Form)";
            string placeholder = "(Property name)";
            Type modelType = typeof(string);
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
            IMapper mapper = serviceProvider.GetRequiredService<IMapper>();

            // Act
            var parameters = new FormGroupSettingsParameters(
                field: field,
                title: title,
                validFormControlText: validFormControlText,
                invalidFormControlText: invalidFormControlText,
                placeholder: placeholder,
                modelType: modelType,
                formGroupTemplate: formGroupTemplate,
                fieldSettings: fieldSettings,
                validationMessages: validationMessages,
                conditionalDirectives: conditionalDirectives,
                headerBindings: headerBindings,
                fieldTypeSource: fieldTypeSource
            );
            var descriptor = mapper.Map<FormGroupSettingsDescriptor>(parameters);

            // Assert
            Assert.Equal(field, descriptor.Field);
            Assert.Equal(title, descriptor.Title);
            Assert.Equal(validFormControlText, descriptor.ValidFormControlText);
            Assert.Equal(invalidFormControlText, descriptor.InvalidFormControlText);
            Assert.Equal(placeholder, descriptor.Placeholder);
            Assert.Equal(modelType.AssemblyQualifiedName, descriptor.ModelType);
            Assert.Equal(formGroupTemplate.TemplateName, descriptor.FormGroupTemplate.TemplateName);
            Assert.Equal(((InputFieldControlSettingsParameters)fieldSettings[0]).Title, ((InputFieldControlSettingsDescriptor)descriptor.FieldSettings[0]).Title);
            Assert.Single(descriptor.ValidationMessages);
            Assert.Single(descriptor.ConditionalDirectives!);
            Assert.Equal(headerBindings.StringFormat, descriptor.HeaderBindings!.StringFormat);
            Assert.Equal(fieldTypeSource, descriptor.FieldTypeSource);
        }

        [Fact]
        public void ConstructorShouldInitializeAllPropertiesWhenFieldshaveRulesOrConditionalDirectivesWithNullValues()
        {
            // Arrange
            string field = "StudentAddress";
            string title = "Title";
            string validFormControlText = "(Form)";
            string invalidFormControlText = "(Invalid Form)";
            string placeholder = "(Property name)";
            Type modelType = typeof(string);
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
            IMapper mapper = serviceProvider.GetRequiredService<IMapper>();

            // Act
            var parameters = new FormGroupSettingsParameters(
                field: field,
                title: title,
                validFormControlText: validFormControlText,
                invalidFormControlText: invalidFormControlText,
                placeholder: placeholder,
                modelType: modelType,
                formGroupTemplate: formGroupTemplate,
                fieldSettings: fieldSettings,
                validationMessages: validationMessages,
                conditionalDirectives: conditionalDirectives,
                headerBindings: headerBindings,
                fieldTypeSource: fieldTypeSource
            );
            var descriptor = mapper.Map<FormGroupSettingsDescriptor>(parameters);

            // Assert
            Assert.Equal(field, descriptor.Field);
            Assert.Equal(title, descriptor.Title);
            Assert.Equal(validFormControlText, descriptor.ValidFormControlText);
            Assert.Equal(invalidFormControlText, descriptor.InvalidFormControlText);
            Assert.Equal(placeholder, descriptor.Placeholder);
            Assert.Equal(modelType.AssemblyQualifiedName, descriptor.ModelType);
            Assert.Equal(formGroupTemplate.TemplateName, descriptor.FormGroupTemplate.TemplateName);
            Assert.Equal(((InputFieldControlSettingsParameters)fieldSettings[0]).Title, ((InputFieldControlSettingsDescriptor)descriptor.FieldSettings[0]).Title);
            Assert.Single(descriptor.ValidationMessages);
            Assert.Single(descriptor.ConditionalDirectives!);
            Assert.Equal(headerBindings.StringFormat, descriptor.HeaderBindings!.StringFormat);
            Assert.Equal(fieldTypeSource, descriptor.FieldTypeSource);
        }

        [Fact]
        public void ConstructorShouldInitializeAllPropertiesWhenConditionalDirectivesIsNull()
        {
            // Arrange
            string field = "StudentAddress";
            string title = "Title";
            string validFormControlText = "(Form)";
            string invalidFormControlText = "(Invalid Form)";
            string placeholder = "(Property name)";
            Type modelType = typeof(string);
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
            IMapper mapper = serviceProvider.GetRequiredService<IMapper>();

            // Act
            var parameters = new FormGroupSettingsParameters(
                field: field,
                title: title,
                validFormControlText: validFormControlText,
                invalidFormControlText: invalidFormControlText,
                placeholder: placeholder,
                modelType: modelType,
                formGroupTemplate: formGroupTemplate,
                fieldSettings: fieldSettings,
                validationMessages: validationMessages,
                conditionalDirectives: null,
                headerBindings: headerBindings,
                fieldTypeSource: fieldTypeSource
            );
            var descriptor = mapper.Map<FormGroupSettingsDescriptor>(parameters);

            // Assert
            Assert.Equal(field, descriptor.Field);
            Assert.Equal(title, descriptor.Title);
            Assert.Equal(validFormControlText, descriptor.ValidFormControlText);
            Assert.Equal(invalidFormControlText, descriptor.InvalidFormControlText);
            Assert.Equal(placeholder, descriptor.Placeholder);
            Assert.Equal(modelType.AssemblyQualifiedName, descriptor.ModelType);
            Assert.Equal(formGroupTemplate.TemplateName, descriptor.FormGroupTemplate.TemplateName);
            Assert.Equal(((InputFieldControlSettingsParameters)fieldSettings[0]).Title, ((InputFieldControlSettingsDescriptor)descriptor.FieldSettings[0]).Title);
            Assert.Single(descriptor.ValidationMessages);
            Assert.Empty(descriptor.ConditionalDirectives!);
            Assert.Equal(headerBindings.StringFormat, descriptor.HeaderBindings!.StringFormat);
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
