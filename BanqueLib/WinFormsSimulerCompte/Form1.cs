using BanqueLib;
using System.Security.Cryptography.X509Certificates;
namespace WinFormsSimulerCompte
{
    public partial class FromIsaakFortin : Form
    {
        private int PlafontRand;
        private int PlancherRand;
        private readonly Compte _compte;
        private Compte? _compteReset;
        public FromIsaakFortin(ref Compte compte)
        {
            _compte = compte;
            InitializeComponent();
            if (_compte.Solde > 0)
            {
                buttonDeposer.Enabled = true;
                buttonRetirer.Enabled = true;
                buttonVider.Enabled = true;
                buttonReset.Enabled = false;
            }
            R�cup�rer();
            
        }

        public void R�cup�rer()
        {
            textBoxNumero.Text = _compte.Num�ro.ToString();
            textBoxDetenteur.Text = _compte.D�tenteur;
            textBoxSolde.Text = _compte.Solde.ToString("C");
            checkBoxGele.Checked = _compte.EstGel�;
        }
        private void checkBoxGele_Click(object sender, EventArgs e)
        {
            if (checkBoxGele.Checked)
            {
                _compte.Geler();
                textBoxLog.AppendText("[IF]Compte Geler" + Environment.NewLine);
            }
            else
            {
                _compte.D�geler();
                textBoxLog.AppendText("[IF]Compte D�geler" + Environment.NewLine);
            }
            R�cup�rer();
        }

        private void buttonDeposer_Click(object sender, EventArgs e)
        {
            _compte.D�poser(numericUpDownMontant.Value);
            textBoxLog.AppendText($"[IF]D�pot de {numericUpDownMontant.Value}$ " + Environment.NewLine);
            R�cup�rer();
        }

        private void buttonRetirer_Click(object sender, EventArgs e)
        {
            _compte.Retirer(numericUpDownMontant.Value);
            textBoxLog.AppendText($"[IF]Retrait de {numericUpDownMontant.Value}$ " + Environment.NewLine);
            R�cup�rer();
        }

        private void buttonVider_Click(object sender, EventArgs e)
        {
            textBoxLog.AppendText($"[IF]Retrait complet de {_compte.Solde}$ " + Environment.NewLine);
            _compte.Vider();
            buttonDeposer.Enabled = true;
            buttonRetirer.Enabled = true;
            buttonVider.Enabled = true;
            buttonReset.Enabled = true;
            R�cup�rer();
        }
        
        private void buttonReset_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Voulez-vous reset le compte?", "Confirmation",
                MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                textBoxLog.Text = "";
                numericUpDownMontant.Value = 0.01m;
                radioButton0a1.Checked = true;
                _compte.Vider();
                _compte.D�poser(Convert.ToDecimal(Random.Shared.Next(0, 100) + Math.Round(Random.Shared.NextDouble(), 2)));
                _compte.SetD�tenteur("Isaak Fortin");
            }
            R�cup�rer();
        }

        private void checkBoxGele_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxGele.Checked)
            {
                buttonDeposer.Enabled = false;
                buttonRetirer.Enabled = false;
                buttonVider.Enabled = false;
                buttonReset.Enabled = false;
            }
            else
            {
                buttonDeposer.Enabled = true;
                buttonRetirer.Enabled = true;
                buttonVider.Enabled = true;
                buttonReset.Enabled = true;
            }
        }

        private void numericUpDownMontant_ValueChanged(object sender, EventArgs e)
        {
            buttonRetirer.Enabled = _compte.PeutRetirer(numericUpDownMontant.Value);
        }

        private void textBoxSolde_TextChanged(object sender, EventArgs e)
        {
            buttonVider.Enabled = _compte.Solde != 0;
            buttonRetirer.Enabled = _compte.Solde != 0;
        }

        private void buttonRestMontant_Click(object sender, EventArgs e)
        {
            numericUpDownMontant.Value = 0.01m;
        }

        public void SetRandom()
        {
            if (radioButton0a1.Checked == true)
            {
                PlafontRand = 1;
                PlancherRand = 0;
            }
            if (radioButton1a10.Checked == true)
            {
                PlafontRand = 10;
                PlancherRand = 0;
            }
            if (radioButton10a100.Checked == true)
            {
                PlafontRand = 100;
                PlancherRand = 10;
            }
            if (radioButton100a1000.Checked == true)
            {
                PlafontRand = 1000;
                PlancherRand = 100;
            }
        }

        private void buttonRandom_Click(object sender, EventArgs e)
        {
            SetRandom();
            if (radioButton0a1.Checked == true)
            {
                numericUpDownMontant.Value = Convert.ToDecimal(Math.Round(Random.Shared.NextDouble(), 2));
            }
            if (radioButton1a10.Checked == true)
            {
                numericUpDownMontant.Value = Convert.ToDecimal(Random.Shared.Next(PlancherRand, PlafontRand) + Math.Round(Random.Shared.NextDouble(), 2));
            }
            if (radioButton10a100.Checked == true)
            {
                numericUpDownMontant.Value = Convert.ToDecimal(Random.Shared.Next(PlancherRand, PlafontRand) + Math.Round(Random.Shared.NextDouble(), 2));
            }
            if (radioButton100a1000.Checked == true)
            {
                numericUpDownMontant.Value = Convert.ToDecimal(Random.Shared.Next(PlancherRand, PlafontRand) + Math.Round(Random.Shared.NextDouble(), 2));
            }
        }

        private void textBoxDetenteur_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Keys)e.KeyChar == Keys.Return)
            {
                try
                {
                    _compte.SetD�tenteur(textBoxDetenteur.Text);
                    MessageBox.Show("[IF] Le nom du d�tenteur � �t� modifier");
                    textBoxLog.AppendText($"[IF]Le nom du d�tenteur a �t� modifi� par {_compte.D�tenteur}$ " + Environment.NewLine);
                }
                catch (Exception)
                {
                    MessageBox.Show("[IF] Syntaxe Incorrecte");
                }
                R�cup�rer();
            }
            if ((Keys)e.KeyChar == Keys.Escape)
            {
                R�cup�rer();
            }
        }

        private void textBoxDetenteur_Leave(object sender, EventArgs e)
        {
            R�cup�rer();
        }
    }
}
