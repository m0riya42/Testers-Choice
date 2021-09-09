using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Test
    {
        #region Fields
      //  public static int test = Configuration.MinTestNumber;//keep the first number: 10000000. the first number will be 10000001
       int n_numberTest;
        //test number (8 dig)
        string n_TesterId;//id 
        string n_StudentId;//id
                           // public DateTime DateTest { set; get; } //תאריך שנקבע לטסט
        public DateTime DateAndHour { set; get; }//data and hour of the test
        public Address StartTest { set; get; }//where the test has begin
        public TestDetails details; //class with the details of the test -->test checking
        public CarType car { get; set; }
        public bool FinalOutcome { get; set; }//the grade of the student pass or fail
       
        //addition:
        public bool IsTestUpdate { set; get; }//if the tester update the results of the test
       // public Address FinishTest { set; get; }// update the last place in the tset
        

        #endregion

        #region Properties
        public string TesterId
        {
            get { return n_TesterId; }
            set
            {

                if (value.Length != 9)
                    throw new ArgumentException("מספר הספרות אינו תואם את הנדרש!");
                for (int i = 0; i < value.Length; i++)
                {
                    if ((value[i] < 48) || (value[i] > 57))//if the char is not between the ascii code of the digits
                        throw new ArgumentException("יש להכניס רק ספרות!");
                }
                n_TesterId = value;
            }
        }
        public string StudentId
        {
            get { return n_StudentId; }
            set
            {

                if (value.Length != 9)
                    throw new ArgumentException("מספר הספרות אינו תואם את הנדרש!");
                for (int i = 0; i < value.Length; i++)
                {
                    if ((value[i] < 48) || (value[i] > 57))//if the char is not between the ascii code of the digits
                        throw new ArgumentException("יש להכניס רק ספרות!");
                }
                n_StudentId = value;
            }
        }
        public int numberTest
        {
            get
            {
                return n_numberTest;
            }
           set {
               // n_numberTest = test;
                 n_numberTest = value;
            }
        }




        #endregion

        #region Constructor
        public Test()
        {
            // test++;

            //number test: length 8 digits:
            numberTest = 0;
            //for (int i = numberTest.Length; i < 8; i++)
            //{ numberTest = "0" + numberTest; } //keep the number of this spesific test

            IsTestUpdate = false;
           // FinishTest = new Address();
           // numberTest = test;

            TesterId = "999999999";
            StudentId = "999999999";
            //DateTest = new DateTime(01, 01, 01);
            DateAndHour = new DateTime();
            StartTest = new Address();
            details = new TestDetails();
        }
        public Test(string n_TesterId, string n_StudentId, DateTime n_DateAndHour, Address n_StartTest, CarType n_car)
        {
            //test++;
            IsTestUpdate = false;
          //  FinishTest = new Address();
            numberTest = 0;
            // numberTest = test;
            TesterId = n_TesterId;
            StudentId = n_StudentId;
            DateAndHour = n_DateAndHour;
            StartTest = n_StartTest;
            car = n_car;
            details = new TestDetails();
        }
        #endregion

        #region ToString
        public override string ToString()
        {
            //  return "Test number: " + numberTest + " \nteste Id: " + TesterId + " \nstudent Id: " + StudentId + " \nDate and hour: " +
            //        DateAndHour + " \nStart Test: " + StartTest + "\n\n" + details.ToString() + "\nFinalOutcome: " + (FinalOutcome ? "passed\n" : "failed\n");

            return DateAndHour+ " \nAddress:\n" + StartTest.street+ " "+StartTest.BuildNum+ " "+StartTest.city;
        }

        #endregion

        public Test ShallowCopy()// copy by value
        {
            return (Test)this.MemberwiseClone();
        }

        //~Test()
        //{

        //    test--;
        //}
    }
}