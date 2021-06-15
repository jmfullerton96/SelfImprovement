using NHibernate;
using NHibernate.Cfg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;

namespace SelfImprovement.Models
{
    public class NHibernateSession
    {
        public static NHibernate.ISession OpenSession()
        {
            var configuration = new NHibernate.Cfg.Configuration();
            /*var configurationPath = HttpContext.Current.Server.MapPath(@"~\Models\Nhibernate\hibernate.cfg.xml");
            configuration.Configure(configurationPath);
            var tasksConfigurationFile = HttpContext.Current.Server.MapPath(@"~\Models\NHibernate\Tasks.hbm.xml");
            configuration.AddFile(tasksConfigurationFile);*/
            configuration.AddAssembly(Assembly.GetCallingAssembly());

            ISessionFactory sessionFactory = configuration.BuildSessionFactory();

            return sessionFactory.OpenSession();
        }
    }
}