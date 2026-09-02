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
    Console.WriteLine("4. Modificar Autor");
    Console.WriteLine("5. Modificar Libro");
    Console.WriteLine("6. Eliminar Libro");
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

        case "4":
            ModificarAutor();
            break;

        case "5":
            ModificarLibro();
            break;

        case "6":
            EliminarLibro();
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
    void ModificarAutor()
    {
        Console.Write("Ingrese el ID del autor que desea modificar: ");
        int id = int.Parse(Console.ReadLine());

        var autores = autorRepository.ObtenerTodos();

        var autor = autores.FirstOrDefault(a => a.Id == id);

        if (autor == null)
        {
            Console.WriteLine("No existe un autor con ese ID.");
            PresioneParaContinuar();
            return;
        }

        Console.WriteLine($"Nombre actual: {autor.Nombre}");

        Console.Write("Ingrese el nuevo nombre: ");
        string nuevoNombre = Console.ReadLine();

        autor.Nombre = nuevoNombre;

        autorRepository.Modificar(autor);

        Console.WriteLine("Autor modificado correctamente.");

        PresioneParaContinuar();
    }
    void ModificarLibro()
    {
        Console.Write("Ingrese el ID del libro que desea modificar: ");
        int id = int.Parse(Console.ReadLine());

        var libros = libroRepository.ObtenerTodosCon("Autor");

        var libro = libros.FirstOrDefault(l => l.Id == id);

        if (libro == null)
        {
            Console.WriteLine("No existe un libro con ese ID.");
            PresioneParaContinuar();
            return;
        }

        Console.WriteLine($"Título actual: {libro.Titulo}");
        Console.WriteLine($"Año de publicación: {libro.AnioPublicacion}");
        Console.WriteLine($"Autor: {libro.Autor.Nombre}");

        Console.WriteLine();

        Console.Write("Ingrese el nuevo título: ");
        string nuevoTitulo = Console.ReadLine();

        libro.Titulo = nuevoTitulo;

        libroRepository.Modificar(libro);

        Console.WriteLine("Libro modificado correctamente.");

        PresioneParaContinuar();
    }
    void EliminarLibro()
    {
        Console.Write("Ingrese el ID del libro que desea eliminar: ");
        int id = int.Parse(Console.ReadLine());

        var libros = libroRepository.ObtenerTodosCon("Autor");

        var libro = libros.FirstOrDefault(l => l.Id == id);

        if (libro == null)
        {
            Console.WriteLine("No existe un libro con ese ID.");
            PresioneParaContinuar();
            return;
        }

        Console.WriteLine($"Libro: {libro.Titulo}");
        Console.WriteLine($"Año: {libro.AnioPublicacion}");
        Console.WriteLine($"Autor: {libro.Autor.Nombre}");

        Console.WriteLine();

        Console.Write("¿Está seguro que desea eliminarlo? (S/N): ");
        string respuesta = Console.ReadLine();

        if (respuesta.ToUpper() == "S")
        {
            libroRepository.Eliminar(libro);

            Console.WriteLine("Libro eliminado correctamente.");
        }
        else
        {
            Console.WriteLine("Operación cancelada.");
        }

        PresioneParaContinuar();
    }
    void PresioneParaContinuar()
{
    Console.WriteLine();
    Console.WriteLine("Presione una tecla para continuar...");
    Console.ReadKey();
    Console.Clear();
}
