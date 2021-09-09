using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Trainee
    {
        #region Fields
        string n_Id;//id
        string n_LName;//family
        string n_Name;//privte name 
        DateTime n_Birthday;//the data of the birthday
        Gender n_GenderType;// man or lady or other(;
        string n_CallNum;//number of the phone
        Address n_address;//house address of the student
        CarType n_car;//type of car (private..)
        Gearbox n_gearbox;//auto or manual
        string n_SchoolName;
        string n_TeacherName;
        int n_NumLessons;//how much lesson 

        string n_Password;
        public List<Test> MyTests { set; get; }

        //image:
        public string ImageSource { set; get; }
        #endregion

        #region Properties
        public string Password
        {
            set { n_Password = value; }
            get { return n_Password; }
        }

        public string Id
        {
            get { return n_Id; }
            set
            {

                if (value.Length != 9)
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
        public CarType car
        {
            get
            {
                return n_car;
            }
            set
            {
                n_car = value;
            }
        }
        public Gearbox gearbox
        {
            get
            {
                return n_gearbox;
            }
            set
            {
                n_gearbox = value;
            }
        }
        public string SchoolName
        {
            get
            {
                return n_SchoolName;
            }
            set
            {
                n_SchoolName = value;
            }
        }
        public string TeacherName
        {
            get
            {
                return n_TeacherName;
            }
            set
            {
                n_TeacherName = value;
            }
        }
        public int NumLessons
        {
            get
            {
                return n_NumLessons;
            }
            set
            {
                n_NumLessons = value;
            }
        }
        #endregion

        #region Constructor
        public Trainee()
        {
            Id = "222222222";
            Name = "";
            LName = "";
            Birthday = new DateTime(01, 01, 01);
            GenderType = Gender.female;
            CallNum = "1234567890";
            address = new Address() { street = "", BuildNum=0 , city = "" };
            car = CarType.privateCar;
            gearbox = Gearbox.Auto;
            SchoolName = "";
            TeacherName = "";
            NumLessons = 0;
            MyTests = new List<Test>();
            ImageSource = (@"Empty Image");
            Password = "";

        }
        #endregion

        #region ToString
        public override string ToString()
        {
            return "*Trainee Name: " + Name + " " + LName + "\n                     id: " + Id ;//+ "\n+ "BirthDay: " + Birthday + "\n" + "Gender: " + GenderType + "\n" + "Phone Number: " + CallNum + "\n" + "Address: " + address.street + " " + address.BuildNum + " " + address.city + "\n" + "gear box: " + gearbox + "\n" + "Car Type: " + car + "\n" + "school name: " + SchoolName + "\n" +
            //    "Teacher Name: " + TeacherName + "\n" + "Num of lessons: " + NumLessons;
        }
        #endregion

        public Trainee ShallowCopy()// copy by value
        {
            return (Trainee)this.MemberwiseClone();
        }
    }
}
