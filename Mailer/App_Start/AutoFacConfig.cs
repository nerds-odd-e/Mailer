using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Mailer.Services;

namespace Mailer
{
    public static class AutofacConfig
    {
        public static IContainer Container { get; set; }

        public static void RegisterConfig()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<SmtpClientWrapper>().As<ISmtpClient>().PropertiesAutowired().SingleInstance();
            builder.RegisterControllers(typeof(MvcApplication).Assembly).PropertiesAutowired();
            Container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(Container));
        }
    }
}