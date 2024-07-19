using Microsoft.AspNetCore.Mvc;

namespace DIPrototype.Models
{
    public class TransactionQueService : ITransactionQueService
    {
        public IActionResult Create(int transactionID, string transaction)
        {
            throw new NotImplementedException();
        }

        public IActionResult Delete(string transactionUUID)
        {
            throw new NotImplementedException();
        }

        public IActionResult Update(string transactionUUID, string transaction)
        {
            throw new NotImplementedException();
        }
    }
    public interface ITransactionQueService
    {
        public IActionResult Create(int transactionID, String transaction);
        public IActionResult Update(string transactionUUID, String transaction);
        public IActionResult Delete(string transactionUUID);
    }
}
