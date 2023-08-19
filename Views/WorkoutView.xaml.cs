using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Workout_Application_Tracker.ViewModels;

namespace Workout_Application_Tracker
{
    /// <summary>
    /// Interaction logic for WorkoutView.xaml
    /// </summary>
    public partial class WorkoutView : Window
    {
        public WorkoutView()
        {
            InitializeComponent();
            DataContext = new WorkoutViewModel();
        }


    }
}
