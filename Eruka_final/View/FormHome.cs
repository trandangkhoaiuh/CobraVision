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
        readonly CameraProcessing Cam=new CameraProcessing();

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

            List<Button> listButtonName = new List<Button> { btnInductor, btnCapacitor_680uF, btnLed, btnDiode, btnJacks, btnQ1, btnC1, btnF2, btnCapacitor_2uF, btnCY1, btnTransistor, btnChargingJack, btnScrewHole, btnScrewHole1 };
            foreach (var btn in listButtonName)
            {
                btn.BackColor = nameColor;
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 0;
            }

            List<Button> listButtonValue = new List<Button> { btnInductorValue, btnCapacitor_680uFValue, btnLedValue, btnDiodeValue, btnJacksValue, btnQ1Value, btnC1Value, btnF2Value, btnPassValue, btnFailValue, btnTotalValue, btnCapacitor_2uFValue, btnCY1Value, btnTransistorValue, btnChargingJackValue, btnScrewHoleValue, btnScrewHole1Value };
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
            if(Cam.StartCam())
            {
                SetLabelText(btnCamstatus, "CONNECTED");
            } 
            else
            {
                SetLabelText(btnCamstatus, "DISCONNECTED");
            }    
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

        
    }
}
