using System;
using AccesoDatos.Data;
using AccesoDatos.Models;
using Microsoft.EntityFrameworkCore;

namespace AppConsola
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Aplicar las migraciones
            using (AplicationDbContext context = CreateContext())
            {
                context.Database.Migrate();
            }

            int opcion;

            do
            {
                Console.WriteLine();
                Console.WriteLine("===== BIBLIOTECA =====");
                Console.WriteLine("1. Alta Autor");
                Console.WriteLine("2. Alta Libro");
                Console.WriteLine("3. Ver Libros");
                Console.WriteLine("0. Salir");
                Console.Write("Seleccione una opción: ");

                opcion = int.Parse(Console.ReadLine());

                switch (opcion)
                {
                    case 1:
                        AltaAutor();
                        break;

                    case 2:
                        AltaLibro();
                        break;

                    case 3:
                        VerLibros();
                        break;

                    case 0:
                        Console.WriteLine("Saliendo...");
                        break;

                    default:
                        Console.WriteLine("Opción incorrecta.");
                        break;
                }

            } while (opcion != 0);
        }


        // Crear conexión con la base de datos
        static AplicationDbContext CreateContext()
        {
            var options = new DbContextOptionsBuilder<AplicationDbContext>();

            options.UseSqlite("Data Source=biblioteca.db");

            return new AplicationDbContext(options.Options);
        }


        // ALTA AUTOR
        static void AltaAutor()
        {
            Console.WriteLine();
            Console.WriteLine("===== ALTA AUTOR =====");

            Console.Write("Nombre del autor: ");
            string nombre = Console.ReadLine();

            Autor autor = new Autor();

            autor.Nombre = nombre;

            using (AplicationDbContext context = CreateContext())
            {
                context.Autores.Add(autor);

                context.SaveChanges();
            }

            Console.WriteLine("Autor agregado correctamente.");
        }


        // ALTA LIBRO
        static void AltaLibro()
        {
            Console.WriteLine();
            Console.WriteLine("===== ALTA LIBRO =====");

            Console.Write("Título del libro: ");
            string titulo = Console.ReadLine();

            Console.Write("Año de publicación: ");
            int anio = int.Parse(Console.ReadLine());

            // Mostrar autores disponibles
            using (AplicationDbContext context = CreateContext())
            {
                List<Autor> autores = context.Autores.ToList();

                if (autores.Count == 0)
                {
                    Console.WriteLine("No hay autores registrados.");
                    return;
                }

                Console.WriteLine();
                Console.WriteLine("Autores disponibles:");

                for (int i = 0; i < autores.Count; i++)
                {
                    Console.WriteLine(
                        (i + 1) + ". " + autores[i].Nombre
                    );
                }

                Console.Write("Seleccione el autor: ");
                int opcionAutor = int.Parse(Console.ReadLine());

                if (opcionAutor < 1 || opcionAutor > autores.Count)
                {
                    Console.WriteLine("Autor incorrecto.");
                    return;
                }

                Autor autorSeleccionado = autores[opcionAutor - 1];

                Libro libro = new Libro();

                libro.Titulo = titulo;
                libro.AnioPublicacion = anio;
                libro.AutorId = autorSeleccionado.Id;

                context.Libros.Add(libro);

                context.SaveChanges();
            }

            Console.WriteLine("Libro agregado correctamente.");
        }


        // VER LIBROS
        static void VerLibros()
        {
            Console.WriteLine();
            Console.WriteLine("===== LIBROS =====");

            using (AplicationDbContext context = CreateContext())
            {
                List<Libro> libros = context.Libros
                    .Include(l => l.Autor)
                    .ToList();

                if (libros.Count == 0)
                {
                    Console.WriteLine("No hay libros registrados.");
                    return;
                }

                foreach (Libro libro in libros)
                {
                    Console.WriteLine("-------------------------");
                    Console.WriteLine("Título: " + libro.Titulo);
                    Console.WriteLine(
                        "Año: " + libro.AnioPublicacion
                    );
                    Console.WriteLine(
                        "Autor: " + libro.Autor.Nombre
                    );
                }
            }
        }
    }
}
