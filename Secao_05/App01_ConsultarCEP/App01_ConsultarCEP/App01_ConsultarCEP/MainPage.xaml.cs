using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using App01_ConsultarCEP.Servico;
using App01_ConsultarCEP.Servico.Modelo;

namespace App01_ConsultarCEP
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();

            btnConsultarCEP.Clicked += BuscarCEP;
		}

        private void BuscarCEP(object sender, EventArgs args)
        {
            lblResultado.Text = string.Empty;
            string cep = txbCep.Text.Trim();

            if (isValidCEP(cep))
            {
                try
                {
                    //TODO - lógica do programa
                    Endereco end = ViaCEPServico.BuscarEnderecoViaCEP(cep);

                    if(end != null)
                        lblResultado.Text = string.Format("Endereço: {0}, {1}, {2}", end.localidade, end.uf, end.logradouro);
                    else
                        DisplayAlert("ERRO", String.Format("Endereço não encontrado CEP: {0}.", cep), "OK");

                }
                catch(Exception ex)
                {
                    DisplayAlert("ERRO CRÍTICO", ex.Message, "OK");
                }
            }
        }

        private bool isValidCEP(string cep)
        {
            bool valido = true;

            int tryCEP = 0;
            if (!int.TryParse(cep, out tryCEP))
            {
                DisplayAlert("CEP Inválido", "CEP de conter somente números.", "OK");
                valido = false;
            }
            else if (cep.Length != 8)
            {
                DisplayAlert("CEP Inválido", "CEP de conter 8 caracteres.", "OK");
                valido = false;
            }             

            return valido;
        }
    }
}
