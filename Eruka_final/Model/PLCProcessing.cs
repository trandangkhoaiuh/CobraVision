using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ActUtlTypeLib;

namespace Eruka_final.Model
{
    public class PLCProcessing
    {
        public ActUtlType PLCcompoment = new ActUtlType();

        public int Connect(int StationNumber)
        {
            PLCcompoment.ActLogicalStationNumber = StationNumber;
            return PLCcompoment.Open();
        }

        public int Disconnect()
        {
            return PLCcompoment.Close();
        }

        public int Read(string add, out int value)
        {
            return PLCcompoment.GetDevice(add, out value);
        }

        public int Write(string add, int value)
        {
            return PLCcompoment.SetDevice(add, Convert.ToInt16(value));
        }


        public int GetDevice()
        {
            return PLCcompoment.GetDevice("SM400", out _);
        }
    }
}
