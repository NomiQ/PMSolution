using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using AutoMapper;
using PMSolution.Web.Models;
using PMSolution.Web.Services;

namespace PMSolution.Web
{
    public class ContainerConfig
    {
        internal static void RegisterContainer()
        {
            var builder = new ContainerBuilder();

            // register mvc controllers
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            // register appDbContext
            builder.RegisterType<ApplicationDbContext>().InstancePerRequest();

            // register auto mapper profiles
            builder.RegisterAssemblyTypes().AssignableTo(typeof(Profile)).As<Profile>();

            // mapper configuration
            builder.Register(c => new MapperConfiguration(cfg =>
            {
                foreach (var profile in c.Resolve<IEnumerable<Profile>>())
                {
                    cfg.AddProfile(profile);
                }
            })).AsSelf().SingleInstance();

            // resolve as IMapper
            builder.Register(c => c.Resolve<MapperConfiguration>()
                    .CreateMapper(c.Resolve))
                    .As<IMapper>()
                    .InstancePerLifetimeScope();

            // register repositories
            builder.RegisterType<PatientRepository>()
                   .As<IPatientRepository>()
                   .InstancePerRequest();
            builder.RegisterType<StaffRepository>()
                  .As<IStaffRepository>()
                  .InstancePerRequest();



            var container = builder.Build();

            // resolve dependency for mvc
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

        }
    }
}