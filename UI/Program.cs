using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using BL;

namespace UI
{
    class Program
    {
        static IBL bl = BL.FactoryBL.getBL();


        static void Main(string[] args)
            {

          
            Console.WriteLine("enter you choice:\n1 to tester, 2 to student, 3 to admin, 0 to exit");
            int a = int.Parse(Console.ReadLine());
            int b;


            while (a != 0)
            {
                try
                {
                    switch (a)
                    {

                        case 1://tester
                            {
                                Console.WriteLine("enter you choice:\n 1-add new tester, 2-update tester, 3-update test, 4-update hours, 5-list of the upcoming tests, 6-delete tester, 0-exit\n");
                                b = int.Parse(Console.ReadLine());
                            while (b != 0)
                            {
                                    switch (b)
                                    {
                                        case 1:// add new tester
                                            {
                                               
                                                Tester s= new Tester { Id = "678090876", Birthday = new DateTime(1972, 12, 13), Name = "nahum", LName = "kala", GenderType = Gender.male, CallNum = "0589087654", address = new Address { city = "Jerushalem", street = "haatsmaut", BuildNum = 6 }, car = CarType.privateCar, MaxDistance = 15, MaxTests = 30, seniority = 7 };
                                                bl.addTester(s);
                                                break;
                                            }
                                        case 2://update tester
                                            {
                                                
                                                Tester p = bl.getTesters()[2];
                                                p.car = (CarType)2;
                                                p.Name = "phuo";
                                                p.CallNum = "1234567898";
                                                bl.UpdateTester(p);
                                                   break;
                                            } 
                                           
                                    
                                        case 3://update test
                                            {
                                                Test p = bl.getTests()[0];
                                                p.car = (CarType)1;
                                                p.details.mirrors = true;
                                                p.details.PrepareToDrive = true;

                                                bl.UpdateTest(p);

                                                break;
                                            }
                                        case 4://update hours
                                            {
                                                break;
                                            }
                                        case 5://list of the upcoming tests
                                            {
                                                //   Console.WriteLine("enter id: ");
                                                //  string id = Console.ReadLine();
                                                string id = "123654897";
                                                int i = bl.FindTester(id);
                                                foreach (Test t in bl.getTesters()[i].futureTests)
                                                    Console.WriteLine(t.ToString());

                                                break;
                                            }
                                        case 6://delete tester
                                            {
                                                bl.deleteTester("123654897");
                                                break;
                                            }
                                    }
                                    Console.WriteLine("if you stiil want to stay in the tester, enter your choice again:\n 1-add new tester, 2-update tester, 3-update test, 4-update hours, 5-list of the upcoming tests,6-delete tester, 0-exit\n");
                                    b = int.Parse(Console.ReadLine());
                                }

                                break;
                            }
                        case 2://student
                            {
                                Console.WriteLine("enter you choice:\n 1-add new student, 2-update student, 3-deleting test, 4-test details, 5-list of the done tests, 6-new test, 7- delete student, 0-exit\n");
                                b = int.Parse(Console.ReadLine());
                                while (b != 0)
                                {
                                    switch (b)
                                    {
                                        case 1:// new student
                                            {
                                                

                                                Trainee t = new Trainee { Id = "675890465", Name = "lea", LName = "nir", Birthday = new DateTime(1995, 6, 18), CallNum = "0587626586", GenderType = Gender.female, address = new Address { city = "Ashqelon", street = "Nisan", BuildNum = 18 }, car = CarType.privateCar, gearbox = Gearbox.Manual, NumLessons = 30, SchoolName = "OR Yrok", TeacherName = "moty dahan" };

                                                bl.addStudent(t);
                                                break;
                                            }
                                        case 2://update student
                                            {
                                                Trainee p = bl.getTrainnes()[2];
                                                p.car = CarType.MedTruck;
                                                p.CallNum = "0235698456";

                                                bl.UpdateStudent(p);
                                                
                                                break;
                                            }
                                        case 3://deleting test
                                            {
                                                break;
                                            }
                                        case 4://test details
                                            {

                                                Console.WriteLine("enter id: ");
                                                string id = Console.ReadLine();
                                                int i = bl.FindTrainee(id);
                                                    Console.WriteLine(bl.getTrainnes()[i].MyTests[bl.getTrainnes()[i].MyTests.Count-1].ToString());
                                                break;
                                                
                                            }
                                        case 5://list of the done tests
                                            {
                                                Console.WriteLine("enter id: ");
                                                string id = Console.ReadLine();
                                                int i = bl.FindTrainee(id);
                                                foreach(Test t in bl.getTrainnes()[i].MyTests)
                                                    Console.WriteLine(t.ToString());
                                                break;
                                            }
                                        case 6://new test
                                            {

                                                try
                                                {
                                                    Console.WriteLine("Enter: your id, the date you want and the address");
                                                    string id = Console.ReadLine();

                                                    DateTime d = DateTime.Parse(Console.ReadLine());
                                                    Address address = new Address();
                                                    address.street = Console.ReadLine();
                                                    address.BuildNum = int.Parse(Console.ReadLine());
                                                    address.city = Console.ReadLine();
                                                    bl.CheckValidTester(bl.getTrainnes()[bl.FindTrainee(id)], d, address);
                                                }
                                                catch (Exception ex)
                                                {
                                                    Console.WriteLine(ex);
                                                }
                                                break;
                                            }
                                    }
                                    Console.WriteLine("if you still want to stay in the student, enter your choice again:\n 1-add new student, 2-update student, 3-deleting test, 4-test details, 5-list of the done tests, 6-new test,  7- delete student, 0-exit\n");
                                    b = int.Parse(Console.ReadLine());
                                }
                            

                                break;
                            }
                        case 3://admin
                            {

                                Console.WriteLine("enter you choice: 1-print all testers, 2-print al trainees, 3-print all next tests, 4-print failed test by coice, 5-statistics, 0-exit\n");
                                b = int.Parse(Console.ReadLine());
                                while (b != 0)
                                {
                                    switch (b)
                                    {
                                        case 1://print all testers
                                            {
                                                foreach (Tester t in bl.getTesters())
                                                    Console.WriteLine(t.ToString() + "\n");
                                                break;
                                            }
                                        case 2://print al trainee
                                            {
                                                foreach (Trainee t in bl.getTrainnes())
                                                    Console.WriteLine(t.ToString() + "\n");
                                                break;
                                            }
                                        case 3://print all next tests
                                            {
                                                foreach (Test t in bl.getTests())
                                                    Console.WriteLine(t.ToString());
                                                break;
                                            }
                                        case 4://print failed test by choice
                                            {
                                                // choice:0-7, test details options
                                                Console.WriteLine("choose criterion of fail, enter numbers 0-7");
                                                int num = int.Parse(Console.ReadLine());
                                                
                                                Func<int, IEnumerable<Test>> sendIt = bl.ListByCriterion;
                                                var keep = sendIt(num);

                                                foreach (Test t in keep)// print all those test details
                                                    Console.WriteLine(t.ToString());



                                                break;
                                            }
                                        case 5://statistics,
                                            {
                                                break;
                                            }
                                    }
                                    Console.WriteLine("if you still want to stay in the admin, enter your choice again:\n 1-print all testers, 2-print al trainees, 3-print all next tests, 4-print failed test by coice, 5-statistics, 0-exit\n");
                                    b = int.Parse(Console.ReadLine());
                                }

                                break;
                            }
                            
                           
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                finally
                {
                    Console.WriteLine("enter you choice:  1 to tester, 2 to student, 3 to admin, 0 to exit");
                    a = int.Parse(Console.ReadLine());
                }
            }
        }
    }
}
//case 1:// To string
//    {
//        Console.WriteLine("To string checks\n\nTESTERS:\n");
//        foreach (Tester t in bl.getTesters())
//            Console.WriteLine(t.ToString()+"\n");
//        Console.WriteLine("\nSTUDENTS:\n");
//        foreach (Trainee t in bl.getTrainnes())
//            Console.WriteLine(t.ToString() + "\n");
//        Console.WriteLine("\nTESTS:");
//        foreach (Test t in bl.getTests())
//            Console.WriteLine(t.ToString() + "\n");
//        break;
//    }
//case 2: //add new tester
//    {
//        Console.WriteLine("input checks\n");
//        Console.WriteLine("TESTERS:\n Enter those arrgument: id, last name, first name,  date of birthday, call num, seniority, max tests, max distance");


//        string n_Id = Console.ReadLine();
//        string n_LName = Console.ReadLine();
//        string n_Name = Console.ReadLine();
//        DateTime n_Birthday = DateTime.Parse(Console.ReadLine());
//        string n_CallNum = Console.ReadLine();
//        int n_seniority = int.Parse(Console.ReadLine());
//        int n_MaxTests = int.Parse(Console.ReadLine());
//        int n_MaxDistance = int.Parse(Console.ReadLine());

//        Console.WriteLine("Enter num 0-3 for car type: privateCar, TwoWheel, MedTruck, HeavyTruck  ");
//        int car = int.Parse(Console.ReadLine());

//        Console.WriteLine("Enter num 0-1 for gender :male, female");
//        int gender = int.Parse(Console.ReadLine());

//        Console.WriteLine("Enter address:street,  BuildNum, city;");
//        string street = Console.ReadLine();
//        int buildnum = int.Parse(Console.ReadLine());
//        string city = Console.ReadLine();


//        Console.WriteLine("Enter 0/1 for work hours:");
//        bool[,] n_WorkDay = new bool[7, 5];
//        for (int i = 0; i < 7; i++)
//            for (int j = 0; j < 5; j++)
//                n_WorkDay[i, j] = bool.Parse(Console.ReadLine());

//        bl.addTester(n_Id, n_LName, n_Name, n_Birthday, street, buildnum, city, n_CallNum, car, n_seniority, n_MaxTests, gender, n_WorkDay, n_MaxDistance);




//        break;
//    }
//case 3:// searching for a date to new test
//    {
//        Console.WriteLine("Enter: your id and the date you want");

//        string id = Console.ReadLine();
//        DateTime d = DateTime.Parse(Console.ReadLine());
//        bl.CheckValidTester(bl.getTrainnes()[bl.FindTrainee(id)], d);


//        break;
//    }
//case 4: // add new student
//    {
//        Console.WriteLine("input checks\n");
//        Console.WriteLine("STUDENT:\n Enter those arrgument: id, last name, first name,  date of birthday, call num, ");


//        string n_Id = Console.ReadLine();
//        string n_LName = Console.ReadLine();
//        string n_Name = Console.ReadLine();
//        DateTime n_Birthday = DateTime.Parse(Console.ReadLine());
//        string n_CallNum = Console.ReadLine();
//        int numLessons = int.Parse(Console.ReadLine());
//        string teacher = Console.ReadLine();
//        string school = Console.ReadLine();

//        Console.WriteLine("Enter num 0-3 for car type: privateCar, TwoWheel, MedTruck, HeavyTruck  ");
//        int car = int.Parse(Console.ReadLine());

//        Console.WriteLine("Enter num 0-1 for gender :male, female");
//        int gender = int.Parse(Console.ReadLine());

//        Console.WriteLine("Enter num 0-1 for gearbox :male, female");
//        int gearbox = int.Parse(Console.ReadLine());

//        Console.WriteLine("Enter address:street,  BuildNum, city;");
//        string street = Console.ReadLine();
//        int buildnum = int.Parse(Console.ReadLine());
//        string city = Console.ReadLine();


//        bl.addStudent(n_Id, n_Name, n_LName, n_Birthday,(Gender)gender, n_CallNum, new Address() { BuildNum = buildnum, city = city, street = street }, (CarType)car, (Gearbox)gearbox, school, teacher, numLessons);

//        break;
//    }