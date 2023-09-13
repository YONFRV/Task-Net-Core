namespace Task.Models
{
    public class ResponseGeneralModel<T>
    {
        public int Status { get; set; }
        public ResponseModel<T>? response { get; set; }

}
}
