using System;


namespace MauiApp1
{
    public partial class KrediHesaplama : ContentPage
    {
        public KrediHesaplama()
        {
            InitializeComponent();
        }

        private void Hesapla_Clicked(object sender, EventArgs e)
        {
            double krediTutari = Convert.ToDouble(txtKrediTutari.Text);
            double faizOrani = Convert.ToDouble(txtFaizOrani.Text);
            int vade = Convert.ToInt32(txtVade.Text);

            double KKDF = 0, BSMV = 0;
            
            switch (pickerKrediTuru.SelectedItem.ToString())
            {
                case "�htiya� Kredisi":
                    KKDF = 0.15;
                    BSMV = 0.10;
                    break;
                case "Konut Kredisi":
                    KKDF = 0;
                    BSMV = 0;
                    break;
                case "Ta��t Kredisi":
                    KKDF = 0.15;
                    BSMV = 0.05;
                    break;
                case "Ticari Kredi":
                    KKDF = 0;
                    BSMV = 0.05;
                    break;
            }

            double brutFaiz = ((faizOrani + (faizOrani * BSMV) + (faizOrani * KKDF)) / 100);

            double taksit = ((Math.Pow(1 + brutFaiz, vade) * brutFaiz) / (Math.Pow(1 + brutFaiz, vade) - 1)) * krediTutari;
            double toplam = taksit * vade;
            double toplamFaiz = toplam - krediTutari;

            lblAyl�kTaksit.Text = "Ayl�k Taksit: " + taksit.ToString("C2");
            lblToplamOdeme.Text = "Toplam �deme: " + toplam.ToString("C2");
            lblToplamFaiz.Text = "Toplam Faiz: " + toplamFaiz.ToString("C2");
        }

        private void TxtVade_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtVade.Text))
            {
                int value;
                if (int.TryParse(txtVade.Text, out value))
                {
                    sliderVade.Value = value;
                }
            }
        }

        private void SliderVade_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            txtVade.Text = ((int)e.NewValue).ToString();
        }
    }
}
