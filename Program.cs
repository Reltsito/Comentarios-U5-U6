using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Comentarios_U5_U6
{
    class Comentario
    {
        public DateTime Fecha { get; set; }
        public int ID { get; set; }
        public string Autor { get; set; }
        public string Comment { get; set; }
        public string DireccionIP { get; set; }
        public int Inapropiado { get; set; }
        public int Likes { get; set; }
        

        public override string ToString()
        {
                return string.Format($"[{ID}] {Autor} : {Comment} || {Fecha} ||{Likes} Me gusta || {Inapropiado} Denuncias || {DireccionIP} ");
        }
    }

    class ComentarioDB
    {
        public static void SaveToFile(List<Comentario> comentario, string path)
        {
            StreamWriter textOut = null;

            try
            {
                textOut = new StreamWriter(new FileStream(path, FileMode.Create, FileAccess.Write));
                foreach (var comen in comentario)
                {
                    textOut.Write(comen.ID + "|");
                    textOut.Write(comen.Autor + "|");
                    textOut.Write(comen.Comment + "|");
                    textOut.Write(comen.Fecha + "|");
                    textOut.Write(comen.Likes + "|");
                    textOut.Write(comen.Inapropiado + "|");
                    textOut.Write(comen.DireccionIP);
                }
            }
            catch (IOException)
            {
                Console.WriteLine("Ya existe el archivo");
            }
            catch (Exception)
            {
                Console.WriteLine("Error");
            }
            finally
            {
                if (textOut != null)
                {
                    textOut.Close();
                }
            }
        }

        public static List<Comentario> ReadFromFile(string path)
        {
            List<Comentario> comentarios = new List<Comentario>();
            StreamReader textIN = new StreamReader(new FileStream(path, FileMode.Open, FileAccess.Read));

            while (textIN.Peek() != -1)
            {
                string row = textIN.ReadLine();
                string[] columnas = row.Split('|');
                Comentario c = new Comentario();
                c.ID = int.Parse(columnas[0]);
                c.Autor = columnas[1];
                c.Comment = columnas[2];
                c.Fecha = DateTime.Parse(columnas[3]);
                c.Likes = int.Parse(columnas[4]);
                c.Inapropiado = int.Parse((columnas[5]));
                c.DireccionIP = (columnas[6]);



                comentarios.Add(c);
            }

            textIN.Close();
            return comentarios;
        }

        public static void ImprimirLike(string path)
        {
            List<Comentario> comentarios;
            comentarios = ReadFromFile(path);

            var OrdenarPorLikes = from c in comentarios
                                  orderby c.Likes descending
                                  select c;

            foreach (var c in OrdenarPorLikes)
            {
                Console.WriteLine(c);
            }
        }

        public static void ImprimirFecha(string path)
        {
            List<Comentario> comentarios;
            comentarios = ReadFromFile(path);

            var OrdenarPorFecha = from c in comentarios
                                  orderby c.Fecha descending
                                  select c;

            foreach (var c in OrdenarPorFecha)
            {
                Console.WriteLine(c);
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // List<Comentario> comentarios1 = new List<Comentario>();

            // comentarios1.Add(new Comentario() { ID = 1212, Autor = "Alberto Clemente", Comment = "Hola Gominola", Fecha = DateTime.Now,  Likes = 3, Inapropiado = 0, DireccionIP = "1.334.2000" });

            Console.WriteLine("Comentarios:");
            List<Comentario> comentarios = ComentarioDB.ReadFromFile(@"C:\Users\genji\Desktop\Comentarios.txt");
            foreach (var c in comentarios)
            {
                Console.WriteLine(c);
            }

            Console.WriteLine("Comentarios destacados");
            ComentarioDB.ImprimirFecha(@"C:\Users\genji\Desktop\Comentarios.txt");
            Console.WriteLine("Comentarios más antigúos");
            ComentarioDB.ImprimirFecha(@"C:\Users\genji\Desktop\Comentarios.txt");


            Console.WriteLine("Comentarios apropiados");
            var filtroInapropiado = from c in comentarios
                                    where c.Inapropiado <= c.Likes
                                    select c;
            foreach (var c in filtroInapropiado)
            {
                Console.WriteLine(c);
            }

            Console.WriteLine("¿Agregar nuevo comentario?");
            Console.WriteLine("Escriba Si o No");

            try
            {
                string des = Console.ReadLine();
                if (des == "Si")
                {
                    Console.WriteLine("Escribe tu ID");
                    int I = int.Parse(Console.ReadLine());
                    Console.WriteLine("Autor");
                    string N = Console.ReadLine();
                    Console.WriteLine("Fecha de publicación");
                    DateTime F = DateTime.UtcNow;
                    Console.WriteLine("Escribir comentario");
                    string M = Console.ReadLine();
                    Console.WriteLine("IP");
                    string P = Console.ReadLine();

                    int n = 0;
                    int m = 0;

                    comentarios.Add(new Comentario() { ID = I, Autor = N, Comment = M, Fecha = F, Likes = m, Inapropiado = n, DireccionIP = P });
                }
            }

            catch (FormatException)
            {
                Console.WriteLine("Error de formato");   
            }
            catch (Exception)
            {
                Console.WriteLine("Error");
            }
            ComentarioDB.SaveToFile(comentarios, @"C:\Users\genji\Desktop\Comentarios.txt");

            /*
            for (var i = 0; i != 4; i = i++)
            {
                try
                {
                    Console.WriteLine("Escribe || 1: Borrar por ID || 2: Ordenar por likes || 3: Ordenar por fecha || 4: Salir ");

                    int seleccionador = Convert.ToInt32(Console.ReadLine());
                    i = seleccionador;
                    if (seleccionador > 0 && seleccionador < 5)
                    {
                        if (seleccionador == 1)
                        {
                            Console.WriteLine("Ingrese la ID del comentario");
                            int BorrarID;
                            BorrarID = Convert.ToInt32(Console.ReadLine());
                            comentarios.RemoveAll(b => b.ID == BorrarID);

                            foreach (var c in comentarios)
                            {
                                Console.WriteLine(c);
                            }
                            ComentarioDB.SaveToFile(comentarios, @"C:\Users\genji\Desktop\Comentarios.txt");
                        }

                        else if (seleccionador == 2)
                        {
                            ComentarioDB.ImprimirLike(@"C:\Users\genji\Desktop\Comentarios.txt");
                        }

                        else if (seleccionador == 3)
                        {
                            ComentarioDB.im(@"C:\Users\genji\Desktop\Comentarios.txt");
                        }

                        Console.WriteLine("Presione cualquier tecla para continuar");
                        Console.ReadKey();
                        Console.Clear();
                    }
                    else
                    {
                        Console.WriteLine("Opción no disponible");
                    }

                }

                catch (Exception)
                {
                    throw;
                }
            }
            /*
            foreach (var c in comentarios)
            {
                Console.WriteLine(c);
            }
            */
        }
    }
}
