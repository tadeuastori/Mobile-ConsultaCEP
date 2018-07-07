using AppConsultaCEP.Servicos;
using AppConsultaCEP.Servicos.Modelo;
using System;
using Xamarin.Forms;

namespace AppConsultaCEP
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            BOTAO.IsEnabled = true;
            CEP.IsEnabled = true;
            RESULTADO.IsVisible = false;

            BOTAO.Clicked += BuscarCEP;
        }

        private void BuscarCEP(object sender, EventArgs e)
        {
            string cep = (!string.IsNullOrEmpty(CEP.Text) ? CEP.Text.Trim() : string.Empty);
            string espaco = Environment.NewLine;
            string resultado = string.Empty;

            if (IsValidCEP(cep))
            {
                try
                {
                    Endereco end = ViaCEPServico.BuscarEnderecoViaCEP(cep);

                    if (end == null)
                    {
                        DisplayAlert("Erro", $"CEP {cep} não encontrado!", "OK");
                    }
                    else
                    {
                        resultado += $@" CEP: {end.cep}{espaco}";
                        resultado += $@" {end.logradouro}{espaco}";
                        resultado += $@" Bairro: {end.bairro}{espaco}";
                        resultado += $@" Cidade: {end.localidade}{espaco}";
                        resultado += $@" UF: {end.uf}{espaco}";
                        resultado += $@" IBGE: {end.ibge}";

                        RESULTADO.IsVisible = true;
                        RESULTADO.Text = resultado;
                        CEP.Text = string.Empty;
                    }

                }
                catch (Exception ex)
                {
                    DisplayAlert("Erro", ex.Message, "OK");
                }
            }
        }

        private bool IsValidCEP(string cep)
        {
            if (cep.Length < 8)
            {
                DisplayAlert("ERRO", "O CEP deve ter 8 dígitos.", "OK");
                return false;
            }

            return true;
        }
    }
}
