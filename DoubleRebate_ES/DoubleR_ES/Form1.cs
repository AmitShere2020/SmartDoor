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
using DoubleR_ES.Json_Model;
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

            //add client profiles here
            var profiles = Enum.GetNames(typeof(ProfileType));
            comboClientType.DataSource = profiles;

            //add profile types
            var profileTypes = new [] {"Custom", "Non Rated", "Fire Rated Mini", "Fire Rated Maxi"};
            comboProfileType.DataSource = profileTypes;
        }
        
        private void getBtn_Click(object sender, EventArgs e)
        {
            Hide();
            if (folderPath == string.Empty)
            {
             
                Show();
                return;
            }

            List<Json> previousData = new List<Json>();
            string jsonString;
            if (File.Exists(fileStored))
            {
                jsonString = File.ReadAllText(fileStored);
                if (!string.IsNullOrWhiteSpace(jsonString))
                {
                    previousData = JsonConvert.DeserializeObject<JArray>(jsonString).ToObject<List<Json>>();
                }
            }

            GetInputData(); // Read all input here
            
            List<string> dxfFiles = Directory.GetFiles(folderPath, "*.*", SearchOption.TopDirectoryOnly)
                .Where(s => s.EndsWith(".dxf")).ToList();

            if (dxfFiles.Count == 0)
            {
                MessageBox.Show("File not contain any dxf file");
                return; // not working now
            }

            Json readData=JsonActivator(GetProfileType());// not sure using of profile type enum
            foreach (var dxfFile in dxfFiles)
            {
                try
                {
                    readData.FetchDxf(dxfFile);
                    readData.CollectBendData();
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

        private Json JsonActivator(ProfileType profileType)
        {
            switch (profileType)
            {
                case ProfileType.MDF:
                    return new MDFJson(profileType);
                case ProfileType.ADL:
                    break;
            }

            return null;
        }

        private ProfileType GetProfileType()
        {
           return (ProfileType) Enum.Parse(typeof(ProfileType), comboClientType.Text);
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

        private void btnCloning_Click(object sender, EventArgs e)
        {
            Hide();

            GetInputData();

            string jsonString = File.ReadAllText(fileStored);
            List<MDFJson> previousData = JsonConvert.DeserializeObject<JArray>(jsonString).ToObject<List<MDFJson>>();
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

        private Frame InitializeFrame(MDFJson jsonData)
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

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            var folderBrowser = new FolderBrowserDialog { Description = "Select a folder consisting of CAD files." };

            if (folderBrowser.ShowDialog() == DialogResult.OK)
            {
                folderPath = folderBrowser.SelectedPath;
                lblInputPath.Text = folderPath;
            }
        }


        // work on this
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
            changeCheckButton();
        }

        private void txtReturn2_TextChanged(object sender, EventArgs e)
        {
            changeThoratSection();
            changeCheckButton();
        }

        private void txtRebate1_TextChanged(object sender, EventArgs e)
        {
            changeThoratSection();
            changeCheckButton();
        }

        private void txtRebate2_TextChanged(object sender, EventArgs e)
        {
            changeThoratSection();
            changeCheckButton();
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

        private void comboProfileType_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedValue = ((ComboBox) sender).Text;
            string[] profileSubTypes = null;
            if (selectedValue=="Non Rated")
            {
                profileSubTypes = new[] {"NR94", "NR102", "NR114", "NR124", "NR145", "NR152", "NR192"};
                comboSubProfileType.Visible = true;
                comboSubProfileType.DataSource = profileSubTypes;

                txtReturn1.Text = "12";
                txtArchitrave1.Text = "38";
                txtStopHeight1.Text = "15";
                txtRebate1.Text = "41";
                txtRebate2.Text = "41";
                txtStopHeight2.Text = "15";
                txtArchitrave2.Text = "38";
                txtReturn2.Text = "12";
                cbGauge.SelectedIndex = 0;

                

            }
            else if (selectedValue=="Fire Rated Mini")
            {
                comboSubProfileType.Visible = false;
                txtReturn1.Text = "12";
                txtArchitrave1.Text = "38";
                txtStopHeight1.Text = "25";
                txtRebate1.Text = "41";
                txtRebate2.Text = "41";
                txtStopHeight2.Text = "25";
                txtArchitrave2.Text = "38";
                txtReturn2.Text = "12";
                cbGauge.SelectedIndex = 1;

            }
            else if (selectedValue == "Fire Rated Maxi")
            {
                comboSubProfileType.Visible = false;
                txtReturn1.Text = "12";
                txtArchitrave1.Text = "38";
                txtStopHeight1.Text = "25";
                txtRebate1.Text = "51";
                txtRebate2.Text = "51";
                txtStopHeight2.Text = "25";
                txtArchitrave2.Text = "38";
                txtReturn2.Text = "12";
                cbGauge.SelectedIndex = 1;
            }
            else
            {
                comboSubProfileType.Visible = false;
            }

        }

        private void comboSubProfileType_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedSubType = comboSubProfileType.Text;

            switch (selectedSubType)
            {
                case "NR94":
                    txtThroat.Text = 94.ToString();
                    break;
                case "NR102":
                    txtThroat.Text = 102.ToString();
                    break;
                case "NR114":
                    txtThroat.Text = 114.ToString();
                    break;
                case "NR124":
                    txtThroat.Text = 124.ToString();
                    break;
                case "NR145":
                    txtThroat.Text = 145.ToString();
                    break;
                case "NR152":
                    txtThroat.Text = 152.ToString();
                    break;
                case "NR192":
                    txtThroat.Text = 192.ToString();
                    break;
            }
        }

        // attach events to textboxes for custom Profile type only, to perform symmetry and non symmetry operations

    }
}
