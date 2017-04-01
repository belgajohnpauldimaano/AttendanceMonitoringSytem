using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;

namespace AMS_Server
{
    public partial class frmAddTag : Form
    {

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
                    MessageBox.Show("Device successfully connected.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(ACR122uCard.GetScardErrMsg(retCode).ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show(ACR122uCard.GetScardErrMsg(retCode).ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                cardUID = "Error";
            }
            else
            {
                cardUID = BitConverter.ToString(receivedUID.Take(4).ToArray()).Replace("-", string.Empty).ToLower();
            }

            return cardUID;
        }
















        //API STARTS
        // Error Return DEscription

        private const byte OK = 0x00;
        private bool fIsAppClose = false;
        private const byte lengthError = 0x01;
        private const byte operationNotSupport = 0x02;
        private const byte dataRangError = 0x03;
        private const byte cmdNotOperation = 0x04;
        private const byte rfClosed = 0x05;
        private const byte EEPROM = 0x06;
        private const byte timeOut = 0x0a;
        private const byte moreUID = 0x0b;
        private const byte ISOError = 0x0c;
        private const byte noElectronicTag = 0x0e;
        private const byte operationError = 0x0f;
        private const byte cmdNotSupport = 0x01;
        private const byte cmdNotIdentify = 0x02;
        private const byte errOperationNotSupport = 0x03;
        private const byte unknownError = 0x0f;
        private const byte blockError = 0x10;
        private const byte blockLockedCntLock = 0x11;
        private const byte blockLockedCntWrite = 0x12;
        private const byte blockCntOperate = 0x13;
        private const byte blockCntLock = 0x14;
        private const byte communicationErr = 0x30;
        private const byte retCRCErr = 0x31;
        private const byte retDataErr = 0x32;
        private const byte communicationBusy = 0x33;
        private const byte executeCmdBusy = 0x34;
        private const byte comPortOpened = 0x35;
        private const byte comPortClose = 0x36;
        private const byte invalidHandle = 0x37;
        private const byte invalidPort = 0x38;

        private string DL810_00GetReturnCodeDesc(int cmdRet)
        {
            switch (cmdRet)
            {
                case lengthError:
                    return "Command operand length error";
                case operationNotSupport:
                    return "Command not supported";
                case dataRangError:
                    return "Operation data range error";
                case rfClosed:
                    return "RF field is inactive";
                case EEPROM:
                    return "EEPROM operation error";
                case timeOut:
                    return "Inventory-Scan-Time overflow";
                case moreUID:
                    return "Not all tag's UID are collected before the Inventory-Scan-Time overflow";
                case ISOError:
                    return "ISO error";
                case noElectronicTag:
                    return "No Tags";
                case operationError:
                    return "Operation error";
                case communicationErr:
                    return "Communication Error";
                case retCRCErr:
                    return "CRC checksummat Error";
                case communicationBusy:
                    return "Communication busy";
                case cmdNotOperation:
                    return "CMD can't execute at current";
                case comPortOpened:
                    return "Port Opend";
                case comPortClose:
                    return "Port Already Closed";
                case invalidHandle:
                    return "Invalid PortHandle";
                case invalidPort:
                    return "Invalid ComPort ";
                case OK:
                    return "successfully";
                default:
                    return "";
            }
        }

        private string ByteArrayToHexString(byte[] data)
        {
            StringBuilder sb = new StringBuilder(data.Length * 3);
            foreach (byte b in data)
                sb.Append(Convert.ToString(b, 16).PadLeft(2, '0').PadRight(3, ' '));
            return sb.ToString().ToUpper();
        }


        private byte readerAdd = 0xff;
        private int fcmdRet = 0x30;
        private int portNum = -1;
        private int portIndex = -1;

        //API ENDS


        public frmAddTag()
        {
            InitializeComponent();

            SelectDevice();
            establishContext();


            btnTagSave.Image = Z.IconLibrary.FarmFresh.Icon.SaveData.GetImage16();
            //btnCloseOpenDevice.Image = Z.IconLibrary.Fugue.Icon.PriceTagLabel.GetImage();
            btnTagCancel.Image = Z.IconLibrary.Fugue.Icon.TagMinus.GetImage();
        }

        private void frmAddTag_Load(object sender, EventArgs e)
        {

        }

        private void btnCloseOpenDevice_Click(object sender, EventArgs e)
        {
            //tmrTagReader.Enabled = true;
            /*
            if (btnCloseOpenDevice.Text == "Open Device")
            {
                fcmdRet = StaticClassReaderA.AutoOpenComPort(ref portNum, ref readerAdd, ref portIndex);
                if (fcmdRet == 0)
                {
                    btnCloseOpenDevice.Text = "Close Device";
                    MessageBox.Show(this, "Device successfully opened. Device are now ready to read a tag." , "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    tmrTagReader.Enabled = true;
                    gbTag.Enabled = true;
                }
                else
                {
                    MessageBox.Show(this, DL810_00GetReturnCodeDesc(fcmdRet), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                fcmdRet = StaticClassReaderA.CloseComPort();
                if (fcmdRet == 0)
                {
                    btnCloseOpenDevice.Text = "Open Device";
                    MessageBox.Show(this, "Device successfully closed", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    tmrTagReader.Enabled = false;
                    gbTag.Enabled = false;
                }
                else
                {
                    MessageBox.Show(this, DL810_00GetReturnCodeDesc(fcmdRet), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }*/
        }

        private void tmrTagReader_Tick(object sender, EventArgs e)
        {
            if (connectCard())
            {
                string cardUID = getcardUID();
                //this.Text = cardUID; //displaying on text block
                if (cardUID != "63000000")
                {
                    tbTagID.Text = cardUID;
                    //tmrTagReader.Enabled = false;
                }
            }
            else if (!connectCard() && (retCode < 0 && retCode != -2146434967))
            {
                SelectDevice();
                establishContext();
            }




            /*
             * 
             * OLD DEVICE
             * 
            byte state = 0;
            byte AFI = 0;
            byte[] DSFIDAndUID = new byte[9];
            byte cardNumber = 0;
            string strDSFIDAndUID = "";

            fcmdRet = StaticClassReaderA.Inventory(ref readerAdd, ref state, ref AFI, DSFIDAndUID, ref cardNumber, portIndex);
            
            if (fcmdRet == 0)
            {

                strDSFIDAndUID = ByteArrayToHexString(DSFIDAndUID).Replace(" ", "");
                System.Threading.Thread.Sleep(200);
                tbTagID.Text = strDSFIDAndUID;
                tmrTagReader.Enabled = false;
            }
             */
        }

        private void btnTagSave_Click(object sender, EventArgs e)
        {
            if (tbTagID.Text != "")
            {
                clsData_Access DataAccess = new clsData_Access("server=127.0.0.1;user id=root;password=;database=sms_app");
                DataSet ds = new DataSet();
                ds = DataAccess.executeQuery("select * from tbl_rfid_tag where rfid_tag_id ='" + tbTagID.Text + "' and rfid_tag_active=1");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    MessageBox.Show(this, "Tag is currently existing and active.", "Existing", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    tbTagID.Text = "";
                    tmrTagReader.Enabled = true;
                    return;
                }

                if (DataAccess.dbConnected)
                {
                    //MessageBox.Show(this, "Connection successfully established.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (DataAccess.ExecuteProcedureCommand_Insert_Tag(tbTagID.Text))
                    {
                        MessageBox.Show(this, "Tag successfully saved", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show(this, "Tag not saved", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }



                tbTagID.Text = "";
                //tmrTagReader.Enabled = true;
            }
        }

        private void btnTagCancel_Click(object sender, EventArgs e)
        {
            tbTagID.Text = "";
            tmrTagReader.Enabled = true;
        }
    }
}
