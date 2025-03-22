package mono.android.app;

public class ApplicationRegistration {

	public static void registerApplications ()
	{
				// Application and Instrumentation ACWs must be registered first.
		mono.android.Runtime.register ("u3a1_moreno.MainApplication, u3a1_moreno, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", crc644a36d6ecb8be9aeb.MainApplication.class, crc644a36d6ecb8be9aeb.MainApplication.__md_methods);
		mono.android.Runtime.register ("Microsoft.Maui.MauiApplication, Microsoft.Maui, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", crc6488302ad6e9e4df1a.MauiApplication.class, crc6488302ad6e9e4df1a.MauiApplication.__md_methods);
		
	}
}
