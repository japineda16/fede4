/* La clase SnakeGame : IRenderable está definiendo una clase llamada SnakeGame que implementa 
la interfaz IRenderable. Esto significa que la clase SnakeGame debe proporcionar una 
implementación para el método Render() definido en la interfaz IRenderable. */
public class SnakeGame : IRenderable
{
    private static readonly Position Origin = new Position(0, 0);

    private Direction _currentDirection;
    private Direction _nextDirection;
    private Snake _snake;
    private Apple _apple;

    /* `public SnakeGame()` is a constructor for the `SnakeGame` class. It creates a new instance of the
    `Snake` class with an initial size of 5, creates a new instance of the `Apple` class using the
    `CreateApple()` method, and sets the current and next directions to `Direction.Right`. */
    public SnakeGame()
    {
        _snake = new Snake(Origin, initialSize: 5);
        _apple = CreateApple();
        _currentDirection = Direction.Right;
        _nextDirection = Direction.Right;
    }

    public bool GameOver => _snake.Dead;
    /*
    Se le pasa como parametro console Key, dicha funcion retorna VOID (vacia).
    ConsoleKey es un enum en C# que representa las teclas de un teclado. 
    Incluye teclas tanto letras como números, teclas de función, teclas flecha y 
    teclas especiales como Enter, tab y Escape. El parámetro ConsoleKey en la 
    firma del método indica que el método espera un valor ConsoleKey.
    */
    public void OnKeyPress(ConsoleKey key)
    {
        Direction newDirection;

        switch (key)
        {
            case ConsoleKey.W:
                newDirection = Direction.Up;
                break;

            case ConsoleKey.A:
                newDirection = Direction.Left;
                break;

            case ConsoleKey.S:
                newDirection = Direction.Down;
                break;

            case ConsoleKey.D:
                newDirection = Direction.Right;
                break;

            default:
                return;
        }
        // La serpiente no puede girar 180 grados.
        if (newDirection == OppositeDirectionTo(_currentDirection))
        {
            return;
        }

        _nextDirection = newDirection;
    }

    /// Esta es una función en C# que se llama en cada parte del juego.
    // Funcionalidad basical del juego, valida si el juego acabo para dar un Exception o,
    // permite que la serpiente siga moviendose en la pantalla y/o valida si la cabeza de la
    // serpiente impacta con la manzana.

    public void OnGameTick()
    {
        if (GameOver) throw new InvalidOperationException();

        _currentDirection = _nextDirection;
        _snake.Move(_currentDirection);

        // Si la cabeza de la serpiente choca con la manzana
        // el se la comerá.
        if (_snake.Head.Equals(_apple.Position))
        {
            _snake.Grow();
            _apple = CreateApple();
        }
    }

    // La funcion Render ya viene declara por defecto en C#.
    public void Render()
    {
        Console.Clear();
        _snake.Render();
        _apple.Render();
        Console.SetCursorPosition(0, 0);
    }

    /*
    Esta funcion retorna la dirección/sentido opuesto del input.
    "Direction" es probablemente un tipo de enumeración que representa las 
    posibles direcciones en un juego o aplicación. Podría incluir valores 
    como Norte, Sur, Este y Oeste, o Arriba, Abajo, Izquierda y Derecha.
    */
    private static Direction OppositeDirectionTo(Direction direction)
    {
        switch (direction)
        {
            case Direction.Up: return Direction.Down;
            case Direction.Left: return Direction.Right;
            case Direction.Right: return Direction.Left;
            case Direction.Down: return Direction.Up;
            default: throw new ArgumentOutOfRangeException();
        }
    }

    /// <summary>
    /// Esta funcion crea una instancia de la
    /// </summary>
    private static Apple CreateApple()
    {
        // Se puede factorizar en otro lugar.
        const int numberOfRows = 20;
        const int numberOfColumns = 20;

        var random = new Random();
        var top = random.Next(0, numberOfRows + 1);
        var left = random.Next(0, numberOfColumns + 1);
        var position = new Position(top, left);
        var apple = new Apple(position);

        return apple;
    }
}

