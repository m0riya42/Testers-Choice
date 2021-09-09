using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using System.Xml.Linq;
using System.IO;
using System.Xml.Serialization;
using System.Reflection;
using System.ComponentModel;

namespace DAL
{
    class DALXml : Idal
    {
        string TesterPath = @"testerXml.xml";
        string TestPath = @"testXml.xml";
        string TraineePath = @"traineeXml.xml";
        string NumberTestPath = @"numberTestXml.xml";
        string PassedTestPath = @"passedtestXml.xml";


        XElement testerRoot;
        XElement testRoot;
        XElement traineeRoot;
        XElement numberTestRoot;
        XElement passedtestRoot;
        public static List<Tester> testers = new List<Tester>();
        public static List<Trainee> trainees = new List<Trainee>();
        public static List<Test> tests = new List<Test>();
        public static List<Test> passedTests = new List<Test>();

        public DALXml()
        {
            if (!File.Exists(TesterPath))
            {
                testerRoot = new XElement("testers");
                testerRoot.Save(TesterPath);
            }
            else
                LoadDataTester();

            if (!File.Exists(TestPath))
            {
                testRoot = new XElement("tests");
                testRoot.Save(TestPath);
            }
            else
                LoadDataTest();

            if (!File.Exists(TraineePath))
            {
                traineeRoot = new XElement("trainees");
                traineeRoot.Save(TraineePath);
            }
            else
                LoadDataTrainee();
            if (!File.Exists(NumberTestPath))
            {
                numberTestRoot = new XElement("NumberTest");
                numberTestRoot.Add(new XElement("numberTest", Configuration.MinTestNumber));
                numberTestRoot.Save(NumberTestPath);

            }
            else
                LoadDataNumberTest();
            if (!File.Exists(PassedTestPath))
            {
                passedtestRoot = new XElement("PassedTest");
                passedtestRoot.Save(PassedTestPath);

            }
            else
                LoadDataPassedTest();
        }
        private void LoadDataTester()
        {
            try
            {
                testerRoot = XElement.Load(TesterPath);
            }
            catch
            {
                throw new Exception("Tester file upload problem");
            }
        }
        private void LoadDataPassedTest()
        {
            try
            {
                passedtestRoot = XElement.Load(PassedTestPath);
            }
            catch
            {
                throw new Exception(" Passed test file upload problem");
            }
        }
        private void LoadDataTrainee()
        {
            try
            {
                traineeRoot = XElement.Load(TraineePath);
            }
            catch
            {
                throw new Exception("Trainee file upload problem");
            }
        }
        private void LoadDataTest()
        {
            try
            {
                testRoot = XElement.Load(TestPath);
            }
            catch
            {
                throw new Exception("Test file upload problem");
            }
        }
        private void LoadDataNumberTest()
        {
            try
            {
                numberTestRoot = XElement.Load(NumberTestPath);
            }
            catch
            {
                throw new Exception("Number test file upload problem");
            }
        }

        private string CopyFiles(string sourcePath, string destinationName)
        {
            try
            {
                int postfixIndex = sourcePath.LastIndexOf('.');
                string postfix = sourcePath.Substring(postfixIndex);
                destinationName += postfix;

                string destinationPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                string destinationFullName = @"/pictures/passport/" + destinationName;
            
                System.IO.File.Copy(sourcePath, destinationFullName, true);
                return destinationFullName;
            }
            catch (Exception ex)
            {

                return sourcePath;
            }

        }
        private void createFileTestNumber()
        {
            numberTestRoot = new XElement("numberTest", Configuration.MinTestNumber);
            numberTestRoot.Save(NumberTestPath);
        }

        #region convertions
        XElement ConvertTester(BE.Tester tester)//convert a Tester element into xelement 
        {
            XElement Id = new XElement("Id", tester.Id);
            XElement Birthday = new XElement("Birthday", tester.Birthday);
            XElement CallNum = new XElement("CallNum", tester.CallNum);
            XElement car = new XElement("car", tester.car);
            XElement GenderType = new XElement("GenderType", tester.GenderType);
            XElement ImageSource = new XElement("ImageSource", tester.ImageSource);
            XElement LName = new XElement("LName", tester.LName);
            XElement Name = new XElement("Name", tester.Name);
            XElement Password = new XElement("Password", tester.Password);
            XElement MaxDistance = new XElement("MaxDistance", tester.MaxDistance);
            XElement MaxTests = new XElement("MaxTests", tester.MaxTests);
            XElement address = new XElement("address", Address.AddressToString( tester.address));//
            XElement seniority = new XElement("seniority", tester.seniority);
            XElement futureTests = new XElement("futureTests", TestToXML(tester.futureTests));
            //from t in TestToXML(tester.futureTests)
            //                        select new XElement("test", t));
            XElement MyTests = new XElement("MyTests", TestToXML(tester.MyTests));

            XElement WorkDay = new XElement("WorkDay", MatrixToXML(tester.WorkDay));


            XElement trainee = new XElement("tester", Id, Birthday, CallNum, car, seniority, GenderType, ImageSource, LName, Name, MaxDistance, Password, MaxTests, address, MyTests, futureTests, WorkDay);
            return trainee;
        }
        XElement ConvertTrainee(BE.Trainee student)//convert a Trainee element into xelement 
        {
            XElement Id = new XElement("Id", student.Id);
            XElement Birthday = new XElement("Birthday", student.Birthday);
            XElement CallNum = new XElement("CallNum", student.CallNum);
            XElement car = new XElement("car", student.car);
            XElement gearbox = new XElement("gearbox", student.gearbox);
            XElement GenderType = new XElement("GenderType", student.GenderType);
            XElement ImageSource = new XElement("ImageSource", student.ImageSource);
            XElement LName = new XElement("LName", student.LName);
            XElement Name = new XElement("Name", student.Name);
            XElement NumLessons = new XElement("NumLessons", student.NumLessons);
            XElement Password = new XElement("Password", student.Password);
            XElement SchoolName = new XElement("SchoolName", student.SchoolName);
            XElement TeacherName = new XElement("TeacherName", student.TeacherName);
            XElement address = new XElement("address", Address.AddressToString(student.address));
            XElement MyTests = new XElement("MyTests", TestToXML(student.MyTests));



            XElement trainee = new XElement("trainee", Id, Birthday, CallNum, car, gearbox, GenderType, ImageSource, LName, Name, NumLessons, Password, SchoolName, TeacherName, address, MyTests);
            return trainee;

        }
        XElement ConvertTest(BE.Test test)//convert a Test element into xelement 
        {
            XElement car = new XElement("car", test.car);
            XElement DateAndHour = new XElement("DateAndHour", test.DateAndHour);
            XElement FinalOutcome = new XElement("FinalOutcome", test.FinalOutcome);
            XElement IsTestUpdate = new XElement("IsTestUpdate", test.IsTestUpdate);
            XElement numberTest = new XElement("numberTest", test.numberTest);
            XElement StartTest = new XElement("StartTest", Address.AddressToString(test.StartTest));
            XElement StudentId = new XElement("StudentId", test.StudentId);
            XElement TesterId = new XElement("TesterId", test.TesterId);
            XElement details = TestDetsilsToXml(test.details);

            XElement testElement = new XElement("test", car, DateAndHour, FinalOutcome/*, FinishTest*/, IsTestUpdate, numberTest, StartTest, StudentId, TesterId, details);

            return testElement;
        }

        BE.Trainee ConvertTrainee(XElement element)
        {
            Trainee trainee = new Trainee();
            trainee.Id = element.Element("Id").Value;
            trainee.Birthday = DateTime.Parse(element.Element("Birthday").Value);
            trainee.CallNum = element.Element("CallNum").Value;
            trainee.car = (CarType)Enum.Parse(typeof(CarType), element.Element("car").Value);
            trainee.gearbox = (Gearbox)Enum.Parse(typeof(Gearbox), element.Element("gearbox").Value);
            trainee.GenderType = (Gender)Enum.Parse(typeof(Gender), element.Element("GenderType").Value);
            trainee.ImageSource = element.Element("ImageSource").Value;
            trainee.LName = element.Element("LName").Value;
            trainee.Name = element.Element("Name").Value;
            trainee.NumLessons = int.Parse(element.Element("NumLessons").Value);
            trainee.Password = element.Element("Password").Value;
            trainee.SchoolName = element.Element("SchoolName").Value;
            trainee.TeacherName = element.Element("TeacherName").Value;
            trainee.address = Address.StringToAddress(element.Element("address").Value);
            trainee.MyTests = XMLtoTest(element.Element("MyTests"));

            //Trainee trainee = new Trainee
            //{
            //    Id = element.Element("Id").Value,
            //    Birthday = DateTime.Parse(element.Element("Birthday").Value),
            //    CallNum = element.Element("CallNum").Value,
            //    car = (CarType)Enum.Parse(typeof(CarType), element.Element("car").Value),
            //    gearbox = (Gearbox)Enum.Parse(typeof(Gearbox), element.Element("gearbox").Value),
            //    GenderType = (Gender)Enum.Parse(typeof(Gender), element.Element("GenderType").Value),
            //    ImageSource = element.Element("ImageSource").Value,
            //    LName = element.Element("LName").Value,
            //    Name = element.Element("Name").Value,
            //    NumLessons = int.Parse(element.Element("NumLessons").Value),
            //    Password = element.Element("Password").Value,
            //    SchoolName = element.Element("SchoolName").Value,
            //    TeacherName = element.Element("TeacherName").Value,
            //    address = Address.StringToAddress(element.Element("address").Value),
            //    MyTests = XMLtoTest(element.Element("MyTests"))
            //};
            return trainee;
        }
        BE.Tester ConvertTester(XElement element)
        {
            //Tester tester = new Tester
            //{
            //    Id = element.Element("Id").Value,
            //    Birthday = DateTime.Parse(element.Element("Birthday").Value),
            //    CallNum = element.Element("CallNum").Value,
            //    car = (CarType)Enum.Parse(typeof(CarType), element.Element("car").Value),
            //    GenderType = (Gender)Enum.Parse(typeof(Gender), element.Element("GenderType").Value),
            //    ImageSource = element.Element("ImageSource").Value,
            //    LName = element.Element("LName").Value,
            //    Name = element.Element("Name").Value,
            //    Password = element.Element("Password").Value,
            //    address = Address.StringToAddress(element.Element("address").Value),
            //    MyTests = XMLtoTest(element.Element("MyTests")),
            //    MaxDistance = int.Parse(element.Element("MaxDistance").Value),
            //    MaxTests = int.Parse(element.Element("MaxTests").Value),
            //    seniority = int.Parse(element.Element("seniority").Value),
            //    futureTests = XMLtoTest(element.Element("futureTests")),
            //    WorkDay = XMLToMatrix(element.Element("WorkDay").Value)
            //  return tester;
            //};
            Tester t = new Tester();

            t.Id = element.Element("Id").Value;
            t.Birthday = DateTime.Parse(element.Element("Birthday").Value);
            t.CallNum = element.Element("CallNum").Value;
            t.car = (CarType)Enum.Parse(typeof(CarType), element.Element("car").Value);
            t.GenderType = (Gender)Enum.Parse(typeof(Gender), element.Element("GenderType").Value);
            t.ImageSource = element.Element("ImageSource").Value;
            t.LName = element.Element("LName").Value;
            t.Name = element.Element("Name").Value;
            t.Password = element.Element("Password").Value;
            t.address = Address.StringToAddress(element.Element("address").Value);
            t.MyTests = XMLtoTest(element.Element("MyTests"));
            t.MaxDistance = int.Parse(element.Element("MaxDistance").Value);
            t.MaxTests = int.Parse(element.Element("MaxTests").Value);
            t.seniority = int.Parse(element.Element("seniority").Value);
            t.futureTests = XMLtoTest(element.Element("futureTests"));
            t.WorkDay = XMLToMatrix(element.Element("WorkDay").Value);

            return t;
        }
        BE.Test ConvertTest(XElement test)
        {
            bool k = bool.Parse(test.Element("FinalOutcome").Value);
            bool b = bool.Parse(test.Element("IsTestUpdate").Value);
            Test t = new Test
            {
                car = (CarType)Enum.Parse(typeof(CarType), test.Element("car").Value),
                DateAndHour = DateTime.Parse(test.Element("DateAndHour").Value),
                FinalOutcome = k,
                IsTestUpdate = b,
                numberTest = int.Parse(test.Element("numberTest").Value),
                StartTest = Address.StringToAddress(test.Element("StartTest").Value),
                StudentId = test.Element("StudentId").Value,
                TesterId = test.Element("TesterId").Value,
                details = stringToTestDetails(test.Element("details").Value),
            };
            return t;

        }
        static List<Test> XMLtoTest(XElement element)
        {
            List<Test> tests = new List<Test>();
            foreach (XElement test in element.Elements())
            {
                Test t = new Test();
                t.car = (CarType)Enum.Parse(typeof(CarType), test.Element("car").Value);
                t.DateAndHour = DateTime.Parse(test.Element("DateAndHour").Value);
                t.FinalOutcome = bool.Parse(test.Element("FinalOutcome").Value);
              //  t.FinishTest = Address.StringToAddress(test.Element("FinishTest").Value);
                t.IsTestUpdate = bool.Parse(test.Element("IsTestUpdate").Value);
                t.numberTest = int.Parse(test.Element("numberTest").Value);
                t.StartTest = Address.StringToAddress(test.Element("StartTest").Value);
                t.StudentId = test.Element("StudentId").Value;
                t.TesterId = test.Element("TesterId").Value;
                t.details = stringToTestDetails(test.Element("details").Value);
                tests.Add(t);
            }
            return tests.ToList();
        }
            //IEnumerable<Test> tests = from test in element.Elements()
            //                          select new Test()
            //                          {
            //                              car = (CarType)Enum.Parse(typeof(CarType), test.Element("car").Value),
            //                              DateAndHour = DateTime.Parse(test.Element("DateAndHour").Value),
            //                              FinalOutcome = bool.Parse(test.Element("FinalOutcome").Value),
            //                              FinishTest = Address.StringToAddress(test.Element("FinishTest").Value),
            //                              IsTestUpdate = bool.Parse(test.Element("IsTestUpdate").Value),
            //                              numberTest = int.Parse(test.Element("numberTest").Value),
            //                              StartTest = Address.StringToAddress(test.Element("StartTest").Value),
            //                              StudentId = test.Element("StudentId").Value,
            //                              TesterId = test.Element("TesterId").Value,
            //                              details = stringToTestDetails(test.Element("details").Value),
            //                          };
            //return tests.ToList();
        
        List<XElement> TestToXML(List<Test> tests1)
        {
            List<XElement> xElements = new List<XElement>();
            foreach (Test t in tests1)
            {
                XElement car = new XElement("car", t.car);
                XElement DateAndHour = new XElement("DateAndHour", t.DateAndHour);
                XElement FinalOutcome = new XElement("FinalOutcome", t.FinalOutcome);
               // XElement FinishTest = new XElement("FinishTest", t.FinishTest);
                XElement IsTestUpdate = new XElement("IsTestUpdate", t.IsTestUpdate);
                XElement numberTest = new XElement("numberTest", t.numberTest);
                XElement StartTest = new XElement("StartTest", Address.AddressToString(t.StartTest));
                XElement TesterId = new XElement("TesterId", t.TesterId);
                XElement StudentId = new XElement("StudentId", t.StudentId);
                XElement details = new XElement("details", TestDetsilsToXml(t.details));
                XElement test = new XElement("test", car, DateAndHour, FinalOutcome, IsTestUpdate, numberTest, StartTest, TesterId, StudentId, details);
                xElements.Add(test);
            }
            return xElements;
        }
        bool[,] XMLToMatrix(string matrix)
        {
            bool[,] UserMetrix;
            string[] values = matrix.Split(',');
            UserMetrix = new bool[7, 5];
            int index = 1;
            for (int i = 0; i < 7; i++)
                for (int j = 0; j < 5; j++)
                    UserMetrix[i, j] = bool.Parse(values[index++]);
            return UserMetrix;
        }
        string MatrixToXML(bool[,] matrix)
        {
            string result = "";
            for (int i = 0; i < 7; i++)
                for (int j = 0; j < 5; j++)
                    result += "," + matrix[i, j];
            return result;
        }
        static TestDetails stringToTestDetails(string details)
        {
            string[] array = details.Split('e');
            bool KeptDistance1 = bool.Parse(array[0] + "e");
            bool reverseParking1 = bool.Parse(array[1] + "e");
            bool mirrors1 = bool.Parse(array[2] + "e");
            bool signal1 = bool.Parse(array[3] + "e");
            bool speed1 = bool.Parse(array[4] + "e");
            bool TesterInvolved1 = bool.Parse(array[5] + "e");
            bool EnterToJuction1 = bool.Parse(array[6] + "e");
            bool PrepareToDrive1 = bool.Parse(array[7] + "e");
            string TesterNote1 = "";
            for (int i = 8; i < array.Length - 1; i++)
            {
                TesterNote1 += array[i] + "e";
            }
            TesterNote1 += array[array.Length - 1];
            TestDetails t = new TestDetails() { EnterToJuction = EnterToJuction1, KeptDistance = KeptDistance1, mirrors = mirrors1, PrepareToDrive = PrepareToDrive1, reverseParking = reverseParking1, signal = signal1, speed = speed1, TesterInvolved = TesterInvolved1, TesterNote = TesterNote1 };
            return t;
        }
        XElement TestDetsilsToXml(TestDetails details)
        {
            //string t = details.KeptDistance + " " + details.reverseParking + " " + details.mirrors + " " + details.signal + " " + details.speed + " " + details.TesterInvolved + " " + details.EnterToJuction + " " + details.PrepareToDrive + " " + details.TesterNote;

            //string[] values = t.Split(' ');
            //int index = 0;
            //string TesterNote = "";
            XElement KeptDistance1 = new XElement("KeptDistance", details.KeptDistance + " ");
            XElement reverseParking1 = new XElement("reverseParking", details.reverseParking + " ");
            XElement mirrors1 = new XElement("mirrors", details.mirrors + " ");
            XElement signal1 = new XElement("signal", details.signal + " ");
            XElement speed1 = new XElement("speed", details.speed);
            XElement TesterInvolved1 = new XElement("TesterInvolved", details.TesterInvolved + " ");
            XElement EnterToJuction1 = new XElement("EnterToJuction", details.EnterToJuction + " ");
            XElement PrepareToDrive1 = new XElement("PrepareToDrive", details.PrepareToDrive + " ");
            // for (int i = 8; i < values.Length; i++)
            //{
            //    TesterNote += values[i];
            //}
            XElement TesterNote1 = new XElement("TesterNote", details.TesterNote);



            XElement element = new XElement("details", KeptDistance1, reverseParking1, mirrors1, signal1, speed1, TesterInvolved1, EnterToJuction1, PrepareToDrive1, TesterNote1);
            return element;


        }
        #endregion

        #region Tester Function
        public int FindTester(string ID)
        {
            LoadDataTester();//load data
            for (int i = 0; i < testerRoot.Elements().Count(); i++)
            {
                if ((testerRoot.Elements().ToList()[i].Element("Id").Value) == ID)
                {
                    return i;
                }
            }
            throw new NotImplementedException("The tester is not found in the testers list. ");
        }
        public void addTester(Tester tester)
        {
            LoadDataTester();//load data
            for (int i = 0; i < testerRoot.Elements().Count(); i++)
            {
                if ((testerRoot.Elements().ToList()[i].Element("Id").Value) == tester.Id)
                {
                    throw new NotImplementedException(" the tester is already exist in the tester list.");
                }
            }
            //tester.address = new Address() { street = tester.address.street + "/", BuildNum = tester.address.BuildNum, city = "/" + tester.address.city };

            tester.ImageSource = CopyFiles(tester.ImageSource, "pasport_" + tester.Id);
            testerRoot.Add(ConvertTester(tester));
            testerRoot.Save(TesterPath);



        }
        public void deleteTester(string id)
        {
            XElement testerElement;
            int i = FindTester(id);
            testerElement = testerRoot.Elements().ToList()[i];
            testerElement.Remove();
            testerRoot.Save(TesterPath);

        }
        public void UpdateTester(Tester tester)
        {
            XElement testerElement = (from stu in testerRoot.Elements()
                                      where (stu.Element("Id").Value) == tester.Id
                                      select stu).FirstOrDefault();
            testerElement.Remove();
            testerRoot.Save(TesterPath);
            addTester(tester);

        }
        #endregion

        #region Trainee Function
        public int FindTrainee(string ID)
        {
            LoadDataTrainee();//load data
            for (int i = 0; i < traineeRoot.Elements().Count(); i++)
            {
                if ((traineeRoot.Elements().ToList()[i].Element("Id").Value) == ID)
                {
                    return i;
                }
            }
            throw new NotImplementedException("The trainee is not exist in the trainees list. ");
        }
        public void addStudent(Trainee student)
        {
            LoadDataTrainee();//load data
            for (int i = 0; i < traineeRoot.Elements().Count(); i++)
            {
                if ((traineeRoot.Elements().ToList()[i].Element("Id").Value) == student.Id)
                {
                    throw new NotImplementedException("The trainee is already exist in the trainees list.");
                }
            }
           // student.address = new Address() { street = student.address.street + "/", BuildNum = student.address.BuildNum, city = "/" + student.address.city };

            student.ImageSource = CopyFiles(student.ImageSource, "pasport_" + student.Id);
            traineeRoot.Add(ConvertTrainee(student));
            traineeRoot.Save(TraineePath);
        }
        public void deleteStudent(string id)
        {
            XElement traineeElement;
            int i = FindTrainee(id);
            traineeElement = traineeRoot.Elements().ToList()[i];
            traineeElement.Remove();
            traineeRoot.Save(TraineePath);
        }
        public void UpdateStudent(Trainee student)
        {
            XElement updateTraineeElement = (from trainee in traineeRoot.Elements()
                                             where trainee.Element("Id").Value == student.Id
                                             select trainee).FirstOrDefault();

            updateTraineeElement.Remove(); //מחיקת התלמיד הקודם- הלא מעודכן
            traineeRoot.Save(TraineePath);

            addStudent(student); //הוספת התלמיד המעודכן

        }
        #endregion

        #region Test Function
        public int FindTest(int testNum)
        {
            LoadDataTest();//load data
            for (int i = 0; i < testRoot.Elements().Count(); i++)
            {
                if (int.Parse(testRoot.Elements().ToList()[i].Element("numberTest").Value) == testNum)
                {
                    return i;
                }
            }
            throw new NotImplementedException(" the test is not exist in the tests list. ");
        }
        public void addTest(Test test)
        {
            LoadDataNumberTest();
            LoadDataTest();//load data
            if (test.numberTest == 0)
            {
                int testNumber = int.Parse(numberTestRoot.Element("numberTest").Value);
                numberTestRoot.RemoveAll();
                numberTestRoot.Add(new XElement("numberTest", testNumber + 1));
                numberTestRoot.Save(NumberTestPath);
                test.numberTest = testNumber;
            }
            //for (int i = 0; i < testRoot.Elements().Count(); i++)
            //{
            //    if (int.Parse(testRoot.Elements().ToList()[i].Element("numberTest").Value) == test.numberTest)
            //    {
            //        throw new NotImplementedException(" the test is already exist in the test list.");
            //    }
            //}
           // test.StartTest = new Address() { street = test.StartTest.street + "/", BuildNum = test.StartTest.BuildNum, city = "/" + test.StartTest.city };

            testRoot.Add(ConvertTest(test));
            testRoot.Save(TestPath);
        }
        public void UpdateTest(Test test)
        {
            XElement updateTestElement = (from t in testRoot.Elements()
                                          where int.Parse(t.Element("numberTest").Value) == test.numberTest
                                          select t).FirstOrDefault();

            updateTestElement.Remove(); //מחיקת המבחן הקודם- הלא מעודכן
            testRoot.Save(TestPath);
            addTest(test); //הוספת המבחן המעודכן




            //update the test in the tests list
            //testRoot.Elements().ToList()[FindTest(test.numberTest)] = ConvertTest(test);
            //testerRoot.Save(TesterPath);
        }
        public void deleteTest(int numTst)
        {
            LoadDataTest();
            XElement removeTestElement = (from test in testRoot.Elements()
                                          where int.Parse(test.Element("numberTest").Value) == numTst
                                          select test).FirstOrDefault(); //או שמחזיר את הראשון שמצא או שמחזיר נל
            if (removeTestElement == null)//לא נמצא
                throw new Exception("This test does not exists");

            removeTestElement.Remove();
            testRoot.Save(TestPath);

        }
        #endregion

        public void addPassedTest(Test test)
        {
            LoadDataPassedTest();//load data
            //test.StartTest = new Address() { street = test.StartTest.street + "/", BuildNum = test.StartTest.BuildNum, city = "/" + test.StartTest.city };
            passedtestRoot.Add(ConvertTest(test));
            passedtestRoot.Save(PassedTestPath);
        }

        public List<Trainee> TraineeList()//load list from xml
        {
            LoadDataTrainee();
            var trainees = from item in traineeRoot.Elements()
                           let c = ConvertTrainee(item)
                           select c;

            return trainees.ToList();
        }
        public List<Tester> TesterList()
        {
            List<Tester> te = new List<Tester>();
            LoadDataTester();
            foreach (XElement item in testerRoot.Elements())
            {
                Tester c = ConvertTester(item);
                te.Add(c);
            }
            return te;
        }
        public List<Test> TestList()
        {
            LoadDataTest();
            IEnumerable<Test> tests = from item in testRoot.Elements()
                                      let c = ConvertTest(item)
                                      select c;
            return tests.ToList();
        }
        public List<Test> PassedList()
        {
            LoadDataPassedTest();
            IEnumerable<Test> tests = from item in passedtestRoot.Elements()
                                      let c = ConvertTest(item)
                                      select c;
            return tests.ToList();
        }
    }
}



