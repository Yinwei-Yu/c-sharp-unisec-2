namespace Week6;

using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

public class OrderService
{
    private static readonly Random random = new Random();
    
    // 获取最新订单ID
    private int GetNextOrderId()
    {
        const string sql = "SELECT MAX(order_ID) FROM orders";
        try
        {
            var maxId = DatabaseHelper.ExecuteScalar<object>(sql);
            return maxId == DBNull.Value ? 1 : Convert.ToInt32(maxId) + 1;
        }
        catch
        {
            return 1; // 如果表为空或出错，从1开始
        }
    }

    // 添加订单
    public void AddOrder(string customerName, string goodsName)
    {
        if (string.IsNullOrWhiteSpace(customerName) || string.IsNullOrWhiteSpace(goodsName))
        {
            MessageBox.Show("客户名称和商品名称不能为空");
            return;
        }

        const string sql = @"INSERT INTO orders (order_customerName, order_goodsName, order_ID, order_price, order_date) 
                            VALUES (@name, @product, @ID, @price, @date)";
        
        try
        {
            int orderId = GetNextOrderId();
            int price = random.Next(100, 1000);
            DateTime orderDate = DateTime.Now;
            
            DatabaseHelper.ExecuteNonQuery(sql, parameters => {
                parameters.AddWithValue("@name", customerName);
                parameters.AddWithValue("@product", goodsName);
                parameters.AddWithValue("@ID", orderId);
                parameters.AddWithValue("@price", price);
                parameters.AddWithValue("@date", orderDate);
            });
            
            // 创建Order对象并触发事件
            var orderDetails = new OrderDetails(customerName, goodsName, price, orderDate);
            var order = new Order(orderId, orderDetails);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"添加订单失败: {ex.Message}");
        }
    }

    // 删除订单
    public void DeleteOrder(int orderId)
    {
        if (orderId <= 0)
        {
            MessageBox.Show("订单ID无效");
            return;
        }

        try
        {
            const string checkSql = "SELECT COUNT(*) FROM orders WHERE order_ID = @id";
            var count = DatabaseHelper.ExecuteScalar<int>(checkSql, parameters => {
                parameters.AddWithValue("@id", orderId);
            });
            
            if (count > 0)
            {
                const string deleteSql = "DELETE FROM orders WHERE order_ID = @delete_id";
                DatabaseHelper.ExecuteNonQuery(deleteSql, parameters => {
                    parameters.AddWithValue("@delete_id", orderId);
                });
                MessageBox.Show("删除订单成功");
            }
            else
            {
                MessageBox.Show("未查询到相关订单");
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"删除订单失败: {ex.Message}");
        }
    }

    // 根据订单ID查询订单
    public Order FindAOrderById(int orderId)
    {
        if (orderId <= 0)
        {
            MessageBox.Show("订单ID无效");
            return null;
        }

        const string sql = "SELECT * FROM orders WHERE order_ID = @id";
        Order order = null;
        
        try
        {
            DatabaseHelper.ExecuteReader(sql, reader => {
                if (reader.Read())
                {
                    var orderDetails = new OrderDetails(
                        reader.GetString("order_customerName"),
                        reader.GetString("order_goodsName"),
                        reader.GetInt32("order_price"),
                        reader.GetDateTime("order_date")
                    );
                    order = new Order(reader.GetInt32("order_ID"), orderDetails);
                }
                else
                {
                    MessageBox.Show("未找到该订单");
                }
            }, parameters => {
                parameters.AddWithValue("@id", orderId);
            });
        }
        catch (Exception ex)
        {
            MessageBox.Show($"查询订单失败: {ex.Message}");
        }
        
        return order;
    }
    
    // 根据商品名称查询订单
    public List<Order> FindAOrderByGoodsname(string goodsName)
    {
        if (string.IsNullOrWhiteSpace(goodsName))
        {
            MessageBox.Show("商品名称不能为空");
            return new List<Order>();
        }

        const string sql = "SELECT * FROM orders WHERE order_goodsName LIKE @goodsName";
        var orders = new List<Order>();
        
        try
        {
            DatabaseHelper.ExecuteReader(sql, reader => {
                while (reader.Read())
                {
                    var orderDetails = new OrderDetails(
                        reader.GetString("order_customerName"),
                        reader.GetString("order_goodsName"),
                        reader.GetInt32("order_price"),
                        reader.GetDateTime("order_date")
                    );
                    orders.Add(new Order(reader.GetInt32("order_ID"), orderDetails));
                }
                
                if (orders.Count == 0)
                {
                    MessageBox.Show("未找到相关订单");
                }
                else
                {
                    MessageBox.Show($"找到 {orders.Count} 个相关订单");
                }
            }, parameters => {
                parameters.AddWithValue("@goodsName", $"%{goodsName}%");
            });
        }
        catch (Exception ex)
        {
            MessageBox.Show($"查询订单失败: {ex.Message}");
        }
        
        return orders;
    }
    
    // 根据客户名称查询订单
    public List<Order> FindAOrderByCustomer(string customerName)
    {
        if (string.IsNullOrWhiteSpace(customerName))
        {
            MessageBox.Show("客户名称不能为空");
            return new List<Order>();
        }

        const string sql = "SELECT * FROM orders WHERE order_customerName LIKE @customerName";
        var orders = new List<Order>();
        
        try
        {
            DatabaseHelper.ExecuteReader(sql, reader => {
                while (reader.Read())
                {
                    var orderDetails = new OrderDetails(
                        reader.GetString("order_customerName"),
                        reader.GetString("order_goodsName"),
                        reader.GetInt32("order_price"),
                        reader.GetDateTime("order_date")
                    );
                    orders.Add(new Order(reader.GetInt32("order_ID"), orderDetails));
                }
                
                if (orders.Count == 0)
                {
                    MessageBox.Show("未找到相关订单");
                }
                else
                {
                    MessageBox.Show($"找到 {orders.Count} 个相关订单");
                }
            }, parameters => {
                parameters.AddWithValue("@customerName", $"%{customerName}%");
            });
        }
        catch (Exception ex)
        {
            MessageBox.Show($"查询订单失败: {ex.Message}");
        }
        
        return orders;
    }

    // 查询所有订单
    public List<Order> GetAllOrders()
    {
        const string sql = "SELECT * FROM orders";
        var orders = new List<Order>();
        
        try
        {
            DatabaseHelper.ExecuteReader(sql, reader => {
                while (reader.Read())
                {
                    var orderDetails = new OrderDetails(
                        reader.GetString("order_customerName"),
                        reader.GetString("order_goodsName"),
                        reader.GetInt32("order_price"),
                        reader.GetDateTime("order_date")
                    );
                    orders.Add(new Order(reader.GetInt32("order_ID"), orderDetails));
                }
                
                if (orders.Count == 0)
                {
                    MessageBox.Show("订单列表为空");
                }
            }, null);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"查询所有订单失败: {ex.Message}");
        }
        
        return orders;
    }

    // 修改订单
    public void UpdateOrder(int orderId, string customerName, string goodsName)
    {
        if (orderId <= 0)
        {
            MessageBox.Show("订单ID无效");
            return;
        }
        
        if (string.IsNullOrWhiteSpace(customerName) || string.IsNullOrWhiteSpace(goodsName))
        {
            MessageBox.Show("客户名称和商品名称不能为空");
            return;
        }

        const string sql = @"UPDATE orders 
                          SET order_customerName = @name, order_goodsName = @product 
                          WHERE order_ID = @id";
                          
        try
        {
            DatabaseHelper.ExecuteNonQuery(sql, parameters => {
                parameters.AddWithValue("@name", customerName);
                parameters.AddWithValue("@product", goodsName);
                parameters.AddWithValue("@id", orderId);
            });
            
            // 验证是否修改成功
            const string checkSql = "SELECT COUNT(*) FROM orders WHERE order_ID = @id";
            var count = DatabaseHelper.ExecuteScalar<int>(checkSql, parameters => {
                parameters.AddWithValue("@id", orderId);
            });
            
            if (count > 0)
            {
                MessageBox.Show("修改订单成功");
            }
            else
            {
                MessageBox.Show("未查询到相关订单，修改失败");
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"修改订单失败: {ex.Message}");
        }
    }
    
    // 重命名方法，与Form5中的调用匹配
    public void ChangeAOrderById(int orderId, string customerName, string goodsName)
    {
        UpdateOrder(orderId, customerName, goodsName);
    }
}