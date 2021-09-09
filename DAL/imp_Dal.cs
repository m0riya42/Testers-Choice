using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;


namespace DAL
{
    internal class imp_Dal : Idal
    {
        public static List<Tester> testers = new List<Tester>();
        public static List<Trainee> trainees = new List<Trainee>();
        public static List<Test> tests = new List<Test>();
        public static List<Test> passedTests = new List<Test>();

        public imp_Dal()
        {

            //Test t = new Test("987654321", "112345678", new DateTime(2018, 11, 24, 9, 0, 0), new Address { city = "Rishon lezion", street = "Rimon", BuildNum = 54 });//keep in future to check the func
            //Test p = new Test("312465234", "055565423", new DateTime(2018, 12, 30, 10, 0, 0), new Address { city = "Rishon5 lezion", street = "Rim5on", BuildNum = 564 });//keep in future to check the func
            //Test pt = new Test("312465234", "055565423", new DateTime(2019, 2, 23, 10, 0, 0), new Address { city = "Rishon5 lezion", street = "Rim5on", BuildNum = 564 });
            //Test pts = new Test("312465234", "055565423", new DateTime(2019, 3, 7, 10, 0, 0), new Address { city = "Rishon5 lezion", street = "Rim5on", BuildNum = 564 });
            //Test ptst = new Test("312465234", "055565423", new DateTime(2018, 3, 7, 9, 0, 0), new Address { city = "Rishon5 lezion", street = "Rim5on", BuildNum = 564 });
            //ptst.IsTestUpdate = true;
            //ptst.FinalOutcome = true;
            //Test ptsts = new Test("312465234", "055565423", new DateTime(2018, 11,24, 10, 0, 0), new Address { city = "Rishon5 lezion", street = "Rim5on", BuildNum = 564 });

            //Test ptstsp = new Test("312465234", "055565423", new DateTime(2018, 12, 30, 9, 0, 0), new Address { city = "Rishon5 lezion", street = "Rim5on", BuildNum = 564 });
            //ptstsp.IsTestUpdate = true;

            //ptst.details = new TestDetails() { KeptDistance = true, reverseParking = false, mirrors = true, speed = false, signal = false, EnterToJuction = true, TesterInvolved = false, PrepareToDrive = false };
            //ptstsp.details = new TestDetails() { KeptDistance = false, reverseParking = true, mirrors = false, speed = true, signal = false, EnterToJuction = false, TesterInvolved = true, PrepareToDrive = true };

            //Test ptstspoo = new Test("312465234", "055565423", new DateTime(2018, 12, 30, 9, 0, 0), new Address { city = "Rishon5 lezion", street = "Rim5on", BuildNum = 564 });
            //ptstspoo.IsTestUpdate = true;
            //ptstspoo.details = new TestDetails() { KeptDistance = true, reverseParking = true, mirrors = false, speed = false, signal = false, EnterToJuction = true, TesterInvolved = true, PrepareToDrive = false };

            //tests.Add(t);
            //tests.Add(p);
            //bool[,] keep = new bool[7, 5] { { true, false, true, true, false }, { false, true, false, true, true }, { false, true, false, true, false }, { true, false, true, false, true }, { true, true, false, false, false }, { true, false, false, true, true }, { true, false, false, true, false } };
            //testers.Add(new Tester { Id = "987654321", Password="987654321", Birthday = new DateTime(1978, 2, 1), Name = "israel", LName = "israely", GenderType = Gender.male, CallNum = "0589766597", address = new Address { city = "Ashqelon", street = "Haavoda", BuildNum = 2 }, WorkDay = keep, car = CarType.TwoWheel, MaxDistance = 15, MaxTests = 15, seniority = 1,  futureTests = { pt, pts, t, p}, MyTests = { ptst, ptsts, ptstsp } });
            //testers.Add(new Tester { Id = "312435234", Birthday = new DateTime(1975, 10, 5), Name = "rafi", LName = "levi", GenderType = Gender.male, CallNum = "0546325576", address = new Address { city = "Ashdod", street = "Avot yeshurun", BuildNum = 1 }, WorkDay = keep, car = CarType.MedTruck, MaxDistance = 20, MaxTests = 30, seniority = 4, futureTests = new List<Test>(), MyTests = new List<Test>() });

            //keep = new bool[7, 5] { { true, false, false, false, false }, { true, false, false, true, true }, { true, true, true, true, true }, { true, false, false, true, true }, { true, true, true, true, true }, { true, false, false, false, true }, { false, false, false, false, true } };
            //testers.Add(new Tester { Id = "567345345", Birthday = new DateTime(1969, 9, 7), Name = "shalom", LName = "cohen", GenderType = Gender.male, CallNum = "0532876574", address = new Address { city = "Kiryat Malachi", street = "hamalachim", BuildNum = 15 }, WorkDay = keep, car = CarType.HeavyTruck, MaxDistance = 12, MaxTests = 30, seniority = 6, futureTests = new List<Test>(), MyTests = new List<Test>() });

            //keep = new bool[7, 5] { { true, false, false, true, true }, { true, false, false, true, true }, { true, false, false, true, true }, { true, false, false, true, true }, { true, false, false, true, true }, { true, false, false, true, true }, { true, false, false, true, true } };
            //testers.Add(new Tester { Id = "678090876", Birthday = new DateTime(1972, 12, 13), Name = "nahum", LName = "kala", GenderType = Gender.male, CallNum = "0589087654", address = new Address { city = "Jerushalem", street = "haatsmaut", BuildNum = 6 }, WorkDay = keep, car = CarType.privateCar, MaxDistance = 15, MaxTests = 30, seniority = 7, futureTests = new List<Test>(), MyTests = new List<Test>() });
            //testers.Add(new Tester { Id = "122344455", Password= "122344455", Birthday = new DateTime(1970, 9, 25), Name = "david", LName = "bibi", GenderType = Gender.male, CallNum = "0556115647", address = new Address { city = "Jerushalem", street = "Ben Zion", BuildNum = 9 }, WorkDay = keep, car = CarType.TwoWheel, MaxDistance = 20, MaxTests = 30, seniority = 5, futureTests = new List<Test>(), MyTests = new List<Test>() });
            //testers.Add(new Tester { Id = "123456789", Birthday = new DateTime(1978, 4, 15), Name = "moshe", LName = "cohen", GenderType = Gender.male, CallNum = "0506111451", address = new Address { city = "Jerushalem", street = "ben gurion", BuildNum = 7 }, WorkDay = keep, car = CarType.privateCar, MaxDistance = 10, MaxTests = 30, seniority = 1, futureTests = new List<Test>(), MyTests = new List<Test>() });

            //trainees.Add(new Trainee { Id = "112345678", Password="112345678", Name = "yehuda", LName = "nun", Birthday = new DateTime(1994, 6, 18), CallNum = "0587626586", GenderType = Gender.male, address = new Address { city = "Ashqelon", street = "rabin", BuildNum = 9 }, car = CarType.HeavyTruck, gearbox = Gearbox.Manual, NumLessons = 21, SchoolName = "lamed", TeacherName = "Moshe cohen", MyTests = { t} });
            //trainees.Add(new Trainee { Id = "314256435", Password= "314256435", Name = "Arik", LName = "Levi", Birthday = new DateTime(1997, 6, 18), CallNum = "0587626586", GenderType = Gender.male, address = new Address { city = "Ashdod", street = "Even Ezra", BuildNum = 12 }, car = CarType.MedTruck, gearbox = Gearbox.Auto, NumLessons = 25, SchoolName = "lamed", TeacherName = "rafi miara", MyTests = { ptst, ptstsp, ptstspoo } });
            //trainees.Add(new Trainee { Id = "325678456", Name = "Michal", LName = "Shalem", Birthday = new DateTime(1999, 6, 18), CallNum = "0587626586", GenderType = Gender.female, address = new Address { city = "Jerushalem", street = "Najara", BuildNum = 13 }, car = CarType.privateCar, gearbox = Gearbox.Auto, NumLessons = 15, SchoolName = "lamed", TeacherName = "Shalom cohen", MyTests = new List<Test>() });
            //trainees.Add(new Trainee { Id = "876543645", Name = "Michal", LName = "kuk", Birthday = new DateTime(1998, 6, 18), CallNum = "0587626586", GenderType = Gender.female, address = new Address { city = "Jerushalem", street = "Bet Hadfus", BuildNum = 7 }, car = CarType.TwoWheel, gearbox = Gearbox.Auto, NumLessons = 18, SchoolName = "OR Yrok", TeacherName = "moty dahan", MyTests = new List<Test>() });
            //trainees.Add(new Trainee { Id = "098765423", Name = "Shimon", LName = "mor", Birthday = new DateTime(2000, 6, 18), CallNum = "0587626586", GenderType = Gender.male, address = new Address { city = "Rishon lezion", street = "Rimon", BuildNum = 54 }, car = CarType.MedTruck, gearbox = Gearbox.Manual, NumLessons = 20, SchoolName = "lamed", TeacherName = "rafi levi", MyTests = new List<Test>() });
            //trainees.Add(new Trainee { Id = "062565423", Name = "bracha", LName = "mor", Birthday = new DateTime(2000, 6, 18), CallNum = "0586236586", GenderType = Gender.female, address = new Address { city = "Rishon lezion", street = "Rimon", BuildNum = 54 }, car = CarType.MedTruck, gearbox = Gearbox.Auto, NumLessons = 20, SchoolName = "lamed", TeacherName = "rafi levi", MyTests = new List<Test>() });

        }
        #region TesterFunctions
        public int FindTester(string ID)
        {
            for (int i = 0; i < testers.Count; i++)
                if (testers[i].Id == ID)
                    return i;
            throw new NotImplementedException("The tester is not found in the testers list. ");
        }
        public void addTester(Tester tester)
        {

            bool b = false;
            for (int i = 0; i < testers.Count; i++)
                if (testers[i].Id == tester.Id)
                    b = true;
            if (b)//true
                throw new NotImplementedException(" the tester is already exist in the tester list.");
            testers.Add(tester);
        }
        public void deleteTester(string id)
        {
            int i = FindTester(id);
            testers.Remove(testers[i]);
        }
        public void UpdateTester(Tester tester)
        {
            int index = FindTester(tester.Id);
            testers[index] = tester;
        }


        #endregion

        #region StudentFunctions
        public int FindTrainee(string ID)
        {
            for (int i = 0; i < trainees.Count; i++)
            {
                if (trainees[i].Id == ID)
                    return i;
            }
            throw new NotImplementedException(" the trainee is not exist in the trainees list. ");
        }
        public void addStudent(Trainee student)
        {
            bool b = false;
            for (int i = 0; i < testers.Count; i++)
                if (testers[i].Id == student.Id)
                    b = true;
            if (b)//true
                throw new NotImplementedException(" the trainee is already exist in the trainees list. ");
            trainees.Add(student);
        }
        public void deleteStudent(string id)
        {
            int index = FindTrainee(id);
            trainees.Remove(trainees[index]);
        }
        public void UpdateStudent(Trainee student)
        {
            int index = FindTrainee(student.Id);
            trainees[index] = student;
        }
        #endregion

        #region TestFunction
        public int FindTest(int testNum)
        {
            for (int i = 0; i < TestList().Count; i++)
            {
                if (TestList()[i].numberTest == testNum)
                    return i;
            }
            throw new NotImplementedException(" the test is not exist in the tests list. ");
        }
        public void addTest(Test test)
        {
            tests.Add(test);
        }
        public void UpdateTest(Test test)
        {
            //update the test in the tests list
            int index = FindTest(test.numberTest);//trow in the find
            tests[index] = test;

            //update the test in the tester tests list
            index = FindTester(test.TesterId);
            int i = 0;
            foreach (Test t in testers[index].futureTests)
            {
                if (t.numberTest == test.numberTest)
                    break;
                i++;//the index of the test in the tester tests list
            }
            testers[index].futureTests[i] = test;

            index = FindTrainee(test.StudentId);
            i = 0;
            foreach (Test t in trainees[index].MyTests)
            {
                if (t.numberTest == test.numberTest)
                    break;
                i++;
            }
            trainees[index].MyTests[i] = test;
        }

        public void deleteTest(int numTst)
        {
            int index = FindTest(numTst);//test[]
            Test test = tests[index];
            testers[FindTester(test.TesterId)].futureTests.Remove(test);//delete from tester
            trainees[FindTrainee(test.StudentId)].MyTests.Remove(test);//delete from trainee
            tests.Remove(test);
        }
        #endregion
        public void addPassedTest(Test test)
        {
            //LoadDataPassedTest();//load data
            //test.StartTest = new Address() { street = test.StartTest.street + "/", BuildNum = test.StartTest.BuildNum, city = "/" + test.StartTest.city };
            //passedtestRoot.Add(ConvertTest(test));
            //passedtestRoot.Save(PassedTestPath);
        }
        #region AccessToTheLists
        public List<Tester> TesterList()
        {
            return testers;
        }
        public List<Trainee> TraineeList()
        {
            return trainees;
        }
        public List<Test> TestList()
        {
            return tests;
        }
        public List<Test> PassedList()
        {
            return passedTests;
        }
        #endregion
    }
}