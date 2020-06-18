namespace DoubleR_ES
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.getBtn = new System.Windows.Forms.Button();
            this.btnCloning = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cbStrikePrep = new System.Windows.Forms.ComboBox();
            this.cbFrameFixing = new System.Windows.Forms.ComboBox();
            this.cbHingeQty = new System.Windows.Forms.ComboBox();
            this.cbHingePrep = new System.Windows.Forms.ComboBox();
            this.cbGauge = new System.Windows.Forms.ComboBox();
            this.txtFrameQty = new System.Windows.Forms.TextBox();
            this.txtTabTop = new System.Windows.Forms.TextBox();
            this.txtTabBase = new System.Windows.Forms.TextBox();
            this.txtRevealHgt = new System.Windows.Forms.TextBox();
            this.txtRevealWidth = new System.Windows.Forms.TextBox();
            this.txtStrikeHgt = new System.Windows.Forms.TextBox();
            this.StrikeHeight = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.Gauge = new System.Windows.Forms.Label();
            this.FrameFixed = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.RevealHeight = new System.Windows.Forms.Label();
            this.Quantity = new System.Windows.Forms.Label();
            this.RevealWidth = new System.Windows.Forms.Label();
            this.txtStopSection = new System.Windows.Forms.TextBox();
            this.txtStopHeight2 = new System.Windows.Forms.TextBox();
            this.txtRebate2 = new System.Windows.Forms.TextBox();
            this.txtArchitrave2 = new System.Windows.Forms.TextBox();
            this.txtArchitrave1 = new System.Windows.Forms.TextBox();
            this.txtRebate1 = new System.Windows.Forms.TextBox();
            this.txtStopHeight1 = new System.Windows.Forms.TextBox();
            this.txtReturn2 = new System.Windows.Forms.TextBox();
            this.txtThroat = new System.Windows.Forms.TextBox();
            this.txtReturn1 = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.comboClientType = new System.Windows.Forms.ComboBox();
            this.chkSymmetry = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.lblInputPath = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.comboProfileType = new System.Windows.Forms.ComboBox();
            this.comboSubProfileType = new System.Windows.Forms.ComboBox();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // getBtn
            // 
            this.getBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.getBtn.Location = new System.Drawing.Point(88, 14);
            this.getBtn.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.getBtn.Name = "getBtn";
            this.getBtn.Size = new System.Drawing.Size(75, 30);
            this.getBtn.TabIndex = 0;
            this.getBtn.Text = "Read DXF";
            this.getBtn.UseVisualStyleBackColor = true;
            this.getBtn.Click += new System.EventHandler(this.getBtn_Click);
            // 
            // btnCloning
            // 
            this.btnCloning.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCloning.Location = new System.Drawing.Point(332, 14);
            this.btnCloning.Name = "btnCloning";
            this.btnCloning.Size = new System.Drawing.Size(75, 30);
            this.btnCloning.TabIndex = 2;
            this.btnCloning.Text = "Create DXF";
            this.btnCloning.UseVisualStyleBackColor = true;
            this.btnCloning.Click += new System.EventHandler(this.btnCloning_Click);
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.cbStrikePrep);
            this.panel2.Controls.Add(this.cbFrameFixing);
            this.panel2.Controls.Add(this.cbHingeQty);
            this.panel2.Controls.Add(this.cbHingePrep);
            this.panel2.Controls.Add(this.cbGauge);
            this.panel2.Controls.Add(this.txtFrameQty);
            this.panel2.Controls.Add(this.txtTabTop);
            this.panel2.Controls.Add(this.txtTabBase);
            this.panel2.Controls.Add(this.txtRevealHgt);
            this.panel2.Controls.Add(this.txtRevealWidth);
            this.panel2.Controls.Add(this.txtStrikeHgt);
            this.panel2.Controls.Add(this.StrikeHeight);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.Gauge);
            this.panel2.Controls.Add(this.FrameFixed);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.RevealHeight);
            this.panel2.Controls.Add(this.Quantity);
            this.panel2.Controls.Add(this.RevealWidth);
            this.panel2.Location = new System.Drawing.Point(20, 44);
            this.panel2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(219, 423);
            this.panel2.TabIndex = 24;
            // 
            // cbStrikePrep
            // 
            this.cbStrikePrep.FormattingEnabled = true;
            this.cbStrikePrep.Location = new System.Drawing.Point(129, 236);
            this.cbStrikePrep.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cbStrikePrep.Name = "cbStrikePrep";
            this.cbStrikePrep.Size = new System.Drawing.Size(76, 21);
            this.cbStrikePrep.TabIndex = 4;
            // 
            // cbFrameFixing
            // 
            this.cbFrameFixing.FormattingEnabled = true;
            this.cbFrameFixing.Location = new System.Drawing.Point(128, 309);
            this.cbFrameFixing.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cbFrameFixing.Name = "cbFrameFixing";
            this.cbFrameFixing.Size = new System.Drawing.Size(76, 21);
            this.cbFrameFixing.TabIndex = 4;
            // 
            // cbHingeQty
            // 
            this.cbHingeQty.FormattingEnabled = true;
            this.cbHingeQty.Location = new System.Drawing.Point(129, 199);
            this.cbHingeQty.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cbHingeQty.Name = "cbHingeQty";
            this.cbHingeQty.Size = new System.Drawing.Size(76, 21);
            this.cbHingeQty.TabIndex = 0;
            // 
            // cbHingePrep
            // 
            this.cbHingePrep.FormattingEnabled = true;
            this.cbHingePrep.Location = new System.Drawing.Point(129, 162);
            this.cbHingePrep.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cbHingePrep.Name = "cbHingePrep";
            this.cbHingePrep.Size = new System.Drawing.Size(76, 21);
            this.cbHingePrep.TabIndex = 0;
            // 
            // cbGauge
            // 
            this.cbGauge.FormattingEnabled = true;
            this.cbGauge.Location = new System.Drawing.Point(129, 53);
            this.cbGauge.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cbGauge.Name = "cbGauge";
            this.cbGauge.Size = new System.Drawing.Size(76, 21);
            this.cbGauge.TabIndex = 0;
            // 
            // txtFrameQty
            // 
            this.txtFrameQty.Location = new System.Drawing.Point(129, 17);
            this.txtFrameQty.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtFrameQty.Name = "txtFrameQty";
            this.txtFrameQty.Size = new System.Drawing.Size(76, 20);
            this.txtFrameQty.TabIndex = 0;
            // 
            // txtTabTop
            // 
            this.txtTabTop.Location = new System.Drawing.Point(129, 386);
            this.txtTabTop.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtTabTop.Name = "txtTabTop";
            this.txtTabTop.Size = new System.Drawing.Size(76, 20);
            this.txtTabTop.TabIndex = 0;
            // 
            // txtTabBase
            // 
            this.txtTabBase.Location = new System.Drawing.Point(129, 348);
            this.txtTabBase.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtTabBase.Name = "txtTabBase";
            this.txtTabBase.Size = new System.Drawing.Size(76, 20);
            this.txtTabBase.TabIndex = 0;
            // 
            // txtRevealHgt
            // 
            this.txtRevealHgt.Location = new System.Drawing.Point(129, 90);
            this.txtRevealHgt.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtRevealHgt.Name = "txtRevealHgt";
            this.txtRevealHgt.Size = new System.Drawing.Size(76, 20);
            this.txtRevealHgt.TabIndex = 0;
            // 
            // txtRevealWidth
            // 
            this.txtRevealWidth.Location = new System.Drawing.Point(129, 126);
            this.txtRevealWidth.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtRevealWidth.Name = "txtRevealWidth";
            this.txtRevealWidth.Size = new System.Drawing.Size(76, 20);
            this.txtRevealWidth.TabIndex = 0;
            // 
            // txtStrikeHgt
            // 
            this.txtStrikeHgt.Location = new System.Drawing.Point(129, 273);
            this.txtStrikeHgt.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtStrikeHgt.Name = "txtStrikeHgt";
            this.txtStrikeHgt.Size = new System.Drawing.Size(76, 20);
            this.txtStrikeHgt.TabIndex = 0;
            // 
            // StrikeHeight
            // 
            this.StrikeHeight.AutoSize = true;
            this.StrikeHeight.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.StrikeHeight.Location = new System.Drawing.Point(18, 274);
            this.StrikeHeight.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.StrikeHeight.Name = "StrikeHeight";
            this.StrikeHeight.Size = new System.Drawing.Size(84, 16);
            this.StrikeHeight.TabIndex = 33;
            this.StrikeHeight.Text = "Strike Height";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.label2.Location = new System.Drawing.Point(18, 164);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 16);
            this.label2.TabIndex = 28;
            this.label2.Text = "Hinge Prep";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.label3.Location = new System.Drawing.Point(17, 238);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 16);
            this.label3.TabIndex = 29;
            this.label3.Text = "Strike Prep";
            // 
            // Gauge
            // 
            this.Gauge.AutoSize = true;
            this.Gauge.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.Gauge.Location = new System.Drawing.Point(17, 55);
            this.Gauge.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Gauge.Name = "Gauge";
            this.Gauge.Size = new System.Drawing.Size(49, 16);
            this.Gauge.TabIndex = 28;
            this.Gauge.Text = "Gauge";
            // 
            // FrameFixed
            // 
            this.FrameFixed.AutoSize = true;
            this.FrameFixed.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.FrameFixed.Location = new System.Drawing.Point(17, 311);
            this.FrameFixed.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.FrameFixed.Name = "FrameFixed";
            this.FrameFixed.Size = new System.Drawing.Size(85, 16);
            this.FrameFixed.TabIndex = 29;
            this.FrameFixed.Text = "Frame Fixing";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.label1.Location = new System.Drawing.Point(17, 18);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 16);
            this.label1.TabIndex = 30;
            this.label1.Text = "Frame Qty";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.label5.Location = new System.Drawing.Point(17, 387);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 16);
            this.label5.TabIndex = 30;
            this.label5.Text = "Tab Top";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.label4.Location = new System.Drawing.Point(17, 349);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 16);
            this.label4.TabIndex = 30;
            this.label4.Text = "Tab Base";
            // 
            // RevealHeight
            // 
            this.RevealHeight.AutoSize = true;
            this.RevealHeight.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.RevealHeight.Location = new System.Drawing.Point(17, 91);
            this.RevealHeight.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.RevealHeight.Name = "RevealHeight";
            this.RevealHeight.Size = new System.Drawing.Size(94, 16);
            this.RevealHeight.TabIndex = 30;
            this.RevealHeight.Text = "Reveal Height";
            // 
            // Quantity
            // 
            this.Quantity.AutoSize = true;
            this.Quantity.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.Quantity.Location = new System.Drawing.Point(18, 201);
            this.Quantity.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Quantity.Name = "Quantity";
            this.Quantity.Size = new System.Drawing.Size(67, 16);
            this.Quantity.TabIndex = 31;
            this.Quantity.Text = "Hinge Qty";
            // 
            // RevealWidth
            // 
            this.RevealWidth.AutoSize = true;
            this.RevealWidth.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.RevealWidth.Location = new System.Drawing.Point(18, 127);
            this.RevealWidth.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.RevealWidth.Name = "RevealWidth";
            this.RevealWidth.Size = new System.Drawing.Size(89, 16);
            this.RevealWidth.TabIndex = 32;
            this.RevealWidth.Text = "Reveal Width";
            // 
            // txtStopSection
            // 
            this.txtStopSection.Enabled = false;
            this.txtStopSection.Location = new System.Drawing.Point(200, 55);
            this.txtStopSection.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtStopSection.Name = "txtStopSection";
            this.txtStopSection.Size = new System.Drawing.Size(76, 20);
            this.txtStopSection.TabIndex = 17;
            // 
            // txtStopHeight2
            // 
            this.txtStopHeight2.Location = new System.Drawing.Point(390, 97);
            this.txtStopHeight2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtStopHeight2.Name = "txtStopHeight2";
            this.txtStopHeight2.Size = new System.Drawing.Size(52, 20);
            this.txtStopHeight2.TabIndex = 20;
            // 
            // txtRebate2
            // 
            this.txtRebate2.Location = new System.Drawing.Point(338, 56);
            this.txtRebate2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtRebate2.Name = "txtRebate2";
            this.txtRebate2.Size = new System.Drawing.Size(53, 20);
            this.txtRebate2.TabIndex = 19;
            this.txtRebate2.TextChanged += new System.EventHandler(this.txtRebate2_TextChanged);
            // 
            // txtArchitrave2
            // 
            this.txtArchitrave2.Location = new System.Drawing.Point(390, 168);
            this.txtArchitrave2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtArchitrave2.Name = "txtArchitrave2";
            this.txtArchitrave2.Size = new System.Drawing.Size(52, 20);
            this.txtArchitrave2.TabIndex = 18;
            // 
            // txtArchitrave1
            // 
            this.txtArchitrave1.Location = new System.Drawing.Point(40, 168);
            this.txtArchitrave1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtArchitrave1.Name = "txtArchitrave1";
            this.txtArchitrave1.Size = new System.Drawing.Size(47, 20);
            this.txtArchitrave1.TabIndex = 14;
            // 
            // txtRebate1
            // 
            this.txtRebate1.Location = new System.Drawing.Point(94, 55);
            this.txtRebate1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtRebate1.Name = "txtRebate1";
            this.txtRebate1.Size = new System.Drawing.Size(50, 20);
            this.txtRebate1.TabIndex = 16;
            this.txtRebate1.TextChanged += new System.EventHandler(this.txtRebate1_TextChanged);
            // 
            // txtStopHeight1
            // 
            this.txtStopHeight1.Location = new System.Drawing.Point(40, 97);
            this.txtStopHeight1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtStopHeight1.Name = "txtStopHeight1";
            this.txtStopHeight1.Size = new System.Drawing.Size(47, 20);
            this.txtStopHeight1.TabIndex = 15;
            // 
            // txtReturn2
            // 
            this.txtReturn2.Location = new System.Drawing.Point(338, 235);
            this.txtReturn2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtReturn2.Name = "txtReturn2";
            this.txtReturn2.Size = new System.Drawing.Size(53, 20);
            this.txtReturn2.TabIndex = 13;
            this.txtReturn2.TextChanged += new System.EventHandler(this.txtReturn2_TextChanged);
            // 
            // txtThroat
            // 
            this.txtThroat.Location = new System.Drawing.Point(200, 235);
            this.txtThroat.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtThroat.Name = "txtThroat";
            this.txtThroat.Size = new System.Drawing.Size(76, 20);
            this.txtThroat.TabIndex = 12;
            // 
            // txtReturn1
            // 
            this.txtReturn1.Location = new System.Drawing.Point(94, 235);
            this.txtReturn1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtReturn1.Name = "txtReturn1";
            this.txtReturn1.Size = new System.Drawing.Size(50, 20);
            this.txtReturn1.TabIndex = 11;
            this.txtReturn1.TextChanged += new System.EventHandler(this.txtReturn1_TextChanged);
            // 
            // pictureBox1
            // 
            this.pictureBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureBox1.Location = new System.Drawing.Point(75, 62);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(329, 175);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 10;
            this.pictureBox1.TabStop = false;
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.chkSymmetry);
            this.panel3.Controls.Add(this.txtStopHeight2);
            this.panel3.Controls.Add(this.txtRebate2);
            this.panel3.Controls.Add(this.txtArchitrave2);
            this.panel3.Controls.Add(this.txtStopSection);
            this.panel3.Controls.Add(this.txtRebate1);
            this.panel3.Controls.Add(this.txtStopHeight1);
            this.panel3.Controls.Add(this.txtArchitrave1);
            this.panel3.Controls.Add(this.txtReturn2);
            this.panel3.Controls.Add(this.txtThroat);
            this.panel3.Controls.Add(this.txtReturn1);
            this.panel3.Controls.Add(this.pictureBox1);
            this.panel3.Location = new System.Drawing.Point(243, 44);
            this.panel3.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(481, 276);
            this.panel3.TabIndex = 25;
            // 
            // comboClientType
            // 
            this.comboClientType.FormattingEnabled = true;
            this.comboClientType.Location = new System.Drawing.Point(23, 11);
            this.comboClientType.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.comboClientType.Name = "comboClientType";
            this.comboClientType.Size = new System.Drawing.Size(92, 21);
            this.comboClientType.TabIndex = 22;
            // 
            // chkSymmetry
            // 
            this.chkSymmetry.AutoSize = true;
            this.chkSymmetry.Location = new System.Drawing.Point(381, 17);
            this.chkSymmetry.Name = "chkSymmetry";
            this.chkSymmetry.Size = new System.Drawing.Size(71, 17);
            this.chkSymmetry.TabIndex = 21;
            this.chkSymmetry.Text = "Symmetry";
            this.chkSymmetry.UseVisualStyleBackColor = true;
            this.chkSymmetry.CheckedChanged += new System.EventHandler(this.chkSymmetry_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.getBtn);
            this.panel1.Controls.Add(this.btnCloning);
            this.panel1.Location = new System.Drawing.Point(243, 338);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(481, 55);
            this.panel1.TabIndex = 26;
            // 
            // btnBrowse
            // 
            this.btnBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowse.Location = new System.Drawing.Point(13, 8);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(75, 30);
            this.btnBrowse.TabIndex = 27;
            this.btnBrowse.Text = "Browse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // lblInputPath
            // 
            this.lblInputPath.AutoSize = true;
            this.lblInputPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.lblInputPath.Location = new System.Drawing.Point(100, 15);
            this.lblInputPath.Name = "lblInputPath";
            this.lblInputPath.Size = new System.Drawing.Size(45, 16);
            this.lblInputPath.TabIndex = 28;
            this.lblInputPath.Text = "label1";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.btnBrowse);
            this.panel4.Controls.Add(this.lblInputPath);
            this.panel4.Location = new System.Drawing.Point(243, 423);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(481, 44);
            this.panel4.TabIndex = 29;
            // 
            // comboProfileType
            // 
            this.comboProfileType.FormattingEnabled = true;
            this.comboProfileType.Location = new System.Drawing.Point(243, 11);
            this.comboProfileType.Name = "comboProfileType";
            this.comboProfileType.Size = new System.Drawing.Size(121, 21);
            this.comboProfileType.TabIndex = 30;
            this.comboProfileType.SelectedIndexChanged += new System.EventHandler(this.comboProfileType_SelectedIndexChanged);
            // 
            // comboSubProfileType
            // 
            this.comboSubProfileType.FormattingEnabled = true;
            this.comboSubProfileType.Location = new System.Drawing.Point(389, 11);
            this.comboSubProfileType.Name = "comboSubProfileType";
            this.comboSubProfileType.Size = new System.Drawing.Size(121, 21);
            this.comboSubProfileType.TabIndex = 30;
            this.comboSubProfileType.SelectedIndexChanged += new System.EventHandler(this.comboSubProfileType_SelectedIndexChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(761, 485);
            this.Controls.Add(this.comboSubProfileType);
            this.Controls.Add(this.comboProfileType);
            this.Controls.Add(this.comboClientType);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel3);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "Form1";
            this.Text = "Main";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button getBtn;
        private System.Windows.Forms.Button btnCloning;
        private System.Windows.Forms.Panel panel2;
        public System.Windows.Forms.ComboBox cbFrameFixing;
        public System.Windows.Forms.ComboBox cbHingeQty;
        public System.Windows.Forms.ComboBox cbGauge;
        public System.Windows.Forms.TextBox txtRevealHgt;
        public System.Windows.Forms.TextBox txtRevealWidth;
        public System.Windows.Forms.TextBox txtStrikeHgt;
        protected System.Windows.Forms.Label StrikeHeight;
        protected System.Windows.Forms.Label Gauge;
        protected System.Windows.Forms.Label FrameFixed;
        protected System.Windows.Forms.Label RevealHeight;
        protected System.Windows.Forms.Label Quantity;
        protected System.Windows.Forms.Label RevealWidth;
        public System.Windows.Forms.TextBox txtStopSection;
        private System.Windows.Forms.TextBox txtStopHeight2;
        private System.Windows.Forms.TextBox txtRebate2;
        public System.Windows.Forms.TextBox txtArchitrave2;
        public System.Windows.Forms.TextBox txtArchitrave1;
        public System.Windows.Forms.TextBox txtRebate1;
        public System.Windows.Forms.TextBox txtStopHeight1;
        public System.Windows.Forms.TextBox txtReturn2;
        public System.Windows.Forms.TextBox txtThroat;
        public System.Windows.Forms.TextBox txtReturn1;
        protected System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Label lblInputPath;
        public System.Windows.Forms.TextBox txtFrameQty;
        protected System.Windows.Forms.Label label1;
        public System.Windows.Forms.ComboBox cbStrikePrep;
        public System.Windows.Forms.ComboBox cbHingePrep;
        protected System.Windows.Forms.Label label2;
        protected System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.CheckBox chkSymmetry;
        public System.Windows.Forms.TextBox txtTabTop;
        public System.Windows.Forms.TextBox txtTabBase;
        protected System.Windows.Forms.Label label5;
        protected System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboClientType;
        private System.Windows.Forms.ComboBox comboProfileType;
        private System.Windows.Forms.ComboBox comboSubProfileType;
    }
}

