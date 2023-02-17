namespace ApiSfCuim.Domain.Models
{
    public  class Reference
    {
        public int? IdTmpArma { get; set; }
        public int? IdDescArmaFK { get; set; }
        public string NumeroSerie { get; set; }
        public int? IdDescripcionArma { get; set; }
        public string Estado { get; set; }
        public string TipoArma { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Calibre { get; set; }
        public string Medida { get; set; }
        public string Clase { get; set; }

    }
}
