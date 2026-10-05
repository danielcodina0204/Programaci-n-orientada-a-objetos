using System;

namespace GestionVehiculos.Modelos
{
    public class Camioneta : Vehiculo
    {
        public double CapacidadCarga { get; set; } // En toneladas
        public string TipoTraccion { get; set; }   // "4x2", "4x4"
        public bool TieneDobleCabina { get; set; }
        public int NumeroPasajeros { get; set; }

        // Por su uso pesado, una camioneta requiere revisión cada 8.000 km
        public override double IntervaloMantenimientoKm => 8000;

        public Camioneta(string codigo, string marca, string modelo, int anio, double kilometros, double precioRenta,
                         double capacidadCarga, string tipoTraccion, bool tieneDobleCabina, int numeroPasajeros)
            : base(codigo, marca, modelo, anio, "Camioneta", kilometros, precioRenta)
        {
            CapacidadCarga = capacidadCarga;
            TipoTraccion = tipoTraccion;
            TieneDobleCabina = tieneDobleCabina;
            NumeroPasajeros = numeroPasajeros;
        }

        public override void MostrarInformacion()
        {
            base.MostrarInformacion();
            string cabina = TieneDobleCabina ? "Doble Cabina" : "Sencilla";
            Console.WriteLine($"Detalles: Carga: {CapacidadCarga} Ton | Tracción: {TipoTraccion} | Cabina: {cabina} | Pasajeros: {NumeroPasajeros}");
        }

        public override void RealizarMantenimiento()
        {
            Console.WriteLine("Revisión de camioneta: aceite, suspensión, frenos y sistema de carga.");
            if (TipoTraccion == "4x4")
                Console.WriteLine("Revisión adicional: caja de transferencia y diferenciales 4x4.");
            base.RealizarMantenimiento();
        }

        public override void ConsultarMantenimiento()
        {
            base.ConsultarMantenimiento();
            Console.WriteLine($"Intervalo: cada {IntervaloMantenimientoKm} km | Faltan: {KilometrosParaProximoMantenimiento()} km");
        }
    }
}
