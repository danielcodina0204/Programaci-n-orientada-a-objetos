using System;

namespace GestionVehiculos.Modelos
{
    public class Motocicleta : Vehiculo
    {
        public int Cilindrada { get; set; }          // En cc
        public string TipoMotocicleta { get; set; }   // "Scooter", "Enduro", "Pistera"
        public bool TieneBaul { get; set; }
        public int NumeroCascos { get; set; }

        // Una motocicleta requiere revisión cada 5.000 km
        public override double IntervaloMantenimientoKm => 5000;

        public Motocicleta(string codigo, string marca, string modelo, int anio, double kilometros, double precioRenta,
                           int cilindrada, string tipoMotocicleta, bool tieneBaul, int numeroCascos)
            : base(codigo, marca, modelo, anio, "Motocicleta", kilometros, precioRenta)
        {
            Cilindrada = cilindrada;
            TipoMotocicleta = tipoMotocicleta;
            TieneBaul = tieneBaul;
            NumeroCascos = numeroCascos;
        }

        public override void MostrarInformacion()
        {
            base.MostrarInformacion();
            string baul = TieneBaul ? "Sí" : "No";
            Console.WriteLine($"Detalles: Cilindrada: {Cilindrada} cc | Tipo: {TipoMotocicleta} | Baúl: {baul} | Cascos: {NumeroCascos}");
        }

        public override void RealizarMantenimiento()
        {
            Console.WriteLine("Revisión de motocicleta: aceite, cadena/kit de arrastre, frenos y llantas.");
            Console.WriteLine($"Revisión adicional: estado de los {NumeroCascos} casco(s) entregados.");
            base.RealizarMantenimiento();
        }

        public override void ConsultarMantenimiento()
        {
            base.ConsultarMantenimiento();
            Console.WriteLine($"Intervalo: cada {IntervaloMantenimientoKm} km | Faltan: {KilometrosParaProximoMantenimiento()} km");
        }
    }
}
