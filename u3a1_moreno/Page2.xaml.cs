namespace u3a1_moreno;

using Microsoft.Maui.Devices.Sensors;
using Microsoft.Maui.Controls;
using Plugin.Maui.Audio;
using System;

public partial class Page2 : ContentPage
{
	private string categoryName;
	private IAudioPlayer currentPlayer;
	private bool isSoundPlaying = false; // Variable si se esta reproduciendo un sonido actualmente

	public Page2(string categoryName)
	{
		InitializeComponent();
		this.categoryName = categoryName;
		TitleLabel.Text = categoryName;
		InitializePage();
	}

	private void InitializePage() {
		StartAccelerometer();
		StartGyroscope();
		ResolutionImages();
	}

	private void ResolutionImages()
	{
		// Imagenes de la categoria F1 y Mario Bros
		Imagen1.Source = categoryName == "F1" ? "Images/mercedes.png" : "Images/luigi.png";
		Imagen2.Source = categoryName == "F1" ? "Images/ferrari.png" : "Images/mario.png";
	}

	// Arranque del acelerometro
	private void StartAccelerometer()
	{
		if (Accelerometer.IsSupported)
		{
			if (Accelerometer.IsMonitoring)
			{
				Accelerometer.Default.Stop();
				Accelerometer.ReadingChanged -= Accelerometer_ReadingChanged;
			}
			Accelerometer.Start(SensorSpeed.UI);
			Accelerometer.ReadingChanged += Accelerometer_ReadingChanged;
		}
		else
		{
			AccelValues.Text = "Acelerómetro no soportado.";
		}
	}

	// Leer valores del Acelerometro y dependiendo del eje X sonara un sonido u otro
	private void Accelerometer_ReadingChanged(object sender, AccelerometerChangedEventArgs e)
	{
		Task.Run(async () =>
		{
			var x = e.Reading.Acceleration.X;
			var y = e.Reading.Acceleration.Y;
			AccelValues.Dispatcher.Dispatch(() => AccelValues.Text = $"X: {x:F2} Y: {y:F2}"); // Actualiza en el hilo UI

			string audioLeft = categoryName == "F1" ? "Mercedes.mp3" : "Luigi.mp3";
			string audioRight = categoryName == "F1" ? "Ferrari.mp3" : "Mario.mp3";

			if (x < -1.5 && !isSoundPlaying)
			{
				await PlaySound(audioLeft);
			}
			else if (x > 1.5 && !isSoundPlaying)
			{
				await PlaySound(audioRight);
			}
		});
	}

	// Arranque del giroscopio
	private void StartGyroscope()
	{
		if (Gyroscope.IsSupported)
		{
			if (Gyroscope.IsMonitoring)
			{
				Gyroscope.Stop();
				Gyroscope.ReadingChanged -= Gyroscope_ReadingChanged;
			}
			Gyroscope.Start(SensorSpeed.UI);
			Gyroscope.ReadingChanged += Gyroscope_ReadingChanged;
		}
		else
		{
			GyroValues.Text = "Giroscopio no soportado.";
		}
	}

	// Método para leer los datos del giroscopio
	private void Gyroscope_ReadingChanged(object sender, GyroscopeChangedEventArgs e)
	{
		Task.Run(() =>
		{
			var x = e.Reading.AngularVelocity.Y;

			GyroValues.Dispatcher.Dispatch(() => GyroValues.Text = $"X: {x:F2}"); // Actualiza en el hilo UI

			if (isSoundPlaying) return;

			Color leftColor = Colors.White;
			Color rightColor = Colors.White;

			if (x > 1.6)
			{
				rightColor = Colors.Red; // Cambia el color de BrdRight
			}
			else if (x < -1.6)
			{
				leftColor = Colors.Green; // Cambia el color de BrdLeft
			}

			// Actualiza los colores directamente
			BrdLeft.Dispatcher.Dispatch(() => BrdLeft.Background = new SolidColorBrush(leftColor));
			BrdRight.Dispatcher.Dispatch(() => BrdRight.Background = new SolidColorBrush(rightColor));
		});
	}

	private async Task PlaySound(string fileName)
	{
		if (currentPlayer != null && currentPlayer.IsPlaying)
		{
			currentPlayer.Stop();
			currentPlayer.Dispose();
		}

		isSoundPlaying = true;

		try
		{
			var fileStream = await FileSystem.OpenAppPackageFileAsync(fileName);
			currentPlayer = AudioManager.Current.CreatePlayer(fileStream);
			currentPlayer.Play();

			while (currentPlayer.IsPlaying)
			{
				await Task.Delay(50); // Reducir el intervalo mejora la reactividad
			}
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Error al reproducir audio: {ex.Message}");
		}
		finally
		{
			isSoundPlaying = false;
		}
	}

	protected override void OnAppearing()
	{
		base.OnAppearing();
		// No es necesario llamar a estos métodos aquí, ya se llaman en InitializePage
	}

	protected override void OnDisappearing()
	{
		base.OnDisappearing();
		// Detener y desuscribir los sensores cuando la página desaparezca
		if (Accelerometer.IsMonitoring)
		{
			Accelerometer.Stop();
			Accelerometer.ReadingChanged -= Accelerometer_ReadingChanged;
		}
		if (Gyroscope.IsMonitoring)
		{
			Gyroscope.Stop();
			Gyroscope.ReadingChanged -= Gyroscope_ReadingChanged;
		}

		// Detener cualquier audio en reproducción
		StopCurrentAudio();
	}

	// Detiene y libera cualquier audio que se esté reproduciendo
	private void StopCurrentAudio()
	{
		if (currentPlayer != null && currentPlayer.IsPlaying)
		{
			currentPlayer.Stop();
			currentPlayer.Dispose();
			currentPlayer = null;
		}
		isSoundPlaying = false;
	}
}