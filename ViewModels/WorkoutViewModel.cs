using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Workout_Application_Tracker.Views;

namespace Workout_Application_Tracker.ViewModels
{
    public class WorkoutViewModel : INotifyPropertyChanged 
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private WorkoutTable _workoutTable;
        public WorkoutTable WorkoutTable
        {
            get { return _workoutTable; }
            set 
            { 
                _workoutTable = value;
                OnPropertyChanged(nameof(WorkoutTable));
            }
        }

        private ObservableCollection<WorkoutTable> _1stWorkoutTable;
        public ObservableCollection<WorkoutTable> LstWorkoutTable
        {
            get { return _1stWorkoutTable; }
            set
            {
                _1stWorkoutTable = value;
                OnPropertyChanged(nameof(LstWorkoutTable));
            }
        }
        private WorkoutTable _selectedWorkout;
        public WorkoutTable SelectedWorkout
        {
            get { return _selectedWorkout; }
            set
            {
                _selectedWorkout = value;
                OnPropertyChanged(nameof(SelectedWorkout));
            }
        }

        public void OpenUpdate()
        {
            UpdateView op = new UpdateView();
            op.Show();
        }

        FitnessDBEntities fitnessDBEntities;
        public WorkoutViewModel()
        {
            fitnessDBEntities = new FitnessDBEntities();
            LoadFitness();
            DeleteCommand = new Command((s) => true, Delete);
        }

        private void Delete(object obj)
        {
            var workout = obj as WorkoutTable;
            fitnessDBEntities.WorkoutTables.Remove(workout);
            fitnessDBEntities.SaveChanges();
            LstWorkoutTable.Remove(workout);
        }

        private void LoadFitness() // Read Details
        {
            LstWorkoutTable = new ObservableCollection<WorkoutTable>(fitnessDBEntities.WorkoutTables);
        }
        public ICommand DeleteCommand { get; set; }
    }

    internal class Command : ICommand
    {
        public Command(Func<object, bool> methodCanExecute, Action<object> methodExecute)
        {
            MethodCanExecute = methodCanExecute;
            MethodExecute = methodExecute;
        }

        Action<object> MethodExecute;
        Func<object, bool> MethodCanExecute;
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
           return MethodExecute!=null && MethodCanExecute.Invoke(parameter);
        }

        public void Execute(object parameter)
        {
            MethodExecute(parameter);
        }
    }
}
