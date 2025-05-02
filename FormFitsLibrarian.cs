using System.IO;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Collections.Specialized;
using System.Linq;
using System.Xml.Linq;
using System.Windows.Forms;
using System.Runtime.CompilerServices;
using System.Reflection;
using Windows.ApplicationModel.VoiceCommands;
using Windows.Storage;

namespace FitsLibrarian

{
    public partial class FormFitsLibrarian : Form
    {
        private bool StartUpFlag = true;

        public FormFitsLibrarian()
        {
            InitializeComponent();
            //Set window header with app name and version
            var ver = Assembly.GetExecutingAssembly().GetName().Version;
            this.Text = string.Format("{3}, Version: {0}.{1}.{2}", ver.Major, ver.Minor, ver.Build, Assembly.GetEntryAssembly().GetName().Name);
            //Get files in directory tree
            if (Properties.Settings.Default.RootDirectory != "")
            {
                txtDirectoryPath.Text = Properties.Settings.Default.RootDirectory;
                LoadDirectory(txtDirectoryPath.Text);
                InitializeGrid();
            }

            Show();
        }

        private void InitializeGrid()
        {
            //Create User control

            //Clear the Field Types and Selections
            FitsFielder.ResetFielder();
            //Clear the date grid rows and columns
            FieldDataGrid.Rows.Clear();
            FieldDataGrid.Columns.Clear();
            FieldDataGrid.ForeColor = Color.Black;

            //Fill in selected types and fits data grid
            if (txtDirectoryPath.Text == "")
                return;
            List<string> fitsList = Directory.GetFiles(txtDirectoryPath.Text, "*.fit?", SearchOption.AllDirectories).ToList();
            if (fitsList.Count < 1)
                return;
            if (fitsList.Count > 40)
            {
                DialogResult dr = MessageBox.Show(fitsList.Count + " fits files have been found in this directory tree.  This could take awhile.  Continue?", "Large count check", MessageBoxButtons.OKCancel);
                if (dr != DialogResult.OK)
                    return;
            }
            FitsFielder.ResetFielder();
            FitsFielder.LoadEnabledFields();
            // For each file compile the fits header data into the common field list and the column list
            int rowIndex = 0;
            foreach (string fpath in fitsList)
            {
                //Read in the fits header to fill out common fields and common names
                //  if the field is not currently in the common field list, then it
                //  will be added and enabled.  Otherwise, just added.
                FitsFile fData = new FitsFile(fpath);
                //Add each header field to the common field list and column list
                for (int iH = 0; iH < fData.FieldList.Count; iH++)
                {
                    FitsHeaderField headerRecord = fData.FieldList[iH];
                    FitsFielder.AddCommonField(headerRecord.FieldName);
                    if (!FitsFielder.IsColumnHeader(headerRecord.FieldName))
                    {
                        FitsFielder.AddColumnHeader(headerRecord.FieldName);
                        FieldDataGrid.Columns.Add(headerRecord.FieldName, headerRecord.FieldName);
                    }
                    int colIndex = FitsFielder.GetColumnIndex(headerRecord.FieldName);
                    if (iH == 0)
                        rowIndex = FieldDataGrid.Rows.Add();  //Add the next row to the grid
                    FieldDataGrid[colIndex, rowIndex].Value = headerRecord.FieldData;
                }
                //Set the header of the row to the Filepath fpath
                //Enter the stripped file path in the first column
                FieldDataGrid.Rows[rowIndex].HeaderCell.Value = Path.GetFileNameWithoutExtension(fpath);
            }

            //Update the selected list with enabled fields which will hide or show columns accoringly
            foreach (string selField in FitsFielder.GetAllColumnHeaders())
            {
                FieldDataGrid.Columns[FitsFielder.GetColumnIndex(selField)].HeaderCell.Value = selField;
                FieldDataGrid.Columns[FitsFielder.GetColumnIndex(selField)].Visible = FitsFielder.IsEnabledField(selField);
            }

            //Read in the enabled field types, enable all if none enabled
            LoadFieldSelections();
        }

        private void FieldSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Remove the column disabled by unchecking a box
            if (FieldSelect.SelectedIndex == -1)
                return;
            if (FieldSelect.GetItemCheckState(FieldSelect.SelectedIndex) == CheckState.Checked)
            {
                FieldSelect.SetItemCheckState(FieldSelect.SelectedIndex, CheckState.Checked);
                FieldDataGrid.Columns[FieldSelect.SelectedItem.ToString()].Visible = true;
                FitsFielder.AddEnabledField(FieldSelect.SelectedItem.ToString());
            }
            else
            {
                FieldSelect.SetItemCheckState(FieldSelect.SelectedIndex, CheckState.Unchecked);
                FieldDataGrid.Columns[FieldSelect.SelectedItem.ToString()].Visible = false;
                FitsFielder.RemoveEnabledField(FieldSelect.SelectedItem.ToString());
            }
            FieldSelect.ClearSelected();
            Show();
            return;
        }

        private void LoadFieldSelections()
        {
            //Loads current field selections from settings
            FieldSelect.Items.Clear();
            if (FitsFielder.HasCommonFields())
            {
                List<string> fs = FitsFielder.GetAllCommonFields();
                fs = fs.OrderBy(x => x).ToList();
                foreach (string h in fs)
                {
                    int idx = FieldSelect.Items.Add(h);
                    if (FitsFielder.IsEnabledField(h))
                        FieldSelect.SetItemChecked(idx, true);
                    else
                        FieldSelect.SetItemChecked(idx, false);
                }
            }
            return;
        }


        #region file explorer

        private void btnDirectoryPath_Click(object sender, EventArgs e)
        {
            //Get initial directory
            RootFolderDialog.SelectedPath = txtDirectoryPath.Text;
            DialogResult drResult = RootFolderDialog.ShowDialog();
            if (drResult == System.Windows.Forms.DialogResult.OK)
            {
                txtDirectoryPath.Text = RootFolderDialog.SelectedPath;
                Properties.Settings.Default.RootDirectory = RootFolderDialog.SelectedPath;
                Properties.Settings.Default.Save();
            }
            // Clear All Nodes if Already Exists
            treeView1.Nodes.Clear();
            toolTip1.ShowAlways = true;
            if (txtDirectoryPath.Text != "" && Directory.Exists(txtDirectoryPath.Text))
            {
                LoadDirectory(txtDirectoryPath.Text);
                InitializeGrid();
            }
            else
            {
                MessageBox.Show("Select Directory!!");
            }
            return;
        }

        public void LoadDirectory(string Dir)
        {
            DirectoryInfo di = new DirectoryInfo(Dir);
            //Setting ProgressBar Maximum Value
            TreeNode tds = treeView1.Nodes.Add(di.FullName, di.Name);
            tds.Tag = di.FullName;
            tds.StateImageIndex = 0;
            LoadFiles(Dir, tds);
            LoadSubDirectories(Dir, tds);
        }

        private void LoadSubDirectories(string dir, TreeNode td)
        {
            // Get all subdirectories
            string[] subdirectoryEntries = Directory.GetDirectories(dir);
            // Loop through them to see if they have any other subdirectories
            foreach (string subdirectory in subdirectoryEntries)
            {
                if (Directory.GetFiles(subdirectory, "*.fit", SearchOption.AllDirectories).Length > 0)
                {
                    DirectoryInfo di = new DirectoryInfo(subdirectory);
                    TreeNode tds = td.Nodes.Add(di.FullName, di.Name);
                    tds.StateImageIndex = 0;
                    tds.Tag = di.FullName;
                    LoadFiles(subdirectory, tds);
                    LoadSubDirectories(subdirectory, tds);
                }
            }
        }

        private void LoadFiles(string dir, TreeNode td)
        {
            string[] Files = Directory.GetFiles(dir, "*.fit", SearchOption.TopDirectoryOnly);
            // Loop through them to see files
            foreach (string file in Files)
            {
                FileInfo fi = new FileInfo(file);
                TreeNode tds = td.Nodes.Add(fi.Name);
                tds.Tag = fi.FullName;
                tds.StateImageIndex = 1;
            }
        }

        private void treeView1_MouseMove(object sender, MouseEventArgs e)
        {
            // Get the node at the current mouse pointer location.
            TreeNode theNode = this.treeView1.GetNodeAt(e.X, e.Y);

            // Set a ToolTip only if the mouse pointer is actually paused on a node.
            if (theNode != null && theNode.Tag != null)
            {
                // Change the ToolTip only if the pointer moved to a new node.
                if (theNode.Tag.ToString() != this.toolTip1.GetToolTip(this.treeView1))
                    this.toolTip1.SetToolTip(this.treeView1, theNode.Tag.ToString());
            }
            else     // Pointer is not over a node so clear the ToolTip.
            {
                this.toolTip1.SetToolTip(this.treeView1, "");
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (!StartUpFlag)
            {
                txtDirectoryPath.Text = treeView1.SelectedNode.Name;
                //LoadDirectory(txtDirectoryPath.Text);
                InitializeGrid();
            }
            else
                StartUpFlag = false;
        }


        #region celledit

        public string CurrentCellValue { get; set; }

        private void FieldDataGrid_CellClick(object sender, DataGridViewCellEventArgs dgArgs)
        {
            CurrentCellValue = FieldDataGrid.CurrentCell.Value.ToString();
            bool cellEditMode = FieldDataGrid.BeginEdit(false);
        }

        private void FieldDataGrid_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.CellStyle.BackColor = Color.Aquamarine;

            if (e.Control is DataGridViewTextBoxEditingControl tb)
            {
                tb.KeyDown -= dataGridView1_KeyDown;
                tb.KeyDown += dataGridView1_KeyDown;
            }
        }

        //then in your keydown event handler, execute your code
        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                FieldDataGrid.EndEdit();
            }
        }

        private void FieldDataGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.FieldDataGrid.SelectedCells.Count > 1)
                return;
            string? oldValue = this.FieldDataGrid.CurrentCell.Value.ToString();
            int fieldIdx = this.FieldDataGrid.CurrentCell.ColumnIndex;
            int fileIdx = this.FieldDataGrid.CurrentCell.RowIndex;
            string fieldName = this.FieldDataGrid.Columns[fieldIdx].HeaderText;
            string filePath = Properties.Settings.Default.RootDirectory + "\\" + this.FieldDataGrid.Rows[fileIdx].HeaderCell.Value.ToString() + ".fit";
            FormEditField eForm = new FormEditField(filePath, fieldName, oldValue);
            eForm.ShowDialog();
            this.FieldDataGrid.CurrentCell.Value = eForm.RevisedValue;
            // FitsFielder.ResetFielder();
            InitializeGrid();
        }

        private void FieldDataGrid_CellEndEdit(object sender, DataGridViewCellEventArgs dgArgs)
        {
            string? fieldValue = this.FieldDataGrid.CurrentCell.Value.ToString();
            if (fieldValue == CurrentCellValue)
            {
                return;
            }
            string? fieldName = FieldDataGrid.Columns[FieldDataGrid.CurrentCell.ColumnIndex].HeaderCell.Value.ToString();
            string? fitsFileName = FieldDataGrid.Rows[FieldDataGrid.CurrentCell.RowIndex].HeaderCell.Value.ToString();
            string? fitsFilePath = Properties.Settings.Default.RootDirectory + "\\" + fitsFileName + ".fit";
            //Update fits file with new field value
            DialogResult vResult = MessageBox.Show("Update FITS data?", "Verify Change", MessageBoxButtons.OKCancel);
            if (vResult == DialogResult.OK)
            {
                FitsFile ff = new FitsFile(fitsFilePath);
                ff.ReplaceKey(fieldName, fieldValue);
                ff.SaveFile();
            }
        }

        #endregion
    }

    #endregion

}



