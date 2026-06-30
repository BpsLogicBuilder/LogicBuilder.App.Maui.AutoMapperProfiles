using AutoMapper;
using LogicBuilder.App.Maui.Forms.Configuration.Bindings;
using LogicBuilder.App.Maui.Forms.Parameters;
using LogicBuilder.App.Maui.Forms.Parameters.Bindings;
using LogicBuilder.EntityFrameworkCore.Mapping;
using LogicBuilder.Expressions.Utils.ExpressionDescriptors;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Abstractions;
using System.Diagnostics.CodeAnalysis;

namespace LogicBuilder.App.Maui.AutoMapperProfiles.Tests.Bindings
{
    public class TextItemBindingParametersTest
    {
        static TextItemBindingParametersTest()
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
            string property = "FirstName";
            string title = "First Name";
            string stringFormat = "{0}";
            var textTemplate = new TextFieldTemplateParameters(templateName: "TextTemplate");
            string fieldTypeSource = "Contoso.Domain.Entities.Person";
            IMapper mapper = serviceProvider.GetRequiredService<IMapper>();

            // Act
            var parameters = new TextItemBindingParameters(
                name: name,
                property: property,
                title: title,
                stringFormat: stringFormat,
                textTemplate: textTemplate,
                fieldTypeSource: fieldTypeSource
            );
            var descriptor = mapper.Map<TextItemBindingDescriptor>(parameters);

            // Assert
            Assert.Equal(name, descriptor.Name);
            Assert.Equal(property, descriptor.Property);
            Assert.Equal(title, descriptor.Title);
            Assert.Equal(stringFormat, descriptor.StringFormat);
            Assert.Equal(textTemplate.TemplateName, descriptor.TextTemplate.TemplateName);
            Assert.Equal(fieldTypeSource, descriptor.FieldTypeSource);
        }

        [Fact]
        public void ConstructorShouldInitializeWithNullFieldTypeSource()
        {
            // Arrange
            string name = "Text";
            string property = "LastName";
            string title = "Last Name";
            string stringFormat = "Name: {0}";
            var textTemplate = new TextFieldTemplateParameters(templateName: "TextTemplate");
            IMapper mapper = serviceProvider.GetRequiredService<IMapper>();

            // Act
            var parameters = new TextItemBindingParameters(
                name: name,
                property: property,
                title: title,
                stringFormat: stringFormat,
                textTemplate: textTemplate
            );
            var descriptor = mapper.Map<TextItemBindingDescriptor>(parameters);

            // Assert
            Assert.Equal(name, descriptor.Name);
            Assert.Equal(property, descriptor.Property);
            Assert.Equal(title, descriptor.Title);
            Assert.Equal(stringFormat, descriptor.StringFormat);
            Assert.Equal(textTemplate.TemplateName, descriptor.TextTemplate.TemplateName);
            Assert.Null(descriptor.FieldTypeSource);
        }

        [Fact]
        public void ConstructorShouldInitializeWithDifferentTemplateTypes()
        {
            // Arrange
            string name = "Detail";
            string property = "Email";
            string title = "Email Address";
            string stringFormat = "{0}";
            var textTemplate = new TextFieldTemplateParameters(templateName: "PasswordTemplate");
            string fieldTypeSource = "Contoso.Domain.Entities.User";
            IMapper mapper = serviceProvider.GetRequiredService<IMapper>();

            // Act
            var parameters = new TextItemBindingParameters(
                name: name,
                property: property,
                title: title,
                stringFormat: stringFormat,
                textTemplate: textTemplate,
                fieldTypeSource: fieldTypeSource
            );
            var descriptor = mapper.Map<TextItemBindingDescriptor>(parameters);

            // Assert
            Assert.Equal("PasswordTemplate", descriptor.TextTemplate.TemplateName);
        }

        [Fact]
        public void ConstructorShouldInitializeWithDateTemplate()
        {
            // Arrange
            string name = "Header";
            string property = "BirthDate";
            string title = "Birth Date";
            string stringFormat = "{0:MM/dd/yyyy}";
            var textTemplate = new TextFieldTemplateParameters(templateName: "DateTemplate");
            string fieldTypeSource = "Contoso.Domain.Entities.Person";
            IMapper mapper = serviceProvider.GetRequiredService<IMapper>();

            // Act
            var parameters = new TextItemBindingParameters(
                name: name,
                property: property,
                title: title,
                stringFormat: stringFormat,
                textTemplate: textTemplate,
                fieldTypeSource: fieldTypeSource
            );
            var descriptor = mapper.Map<TextItemBindingDescriptor>(parameters);

            // Assert
            Assert.Equal("DateTemplate", descriptor.TextTemplate.TemplateName);
            Assert.Equal("{0:MM/dd/yyyy}", descriptor.StringFormat);
        }

        [Fact]
        public void ConstructorShouldInitializeWithCheckboxTemplate()
        {
            // Arrange
            string name = "Text";
            string property = "IsActive";
            string title = "Active";
            string stringFormat = "{0}";
            var textTemplate = new TextFieldTemplateParameters(templateName: "CheckboxTemplate");
            string fieldTypeSource = "Contoso.Domain.Entities.Employee";
            IMapper mapper = serviceProvider.GetRequiredService<IMapper>();

            // Act
            var parameters = new TextItemBindingParameters(
                name: name,
                property: property,
                title: title,
                stringFormat: stringFormat,
                textTemplate: textTemplate,
                fieldTypeSource: fieldTypeSource
            );
            var descriptor = mapper.Map<TextItemBindingDescriptor>(parameters);

            // Assert
            Assert.Equal("CheckboxTemplate", descriptor.TextTemplate.TemplateName);
        }

        [Fact]
        public void ConstructorShouldInitializeWithSwitchTemplate()
        {
            // Arrange
            string name = "Detail";
            string property = "IsEnabled";
            string title = "Enabled";
            string stringFormat = "{0}";
            var textTemplate = new TextFieldTemplateParameters(templateName: "SwitchTemplate");
            string fieldTypeSource = "Enrollment.Domain.Entities.Configuration";
            IMapper mapper = serviceProvider.GetRequiredService<IMapper>();

            // Act
            var parameters = new TextItemBindingParameters(
                name: name,
                property: property,
                title: title,
                stringFormat: stringFormat,
                textTemplate: textTemplate,
                fieldTypeSource: fieldTypeSource
            );
            var descriptor = mapper.Map<TextItemBindingDescriptor>(parameters);

            // Assert
            Assert.Equal("SwitchTemplate", descriptor.TextTemplate.TemplateName);
        }

        [Fact]
        public void ConstructorShouldInitializeWithLabelTemplate()
        {
            // Arrange
            string name = "Header";
            string property = "DisplayName";
            string title = "Display Name";
            string stringFormat = "{0}";
            var textTemplate = new TextFieldTemplateParameters(templateName: "LabelTemplate");
            string fieldTypeSource = "Enrollment.Domain.Entities.Student";
            IMapper mapper = serviceProvider.GetRequiredService<IMapper>();

            // Act
            var parameters = new TextItemBindingParameters(
                name: name,
                property: property,
                title: title,
                stringFormat: stringFormat,
                textTemplate: textTemplate,
                fieldTypeSource: fieldTypeSource
            );
            var descriptor = mapper.Map<TextItemBindingDescriptor>(parameters);

            // Assert
            Assert.Equal("LabelTemplate", descriptor.TextTemplate.TemplateName);
        }

        [Fact]
        public void ConstructorShouldInitializeWithHiddenTemplate()
        {
            // Arrange
            string name = "Text";
            string property = "InternalId";
            string title = "Internal ID";
            string stringFormat = "{0}";
            var textTemplate = new TextFieldTemplateParameters(templateName: "HiddenTemplate");
            IMapper mapper = serviceProvider.GetRequiredService<IMapper>();

            // Act
            var parameters = new TextItemBindingParameters(
                name: name,
                property: property,
                title: title,
                stringFormat: stringFormat,
                textTemplate: textTemplate
            );
            var descriptor = mapper.Map<TextItemBindingDescriptor>(parameters);

            // Assert
            Assert.Equal("HiddenTemplate", descriptor.TextTemplate.TemplateName);
            Assert.Null(descriptor.FieldTypeSource);
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