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
            this.Mmethod_choice = new System.Windows.Forms.RadioButton();
            this.Double_simplex_choice = new System.Windows.Forms.RadioButton();
            this.Gomori_choice = new System.Windows.Forms.RadioButton();
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
            this.Simplex_choice.Location = new System.Drawing.Point(52, 39);
            this.Simplex_choice.Name = "Simplex_choice";
            this.Simplex_choice.Size = new System.Drawing.Size(140, 17);
            this.Simplex_choice.TabIndex = 3;
            this.Simplex_choice.TabStop = true;
            this.Simplex_choice.Text = "Симплекс-метод (лб.2)";
            this.Simplex_choice.UseVisualStyleBackColor = true;
            // 
            // Mmethod_choice
            // 
            this.Mmethod_choice.AutoSize = true;
            this.Mmethod_choice.Location = new System.Drawing.Point(52, 62);
            this.Mmethod_choice.Name = "Mmethod_choice";
            this.Mmethod_choice.Size = new System.Drawing.Size(101, 17);
            this.Mmethod_choice.TabIndex = 4;
            this.Mmethod_choice.TabStop = true;
            this.Mmethod_choice.Text = "M-метод (лб. 3)";
            this.Mmethod_choice.UseVisualStyleBackColor = true;
            // 
            // Double_simplex_choice
            // 
            this.Double_simplex_choice.AutoSize = true;
            this.Double_simplex_choice.Location = new System.Drawing.Point(52, 85);
            this.Double_simplex_choice.Name = "Double_simplex_choice";
            this.Double_simplex_choice.Size = new System.Drawing.Size(221, 17);
            this.Double_simplex_choice.TabIndex = 5;
            this.Double_simplex_choice.TabStop = true;
            this.Double_simplex_choice.Text = "Двойственный симплекс-метод (лб. 4)";
            this.Double_simplex_choice.UseVisualStyleBackColor = true;
            // 
            // Gomori_choice
            // 
            this.Gomori_choice.AutoSize = true;
            this.Gomori_choice.Location = new System.Drawing.Point(52, 108);
            this.Gomori_choice.Name = "Gomori_choice";
            this.Gomori_choice.Size = new System.Drawing.Size(131, 17);
            this.Gomori_choice.TabIndex = 6;
            this.Gomori_choice.TabStop = true;
            this.Gomori_choice.Text = "Метод Гомори (лб. 4)";
            this.Gomori_choice.UseVisualStyleBackColor = true;
            // 
            // Opening_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(408, 348);
            this.Controls.Add(this.Gomori_choice);
            this.Controls.Add(this.Double_simplex_choice);
            this.Controls.Add(this.Mmethod_choice);
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
        private System.Windows.Forms.RadioButton Mmethod_choice;
        private System.Windows.Forms.RadioButton Double_simplex_choice;
        private System.Windows.Forms.RadioButton Gomori_choice;
    }
}

