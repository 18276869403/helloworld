using Autofac;
using Autofac.Integration.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Configuration;
using System.Web.Mvc;

namespace MVCMedicine.App_Start
{
    public class AutofacConfig
    {
        /**
         * IOC即控制反转，一种将控制权转移的设计模式，由传统的程序控制转移到容器控制；
         * DI即依赖注入，将相互依赖的对象分离，这些依赖关系也只在使用时才被建立。
         * AOP面向切面，OOP面向对象
         */
        #region Autofac组件 是将IOC 和DI即控制反转和依赖注入
        public static void MyAutofacConfig()
        {
            //构造一个AutoFac的builder容器
            ContainerBuilder builder = new ContainerBuilder();
            //注册当前程序集的所有Controllers
            builder.RegisterControllers(typeof(MvcApplication).Assembly).PropertiesAutowired();

            //通过配置文件来修改功能
            string AutofacName = ConfigurationManager.AppSettings["Autofac"];
            Assembly Service = Assembly.Load(AutofacName);

            //以接口形式保存被创建类的对象实例，
            //PropertiesAutowired表示：如果实现类中也使用了的接口，实现类中的接口也会被注入（前提是，该对象必须是Autofac创建的）
            builder.RegisterAssemblyTypes(Service).Where(u => !u.IsAbstract).AsImplementedInterfaces().PropertiesAutowired();

            //创建一个真正的Autofac的工作容器
            var container = builder.Build();

            //移除原本的Mvc的容器使用Autofac的容器
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
        #endregion
    }
}