namespace Task.Models
{
    public class RequestTask
    {
        public int? TaskId { get; set; }

        public string Titulo { get; set; }

        public string? Descripcion { get; set; }

        public int State { get; set; }

    }
}
