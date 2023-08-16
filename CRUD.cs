using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Workout_Application_Tracker
{
    interface CRUD // Use interface to setup the rules, no implementation.
    {

        int NextWorkoutId();  // ID auto generation.
        void AddWorkout(WorkoutTable wrkTable); // Create workout.
        void DeleteWorkout(WorkoutTable wrkTable); // Delete workout.
        ICollection<WorkoutTable> GetAll(); // Read workout.
        WorkoutTable FindWorkout(decimal id); // Find workout with id. 
        void UpdateWorkout(decimal id, WorkoutTable wrkTable); // Update workout.

        ICollection<Ab> GetAbs();
        ICollection<LowerBody> GetLowerBody();
        ICollection<UpperBody> GetUpperBody();
    }

    class FitnessRepository : CRUD
    {
        FitnessDBEntities entities; // Work with database, like a list. 

        public FitnessRepository() // Constructor.
        {
            entities = new FitnessDBEntities();
        }

        public void AddWorkout(WorkoutTable wrkTable)
        {
            entities.WorkoutTables.Add(wrkTable);
            entities.SaveChanges();
        }

        public void DeleteWorkout(WorkoutTable wrkTable)
        {
            entities.WorkoutTables.Remove(wrkTable);
            entities.SaveChanges();
        }

        public WorkoutTable FindWorkout(decimal id)
        {
            return entities.WorkoutTables.Find(id);
        }

        public ICollection<Ab> GetAbs()
        {
            return entities.Abs.ToList();
        }

        public ICollection<WorkoutTable> GetAll()
        {
            return entities.WorkoutTables.ToList();
        }

        public ICollection<LowerBody> GetLowerBody()
        {
            return entities.LowerBodies.ToList();
        }

        public ICollection<UpperBody> GetUpperBody()
        {
            return entities.UpperBodies.ToList();
        }

        public int NextWorkoutId()
        {
            return Convert.ToInt32(entities.WorkoutTables.Max(p=>p.WorkoutID)); // Converts the decimal to int. 
        }

        public void UpdateWorkout(decimal id, WorkoutTable wrkTable)
        {
            var workoutToUpdate = entities.WorkoutTables.Find(id); // ref of workout to update.
            workoutToUpdate.WorkoutID = wrkTable.WorkoutID;
            workoutToUpdate.UpperBodyID = wrkTable.UpperBodyID;
            workoutToUpdate.Sets___Reps1 = wrkTable.Sets___Reps1;
            workoutToUpdate.Weight1 = wrkTable.Weight1;
            workoutToUpdate.LowerBodyID = wrkTable.LowerBodyID;
            workoutToUpdate.Sets___Reps2 = wrkTable.Sets___Reps2;
            workoutToUpdate.Weight2 = wrkTable.Weight2;
            workoutToUpdate.AbsID = wrkTable.AbsID;
            workoutToUpdate.Sets___Reps_3 = wrkTable.Sets___Reps_3;
            workoutToUpdate.Weight_3 = wrkTable.Weight_3;
            entities.SaveChanges();
        }
    }
}
