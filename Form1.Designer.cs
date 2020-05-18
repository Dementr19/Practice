namespace ExtrapolationApp
{
    partial class Form1
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
            this.label1 = new System.Windows.Forms.Label();
            this.tbxVectorX = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbxVectorY = new System.Windows.Forms.TextBox();
            this.btnPower = new System.Windows.Forms.Button();
            this.btnQuadratic = new System.Windows.Forms.Button();
            this.btnCubic = new System.Windows.Forms.Button();
            this.btnHyperbolic = new System.Windows.Forms.Button();
            this.btnPokazatel = new System.Windows.Forms.Button();
            this.btnLogarithmic = new System.Windows.Forms.Button();
            this.btтExponential = new System.Windows.Forms.Button();
            this.btnCLear = new System.Windows.Forms.Button();
            this.btnProbe = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbResult = new System.Windows.Forms.ListBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Вектор X";
            // 
            // tbxVectorX
            // 
            this.tbxVectorX.Location = new System.Drawing.Point(71, 21);
            this.tbxVectorX.Name = "tbxVectorX";
            this.tbxVectorX.Size = new System.Drawing.Size(468, 20);
            this.tbxVectorX.TabIndex = 1;
            this.tbxVectorX.Text = "-3  -2  -1  0.2  1  2  3";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Вектор Y";
            // 
            // tbxVectorY
            // 
            this.tbxVectorY.Location = new System.Drawing.Point(71, 47);
            this.tbxVectorY.Name = "tbxVectorY";
            this.tbxVectorY.Size = new System.Drawing.Size(468, 20);
            this.tbxVectorY.TabIndex = 1;
            this.tbxVectorY.Text = "3.5  -8.4  -7.3  4  7.6  8.1  -3.9";
            // 
            // btnPower
            // 
            this.btnPower.Location = new System.Drawing.Point(14, 85);
            this.btnPower.Name = "btnPower";
            this.btnPower.Size = new System.Drawing.Size(179, 23);
            this.btnPower.TabIndex = 3;
            this.btnPower.Text = "Степенная регрессия";
            this.btnPower.UseVisualStyleBackColor = true;
            this.btnPower.Click += new System.EventHandler(this.btnPower_Click);
            // 
            // btnQuadratic
            // 
            this.btnQuadratic.Location = new System.Drawing.Point(14, 114);
            this.btnQuadratic.Name = "btnQuadratic";
            this.btnQuadratic.Size = new System.Drawing.Size(179, 23);
            this.btnQuadratic.TabIndex = 3;
            this.btnQuadratic.Text = "Квадратичная регрессия";
            this.btnQuadratic.UseVisualStyleBackColor = true;
            this.btnQuadratic.Click += new System.EventHandler(this.btnQuadratic_Click);
            // 
            // btnCubic
            // 
            this.btnCubic.Location = new System.Drawing.Point(14, 143);
            this.btnCubic.Name = "btnCubic";
            this.btnCubic.Size = new System.Drawing.Size(179, 23);
            this.btnCubic.TabIndex = 3;
            this.btnCubic.Text = "Кубическая регрессия";
            this.btnCubic.UseVisualStyleBackColor = true;
            this.btnCubic.Click += new System.EventHandler(this.btnCubic_Click);
            // 
            // btnHyperbolic
            // 
            this.btnHyperbolic.Location = new System.Drawing.Point(14, 173);
            this.btnHyperbolic.Name = "btnHyperbolic";
            this.btnHyperbolic.Size = new System.Drawing.Size(179, 23);
            this.btnHyperbolic.TabIndex = 2;
            this.btnHyperbolic.Text = "Гиперболическая регрессия";
            this.btnHyperbolic.UseVisualStyleBackColor = true;
            this.btnHyperbolic.Click += new System.EventHandler(this.btnHyperbolic_Click);
            // 
            // btnPokazatel
            // 
            this.btnPokazatel.Location = new System.Drawing.Point(220, 85);
            this.btnPokazatel.Name = "btnPokazatel";
            this.btnPokazatel.Size = new System.Drawing.Size(179, 23);
            this.btnPokazatel.TabIndex = 3;
            this.btnPokazatel.Text = "Показательная регрессия";
            this.btnPokazatel.UseVisualStyleBackColor = true;
            this.btnPokazatel.Click += new System.EventHandler(this.btnPokazatel_Click);
            // 
            // btnLogarithmic
            // 
            this.btnLogarithmic.Location = new System.Drawing.Point(220, 114);
            this.btnLogarithmic.Name = "btnLogarithmic";
            this.btnLogarithmic.Size = new System.Drawing.Size(179, 23);
            this.btnLogarithmic.TabIndex = 3;
            this.btnLogarithmic.Text = "Логарифмическая регрессия";
            this.btnLogarithmic.UseVisualStyleBackColor = true;
            this.btnLogarithmic.Click += new System.EventHandler(this.btnLogarithmic_Click);
            // 
            // btтExponential
            // 
            this.btтExponential.Location = new System.Drawing.Point(220, 143);
            this.btтExponential.Name = "btтExponential";
            this.btтExponential.Size = new System.Drawing.Size(179, 23);
            this.btтExponential.TabIndex = 3;
            this.btтExponential.Text = "Экспоненциальная регрессия";
            this.btтExponential.UseVisualStyleBackColor = true;
            this.btтExponential.Click += new System.EventHandler(this.btтExponential_Click);
            // 
            // btnCLear
            // 
            this.btnCLear.Location = new System.Drawing.Point(467, 173);
            this.btnCLear.Name = "btnCLear";
            this.btnCLear.Size = new System.Drawing.Size(75, 23);
            this.btnCLear.TabIndex = 5;
            this.btnCLear.Text = "Очистить";
            this.btnCLear.UseVisualStyleBackColor = true;
            this.btnCLear.Click += new System.EventHandler(this.btnCLear_Click);
            // 
            // btnProbe
            // 
            this.btnProbe.Location = new System.Drawing.Point(423, 85);
            this.btnProbe.Name = "btnProbe";
            this.btnProbe.Size = new System.Drawing.Size(116, 23);
            this.btnProbe.TabIndex = 6;
            this.btnProbe.Text = "Обработать";
            this.btnProbe.UseVisualStyleBackColor = true;
            this.btnProbe.Click += new System.EventHandler(this.btnProbe_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnLogarithmic);
            this.panel1.Controls.Add(this.btnProbe);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btnCLear);
            this.panel1.Controls.Add(this.tbxVectorX);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.btтExponential);
            this.panel1.Controls.Add(this.tbxVectorY);
            this.panel1.Controls.Add(this.btnPower);
            this.panel1.Controls.Add(this.btnCubic);
            this.panel1.Controls.Add(this.btnHyperbolic);
            this.panel1.Controls.Add(this.btnPokazatel);
            this.panel1.Controls.Add(this.btnQuadratic);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(554, 209);
            this.panel1.TabIndex = 7;
            // 
            // lbResult
            // 
            this.lbResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbResult.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbResult.FormattingEnabled = true;
            this.lbResult.HorizontalScrollbar = true;
            this.lbResult.ItemHeight = 16;
            this.lbResult.Location = new System.Drawing.Point(0, 209);
            this.lbResult.Name = "lbResult";
            this.lbResult.Size = new System.Drawing.Size(554, 353);
            this.lbResult.TabIndex = 8;
            // 
            // Form1
            // 
            this.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(554, 562);
            this.Controls.Add(this.lbResult);
            this.Controls.Add(this.panel1);
            this.MinimumSize = new System.Drawing.Size(570, 600);
            this.Name = "Form1";
            this.Text = "Экстраполяция";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbxVectorX;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbxVectorY;
        private System.Windows.Forms.Button btnPower;
        private System.Windows.Forms.Button btnQuadratic;
        private System.Windows.Forms.Button btnCubic;
        private System.Windows.Forms.Button btnHyperbolic;
        private System.Windows.Forms.Button btnPokazatel;
        private System.Windows.Forms.Button btnLogarithmic;
        private System.Windows.Forms.Button btтExponential;
        private System.Windows.Forms.Button btnCLear;
        private System.Windows.Forms.Button btnProbe;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ListBox lbResult;
    }
}

