using System.Device.Gpio;
using System.Net.Http.Json;
using System.Threading.Tasks;

class Program
{
    private static readonly int PinLed = 23;
    private static readonly int PinBouton = 24;
    private const string Alert = "ALERT 🚨";
    private const string Ready = "READY ✅";

    // Variable pour stocker l'heure du dernier événement
    private static DateTime lastEventTime = DateTime.MinValue;
    private static readonly TimeSpan debounceDelay = TimeSpan.FromMilliseconds(200); // Délai de debounce

    private static GpioController controller;

    static async Task Main(string[] args)
    {
        controller = new GpioController();
        controller.OpenPin(PinBouton, PinMode.Input);
        controller.OpenPin(PinLed, PinMode.Output);
        controller.Write(PinLed, PinValue.Low); // Éteindre la LED au démarrage

        Console.WriteLine(
            $"Initial status ({DateTime.Now}): {(controller.Read(PinBouton) == PinValue.High ? Alert : Ready)}");

        controller.RegisterCallbackForPinValueChangedEvent(
            PinBouton,
            PinEventTypes.Falling | PinEventTypes.Rising,
            OnPinEvent);

        await Task.Delay(Timeout.Infinite);
    }

    private static void OnPinEvent(object sender, PinValueChangedEventArgs args)
    {
        // Vérifier si le délai de debounce est respecté
        var currentTime = DateTime.Now;
        if (currentTime - lastEventTime < debounceDelay)
        {
            // Ignorer l'événement si trop proche du précédent
            return;
        }

        // Mettre à jour l'heure du dernier événement
        lastEventTime = currentTime;

        if (args.ChangeType == PinEventTypes.Falling)
        {
            controller.Write(PinLed, PinValue.High);
            Console.WriteLine($"({DateTime.Now}) {Alert}: Led on");

            SendAlertAsync().Wait(); // Appel API ici
        }
        else if (args.ChangeType == PinEventTypes.Rising)
        {
            controller.Write(PinLed, PinValue.Low);
            Console.WriteLine($"({DateTime.Now}) {Ready}: Led off");
        }
    }

    private static async Task SendAlertAsync()
    {
        using var client = new HttpClient();

        var url = "http://172.16.144.22:5000/api/chatapi/send"; // l'URL Blazor Server

        var data = new
        {
            user = "Sonnette",
            text = "Bouton appuyé depuis la Raspberry Pi"
        };

        try
        {
            // Utilisation de PostAsJsonAsync pour envoyer les données JSON
            var response = await client.PostAsJsonAsync(url, data);
            response.EnsureSuccessStatusCode();
            Console.WriteLine("Notification envoyée à l’API.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Erreur en envoyant la notification : " + ex.Message);
        }
    }
}
