using System;
using System.Web;
using AutogearWeb.Controllers;
using AutogearWeb.Models;
using AutogearWeb.Repositories;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Microsoft.Practices.Unity;

namespace AutogearWeb
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public class UnityConfig
    {
        #region Unity Container
        private static readonly Lazy<IUnityContainer> Container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        /// <summary>
        /// Gets the configured Unity container.
        /// </summary>
        public static IUnityContainer GetConfiguredContainer()
        {
            return Container.Value;
        }
        #endregion

        /// <summary>Registers the type mappings with the Unity container.</summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>There is no need to register concrete types such as controllers or API controllers (unless you want to 
        /// change the defaults), as Unity allows resolving a concrete type even if it was not previously registered.</remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below. Make sure to add a Microsoft.Practices.Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            // TODO: Register your types here
            // container.RegisterType<IProductRepository, ProductRepository>();
            container.RegisterType<ApplicationDbContext>();
            container.RegisterType<ApplicationUserManager>();
            container.RegisterType<ApplicationSignInManager>();
            container.RegisterType<IAuthenticationManager>(
                new InjectionFactory(c => HttpContext.Current.GetOwinContext().Authentication));
            container.RegisterType<IUserStore<ApplicationUser>, UserStore<ApplicationUser>>(
               new InjectionConstructor(typeof(ApplicationDbContext)));
            // GlobalConfiguration.Configuration.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(container);
            container.RegisterType<IRoleStore<IdentityRole, string>, RoleStore<IdentityRole>>(
            new InjectionConstructor(typeof(ApplicationDbContext)));

            container.RegisterType<IInstructorRepo, InstructorRepo>();
            container.RegisterType<IStudentRepo, StudentRepo>();
            container.RegisterType<IPostalRepo, PostalRepo>();
            container.RegisterType<IAutogearRepo, AutogearRepo>();
            container.RegisterType<AccountController>(
                new InjectionConstructor(
                    typeof(ApplicationUserManager),
                    typeof(ApplicationSignInManager),
                    typeof(ApplicationRoleManager)));
            
        }
    }
}
