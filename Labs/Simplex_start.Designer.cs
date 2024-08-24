namespace Labs
{
    partial class Simplex_start
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
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.inequalities_q = new System.Windows.Forms.NumericUpDown();
            this.Exes_q = new System.Windows.Forms.NumericUpDown();
            this.table_button = new System.Windows.Forms.Button();
            this.starting_button = new System.Windows.Forms.Button();
            this.label_if_empty = new System.Windows.Forms.Label();
            this.minButton = new System.Windows.Forms.RadioButton();
            this.maxButton = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.inequalities_q)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Exes_q)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(30, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(125, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Сколько неизвестных?";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(118, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Сколько неравенств?";
            // 
            // inequalities_q
            // 
            this.inequalities_q.Location = new System.Drawing.Point(164, 37);
            this.inequalities_q.Maximum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.inequalities_q.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.inequalities_q.Name = "inequalities_q";
            this.inequalities_q.Size = new System.Drawing.Size(120, 20);
            this.inequalities_q.TabIndex = 5;
            this.inequalities_q.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // Exes_q
            // 
            this.Exes_q.Location = new System.Drawing.Point(164, 70);
            this.Exes_q.Maximum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.Exes_q.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.Exes_q.Name = "Exes_q";
            this.Exes_q.Size = new System.Drawing.Size(120, 20);
            this.Exes_q.TabIndex = 4;
            this.Exes_q.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // table_button
            // 
            this.table_button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.table_button.ForeColor = System.Drawing.Color.Black;
            this.table_button.Location = new System.Drawing.Point(324, 53);
            this.table_button.Name = "table_button";
            this.table_button.Size = new System.Drawing.Size(115, 23);
            this.table_button.TabIndex = 8;
            this.table_button.Text = "Показать таблицу";
            this.table_button.UseVisualStyleBackColor = false;
            this.table_button.Click += new System.EventHandler(this.table_button_Click);
            // 
            // starting_button
            // 
            this.starting_button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.starting_button.ForeColor = System.Drawing.Color.Black;
            this.starting_button.Location = new System.Drawing.Point(410, 370);
            this.starting_button.Name = "starting_button";
            this.starting_button.Size = new System.Drawing.Size(115, 23);
            this.starting_button.TabIndex = 9;
            this.starting_button.Text = "Вычислить";
            this.starting_button.UseVisualStyleBackColor = false;
            this.starting_button.Click += new System.EventHandler(this.starting_button_Click);
            // 
            // label_if_empty
            // 
            this.label_if_empty.AutoSize = true;
            this.label_if_empty.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label_if_empty.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label_if_empty.Location = new System.Drawing.Point(30, 370);
            this.label_if_empty.Name = "label_if_empty";
            this.label_if_empty.Size = new System.Drawing.Size(228, 13);
            this.label_if_empty.TabIndex = 10;
            this.label_if_empty.Text = "Пожалуйста, проверьте введенные данные";
            // 
            // minButton
            // 
            this.minButton.AutoSize = true;
            this.minButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.minButton.Location = new System.Drawing.Point(164, 332);
            this.minButton.Name = "minButton";
            this.minButton.Size = new System.Drawing.Size(112, 17);
            this.minButton.TabIndex = 21;
            this.minButton.TabStop = true;
            this.minButton.Text = "Искать минимум";
            this.minButton.UseVisualStyleBackColor = false;
            // 
            // maxButton
            // 
            this.maxButton.AutoSize = true;
            this.maxButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.maxButton.Location = new System.Drawing.Point(33, 332);
            this.maxButton.Name = "maxButton";
            this.maxButton.Size = new System.Drawing.Size(118, 17);
            this.maxButton.TabIndex = 20;
            this.maxButton.TabStop = true;
            this.maxButton.Text = "Искать максимум";
            this.maxButton.UseVisualStyleBackColor = false;
            // 
            // Simplex_start
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(537, 401);
            this.Controls.Add(this.minButton);
            this.Controls.Add(this.maxButton);
            this.Controls.Add(this.label_if_empty);
            this.Controls.Add(this.starting_button);
            this.Controls.Add(this.table_button);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.inequalities_q);
            this.Controls.Add(this.Exes_q);
            this.Name = "Simplex_start";
            this.Text = "Simplex_start";
            this.Load += new System.EventHandler(this.Simplex_start_Load);
            ((System.ComponentModel.ISupportInitialize)(this.inequalities_q)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Exes_q)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown inequalities_q;
        private System.Windows.Forms.NumericUpDown Exes_q;
        private System.Windows.Forms.Button table_button;
        private System.Windows.Forms.Button starting_button;
        private System.Windows.Forms.Label label_if_empty;
        private System.Windows.Forms.RadioButton minButton;
        private System.Windows.Forms.RadioButton maxButton;
    }
}