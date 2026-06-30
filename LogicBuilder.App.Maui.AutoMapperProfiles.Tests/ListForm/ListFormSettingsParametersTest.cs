using AutoMapper;
using LogicBuilder.App.Maui.Forms.Configuration.ListForm;
using LogicBuilder.App.Maui.Forms.Parameters;
using LogicBuilder.App.Maui.Forms.Parameters.Bindings;
using LogicBuilder.App.Maui.Forms.Parameters.ListForm;
using LogicBuilder.EntityFrameworkCore.Mapping;
using LogicBuilder.Expressions.Utils.ExpressionDescriptors;
using LogicBuilder.Forms.Parameters.Expressions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace LogicBuilder.App.Maui.AutoMapperProfiles.Tests.ListForm
{
    public class ListFormSettingsParametersTest
    {
        static ListFormSettingsParametersTest()
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
            Type modelType = typeof(string);
            string loadingIndicatorText = "Loading ...";
            string itemTemplateName = "HeaderTextDetailTemplate";
            var bindings = new List<ItemBindingParameters>
            {
                new TextItemBindingParameters("Header", "Name", "Header Text", "{0}", new TextFieldTemplateParameters("TextTemplate"))
            };
            var fieldsSelector = new SelectorLambdaOperatorParameters(null!, null!, null!);
            var requestDetails = new RequestDetailsParameters(null!, null!, null!, null!, null!);
            IMapper mapper = serviceProvider.GetRequiredService<IMapper>();

            // Act
            var parameters = new ListFormSettingsParameters(
                title: title,
                modelType: modelType,
                loadingIndicatorText: loadingIndicatorText,
                itemTemplateName: itemTemplateName,
                bindings: bindings,
                fieldsSelector: fieldsSelector,
                requestDetails: requestDetails
            );
            var descriptor = mapper.Map<ListFormSettingsDescriptor>(parameters);

            // Assert
            Assert.Equal(title, descriptor.Title);
            Assert.Equal(modelType.AssemblyQualifiedName, descriptor.ModelType);
            Assert.Equal(loadingIndicatorText, descriptor.LoadingIndicatorText);
            Assert.Equal(itemTemplateName, descriptor.ItemTemplateName);
            Assert.Single(descriptor.Bindings);
            Assert.Equal(fieldsSelector.ParameterName, descriptor.FieldsSelector.ParameterName);
            Assert.Equal(requestDetails.DataSourceUrl, descriptor.RequestDetails.DataSourceUrl);
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
