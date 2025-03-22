namespace u3a1_moreno
{
	public partial class App : Application
	{
		public App()
		{
			InitializeComponent();
			MainPage = new AppShell();
		}

		protected override void OnSleep()
		{
			base.OnSleep();

			// Detener sensores
			if (Accelerometer.IsMonitoring)
			{
				Accelerometer.Stop();
			}

			if (Gyroscope.IsMonitoring)
			{
				Gyroscope.Stop();
			}
		}

		protected override void OnResume()
		{
			base.OnResume();
			Gyroscope.Start(SensorSpeed.UI);
			Accelerometer.Start(SensorSpeed.UI);
		}
	}
}
