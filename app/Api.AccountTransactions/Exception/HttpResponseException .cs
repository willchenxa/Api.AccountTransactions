namespace Api.AccountTransactions.Exception
{
    public class HttpResponseException : System.Exception
    {
        public int Status { get; set; }
        public object Value { get; set; }
    }
}
