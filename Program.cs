using System;
using System.Globalization;
using System.Text;
using GestionVehiculos.Servicios;

namespace GestionVehiculos
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            GestorFlota gestor = new GestorFlota();
            gestor.CargarDatosDePrueba(); // Comente esta línea para iniciar con la flota vacía

            int opcion;
            do
            {
                MostrarMenu();
                opcion = LeerEntero("Seleccione una opción: ");

                switch (opcion)
                {
                    case 1: gestor.RegistrarVehiculo(); break;
                    case 2: gestor.MostrarVehiculos(); break;
                    case 3: gestor.BuscarVehiculo(); break;
                    case 4: gestor.MostrarVehiculos("Disponible"); break;
                    case 5: gestor.MostrarVehiculos("Rentado"); break;
                    case 6: gestor.CambiarEstado(); break;
                    case 7: gestor.MostrarMayorKilometraje(); break;
                    case 8: gestor.CalcularPrecioPromedio(); break;
                    case 9: gestor.ContarPorTipo(); break;
                    case 10: gestor.GestionarMantenimiento(); break;
                    case 11: Console.WriteLine("\nGracias por usar el sistema. ¡Hasta pronto!"); break;
                    default: MostrarError("Opción no válida. Elija un número entre 1 y 11."); break;
                }

                if (opcion != 11)
                    Pausar();

            } while (opcion != 11);
        }

        private static void MostrarMenu()
        {
            Console.WriteLine();
            Console.WriteLine("========================================");
            Console.WriteLine("===== SISTEMA DE RENTA DE VEHÍCULOS =====");
            Console.WriteLine("========================================");
            Console.WriteLine("1. Registrar vehículo");
            Console.WriteLine("2. Mostrar todos los vehículos");
            Console.WriteLine("3. Buscar vehículo por código");
            Console.WriteLine("4. Mostrar vehículos disponibles");
            Console.WriteLine("5. Mostrar vehículos rentados");
            Console.WriteLine("6. Cambiar estado");
            Console.WriteLine("7. Vehículo con mayor kilometraje");
            Console.WriteLine("8. Calcular precio promedio de renta");
            Console.WriteLine("9. Cantidad de vehículos por tipo");
            Console.WriteLine("10. Realizar mantenimiento");
            Console.WriteLine("11. Salir");
            Console.WriteLine("========================================");
        }

        private static void Pausar()
        {
            Console.Write("\nPresione ENTER para continuar...");
            Console.ReadLine();
        }

        // ===================== Lectura segura de datos =====================

        public static int LeerEntero(string mensaje)
        {
            int valor;
            Console.Write(mensaje);
            while (!int.TryParse(Console.ReadLine(), out valor))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("Entrada no válida. Ingrese un número entero: ");
                Console.ResetColor();
            }
            return valor;
        }

        // Sobrecarga: entero dentro de un rango
        public static int LeerEntero(string mensaje, int minimo, int maximo)
        {
            int valor = LeerEntero(mensaje);
            while (valor < minimo || valor > maximo)
            {
                MostrarError($"El valor debe estar entre {minimo} y {maximo}.");
                valor = LeerEntero(mensaje);
            }
            return valor;
        }

        // Acepta punto o coma como separador decimal
        public static double LeerDouble(string mensaje, double minimo)
        {
            Console.Write(mensaje);
            while (true)
            {
                string entrada = (Console.ReadLine() ?? "").Trim().Replace(',', '.');
                if (double.TryParse(entrada, NumberStyles.Float, CultureInfo.InvariantCulture, out double valor))
                {
                    if (valor >= minimo) return valor;
                    MostrarError($"El valor debe ser mayor o igual a {minimo}.");
                }
                else
                {
                    MostrarError("Entrada no válida. Ingrese un número.");
                }
                Console.Write(mensaje);
            }
        }

        public static string LeerTexto(string mensaje)
        {
            Console.Write(mensaje);
            string texto = (Console.ReadLine() ?? "").Trim();
            while (texto.Length == 0)
            {
                MostrarError("El campo no puede estar vacío.");
                Console.Write(mensaje);
                texto = (Console.ReadLine() ?? "").Trim();
            }
            return texto;
        }

        public static bool LeerSiNo(string mensaje)
        {
            while (true)
            {
                string r = LeerTexto(mensaje).ToUpper();
                if (r == "S" || r == "SI" || r == "SÍ") return true;
                if (r == "N" || r == "NO") return false;
                MostrarError("Responda S (sí) o N (no).");
            }
        }

        // Muestra una lista numerada de opciones y devuelve la elegida
        public static string LeerOpcion(string titulo, params string[] opciones)
        {
            Console.WriteLine($"{titulo}:");
            for (int i = 0; i < opciones.Length; i++)
                Console.WriteLine($"  {i + 1}. {opciones[i]}");
            int indice = LeerEntero("Seleccione: ", 1, opciones.Length);
            return opciones[indice - 1];
        }

        // ===================== Mensajes con color =====================

        public static void MostrarError(string mensaje) => Escribir(mensaje, ConsoleColor.Red);
        public static void MostrarExito(string mensaje) => Escribir(mensaje, ConsoleColor.Green);
        public static void MostrarAdvertencia(string mensaje) => Escribir(mensaje, ConsoleColor.Yellow);

        private static void Escribir(string mensaje, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(mensaje);
            Console.ResetColor();
        }
    }
}
