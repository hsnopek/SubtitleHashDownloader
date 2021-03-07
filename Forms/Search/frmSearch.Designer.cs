namespace SubtitleDownloader.Forms.Search
{
    partial class frmSearch
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
            this.KeyPreview = true;

            this.groupBoxFilter = new System.Windows.Forms.GroupBox();
            this.cmbStranica = new System.Windows.Forms.ComboBox();
            this.lblStranica = new System.Windows.Forms.Label();
            this.etEpisode = new System.Windows.Forms.TextBox();
            this.etSeason = new System.Windows.Forms.TextBox();
            this.etTitle = new System.Windows.Forms.TextBox();
            this.lblEpisode = new System.Windows.Forms.Label();
            this.lblSeason = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.lblLanguage = new System.Windows.Forms.Label();
            this.cmbLanguage = new System.Windows.Forms.ComboBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBoxResults = new System.Windows.Forms.GroupBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnDownload = new System.Windows.Forms.Button();
            this.groupBoxFilter.SuspendLayout();
            this.groupBoxResults.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBoxFilter
            // 
            this.groupBoxFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxFilter.Controls.Add(this.cmbStranica);
            this.groupBoxFilter.Controls.Add(this.lblStranica);
            this.groupBoxFilter.Controls.Add(this.etEpisode);
            this.groupBoxFilter.Controls.Add(this.etSeason);
            this.groupBoxFilter.Controls.Add(this.etTitle);
            this.groupBoxFilter.Controls.Add(this.lblEpisode);
            this.groupBoxFilter.Controls.Add(this.lblSeason);
            this.groupBoxFilter.Controls.Add(this.lblTitle);
            this.groupBoxFilter.Controls.Add(this.btnSearch);
            this.groupBoxFilter.Controls.Add(this.lblLanguage);
            this.groupBoxFilter.Controls.Add(this.cmbLanguage);
            this.groupBoxFilter.Location = new System.Drawing.Point(14, 14);
            this.groupBoxFilter.Name = "groupBoxFilter";
            this.groupBoxFilter.Size = new System.Drawing.Size(359, 196);
            this.groupBoxFilter.TabIndex = 0;
            this.groupBoxFilter.TabStop = false;
            this.groupBoxFilter.Text = "Filter";
            // 
            // cmbStranica
            // 
            this.cmbStranica.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbStranica.FormattingEnabled = true;
            this.cmbStranica.Items.AddRange(new object[] {
            "OpenSubtitles",
            "PodnapisiNET"});
            this.cmbStranica.Location = new System.Drawing.Point(61, 24);
            this.cmbStranica.Name = "cmbStranica";
            this.cmbStranica.Size = new System.Drawing.Size(284, 21);
            this.cmbStranica.TabIndex = 12;
            this.cmbStranica.SelectedIndexChanged += new System.EventHandler(this.cmbStranica_SelectedIndexChanged);
            // 
            // lblStranica
            // 
            this.lblStranica.AutoSize = true;
            this.lblStranica.Location = new System.Drawing.Point(8, 27);
            this.lblStranica.Name = "lblStranica";
            this.lblStranica.Size = new System.Drawing.Size(49, 13);
            this.lblStranica.TabIndex = 11;
            this.lblStranica.Text = "Stranica:";
            // 
            // etEpisode
            // 
            this.etEpisode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.etEpisode.Location = new System.Drawing.Point(61, 132);
            this.etEpisode.Name = "etEpisode";
            this.etEpisode.Size = new System.Drawing.Size(284, 20);
            this.etEpisode.TabIndex = 10;
            // 
            // etSeason
            // 
            this.etSeason.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.etSeason.Location = new System.Drawing.Point(61, 106);
            this.etSeason.Name = "etSeason";
            this.etSeason.Size = new System.Drawing.Size(283, 20);
            this.etSeason.TabIndex = 9;
            // 
            // etTitle
            // 
            this.etTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.etTitle.Location = new System.Drawing.Point(61, 80);
            this.etTitle.Name = "etTitle";
            this.etTitle.Size = new System.Drawing.Size(284, 20);
            this.etTitle.TabIndex = 8;
            // 
            // lblEpisode
            // 
            this.lblEpisode.AutoSize = true;
            this.lblEpisode.Location = new System.Drawing.Point(8, 135);
            this.lblEpisode.Name = "lblEpisode";
            this.lblEpisode.Size = new System.Drawing.Size(48, 13);
            this.lblEpisode.TabIndex = 7;
            this.lblEpisode.Text = "Epizoda:";
            // 
            // lblSeason
            // 
            this.lblSeason.AutoSize = true;
            this.lblSeason.Location = new System.Drawing.Point(8, 109);
            this.lblSeason.Name = "lblSeason";
            this.lblSeason.Size = new System.Drawing.Size(46, 13);
            this.lblSeason.TabIndex = 6;
            this.lblSeason.Text = "Sezona:";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new System.Drawing.Point(8, 83);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(37, 13);
            this.lblTitle.TabIndex = 5;
            this.lblTitle.Text = "Naziv:";
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnSearch.Location = new System.Drawing.Point(247, 158);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(98, 23);
            this.btnSearch.TabIndex = 1;
            this.btnSearch.Text = "Pretraži";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
            // 
            // lblLanguage
            // 
            this.lblLanguage.AutoSize = true;
            this.lblLanguage.Location = new System.Drawing.Point(8, 56);
            this.lblLanguage.Name = "lblLanguage";
            this.lblLanguage.Size = new System.Drawing.Size(34, 13);
            this.lblLanguage.TabIndex = 4;
            this.lblLanguage.Text = "Jezik:";
            // 
            // cmbLanguage
            // 
            this.cmbLanguage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbLanguage.FormattingEnabled = true;
            this.cmbLanguage.Location = new System.Drawing.Point(61, 53);
            this.cmbLanguage.Name = "cmbLanguage";
            this.cmbLanguage.Size = new System.Drawing.Size(284, 21);
            this.cmbLanguage.TabIndex = 0;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(214, 417);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 22);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Odustani";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // groupBoxResults
            // 
            this.groupBoxResults.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxResults.Controls.Add(this.dataGridView1);
            this.groupBoxResults.Location = new System.Drawing.Point(14, 216);
            this.groupBoxResults.Name = "groupBoxResults";
            this.groupBoxResults.Padding = new System.Windows.Forms.Padding(5);
            this.groupBoxResults.Size = new System.Drawing.Size(358, 195);
            this.groupBoxResults.TabIndex = 3;
            this.groupBoxResults.TabStop = false;
            this.groupBoxResults.Text = "Titlovi";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(5, 18);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(348, 172);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.DataSourceChanged += new System.EventHandler(this.dataGridView1_DataSourceChanged);
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.DataGridView1_SelectionChanged);
            // 
            // btnDownload
            // 
            this.btnDownload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDownload.Enabled = false;
            this.btnDownload.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnDownload.Location = new System.Drawing.Point(295, 417);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(75, 22);
            this.btnDownload.TabIndex = 4;
            this.btnDownload.Text = "Preuzmi";
            this.btnDownload.UseVisualStyleBackColor = true;
            this.btnDownload.Click += new System.EventHandler(this.BtnDownload_Click);
            // 
            // frmSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(383, 451);
            this.Controls.Add(this.btnDownload);
            this.Controls.Add(this.groupBoxResults);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.groupBoxFilter);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(399, 490);
            this.Name = "frmSearch";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tražilica";
            this.Load += new System.EventHandler(this.Search_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.frmSearch_KeyUp);
            this.groupBoxFilter.ResumeLayout(false);
            this.groupBoxFilter.PerformLayout();
            this.groupBoxResults.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxFilter;
        private System.Windows.Forms.ComboBox cmbLanguage;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox groupBoxResults;
        private System.Windows.Forms.Label lblEpisode;
        private System.Windows.Forms.Label lblSeason;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblLanguage;
        private System.Windows.Forms.Button btnDownload;
        private System.Windows.Forms.DataGridView dataGridView1;
        public System.Windows.Forms.TextBox etEpisode;
        public System.Windows.Forms.TextBox etSeason;
        public System.Windows.Forms.TextBox etTitle;
        public System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.ComboBox cmbStranica;
        private System.Windows.Forms.Label lblStranica;
    }
}