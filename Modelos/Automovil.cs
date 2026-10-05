using System;

namespace GestionVehiculos.Modelos
{
    public class Automovil : Vehiculo
    {
        public int NumeroPuertas { get; set; }
        public string TipoTransmision { get; set; } // "Manual", "Automática"
        public string TipoCombustible { get; set; }  // "Gasolina", "Diésel", "Híbrido"
        public bool TieneAireAcondicionado { get; set; }
        public int NumeroPasajeros { get; set; }

        // Un automóvil requiere revisión cada 10.000 km
        public override double IntervaloMantenimientoKm => 10000;

        public Automovil(string codigo, string marca, string modelo, int anio, double kilometros, double precioRenta,
                         int numeroPuertas, string tipoTransmision, string tipoCombustible, bool tieneAireAcondicionado, int numeroPasajeros)
            : base(codigo, marca, modelo, anio, "Automovil", kilometros, precioRenta)
        {
            NumeroPuertas = numeroPuertas;
            TipoTransmision = tipoTransmision;
            TipoCombustible = tipoCombustible;
            TieneAireAcondicionado = tieneAireAcondicionado;
            NumeroPasajeros = numeroPasajeros;
        }

        public override void MostrarInformacion()
        {
            base.MostrarInformacion();
            string aire = TieneAireAcondicionado ? "Sí" : "No";
            Console.WriteLine($"Detalles: {NumeroPuertas} puertas | Transmisión: {TipoTransmision} | Combustible: {TipoCombustible}");
            Console.WriteLine($"A/C: {aire} | Capacidad: {NumeroPasajeros} pasajeros");
        }

        public override void RealizarMantenimiento()
        {
            Console.WriteLine("Revisión de automóvil: cambio de aceite, filtros, frenos y alineación.");
            if (TieneAireAcondicionado)
                Console.WriteLine("Revisión adicional: carga de gas del aire acondicionado.");
            base.RealizarMantenimiento();
        }

        public override void ConsultarMantenimiento()
        {
            base.ConsultarMantenimiento();
            Console.WriteLine($"Intervalo: cada {IntervaloMantenimientoKm} km | Faltan: {KilometrosParaProximoMantenimiento()} km");
        }
    }
}
