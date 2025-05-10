namespace FitsLibrarian
{
    partial class FormEditField
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormEditField));
            OkButton = new Button();
            CancelButton = new Button();
            label1 = new Label();
            label3 = new Label();
            NewValueBox = new TextBox();
            AddRadioButton = new RadioButton();
            DeleteRadioButton = new RadioButton();
            ModifyGroupBox = new GroupBox();
            FieldNameBox = new TextBox();
            ModifyGroupBox.SuspendLayout();
            SuspendLayout();
            // 
            // OkButton
            // 
            OkButton.BackColor = Color.Honeydew;
            OkButton.ForeColor = Color.Black;
            OkButton.Location = new Point(32, 310);
            OkButton.Margin = new Padding(6);
            OkButton.Name = "OkButton";
            OkButton.Size = new Size(139, 49);
            OkButton.TabIndex = 0;
            OkButton.Text = "OK";
            OkButton.UseVisualStyleBackColor = false;
            OkButton.Click += OkButton_Click;
            // 
            // CancelButton
            // 
            CancelButton.BackColor = Color.Honeydew;
            CancelButton.ForeColor = Color.Black;
            CancelButton.Location = new Point(357, 310);
            CancelButton.Margin = new Padding(6);
            CancelButton.Name = "CancelButton";
            CancelButton.Size = new Size(139, 49);
            CancelButton.TabIndex = 1;
            CancelButton.Text = "Cancel";
            CancelButton.UseVisualStyleBackColor = false;
            CancelButton.Click += CancelButton_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(32, 173);
            label1.Margin = new Padding(6, 0, 6, 0);
            label1.Name = "label1";
            label1.Size = new Size(136, 32);
            label1.TabIndex = 2;
            label1.Text = "Field Name";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(32, 236);
            label3.Margin = new Padding(6, 0, 6, 0);
            label3.Name = "label3";
            label3.Size = new Size(127, 32);
            label3.TabIndex = 6;
            label3.Text = "New Value";
            // 
            // NewValueBox
            // 
            NewValueBox.BackColor = Color.Honeydew;
            NewValueBox.Enabled = false;
            NewValueBox.ForeColor = Color.Black;
            NewValueBox.Location = new Point(199, 236);
            NewValueBox.Margin = new Padding(6);
            NewValueBox.Name = "NewValueBox";
            NewValueBox.Size = new Size(292, 39);
            NewValueBox.TabIndex = 7;
            // 
            // AddRadioButton
            // 
            AddRadioButton.AutoSize = true;
            AddRadioButton.ForeColor = Color.Black;
            AddRadioButton.Location = new Point(125, 43);
            AddRadioButton.Margin = new Padding(4, 2, 4, 2);
            AddRadioButton.Name = "AddRadioButton";
            AddRadioButton.Size = new Size(88, 36);
            AddRadioButton.TabIndex = 9;
            AddRadioButton.Text = "Add";
            AddRadioButton.UseVisualStyleBackColor = true;
            AddRadioButton.CheckedChanged += AddRadioButton_CheckedChanged;
            // 
            // DeleteRadioButton
            // 
            DeleteRadioButton.AutoSize = true;
            DeleteRadioButton.ForeColor = Color.Black;
            DeleteRadioButton.Location = new Point(258, 43);
            DeleteRadioButton.Margin = new Padding(4, 2, 4, 2);
            DeleteRadioButton.Name = "DeleteRadioButton";
            DeleteRadioButton.Size = new Size(115, 36);
            DeleteRadioButton.TabIndex = 10;
            DeleteRadioButton.Text = "Delete";
            DeleteRadioButton.UseVisualStyleBackColor = true;
            DeleteRadioButton.CheckedChanged += DeleteRadioButton_CheckedChanged;
            // 
            // ModifyGroupBox
            // 
            ModifyGroupBox.BackColor = Color.Honeydew;
            ModifyGroupBox.Controls.Add(DeleteRadioButton);
            ModifyGroupBox.Controls.Add(AddRadioButton);
            ModifyGroupBox.Location = new Point(24, 19);
            ModifyGroupBox.Margin = new Padding(4, 2, 4, 2);
            ModifyGroupBox.Name = "ModifyGroupBox";
            ModifyGroupBox.Padding = new Padding(4, 2, 4, 2);
            ModifyGroupBox.Size = new Size(472, 113);
            ModifyGroupBox.TabIndex = 11;
            ModifyGroupBox.TabStop = false;
            ModifyGroupBox.Text = "Modify ";
            // 
            // FieldNameBox
            // 
            FieldNameBox.BackColor = Color.Honeydew;
            FieldNameBox.ForeColor = Color.Black;
            FieldNameBox.Location = new Point(199, 169);
            FieldNameBox.Margin = new Padding(4, 2, 4, 2);
            FieldNameBox.Name = "FieldNameBox";
            FieldNameBox.Size = new Size(292, 39);
            FieldNameBox.TabIndex = 12;
            // 
            // FormEditField
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.LightSeaGreen;
            ClientSize = new Size(524, 391);
            Controls.Add(FieldNameBox);
            Controls.Add(ModifyGroupBox);
            Controls.Add(NewValueBox);
            Controls.Add(label3);
            Controls.Add(label1);
            Controls.Add(CancelButton);
            Controls.Add(OkButton);
            ForeColor = Color.White;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(6);
            Name = "FormEditField";
            Text = "Modify Header Field";
            ModifyGroupBox.ResumeLayout(false);
            ModifyGroupBox.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button OkButton;
        private Button CancelButton;
        private Label label1;
        private Label label3;
        private TextBox NewValueBox;
        private RadioButton AddRadioButton;
        private RadioButton DeleteRadioButton;
        private GroupBox ModifyGroupBox;
        private TextBox FieldNameBox;
    }
}