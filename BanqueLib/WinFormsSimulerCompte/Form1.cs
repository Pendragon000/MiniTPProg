using BanqueLib;
namespace WinFormsSimulerCompte
{
    public partial class FromIsaakFortin : Form
    {
        private readonly Compte _compte;
        public FromIsaakFortin(Compte compte)
        {
            _compte = compte;
            InitializeComponent();
            R�cup�rer();
        }

        public void R�cup�rer()
        {
            textBoxNumero.Text = _compte.Num�ro.ToString();
            textBoxDetenteur.Text = _compte.D�tenteur;
            textBoxSolde.Text = _compte.Solde.ToString();
        }
    }
}
