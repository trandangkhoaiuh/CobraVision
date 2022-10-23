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

        //Edit something
        readonly private string cameraIP;
        readonly private string cameraPort;
        
        public CameraProcessing()
        {
             cameraIP = "192.168.137.190";
             cameraPort = "3000";
             
        }
        public bool StartCam()
        {
            bool result;
            SimpleTcpClient client = new SimpleTcpClient(cameraIP + ":" + cameraPort);
            client.Events.Connected += Camera_Connected;
            client.Events.DataReceived += Camera_DataReceived;
            try
            {
                client.Connect();
                result = true;
            }
            catch (Exception loi)
            {
                result = false;
                MessageBox.Show(loi.ToString());
            }
            return result;
        }
        private void Camera_DataReceived(object sender, DataReceivedEventArgs e)
        {
            //TODO: Handle DataReceived
        }
        private void Camera_Connected(object sender, ConnectionEventArgs e)
        {
            //TODO: Webwbrowse referesh
        }




    }
    
}
