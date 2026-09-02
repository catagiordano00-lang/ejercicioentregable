using AccesoDatos.Models;
using AccesoDatos.Repositories;

IGenericRepository<Autor> autorRepository = new GenericRepository<Autor>();
IGenericRepository<Libro> libroRepository = new GenericRepository<Libro>();

bool continuar = true;

while (continuar)
{
    Console.WriteLine("================================");
    Console.WriteLine(" SISTEMA DE BIBLIOTECA ");
    Console.WriteLine("================================");
    Console.WriteLine("1. Alta Autor");
    Console.WriteLine("2. Alta Libro");
    Console.WriteLine("3. Ver Libros");
    Console.WriteLine("0. Salir");
    Console.WriteLine();

    Console.Write("Seleccione una opción: ");
    string opcion = Console.ReadLine();

    Console.Clear();

    switch (opcion)
    {
        case "1":
            AltaAutor();
            break;

        case "2":
            AltaLibro();
            break;

        case "3":
            MostrarLibros();
            break;

        case "0":
            continuar = false;
            Console.WriteLine("Aplicación finalizada.");
            break;

        default:
            Console.WriteLine("Opción inválida.");
            PresioneParaContinuar();
            break;
    }
}

void AltaAutor()
{
    Console.Write("Nombre del autor: ");

    string nombre = Console.ReadLine();

    Autor autor = new Autor
    {
        Nombre = nombre
    };

    autorRepository.Agregar(autor);

    Console.WriteLine("Autor registrado correctamente.");

    PresioneParaContinuar();
}

void AltaLibro()
{
    Console.Write("Título: ");
    string titulo = Console.ReadLine();

    Console.Write("Año publicación: ");
    int anio = int.Parse(Console.ReadLine());

    Console.WriteLine();
    Console.WriteLine("Autores disponibles:");

    var autores = autorRepository.ObtenerTodos();

    foreach (var autor in autores)
    {
        Console.WriteLine(
            $"ID: {autor.Id} - {autor.Nombre}");
    }

    Console.WriteLine();

    Console.Write("Seleccione el ID del autor: ");

    int autorId = int.Parse(Console.ReadLine());

    Libro libro = new Libro
    {
        Titulo = titulo,
        AnioPublicacion = anio,
        AutorId = autorId
    };

    libroRepository.Agregar(libro);

    Console.WriteLine("Libro registrado correctamente.");

    PresioneParaContinuar();
}

void MostrarLibros()
{
    Console.WriteLine("===== LISTADO DE LIBROS =====");

    var libros = libroRepository.ObtenerTodosCon("Autor");

    if (!libros.Any())
    {
        Console.WriteLine("No existen libros registrados.");
    }
    else
    {
        foreach (var libro in libros)
        {
            Console.WriteLine($"ID: {libro.Id} | Título: {libro.Titulo} | Año: {libro.AnioPublicacion} " +
                $"| Autor: {libro.Autor.Nombre}");
        }
    }

    Console.WriteLine("=============================");

    PresioneParaContinuar();
}

void PresioneParaContinuar()
{
    Console.WriteLine();
    Console.WriteLine("Presione una tecla para continuar...");
    Console.ReadKey();
    Console.Clear();
}
