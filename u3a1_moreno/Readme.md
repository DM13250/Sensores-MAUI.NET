# u3a1_moreno

Una aplicación para Android que utiliza sensores del dispositivo para interactuar con el usuario de manera dinámica. La app incluye características como detección de movimiento, reproducción de sonidos personalizados, y cambio de color de la interfaz, todo controlado por los sensores del teléfono.

## Características

### 1. Selección de Tipos de Sonidos
- La aplicación permite elegir entre diferentes categorías de sonidos, como:
  - **F1**: sonidos relacionados con automóviles de Fórmula 1.
  - **Super Mario**: sonidos de personajes de Mario Bros.
- Cada categoría incluye al menos dos sonidos diferentes.

### 2. Detección de Movimientos
- **Acelerómetro**:
  - Movimiento a la izquierda: reproduce un sonido específico de la categoría elegida.
  - Movimiento a la derecha: reproduce otro sonido de la categoría.
- La detección de movimientos se basa en el eje **X** del acelerómetro.

### 3. Cambio de Color de Fondo
- **Giroscopio**:
  - Cambia el color de fondo en función de los valores del sensor:
    - Movimiento hacia un lado: el fondo de la pantalla cambia a **verde**.
    - Movimiento hacia el otro lado: el fondo de la pantalla cambia a **rojo**.

### 4. Visualización de Datos de Sensores
- En la pantalla se muestran:
  - **Valores actuales** de los sensores (acelerómetro y giroscopio).
  - **Límites** para la detección de movimientos.
  - Indicaciones claras de cuándo se alcanza un límite.

## Código Principal

### `App.xaml.cs`
- Configura el ciclo de vida de la aplicación.
- Inicia y detiene los sensores (acelerómetro y giroscopio) según el estado de la app (activa, en segundo plano, etc.).

### `MainPage.xaml.cs`
- Muestra la pantalla principal con la lista de categorías de sonidos disponibles.
- Navega a la segunda pantalla al seleccionar una categoría.

### `Page2.xaml.cs`
- Implementa la lógica de interacción:
  - Manejo del acelerómetro y giroscopio.
  - Reproducción de sonidos según el movimiento detectado.
  - Cambio dinámico de colores del fondo basado en el giroscopio.
- Optimiza el uso de sensores y audio para asegurar un rendimiento fluido.

## Requisitos del Proyecto

- **Framework**: Microsoft MAUI.
- **Sensores utilizados**:
  - Acelerómetro para detectar movimientos en el eje X.
  - Giroscopio para detectar movimientos angulares.
- **Audio**:
  - Uso de la biblioteca `Plugin.Maui.Audio` para la reproducción de sonidos.
- **Diseño**:
  - UI interactiva con cambios visuales en tiempo real basados en los datos de los sensores.
