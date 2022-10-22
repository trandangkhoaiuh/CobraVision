using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Eruka_final
{
  public partial class FormSetting : Form
  {
    public FormSetting()
    {
      InitializeComponent();
    }

    FormHome frmhome2 = new FormHome();
    string[] CameraIP = { "127.0.0.1", "192.168.186.1", "192.168.3.123" };
    string[] CameraPort = { "3000", "3000", "3000" };
    string[] PLCnumber = { "25", "20", "25" };
    private void FormSetting_Load(object sender, EventArgs e)
    {
      cbPLCnumber.SelectedIndex = 0;
      cbCameraPort.SelectedIndex = 0;
      cbCameraIP.SelectedIndex = 0;
      
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      
      this.DialogResult = DialogResult.Cancel;
    }


    public string cameraIP;
    public string cameraPort;
    public int ComponentNumber;
    private void btnSave_Click(object sender, EventArgs e)
    {
      ComponentNumber = Int32.Parse(txtPLCNumber.Text);
      cameraIP = txtCameraIp.Text;
      cameraPort = txtCameraPort.Text;
     
      this.DialogResult = DialogResult.OK;
      
    }

    private void cbCameraIP_SelectedIndexChanged(object sender, EventArgs e)
    {
      txtCameraIp.Text = CameraIP[cbCameraIP.SelectedIndex];
    }

    private void cbCameraPort_SelectedIndexChanged(object sender, EventArgs e)
    {
      txtCameraPort.Text = CameraPort[cbCameraPort.SelectedIndex];
    }

    private void cbPLCnumber_SelectedIndexChanged(object sender, EventArgs e)
    {
      txtPLCNumber.Text = PLCnumber[cbPLCnumber.SelectedIndex];
    }
  }
}
