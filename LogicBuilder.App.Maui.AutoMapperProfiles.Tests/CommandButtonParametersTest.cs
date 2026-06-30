using AutoMapper;
using LogicBuilder.App.Maui.Forms.Configuration;
using LogicBuilder.App.Maui.Forms.Parameters;
using LogicBuilder.Forms.Parameters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Abstractions;
using System.Diagnostics.CodeAnalysis;

namespace LogicBuilder.App.Maui.AutoMapperProfiles.Tests
{
    public class CommandButtonParametersTest
    {
        static CommandButtonParametersTest()
        {
            Initialize();
        }

        private static MapperConfiguration MapperConfiguration;
        private static IServiceProvider serviceProvider;

        [Fact]
        public void ConstructorShouldInitializeAllProperties()
        {
            // Arrange
            string command = "SubmitCommand";
            string buttonIcon = "Save";
            IMapper mapper = serviceProvider.GetRequiredService<IMapper>();

            // Act
            var parameters = new CommandButtonParameters(
                command: command,
                buttonIcon: buttonIcon
            );
            CommandButtonDescriptor button = mapper.Map<CommandButtonDescriptor>(parameters);

            // Assert
            Assert.Equal(command, button.Command);
            Assert.Equal(buttonIcon, button.ButtonIcon);
        }

        [Fact]
        public void Map_ConnectorParameters_To_CommandButtonDescriptor()
        {
            // Arrange
            ConnectorParameters parameters = new()
            {
                Id = 1,
                ShortString = "EDT",
                LongString = "Edit",
                ConnectorData = new CommandButtonParameters("SubmitCommand", "Save")
            };
            IMapper mapper = serviceProvider.GetRequiredService<IMapper>();

            // Act
            CommandButtonDescriptor button = mapper.Map<CommandButtonDescriptor>(parameters);

            // Assert
            Assert.Equal(1, button.Id);
            Assert.Equal("EDT", button.ShortString);
            Assert.Equal("Edit", button.LongString);
            Assert.Equal("Save", button.ButtonIcon);
            Assert.Equal("SubmitCommand", button.Command);
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
