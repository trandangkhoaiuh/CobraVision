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
  public partial class FormLogin : Form
  {
    string[] username = { "Phoenixteam", "Admin", "KhoaTran", "User", "user123", "a" ,"Phong","Hưng",};
    string[] userpass = { "123456789", "123", "TranDangKhoa", "123", "123", "a" ,"1111","1111"};
    string[] phanquyen = { "Manager", "Admin", "Admin", "User", "User", "Admin","Engineer" ,"Engineer"};
    string[] fullname = { "Phoenix Team", "Ngo Thanh Quyen", "Tran Dang Khoa", "Van Hanh 1", "Van Hanh 2", "Tran Dang Khoa","Nguyen Hong Phong","Nguyen An Hung" };
    public string rightUserName;
    public string rightPhanquyen;
    public string rightName;

    public FormLogin()
    {
      InitializeComponent();
    }

    private void button1_Click(object sender, EventArgs e)
    {
      if (txtName.Text == "" || txtPass.Text == "")
      {
        MessageBox.Show("Please import Username and Password");
      }
      else
      {
        rightPhanquyen = checkuser(txtName.Text, txtPass.Text, out rightName);
        rightUserName = txtName.Text;
        this.DialogResult = DialogResult.OK;
      }

    }

    private void label2_Click(object sender, EventArgs e)
    {
      this.DialogResult = DialogResult.Cancel;
    }

    string checkuser(string name, string passw, out string namee)
    {
      bool rightPass = false;
      string quyen = "";
      namee = "";
      for (int i = 0; i < username.Length; i++)
      {
        if (name == username[i])
        {
          if (passw == userpass[i])
          {
            rightPass = true;
            quyen = phanquyen[i];
            namee = fullname[i];
          }
        }
      }

      if (rightPass) MessageBox.Show("Wellcome " + namee);
      else MessageBox.Show("Username or password is wrong" + Environment.NewLine +
        "Please check again");


      return quyen;
    }

    private void button2_Click(object sender, EventArgs e)
    {
      
      
    }
  }
}
