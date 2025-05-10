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
using System.Diagnostics.Eventing.Reader;

namespace FitsLibrarian

{
    public partial class FormFitsLibrarian : Form
    {
        private bool StartUpFlag = true;
        private List<string> FitsList = new List<string>();

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
            treeView1.ExpandAll();
            Show();
        }

        private void InitializeGrid(string singleFilePath = "")
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
                FitsList = new List<string>() { singleFilePath };
            else
                FitsList = Directory.GetFiles(txtDirectoryPath.Text, "*.fit?", SearchOption.AllDirectories).ToList();
            if (FitsList.Count < 1)
                return;
            if (FitsList.Count > 40)
            {
                DialogResult dr = MessageBox.Show(FitsList.Count + " fits files have been found in this directory tree.  This could take awhile.  Continue?", "Large count check", MessageBoxButtons.OKCancel);
                if (dr != DialogResult.OK)
                    return;
            }
            FitsFielder.ResetFielder();
            FitsFielder.LoadEnabledFields();
            // For each file compile the fits header data into the common field list and the column list
            int rowIndex = 0;
            foreach (string fpath in FitsList)
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
                if (txtDirectoryPath.Text == "")
                {
                    string rootPath = Properties.Settings.Default.RootDirectory;
                    string treePath = Directory.GetFiles(rootPath, "*.fit", SearchOption.AllDirectories).First(x => x.Contains(treeView1.SelectedNode.FullPath));
                    InitializeGrid(treePath);
                }
                else
                    InitializeGrid();
            }
            else
                StartUpFlag = false;
        }

        #region celledit

        public string CurrentCellValue { get; set; }
        public DataGridViewSelectedCellCollection SelectedGridCells { get; set; }

        private void FieldDataGrid_CellClick(object sender, DataGridViewCellEventArgs dgArgs)
        {
            //if (dgArgs.)
            //If a whole row is selected (by picking the row header)
            //then this is going to be an add or delete a whole field.
            //If so, then launch FormEditField
            int col = dgArgs.ColumnIndex;
            int row = dgArgs.RowIndex;
            if (col == -1 && row == -1) //Add or Delete Field in all files
            {
                List<string> filePathList = FitsList;
                FormEditField eForm = new FormEditField(filePathList);
                eForm.ShowDialog();
                InitializeGrid();
            }
            else if (dgArgs.ColumnIndex == -1) //Row Header Selected:  Add/Delete Fields
            {
                List<string> filePathList = new List<string>();
                string fitsFileName = FieldDataGrid.Rows[row].HeaderCell.Value.ToString();
                string fullFilePath = FitsList.First(x => x.Contains(fitsFileName));
                filePathList.Add(fullFilePath);
                FormEditField eForm = new FormEditField(filePathList);
                eForm.ShowDialog();
                InitializeGrid();
            }
            else if (dgArgs.RowIndex == -1) //Column Header Selected:  Ignore
                return;
            else
            {
                SelectedGridCells = FieldDataGrid.SelectedCells;
                CurrentCellValue = (FieldDataGrid.CurrentCell.Value ?? "").ToString();
                bool cellEditMode = FieldDataGrid.BeginEdit(false);
            }
        }

        private void FieldDataGrid_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.CellStyle.BackColor = Color.Aquamarine;
            ColorSelectedCells(SelectedGridCells, Color.Green);
            if (e.Control is DataGridViewTextBoxEditingControl tb)
            {
                tb.KeyDown -= FieldDataGrid_KeyDown;
                tb.KeyDown += FieldDataGrid_KeyDown;
            }
        }

        //then in your keydown event handler, execute your code
        private void FieldDataGrid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                FieldDataGrid.EndEdit();
            }
        }

        private void FieldDataGrid_CellEndEdit(object sender, DataGridViewCellEventArgs dgArgs)
        {
            string? fieldValue = (this.FieldDataGrid.CurrentCell.Value ?? "").ToString();
            if (fieldValue == CurrentCellValue)
            {
                return;
            }
            string? fieldName = FieldDataGrid.Columns[FieldDataGrid.CurrentCell.ColumnIndex].HeaderCell.Value.ToString();
            //Update fits file with new field value
            DialogResult vResult = MessageBox.Show("Update FITS data?", "Verify Change", MessageBoxButtons.OKCancel);
            if (vResult == DialogResult.OK)
            {
                foreach (DataGridViewCell dgvc in SelectedGridCells)
                {
                    int gridRow = dgvc.RowIndex;
                    string? fitsFileName = FieldDataGrid.Rows[gridRow].HeaderCell.Value.ToString();
                    string? fitsFullFilePath = FitsList.First(x => x.Contains(fitsFileName));
                    FitsFile ff = new FitsFile(fitsFullFilePath);
                    ff.ReplaceKey(fieldName, fieldValue);
                    ff.SaveFile();
                }
            }
            InitializeGrid();
        }

        #endregion

        private void FieldDataGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            return;
            //if (this.FieldDataGrid.SelectedCells.Count > 1)
            //    return;
            //else
            //    LaunchFormEditField();
        }

        private void FieldDataGrid_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //Row header has been clicked on.  Launch Add/Delete Field

        }

        private void FieldDataGrid_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //Column header has been clicked on.  Launch Add/Delete Field

        }

        private void FieldDataGrid_ColumnHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //Column header has been double clicked on.  Launch Add/Delete Field

        }

        private void ColorSelectedCells(DataGridViewSelectedCellCollection dgvCells, Color color)
        {
            foreach (DataGridViewCell cell in dgvCells) FieldDataGrid.Rows[cell.RowIndex].Cells[cell.ColumnIndex].Style.BackColor = color;
        }

    }

    #endregion

}



