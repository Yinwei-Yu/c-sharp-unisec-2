using OrderManagementAPI.Data;
using OrderManagementAPI.Models;
using MySqlConnector;
using System.Data;
using System.Collections.Generic;
using System;

namespace OrderManagementAPI.Services
{
    public class OrderService
    {
        private readonly OrderContext _context;

        public OrderService(OrderContext context)
        {
            _context = context;
        }

        // 添加订单
        public void AddOrder(Order order)
        {
            using (var connection = _context.CreateConnection())
            {
                connection.Open();
                const string sql = @"INSERT INTO orders (order_customerName, order_goodsName, order_ID, order_price, order_date) 
                                     VALUES (@name, @product, @ID, @price, @date)";
                using (var command = new MySqlCommand(sql, (MySqlConnection)connection))
                {
                    command.Parameters.AddWithValue("@name", order.OrderDetails.CustomerName);
                    command.Parameters.AddWithValue("@product", order.OrderDetails.GoodsName);
                    command.Parameters.AddWithValue("@ID", GetNextOrderId());
                    command.Parameters.AddWithValue("@price", order.OrderDetails.Price);
                    command.Parameters.AddWithValue("@date", order.OrderDetails.OrderDate);
                    command.ExecuteNonQuery();
                }
            }
        }

        // 删除订单
        public void DeleteOrder(int orderId)
        {
            using (var connection = _context.CreateConnection())
            {
                connection.Open();
                const string sql = "DELETE FROM orders WHERE order_ID = @id";
                using (var command = new MySqlCommand(sql, (MySqlConnection)connection))
                {
                    command.Parameters.AddWithValue("@id", orderId);
                    command.ExecuteNonQuery();
                }
            }
        }

        // 根据订单 ID 查询订单
        public Order FindAOrderById(int orderId)
        {
            using (var connection = _context.CreateConnection())
            {
                connection.Open();
                const string sql = "SELECT * FROM orders WHERE order_ID = @id";
                using (var command = new MySqlCommand(sql, (MySqlConnection)connection))
                {
                    command.Parameters.AddWithValue("@id", orderId);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            var orderDetails = new OrderDetails
                            {
                                CustomerName = reader.GetString("order_customerName"),
                                GoodsName = reader.GetString("order_goodsName"),
                                Price = reader.GetInt32("order_price"),
                                OrderDate = reader.GetDateTime("order_date")
                            };
                            return new Order { OrderId = orderId, OrderDetails = orderDetails };
                        }
                    }
                }
            }
            return null;
        }

        // 根据商品名称查询订单
        public List<Order> FindAOrderByGoodsname(string goodsName)
        {
            var orders = new List<Order>();
            using (var connection = _context.CreateConnection())
            {
                connection.Open();
                const string sql = "SELECT * FROM orders WHERE order_goodsName LIKE @goodsName";
                using (var command = new MySqlCommand(sql, (MySqlConnection)connection))
                {
                    command.Parameters.AddWithValue("@goodsName", $"%{goodsName}%");
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var orderDetails = new OrderDetails
                            {
                                CustomerName = reader.GetString("order_customerName"),
                                GoodsName = reader.GetString("order_goodsName"),
                                Price = reader.GetInt32("order_price"),
                                OrderDate = reader.GetDateTime("order_date")
                            };
                            orders.Add(new Order { OrderId = reader.GetInt32("order_ID"), OrderDetails = orderDetails });
                        }
                    }
                }
            }
            return orders;
        }

        // 根据客户名称查询订单
        public List<Order> FindAOrderByCustomer(string customerName)
        {
            var orders = new List<Order>();
            using (var connection = _context.CreateConnection())
            {
                connection.Open();
                const string sql = "SELECT * FROM orders WHERE order_customerName LIKE @customerName";
                using (var command = new MySqlCommand(sql, (MySqlConnection)connection))
                {
                    command.Parameters.AddWithValue("@customerName", $"%{customerName}%");
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var orderDetails = new OrderDetails
                            {
                                CustomerName = reader.GetString("order_customerName"),
                                GoodsName = reader.GetString("order_goodsName"),
                                Price = reader.GetInt32("order_price"),
                                OrderDate = reader.GetDateTime("order_date")
                            };
                            orders.Add(new Order { OrderId = reader.GetInt32("order_ID"), OrderDetails = orderDetails });
                        }
                    }
                }
            }
            return orders;
        }

        // 查询所有订单
        public List<Order> GetAllOrders()
        {
            var orders = new List<Order>();
            using (var connection = _context.CreateConnection())
            {
                connection.Open();
                const string sql = "SELECT * FROM orders";
                using (var command = new MySqlCommand(sql, (MySqlConnection)connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var orderDetails = new OrderDetails
                            {
                                CustomerName = reader.GetString("order_customerName"),
                                GoodsName = reader.GetString("order_goodsName"),
                                Price = reader.GetInt32("order_price"),
                                OrderDate = reader.GetDateTime("order_date")
                            };
                            orders.Add(new Order { OrderId = reader.GetInt32("order_ID"), OrderDetails = orderDetails });
                        }
                    }
                }
            }
            return orders;
        }

        // 修改订单
        public void UpdateOrder(int orderId, Order order)
        {
            using (var connection = _context.CreateConnection())
            {
                connection.Open();
                const string sql = @"UPDATE orders 
                                     SET order_customerName = @name, order_goodsName = @product 
                                     WHERE order_ID = @id";
                using (var command = new MySqlCommand(sql, (MySqlConnection)connection))
                {
                    command.Parameters.AddWithValue("@name", order.OrderDetails.CustomerName);
                    command.Parameters.AddWithValue("@product", order.OrderDetails.GoodsName);
                    command.Parameters.AddWithValue("@id", orderId);
                    command.ExecuteNonQuery();
                }
            }
        }

        // 获取最新订单 ID
        private int GetNextOrderId()
        {
            using (var connection = _context.CreateConnection())
            {
                connection.Open();
                const string sql = "SELECT MAX(order_ID) FROM orders";
                using (var command = new MySqlCommand(sql, (MySqlConnection)connection))
                {
                    var maxId = command.ExecuteScalar();
                    return maxId == DBNull.Value ? 1 : Convert.ToInt32(maxId) + 1;
                }
            }
        }
    }
}