namespace locationapi.Modelos
{
    public class Device
    {
        public int ID { get; set; }
        public string IMEI { get; set; }
        public string NúmeroDeControl { get; set; }
        public string CustomerID { get; set; }

        public bool IsActive { get; set; } //logic/bussines

        public bool IsHistoryActive { get; set; }//historial

        /// <summary>
        /// Sería mejor que esta parte fuera de otra clase pero para probar funcionamente general se va a dejar aquí.
        /// vienen de la app de MAUI
        /// </summary>
        public string NombreAsignadoAlDispositivo { get; set; }
        public string ModeloDeAuto { get; set; }
        public string Propietario { get; set; }

        // Agregado para dibujar círculo de área definida en la aplicación de .NET MAUI.
        public string LatitudActual { get; set; }
        public string LongitudActual { get; set; }

    }
}