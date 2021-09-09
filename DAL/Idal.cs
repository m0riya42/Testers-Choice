using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace DAL
{
    public interface Idal
    {
        void addTester(Tester tester);//change
        void deleteTester(string id);
        void UpdateTester(Tester tester);
        int FindTester(string ID);

        void addStudent(Trainee student);
        void deleteStudent(string id);
        void UpdateStudent(Trainee student);
        int FindTrainee(string ID);

        void addTest(Test test);
        void UpdateTest(Test test);
        int FindTest(int testNum);
        void deleteTest(int t);

        void addPassedTest(Test test);

        //functions which returns access to the lists:
        List<Tester> TesterList();          //List<Tester> testers
        List<Trainee> TraineeList();      //List<Trainee> trainees
        List<Test> TestList();       //List<Test> tests
        List<Test> PassedList(); //l


    }
}
