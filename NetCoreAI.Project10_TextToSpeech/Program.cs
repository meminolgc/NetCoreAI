using System.Speech.Synthesis;

class Program
{
	private static async Task Main(string[] args)
	{
		SpeechSynthesizer speechSynthesizer = new SpeechSynthesizer();
		speechSynthesizer.Volume = 100;
		speechSynthesizer.Rate = -1;

		Console.Write("Metni giriniz: ");
		string input;
		input = Console.ReadLine();

		if(!string.IsNullOrEmpty(input))
		{
			speechSynthesizer.Speak(input);
		}

		Console.ReadLine();
	}
}