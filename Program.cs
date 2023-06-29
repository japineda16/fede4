
/* La clase Program, es el punto de entrada del programa. Dicha clase tiene el metodo/tarea
que inicializa el juego de la serpiente. */
public static class Program
{
    /*
    La función anterior es el punto de entrada para un programa de C# y está marcada como asíncrona.
    El parámetro args es una matriz de cadenas que representa los argumentos de línea de comandos 
    pasados al programa cuando se ejecuta.
    */
    public static async Task Main(string[] args)
    {
        /* El código anterior está creando una nueva instancia de la clase SnakeGame y asignándola a 
        la variable snakeGame. También establece la variable tickRate en un TimeSpan de 100 milisegundos. */
        var tickRate = TimeSpan.FromMilliseconds(100);
        var snakeGame = new SnakeGame();

        using (var cts = new CancellationTokenSource())
        {
            /*
            La función MonitorKeyPresses monitorea de forma asíncrona las pulsaciones de teclas 
            desde la consola y llama al método OnKeyPress del objeto snakeGame.
            */
            async Task MonitorKeyPresses()
            {
                while (!cts.Token.IsCancellationRequested)
                {
                    if (Console.KeyAvailable)
                    {
                        var key = Console.ReadKey(intercept: true).Key;
                        snakeGame.OnKeyPress(key);
                    }

                    await Task.Delay(10);
                }
            }

            var monitorKeyPresses = MonitorKeyPresses();
            /*
            El código anterior está ejecutando un bucle de juego para un juego de serpiente. Llama 
            repetidamente al método `OnGameTick()`, que actualiza el estado del juego, y al método `Render()`,
             que muestra el juego en la pantalla. Luego espera durante una cantidad especificada de tiempo 
             utilizando `Task.Delay()` antes de repetir el bucle. El bucle continúa hasta que la propiedad `GameOver` 
             del objeto `snakeGame` sea verdadera.
            */
            do
            {
                snakeGame.OnGameTick();
                snakeGame.Render();
                await Task.Delay(tickRate);
            } while (!snakeGame.GameOver);

            // Permita que el usuario llore antes de que la aplicación salga.
            for (var i = 0; i < 3; i++)
            {
                Console.Clear();
                await Task.Delay(500);
                snakeGame.Render();
                await Task.Delay(500);
            }

            cts.Cancel();
            await monitorKeyPresses;
        }
    }
}

public enum Direction
{
    Up,
    Down,
    Left,
    Right
}

interface IRenderable
{
    void Render();
}

/* El comentario está explicando que `readonly struct Position` está definiendo una estructura de solo 
lectura llamada `Position`. Una estructura es un tipo de valor que puede contener miembros de datos y
 funciones, similar a una clase. La palabra clave `readonly` indica que la estructura es inmutable, 
 lo que significa que sus valores no pueden ser cambiados después de ser creados. La estructura 
 `Position` tiene dos propiedades, `Top` y `Left`, que representan la posición de un objeto en un 
 tablero de juego. La estructura también tiene dos métodos, `RightBy` y `DownBy`, que devuelven un 
 nuevo objeto `Position` desplazado hacia la derecha o hacia abajo por una cantidad especificada. */
public readonly struct Position
{
    public Position(int top, int left)
    {
        Top = top;
        Left = left;
    }
    public int Top { get; }
    public int Left { get; }

    public Position RightBy(int n) => new Position(Top, Left + n);
    public Position DownBy(int n) => new Position(Top + n, Left);
}
