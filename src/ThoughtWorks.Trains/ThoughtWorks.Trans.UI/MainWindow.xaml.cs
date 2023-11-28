using System;
using System.Text;
using System.Windows;

namespace ThoughtWorks.Trans.UI
{
    using ThoughtWorks.Trains.Application;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.InitValues();
        }

        private void InitValues()
        {
            // Define the default routes.
            this.txtRoutes.Text = "AB5, BC4, CD8, DC8, DE6, AD5, CE2, EB3, AE7";

            // Define the default dsl.
            var input = new StringBuilder();
            input.AppendLine( "1. The distance of the route A-B-C." );
            input.AppendLine( "2. The distance of the route A-D." );
            input.AppendLine( "3. The distance of the route A-D-C." );
            input.AppendLine( "4. The distance of the route A-E-B-C-D." );
            input.AppendLine( "5. The distance of the route A-E-D." );
            input.AppendLine( "6. The number of trips starting at C and ending at C with a maximum of 3 stops." );
            input.AppendLine( "7. The number of trips starting at A and ending at C with exactly 4 stops." );
            input.AppendLine( "8. The length of the shortest route (in terms of distance to travel) from A to C." );
            input.AppendLine( "9. The length of the shortest route (in terms of distance to travel) from B to B." );
            input.AppendLine( "10. The number of different routes from C to C with a distance of less than 30." );
            this.txtInputDsl.Text = input.ToString();
        }

        private void btnProcess_Click( object sender, RoutedEventArgs e )
        {
            try
            {
                var dsls = Regex.Split( this.txtInputDsl.Text.Trim(), "\r\n" );
                this.txtOutput.Text = GetRailroadService().RunRouteOperations( this.txtRoutes.Text, dsls );
            }
            catch( Exception err )
            {
                MessageBox.Show( "Inuspected error in the application, Error: " + err.Message );
            }
        }

        private static RailroadService GetRailroadService()
        {
            return RailroadService.Instance;
        }
    }
}
