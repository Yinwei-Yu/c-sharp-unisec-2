﻿namespace window
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.txtNum1 = new System.Windows.Forms.TextBox();
            this.txtNum2 = new System.Windows.Forms.TextBox();
            this.combOperator = new System.Windows.Forms.ComboBox();
            this.btnCalculate = new System.Windows.Forms.Button();
            this.lbResult = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtNum1
            // 
            this.txtNum1.Location = new System.Drawing.Point(107, 131);
            this.txtNum1.Name = "txtNum1";
            this.txtNum1.Size = new System.Drawing.Size(200, 28);
            this.txtNum1.TabIndex = 0;
            this.txtNum1.Text = "请输入第一个数字";
            // 
            // txtNum2
            // 
            this.txtNum2.Location = new System.Drawing.Point(490, 131);
            this.txtNum2.Name = "txtNum2";
            this.txtNum2.Size = new System.Drawing.Size(200, 28);
            this.txtNum2.TabIndex = 1;
            this.txtNum2.Text = "请输入第二个数字";
            // 
            // combOperator
            // 
            this.combOperator.FormattingEnabled = true;
            this.combOperator.Items.AddRange(new object[] {
            "+",
            "-",
            "*",
            "/",
            "%",
            "^"});
            this.combOperator.Location = new System.Drawing.Point(326, 134);
            this.combOperator.Name = "combOperator";
            this.combOperator.Size = new System.Drawing.Size(158, 26);
            this.combOperator.TabIndex = 2;
            this.combOperator.SelectedIndexChanged += new System.EventHandler(this.combOperator_SelectedIndexChanged);
            // 
            // btnCalculate
            // 
            this.btnCalculate.Location = new System.Drawing.Point(352, 200);
            this.btnCalculate.Name = "btnCalculate";
            this.btnCalculate.Size = new System.Drawing.Size(102, 44);
            this.btnCalculate.TabIndex = 3;
            this.btnCalculate.Text = "计算";
            this.btnCalculate.UseVisualStyleBackColor = true;
            this.btnCalculate.Click += new System.EventHandler(this.btnCalculate_Click);
            // 
            // lbResult
            // 
            this.lbResult.AutoSize = true;
            this.lbResult.Location = new System.Drawing.Point(380, 295);
            this.lbResult.Name = "lbResult";
            this.lbResult.Size = new System.Drawing.Size(44, 18);
            this.lbResult.TabIndex = 4;
            this.lbResult.Text = "结果";
            this.lbResult.Click += new System.EventHandler(this.lbResult_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lbResult);
            this.Controls.Add(this.btnCalculate);
            this.Controls.Add(this.combOperator);
            this.Controls.Add(this.txtNum2);
            this.Controls.Add(this.txtNum1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtNum1;
        private System.Windows.Forms.TextBox txtNum2;
        private System.Windows.Forms.ComboBox combOperator;
        private System.Windows.Forms.Button btnCalculate;
        private System.Windows.Forms.Label lbResult;
    }
}

