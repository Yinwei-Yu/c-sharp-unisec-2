using System;

namespace order_winform
{
    public class Customer
    {
        public string Name { get; set; }
        public string Contact { get; set; }

        public override string ToString() => $"客户：{Name}";
    }
}