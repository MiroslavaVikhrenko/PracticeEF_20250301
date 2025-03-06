using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task7_20250306
{
    internal class Order
    {
        /*
         Идентификатор (Id) 
Дата создания (CreationDate) 
Сумма заказа (TotalAmount) 
Статус заказа (Status) 
Идентификатор клиента (CustomerId) 
         */
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public decimal TotalAmount  { get; set; }
        public Status Status { get; set; }
        public int CustomerId { get; set; } // Foreign key property
        public Customer Customer { get; set; } // Navigation property

    }
    //"Новый", "В обработке", "Отправлен", "Доставлен", "Отменен". 
    public enum Status: int
    {
        New,
        InProgress,
        Dispatched,
        Delivered,
        Canceled
    }
}
