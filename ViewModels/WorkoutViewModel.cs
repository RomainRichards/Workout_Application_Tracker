using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Migrations;
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

        private ObservableCollection<WorkoutTable> _lstWorkoutTable;
        public ObservableCollection<WorkoutTable> LstWorkoutTable
        {
            get { return _lstWorkoutTable; }
            set
            {
                _lstWorkoutTable = value;
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

        // Get combobox data for ab workouts.
        private ObservableCollection<Ab> _abCollection;
        public ObservableCollection<Ab> AbCollection
        {
            get { return _abCollection; }
            set
            {
                _abCollection = value;
                OnPropertyChanged(nameof(AbCollection));
            }
        }

        private ObservableCollection<LowerBody> _lowerBodyCollection;
        public ObservableCollection<LowerBody> LowerBodyCollection
        {
            get { return _lowerBodyCollection; }
            set
            {
                _lowerBodyCollection = value;
                OnPropertyChanged(nameof(LowerBodyCollection));
            }
        }

        private ObservableCollection<UpperBody> _upperBodyCollection;
        public ObservableCollection<UpperBody> UpperBodyCollection
        {
            get { return _upperBodyCollection; }
            set
            {
                _upperBodyCollection = value;
                OnPropertyChanged(nameof(UpperBodyCollection));
            }
        }

        FitnessDBEntities fitnessDBEntities;
        public WorkoutViewModel()
        {
            fitnessDBEntities = new FitnessDBEntities();
            LoadFitness();
            LoadAb();
            LoadLowerBody();
            LoadUpperBody();
            DeleteCommand = new Command((s) => true, Delete);
            UpdateCommand = new Command((s) => true, Update);
            UpdateWorkoutCommand = new Command((s) => true, UpdateWorkout);

        }

        

        private void LoadAb()
        {
            AbCollection = new ObservableCollection<Ab>(fitnessDBEntities.Abs);
        }

        private void LoadLowerBody()
        {
            LowerBodyCollection = new ObservableCollection<LowerBody>(fitnessDBEntities.LowerBodies);
        }
        private void LoadUpperBody()
        {
            UpperBodyCollection = new ObservableCollection<UpperBody>(fitnessDBEntities.UpperBodies);
        }
        private void UpdateWorkout(object obj)
        {
            fitnessDBEntities.SaveChanges();
        }

        private void Update(object obj)
        {
            SelectedWorkout = obj as WorkoutTable;

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
        public ICommand UpdateCommand { get; set; }
        public ICommand UpdateWorkoutCommand { get; set; }
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
