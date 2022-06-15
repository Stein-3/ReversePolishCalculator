namespace RPNCalculator.MAUI;

public partial class MainPage : ContentPage
{
	int count = 0;

	public MainPage()
	{
        BindingContext = new RPNViewModel();
		InitializeComponent();
	}
}

