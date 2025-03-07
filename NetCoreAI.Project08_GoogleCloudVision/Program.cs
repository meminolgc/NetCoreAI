﻿using Google.Cloud.Vision.V1;

class Program
{
	static void Main(string[] args)
	{
		Console.Write("Resim Yolunu Giriniz: ");
		string imagePath = Console.ReadLine().Trim();
		Console.WriteLine();

		string credentialPath = @"json dosyasının yolunu buraya ekle";
		Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", credentialPath);

		try
		{
			var client = ImageAnnotatorClient.Create();

			var image = Image.FromFile(imagePath);
			var response = client.DetectText(image);
			Console.WriteLine("Resimdeki Metin:");
			Console.WriteLine();

			foreach (var annotation in response)
			{
				if (!string.IsNullOrEmpty(annotation.Description))
				{
					Console.WriteLine(annotation.Description);
				}
			}

		}
		catch (Exception ex)
		{

			Console.WriteLine($"Bir hata oluştu: {ex.Message}");
		}
	}
}
