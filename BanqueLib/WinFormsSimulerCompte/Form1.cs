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
            Récupérer();
            
        }

        public void Récupérer()
        {
            textBoxNumero.Text = _compte.Numéro.ToString();
            textBoxDetenteur.Text = _compte.Détenteur;
            textBoxSolde.Text = _compte.Solde.ToString("C");
            checkBoxGele.Checked = _compte.EstGelé;
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
                _compte.Dégeler();
                textBoxLog.AppendText("[IF]Compte Dégeler" + Environment.NewLine);
            }
            Récupérer();
        }

        private void buttonDeposer_Click(object sender, EventArgs e)
        {
            _compte.Déposer(numericUpDownMontant.Value);
            textBoxLog.AppendText($"[IF]Dépot de {numericUpDownMontant.Value}$ " + Environment.NewLine);
            Récupérer();
        }

        private void buttonRetirer_Click(object sender, EventArgs e)
        {
            _compte.Retirer(numericUpDownMontant.Value);
            textBoxLog.AppendText($"[IF]Retrait de {numericUpDownMontant.Value}$ " + Environment.NewLine);
            Récupérer();
        }

        private void buttonVider_Click(object sender, EventArgs e)
        {
            textBoxLog.AppendText($"[IF]Retrait complet de {_compte.Solde}$ " + Environment.NewLine);
            _compte.Vider();
            buttonDeposer.Enabled = true;
            buttonRetirer.Enabled = true;
            buttonVider.Enabled = true;
            buttonReset.Enabled = true;
            Récupérer();
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
                _compte.Déposer(Convert.ToDecimal(Random.Shared.Next(0, 100) + Math.Round(Random.Shared.NextDouble(), 2)));
                _compte.SetDétenteur("Isaak Fortin");
            }
            Récupérer();
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
                    _compte.SetDétenteur(textBoxDetenteur.Text);
                    MessageBox.Show("[IF] Le nom du détenteur à été modifier");
                    textBoxLog.AppendText($"[IF]Le nom du détenteur a été modifié par {_compte.Détenteur}$ " + Environment.NewLine);
                }
                catch (Exception)
                {
                    MessageBox.Show("[IF] Syntaxe Incorrecte");
                }
                Récupérer();
            }
            if ((Keys)e.KeyChar == Keys.Escape)
            {
                Récupérer();
            }
        }

        private void textBoxDetenteur_Leave(object sender, EventArgs e)
        {
            Récupérer();
        }
    }
}
