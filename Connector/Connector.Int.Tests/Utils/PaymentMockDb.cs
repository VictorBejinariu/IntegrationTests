using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Connector.Entities;
using Connector.Interfaces;

public class PaymentMockDb:IPaymentRepository
{
    public ICollection<Payment> Data { get; }
    
    private Func<Payment, Task<bool>> insert;    

    public PaymentMockDb()
    {
        Data = new List<Payment>();
        insert = p =>
        {
            Data.Add(p);
            return Task.FromResult(true);
        };
    }

    public Task<bool> Insert(Payment payment) => insert(payment);

    public Task<Payment> Get(string id)
    {
        return Task.FromResult(Data.FirstOrDefault(p => p.Id == id))!;
    }

    public void SetupInsert(Func<Payment, Task<bool>> newInsert)
    {
        insert = newInsert;
    }
    
}