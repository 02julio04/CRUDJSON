using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDJSON
{
    public class Menu:Program
    {
        string op_menu = "";
        public void iniciar()
        {
            do
            {
                cabecera();

            } while (op_menu != "0");
        }
        private void cabecera()
        {
            Console.WriteLine("|-------------WELCOME Screen #1----------|");
            Console.WriteLine("|-------------------Menu-----------------|");
            Console.WriteLine("|1 [Crear Registro]   | 3 [Eliminar]     |");
            Console.WriteLine("|2 [Listar Registro]  | 4 [Actualizar]   |");
            Console.WriteLine("|5 [Buscar]           | 0 [Salir]        |");
            Console.WriteLine("|----------------------------------------|");
            Console.WriteLine("\n");
            Console.WriteLine("Digite su opcion:");
            op_menu = Console.ReadLine();
            selecMenu(op_menu);
        }
        private void selecMenu(string op)
        {
            if (op == "")
                return;
            switch (op)
            {
                case "1":
                    Console.Clear();
                    Console.WriteLine("--WELCOME Screen Registrarse---");
                    Agregarperson();
                    volver_menu();
                    Console.ReadKey();
                    break;
                case "2":
                    Console.Clear();
                    Console.WriteLine("--WELCOME Screen Datos---");
                    MostrarPerson();
                    volver_menu();
                    Console.ReadKey();
                    break;
                case "3":
                    Console.Clear();
                    Console.WriteLine("--WELCOME Screen Eliminar---");
                    EliminarPerson();
                    volver_menu();
                    Console.ReadKey();
                    break;
                case "4":
                    Console.Clear();
                    Console.WriteLine("--WELCOME Screen Actualizar---");
                    ActualizarPerson();
                    volver_menu();
                    Console.ReadKey();
                    break;
                case "5":
                    Console.Clear();
                    Console.WriteLine("--WELCOME Screen Buscar---");
                    buscar();
                    volver_menu();
                    Console.ReadKey();
                    break;
                case "q":
                    Console.Clear();
                    cabecera();
                    Console.ReadKey();
                    break;
                case "0":
                    Console.Clear();
                    Console.WriteLine("Screen Exit");
                    Console.WriteLine("-----------BYE-----------------");
                    Console.ReadKey();
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("opcion incorrecta");
                    Console.ReadKey();
                    Console.Clear ();
                    cabecera ();

                    break;
            }
        }
        private void volver_menu()
        {
            string op;
            Console.WriteLine("----------------------------");
            Console.WriteLine("Digite (q) para volver al menu");

            op = Console.ReadLine();
            selecMenu(op);
        }
        /*
        private bool lista_vacia()
        {
            
            if (jsonFile.ToString())
            {
                return true;
            }
            else
            {
                return false;
            }
        }*/
    }
}
