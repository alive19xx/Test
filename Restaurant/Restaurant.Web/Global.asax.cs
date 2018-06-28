using System.Data.Entity.Migrations;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Restaurant.Web.AutomapperProfiles;

namespace Restaurant.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            UpdateDatabase();
            AutoMapper.Mapper.Initialize(cfg => cfg.AddProfile<AutomapperWebProfile>());
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);


        }

        private void UpdateDatabase()
        {
            var migrator = new DbMigrator(new DataAccess.Migrations.Configuration());
            migrator.Update();
        }
    }
}
    
