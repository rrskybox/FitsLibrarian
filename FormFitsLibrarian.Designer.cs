namespace FitsLibrarian
{
    partial class FormFitsLibrarian
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormFitsLibrarian));
            RootFolderDialog = new FolderBrowserDialog();
            treeView1 = new TreeView();
            label1 = new Label();
            txtDirectoryPath = new TextBox();
            btnDirectoryPath = new Button();
            toolTip1 = new ToolTip(components);
            FieldSelect = new CheckedListBox();
            FieldDataGrid = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)FieldDataGrid).BeginInit();
            SuspendLayout();
            // 
            // treeView1
            // 
            treeView1.BackColor = Color.DarkCyan;
            treeView1.ForeColor = Color.White;
            treeView1.Location = new Point(6, 36);
            treeView1.Name = "treeView1";
            treeView1.Size = new Size(194, 364);
            treeView1.TabIndex = 2;
            treeView1.AfterSelect += treeView1_AfterSelect;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = Color.Black;
            label1.Location = new Point(7, 11);
            label1.Name = "label1";
            label1.Size = new Size(89, 15);
            label1.TabIndex = 3;
            label1.Text = "Select Directory";
            // 
            // txtDirectoryPath
            // 
            txtDirectoryPath.Location = new Point(111, 8);
            txtDirectoryPath.Name = "txtDirectoryPath";
            txtDirectoryPath.Size = new Size(615, 23);
            txtDirectoryPath.TabIndex = 4;
            // 
            // btnDirectoryPath
            // 
            btnDirectoryPath.BackColor = Color.Turquoise;
            btnDirectoryPath.ForeColor = Color.Black;
            btnDirectoryPath.Location = new Point(733, 5);
            btnDirectoryPath.Name = "btnDirectoryPath";
            btnDirectoryPath.Size = new Size(65, 25);
            btnDirectoryPath.TabIndex = 5;
            btnDirectoryPath.Text = "Browse";
            btnDirectoryPath.UseVisualStyleBackColor = false;
            btnDirectoryPath.Click += btnDirectoryPath_Click;
            // 
            // FieldSelect
            // 
            FieldSelect.BackColor = Color.DarkCyan;
            FieldSelect.CheckOnClick = true;
            FieldSelect.ForeColor = Color.White;
            FieldSelect.FormattingEnabled = true;
            FieldSelect.Location = new Point(206, 36);
            FieldSelect.Name = "FieldSelect";
            FieldSelect.Size = new Size(155, 346);
            FieldSelect.TabIndex = 7;
            FieldSelect.SelectedIndexChanged += FieldSelect_SelectedIndexChanged;
            // 
            // FieldDataGrid
            // 
            FieldDataGrid.AllowUserToAddRows = false;
            FieldDataGrid.AllowUserToDeleteRows = false;
            FieldDataGrid.BackgroundColor = Color.DarkCyan;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = Color.MintCream;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle1.ForeColor = Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            FieldDataGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            FieldDataGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.MintCream;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            FieldDataGrid.DefaultCellStyle = dataGridViewCellStyle2;
            FieldDataGrid.EditMode = DataGridViewEditMode.EditOnKeystroke;
            FieldDataGrid.Location = new Point(375, 41);
            FieldDataGrid.Name = "FieldDataGrid";
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = Color.MintCream;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle3.ForeColor = Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
            FieldDataGrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            FieldDataGrid.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            FieldDataGrid.Size = new Size(994, 357);
            FieldDataGrid.TabIndex = 9;
            FieldDataGrid.CellClick += FieldDataGrid_CellClick;
            FieldDataGrid.CellDoubleClick += FieldDataGrid_CellDoubleClick;
            FieldDataGrid.CellEndEdit += FieldDataGrid_CellEndEdit;
            FieldDataGrid.EditingControlShowing += FieldDataGrid_EditingControlShowing;
            // 
            // FormFitsLibrarian
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.LightSeaGreen;
            ClientSize = new Size(1381, 411);
            Controls.Add(FieldDataGrid);
            Controls.Add(FieldSelect);
            Controls.Add(btnDirectoryPath);
            Controls.Add(txtDirectoryPath);
            Controls.Add(label1);
            Controls.Add(treeView1);
            ForeColor = Color.White;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "FormFitsLibrarian";
            Text = "Fits Librarian";
            ((System.ComponentModel.ISupportInitialize)FieldDataGrid).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private FolderBrowserDialog RootFolderDialog;
        private TreeView treeView1;
        private Label label1;
        private TextBox txtDirectoryPath;
        private Button btnDirectoryPath;
        private ToolTip toolTip1;
        public CheckedListBox FieldSelect;
        private DataGridView FieldDataGrid;
    }
}