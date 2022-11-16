using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Eruka_final.Model;

namespace Eruka_final
{
    public partial class FormHome : Form
    {
        readonly MxComponent PLC = new MxComponent(3);
        readonly CameraProcessing Cam = new CameraProcessing();

        public FormHome()
        {
            InitializeComponent();
            SettingColorFormStartup();
            ConnectDevice();
        }
        private void ConnectDevice()
        {
            //PLC 
            timerCheckStautusDevice.Enabled = true;
            timerCheckStautusDevice.Interval = 500;
            timerCheckStautusDevice.Start();
        }
        private void SettingColorFormStartup()
        {
            var backColor = Color.FromArgb(42, 57, 78);

            var menuColor = Color.FromArgb(33, 45, 63);

            var titleColor = Color.FromArgb(65, 67, 106);

            var nameColor = Color.FromArgb(68, 114, 196);

            this.BackColor = backColor;
            layoutLogo.BackColor = menuColor;
            layoutMenu.BackColor = menuColor;
            layoutHeader.BackColor = menuColor;
            btnStatus.BackColor = titleColor;

            List<Button> listButtonName = new List<Button> { btnInductor, btnCapacitor_680uF, btnLed, btnDiode,btnEC2, btnJacks, btnQ1, btnC1, btnF2, btnCapacitor_2uF, btnCY1, btnTransistor, btnChargingJack, btnScrewHole, btnScrewHole1 };
            foreach (var btn in listButtonName)
            {
                btn.BackColor = nameColor;
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 0;
            }

            List<Button> listButtonValue = new List<Button> { btnInductorValue, btnCapacitor_680uFValue, btnLedValue, btnDiodeValue,btnEC2Value, btnJacksValue, btnQ1Value, btnC1Value, btnF2Value, btnPassValue, btnFailValue, btnTotalValue, btnCapacitor_2uFValue, btnCY1Value, btnTransistorValue, btnChargingJackValue, btnScrewHoleValue, btnScrewHole1Value };
            foreach (var btn in listButtonValue)
            {
                btn.BackColor = titleColor;
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 0;
            }
        }
        private void BtnLogoMit_Click(object sender, EventArgs e)
        {
            this.Close();
            //TODO: Dispose all connect
        }
        private void TimerCheckStautusDevice_Tick(object sender, EventArgs e)
        {
            if (PLC.IsOnline())
            {
                btnPLCStatus.Text = "CONNECTED";
                btnPLCStatus.Image = Eruka_final.Properties.Resources.PLC_CONNECTED;
            }
            else
            {
                btnPLCStatus.Text = "DISCONNECTED";
                btnPLCStatus.Image = Eruka_final.Properties.Resources.disconected_plc;
            }
            if (Cam.Result)
            {
                SetLabelText(btnCamstatus, "CONNECTED");
                btnCamstatus.Image = Eruka_final.Properties.Resources.Cam_Connected;
            }
            else
            {
                SetLabelText(btnCamstatus, "DISCONNECTED");
                btnCamstatus.Image = Eruka_final.Properties.Resources.Cam_Disconnected;
            }
            if(Cam.x==1)
            { 
                ActionCameraResult(Cam.CheckRight); 
            }    
            Set();
            PLC.SetDevice("D200", Cam.PlcCase);
            //btnPLCStatus.Text = Convert.ToString(Cam.PlcCase);
        }

        private void BtnLogoCobra_Click(object sender, EventArgs e)
        {
        //    var isDone = PLC.GetDevice("D200", out int D200);
        //    MessageBox.Show(Convert.ToString(D200));
        }
        private void SetLabelText(Button label, string text)
        {
            if (label.InvokeRequired)
            {
                this.BeginInvoke(new Action(() =>
                {
                    label.Text = text;
                }));

            }
            else
            {
                label.Text = text;
            }
        }
        private void Set()
        {
            SetButtonText(btnInductorValue,Cam.Inductor.ToString());
            SetButtonText(btnCapacitor_680uFValue, Cam.Capacitor_680uF.ToString());
            SetButtonText(btnLedValue, Cam.Led.ToString());
            SetButtonText(btnDiodeValue, Cam.Diode.ToString());
            SetButtonText(btnJacksValue, Cam.Jacks.ToString());
            SetButtonText(btnQ1Value, Cam.Q1.ToString());
            SetButtonText(btnC1Value, Cam.C1.ToString());
            SetButtonText(btnF2Value, Cam.F2.ToString());
            SetButtonText(btnEC2Value, Cam.EC2.ToString());
            SetButtonText(btnCapacitor_2uFValue, Cam.Capacitor_22uF.ToString());
            SetButtonText(btnCY1Value, Cam.CY1.ToString());
            SetButtonText(btnTransistorValue, Cam.Transistor.ToString());
            SetButtonText(btnChargingJackValue, Cam.ChargingJack.ToString());
            SetButtonText(btnScrewHoleValue, Cam.ScrewHole.ToString());
            SetButtonText(btnScrewHole1Value, Cam.ScrewHole1.ToString());
        }
        void ActionCameraResult(bool isRight)
        {
            if (isRight)
            {
                btnPanelstatus.BackColor = Color.SeaGreen;
                btnStatus.BackColor = Color.DarkSlateGray;
                btnStatus.ForeColor = Color.ForestGreen;
                SetButtonText(btnStatus, "PASS");
            }
            else
            {
                btnPanelstatus.BackColor = Color.Red;
                btnStatus.BackColor = Color.Orange;
                btnStatus.ForeColor = Color.Red;
                SetButtonText(btnStatus, "FAIL");
            }
            CountTotalstring = Cam.countTotal.ToString();
            CountPassstring = Cam.countPass.ToString();
            CountFailstring = Cam.countFail.ToString();
        }
        public string CountTotalstring
        {
            get => this.btnTotalValue.Text;
            set
            {
                if (btnTotalValue.InvokeRequired)
                {
                    this.BeginInvoke(new Action(() =>
                    {
                        btnTotalValue.Text = value;
                    }));
                }
                else
                {
                    btnTotalValue.Text = value;
                }
            }
        }
        public string CountPassstring
        {
            get => this.btnPassValue.Text;
            set
            {
                if (btnPassValue.InvokeRequired)
                {
                    this.BeginInvoke(new Action(() =>
                    {
                        btnPassValue.Text = value;
                    }));
                }
                else
                {
                    btnPassValue.Text = value;
                }
            }
        }
        public string CountFailstring
        {
            get => this.btnFailValue.Text;
            set
            {
                if (btnFailValue.InvokeRequired)
                {
                    this.BeginInvoke(new Action(() =>
                    {
                        btnFailValue.Text = value;
                    }));
                }
                else
                {
                    btnFailValue.Text = value;
                }
            }
        }
        public void SetButtonText(Button label, string text)
        {
            if (label.InvokeRequired)
            {
                this.BeginInvoke(new Action(() =>
                {
                    label.Text = text;
                }));
            }
            else
            {
                label.Text = text;
            }
        }
        private void FormHome_Load(object sender, EventArgs e)
        {
            Cam.CheckCam();
        }
    }
}
