using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public struct Address
    {
        public string street { get; set; }
        public int BuildNum { get; set; }
        public string city { get; set; }

        public override string ToString()
        {
            return  street + " " + BuildNum + " " + city;
        }
        public static Address StringToAddress(string address)
        {
            string[] array = address.Split('/');
            string street1 = array[0];
            int numBuilding = int.Parse(array[1]);
            string city1 = array[2];
            Address a = new Address { street = street1, BuildNum = numBuilding, city = city1 };
            return a;
        }
        public static string AddressToString(Address address)
        {
            return address.street + "/ " + address.BuildNum + " /" + address.city;
        }


    }
}
