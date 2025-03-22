using System.Collections.ObjectModel;

namespace u3a1_moreno
{
	public partial class MainPage : ContentPage
	{
		// Definir una lista de categorías
		public ObservableCollection<Category> Categories { get; set; }

		public MainPage()
		{
			InitializeComponent();

			// Creamos una lista de categorias
			Categories = new ObservableCollection<Category>
			{
				new Category { Images = "Images/f1.png", Name = "F1" },
				new Category { Images = "Images/estrella.png", Name = "Super Mario" }
			};

			// Asignar la lista a la propiedad ItemsSource de la CollectionView
			BindingContext = this;
		}

		private async void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
		{
			// Obtenemos el nombre a traves de CommandParameter
			var frame = sender as Frame;
			if (frame == null) return;

			var tappedItem = (e as TappedEventArgs)?.Parameter as string;

			if (tappedItem != null)
			{
				// Navega a Page2 pasando la categoría seleccionada
				await Navigation.PushAsync(new Page2(tappedItem));
			}
		}
	}

	// Clase para los elementos de la lista
	public class Category
	{
		public string Images { get; set; }
		public string Name { get; set; }
	}
}
