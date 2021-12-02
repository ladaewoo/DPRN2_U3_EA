/****************************************************/
/*                                                  */
/*    Universidad Abierta y a Distancia de México   */
/*    Ingeniería en Desarrollo de Software          */
/*    Programación NET II                           */
/*    Unidad 3. Manipulación de errores y           */
/*    conjunto de objetos                           */
/*    Alumno: Jonathan Amin Morales Valencia        */
/*    Profesor: José Francisco Rico Gallegos        */
/*                                                  */
/****************************************************/

using System;
using System.Collections.Generic;

// Creamos una clase para poder imprimir en consola de una manera mas ordenada
class Imprimir
{
    public static void AlCentro(string texto, int techo)
    {
        Console.SetCursorPosition((Console.WindowWidth - texto.Length) / 2, techo);
        Console.WriteLine(texto);
    }

    public static void AlCentro(string texto)
    {
        AlCentro(texto, (Console.WindowHeight / 2) - 4);
    }
    
    public Imprimir()
    {

    }
}

class Jugadores 
{
    private static int vector = 0;
    private static string[,] jugadores = new string[vector + 1, 2];
    public Jugadores()
    {}
    // encapsulamos el vector para que no se pueda modificar desde el exterior
    // y el valor solo puede ser leído a través desde la propiedad Count
    public int Count
    {
        get {
            return vector;
        }
    }

    // Con el índice podemos acceder a cada elemento del vector
    public string[] getAt(int i)
    {
        string[] jugador = new string[2];
        jugador[0] = jugadores[i, 0];
        jugador[1] = jugadores[i, 1];
        return jugador;
    }


    // Añadimos un nuevo jugador al vector
    // incrementamos el tamaño de la matríz
    private void Actualizar()
    {
        string [,] jugadores_aux = new string[vector + 2, 2];

        for (int i = 0; i < vector; i++)
        {
            jugadores_aux[i, 0] = jugadores[i, 0];
            jugadores_aux[i, 1] = jugadores[i, 1];
        }

        jugadores = jugadores_aux;
    }

    public void Agregar(string nombre, string boleto)
    {
        
        Actualizar();
        jugadores[vector, 0] = nombre;
        jugadores[vector, 1] = boleto.ToString();
        vector++;
    }
}

class Juego 
{
    // Implementaremos la clase List, que serán las colecciones de Premios y Boletos
    public static List<string> Premios = new List<string>();
    private static Jugadores jugadores = new Jugadores();

    public Juego()
    {

    }

    // Cargamos los premios
    public void CargarPremios()
    {
        Premios.Add("$1,000.00");
        Premios.Add("Batidora Oster");
        Premios.Add("Sin premio");
        Premios.Add("Sin premio");
        Premios.Add("Tesla Model S");
        Premios.Add("Sin premio");
        Premios.Add("$1,000.00");
        Premios.Add("Sin premio");
        Premios.Add("Sin premio");
        Premios.Add("$10,000.00");
        Premios.Add("Macbook pro M1 Max");
        Premios.Add("Sin premio");
        Premios.Add("Sin premio");
        Premios.Add("Sin premio"); 
        Premios.Add("Tarjeta de regalo Amazon $500"); 
        Premios.Add("Sin premio");
    }

    public int ElegirNumero()
    {
        return ElegirNumero(0);
    }

    public void Resultados()
    {
        Console.Clear();
        int top = (Console.WindowHeight / 2) - 4;
        
        Imprimir.AlCentro("Se agotaron los boletos, presiona cualquier tecla para ver la lista de resultados.");
        
        Console.ReadKey();
        Console.Clear();
 
        Imprimir.AlCentro("|------------------------------------------|", top - 2);
        Imprimir.AlCentro("Lista de resultados", top - 1);
        Imprimir.AlCentro("|------------------------------------------|", top);

        try {
            for (int i = 0; i < jugadores.Count; i++)
            {
                string[] jugador = jugadores.getAt(i);
                Imprimir.AlCentro(String.Format("|{0, 15}|{1, 20}|",  jugador[0], jugador[1]), top + i + 1);
            }

            Imprimir.AlCentro("|------------------------------------------|", Console.CursorTop);
        } catch (IndexOutOfRangeException) {
            Imprimir.AlCentro("No hay jugadores", top);
        }

        Console.ReadKey();
    }

    static private int ElegirNumero(int numero)
    {
        int top = (Console.WindowHeight / 2) - 6;
        string[] jugador = new string[2];
        Console.Clear();

         // Para debugegar
/*
        for (int i = 0; i < Premios.Count; i++)
        {
            Imprimir.AlCentro(Premios[i], top + i + 2);
        }
 */

        Imprimir.AlCentro("¿Como te llamas?");
        Console.SetCursorPosition((Console.WindowWidth / 2) - 8, (Console.WindowHeight / 2));
        jugador[0] = Console.ReadLine();
        Console.Clear();
        Imprimir.AlCentro($"💬 Elige un número entre el 1 y el {Premios.Count}");
        Console.SetCursorPosition(Console.WindowWidth / 2, (Console.WindowHeight / 2));

        // Atrapamos los errores si el usuario no introduce un número
        try {
            numero = Convert.ToInt32(Console.ReadLine());
        } catch (Exception e) {
            Console.WriteLine("🙈👷🚧 " + e.Message);
            numero = 0; 
        }
        
        Console.Clear();

        // Si el número es menor que 1 o mayor que el número de premios
        // se avisará del error
        try {
            jugador[1] = Premios[numero - 1];
            Premios.RemoveAt(numero - 1);
            Imprimir.AlCentro(jugador[1]);
            jugadores.Agregar(jugador[0], jugador[1]);
        } catch (Exception e) {
            Imprimir.AlCentro("🙈 " + e.Message + " 💥💥💥");
        }
    
        Console.ReadKey();
        return Premios.Count;
    }

    int top = (Console.WindowHeight / 2) - 5;

    public void Inicio(int estado)
    {
        Console.BackgroundColor = ConsoleColor.DarkGray;
        Console.Clear();

        Imprimir.AlCentro("Bienvenido al juego de la suerte 💫", top - 1);
        Imprimir.AlCentro("Hay grandes premios", top);
        Imprimir.AlCentro("¿Quieres jugar?", top + 1);

        if(estado == 1){
            Console.BackgroundColor = ConsoleColor.DarkRed;
        }
        
        Imprimir.AlCentro("Si", top + 3);
        Console.BackgroundColor = ConsoleColor.DarkGray;

        if(estado == 0){
            Console.BackgroundColor = ConsoleColor.DarkRed;
        }

        Imprimir.AlCentro("No", top + 5);
    }
}


namespace DPRN2_U3_A4
{
    class Program
    {
        static private int estado = 0;
        static private int capitulo = 0;

        // Obtenemos el alto de la pantalla lo dividimos entre 2 para obtener el centro 
        // y le restamos 5 líneas mas para que quede bien
        static int top = (Console.WindowHeight / 2) - 5;

        static int contador = 0;

        static Juego juego = new Juego();

        static void Main(string[] args)
        {
            Console.Title = "DPRN2_U3_A4";
            juego.CargarPremios();


            while (true)
            {
                switch(capitulo){
                    case 0:
                        juego.Inicio(estado);
                        break;
                    case 1:
                        // La función elegirNumero() devuelve el número de premios que quedan
                        // Si es 0, termina el juego y muestra los resultados
                        if(juego.ElegirNumero() > 0){
                            capitulo = 2;
                        } else {
                            juego.Resultados();
                            capitulo = 0;
                        }
                        
                        break;
                }
                
                ConsoleKeyInfo tecla;
                
                tecla = Console.ReadKey();

                // Escuchamos los eventos del teclado, para saber si quiere jugar o no
                if (tecla.Key == ConsoleKey.UpArrow || tecla.Key == ConsoleKey.LeftArrow)
                {
                    estado = 1;
                }
                else if (tecla.Key == ConsoleKey.DownArrow || tecla.Key == ConsoleKey.RightArrow)
                {
                    estado = 0;
                }
                else if(tecla.Key == ConsoleKey.Enter){
                    if(estado == 1){
                        capitulo = 1;
                    }
                    else {
                        capitulo = 0;
                        Console.Clear();
                        Imprimir.AlCentro("Hasta luego!", top);
                        Console.ReadKey();
                        Environment.Exit(0);
                    }
                }
                else if(tecla.Key == ConsoleKey.Escape){
                    capitulo = 0;
                }
                
            } 
        }
    }
}