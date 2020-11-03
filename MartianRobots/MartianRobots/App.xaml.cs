using Composer;
using Ninject;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace MartianRobots
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        protected override void OnStartup(StartupEventArgs e)
        {

            base.OnStartup(e);
            StandardKernel kernel = new StandardKernel(new NinjectComposer());
            Window VentanaPrincipal = Application.Current.MainWindow;
            VentanaPrincipal = kernel.Get<MartianRobotsMainView>();

            VentanaPrincipal.Show();




        }
    }
}
