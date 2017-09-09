
namespace BlitzDemoGunBoat
{
    using System;
    using Base.StartUp;

    /// <summary>
    /// The program.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// The famous "main".
        /// </summary>
        [STAThread]
        public static void Main()
        {
            StartUp.InitializeComponents();

            var demo = new DemoGame();
            demo.Run();
        }
    }
}
