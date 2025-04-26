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
            label2 = new Label();
            FieldValueBox = new Label();
            label3 = new Label();
            NewValueBox = new TextBox();
            ChangeRadioButton = new RadioButton();
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
            OkButton.Location = new Point(17, 167);
            OkButton.Name = "OkButton";
            OkButton.Size = new Size(75, 23);
            OkButton.TabIndex = 0;
            OkButton.Text = "OK";
            OkButton.UseVisualStyleBackColor = false;
            OkButton.Click += OkButton_Click;
            // 
            // CancelButton
            // 
            CancelButton.BackColor = Color.Honeydew;
            CancelButton.ForeColor = Color.Black;
            CancelButton.Location = new Point(192, 167);
            CancelButton.Name = "CancelButton";
            CancelButton.Size = new Size(75, 23);
            CancelButton.TabIndex = 1;
            CancelButton.Text = "Cancel";
            CancelButton.UseVisualStyleBackColor = false;
            CancelButton.Click += CancelButton_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(17, 81);
            label1.Name = "label1";
            label1.Size = new Size(32, 15);
            label1.TabIndex = 2;
            label1.Text = "Field";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(17, 106);
            label2.Name = "label2";
            label2.Size = new Size(47, 15);
            label2.TabIndex = 4;
            label2.Text = "Current";
            // 
            // FieldValueBox
            // 
            FieldValueBox.AutoSize = true;
            FieldValueBox.Location = new Point(107, 106);
            FieldValueBox.Name = "FieldValueBox";
            FieldValueBox.Size = new Size(38, 15);
            FieldValueBox.TabIndex = 5;
            FieldValueBox.Text = "label2";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(17, 132);
            label3.Name = "label3";
            label3.Size = new Size(31, 15);
            label3.TabIndex = 6;
            label3.Text = "New";
            // 
            // NewValueBox
            // 
            NewValueBox.BackColor = Color.Honeydew;
            NewValueBox.ForeColor = Color.Black;
            NewValueBox.Location = new Point(107, 132);
            NewValueBox.Name = "NewValueBox";
            NewValueBox.Size = new Size(159, 23);
            NewValueBox.TabIndex = 7;
            // 
            // ChangeRadioButton
            // 
            ChangeRadioButton.AutoSize = true;
            ChangeRadioButton.Checked = true;
            ChangeRadioButton.ForeColor = Color.Black;
            ChangeRadioButton.Location = new Point(14, 20);
            ChangeRadioButton.Margin = new Padding(2, 1, 2, 1);
            ChangeRadioButton.Name = "ChangeRadioButton";
            ChangeRadioButton.Size = new Size(66, 19);
            ChangeRadioButton.TabIndex = 8;
            ChangeRadioButton.TabStop = true;
            ChangeRadioButton.Text = "Change";
            ChangeRadioButton.UseVisualStyleBackColor = true;
            // 
            // AddRadioButton
            // 
            AddRadioButton.AutoSize = true;
            AddRadioButton.ForeColor = Color.Black;
            AddRadioButton.Location = new Point(107, 20);
            AddRadioButton.Margin = new Padding(2, 1, 2, 1);
            AddRadioButton.Name = "AddRadioButton";
            AddRadioButton.Size = new Size(47, 19);
            AddRadioButton.TabIndex = 9;
            AddRadioButton.Text = "Add";
            AddRadioButton.UseVisualStyleBackColor = true;
            // 
            // DeleteRadioButton
            // 
            DeleteRadioButton.AutoSize = true;
            DeleteRadioButton.ForeColor = Color.Black;
            DeleteRadioButton.Location = new Point(179, 20);
            DeleteRadioButton.Margin = new Padding(2, 1, 2, 1);
            DeleteRadioButton.Name = "DeleteRadioButton";
            DeleteRadioButton.Size = new Size(58, 19);
            DeleteRadioButton.TabIndex = 10;
            DeleteRadioButton.Text = "Delete";
            DeleteRadioButton.UseVisualStyleBackColor = true;
            // 
            // ModifyGroupBox
            // 
            ModifyGroupBox.BackColor = Color.Honeydew;
            ModifyGroupBox.Controls.Add(DeleteRadioButton);
            ModifyGroupBox.Controls.Add(AddRadioButton);
            ModifyGroupBox.Controls.Add(ChangeRadioButton);
            ModifyGroupBox.Location = new Point(13, 9);
            ModifyGroupBox.Margin = new Padding(2, 1, 2, 1);
            ModifyGroupBox.Name = "ModifyGroupBox";
            ModifyGroupBox.Padding = new Padding(2, 1, 2, 1);
            ModifyGroupBox.Size = new Size(254, 53);
            ModifyGroupBox.TabIndex = 11;
            ModifyGroupBox.TabStop = false;
            ModifyGroupBox.Text = "Modify";
            // 
            // FieldNameBox
            // 
            FieldNameBox.BackColor = Color.Honeydew;
            FieldNameBox.ForeColor = Color.Black;
            FieldNameBox.Location = new Point(107, 79);
            FieldNameBox.Margin = new Padding(2, 1, 2, 1);
            FieldNameBox.Name = "FieldNameBox";
            FieldNameBox.Size = new Size(159, 23);
            FieldNameBox.TabIndex = 12;
            // 
            // FormEditField
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.LightSeaGreen;
            ClientSize = new Size(282, 206);
            Controls.Add(FieldNameBox);
            Controls.Add(ModifyGroupBox);
            Controls.Add(NewValueBox);
            Controls.Add(label3);
            Controls.Add(FieldValueBox);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(CancelButton);
            Controls.Add(OkButton);
            ForeColor = Color.White;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "FormEditField";
            Text = "Modify Heard Field";
            ModifyGroupBox.ResumeLayout(false);
            ModifyGroupBox.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button OkButton;
        private Button CancelButton;
        private Label label1;
        private Label label2;
        private Label FieldValueBox;
        private Label label3;
        private TextBox NewValueBox;
        private RadioButton ChangeRadioButton;
        private RadioButton AddRadioButton;
        private RadioButton DeleteRadioButton;
        private GroupBox ModifyGroupBox;
        private TextBox FieldNameBox;
    }
}