using Sicv1.Domain.Contracts.Repositories;
using Sicv1.Domain.Contracts.Services;
using Sicv1.Domain.Services;
using Sicv1.Infrastructure.Repositories;
using System;
using Unity;

namespace Sicv1.Presentation
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public static class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container =
          new Lazy<IUnityContainer>(() =>
          {
              var container = new UnityContainer();
              RegisterTypes(container);
              return container;
          });

        /// <summary>
        /// Configured Unity Container.
        /// </summary>
        public static IUnityContainer Container => container.Value;
        #endregion

        /// <summary>
        /// Registers the type mappings with the Unity container.
        /// </summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>
        /// There is no need to register concrete types such as controllers or
        /// API controllers (unless you want to change the defaults), as Unity
        /// allows resolving a concrete type even if it was not previously
        /// registered.
        /// </remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below.
            // Make sure to add a Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            // TODO: Register your type's mappings here.
            #region IOC
            container.RegisterType<ICategoryRepository, CategoryRepository>();
            container.RegisterType<ICategoryService, CategoryService>();

            container.RegisterType<IUserRepository, UserRepository>();
            container.RegisterType<IUserService, UserService>();

            container.RegisterType<IScheduleRepository, ScheduleRepository>();
            container.RegisterType<IScheduleService, ScheduleService>();

            container.RegisterType<ICompanyRepository, CompanyRepository>();
            container.RegisterType<ICompanyService, CompanyService>();            

            container.RegisterType<ICategoryCodeQrRepository, CategoryCodeQrRepository>();
            container.RegisterType<ICategoryCodeQrService, CategoryCodeQrService>();

            container.RegisterType<IConfigurationRepository, ConfigurationRepository>();
            container.RegisterType<IConfigurationService, ConfigurationService>();

            container.RegisterType<IMenuRepository, MenuRepository>();
            container.RegisterType<IMenuService, MenuService>();

            container.RegisterType<IRoleRepository, RoleRepository>();
            container.RegisterType<IRoleService, RoleService>();

            container.RegisterType<IUbigeoRepository, UbigeoRepository>();
            container.RegisterType<IUbigeoService, UbigeoService>();

            container.RegisterType<ICategoryCardRepository, CategoryCardRepository>();
            container.RegisterType<ICategoryCardService, CategoryCardService>();

            container.RegisterType<IPopupConfigurationRepository, PopupConfigurationRepository>();
            container.RegisterType<IPopupConfigurationService, PopupConfigurationService>();

            container.RegisterType<INewsNessRepository, NewsNessRepository>();
            container.RegisterType<INewsNessService, NewsNessService>();

            container.RegisterType<INewsNessTypeRepository, NewsNessTypeRepository>();
            container.RegisterType<INewsNessTypeService, NewsNessTypeService>();

			container.RegisterType<IUploadsRepository, UploadsRepository>();
			container.RegisterType<IUploadService, UploadService>();

			container.RegisterType<IDashboardRepository, DashboardRepository>();
			container.RegisterType<IDashboardService, DashboardService>();

			container.RegisterType<INotifyRepository, NotifyRepository>();
			container.RegisterType<INotifyService, NotifyService>();

            container.RegisterType<IRewardsRepository, RewardsRepository>();
            container.RegisterType<IRewardsService, RewardsService>();

            container.RegisterType<ICountryRepository, CountryRepository>();
            container.RegisterType<ICountryService, CountryService>();

            container.RegisterType<IBranchOfficesRepository, BranchOfficesRepository>();
            container.RegisterType<IBranchOfficesService, BranchOfficesService>();

            container.RegisterType<IParameterRepository, ParameterRepository>();
            container.RegisterType<IParameterService, ParameterService>();
            #endregion
        }
    }
}