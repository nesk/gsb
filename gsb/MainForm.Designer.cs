namespace gsb
{
    partial class MainForm
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
            this.expensesSelect = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.repText = new System.Windows.Forms.TextBox();
            this.nuiText = new System.Windows.Forms.TextBox();
            this.kmText = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.etpText = new System.Windows.Forms.TextBox();
            this.createExpenseButton = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.removeExpenseOPButton = new System.Windows.Forms.Button();
            this.addExpenseOPButton = new System.Windows.Forms.Button();
            this.expenseOPCostText = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.expenseOPLabelText = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.expenseOPDate = new System.Windows.Forms.DateTimePicker();
            this.expensesOPList = new System.Windows.Forms.ListBox();
            this.saveExpenseButton = new System.Windows.Forms.Button();
            this.cancelExpenseButton = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // expensesSelect
            // 
            this.expensesSelect.FormattingEnabled = true;
            this.expensesSelect.Location = new System.Drawing.Point(12, 25);
            this.expensesSelect.Name = "expensesSelect";
            this.expensesSelect.Size = new System.Drawing.Size(121, 21);
            this.expensesSelect.TabIndex = 0;
            this.expensesSelect.SelectedIndexChanged += new System.EventHandler(this.ListControl_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Fiche de frais consultée :";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.tableLayoutPanel1);
            this.groupBox1.Location = new System.Drawing.Point(13, 59);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(425, 75);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Eléments forfaitisés";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Controls.Add(this.repText, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.nuiText, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.kmText, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label5, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.label4, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.label3, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.etpText, 0, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(7, 20);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(412, 49);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // repText
            // 
            this.repText.Location = new System.Drawing.Point(327, 25);
            this.repText.Margin = new System.Windows.Forms.Padding(20, 3, 20, 3);
            this.repText.Name = "repText";
            this.repText.Size = new System.Drawing.Size(61, 20);
            this.repText.TabIndex = 7;
            this.repText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // nuiText
            // 
            this.nuiText.Location = new System.Drawing.Point(225, 25);
            this.nuiText.Margin = new System.Windows.Forms.Padding(20, 3, 20, 3);
            this.nuiText.Name = "nuiText";
            this.nuiText.Size = new System.Drawing.Size(61, 20);
            this.nuiText.TabIndex = 6;
            this.nuiText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // kmText
            // 
            this.kmText.Location = new System.Drawing.Point(123, 25);
            this.kmText.Margin = new System.Windows.Forms.Padding(20, 3, 20, 3);
            this.kmText.Name = "kmText";
            this.kmText.Size = new System.Drawing.Size(61, 20);
            this.kmText.TabIndex = 5;
            this.kmText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(310, 1);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(98, 20);
            this.label5.TabIndex = 3;
            this.label5.Text = "Repas Restaurant";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(208, 1);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(95, 20);
            this.label4.TabIndex = 2;
            this.label4.Text = "Nuitée Hôtel";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(106, 1);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(95, 20);
            this.label3.TabIndex = 1;
            this.label3.Text = "Frais Kilométrique";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 1);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 20);
            this.label2.TabIndex = 0;
            this.label2.Text = "Forfait Etape";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // etpText
            // 
            this.etpText.Location = new System.Drawing.Point(21, 25);
            this.etpText.Margin = new System.Windows.Forms.Padding(20, 3, 20, 3);
            this.etpText.Name = "etpText";
            this.etpText.Size = new System.Drawing.Size(61, 20);
            this.etpText.TabIndex = 4;
            this.etpText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // createExpenseButton
            // 
            this.createExpenseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.createExpenseButton.Location = new System.Drawing.Point(198, 23);
            this.createExpenseButton.Name = "createExpenseButton";
            this.createExpenseButton.Size = new System.Drawing.Size(240, 23);
            this.createExpenseButton.TabIndex = 3;
            this.createExpenseButton.Text = "Créer une fiche de frais pour le mois courant";
            this.createExpenseButton.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.removeExpenseOPButton);
            this.groupBox2.Controls.Add(this.addExpenseOPButton);
            this.groupBox2.Controls.Add(this.expenseOPCostText);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.expenseOPLabelText);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.expenseOPDate);
            this.groupBox2.Controls.Add(this.expensesOPList);
            this.groupBox2.Location = new System.Drawing.Point(13, 147);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(425, 131);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Eléments hors forfait";
            // 
            // removeExpenseOPButton
            // 
            this.removeExpenseOPButton.Location = new System.Drawing.Point(28, 105);
            this.removeExpenseOPButton.Margin = new System.Windows.Forms.Padding(0);
            this.removeExpenseOPButton.Name = "removeExpenseOPButton";
            this.removeExpenseOPButton.Size = new System.Drawing.Size(20, 20);
            this.removeExpenseOPButton.TabIndex = 7;
            this.removeExpenseOPButton.Text = "-";
            this.removeExpenseOPButton.UseVisualStyleBackColor = true;
            // 
            // addExpenseOPButton
            // 
            this.addExpenseOPButton.Location = new System.Drawing.Point(7, 105);
            this.addExpenseOPButton.Margin = new System.Windows.Forms.Padding(0);
            this.addExpenseOPButton.Name = "addExpenseOPButton";
            this.addExpenseOPButton.Size = new System.Drawing.Size(20, 20);
            this.addExpenseOPButton.TabIndex = 6;
            this.addExpenseOPButton.Text = "+";
            this.addExpenseOPButton.UseVisualStyleBackColor = true;
            // 
            // expenseOPCostText
            // 
            this.expenseOPCostText.Location = new System.Drawing.Point(320, 70);
            this.expenseOPCostText.Name = "expenseOPCostText";
            this.expenseOPCostText.Size = new System.Drawing.Size(65, 20);
            this.expenseOPCostText.TabIndex = 5;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(317, 54);
            this.label7.Margin = new System.Windows.Forms.Padding(3, 10, 3, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(52, 13);
            this.label7.TabIndex = 4;
            this.label7.Text = "Montant :";
            // 
            // expenseOPLabelText
            // 
            this.expenseOPLabelText.Location = new System.Drawing.Point(185, 70);
            this.expenseOPLabelText.Name = "expenseOPLabelText";
            this.expenseOPLabelText.Size = new System.Drawing.Size(125, 20);
            this.expenseOPLabelText.TabIndex = 3;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(182, 53);
            this.label6.Margin = new System.Windows.Forms.Padding(3, 10, 3, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(43, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "Libellé :";
            // 
            // expenseOPDate
            // 
            this.expenseOPDate.Location = new System.Drawing.Point(185, 20);
            this.expenseOPDate.Name = "expenseOPDate";
            this.expenseOPDate.Size = new System.Drawing.Size(200, 20);
            this.expenseOPDate.TabIndex = 1;
            // 
            // expensesOPList
            // 
            this.expensesOPList.FormattingEnabled = true;
            this.expensesOPList.Location = new System.Drawing.Point(7, 20);
            this.expensesOPList.Name = "expensesOPList";
            this.expensesOPList.Size = new System.Drawing.Size(154, 82);
            this.expensesOPList.TabIndex = 0;
            this.expensesOPList.SelectedIndexChanged += new System.EventHandler(this.ListControl_SelectedIndexChanged);
            // 
            // saveExpenseButton
            // 
            this.saveExpenseButton.Location = new System.Drawing.Point(39, 284);
            this.saveExpenseButton.Margin = new System.Windows.Forms.Padding(30, 3, 3, 3);
            this.saveExpenseButton.Name = "saveExpenseButton";
            this.saveExpenseButton.Size = new System.Drawing.Size(177, 23);
            this.saveExpenseButton.TabIndex = 5;
            this.saveExpenseButton.Text = "Sauvegarder ma fiche de frais";
            this.saveExpenseButton.UseVisualStyleBackColor = true;
            // 
            // cancelExpenseButton
            // 
            this.cancelExpenseButton.Location = new System.Drawing.Point(234, 284);
            this.cancelExpenseButton.Margin = new System.Windows.Forms.Padding(3, 3, 30, 3);
            this.cancelExpenseButton.Name = "cancelExpenseButton";
            this.cancelExpenseButton.Size = new System.Drawing.Size(177, 23);
            this.cancelExpenseButton.TabIndex = 6;
            this.cancelExpenseButton.Text = "Annuler mes modifications";
            this.cancelExpenseButton.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(450, 312);
            this.Controls.Add(this.cancelExpenseButton);
            this.Controls.Add(this.saveExpenseButton);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.createExpenseButton);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.expensesSelect);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GSB - Suivi du remboursement des frais";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox expensesSelect;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox repText;
        private System.Windows.Forms.TextBox nuiText;
        private System.Windows.Forms.TextBox kmText;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox etpText;
        private System.Windows.Forms.Button createExpenseButton;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox expenseOPCostText;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox expenseOPLabelText;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker expenseOPDate;
        private System.Windows.Forms.ListBox expensesOPList;
        private System.Windows.Forms.Button removeExpenseOPButton;
        private System.Windows.Forms.Button addExpenseOPButton;
        private System.Windows.Forms.Button saveExpenseButton;
        private System.Windows.Forms.Button cancelExpenseButton;

    }
}