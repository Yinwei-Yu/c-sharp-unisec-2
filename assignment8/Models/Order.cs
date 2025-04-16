namespace Week6;

using System.Diagnostics;

public class Order
{
    public int OrderId { get; set; }
    public OrderDetails OrderDetails { get; set; }

    public static event Action<Order> OrderCreated;

    public Order(int orderId, OrderDetails orderDetails)
    {
        OrderId = orderId;
        OrderDetails = orderDetails;
        OrderCreated?.Invoke(this);
    }
}

public class OrderDetails
{
    public string CustomerName { get; set; }
    public string GoodsName { get; set; }
    public int Price { get; set; }
    public DateTime OrderDate { get; set; }

    public OrderDetails(string customerName, string goodsName, int price, DateTime orderDate)
    {
        CustomerName = customerName;
        GoodsName = goodsName;
        Price = price;
        OrderDate = orderDate;
    }
}    