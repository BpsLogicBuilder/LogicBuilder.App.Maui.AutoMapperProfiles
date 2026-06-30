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
    public class DataFormSettingsParametersTest
    {
        static DataFormSettingsParametersTest()
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
            var validationMessages = new List<ValidationMessageParameters>
            {
                new("Field1", [])
            };
            var fieldSettings = new List<IFormItemSettingsParameters>
            {
                new InputFieldControlSettingsParameters("Field1", "Title", "Placeholder", "{0}", typeof(string), new TextFieldTemplateParameters("TextTemplate"))
            };
            Forms.Parameters.DataForm.FormType formType = Forms.Parameters.DataForm.FormType.Add;
            Type modelType = typeof(string);
            var headerBindings = new MultiBindingParameters("{0}", ["Field1"]);
            var requestDetails = new FormRequestDetailsParameters("getUrl", "addUrl", "updateUrl", "deleteUrl", typeof(string), typeof(int));
            var conditionalDirectives = new List<VariableDirectivesParameters>
            {
                new("Field1", [])
            };
            var subtitleBindings = new MultiBindingParameters("{0}", ["Field2"]);
            IMapper mapper = serviceProvider.GetRequiredService<IMapper>();

            // Act
            var parameters = new DataFormSettingsParameters(
                title: title,
                validationMessages: validationMessages,
                fieldSettings: fieldSettings,
                formType: formType,
                modelType: modelType,
                headerBindings: headerBindings,
                requestDetails: requestDetails,
                conditionalDirectives: conditionalDirectives,
                subtitleBindings: subtitleBindings
            );
            var descriptor = mapper.Map<DataFormSettingsDescriptor>(parameters);

            // Assert
            Assert.Equal(title, descriptor.Title);
            Assert.Single(descriptor.ValidationMessages);
            Assert.Equal(((InputFieldControlSettingsParameters)fieldSettings[0]).Title, ((InputFieldControlSettingsDescriptor)descriptor.FieldSettings[0]).Title);
            Assert.Equal(Forms.Configuration.DataForm.FormType.Add, descriptor.FormType);
            Assert.Equal(modelType.AssemblyQualifiedName, descriptor.ModelType);
            Assert.Equal(headerBindings.StringFormat, descriptor.HeaderBindings!.StringFormat);
            Assert.Equal(requestDetails.GetUrl, descriptor.RequestDetails!.GetUrl);
            Assert.Single(descriptor.ConditionalDirectives!);
            Assert.Equal(subtitleBindings.Fields.Count, descriptor.SubtitleBindings!.Fields.Count);
        }

        [Fact]
        public void ConstructorShouldInitializeAllPropertiesWhenValidationMessagesOrDirectivesHaveFieldsWithNullValues()
        {
            // Arrange
            string title = "Title";
            var validationMessages = new List<ValidationMessageParameters>
            {
                new("Field1", null!)
            };
            var fieldSettings = new List<IFormItemSettingsParameters>
            {
                new InputFieldControlSettingsParameters("Field1", "Title", "Placeholder", "{0}", typeof(string), new TextFieldTemplateParameters("TextTemplate"))
            };
            Forms.Parameters.DataForm.FormType formType = Forms.Parameters.DataForm.FormType.Add;
            Type modelType = typeof(string);
            var headerBindings = new MultiBindingParameters("{0}", ["Field1"]);
            var requestDetails = new FormRequestDetailsParameters("getUrl", "addUrl", "updateUrl", "deleteUrl", typeof(string), typeof(int));
            var conditionalDirectives = new List<VariableDirectivesParameters>
            {
                new("Field1", null!)
            };
            var subtitleBindings = new MultiBindingParameters("{0}", ["Field2"]);
            IMapper mapper = serviceProvider.GetRequiredService<IMapper>();

            // Act
            var parameters = new DataFormSettingsParameters(
                title: title,
                validationMessages: validationMessages,
                fieldSettings: fieldSettings,
                formType: formType,
                modelType: modelType,
                headerBindings: headerBindings,
                requestDetails: requestDetails,
                conditionalDirectives: conditionalDirectives,
                subtitleBindings: subtitleBindings
            );
            var descriptor = mapper.Map<DataFormSettingsDescriptor>(parameters);

            // Assert
            Assert.Equal(title, descriptor.Title);
            Assert.Single(descriptor.ValidationMessages);
            Assert.Equal(((InputFieldControlSettingsParameters)fieldSettings[0]).Title, ((InputFieldControlSettingsDescriptor)descriptor.FieldSettings[0]).Title);
            Assert.Equal(Forms.Configuration.DataForm.FormType.Add, descriptor.FormType);
            Assert.Equal(modelType.AssemblyQualifiedName, descriptor.ModelType);
            Assert.Equal(headerBindings.StringFormat, descriptor.HeaderBindings!.StringFormat);
            Assert.Equal(requestDetails.GetUrl, descriptor.RequestDetails!.GetUrl);
            Assert.Single(descriptor.ConditionalDirectives!);
            Assert.Equal(subtitleBindings.StringFormat, descriptor.SubtitleBindings!.StringFormat);
        }

        [Fact]
        public void ConstructorShouldInitializeAllPropertiesWhenConditionalDirectivesIsNull()
        {
            // Arrange
            string title = "Title";
            var validationMessages = new List<ValidationMessageParameters>
            {
                new("Field1", [])
            };
            var fieldSettings = new List<IFormItemSettingsParameters>
            {
                new InputFieldControlSettingsParameters("Field1", "Title", "Placeholder", "{0}", typeof(string), new TextFieldTemplateParameters("TextTemplate"))
            };
            Forms.Parameters.DataForm.FormType formType = Forms.Parameters.DataForm.FormType.Add;
            Type modelType = typeof(string);
            var headerBindings = new MultiBindingParameters("{0}", ["Field1"]);
            var requestDetails = new FormRequestDetailsParameters("getUrl", "addUrl", "updateUrl", "deleteUrl", typeof(string), typeof(int));

            var subtitleBindings = new MultiBindingParameters("{0}", ["Field2"]);
            IMapper mapper = serviceProvider.GetRequiredService<IMapper>();

            // Act
            var parameters = new DataFormSettingsParameters(
                title: title,
                validationMessages: validationMessages,
                fieldSettings: fieldSettings,
                formType: formType,
                modelType: modelType,
                headerBindings: headerBindings,
                requestDetails: requestDetails,
                conditionalDirectives: null,
                subtitleBindings: subtitleBindings
            );
            var descriptor = mapper.Map<DataFormSettingsDescriptor>(parameters);

            // Assert
            Assert.Equal(title, descriptor.Title);
            Assert.Single(descriptor.ValidationMessages);
            Assert.Equal(((InputFieldControlSettingsParameters)fieldSettings[0]).Title, ((InputFieldControlSettingsDescriptor)descriptor.FieldSettings[0]).Title);
            Assert.Equal(Forms.Configuration.DataForm.FormType.Add, descriptor.FormType);
            Assert.Equal(modelType.AssemblyQualifiedName, descriptor.ModelType);
            Assert.Equal(headerBindings.StringFormat, descriptor.HeaderBindings!.StringFormat);
            Assert.Equal(requestDetails.GetUrl, descriptor.RequestDetails!.GetUrl);
            Assert.Empty(descriptor.ConditionalDirectives!);
            Assert.Equal(subtitleBindings.StringFormat, descriptor.SubtitleBindings!.StringFormat);
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
