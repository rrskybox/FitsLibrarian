using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Windows.Media.AppBroadcasting;

namespace FitsLibrarian
{
    public partial class FormEditField : Form
    {

        public List<string> FitsFilePath { get; set; }

        public FormEditField(List<string> fPath)
        {
            InitializeComponent();
            FitsFilePath = fPath;
            FieldNameBox.Enabled = false;
            NewValueBox.Enabled = false;
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            NewValueBox = null;
            Close();
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            //Add or Delete according to radio button
            if (AddRadioButton.Checked)
            {
                foreach (string filePath in FitsFilePath)
                {
                    FitsFile ff = new FitsFile(filePath);
                    if (ff.AddKey(FieldNameBox.Text, NewValueBox.Text))
                    {
                        ff.SaveFile();
                    }
                }
            }
            if (DeleteRadioButton.Checked)
            {
                foreach (string filePath in FitsFilePath)
                {
                    FitsFile ff = new FitsFile(filePath);
                    ff.DeleteKey(FieldNameBox.Text);
                    ff.SaveFile();
                }
            }
            Close();
        }

        private void AddRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            NewValueBox.Enabled = true;
            FieldNameBox.Enabled = true;
        }

        private void DeleteRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            NewValueBox.Enabled = false;
            FieldNameBox.Enabled = true;
        }
    }
}
