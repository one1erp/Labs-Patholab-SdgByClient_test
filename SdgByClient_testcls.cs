using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using LSExtensionControlLib;
using LSExtensionExplorer;
using LSSERVICEPROVIDERLib;
using Patholab_Common;
using Patholab_DAL;



namespace SdgByClient_test
{

 
    public partial class SdgByClient_testcls : UserControl, ILSXplVisualControl
    {

        #region members

    

        private INautilusProcessXML nautilusProcessXML;

        private INautilusDBConnection ntlCon;

        /// <summary>
        ///     Id of the record selected
        /// </summary>
        private double recordID;

        /// <summary>
        ///     Service Provider object
        /// </summary>
        private LSSERVICEPROVIDERLib.NautilusServiceProvider serviceProvider;
        #endregion



        public SdgByClient_testcls()
        {
            InitializeComponent();
        }

        public void PreDisplay()
        {
           
         
            ExceptionThrown += LSExtensionExpl_ExceptionThrown;
   
            if (recordID != 0)
            {


                DataLayer d = new DataLayer();
                d.Connect(ntlCon);
                var sdg = d.GetAllSdg().Where(x => x.SDG_USER.U_PATIENT == recordID).ToList();

                listBox1.Items.Clear();

                foreach (var item in sdg)
                {
                    listBox1.Items.Add(item.NAME + "       " + item.STATUS);
                }
                d.Close();
            }
     

        }

   
        void LSExtensionExpl_ExceptionThrown(object sender, Exception e)
        {
            MessageBox.Show("Error");
        }

        public void ChangeDataExplorerView(DataExplorerViewStyles style)
        {
            //throw new NotImplementedException();
        }

        public string GetObjectsStaticItemText()
        {
            //throw new NotImplementedException();
            return "12";
        }

        public void BeforeFocusedNodeChange(string keyData)
        {
            //throw new NotImplementedException();
        }
        public void FocusedNodeChanged(string keyData)
        {
            try
            {

        
           
            string s = keyData.Split('/')[0];
            recordID = double.Parse(s);
            if (recordID != 0)
            {
                PreDisplay();
            }
       

            }
            catch (Exception e)
            {

                throw;
            }
        }

        public void NeedRefresh(string keyData, params string[] parameters)
        {
            //throw new NotImplementedException();
        }

        public void ProcessToolbarItemClick(ToolbarItem item)
        {
     

            //throw new NotImplementedException();
        }

        public void DataExplorerToolbarButtonClicked(ToolbarItem item)
        {
            //throw new NotImplementedException();
        }
        private IExtensionControlSite site;
        public void SetServiceProvider(object sp)
        {

            if (sp != null)
            {
                // Cast the object to the correct type
                serviceProvider = (NautilusServiceProvider)sp;

                // Using the service provider object get the XML Processor interface
                // We will use this object to get information from the database
                nautilusProcessXML = Utils.GetXmlProcessor(serviceProvider);


                ntlCon = Utils.GetNtlsCon(serviceProvider);
            }

        }

        public void InitializeToolbarItemsStatus(ref Hashtable toolbarItems)
        {
            //throw new NotImplementedException();
        }

        public event ExceptionThrownEventHandler ExceptionThrown;

  




    }
}