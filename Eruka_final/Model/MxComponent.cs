using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Eruka_final.Model
{
    public class MxComponent
    {
        private int iErrorCode = 0;
        private string sErrorCode = "";

        private int iLogicalStationNumber = 0;

        private readonly Object thisLock = new Object();

        private readonly ActUtlTypeLib.ActUtlType actUtlType;

        private bool bConnected = false;
        readonly System.Timers.Timer timer = new System.Timers.Timer();

        public MxComponent(int iLogicalStationNumber)
        {
            actUtlType = new ActUtlTypeLib.ActUtlType();

            actUtlType.OnDeviceStatus += new ActUtlTypeLib._IActUtlTypeEvents_OnDeviceStatusEventHandler(OnDeviceStatusEvent);

            this.iLogicalStationNumber = iLogicalStationNumber;

            timer.Interval = 1000;
            timer.Elapsed += new System.Timers.ElapsedEventHandler(Timer_Elasped);
            timer.Enabled = true;
        }

        void Timer_Elasped(object sender, System.Timers.ElapsedEventArgs e)
        {
            timer.Enabled = false;

            if (!IsOnline())
            {
                Open(iLogicalStationNumber);
             }

            timer.Enabled = true;
        }

        short[] sInt = new short[100];

        public bool WriteDeviceBlock(string _sAdd, string _str)
        {
            if (_str == "")
            {
                sInt = new short[10];
                Array.Clear(sInt, 0, sInt.Length);
                bool bCheck1 = WriteDeviceBlock2(_sAdd, sInt.Length, ref sInt[0]);
                return bCheck1;
            }

            string str = _str;
            string[] str_temp;

            if (str.Length % 2 == 0)
            {
                str_temp = new string[str.Length / 2];
                sInt = new short[str_temp.Length];

                for (int i = 0; i < str.Length / 2; i++)
                {
                    str_temp[i] = str.Substring(i * 2, 2);
                }

                for (int i = 0; i < str_temp.Length; i++)
                {
                    byte[] bytes = Encoding.ASCII.GetBytes(str_temp[i]);
                    short sh = BitConverter.ToInt16(bytes, 0);
                    sInt[i] = sh;
                }
            }
            else
            {
                str_temp = new string[(str.Length / 2) + 1];
                sInt = new short[str_temp.Length];

                for (int i = 0; i < str.Length / 2 + 1; i++)
                {
                    if (i < (str.Length - 1) / 2)
                        str_temp[i] = str.Substring(i * 2, 2);
                    else
                        str_temp[i] = str.Substring(i * 2, 1);
                }

                for (int i = 0; i < str_temp.Length; i++)
                {
                    if (i < str_temp.Length - 1)
                    {
                        byte[] bytes = Encoding.ASCII.GetBytes(str_temp[i]);
                        short sh = BitConverter.ToInt16(bytes, 0);
                        sInt[i] = sh;
                    }
                    else
                    {
                        char data = Convert.ToChar(str_temp[i].Substring(0, 1));
                        sInt[i] = (short)data;
                    }
                }
            }

            bool bCheck2 = WriteDeviceBlock2(_sAdd, sInt.Length, ref sInt[0]);
            return bCheck2;

        }

        public bool ReadDeviceBlock(string _sAdd, out string _str)
        {
            sInt = new short[10];
            Array.Clear(sInt, 0, sInt.Length);
            bool bCheck = ReadDeviceBlock2(_sAdd, sInt.Length, out sInt[0]);

            _str = "";
            if (bCheck)
            {
                for (int i = 0; i < sInt.Length; i++)
                {
                    byte[] bytes = BitConverter.GetBytes(sInt[i]);
                    _str += Encoding.Default.GetString(bytes);
                }
            }

            return bCheck;
        }

        public int Initialization(int iLogicalStationNumber)
        {
            this.iLogicalStationNumber = iLogicalStationNumber;

            return 0;
        }

        public bool IsOnline()
        {
            int iRst = -1;
            int iData = 0;

            lock (thisLock)
            {
                try
                {
                    iRst = actUtlType.GetDevice("SM400", out iData);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            if (iRst == 0 && iData == 1)
            {
                bConnected = true;
                return true;
            }

            bConnected = false;
            return false;
        }

        public bool Open(int iLogicalStationNumber)
        {
            int iRst = -1;

            this.iLogicalStationNumber = iLogicalStationNumber;

            actUtlType.ActLogicalStationNumber = this.iLogicalStationNumber;

            actUtlType.ActPassword = "";
            lock (thisLock)
            {
                try
                {
                    actUtlType.Close();
                    iRst = actUtlType.Open();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            if (iRst == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void SetError(int _iErrorCode)
        {
            sErrorCode = String.Format("0x{0:x8} [HEX]", iErrorCode);
            iErrorCode = _iErrorCode;
            MessageBox.Show("MxComponent Error" + sErrorCode);
        }

        public bool Close()
        {
            timer.Enabled = false;
            if (!bConnected) return true;

            int iRst = -1;
            lock (thisLock)
            {
                try
                {
                    iRst = actUtlType.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            if (iRst == 0) return true;

            SetError(iRst);
            return false;
        }

        public bool SetDevice(string sDevice, int iData)
        {
            if (!bConnected) return false;

            int iRst = -1;

            lock (thisLock)
            {
                try
                {
                    iRst = actUtlType.SetDevice(sDevice, iData);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            if (iRst == 0) return true;

            SetError(iRst);
            return false;
        }

        public bool GetDevice(string sDevice, out int lplData)
        {
            lplData = 0;

            if (!bConnected) return false;

            int iRst = -1;

            lock (thisLock)
            {
                try
                {
                    iRst = actUtlType.GetDevice(sDevice, out lplData);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            if (iRst == 0) return true;

            SetError(iRst);
            return false;
        }

        public bool ReadDeviceBlock2(string szDevice, int iSize, out short lplData)
        {
            lplData = 0;

            if (!bConnected) return false;

            int iRst = -1;

            lock (thisLock)
            {
                try
                {
                    iRst = actUtlType.ReadDeviceBlock2(szDevice, iSize, out lplData);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            if (iRst == 0) return true;

            SetError(iRst);
            return false;
        }

        public bool WriteDeviceBlock2(string szDevice, int iSize, ref short iData)
        {
            if (!bConnected) return false;

            int iRst = -1;

            lock (thisLock)
            {
                try
                {
                    iRst = actUtlType.WriteDeviceBlock2(szDevice, iSize, ref iData);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            if (iRst == 0) return true;

            SetError(iRst);
            return false;
        }

        public virtual void OnDeviceStatusEvent(String szDevice, int iData, int iReturnCode)
        {
            if (iReturnCode == 0)
            {
                return;
            }
            sErrorCode = String.Format("0x{0:x8} [HEX]", iReturnCode);
            MessageBox.Show(sErrorCode);
            iErrorCode = iReturnCode;
        }
    }
}
