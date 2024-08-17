namespace Labs
{
    partial class Opening_Form
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.go_button = new System.Windows.Forms.Button();
            this.Simplex_choice = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // go_button
            // 
            this.go_button.Location = new System.Drawing.Point(321, 313);
            this.go_button.Name = "go_button";
            this.go_button.Size = new System.Drawing.Size(75, 23);
            this.go_button.TabIndex = 1;
            this.go_button.Text = "Начать";
            this.go_button.UseVisualStyleBackColor = true;
            this.go_button.Click += new System.EventHandler(this.go_button_Click);
            // 
            // Simplex_choice
            // 
            this.Simplex_choice.AutoSize = true;
            this.Simplex_choice.Location = new System.Drawing.Point(146, 72);
            this.Simplex_choice.Name = "Simplex_choice";
            this.Simplex_choice.Size = new System.Drawing.Size(140, 17);
            this.Simplex_choice.TabIndex = 3;
            this.Simplex_choice.TabStop = true;
            this.Simplex_choice.Text = "Симплекс-метод (лб.2)";
            this.Simplex_choice.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(146, 95);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(101, 17);
            this.radioButton1.TabIndex = 4;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "M-метод (лб. 3)";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // Opening_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(408, 348);
            this.Controls.Add(this.radioButton1);
            this.Controls.Add(this.Simplex_choice);
            this.Controls.Add(this.go_button);
            this.Name = "Opening_Form";
            this.Text = "LabsMMP";
            this.Load += new System.EventHandler(this.Opening_Form_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button go_button;
        private System.Windows.Forms.RadioButton Simplex_choice;
        private System.Windows.Forms.RadioButton radioButton1;
    }
}

