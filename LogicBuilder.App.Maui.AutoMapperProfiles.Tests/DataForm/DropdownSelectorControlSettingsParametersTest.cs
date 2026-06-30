using AutoMapper;
using LogicBuilder.App.Maui.Forms.Configuration.DataForm;
using LogicBuilder.App.Maui.Forms.Parameters;
using LogicBuilder.App.Maui.Forms.Parameters.DataForm;
using LogicBuilder.App.Maui.Forms.Parameters.Validation;
using LogicBuilder.EntityFrameworkCore.Mapping;
using LogicBuilder.Forms.Parameters.Expressions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Abstractions;
using System.Diagnostics.CodeAnalysis;

namespace LogicBuilder.App.Maui.AutoMapperProfiles.Tests.DataForm
{
    public class DropdownSelectorControlSettingsParametersTest
    {
        static DropdownSelectorControlSettingsParametersTest()
        {
            Initialize();
        }

        private static MapperConfiguration MapperConfiguration;
        private static IServiceProvider serviceProvider;

        [Fact]
        public void ConstructorShouldInitializeAllProperties()
        {
            // Arrange
            string field = "DepartmentID";
            string title = "Title";
            string placeholder = "(Title) required";
            string stringFormat = "{0}";
            Type type = typeof(int);
            var dropDownTemplate = new DropDownTemplateParameters(
                "PickerTemplate",
                "Select Item",
                "Text",
                "Value",
                "Loading ...",
                new SelectorLambdaOperatorParameters(null!, null!, null!),
                new RequestDetailsParameters(null!, null!, null!, null!, null!)
            );
            var validationSetting = new FieldValidationSettingsParameters(null, null);
            string? fieldTypeSource = "Contoso.Domain.Entities";
            IMapper mapper = serviceProvider.GetRequiredService<IMapper>();

            // Act
            var parameters = new DropdownSelectorControlSettingsParameters(
                field: field,
                title: title,
                placeholder: placeholder,
                stringFormat: stringFormat,
                type: type,
                dropDownTemplate: dropDownTemplate,
                validationSetting: validationSetting,
                fieldTypeSource: fieldTypeSource
            );
            var descriptor = mapper.Map<DropdownSelectorControlSettingsDescriptor>(parameters);

            // Assert
            Assert.Equal(field, descriptor.Field);
            Assert.Equal(title, descriptor.Title);
            Assert.Equal(placeholder, descriptor.Placeholder);
            Assert.Equal(stringFormat, descriptor.StringFormat);
            Assert.Equal(type.AssemblyQualifiedName, descriptor.Type);
            Assert.Equal(dropDownTemplate.TemplateName, descriptor.DropDownTemplate.TemplateName);
            Assert.Equal(validationSetting.DefaultValue, descriptor.ValidationSetting!.DefaultValue);
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
