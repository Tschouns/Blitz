//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Blitz
{
    using System.Windows;
    using Base.StartUp;

    /// <summary>
    /// Interaction logic.
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="App"/> class.
        /// </summary>
        public App()
        {
            this.Startup += this.Application_Startup;
        }

        /// <summary>
        /// Handles the <see cref="Application.Startup"/> event. Initializes all components.
        /// </summary>
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            StartUp.InitializeComponents();
        }
    }
}
