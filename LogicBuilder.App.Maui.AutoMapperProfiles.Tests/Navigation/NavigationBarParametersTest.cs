using AutoMapper;
using LogicBuilder.App.Maui.Forms.Configuration.Navigation;
using LogicBuilder.App.Maui.Forms.Parameters.Navigation;
using LogicBuilder.EntityFrameworkCore.Mapping;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Abstractions;
using System.Diagnostics.CodeAnalysis;

namespace LogicBuilder.App.Maui.AutoMapperProfiles.Tests.Navigation
{
    public class NavigationBarParametersTest
    {
        static NavigationBarParametersTest()
        {
            Initialize();
        }

        private static MapperConfiguration MapperConfiguration;
        private static IServiceProvider serviceProvider;

        [Fact]
        public void ConstructorShouldInitializeAllProperties()
        {
            // Arrange
            string brandText = "Contoso";
            string currentModule = "initial";
            var menuItems = new List<NavigationMenuItemParameters>
            {
                new("home", "Home", "Home")
            };
            IMapper mapper = serviceProvider.GetRequiredService<IMapper>();

            // Act
            var parameters = new NavigationBarParameters(
                brandText: brandText,
                currentModule: currentModule,
                MenuItems: menuItems
            );
            var descriptor = mapper.Map<NavigationBarDescriptor>(parameters);

            // Assert
            Assert.Equal(brandText, descriptor.BrandText);
            Assert.Equal(currentModule, descriptor.CurrentModule);
            Assert.Equal(menuItems[0].Text, descriptor.MenuItems[0].Text);
        }

        [Fact]
        public void ConstructorShouldInitializeAllPropertiesWhenMenuItemsIsNull()
        {
            // Arrange
            string brandText = "Contoso";
            string currentModule = "initial";
            IMapper mapper = serviceProvider.GetRequiredService<IMapper>();

            // Act
            var parameters = new NavigationBarParameters(
                brandText: brandText,
                currentModule: currentModule,
                MenuItems: null
            );
            var descriptor = mapper.Map<NavigationBarDescriptor>(parameters);

            // Assert
            Assert.Equal(brandText, descriptor.BrandText);
            Assert.Equal(currentModule, descriptor.CurrentModule);
            Assert.Empty(descriptor.MenuItems);
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
