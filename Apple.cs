
/* La clase "Apple : IRenderable" está definiendo una clase llamada "Apple" que implementa la 
interfaz "IRenderable". Esto significa que la clase "Apple" debe proporcionar una implementación 
para el método "Render()" definido en la interfaz "IRenderable". La clase "Apple" representa
una manzana en el juego y contiene una propiedad de posición que representa la posición de la 
manzana en el tablero del juego, así como un método "Render()" que renderiza la manzana 
en la consola. */
public class Apple : IRenderable
{
    public Apple(Position position)
    {
        Position = position;
    }

    public Position Position { get; }

    public void Render()
    {
        Console.SetCursorPosition(Position.Left, Position.Top);
        Console.Write("🍏");
    }
}
