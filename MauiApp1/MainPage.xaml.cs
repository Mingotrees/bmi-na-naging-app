using Android.Graphics;

namespace MauiApp1;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private async void OnCalculateBMI(object sender, EventArgs e)
    {
        if (double.TryParse(HeightEntry.Text, out double height) &&
            double.TryParse(WeightEntry.Text, out double weight))
        {
            if (height <= 0 || weight <= 0)
            {
                await DisplayAlertAsync("Error", "Height and weight must be greater than 0", "OK");
                return;
            }

            double heightInMeters = height / 100;
            double bmi = weight / (heightInMeters * heightInMeters);
            
            string category = GetBmiCategory(bmi);
            
            BmiResultLabel.Text = $"BMI: {bmi:F1}";
            BmiCategoryLabel.Text = $"Category: {category}";
            switch (category)
            {
                case "Underweight" :
                    BmiCategoryLabel.TextColor = Colors.Red;
                    BmiImage.Source = ImageSource.FromFile("underweight.gif");
                    break;
                case "Normal weight":
                    BmiCategoryLabel.TextColor = Colors.Green;
                    BmiImage.Source = ImageSource.FromFile("normal.gif");
                    break;
                case "Overweight":
                    BmiCategoryLabel.TextColor = Colors.Red;
                    BmiImage.Source = ImageSource.FromFile("overweight.gif");
                    break;
                case "Obese":
                    BmiCategoryLabel.TextColor = Colors.Purple;
                    BmiImage.Source = ImageSource.FromFile("obese.gif");
                    break;
            } 
        }
        else
        {
            await DisplayAlertAsync("Error", "Please enter valid numbers", "OK");
        }
    }

    private string GetBmiCategory(double bmi)
    {
        return bmi switch
        {
            < 18.5 => "Underweight",
            >= 18.5 and < 25 => "Normal weight",
            >= 25 and < 30 => "Overweight",
            _ => "Obese"
        };
    }
}