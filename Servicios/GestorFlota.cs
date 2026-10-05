using System;
using GestionVehiculos.Interfaces;
using GestionVehiculos.Modelos;

namespace GestionVehiculos.Servicios
{
    /// <summary>
    /// Administra la flota usando un arreglo polimórfico Vehiculo[] de tamaño fijo.
    /// </summary>
    public class GestorFlota
    {
        private const int CapacidadMaxima = 50;
        private readonly Vehiculo[] flota = new Vehiculo[CapacidadMaxima];
        private int cantidad = 0;

        // ===================== Utilidades internas =====================

        private bool Agregar(Vehiculo vehiculo)
        {
            if (cantidad >= CapacidadMaxima)
            {
                Program.MostrarError("La flota está llena. No se pueden registrar más vehículos.");
                return false;
            }
            flota[cantidad] = vehiculo;
            cantidad++;
            return true;
        }

        private bool FlotaVacia()
        {
            if (cantidad == 0)
            {
                Program.MostrarAdvertencia("No hay vehículos registrados en la flota.");
                return true;
            }
            return false;
        }

        // Sobrecarga 1: busca por código (comparación sin distinguir mayúsculas)
        public Vehiculo Buscar(string codigo)
        {
            for (int i = 0; i < cantidad; i++)
            {
                if (flota[i].Codigo.Equals(codigo, StringComparison.OrdinalIgnoreCase))
                    return flota[i];
            }
            return null;
        }

        // Sobrecarga 2: pide el código por consola y busca
        public Vehiculo Buscar()
        {
            string codigo = Program.LeerTexto("Ingrese el código del vehículo: ");
            Vehiculo v = Buscar(codigo);
            if (v == null)
                Program.MostrarError($"No se encontró ningún vehículo con el código '{codigo}'.");
            return v;
        }

        // ===================== Opción 1: Registro polimórfico =====================

        public void RegistrarVehiculo()
        {
            if (cantidad >= CapacidadMaxima)
            {
                Program.MostrarError("La flota está llena. No se pueden registrar más vehículos.");
                return;
            }

            Console.WriteLine("\n--- REGISTRAR VEHÍCULO ---");
            Console.WriteLine("1. Automóvil");
            Console.WriteLine("2. Camioneta");
            Console.WriteLine("3. Motocicleta");
            int tipo = Program.LeerEntero("Seleccione el tipo: ", 1, 3);

            // Datos generales
            string codigo;
            while (true)
            {
                codigo = Program.LeerTexto("Código: ").ToUpper();
                if (Buscar(codigo) == null) break;
                Program.MostrarError("Ya existe un vehículo con ese código. Ingrese otro.");
            }
            string marca = Program.LeerTexto("Marca: ");
            string modelo = Program.LeerTexto("Modelo: ");
            int anio = Program.LeerEntero("Año: ", 1980, DateTime.Now.Year + 1);
            double km = Program.LeerDouble("Kilometraje: ", 0);
            double precio = Program.LeerDouble("Precio de renta por día: ", 1);

            // Datos específicos según el tipo -> se guarda en el arreglo base Vehiculo[]
            Vehiculo nuevo;
            switch (tipo)
            {
                case 1:
                    int puertas = Program.LeerEntero("Número de puertas (2-5): ", 2, 5);
                    string transmision = Program.LeerOpcion("Tipo de transmisión", "Manual", "Automática");
                    string combustible = Program.LeerOpcion("Tipo de combustible", "Gasolina", "Diésel", "Híbrido", "Eléctrico");
                    bool aire = Program.LeerSiNo("¿Tiene aire acondicionado? (S/N): ");
                    int pasajerosAuto = Program.LeerEntero("Número de pasajeros (1-9): ", 1, 9);
                    nuevo = new Automovil(codigo, marca, modelo, anio, km, precio, puertas, transmision, combustible, aire, pasajerosAuto);
                    break;
                case 2:
                    double carga = Program.LeerDouble("Capacidad de carga (toneladas): ", 0.1);
                    string traccion = Program.LeerOpcion("Tipo de tracción", "4x2", "4x4");
                    bool dobleCabina = Program.LeerSiNo("¿Tiene doble cabina? (S/N): ");
                    int pasajerosCam = Program.LeerEntero("Número de pasajeros (1-6): ", 1, 6);
                    nuevo = new Camioneta(codigo, marca, modelo, anio, km, precio, carga, traccion, dobleCabina, pasajerosCam);
                    break;
                default:
                    int cilindrada = Program.LeerEntero("Cilindrada (cc): ", 50, 2500);
                    string tipoMoto = Program.LeerOpcion("Tipo de motocicleta", "Scooter", "Enduro", "Pistera", "Urbana");
                    bool baul = Program.LeerSiNo("¿Tiene baúl? (S/N): ");
                    int cascos = Program.LeerEntero("Número de cascos (1-2): ", 1, 2);
                    nuevo = new Motocicleta(codigo, marca, modelo, anio, km, precio, cilindrada, tipoMoto, baul, cascos);
                    break;
            }

            if (Agregar(nuevo))
                Program.MostrarExito($"{nuevo.Tipo} '{nuevo.Codigo}' registrado correctamente.");
        }

        // ===================== Opciones 2, 4 y 5: Listados =====================

        // Sobrecarga 1: muestra todos los vehículos
        public void MostrarVehiculos()
        {
            Console.WriteLine("\n--- TODOS LOS VEHÍCULOS ---");
            if (FlotaVacia()) return;

            for (int i = 0; i < cantidad; i++)
                flota[i].MostrarInformacion(); // Llamada polimórfica: se ejecuta el override de cada clase
            Console.WriteLine($"----------------------------------------\nTotal: {cantidad} vehículo(s).");
        }

        // Sobrecarga 2: muestra solo los vehículos con el estado indicado
        public void MostrarVehiculos(string estado)
        {
            Console.WriteLine($"\n--- VEHÍCULOS EN ESTADO: {estado.ToUpper()} ---");
            if (FlotaVacia()) return;

            int encontrados = 0;
            foreach (Vehiculo v in flota)
            {
                if (v != null && v.Estado == estado)
                {
                    v.MostrarInformacion();
                    encontrados++;
                }
            }

            if (encontrados == 0)
                Program.MostrarAdvertencia($"No hay vehículos en estado '{estado}'.");
            else
                Console.WriteLine($"----------------------------------------\nTotal: {encontrados} vehículo(s).");
        }

        // ===================== Opción 3: Buscar por código =====================

        public void BuscarVehiculo()
        {
            Console.WriteLine("\n--- BUSCAR VEHÍCULO ---");
            if (FlotaVacia()) return;

            Vehiculo v = Buscar();
            if (v != null)
                v.MostrarInformacion();
        }

        // ===================== Opción 6: Cambiar estado =====================

        public void CambiarEstado()
        {
            Console.WriteLine("\n--- CAMBIAR ESTADO ---");
            if (FlotaVacia()) return;

            Vehiculo v = Buscar();
            if (v == null) return;

            Console.WriteLine($"Estado actual de {v.Codigo}: {v.Estado}");
            string nuevoEstado = Program.LeerOpcion("Nuevo estado", "Disponible", "Rentado", "En Mantenimiento");

            if (nuevoEstado == v.Estado)
            {
                Program.MostrarAdvertencia($"El vehículo ya se encuentra en estado '{v.Estado}'.");
                return;
            }

            // Reglas de negocio
            if (v.Estado == "En Mantenimiento" && nuevoEstado == "Rentado")
            {
                Program.MostrarError("No se puede rentar un vehículo que está en mantenimiento. Realice el mantenimiento primero (opción 10).");
                return;
            }
            if (v.Estado == "En Mantenimiento" && nuevoEstado == "Disponible")
            {
                Program.MostrarError("Para liberar un vehículo en mantenimiento use la opción 10 (Realizar mantenimiento).");
                return;
            }
            if (v.Estado == "Rentado" && nuevoEstado == "Disponible")
            {
                // Al devolver el vehículo se actualiza el kilometraje recorrido
                double kmFinal = Program.LeerDouble($"Kilometraje al devolverlo (actual {v.Kilometros} km): ", v.Kilometros);
                v.Kilometros = kmFinal;
            }

            v.Estado = nuevoEstado;
            Program.MostrarExito($"Estado de {v.Codigo} actualizado a '{v.Estado}'.");
        }

        // ===================== Opción 7: Mayor kilometraje =====================

        public void MostrarMayorKilometraje()
        {
            Console.WriteLine("\n--- VEHÍCULO CON MAYOR KILOMETRAJE ---");
            if (FlotaVacia()) return;

            // Algoritmo de búsqueda del máximo
            Vehiculo mayor = flota[0];
            foreach (var v in flota)
            {
                if (v != null && v.Kilometros > mayor.Kilometros)
                    mayor = v;
            }
            mayor.MostrarInformacion();
        }

        // ===================== Opción 8: Precio promedio =====================

        public void CalcularPrecioPromedio()
        {
            Console.WriteLine("\n--- PRECIO PROMEDIO DE RENTA ---");
            if (FlotaVacia()) return;

            double suma = 0;
            for (int i = 0; i < cantidad; i++)
                suma += flota[i].PrecioRenta;

            double promedio = suma / cantidad;
            Console.WriteLine($"Vehículos considerados: {cantidad}");
            Console.WriteLine($"Precio promedio de renta por día: ${promedio:F2}");
        }

        // ===================== Opción 9: Conteo por tipo (is + casting) =====================

        public void ContarPorTipo()
        {
            Console.WriteLine("\n--- CANTIDAD DE VEHÍCULOS POR TIPO ---");
            if (FlotaVacia()) return;

            int autos = 0, camionetas = 0, motos = 0;
            int pasajerosAutos = 0;
            double cargaTotal = 0;
            int cilindradaTotal = 0;

            foreach (var v in flota)
            {
                if (v is Automovil)
                {
                    autos++;
                    Automovil a = (Automovil)v;          // Casting explícito
                    pasajerosAutos += a.NumeroPasajeros;
                }
                else if (v is Camioneta)
                {
                    camionetas++;
                    Camioneta c = (Camioneta)v;          // Casting explícito
                    cargaTotal += c.CapacidadCarga;
                }
                else if (v is Motocicleta)
                {
                    motos++;
                    Motocicleta m = (Motocicleta)v;      // Casting explícito
                    cilindradaTotal += m.Cilindrada;
                }
            }

            Console.WriteLine($"Automóviles : {autos}  (capacidad total: {pasajerosAutos} pasajeros)");
            Console.WriteLine($"Camionetas  : {camionetas}  (capacidad de carga total: {cargaTotal} Ton)");
            string promCc = motos > 0 ? $"{cilindradaTotal / motos} cc" : "N/A";
            Console.WriteLine($"Motocicletas: {motos}  (cilindrada promedio: {promCc})");
            Console.WriteLine($"TOTAL       : {autos + camionetas + motos}");
        }

        // ===================== Opción 10: Mantenimiento (IMantenimiento) =====================

        public void GestionarMantenimiento()
        {
            Console.WriteLine("\n--- MANTENIMIENTO ---");
            if (FlotaVacia()) return;

            Vehiculo v = Buscar();
            if (v == null) return;

            // Se trabaja a través de la interfaz: solo se ven los métodos del contrato
            IMantenimiento mantenimiento = v;

            Console.WriteLine("1. Consultar estado de mantenimiento");
            Console.WriteLine("2. Realizar mantenimiento");
            int opcion = Program.LeerEntero("Seleccione una opción: ", 1, 2);

            if (opcion == 1)
            {
                mantenimiento.ConsultarMantenimiento();
                return;
            }

            if (v.Estado == "Rentado")
            {
                Program.MostrarError("El vehículo está rentado. Debe devolverse antes de hacerle mantenimiento.");
                return;
            }

            mantenimiento.ConsultarMantenimiento();
            if (Program.LeerSiNo("¿Confirmar la realización del mantenimiento? (S/N): "))
                mantenimiento.RealizarMantenimiento();
            else
                Program.MostrarAdvertencia("Mantenimiento cancelado.");
        }

        // ===================== Datos de prueba =====================

        public void CargarDatosDePrueba()
        {
            Agregar(new Automovil("AUT001", "Toyota", "Corolla", 2022, 35000, 180000, 4, "Automática", "Híbrido", true, 5));
            Agregar(new Automovil("AUT002", "Chevrolet", "Onix", 2023, 12000, 150000, 4, "Manual", "Gasolina", true, 5));
            Agregar(new Camioneta("CAM001", "Ford", "Ranger", 2021, 68000, 320000, 1.2, "4x4", true, 5));
            Agregar(new Camioneta("CAM002", "Nissan", "Frontier", 2020, 7500, 290000, 1.0, "4x2", false, 2));
            Agregar(new Motocicleta("MOT001", "Yamaha", "NMAX", 2023, 4200, 70000, 155, "Scooter", true, 2));
            Agregar(new Motocicleta("MOT002", "Honda", "XR190L", 2022, 15000, 85000, 190, "Enduro", false, 1));

            flota[1].Estado = "Rentado";
        }
    }
}
