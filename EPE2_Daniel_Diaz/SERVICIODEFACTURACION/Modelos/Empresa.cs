namespace SERVICIODEFACTURACION.Modelos
{
    //Modelo Empresa
    public class Empresa
    {
        public string RutEmpresa {  get; set; }
        public string NombreEmpresa { get; set;}
        public string GiroEmpresa { get; set;}
        public string RutCliente { get; set; }
        public string NombreCliente { get; set; }
        public string? ApellidosCliente { get; set; }
        public int? EdadCliente { get; set; }
        public int TotalVentas { get; set; }
        public decimal MontoVentas { get; set; }
        public decimal MontoIVAApagar { get; set; }
        public decimal MontoUtilidades { get; set; }
    }
}
