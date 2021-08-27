using System;

namespace Api.AccountTransactions.Dtos
{
    public class Transaction
    {
        public string id { get; set; }
        public string fromAccount { get; set; }
        public string toAccount { get; set; }
        public string description { get; set; }
        public double amount { get; set; }
        public string date { get; set; }
        public Customer owner { get; set; }

    }
}
