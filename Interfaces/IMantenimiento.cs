namespace GestionVehiculos.Interfaces
{
    /// <summary>
    /// Contrato para las operaciones de mantenimiento programado de la flota.
    /// </summary>
    public interface IMantenimiento
    {
        void RealizarMantenimiento();
        void ConsultarMantenimiento();
    }
}
