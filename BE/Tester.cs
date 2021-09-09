using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Tester
        {
            #region Fields
            string n_Id;
            string n_LName;
            string n_Name;
            DateTime n_Birthday;
            Gender n_GenderType;
            string n_CallNum;
            Address n_address;
            int n_seniority;//experience
            int n_MaxTests;//how many tests the tester can do in one week 
            CarType n_car;
            bool[,] n_WorkDay;
            int n_MaxDistance;
            public List<Test> MyTests { get; set; }//  הטסטים שהיו בעבר 
        List<Test> n_futureTests;
        public string ImageSource { set; get; }

        public string Password { get; set; }

        //  List<Test> futureTests { get; set; }
        #endregion

        #region Properties

        public string Id
            {
                get { return n_Id; }
                set
                {

                    if (value.Length > 9)
                        throw new ArgumentException("מספר הספרות אינו תואם את הנדרש!");
                    for (int i = 0; i < value.Length; i++)
                    {
                        if ((value[i] < 48) || (value[i] > 57))//if the char is not between the ascii code of the digits
                            throw new ArgumentException("יש להכניס רק ספרות!");
                    }
                    n_Id = value;
                }
            }
            public string LName
            {
                get { return n_LName; }
                set
                {

                    for (int i = 0; i < value.Length; i++)
                    {
                        if (((value[i] < 65) || (value[i] > 90)) && ((value[i] > 122) || (value[i] < 97)))//if the char is not between the ascii code of the characters
                            throw new ArgumentException("יש להכניס רק אותיות!");
                    }
                    n_LName = value;
                }

            }
            public string Name
            {
                get { return n_Name; }
                set
                {

                    for (int i = 0; i < value.Length; i++)
                    {
                        if (((value[i] < 65) || (value[i] > 90)) && ((value[i] > 122) || (value[i] < 97)))//if the char is not between the ascii code of the characters
                            throw new ArgumentException("יש להכניס רק אותיות!");
                    }
                    n_Name = value;
                }

            }
            public DateTime Birthday
            {
                get { return n_Birthday; }
                set { n_Birthday = value; }

            }
            public Gender GenderType
            {
                get
                {
                    return n_GenderType;
                }
                set
                {
                    n_GenderType = value;
                }
            }
            public string CallNum
            {
                get { return n_CallNum; }
                set
                {

                    if (value.Length != 10)
                        throw new ArgumentException("מספר הספרות אינו תואם את הנדרש!");
                    for (int i = 0; i < value.Length; i++)
                    {
                        if ((value[i] < 48) || (value[i] > 57))//if the char is not between the ascii code of the digits
                            throw new ArgumentException("יש להכניס רק ספרות!");
                    }
                    n_CallNum = value;

                }
            }
            public Address address
            {
                get { return n_address; }
                set
                {
                    n_address = value;
                }
            }
            public int seniority
            {
                get
                {
                    return n_seniority;
                }
                set
                {
                    n_seniority = value;
                }
            }
            public int MaxTests
            {
                get
                {
                    return n_MaxTests;
                }
                set
                {
                    n_MaxTests = value;
                }
            }
            public CarType car
            {
                get { return n_car; }
                set
                {
                    n_car = value;
                }
            }
            public bool[,] WorkDay
            {
                get { return n_WorkDay; }
                set
                {
                    n_WorkDay = value;
                }
            }//---->>>>>> עדכון מערכת השעות של הטסטר, לשנות
            public int MaxDistance
            {
                get { return n_MaxDistance; }
                set { n_MaxDistance = value; }
            }
        public List<Test> futureTests
        {
            get
            {
                return n_futureTests;
            }
            set
            {
                n_futureTests = value;
            }
        }


        #endregion

        #region Constructor
        public Tester()//constructor
            {
                Id = "123456789";
                Name = "";
                LName = "";
                Birthday = new DateTime(01, 01, 01);
                GenderType = Gender.female;
                CallNum = "1234567890";
                address = new Address();//-->
                seniority = 0;
                MaxTests = 0;
                car = CarType.privateCar;
                MaxDistance = 0;
                WorkDay = new bool[7, 5];
            MyTests = new List<Test>();
            futureTests = new List<Test>();
            ImageSource = (@"Empty Image");
            Password = "";
        }

        
            #endregion

            #region ToString
            public override string ToString()
            {

            string keep = "Tester Name: " + Name + " " + LName;
            //+ "\nid: " + Id + "\nBirthDay: " + Birthday + "\nGender: " + GenderType + "\nPhone Number: " + CallNum + "\nAddress: " + address.street + " " + address.BuildNum + " " + address.city  + "\nSeniority: " + seniority + "\nCar Type: " + car + "\nMax Distance: " + MaxDistance+"\nMax Test: " + MaxTests+"\n";

            //keep = keep + "\nDaily Sechedul Work:\n       sunday    monday    tuesday    wednesday    thursday\n";

            //double d = 9.00;
           
            //for (int i = 0; i < 7; i++)
            //{
            //    keep += d + ":00    ";
            //    for (int j = 0; j < 5; j++)
            //        keep += WorkDay[i, j] + "      ";
            //    keep += "\n";
            //    d++;
               
            //}
            ////keep += "\n";

            return keep;
            }
        #endregion
        public Tester ShallowCopy()// copy by value
        {
            return (Tester)this.MemberwiseClone();
        }

    }
}

//catch(Exception warnning)
//           {
//               Console.WriteLine(warnning.Message);
//           }
