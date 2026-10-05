using System;
using GestionVehiculos.Interfaces;

namespace GestionVehiculos.Modelos
{
    public abstract class Vehiculo : IMantenimiento
    {
        public string Codigo { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public int Anio { get; set; }
        public string Tipo { get; protected set; }
        public string Estado { get; set; } // "Disponible", "Rentado", "En Mantenimiento"
        public double Kilometros { get; set; }
        public double PrecioRenta { get; set; }
        public bool RequiereMantenimiento { get; protected set; }

        // Kilometraje registrado en el último mantenimiento (solo accesible desde la jerarquía)
        protected double KilometrosUltimoMantenimiento { get; set; }

        // Cada tipo de vehículo define cada cuántos km necesita mantenimiento
        public abstract double IntervaloMantenimientoKm { get; }

        protected Vehiculo(string codigo, string marca, string modelo, int anio, string tipo, double kilometros, double precioRenta)
        {
            Codigo = codigo;
            Marca = marca;
            Modelo = modelo;
            Anio = anio;
            Tipo = tipo;
            Estado = "Disponible";
            Kilometros = kilometros;
            PrecioRenta = precioRenta;
            RequiereMantenimiento = false;
            KilometrosUltimoMantenimiento = 0;
        }

        // Método virtual para sobreescritura polimórfica
        public virtual void MostrarInformacion()
        {
            Console.WriteLine($"----------------------------------------");
            Console.WriteLine($"[CÓDIGO: {Codigo}] - {Tipo.ToUpper()}");
            Console.WriteLine($"Marca/Modelo: {Marca} {Modelo} ({Anio})");
            Console.WriteLine($"Estado: {Estado} | Kilometraje: {Kilometros} km");
            Console.WriteLine($"Precio Renta/Día: ${PrecioRenta:F2}");
        }

        // Kilómetros que faltan para el próximo mantenimiento (negativo = vencido)
        public double KilometrosParaProximoMantenimiento()
        {
            return IntervaloMantenimientoKm - (Kilometros - KilometrosUltimoMantenimiento);
        }

        // Actualiza la bandera RequiereMantenimiento según el estado y el desgaste
        protected void EvaluarDesgaste()
        {
            if (Estado == "En Mantenimiento" || KilometrosParaProximoMantenimiento() <= 0)
                RequiereMantenimiento = true;
        }

        // Implementación de la interfaz IMantenimiento
        public virtual void RealizarMantenimiento()
        {
            Estado = "Disponible";
            RequiereMantenimiento = false;
            KilometrosUltimoMantenimiento = Kilometros;
            Console.WriteLine($"Mantenimiento completado para el vehículo {Codigo}. Estado actualizado a 'Disponible'.");
        }

        public virtual void ConsultarMantenimiento()
        {
            EvaluarDesgaste();
            string diagnostico = RequiereMantenimiento ? "Requiere mantenimiento inmediato" : "En óptimas condiciones";
            Console.WriteLine($"Vehículo {Codigo} -> Diagnóstico: {diagnostico} | Estado actual: {Estado}");
        }
    }
}
