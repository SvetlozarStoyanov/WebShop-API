﻿using Database.Entities.Common.Types;
using Database.Entities.Products;

namespace Database.Entities.Inventory
{
    public class InventoryTransaction
    {
        public long Id { get; set; }
        public int Quantity { get; set; }
        public long TypeId { get; set; }
        public InventoryTransactionType Type { get; set; }
        public long ProductId { get; set; }
        public Product Product { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
}
