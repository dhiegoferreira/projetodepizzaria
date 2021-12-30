namespace projetodepizzaria
{
    public partial class TelaLogin : Form
    {

        //M�TODO CONSTRUTOR
        public TelaLogin()
        {
            InitializeComponent();
            CenterToScreen(); //N�o faz sentido TelaLogin.CenterToScreen pois j� est� sendo construido


        }

        //M�todo para autentica��o
        public void autenticacao()
        {

            if (txtUsuario.Text == "" || txtSenha.Text == "")
            {
                MessageBox.Show("Prenncha os campos corretamente.");
                Console.WriteLine("Preencha os campos corretamente.");
                 
            } else
            {
                //Aqui n�s iremos passar os textos inseridos pelo usu�rio como par�metros
                Console.WriteLine(txtUsuario);
                Console.WriteLine(txtSenha);
                DAOLogin.autenticarFuncionario(txtUsuario.Text, txtSenha.Text);

            }


        }

        private void button3_Click(object sender, EventArgs e)
        {
            //Chamar o m�todo autentica��o
            autenticacao();
            //Abrir tela Principal;
            TelaCadastroCliente telacad = new TelaCadastroCliente();
            telacad.Show();




        }

        //M�todo de m�scara para a senha
        private void txtSenha_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        //A��o do bot�o Sair
        private void btnSair_Click(object sender, EventArgs e)
        {
            Dispose();
        }
    }
}