using AutoMapper;
using LogicBuilder.App.Maui.Forms.Configuration.DataForm;
using LogicBuilder.App.Maui.Forms.Parameters;
using LogicBuilder.App.Maui.Forms.Parameters.DataForm;
using LogicBuilder.EntityFrameworkCore.Mapping;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Abstractions;
using System.Diagnostics.CodeAnalysis;

namespace LogicBuilder.App.Maui.AutoMapperProfiles.Tests.DataForm
{
    public class FormGroupBoxSettingsParametersTest
    {
        static FormGroupBoxSettingsParametersTest()
        {
            Initialize();
        }

        private static MapperConfiguration MapperConfiguration;
        private static IServiceProvider serviceProvider;

        [Fact]
        public void ConstructorShouldInitializeAllProperties()
        {
            // Arrange
            string groupHeader = "Header";
            var fieldSettings = new List<IFormItemSettingsParameters>
            {
                new InputFieldControlSettingsParameters("Field1", "Title", "Placeholder", "{0}", typeof(string), new TextFieldTemplateParameters("TextTemplate"))
            };
            var headerBindings = new MultiBindingParameters("{0}", ["Field1"]);
            bool isHidden = false;
            IMapper mapper = serviceProvider.GetRequiredService<IMapper>();

            // Act
            var parameters = new FormGroupBoxSettingsParameters(
                groupHeader: groupHeader,
                fieldSettings: fieldSettings,
                headerBindings: headerBindings,
                isHidden: isHidden
            );
            var descriptor = mapper.Map<FormGroupBoxSettingsDescriptor>(parameters);

            // Assert
            Assert.Equal(groupHeader, descriptor.GroupHeader);
            Assert.Equal(((InputFieldControlSettingsParameters)fieldSettings[0]).Title, ((InputFieldControlSettingsDescriptor)descriptor.FieldSettings[0]).Title);
            Assert.Equal(headerBindings.StringFormat, descriptor.HeaderBindings!.StringFormat);
            Assert.Equal(isHidden, descriptor.IsHidden);
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
