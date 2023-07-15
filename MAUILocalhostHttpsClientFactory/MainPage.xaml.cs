namespace MAUILocalhostHttpsClientFactory;

public partial class MainPage : ContentPage
{
    private readonly IHttpClientFactory _httpClientFactory;

    public MainPage(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
        InitializeComponent();
    }

    private async void OnGetDataFromApiClicked(object sender, EventArgs e)
    {
        var httpClient = _httpClientFactory.CreateClient("maui-to-https-localhost");
        var response = await httpClient.GetAsync("/weatherforecast");
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            label.Text = content;
        }
    }
}


