using AutoMapper;
using LogicBuilder.App.Maui.Forms.Configuration.Navigation;
using LogicBuilder.App.Maui.Forms.Parameters.Navigation;
using LogicBuilder.EntityFrameworkCore.Mapping;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Abstractions;
using System.Diagnostics.CodeAnalysis;

namespace LogicBuilder.App.Maui.AutoMapperProfiles.Tests.Navigation
{
    public class NavigationMenuItemParametersTest
    {
        static NavigationMenuItemParametersTest()
        {
            Initialize();
        }

        private static MapperConfiguration MapperConfiguration;
        private static IServiceProvider serviceProvider;

        [Fact]
        public void ConstructorShouldInitializeAllProperties()
        {
            // Arrange
            string initialModule = "initial";
            string text = "menuText";
            string icon = "Home";
            var subItems = new List<NavigationMenuItemParameters>
            {
                new("submenu", "SubMenu", "List")
            };
            IMapper mapper = serviceProvider.GetRequiredService<IMapper>();

            // Act
            var parameters = new NavigationMenuItemParameters(
                initialModule: initialModule,
                text: text,
                icon: icon,
                active: true,
                SubItems: subItems
            );
            var descriptor = mapper.Map<NavigationMenuItemDescriptor>(parameters);

            // Assert
            Assert.Equal(initialModule, descriptor.InitialModule);
            Assert.Equal(text, descriptor.Text);
            Assert.Equal(icon, descriptor.Icon);
            Assert.Equal(subItems[0].Text, descriptor.SubItems[0].Text);
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
