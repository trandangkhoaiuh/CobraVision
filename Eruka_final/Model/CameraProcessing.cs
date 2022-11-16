using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperSimpleTcp;
using System.Windows.Forms;

namespace Eruka_final.Model
{
    
    public class CameraProcessing
    {
         //FormHome frmHome = new FormHome();
        //Edit something
        readonly private string cameraIP;
        readonly private string cameraPort;
        public int Inductor, Capacitor_22uF,Led, Diode, Jacks, Q1, C1, F2, EC2, Capacitor_680uF, CY1, Transistor, ChargingJack, ScrewHole, ScrewHole1;
        public bool CheckRight = false;
        public double CaseObject;
        public int PlcCase = 0;
        public int countPass=0, countFail=0, countTotal=0;
        public bool Result=false;
        public int x=0;
        public CameraProcessing()
        {
             cameraIP = "192.168.158.45";
             cameraPort = "3000";
        }
        public void CheckCam()
        {
            
            SimpleTcpClient client = new SimpleTcpClient(cameraIP + ":" + cameraPort);
            client.Events.Connected += Camera_Connected;
            client.Events.DataReceived += Camera_DataReceived;
            try
            {
                client.Connect();
                Result = true;
            }
            catch (Exception loi)
            {
                Result = false;
                MessageBox.Show(loi.ToString());
            }
        }
        public void Camera_DataReceived(object sender, DataReceivedEventArgs e)
        {
            x = 1;
            string dataa = Encoding.UTF8.GetString(e.Data);
            string[] dataList = dataa.Split(',');
            if (dataList.Length == 21)
            {
                Inductor = Int32.Parse(dataList[0]);
                Capacitor_22uF = Int32.Parse(dataList[1]);
                Led = Int32.Parse(dataList[2]);
                Diode = Int32.Parse(dataList[3]);
                Jacks = Int32.Parse(dataList[4]) + Int32.Parse(dataList[5]) + Int32.Parse(dataList[6]);
                Q1 = Int32.Parse(dataList[7]);
                C1 = Int32.Parse(dataList[8]);
                F2 = Int32.Parse(dataList[9]);
                EC2 = Int32.Parse(dataList[10]);
                Capacitor_680uF = Int32.Parse(dataList[11]) + Int32.Parse(dataList[12]);
                CY1 = Int32.Parse(dataList[13]);
                Transistor = Int32.Parse(dataList[14]);
                ChargingJack = Int32.Parse(dataList[15]);
                ScrewHole = Int32.Parse(dataList[16]) + Int32.Parse(dataList[17]);
                ScrewHole1 = Int32.Parse(dataList[18]) + Int32.Parse(dataList[19]);
                CaseObject = Convert.ToDouble(dataList[20])/1000;
                CheckRight = CheckResult();
                Count(CheckRight);
            }
        }
         public bool CheckResult()
        {
            bool result;
            if (Inductor == 1 && Capacitor_680uF == 2 && EC2 == 1 && Led == 1 && Diode == 1 && Jacks == 7 && Q1 == 1 && C1 == 1 && F2 == 1 && Capacitor_22uF == 1 && CY1 == 1 && Transistor == 1 && ChargingJack == 1 && ScrewHole == 4 && ScrewHole1 == 2)
            {
                result = true;
                if (CaseObject > -1.0 && CaseObject < 1.0)
                {
                    PlcCase = 1;
                }
                else if (CaseObject > (-181.0) && CaseObject < (-179.0))
                {
                    PlcCase = 2;
                }
                else
                    PlcCase = 3;
            }
            else
            {
                PlcCase = 3;
                result = false;
            }
            return result;
        }
        private void Count(bool isRight)
        {
            if (isRight)
            {
                countPass++;
            }
            else
            {
                countFail++; 
            }
            countTotal++;
        }
        private void Camera_Connected(object sender, ConnectionEventArgs e)
        {
            //TODO: Webwbrowse referesh
        }




    }
    
}
