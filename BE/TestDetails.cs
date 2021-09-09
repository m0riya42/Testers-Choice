using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class TestDetails
    {
        #region Field&Properties
        public bool KeptDistance { get; set; }//if the student keep distance
        public bool reverseParking { get; set; }//if the student succeed the parking in reverse
        public bool mirrors { get; set; }//look at the mirrors
        public bool signal { get; set; }//right and left signal
        public bool speed { set; get; }//if the trainee drived at the allowed speed
        public bool TesterInvolved { set; get; }// if the tester involved the driving, include touch the steering wheel or the brake
        public bool EnterToJuction { set; get; }//how the trainee enter to a junction
        public bool PrepareToDrive { get; set; }//including fold a safety belt,  rasing hand brake, and arranging the driver seat
        public string TesterNote { get; set; }//what the tester think 


        #endregion

        //func:
      
        public bool getValue(int x)
        {
            //    public enum TestDetailChoice { ,, ,, , ,  , }


            switch (x)
            {
                case 0: return this.KeptDistance;
              
                case 1: return this.reverseParking;

                case 2: return this.mirrors;

                case 3: return this.signal;

                case 4: return this.speed;

                case 5: return this.TesterInvolved;

                case 6: return this.EnterToJuction;

                case 7: return this.PrepareToDrive;
                default: throw new Exception("unvaild testDetail object");

            }
        }

      
        //    private List<int> values = new List<int>();

        //public int GetValue(int index)
        //{
        //    return values[index];
        //}
        #region Constructor+ToString
        public TestDetails() { TesterNote = "note of the tester..."; }
        public override string ToString()
        {
            return "Test Results: \n" + "kept Distance? " + (KeptDistance ? "true" : "false") + "/n" + "reverse Parking? " + (reverseParking ? "true" : "false") + "/n" + "mirrors? " + (mirrors ? "true" : "false") + "/n" + "signal? " + (signal ? "true" : "false") + "/n" + TesterNote;
        }
        #endregion
    }
}
