namespace EduSearch_Information_System
{
    partial class SearchEngineApplication
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
            this.BrowseSourceButton = new System.Windows.Forms.Button();
            this.BrowseIndexButton = new System.Windows.Forms.Button();
            this.SourceBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.IndexBrowseDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.SourcePathLabel = new System.Windows.Forms.Label();
            this.IndexPathLabel = new System.Windows.Forms.Label();
            this.CreateIndexButton = new System.Windows.Forms.Button();
            this.TimeExcutionLabel = new System.Windows.Forms.Label();
            this.SearchBox = new System.Windows.Forms.TextBox();
            this.SearchButton = new System.Windows.Forms.Button();
            this.listView = new System.Windows.Forms.ListView();
            this.Rank = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Title = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Author = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Bibliographic = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Abstract = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SearchTimeExcutionLabel = new System.Windows.Forms.Label();
            this.totalresultLabel = new System.Windows.Forms.Label();
            this.cbPagingIndex = new System.Windows.Forms.ComboBox();
            this.btn_SaveFile = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtBox_FileName = new System.Windows.Forms.TextBox();
            this.txtBox_Identifier = new System.Windows.Forms.TextBox();
            this.txtBox_FinalQuery = new System.Windows.Forms.TextBox();
            this.btn_ClearQuery = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // BrowseSourceButton
            // 
            this.BrowseSourceButton.Location = new System.Drawing.Point(23, 12);
            this.BrowseSourceButton.Name = "BrowseSourceButton";
            this.BrowseSourceButton.Size = new System.Drawing.Size(98, 23);
            this.BrowseSourceButton.TabIndex = 0;
            this.BrowseSourceButton.Text = "Browse Source";
            this.BrowseSourceButton.UseVisualStyleBackColor = true;
            this.BrowseSourceButton.Click += new System.EventHandler(this.BrowseSourceButton_Click);
            // 
            // BrowseIndexButton
            // 
            this.BrowseIndexButton.Location = new System.Drawing.Point(23, 41);
            this.BrowseIndexButton.Name = "BrowseIndexButton";
            this.BrowseIndexButton.Size = new System.Drawing.Size(98, 23);
            this.BrowseIndexButton.TabIndex = 1;
            this.BrowseIndexButton.Text = "Browse Index";
            this.BrowseIndexButton.UseVisualStyleBackColor = true;
            this.BrowseIndexButton.Click += new System.EventHandler(this.BrowseIndexButton_Click);
            // 
            // SourcePathLabel
            // 
            this.SourcePathLabel.AutoSize = true;
            this.SourcePathLabel.Location = new System.Drawing.Point(123, 19);
            this.SourcePathLabel.Name = "SourcePathLabel";
            this.SourcePathLabel.Size = new System.Drawing.Size(267, 13);
            this.SourcePathLabel.TabIndex = 2;
            this.SourcePathLabel.Text = "H:\\EduSearch Information System\\collection\\crandocs";
            // 
            // IndexPathLabel
            // 
            this.IndexPathLabel.AutoSize = true;
            this.IndexPathLabel.Location = new System.Drawing.Point(124, 46);
            this.IndexPathLabel.Name = "IndexPathLabel";
            this.IndexPathLabel.Size = new System.Drawing.Size(248, 13);
            this.IndexPathLabel.TabIndex = 3;
            this.IndexPathLabel.Text = "H:\\EduSearch Information System\\collection\\index";
            // 
            // CreateIndexButton
            // 
            this.CreateIndexButton.Location = new System.Drawing.Point(716, 19);
            this.CreateIndexButton.Name = "CreateIndexButton";
            this.CreateIndexButton.Size = new System.Drawing.Size(98, 23);
            this.CreateIndexButton.TabIndex = 4;
            this.CreateIndexButton.Text = "Build Index";
            this.CreateIndexButton.UseVisualStyleBackColor = true;
            this.CreateIndexButton.Click += new System.EventHandler(this.CreateIndexButton_Click);
            // 
            // TimeExcutionLabel
            // 
            this.TimeExcutionLabel.AutoSize = true;
            this.TimeExcutionLabel.Location = new System.Drawing.Point(716, 46);
            this.TimeExcutionLabel.Name = "TimeExcutionLabel";
            this.TimeExcutionLabel.Size = new System.Drawing.Size(0, 13);
            this.TimeExcutionLabel.TabIndex = 5;
            // 
            // SearchBox
            // 
            this.SearchBox.Location = new System.Drawing.Point(23, 81);
            this.SearchBox.Name = "SearchBox";
            this.SearchBox.Size = new System.Drawing.Size(687, 20);
            this.SearchBox.TabIndex = 6;
            // 
            // SearchButton
            // 
            this.SearchButton.Location = new System.Drawing.Point(716, 81);
            this.SearchButton.Name = "SearchButton";
            this.SearchButton.Size = new System.Drawing.Size(98, 23);
            this.SearchButton.TabIndex = 7;
            this.SearchButton.Text = "Search";
            this.SearchButton.UseVisualStyleBackColor = true;
            this.SearchButton.Click += new System.EventHandler(this.SearchButton_Click);
            // 
            // listView
            // 
            this.listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Rank,
            this.Title,
            this.Author,
            this.Bibliographic,
            this.Abstract});
            this.listView.FullRowSelect = true;
            this.listView.GridLines = true;
            this.listView.Location = new System.Drawing.Point(23, 139);
            this.listView.Name = "listView";
            this.listView.Size = new System.Drawing.Size(687, 368);
            this.listView.TabIndex = 8;
            this.listView.UseCompatibleStateImageBehavior = false;
            this.listView.View = System.Windows.Forms.View.Details;
            this.listView.ItemActivate += new System.EventHandler(this.listView_ItemActivate);
            // 
            // Rank
            // 
            this.Rank.Text = "Rank";
            // 
            // Title
            // 
            this.Title.Text = "Title";
            this.Title.Width = 150;
            // 
            // Author
            // 
            this.Author.Text = "Author";
            // 
            // Bibliographic
            // 
            this.Bibliographic.Text = "Bibliographic";
            this.Bibliographic.Width = 78;
            // 
            // Abstract
            // 
            this.Abstract.Text = "Abstract";
            this.Abstract.Width = 333;
            // 
            // SearchTimeExcutionLabel
            // 
            this.SearchTimeExcutionLabel.AutoSize = true;
            this.SearchTimeExcutionLabel.Location = new System.Drawing.Point(721, 154);
            this.SearchTimeExcutionLabel.Name = "SearchTimeExcutionLabel";
            this.SearchTimeExcutionLabel.Size = new System.Drawing.Size(0, 13);
            this.SearchTimeExcutionLabel.TabIndex = 9;
            // 
            // totalresultLabel
            // 
            this.totalresultLabel.AutoSize = true;
            this.totalresultLabel.Location = new System.Drawing.Point(721, 269);
            this.totalresultLabel.Name = "totalresultLabel";
            this.totalresultLabel.Size = new System.Drawing.Size(0, 13);
            this.totalresultLabel.TabIndex = 12;
            // 
            // cbPagingIndex
            // 
            this.cbPagingIndex.FormattingEnabled = true;
            this.cbPagingIndex.Location = new System.Drawing.Point(590, 513);
            this.cbPagingIndex.Name = "cbPagingIndex";
            this.cbPagingIndex.Size = new System.Drawing.Size(120, 21);
            this.cbPagingIndex.TabIndex = 14;
            this.cbPagingIndex.SelectedIndexChanged += new System.EventHandler(this.cbPagingIndex_SelectedIndexChanged);
            // 
            // btn_SaveFile
            // 
            this.btn_SaveFile.Location = new System.Drawing.Point(716, 394);
            this.btn_SaveFile.Name = "btn_SaveFile";
            this.btn_SaveFile.Size = new System.Drawing.Size(97, 23);
            this.btn_SaveFile.TabIndex = 23;
            this.btn_SaveFile.Text = "Save Result";
            this.btn_SaveFile.UseVisualStyleBackColor = true;
            this.btn_SaveFile.Click += new System.EventHandler(this.btn_SaveFile_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(719, 327);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 24;
            this.label1.Text = "FileName";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(721, 354);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 25;
            this.label2.Text = "Identifier";
            // 
            // txtBox_FileName
            // 
            this.txtBox_FileName.Location = new System.Drawing.Point(775, 323);
            this.txtBox_FileName.Name = "txtBox_FileName";
            this.txtBox_FileName.Size = new System.Drawing.Size(142, 20);
            this.txtBox_FileName.TabIndex = 26;
            // 
            // txtBox_Identifier
            // 
            this.txtBox_Identifier.Location = new System.Drawing.Point(775, 351);
            this.txtBox_Identifier.Name = "txtBox_Identifier";
            this.txtBox_Identifier.Size = new System.Drawing.Size(142, 20);
            this.txtBox_Identifier.TabIndex = 27;
            // 
            // txtBox_FinalQuery
            // 
            this.txtBox_FinalQuery.Location = new System.Drawing.Point(23, 105);
            this.txtBox_FinalQuery.Name = "txtBox_FinalQuery";
            this.txtBox_FinalQuery.Size = new System.Drawing.Size(687, 20);
            this.txtBox_FinalQuery.TabIndex = 28;
            // 
            // btn_ClearQuery
            // 
            this.btn_ClearQuery.Location = new System.Drawing.Point(716, 110);
            this.btn_ClearQuery.Name = "btn_ClearQuery";
            this.btn_ClearQuery.Size = new System.Drawing.Size(97, 23);
            this.btn_ClearQuery.TabIndex = 29;
            this.btn_ClearQuery.Text = "Clear Query";
            this.btn_ClearQuery.UseVisualStyleBackColor = true;
            this.btn_ClearQuery.Click += new System.EventHandler(this.btn_ClearQuery_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(942, 544);
            this.Controls.Add(this.btn_ClearQuery);
            this.Controls.Add(this.txtBox_FinalQuery);
            this.Controls.Add(this.txtBox_Identifier);
            this.Controls.Add(this.txtBox_FileName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_SaveFile);
            this.Controls.Add(this.cbPagingIndex);
            this.Controls.Add(this.totalresultLabel);
            this.Controls.Add(this.SearchTimeExcutionLabel);
            this.Controls.Add(this.listView);
            this.Controls.Add(this.SearchButton);
            this.Controls.Add(this.SearchBox);
            this.Controls.Add(this.TimeExcutionLabel);
            this.Controls.Add(this.CreateIndexButton);
            this.Controls.Add(this.IndexPathLabel);
            this.Controls.Add(this.SourcePathLabel);
            this.Controls.Add(this.BrowseIndexButton);
            this.Controls.Add(this.BrowseSourceButton);
            this.Name = "Form1";
            this.Text = " ";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BrowseSourceButton;
        private System.Windows.Forms.Button BrowseIndexButton;
        private System.Windows.Forms.FolderBrowserDialog SourceBrowserDialog;
        private System.Windows.Forms.FolderBrowserDialog IndexBrowseDialog;
        private System.Windows.Forms.Label SourcePathLabel;
        private System.Windows.Forms.Label IndexPathLabel;
        private System.Windows.Forms.Button CreateIndexButton;
        private System.Windows.Forms.Label TimeExcutionLabel;
        private System.Windows.Forms.TextBox SearchBox;
        private System.Windows.Forms.Button SearchButton;
        private System.Windows.Forms.ColumnHeader Rank;
        private System.Windows.Forms.ColumnHeader Title;
        private System.Windows.Forms.Label SearchTimeExcutionLabel;
        public System.Windows.Forms.ListView listView;
        private System.Windows.Forms.Label totalresultLabel;
        private System.Windows.Forms.ColumnHeader Author;
        private System.Windows.Forms.ColumnHeader Bibliographic;
        private System.Windows.Forms.ColumnHeader Abstract;
        private System.Windows.Forms.ComboBox cbPagingIndex;
        private System.Windows.Forms.Button btn_SaveFile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtBox_FileName;
        private System.Windows.Forms.TextBox txtBox_Identifier;
        private System.Windows.Forms.TextBox txtBox_FinalQuery;
        private System.Windows.Forms.Button btn_ClearQuery;
    }
}
