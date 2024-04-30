using BanqueLib;
namespace WinFormsSimulerCompte
{
    partial class FromIsaakFortin
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            groupBoxDonneCompte = new GroupBox();
            checkBoxGele = new CheckBox();
            textBoxSolde = new TextBox();
            textBoxDetenteur = new TextBox();
            textBoxNumero = new TextBox();
            labelSolde = new Label();
            labelDetenteur = new Label();
            labelNumero = new Label();
            buttonDeposer = new Button();
            textBoxLog = new TextBox();
            groupBoxMontant = new GroupBox();
            radioButton100a1000 = new RadioButton();
            radioButton10a100 = new RadioButton();
            radioButton1a10 = new RadioButton();
            radioButton0a1 = new RadioButton();
            buttonRandom = new Button();
            buttonRestMontant = new Button();
            numericUpDownMontant = new NumericUpDown();
            buttonRetirer = new Button();
            buttonVider = new Button();
            buttonReset = new Button();
            groupBoxDonneCompte.SuspendLayout();
            groupBoxMontant.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownMontant).BeginInit();
            SuspendLayout();
            // 
            // groupBoxDonneCompte
            // 
            groupBoxDonneCompte.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            groupBoxDonneCompte.Controls.Add(checkBoxGele);
            groupBoxDonneCompte.Controls.Add(textBoxSolde);
            groupBoxDonneCompte.Controls.Add(textBoxDetenteur);
            groupBoxDonneCompte.Controls.Add(textBoxNumero);
            groupBoxDonneCompte.Controls.Add(labelSolde);
            groupBoxDonneCompte.Controls.Add(labelDetenteur);
            groupBoxDonneCompte.Controls.Add(labelNumero);
            groupBoxDonneCompte.Location = new Point(12, 12);
            groupBoxDonneCompte.Name = "groupBoxDonneCompte";
            groupBoxDonneCompte.Size = new Size(516, 254);
            groupBoxDonneCompte.TabIndex = 0;
            groupBoxDonneCompte.TabStop = false;
            groupBoxDonneCompte.Text = "Données du compte";
            // 
            // checkBoxGele
            // 
            checkBoxGele.AutoSize = true;
            checkBoxGele.Location = new Point(399, 59);
            checkBoxGele.Name = "checkBoxGele";
            checkBoxGele.Size = new Size(94, 36);
            checkBoxGele.TabIndex = 6;
            checkBoxGele.Text = "Gelé";
            checkBoxGele.UseVisualStyleBackColor = true;
            checkBoxGele.CheckedChanged += checkBoxGele_CheckedChanged;
            checkBoxGele.Click += checkBoxGele_Click;
            // 
            // textBoxSolde
            // 
            textBoxSolde.Location = new Point(149, 188);
            textBoxSolde.Name = "textBoxSolde";
            textBoxSolde.ReadOnly = true;
            textBoxSolde.Size = new Size(344, 39);
            textBoxSolde.TabIndex = 5;
            textBoxSolde.TextChanged += textBoxSolde_TextChanged;
            // 
            // textBoxDetenteur
            // 
            textBoxDetenteur.Location = new Point(149, 127);
            textBoxDetenteur.Name = "textBoxDetenteur";
            textBoxDetenteur.Size = new Size(344, 39);
            textBoxDetenteur.TabIndex = 4;
            textBoxDetenteur.KeyPress += textBoxDetenteur_KeyPress;
            textBoxDetenteur.Leave += textBoxDetenteur_Leave;
            // 
            // textBoxNumero
            // 
            textBoxNumero.Location = new Point(149, 56);
            textBoxNumero.Name = "textBoxNumero";
            textBoxNumero.ReadOnly = true;
            textBoxNumero.Size = new Size(210, 39);
            textBoxNumero.TabIndex = 3;
            // 
            // labelSolde
            // 
            labelSolde.AutoSize = true;
            labelSolde.Location = new Point(58, 195);
            labelSolde.Name = "labelSolde";
            labelSolde.Size = new Size(79, 32);
            labelSolde.TabIndex = 2;
            labelSolde.Text = "Solde:";
            // 
            // labelDetenteur
            // 
            labelDetenteur.AutoSize = true;
            labelDetenteur.Location = new Point(10, 127);
            labelDetenteur.Name = "labelDetenteur";
            labelDetenteur.Size = new Size(127, 32);
            labelDetenteur.TabIndex = 1;
            labelDetenteur.Text = "Détenteur:";
            // 
            // labelNumero
            // 
            labelNumero.AutoSize = true;
            labelNumero.Location = new Point(30, 56);
            labelNumero.Name = "labelNumero";
            labelNumero.Size = new Size(107, 32);
            labelNumero.TabIndex = 0;
            labelNumero.Text = "Numéro:";
            // 
            // buttonDeposer
            // 
            buttonDeposer.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            buttonDeposer.Location = new Point(58, 337);
            buttonDeposer.Name = "buttonDeposer";
            buttonDeposer.Size = new Size(124, 44);
            buttonDeposer.TabIndex = 1;
            buttonDeposer.Text = "Déposer";
            buttonDeposer.UseVisualStyleBackColor = true;
            buttonDeposer.Click += buttonDeposer_Click;
            // 
            // textBoxLog
            // 
            textBoxLog.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            textBoxLog.Location = new Point(22, 399);
            textBoxLog.Multiline = true;
            textBoxLog.Name = "textBoxLog";
            textBoxLog.ReadOnly = true;
            textBoxLog.ScrollBars = ScrollBars.Vertical;
            textBoxLog.Size = new Size(935, 179);
            textBoxLog.TabIndex = 2;
            // 
            // groupBoxMontant
            // 
            groupBoxMontant.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            groupBoxMontant.Controls.Add(radioButton100a1000);
            groupBoxMontant.Controls.Add(radioButton10a100);
            groupBoxMontant.Controls.Add(radioButton1a10);
            groupBoxMontant.Controls.Add(radioButton0a1);
            groupBoxMontant.Controls.Add(buttonRandom);
            groupBoxMontant.Controls.Add(buttonRestMontant);
            groupBoxMontant.Controls.Add(numericUpDownMontant);
            groupBoxMontant.Location = new Point(553, 12);
            groupBoxMontant.Name = "groupBoxMontant";
            groupBoxMontant.Size = new Size(409, 254);
            groupBoxMontant.TabIndex = 3;
            groupBoxMontant.TabStop = false;
            groupBoxMontant.Text = "Montant";
            // 
            // radioButton100a1000
            // 
            radioButton100a1000.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            radioButton100a1000.AutoSize = true;
            radioButton100a1000.Location = new Point(216, 182);
            radioButton100a1000.Name = "radioButton100a1000";
            radioButton100a1000.Size = new Size(162, 36);
            radioButton100a1000.TabIndex = 6;
            radioButton100a1000.TabStop = true;
            radioButton100a1000.Text = "100 à 1000";
            radioButton100a1000.UseVisualStyleBackColor = true;
            // 
            // radioButton10a100
            // 
            radioButton10a100.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            radioButton10a100.AutoSize = true;
            radioButton10a100.Location = new Point(216, 136);
            radioButton10a100.Name = "radioButton10a100";
            radioButton10a100.Size = new Size(136, 36);
            radioButton10a100.TabIndex = 5;
            radioButton10a100.TabStop = true;
            radioButton10a100.Text = "10 à 100";
            radioButton10a100.UseVisualStyleBackColor = true;
            // 
            // radioButton1a10
            // 
            radioButton1a10.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            radioButton1a10.AutoSize = true;
            radioButton1a10.Location = new Point(216, 94);
            radioButton1a10.Name = "radioButton1a10";
            radioButton1a10.Size = new Size(110, 36);
            radioButton1a10.TabIndex = 4;
            radioButton1a10.TabStop = true;
            radioButton1a10.Text = "1 à 10";
            radioButton1a10.UseVisualStyleBackColor = true;
            // 
            // radioButton0a1
            // 
            radioButton0a1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            radioButton0a1.AutoSize = true;
            radioButton0a1.Location = new Point(216, 52);
            radioButton0a1.Name = "radioButton0a1";
            radioButton0a1.Size = new Size(97, 36);
            radioButton0a1.TabIndex = 3;
            radioButton0a1.TabStop = true;
            radioButton0a1.Text = "0 à 1";
            radioButton0a1.UseVisualStyleBackColor = true;
            // 
            // buttonRandom
            // 
            buttonRandom.Anchor = AnchorStyles.None;
            buttonRandom.Location = new Point(33, 188);
            buttonRandom.Name = "buttonRandom";
            buttonRandom.Size = new Size(133, 46);
            buttonRandom.TabIndex = 2;
            buttonRandom.Text = "Random";
            buttonRandom.UseVisualStyleBackColor = true;
            buttonRandom.Click += buttonRandom_Click;
            // 
            // buttonRestMontant
            // 
            buttonRestMontant.Anchor = AnchorStyles.None;
            buttonRestMontant.Location = new Point(33, 123);
            buttonRestMontant.Name = "buttonRestMontant";
            buttonRestMontant.Size = new Size(133, 46);
            buttonRestMontant.TabIndex = 1;
            buttonRestMontant.Text = "0.01";
            buttonRestMontant.UseVisualStyleBackColor = true;
            buttonRestMontant.Click += buttonRestMontant_Click;
            // 
            // numericUpDownMontant
            // 
            numericUpDownMontant.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            numericUpDownMontant.DecimalPlaces = 2;
            numericUpDownMontant.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
            numericUpDownMontant.Location = new Point(33, 54);
            numericUpDownMontant.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            numericUpDownMontant.Minimum = new decimal(new int[] { 1, 0, 0, 131072 });
            numericUpDownMontant.Name = "numericUpDownMontant";
            numericUpDownMontant.ReadOnly = true;
            numericUpDownMontant.RightToLeft = RightToLeft.No;
            numericUpDownMontant.Size = new Size(133, 39);
            numericUpDownMontant.TabIndex = 0;
            numericUpDownMontant.Value = new decimal(new int[] { 1, 0, 0, 131072 });
            numericUpDownMontant.ValueChanged += numericUpDownMontant_ValueChanged;
            // 
            // buttonRetirer
            // 
            buttonRetirer.Anchor = AnchorStyles.None;
            buttonRetirer.Location = new Point(310, 337);
            buttonRetirer.Name = "buttonRetirer";
            buttonRetirer.Size = new Size(124, 44);
            buttonRetirer.TabIndex = 4;
            buttonRetirer.Text = "Retirer";
            buttonRetirer.UseVisualStyleBackColor = true;
            buttonRetirer.Click += buttonRetirer_Click;
            // 
            // buttonVider
            // 
            buttonVider.Anchor = AnchorStyles.None;
            buttonVider.Location = new Point(586, 337);
            buttonVider.Name = "buttonVider";
            buttonVider.Size = new Size(124, 44);
            buttonVider.TabIndex = 5;
            buttonVider.Text = "Vider";
            buttonVider.UseVisualStyleBackColor = true;
            buttonVider.Click += buttonVider_Click;
            // 
            // buttonReset
            // 
            buttonReset.Anchor = AnchorStyles.None;
            buttonReset.Location = new Point(829, 337);
            buttonReset.Name = "buttonReset";
            buttonReset.Size = new Size(124, 44);
            buttonReset.TabIndex = 6;
            buttonReset.Text = "Reset";
            buttonReset.UseVisualStyleBackColor = true;
            buttonReset.Click += buttonReset_Click;
            // 
            // FromIsaakFortin
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(977, 590);
            Controls.Add(buttonReset);
            Controls.Add(buttonVider);
            Controls.Add(buttonRetirer);
            Controls.Add(groupBoxMontant);
            Controls.Add(textBoxLog);
            Controls.Add(buttonDeposer);
            Controls.Add(groupBoxDonneCompte);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "FromIsaakFortin";
            Text = "Simulateur de Compte --- Isaak Fortin";
            groupBoxDonneCompte.ResumeLayout(false);
            groupBoxDonneCompte.PerformLayout();
            groupBoxMontant.ResumeLayout(false);
            groupBoxMontant.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownMontant).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private GroupBox groupBoxDonneCompte;
        private Label labelNumero;
        private Label labelSolde;
        private Label labelDetenteur;
        private Button buttonDeposer;
        private TextBox textBoxLog;
        private CheckBox checkBoxGele;
        private TextBox textBoxSolde;
        private TextBox textBoxDetenteur;
        private TextBox textBoxNumero;
        private GroupBox groupBoxMontant;
        private NumericUpDown numericUpDownMontant;
        private RadioButton radioButton0a1;
        private Button buttonRandom;
        private Button buttonRestMontant;
        private RadioButton radioButton100a1000;
        private RadioButton radioButton10a100;
        private RadioButton radioButton1a10;
        private Button buttonRetirer;
        private Button buttonVider;
        private Button buttonReset;
    }
}
