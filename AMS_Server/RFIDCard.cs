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
    public partial class RFIDCard : Form
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
        public ACR122uCard.SCARD_READERSTATE RdrState;
        public ACR122uCard.SCARD_IO_REQUEST pioSendRequest;
        private int id;
        private string cardno;





        public void SelectDevice()
        {
            List<string> availableReaders = this.ListReaders();
            this.RdrState = new ACR122uCard.SCARD_READERSTATE();
            readername = availableReaders[0].ToString();//selecting first device
            this.RdrState.RdrName = readername;

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
                MessageBox.Show("Check your device and please restart again", "Reader not connected");
                connActive = false;
                return;
            }
        }
        public bool connectCard()
        {

            connActive = true;

            retCode = ACR122uCard.SCardConnect(hContext, readername, ACR122uCard.SCARD_SHARE_SHARED,
                      ACR122uCard.SCARD_PROTOCOL_T0 | ACR122uCard.SCARD_PROTOCOL_T1, ref hCard, ref Protocol);

            //MessageBox.Show(retCode.ToString());
            if (retCode != ACR122uCard.SCARD_S_SUCCESS)
            {
                //MessageBox.Show(ACR122uCard.GetScardErrMsg(retCode), "ACR122uCard not available");

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



        public RFIDCard()
        {
            InitializeComponent();
            SelectDevice();
            establishContext();

        }

        private void RFIDCard_Load(object sender, EventArgs e)
        {
            if (connectCard())
            {
                string cardUID = getcardUID();
                MessageBox.Show(cardUID);
            }
        }

        private void RFIDCard_Click(object sender, EventArgs e)
        {
            if (connectCard())
            {
                string cardUID = getcardUID();
                MessageBox.Show(cardUID);
            }
        }
    }
}
