/* La línea "`class Snake : IRenderable`" está definiendo una clase llamada "Snake" que implementa 
la interfaz "IRenderable". Esto significa que la clase "Snake" debe proporcionar una implementación
 para el método "Render()" definido en la interfaz "IRenderable". La clase "Snake" representa 
 una serpiente en el juego y contiene métodos para mover la serpiente, hacerla crecer y 
 renderizarla en la consola. */
public class Snake : IRenderable
{
    private List<Position> _body;
    private int _growthSpurtsRemaining;

    /* El metodo 'Snake' es el constructor de la clase del mismo nombre. Es responsable de inicializar
    una nueva instancia de la clase, con un punto de spawn  y un tamaño inicial.*/
    public Snake(Position spawnLocation, int initialSize = 1)
    {
        _body = new List<Position> { spawnLocation };
        _growthSpurtsRemaining = Math.Max(0, initialSize - 1);
        Dead = false;
    }

    public bool Dead { get; private set; }
    public Position Head => _body.First();
    private IEnumerable<Position> Body => _body.Skip(1);

    /*
    Esta función mueve la cabeza de la serpiente en una dirección especificada y actualiza su 
    cuerpo en consecuencia, al mismo tiempo que verifica colisiones y rápidos crecimientos. 
    Un enumerador que representa la dirección en la que debe moverse la serpiente. 
    Puede tener uno de cuatro valores: Arriba, Izquierda, Abajo o Derecha. Si la serpiente 
    está muerta, se lanza una "InvalidOperationException" y no se devuelve nada. 
    Si la serpiente no está muerta y es un movimiento válido, el método no devuelve nada. 
    Si la serpiente no está muerta pero el movimiento es inválido, la serpiente muere y 
    no se devuelve nada.
    */
    public void Move(Direction direction)
    {
        if (Dead) throw new InvalidOperationException();

        Position newHead;

        switch (direction)
        {
            case Direction.Up:
                newHead = Head.DownBy(-1);
                break;

            case Direction.Left:
                newHead = Head.RightBy(-1);
                break;

            case Direction.Down:
                newHead = Head.DownBy(1);
                break;

            case Direction.Right:
                newHead = Head.RightBy(1);
                break;

            default:
                throw new ArgumentOutOfRangeException();
        }

        if (_body.Contains(newHead) || !PositionIsValid(newHead))
        {
            Dead = true;
            return;
        }

        _body.Insert(0, newHead);

        if (_growthSpurtsRemaining > 0)
        {
            _growthSpurtsRemaining--;
        }
        else
        {
            _body.RemoveAt(_body.Count - 1);
        }
    }

    // La función "Grow" aumenta el tamaño de la serpiente.
    public void Grow()
    {
        if (Dead) throw new InvalidOperationException();

        _growthSpurtsRemaining++;
    }

    // Esta funcion imprime la serpiente en la consola, usando caracteres especiales Unicode.
    public void Render()
    {
        Console.SetCursorPosition(Head.Left, Head.Top);
        Console.Write("◉");

        foreach (var position in Body)
        {
            Console.SetCursorPosition(position.Left, position.Top);
            Console.Write("■");
        }
    }

    /* 
    La función verifica si una posición dada es válida asegurándose de que sus valores 
    verticales y horizontales no sean negativos.
    Position es un tipo de datos personalizado que representa una posición en un plano 
    bidimensional. Normalmente contiene dos propiedades: Top y Left, que representan las 
    coordenadas verticales y horizontales de la posición, respectivamente. 

    El método PositionIsValid toma un objeto de posición como parámetro y devuelve un 
    valor booleano que indica si es válido o no.
    */
    private static bool PositionIsValid(Position position) =>
        position.Top >= 0 && position.Left >= 0;
}
