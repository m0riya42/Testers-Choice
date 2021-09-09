using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace BL
{
    public interface IBL
    {
        //tester func
        void checkTesterAge(Tester t);//Check if the tester is beneath to 40 age
        List<Tester> createRec(Trainee s, DateTime Sdate, Address ds); //help to the func check valid tester
        void CheckValidTester(Trainee s, DateTime Sdate, Address ds);//check if there is available tester to the test at the date that set and if the tester has no test in tha hour
                                                                     //אין אפשרות להוסיף מבחן אם אין בוחן זמין בתאריך המבוקש. אם אין המערכת תציע לתלמיד זמן חילופי
                                                                     //כלומר, התלמיד בוחן תאריך שטוב לו, המערכת בודקת אם יש בוחן זמין, אם אין היא מציעה לתלמיד את זמן אחר
                                                                     //הקרוב ביותר לתאריך המבוקש
        List<Tester> CheckNumWeeklyTests(List<Tester> listTesters, DateTime date);//checks if the tester passed the number of lessons per week
        Tester CheckTesterHour(List<Tester> TesterList, DateTime d);//checks if the tester has more than 1 test in a hour
        IEnumerable<Tester> distance(IEnumerable<Tester> listTesters, Address address); //gets address and return  list off available testers
        void Finalresult(Test t);

        //new--->Tester
        IEnumerable<Test> showUnUpdatedTests(Tester sentTester);
        IEnumerable<Test> showPassedTests(Tester sentTester);
        Tester BigAgeTester();





        //trainee func
        void checkTraineeAge(Trainee s);//Check if the student is beneath to 18 age
        void CheckNumLessons(Trainee s);//check if the student did 20 lessons at least
        void CheckDaysToTest(Trainee s, DateTime d);//check if 7 days passed from the last test
        void CheckStudentCar(Trainee s);//Checks if the student has been passed the test on this type of car
        int numOfTests(Trainee s); //gets student, return the number of tests he did.
        void PassedTheTest(Trainee s);//gets student return yes if passed the test
        bool ISFutureTest(Trainee s);//check if there is a future test
        Trainee BigAgeTrainee();//return the biggest trainee
        
        //both:
        IEnumerable<Tester> MatchCarType(Trainee s, List<Tester> listTesters);//Match a tester to the car type of the student
        IEnumerable<object> SpecificFutureTest(Tester sentTester);//return list of tests with count,futere tests of specific tester

        //test func
        void CheckUpdateTest(Test t);//לפי מה שאני הבנתי, זה הפונקציה שצריך לעשות במקום הפונקציה הירוקה למטה
        IEnumerable<Test> testSuitable(IEnumerable<Test> listTest, Predicate<Test> func);//return all the tests that suits the terms in the func
        IEnumerable<Test> SortedTestAccordingDay(IEnumerable<Test> listTest, DateTime Rdate); //return all the test according the day
        IEnumerable<Test> SortedTestAccordingMonth(IEnumerable<Test> listTest, DateTime Rdate); //return all the test according the month
        IEnumerable<Test> ListByCriterion(int choice);
        IEnumerable<Test> PassedTests();
        IEnumerable<Test> NotPassedTests();



        //Grouping:
        IEnumerable<IGrouping<int, Tester>> GroupTestersBySeniority(bool sort = false);// group testers by their seniority
        IEnumerable<IGrouping<CarType, Tester>> GroupTestersByCarType(bool sort = false);// group by name
        IEnumerable<IGrouping<string, Tester>> GroupTestersByName(bool sort = false);// group testers by their cartype 


        IEnumerable<IGrouping<string, Trainee>> GroupTraineesByDriSchool(bool sort = false);//group students by their drive school
        IEnumerable<IGrouping<string, Trainee>> GroupTraineesByDriveTeacher(bool sort = false);//group students by their drive teacher
        IEnumerable<IGrouping<int, Trainee>> GroupTraineesByNumOfTests(bool sort = false);//group students by the number of the lessons they did  
        IEnumerable<IGrouping<Gender, Trainee>> GroupTraineesByGender(bool sort = false);//group by gender
        IEnumerable<IGrouping<String, Trainee>> GroupTraineesByName(bool sort = false);//group by name
        IEnumerable<IGrouping<Gearbox, Trainee>> GroupTraineesByGearbox(bool sort = false);//group by gearbox

        IEnumerable<IGrouping<DateTime, Test>> GroupTestByIsDate(Tester sentTester, IEnumerable<Test> p);//
        IEnumerable<IGrouping<int, Test>> GroupTestByNumTest(Tester sentTester, IEnumerable<Test> p);//
        IEnumerable<IGrouping<DateTime, Test>> GroupTestsByDate(bool sort = true);
        IEnumerable<IGrouping<CarType, Test>> GroupTestByCarType(bool sort = false);
        IEnumerable<IGrouping<bool, Test>> GroupTestByIsPassed(bool sort = false);


        //Get:
        List<Tester> getTesters();
        List<Test> getTests();
        List<Trainee> getTrainnes();
        List<Test> getPassedTests();
        List<Test> getAllTest();


        // passwordFunc:
        PasswordScore CheckStrength(string password);

        //-->statistics func
        int[] StaticTest(Test t);

        #region Idal func

        //Tester:
        void addTester(Tester t);//, bool[,] n_WorkDay
        void deleteTester(string id);
        void UpdateTester(Tester t);
        int FindTester(string ID);

        //student:
        void addStudent(Trainee t);
        void deleteStudent(string IdStudent);
        int FindTrainee(string ID);
        void UpdateStudent(Trainee t);

        //tests:
        void addTest(string id, DateTime date, Address address);
        void UpdateTest(Test t);
        int FindTest(int testNum);
        void deleteTest(int numTest);
        #endregion

      
      


       

       

    }
}
