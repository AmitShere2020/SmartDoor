using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using devDept.Eyeshot.Entities;
using devDept.Eyeshot.Translators;
using DoubleR_ES.FrameModel;
using DoubleR_ES.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DoubleR_ES
{
    public partial class Form1 : Form
    {
        public string fileStored;

        private string folderPath;

        public Form1()
        {
            InitializeComponent();
            fileStored =Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            folderPath = Path.Combine(fileStored, "MDF Test");
            fileStored = Path.Combine(folderPath, "DoubleRebate.json");
        }

        private void getBtn_Click(object sender, EventArgs e)
        {
            Hide();
            List<JsonData> previousData = new List<JsonData>();
            string jsonString;
            if (File.Exists(fileStored))
            {
                jsonString = File.ReadAllText(fileStored);
                if (!string.IsNullOrWhiteSpace(jsonString))
                {
                    previousData = JsonConvert.DeserializeObject<JArray>(jsonString).ToObject<List<JsonData>>();
                }
            }

            GetInputData(); // Read all input here

            if (folderPath == string.Empty)
            {
                Show();
                return;
            }

            List<string> dxfFiles = Directory.GetFiles(folderPath, "*.*", SearchOption.TopDirectoryOnly)
                .Where(s => s.EndsWith(".dxf")).ToList();

            if (dxfFiles.Count == 0)
            {
                MessageBox.Show("File not contain any dxf file");
                return; // not working now
            }

            JsonData readData=new JsonData(GetProfileType());// not sure using of profile type enum
            foreach (var dxfFile in dxfFiles)
            {
                try
                {
                    readData.FetchDXF(dxfFile);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }

            //Final Data loading
            readData.GaugeSize = Utilities.InputData.GaugeSize;

            var data = previousData.FirstOrDefault(item => item.GaugeSize == Utilities.InputData.GaugeSize);
            if (data != null)
                previousData.Remove(data);
            previousData.Add(readData);

            var filePath = fileStored;
            jsonString = JsonConvert.SerializeObject(previousData, Formatting.Indented);
            File.WriteAllText(filePath, jsonString);

            Show();
        }

        private ProfileType GetProfileType()
        {
           return (ProfileType) Enum.Parse(typeof(ProfileType), comboProfileType.Text);
        }

        private void btnCloning_Click(object sender, EventArgs e)
        {
            Hide();
            GetInputData();

            string jsonString = File.ReadAllText(fileStored);
            List<JsonData> previousData = JsonConvert.DeserializeObject<JArray>(jsonString).ToObject<List<JsonData>>();
            var jsonData = previousData.FirstOrDefault(data => data.GaugeSize == Utilities.InputData.GaugeSize);

            Frame mdFrame = InitializeFrame(jsonData);

            mdFrame.CreateHingeView();
            var hingeEntities = mdFrame.HingEntities;
            Utilities.CloningDxf(hingeEntities);

            
            mdFrame.CreateLockView();
            var lockEntities = mdFrame.LockEntities;
            Utilities.CloningDxf(lockEntities);

            mdFrame.CreateTopView();
            var topEntities = mdFrame.TopEntities;
            Utilities.CloningDxf(topEntities);
            
            MessageBox.Show("Operation completed");

            Show();
        }

        private Frame InitializeFrame(JsonData jsonData)
        {
            Frame frame = null;
            switch (jsonData.ProfileType)
            {
                case ProfileType.MDF:
                    frame = new MDFFrame(jsonData);
                    break;
                case ProfileType.ADL:
                    frame = new ADLFrame(jsonData);
                    break;
            }

            return frame;
        }

        private void GetInputData()
        {
            var inputData = new InputData
            {
                Return1 = Convert.ToDouble(txtReturn1.Text),
                Architrave1 = Convert.ToDouble(txtArchitrave1.Text),
                Rebate1 = Convert.ToDouble(txtRebate1.Text),
                StopHgt1 = Convert.ToDouble(txtStopHeight1.Text),
                StopSection = Convert.ToDouble(txtStopSection.Text),
                StopHgt2 = Convert.ToDouble(txtStopHeight2.Text),
                Rebate2 = Convert.ToDouble(txtRebate2.Text),
                Architrave2 = Convert.ToDouble(txtArchitrave2.Text),
                Return2 = Convert.ToDouble(txtReturn2.Text),
                Throat = Convert.ToDouble(txtThroat.Text),
                FrameQty = Convert.ToInt16(txtFrameQty.Text),
                GaugeSize = Convert.ToDouble(cbGauge.Text),
                RevealHeight = Convert.ToDouble(txtRevealHgt.Text),
                RevealWidth = Convert.ToDouble(txtRevealWidth.Text),
                HingePrep = cbHingePrep.Text,
                HingeQty = Convert.ToInt16(cbHingeQty.Text),
                StrikePrep = cbStrikePrep.Text,
                StrikeHeight = Convert.ToDouble(txtStrikeHgt.Text),
                FrameFixing = cbFrameFixing.Text,
                TabBase = Convert.ToDouble(txtTabBase.Text),
                TabTop = Convert.ToDouble(txtTabTop.Text)
            };

            Utilities.InputData = inputData;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Input data

            txtFrameQty.Text = "1";

            var gaugeSizeList = new List<string>
            {
                "1.1", "1.5", "2"
            };
            cbGauge.DataSource = gaugeSizeList;
            cbGauge.SelectedIndex = 0;

            txtRevealHgt.Text = "2500";
            txtRevealWidth.Text = "950";

            var hingePrepList = new List<string>
            {
                "SFH", "NA"
            };
            cbHingePrep.DataSource = hingePrepList;
            cbHingePrep.SelectedIndex = 0;

            var hingeQtyList = new List<string>
            {
                "2", "3", "4", "5"
            };
            cbHingeQty.DataSource = hingeQtyList;
            cbHingeQty.SelectedIndex = 0;

            var strikePrepList = new List<string>
            {
                "UNI", "NA"
            };
            cbStrikePrep.DataSource = strikePrepList;
            cbStrikePrep.SelectedIndex = 0;

            txtStrikeHgt.Text = "1025";

            var frameFixedList = new List<string>
            {
                "D/B-3.5", "D/B-7.0", "NA"
            };

            cbFrameFixing.DataSource = frameFixedList;
            cbFrameFixing.SelectedIndex = 0;

            txtTabBase.Text = "15";
            txtTabTop.Text = "10";

            //  Profile Input data

            txtReturn1.Text = "15";
            txtArchitrave1.Text = "38";
            txtRebate1.Text = "41";
            txtStopHeight1.Text = "25";
            txtStopSection.Text = "83";
            txtStopHeight2.Text = "25";
            txtRebate2.Text = "41";
            txtArchitrave2.Text = "38";
            txtReturn2.Text = "15";
            txtThroat.Text = "135";

            //
            chkSymmetry.Checked = true;
            lblInputPath.Text = folderPath;

            //add profiles here
            var profiles = Enum.GetNames(typeof(ProfileType));
            comboProfileType.DataSource = profiles;
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            var folderBrowser = new FolderBrowserDialog { Description = "Select a folder consisting of CAD files." };

            if (folderBrowser.ShowDialog() == DialogResult.OK)
            {
                folderPath = folderBrowser.SelectedPath;
                lblInputPath.Text = folderPath;
            }
        }

        private void chkSymmetry_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSymmetry.Checked) changeCheckButton();
        }

        #region Textbox Control

        public void changeCheckButton()
        {
            txtArchitrave2.Text = txtArchitrave1.Text;
            txtRebate2.Text = txtRebate1.Text;
            txtStopHeight2.Text = txtStopHeight1.Text;
        }

        public void changeThoratSection()
        {
            if (!string.IsNullOrEmpty(txtThroat.Text) && !string.IsNullOrEmpty(txtReturn1.Text) &&
                !string.IsNullOrEmpty(txtRebate1.Text))
            { var text1 = Convert.ToDouble(txtRebate1.Text) + Convert.ToDouble(txtRebate2.Text) +
                          Convert.ToDouble(txtStopSection.Text) - Convert.ToDouble(txtReturn1.Text) -
                          Convert.ToDouble(txtReturn2.Text);
                txtThroat.Text = text1.ToString(CultureInfo.InvariantCulture);
            }
        }

        private void txtReturn1_TextChanged(object sender, EventArgs e)
        {
            changeThoratSection();
        }

        private void txtReturn2_TextChanged(object sender, EventArgs e)
        {
            changeThoratSection();
        }

        private void txtRebate1_TextChanged(object sender, EventArgs e)
        {
            changeThoratSection();
        }

        private void txtRebate2_TextChanged(object sender, EventArgs e)
        {
            changeThoratSection();
        }


        public void ChangeArchi()
        {
            if (!string.IsNullOrEmpty(txtArchitrave1.Text))
            {
                var text2 = Convert.ToDouble(txtArchitrave1.Text);
                txtArchitrave2.Text = text2.ToString(CultureInfo.InvariantCulture);
            }
        }

        #endregion
    }
}
