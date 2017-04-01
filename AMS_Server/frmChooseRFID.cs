using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AMS_Server
{
    
    public partial class frmChooseRFID : Form
    {
        clsData_Access DataAccess;
        DataSet DS = new DataSet();
        public string rfid = "";
        public frmChooseRFID()
        {
            InitializeComponent();
            SelectDevice();
            establishContext();
            DataAccess = new clsData_Access("server=127.0.0.1;user id=root;password=;database=sms_app");
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            tmrRead.Enabled = false;
            rfid = lblRFID.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            tmrRead.Enabled = false;
            this.Close();
        }



















        /*
         * 
         * ACR 1222U 
         * 
         */

        int retCode;
        int hCard;
        int hContext;
        int Protocol;
        public bool connActive = false;
        string readername = "ACS ACR122 0";      // change depending on reader
        public byte[] SendBuff = new byte[263];
        public byte[] RecvBuff = new byte[263];
        public int SendLen, RecvLen, nBytesRet, reqType, Aprotocol, dwProtocol, cbPciLength;
        ACR122uCard.SCARD_READERSTATE RdrState;
        ACR122uCard.SCARD_IO_REQUEST pioSendRequest;
        private int id;
        private string cardno;

        public void SelectDevice()
        {
            //MessageBox.Show(this.ListReaders().Count.ToString());
            if (this.ListReaders().Count > 0)
            {
                List<string> availableReaders = this.ListReaders();
                this.RdrState = new ACR122uCard.SCARD_READERSTATE();
                readername = availableReaders[0].ToString();//selecting first device
                this.RdrState.RdrName = readername;
                if (retCode == 0)
                {
                    //MessageBox.Show("Device successfully connected.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    //MessageBox.Show(ACR122uCard.GetScardErrMsg(retCode).ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                //MessageBox.Show(ACR122uCard.GetScardErrMsg(retCode).ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public List<string> ListReaders()
        {
            int ReaderCount = 0;
            List<string> AvailableReaderList = new List<string>();

            //Make sure a context has been established before 
            //retrieving the list of smartcard readers.
            retCode = ACR122uCard.SCardListReaders(hContext, null, null, ref ReaderCount);
            if (retCode != ACR122uCard.SCARD_S_SUCCESS)
            {
                //MessageBox.Show(ACR122uCard.GetScardErrMsg(retCode));
                //connActive = false;
            }

            byte[] ReadersList = new byte[ReaderCount];

            //Get the list of reader present again but this time add sReaderGroup, retData as 2rd & 3rd parameter respectively.
            retCode = ACR122uCard.SCardListReaders(hContext, null, ReadersList, ref ReaderCount);
            if (retCode != ACR122uCard.SCARD_S_SUCCESS)
            {
                //MessageBox.Show(ACR122uCard.GetScardErrMsg(retCode));
            }

            string rName = "";
            int indx = 0;
            if (ReaderCount > 0)
            {
                // Convert reader buffer to string
                while (ReadersList[indx] != 0)
                {

                    while (ReadersList[indx] != 0)
                    {
                        rName = rName + (char)ReadersList[indx];
                        indx = indx + 1;
                    }

                    //Add reader name to list
                    AvailableReaderList.Add(rName);
                    rName = "";
                    indx = indx + 1;

                }
            }
            return AvailableReaderList;

        }

        internal void establishContext()
        {
            retCode = ACR122uCard.SCardEstablishContext(ACR122uCard.SCARD_SCOPE_SYSTEM, 0, 0, ref hContext);
            if (retCode != ACR122uCard.SCARD_S_SUCCESS)
            {
                //MessageBox.Show("Check your device and please restart again", "Reader not connected");
                connActive = false;
                return;
            }
        }
        public bool connectCard()
        {

            connActive = true;

            retCode = ACR122uCard.SCardConnect(hContext, readername, ACR122uCard.SCARD_SHARE_SHARED,
                      ACR122uCard.SCARD_PROTOCOL_T0 | ACR122uCard.SCARD_PROTOCOL_T1, ref hCard, ref Protocol);


            if (retCode != ACR122uCard.SCARD_S_SUCCESS)
            {
                //MessageBox.Show(Card.GetScardErrMsg(retCode), "Card not available");
                //this.Text = ACR122uCard.GetScardErrMsg(retCode).ToString() + " " + "Card not available " + retCode.ToString();
                connActive = false;
                return false;


            }

            return true;

        }

        private string getcardUID()//only for mifare 1k cards
        {
            string cardUID = "";
            byte[] receivedUID = new byte[256];
            ACR122uCard.SCARD_IO_REQUEST request = new ACR122uCard.SCARD_IO_REQUEST();
            request.dwProtocol = ACR122uCard.SCARD_PROTOCOL_T1;
            request.cbPciLength = System.Runtime.InteropServices.Marshal.SizeOf(typeof(ACR122uCard.SCARD_IO_REQUEST));
            byte[] sendBytes = new byte[] { 0xFF, 0xCA, 0x00, 0x00, 0x00 }; //get UID command      for Mifare cards
            int outBytes = receivedUID.Length;
            int status = ACR122uCard.SCardTransmit(hCard, ref request, ref sendBytes[0], sendBytes.Length, ref request, ref receivedUID[0], ref outBytes);

            if (status != ACR122uCard.SCARD_S_SUCCESS)
            {
                //cardUID = "Error";
                cardUID = "";
            }
            else
            {
                cardUID = BitConverter.ToString(receivedUID.Take(4).ToArray()).Replace("-", string.Empty).ToLower();
            }

            return cardUID;
        }

        private void tmrRead_Tick(object sender, EventArgs e)
        {
            if (connectCard())
            {
                string cardUID = getcardUID();
                //this.Text = cardUID; //displaying on text block
                if (cardUID != "63000000")
                {
                    lblRFID.Text = cardUID;
                    
                    //tmrTagReader.Enabled = false;
                }
            }
            else if (!connectCard() && (retCode < 0 && retCode != -2146434967))
            {
                SelectDevice();
                establishContext();

                this.Text = "Reader not connected";
                tmrRead.Enabled = false;
                MessageBox.Show(this,"Please plug in device and reopen this form.","Device not connected",MessageBoxButtons.OK,MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                this.Text = "Reader connected";
            }
        }

        private void frmChooseRFID_Load(object sender, EventArgs e)
        {
            tmrRead.Enabled = true;
        }

        private void lblRFID_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DS = DataAccess.executeQuery("select rfid_id from tbl_rfid_tag where rfid_tag_id='" + lblRFID.Text + "' and rfid_tag_active='1'");

                if (DS.Tables[0].Rows.Count > 0)
                {
                    lblMessage.Text = "Tag is already in used.\nSelect another";
                    btnOk.Enabled = false;
                }
                else
                {
                    lblMessage.Text = "";
                    btnOk.Enabled = true;
                }
            }
            catch { 
                
            }
        }




    }
}
