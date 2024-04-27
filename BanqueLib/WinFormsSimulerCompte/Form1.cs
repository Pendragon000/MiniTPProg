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
            Récupérer();
        }

        public void Récupérer()
        {
            textBoxNumero.Text = _compte.Numéro.ToString();
            textBoxDetenteur.Text = _compte.Détenteur;
            textBoxSolde.Text = _compte.Solde.ToString();
        }
    }
}
