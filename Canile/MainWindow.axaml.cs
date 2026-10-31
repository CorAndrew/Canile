using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Metadata;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.IO;

namespace Canile
{
    public partial class MainWindow : Window
    {
        private List<Cane> listaCani;
        private string percorsoFile = "cani.json";
        public MainWindow()
        {
            InitializeComponent();
            listaCani = new List<Cane>();

            CaricaDatiDaJson();
            AggiornaGrafica();
            CmbRazza.ItemsSource = Enum.GetValues(typeof(razza));
        }

        public void CmbRazza_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //
        }

        public void BtnRegistra_Click(object sender, RoutedEventArgs e)
        {
            string nome = TxtNome.Text;
            if (string.IsNullOrWhiteSpace(nome))
            {
                TxtRisultato.Text = "Il nome del cane non può essere vuoto.";
                return;
            }

            int eta;
            if (!int.TryParse(TxtEta.Text, out eta) || eta < 0 || eta > 25)
            {
                TxtRisultato.Text = "Inserisci un'età valida";
                return;
            }

            if (CalDataIngresso.SelectedDate == null)
            {
                TxtRisultato.Text = "Seleziona una data di ingresso valida";
                return;
            }
            DateTime dataIngresso = CalDataIngresso.SelectedDate.Value;

            razza razzaScelta = (razza)CmbRazza.SelectedItem;
            string note = TxtNote.Text;

            Cane nuovoCane = new Cane(nome, eta, razzaScelta, dataIngresso, note);

            // Aggiunta alla lista e salvataggio su file JSON
            listaCani.Add(nuovoCane);
            SalvaDatiSuJson();

            AggiornaGrafica();

            TxtRisultato.Text = "Cane registrato e salvato nel file";
            PulisciCampi();
        }

        public void BtnAdotta_Click(object sender, RoutedEventArgs e)
        {
            if (ListaCani.SelectedItem == null)
            {
                TxtRisultato.Text = "Seleziona un cane dalla lista da fare adottare";
                return;
            }

            Cane caneSelezionato = (Cane)ListaCani.SelectedItem;

            // Cambia lo stato a adottato e lo rimuove dalla lista attiva del canile
            caneSelezionato.stato = true;
            listaCani.Remove(caneSelezionato);

            SalvaDatiSuJson();

            AggiornaGrafica();

            TxtRisultato.Text = "Il cane " + caneSelezionato.nome + " è stato adottato con successo";
        }

        private void SalvaDatiSuJson()
        {
            try
            {
                string jsonString = JsonSerializer.Serialize(listaCani);
                File.WriteAllText(percorsoFile, jsonString);
            }
            catch (Exception ex)
            {
                TxtRisultato.Text = "Errore durante il salvataggio: " + ex.Message;
            }
        }

        private void CaricaDatiDaJson()
        {
            try
            {
                if (File.Exists(percorsoFile))
                {
                    string jsonString = File.ReadAllText(percorsoFile);
                    List<Cane> caniCaricati = JsonSerializer.Deserialize<List<Cane>>(jsonString);

                    if (caniCaricati != null)
                    {
                        listaCani = caniCaricati;
                    }
                }
            }
            catch (Exception ex)
            {
                TxtRisultato.Text = "Errore durante il caricamento: " + ex.Message;
            }
        }

        private void AggiornaGrafica()
        {
            ListaCani.ItemsSource = null;
            ListaCani.ItemsSource = listaCani;
        }

        private void PulisciCampi()
        {
            TxtNome.Text = "";
            TxtEta.Text = "";
            TxtNote.Text = "";
            CalDataIngresso.SelectedDate = null;
        }
    }
}