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

        public FormEditField(string fPath, string fieldName)
        {
            InitializeComponent();
            FieldNameBox.Text = fieldName;
            FieldValueBox.Text = "";
            FitsFilePath = fPath;
            RevisedValue = "";
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
                FitsFile ff = new FitsFile(FitsFilePath);
                if (ff.AddKey(FieldNameBox.Text, NewValueBox.Text))
                {
                    ff.SaveFile();
                    RevisedValue = NewValueBox.Text;
                }
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
