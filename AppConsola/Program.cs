using AccesoDatos.Data;
using AccesoDatos.Models;
using Microsoft.EntityFrameworkCore;
using AccesoDatos.Repositores;

using (AplicationDbContext context = new AplicationDbContext())
{
    context.Database.Migrate();
}

IGenericRepository<Autor> autorRepository = new GenericRepository<Autor>();
IGenericRepository<Categoria> categoriaRepository = new GenericRepository<Categoria>();
IGenericRepository<Libro> libroRepository = new GenericRepository<Libro>();

bool continuar = true;

while (continuar)
{
    Console.WriteLine("================================");
    Console.WriteLine(" SISTEMA DE BIBLIOTECA ");
    Console.WriteLine("================================");
    Console.WriteLine("1. Alta Autor");
    Console.WriteLine("2. Alta Categoría");
    Console.WriteLine("3. Alta Libro");
    Console.WriteLine("4. Ver Autores");
    Console.WriteLine("5. Ver Categorías");
    Console.WriteLine("6. Ver Libros");
    Console.WriteLine("7. Modificar Libro");
    Console.WriteLine("8. Eliminar Libro");
    Console.WriteLine("9. Modificar Autor");
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
            AltaCategoria();
            break;
        case "3":
            AltaLibro();
            break;
        case "4":
            MostrarAutores();
            break;
        case "5":
            MostrarCategorias();
            break;
        case "6":
            MostrarLibros();
            break;
        case "7":
            ModificarLibro();
            break;
        case "8":
            EliminarLibro();
            break;
        case "9":
            ModificarAutor();
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
    Console.WriteLine("===== ALTA AUTOR =====");
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

void AltaCategoria()
{
    Console.WriteLine("===== ALTA CATEGORÍA =====");
    Console.Write("Nombre de la categoría: ");
    string nombre = Console.ReadLine();

    Categoria categoria = new Categoria
    {
        Nombre = nombre
    };

    categoriaRepository.Agregar(categoria);

    Console.WriteLine("Categoría registrada correctamente.");
    PresioneParaContinuar();
}

void AltaLibro()
{
    Console.WriteLine("===== ALTA LIBRO =====");

    Console.Write("Título: ");
    string titulo = Console.ReadLine();

    Console.Write("Año publicación: ");
    int anio = int.Parse(Console.ReadLine());

    Console.WriteLine();
    Console.WriteLine("Autores disponibles:");

    var autores = autorRepository.ObtenerTodos();

    if (!autores.Any())
    {
        Console.WriteLine("Primero debe registrar un autor.");
        PresioneParaContinuar();
        return;
    }

    foreach (var autor in autores)
    {
        Console.WriteLine($"ID: {autor.Id} - {autor.Nombre}");
    }

    Console.Write("Seleccione el ID del autor: ");
    int autorId = int.Parse(Console.ReadLine());

    Console.WriteLine();
    Console.WriteLine("Categorías disponibles:");

    var categorias = categoriaRepository.ObtenerTodos();

    if (!categorias.Any())
    {
        Console.WriteLine("Primero debe registrar una categoría.");
        PresioneParaContinuar();
        return;
    }

    foreach (var categoria in categorias)
    {
        Console.WriteLine($"ID: {categoria.Id} - {categoria.Nombre}");
    }

    Console.Write("Seleccione el ID de la categoría: ");
    int categoriaId = int.Parse(Console.ReadLine());

    Libro libro = new Libro
    {
        Titulo = titulo,
        AnioPublicacion = anio,
        AutorId = autorId,
        CategoriaId = categoriaId,
        Activo = true
    };

    libroRepository.Agregar(libro);

    Console.WriteLine("Libro registrado correctamente.");
    PresioneParaContinuar();
}

void MostrarAutores()
{
    Console.WriteLine("===== AUTORES =====");

    var autores = autorRepository.ObtenerTodos();

    if (!autores.Any())
    {
        Console.WriteLine("No existen autores registrados.");
    }
    else
    {
        foreach (var autor in autores)
        {
            Console.WriteLine($"ID: {autor.Id} | Nombre: {autor.Nombre}");
        }
    }

    PresioneParaContinuar();
}

void MostrarCategorias()
{
    Console.WriteLine("===== CATEGORÍAS =====");

    var categorias = categoriaRepository.ObtenerTodos();

    if (!categorias.Any())
    {
        Console.WriteLine("No existen categorías registradas.");
    }
    else
    {
        foreach (var categoria in categorias)
        {
            Console.WriteLine($"ID: {categoria.Id} | Nombre: {categoria.Nombre}");
        }
    }

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
            Console.WriteLine(
                $"ID: {libro.Id} | Título: {libro.Titulo} | Año: {libro.AnioPublicacion} " +
                $"| Autor: {libro.Autor.Nombre} | Categoría ID: {libro.CategoriaId} | " +
                $"Estado: {(libro.Activo ? "Activo" : "Eliminado")}");
        }
    }

    Console.WriteLine("=============================");
    PresioneParaContinuar();
}


void ModificarAutor()
{
    Console.WriteLine("===== MODIFICAR AUTOR =====");

    Console.Write("Ingrese el ID del autor: ");
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
    Console.WriteLine("===== MODIFICAR LIBRO =====");

    Console.Write("Ingrese el ID del libro: ");
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

    Console.Write("Ingrese el nuevo título: ");
    string nuevoTitulo = Console.ReadLine();

    libro.Titulo = nuevoTitulo;

    libroRepository.Modificar(libro);

    Console.WriteLine("Libro modificado correctamente.");
    PresioneParaContinuar();
}

void EliminarLibro()
{
    Console.WriteLine("===== ELIMINAR LIBRO =====");

    Console.Write("Ingrese el ID del libro: ");
    int id = int.Parse(Console.ReadLine());

    var libros = libroRepository.ObtenerTodosCon("Autor");
    var libro = libros.FirstOrDefault(l => l.Id == id);

    if (libro == null)
    {
        Console.WriteLine("No existe un libro con ese ID.");
        PresioneParaContinuar();
        return;
    }

    Console.WriteLine($"Título: {libro.Titulo}");
    Console.WriteLine($"Año de publicación: {libro.AnioPublicacion}");
    Console.WriteLine($"Autor: {libro.Autor.Nombre}");

    Console.Write("¿Está seguro que desea eliminar este libro? (S/N): ");
    string respuesta = Console.ReadLine();

    if (respuesta.ToUpper() == "S")
    {
        libro.Activo = false;
        libroRepository.Modificar(libro);

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


