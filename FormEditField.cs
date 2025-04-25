using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Windows.Media.AppBroadcasting;

namespace FitsLibrarian
{
    public partial class FormEditField : Form
    {

        public string FitsFilePath { get; set; } = string.Empty;
        public string RevisedValue { get; set; } = string.Empty;

        public FormEditField(string fPath, string fieldName, string? fieldValue)
        {
            InitializeComponent();
            FieldNameBox.Text = fieldName;
            FieldValueBox.Text = fieldValue;
            FitsFilePath = fPath;
            RevisedValue = fieldValue;
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            NewValueBox = null;
            Close();
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            //Modify, Add or Delete according to radio button
            if (ChangeRadioButton.Checked)
            {
                //Update fits file with new field value
                FitsFile ff = new FitsFile(FitsFilePath);
                ff.ReplaceKey(FieldNameBox.Text, NewValueBox.Text);
                ff.SaveFile();
                RevisedValue = NewValueBox.Text;
            }
            if (AddRadioButton.Checked)
            {
                FitsFile ff = new FitsFile(FitsFilePath);
                ff.AddKey(FieldNameBox.Text, NewValueBox.Text);
                ff.SaveFile();
                RevisedValue = NewValueBox.Text;
            }
            if (DeleteRadioButton.Checked)
            {
                FitsFile ff = new FitsFile(FitsFilePath);
                ff.DeleteKey(FieldNameBox.Text);
                ff.SaveFile();
                RevisedValue = null;

            }
            Close();
        }
    }
}
