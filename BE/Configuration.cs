using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Configuration
    {
        //tester:
        public static int MaxTesterAge = 70;
        public static int MinTesterAge = 40;

        //student:

        public static int MaxStudentAge = 90;
        public static int MinStudentAge = 18;
        public static int MinNumOfLessons = 20;
        public static int MinDaysBetweenTests = 7;


        //test:
        public static int MinTestNumber = 10000000;

        //  Configuration() { } //constructor
    }
}
