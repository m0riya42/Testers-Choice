using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Windows.Input;
using System.IO;
using System.Net;
using System.Xml;
using BE;
using DAL;
using System.ComponentModel;
using System.Threading;

//delete func much car
namespace BL
{
    public class imp_IBL : IBL
    {
        //Access to the data Files
        DAL.Idal dal;
       public static double dis;//keep distance to MapReqest

       public static List<BE.Address> keepAdd = new List<Address>();// keep Address for MapReqest
        static CountdownEvent _countdown;// count Thread to fininish;

        public imp_IBL()
        {
            dal = DAL.FactoryDal.getDal();
        }


        

        //tester func:
        public void checkTesterAge(Tester t)//Check if the tester is beneath to 40 age
        {
            DateTime N = DateTime.Now;
            if ((N - t.Birthday).Days > (40 * 365))
                return;
            throw new NotImplementedException("Testers's age cant be beneath to 40 years");

        }
        public List<Tester> createRec(Trainee s, DateTime Sdate, Address ds)
        {
            if (ISFutureTest(s))
                throw new Exception("it is not possible to register for 2 tests!");
            var TempList = dal.TesterList().FindAll(tester => tester.car == s.car);
            CheckDaysToTest(s, Sdate);//check if 7 days passed from the last test
            CheckNumLessons(s);//check if the student did 20 lessons at least
            PassedTheTest(s); //Checks if the student has been passed the test on this type of car
            TempList = CheckNumWeeklyTests(TempList, Sdate);

            TempList = (List<Tester>)distance(TempList, ds);
            return TempList;
        }
        public void CheckValidTester(Trainee s, DateTime Sdate, Address ds)//check if there is available tester to the test at the date that set and if the tester has no test in that hour
        {
            List<Tester> TempList = createRec(s, Sdate, ds);
            if (TempList.Count == 0)
                throw new Exception("there is no vailable taster for you");

            Tester tester = CheckTesterHour(TempList, Sdate);
            while (tester == null) //we try another date to test
            {
                if (Sdate.Hour == 15)
                {
                    if (Sdate.DayOfWeek == DayOfWeek.Thursday)
                    {
                        Sdate = Sdate.AddDays(2);
                    }

                    Sdate = Sdate.AddHours(18);////////////////////////////////////////////////////אמן יעבוד
                }
                else
                    Sdate = Sdate.AddHours(1);
                tester = CheckTesterHour(TempList, Sdate);
            }

            Test t = new Test(tester.Id, s.Id, Sdate, ds, s.car);
            dal.addTest(t);

          
            Tester te = dal.TesterList()[FindTester(tester.Id)];
            te.futureTests.Add(t);
            dal.UpdateTester(te);

            Trainee tr = dal.TraineeList()[FindTrainee(s.Id)];
            tr.MyTests.Add(t);
            dal.UpdateStudent(tr);
        }
        public List<Tester> CheckNumWeeklyTests(List<Tester> listTesters, DateTime d)//checks if the tester passed the number of lessons per week//--> return true if he still hasent passed the max num of lessons
        {
            foreach (Tester t in listTesters)
            {
                if (t.futureTests.Count != 0)
                {
                    DateTime sun = d;
                  
                    while (sun.DayOfWeek != DayOfWeek.Sunday) //keep the last sunday
                        sun = sun.AddDays(-1);
                  
                    int TestCouter = 0;
                    

                    for (int i = 0; i < t.futureTests.Count(); i++)
                    {
                       
                        if ((t.futureTests[i].DateAndHour.Date >= sun) && (t.futureTests[i].DateAndHour.Date <= sun.AddDays(4)))
                            TestCouter++;
                    }

                    for (int i = 0; i < t.MyTests.Count(); i++)
                    {

                        if ((t.MyTests[i].DateAndHour.Date >= sun) && (t.MyTests[i].DateAndHour.Date <= sun.AddDays(4)))
                            TestCouter++;
                    }
                    

                    if (TestCouter > t.MaxTests)
                        listTesters.Remove(t);//if the tester passed the weekly number of teset remove it from the list
                                              // throw new NotImplementedException("The tester passed the max weekly number of tests");
                }

            }
            return listTesters;
        }
        public Tester CheckTesterHour(List<Tester> listTesters, DateTime p)//checks if the tester has more than 1 test in a hour
        {
            bool flag = false;
            if (listTesters != null)
            {
                foreach (Tester n in listTesters)//עוברים עך כל הטסטרים ולכל טסטר עוברים על רשימת המבחנים העתידיים אם מצאנו מבחן שהתאריך והשעה שלו זהים אז נמחק את הטסטר מהרשימה כי הוא לא רלוונטי לנו.
                {
                    if (n.futureTests.Count != 0)
                    {
                        foreach (Test t in n.futureTests)
                        {
                            if (t.DateAndHour == p)
                            {
                                flag = true;//if tis tester has a test at this time flag=true 
                                            //listTesters.Remove(n);
                                            //break;
                            }
                        }
                    }
                    if (flag == false)//if flag=false it mean that the tester doesnt have a test in tha time so return the tester
                    {
                        if (n.WorkDay[(int)p.Hour - 9, (int)p.DayOfWeek - 1])
                            return n;
                    }
                }
            }
            return null;

        }

        public static void ConnectToMapRequest()//the thread the connect to map request
        {

            double result;

            List<BE.Address> addr = keepAdd;

            string origin = addr[0].street + " " + addr[0].BuildNum + " st. " + addr[0].city;
            string destination = addr[1].street + " " + addr[1].BuildNum + " st. " + addr[1].city;
            String KEY = @"NWTWMy94XndgECSnbG4BGAvGIOLkGhwB";// moriya code

            string url = @"https://www.mapquestapi.com/directions/v2/route" +
            @"?key=" + KEY +
            @"&from=" + origin +
            @"&to=" + destination +
            @"&outFormat=xml" +
            @"&ambiguities=ignore&routeType=fastest&doReverseGeocode=false" +
            @"&enhancedNarrative=false&avoidTimedConditions=false";
            //request from MapQuest service the distance between the 2 addresses
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            WebResponse response = request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader sreader = new StreamReader(dataStream);
            string responsereader = sreader.ReadToEnd();
            response.Close();
            //the response is given in an XML format
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.LoadXml(responsereader);

            if (xmldoc.GetElementsByTagName("statusCode")[0].ChildNodes[0].InnerText == "0")
            //we have the expected answer
            {
                //display the returned distance
                XmlNodeList distance = xmldoc.GetElementsByTagName("distance");
                double distInMiles = Convert.ToDouble(distance[0].ChildNodes[0].InnerText);
                // Console.WriteLine("Distance In KM: " + distInMiles * 1.609344);

                result = (distInMiles * 1.609344);

             
                dis = (double)result; //convert miles
                                

            }
            else if (xmldoc.GetElementsByTagName("statusCode")[0].ChildNodes[0].InnerText == "402")
                dis = -1;
            _countdown.Signal();


        }


        public IEnumerable<Tester> distance(IEnumerable<Tester> listTesters, Address address)
        {
                List<Tester> inDistance = new List<Tester>();
                Random rand = new Random();
              keepAdd.Add(address);

            foreach (Tester n in listTesters)
                {
                keepAdd.Add(n.address);

                _countdown = new CountdownEvent(1);

                
                    new Thread(ConnectToMapRequest).Start();
                 
                    _countdown.Wait();   // Blocks until Signal has been called 1 time

                if (dis==-1)
                    dis = rand.Next(50);

                    if (dis <= n.MaxDistance)
                        inDistance.Add(n);

                keepAdd.Remove(n.address);
                }
          
            return inDistance;
        }

        //new--->Tester
        public IEnumerable<Test> showUnUpdatedTests(Tester sentTester)
        {
            var sortedTests = from t in sentTester.futureTests
                              where (t.DateAndHour <= DateTime.Now)
                              select t;
            return sortedTests;
        }
        public IEnumerable<Test> showPassedTests(Tester sentTester)
        {
            var sortedTests = from t in sentTester.MyTests
                              where (t.FinalOutcome == true)
                              select t;
            return sortedTests;
        }
        public Tester BigAgeTester()
        {
            int max = 0;
            Tester keep = new Tester();

            foreach (Tester t in dal.TesterList())
            {
                if ((DateTime.Now.Year) - (t.Birthday.Year) > max)
                {
                    max = (DateTime.Now.Year) - (t.Birthday.Year);
                    keep = t;
                }
            }
            return keep;
        }
        public void Finalresult(Test t)
        {
            int counter = 0;
            if ((t.details.reverseParking) && (t.details.TesterInvolved) && (t.details.speed) && (t.details.KeptDistance) && (t.details.EnterToJuction))
            {
                if (t.details.reverseParking)
                {
                    counter++;
                }
                if (t.details.signal)
                {
                    counter++;
                }
                if (t.details.PrepareToDrive)
                {
                    counter++;
                }
                if (counter >= 2)
                    t.FinalOutcome = true;

            }

            return;
        }//conculate finale result

        //trainee func:
        public void checkTraineeAge(Trainee s)//Check if the student is beneath to 18 age
        {
            DateTime N = DateTime.Now;
            if ((N - s.Birthday).Days > (Configuration.MinStudentAge * 365))
                return;
            throw new NotImplementedException("Trainee's age cant be beneath to 18 years");
        }
        public void CheckNumLessons(Trainee s)//check if the student did 20 lessons at least
        {
            if ((s.NumLessons) >= 20)
                return;
            throw new NotImplementedException("Trainee needs 20 lessons at least ");
        }
        public void CheckDaysToTest(Trainee s, DateTime d)//check if 7 days passed from the last test
        {
            if ((s.MyTests).Count != 0)
            {
                if ((d.AddDays(-7)) < (s.MyTests)[s.MyTests.Count - 1].DateAndHour)
                    throw new NotImplementedException("7 days didnt pass from the last test");
            }
        }
        public void CheckStudentCar(Trainee s)//Checks if the student has been passed the test on this type of car//-->if true-- already did the test and passed
        {
            foreach (Test t in s.MyTests)
            {
                if ((t.car == s.car) && (t.FinalOutcome == true))
                    throw new NotImplementedException("Trainee cant pass a test on the same type of car more than 1 time");

            }
            return;
        }
        public int numOfTests(Trainee s) //gets student, return the number of tests he did.
        {
            return s.MyTests.Count;
        }
        public void PassedTheTest(Trainee s)//gets student return yes if passed the test
        {
            if (s.MyTests.Count != 0)
            {
                if (s.MyTests[numOfTests(s) - 1].FinalOutcome == true)
                    if (s.MyTests[numOfTests(s) - 1].car== s.car)
                         throw new NotImplementedException("Trainee cant pass the same test twice");
            }
            return;
        }
        public bool ISFutureTest(Trainee s)//check if there is a future test
        {
            if (s.MyTests.Count == 0)
                return false;
            if ((s.MyTests[(s.MyTests.Count) - 1]).DateAndHour > DateTime.Now)
                return true;
            return false;

        }
        public Trainee BigAgeTrainee()
        {
            int max = 0;
            Trainee keep = new Trainee();
            foreach (Trainee t in dal.TraineeList())
            {
                if ((DateTime.Now.Year) - (t.Birthday.Year) > max)
                {
                    max = (DateTime.Now.Year) - (t.Birthday.Year);
                    keep = t;
                }
            }
            return keep;
        }// return the biggest trainee



        //both:
        public IEnumerable<Tester> MatchCarType(Trainee s, List<Tester> listTesters)//Match a tester to the car type of the student-->retun true if they are using the same car
        {
            var suitTest = from t in listTesters
                           where (s.car == t.car)
                           select t;
            return suitTest;
            // throw new NotImplementedException("The tester doesnt test on the car type of the student");
        }
        public IEnumerable<object> SpecificFutureTest(Tester sentTester)//func to return the future test+their count
        {
            int mone = 0;
            var sortedTests = from t in sentTester.futureTests
                              where t.DateAndHour > DateTime.Now
                              orderby t.numberTest
                              select new
                              {
                                  count = ++mone,
                                  numberTest = t.numberTest,
                                  DateAndHour = t.DateAndHour,
                                  StartTest = t.StartTest,
                                  car = t.car
                              };

            return sortedTests;
        }


        //Test func:
        public void CheckUpdateTest(Test t)//checks if the tester entered all the required field
        {
            if (t.IsTestUpdate == false)
                throw new NotImplementedException("canot update the test as long as not all the required field are full ");
        }
        public IEnumerable<Test> testSuitable(IEnumerable<Test> listTest, Predicate<Test> func)//Delegate test1)// delegate gets: test, return: bool
        {
            var suitTest = from t in listTest
                           where (func(t) == true)
                           select t;
            return suitTest;

        }
        public IEnumerable<Test> SortedTestAccordingDay(IEnumerable<Test> listTest, DateTime Rdate) //return all the test according the date
        {
            var sortedTests = from t in listTest
                              where (t.DateAndHour.Day == Rdate.Day)
                              orderby t.DateAndHour.Month
                              select t;
            return sortedTests;
        }
        public IEnumerable<Test> SortedTestAccordingMonth(IEnumerable<Test> listTest, DateTime Rdate)
        {
            var sortedTests = from t in listTest
                              where (t.DateAndHour.Month == Rdate.Month)
                              orderby t.DateAndHour.Day
                              select t;
            return sortedTests;
        }
        public IEnumerable<Test> ListByCriterion(int choice)
        {

            var tests = from item in dal.PassedList()
                        where item.details.getValue(choice) == false //the student faild in this criterion
                        orderby item.DateAndHour //order by date
                        select item;
            return tests;


        }
        public IEnumerable<Test> PassedTests()
        {
            return from item in dal.PassedList()
                   where item.FinalOutcome == true
                   select item;
        }
        public IEnumerable<Test> NotPassedTests()
        {
            return from item in dal.PassedList()
                   where item.FinalOutcome == false
                   select item;
        }
        public List<Test> getAllTest()
        {

            List<Test> AllTest = new List<Test>();

            foreach (Test t in dal.TestList())
                AllTest.Add(t);

            foreach (Test t in dal.PassedList())
                AllTest.Add(t);
                   
         
            return AllTest.ToList();
        }



        //Grouping:
     
        public IEnumerable<IGrouping<int, Tester>> GroupTestersBySeniority(bool sort = false)// group testers by their seniority
        {
            var testers = from tr in dal.TesterList()
                          group tr by tr.seniority into g
                          orderby g.Key
                          select g;
            return testers;
        }
        public IEnumerable<IGrouping<CarType, Tester>> GroupTestersByCarType(bool sort = false)// group testers by their cartype 
        {
            var testers = from tr in dal.TesterList()
                          group tr by tr.car into g
                          orderby g.Key
                          select g;
            return testers;
          
        }
        public IEnumerable<IGrouping<string, Tester>> GroupTestersByName(bool sort = false)// group testers by their cartype 
        {
            var testers = from tr in dal.TesterList()
                          group tr by tr.Name into g
                          orderby g.Key
                          select g;
            return testers;
        }


        public IEnumerable<IGrouping<string, Trainee>> GroupTraineesByDriSchool(bool sort = false)//group students by their drive school
        {
          

            var trainnes = from te in dal.TraineeList()
                           group te by te.SchoolName into g
                           orderby g.Key
                           select g;
            return trainnes;
        }
        public IEnumerable<IGrouping<string, Trainee>> GroupTraineesByDriveTeacher(bool sort = false)//group students by their drive teacher
        {
            var trainnes = from te in dal.TraineeList()
                           group te by te.TeacherName into g
                           orderby g.Key
                           select g;
            return trainnes;
        }
        public IEnumerable<IGrouping<int, Trainee>> GroupTraineesByNumOfTests(bool sort = false)//group students by the number of the lessons they did
        {
            var trainnes = from te in dal.TraineeList()
                           group te by te.MyTests.Count() into g
                           orderby g.Key
                           select g;
            return trainnes;
        }
        public IEnumerable<IGrouping<Gender, Trainee>> GroupTraineesByGender(bool sort = false)
        {
            var trainnes = from te in dal.TraineeList()
                           group te by te.GenderType into g
                           orderby g.Key
                           select g;
            return trainnes;
        }
        public IEnumerable<IGrouping<String, Trainee>> GroupTraineesByName(bool sort = false)
        {
            var trainnes = from te in dal.TraineeList()
                           group te by te.Name into g
                           orderby g.Key
                           select g;
            return trainnes;
        }
        public IEnumerable<IGrouping<Gearbox, Trainee>> GroupTraineesByGearbox(bool sort = false)
        {
            var trainnes = from te in dal.TraineeList()
                           group te by te.gearbox into g
                           orderby g.Key
                           select g;
            return trainnes;
        }

        public IEnumerable<IGrouping<DateTime, Test>> GroupTestByIsDate(Tester sentTester, IEnumerable<Test> Gotests)// group testers by their seniority
        {
            var tests = from tr in Gotests
                        group tr by tr.DateAndHour into g
                        orderby g.Key
                        select g;

            return tests;
        }
        public IEnumerable<IGrouping<int, Test>> GroupTestByNumTest(Tester sentTester, IEnumerable<Test> p)
        {
            var tests = from tr in p
                        group tr by tr.numberTest into g
                        orderby g.Key
                        select g;

            return tests;
        }
        public IEnumerable<IGrouping<DateTime, Test>> GroupTestsByDate(bool sort = true)
        {
            var tests = from t in getAllTest()
                        group t by t.DateAndHour into g
                        select g;
            return tests;
        }
        public IEnumerable<IGrouping<CarType, Test>> GroupTestByCarType(bool sort = false)
        {
            var tests = from t in getAllTest()
                        group t by t.car into g
                        select g;
            return tests;
        }
        public IEnumerable<IGrouping<bool, Test>> GroupTestByIsPassed(bool sort = false)
        {
            var tests = from t in getAllTest()
                        group t by t.FinalOutcome into g
                        select g;
            return tests;
        }




        //Get:

        public List<Tester> getTesters()
        {
            return dal.TesterList();
        }
        public List<Test> getTests()
        {
            return dal.TestList();
        }
        public List<Trainee> getTrainnes()
        {
            return dal.TraineeList();
        }
        public List<Test> getPassedTests()
        {
            return dal.PassedList();
        }

        // passwordFunc:
        public PasswordScore CheckStrength(string password)
        {
            int score = 1;

            if (password.Length < 1)
                return PasswordScore.Blank;
            if (password.Length < 3)
                return PasswordScore.VeryWeak;

            if (password.Length >= 4)
                score++;
            if (password.Length >= 7)
                score += 2;
            if ((Regex.Match(password, @"[a-z]", RegexOptions.ECMAScript).Success) && (Regex.Match(password, @"[A-Z]", RegexOptions.ECMAScript).Success) && (Regex.Match(password, @"[0-9]", RegexOptions.ECMAScript).Success))
                score += 2;
            if (score > 5)
                score = 5;
            return (PasswordScore)score;

        }

        //-->statistics func
        public int[] StaticTest(Test t)
        {
            int[] countFailur = new int[8];

            if (t.details.KeptDistance == false)
                countFailur[0] = 1;
            if (t.details.reverseParking == false)
                countFailur[1] = 1;
            if (t.details.mirrors == false)
                countFailur[2] = 1;
            if (t.details.signal == false)
                countFailur[3] = 1;
            if (t.details.speed == false)
                countFailur[4] = 1;
            if (t.details.TesterInvolved == false)
                countFailur[5] = 1;
            if (t.details.EnterToJuction == false)
                countFailur[6] = 1;
            if (t.details.PrepareToDrive == false)
                countFailur[7] = 1;

            return countFailur;
        }

     

        #region Idal func


        //Tester:
        public void addTester(Tester t) 
        {
            checkTesterAge(t);
            dal.addTester(t);
        }
        public void deleteTester(string id)
        {
            dal.deleteTester(id);
        }
        public void UpdateTester(Tester t)
        {
            dal.UpdateTester(t);
        }
        public int FindTester(string ID)
        {
            return dal.FindTester(ID);
        }

        //student:
        public void addStudent(Trainee t)
        {
            checkTraineeAge(t);
            dal.addStudent(t);
        }
        public void deleteStudent(string IdStudent)
        {
            dal.deleteStudent(IdStudent);
        }
        public void UpdateStudent(Trainee t)
        {
            dal.UpdateStudent(t);
        }
        public int FindTrainee(string ID)
        {
            return dal.FindTrainee(ID);
        }

        //tests:
        public void addTest(string id, DateTime date, Address address)
        {
            Trainee s = getTrainnes()[dal.FindTrainee(id)];
            CheckDaysToTest(s, date);//check if 7 days passed from the last test
            CheckNumLessons(s);//check if the student did 20 lessons at least
            PassedTheTest(s); //Checks if the student has been passed the test on this type of car
            CheckValidTester(s, date, address);
        }
        public void UpdateTest(Test t)
        {// the tester must enter all of the new details   
            t.IsTestUpdate = true;
            Finalresult(t);// send to the function of the finale result
            dal.UpdateTest(t);

            int index = dal.FindTest(t.numberTest);
            Test test = dal.TestList()[index];


            //the big lists
            dal.addPassedTest(t);
            dal.deleteTest(t.numberTest); //delete from the upcoming tests;




            Tester tester = dal.TesterList()[FindTester(t.TesterId)];
            tester.MyTests.Add(t);//move to passed tests


            //  tester.futureTests.Remove(test);// remove from future tests
            tester.futureTests = RemoveTest(tester.futureTests, test);
            dal.UpdateTester(tester);// update tester

            Trainee trainee = dal.TraineeList()[FindTrainee(t.StudentId)];

            trainee.MyTests = RemoveTest(trainee.MyTests , test);
           
            trainee.MyTests.Add(test);
            dal.UpdateStudent(trainee);


       


        }


        public int FindTest(int testNum)
        {
            return dal.FindTest(testNum);
        }
        public void deleteTest(int numTest)
        {


            Test test = getTests()[FindTest(numTest)];
            if (DateTime.Today > test.DateAndHour)
                throw new NotImplementedException("cannot delete this test, the date is already passed");
            if (DateTime.Now.AddDays(7) >= test.DateAndHour)
                throw new Exception("it's too late, you can't delete a test 7 days before");

            dal.deleteTest(numTest);

            Tester tester = dal.TesterList()[FindTester(test.TesterId)];
            tester.futureTests = RemoveTest(tester.futureTests, test);

            dal.UpdateTester(tester);// update tester


            Trainee trainee = dal.TraineeList()[FindTrainee(test.StudentId)];
            trainee.MyTests = RemoveTest(tester.futureTests, test);
            dal.UpdateStudent(trainee);// update trainee
        }
        #endregion


       List<Test> RemoveTest(List<Test>list1, Test test )
        {
            var tipo = from item in list1
                       where item.numberTest != test.numberTest
                       select item;
            return tipo.ToList();
        }
      





    }
}


