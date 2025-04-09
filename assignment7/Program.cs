namespace Week6;

using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;

public static class Program
{
    public static OrderService orderService = new OrderService();

    [STAThread]
    static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        
        // 确保数据库表存在
        EnsureOrdersTableExists();

        // 添加示例订单
        AddSampleOrders();

        Application.Run(new Form1());

        // 程序结束时清空订单表
        ClearOrdersTable();
    }
    
    private static void EnsureOrdersTableExists()
    {
        const string createTableSql = @"
            CREATE TABLE IF NOT EXISTS orders (
                order_ID INT PRIMARY KEY,
                order_customerName VARCHAR(100) NOT NULL,
                order_goodsName VARCHAR(100) NOT NULL,
                order_price INT NOT NULL,
                order_date DATETIME NOT NULL
            )";
            
        try
        {
            DatabaseHelper.ExecuteNonQuery(createTableSql);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"创建订单表失败: {ex.Message}");
            Environment.Exit(1);
        }
    }
    
    private static void AddSampleOrders()
    {
        try
        {
            orderService.AddOrder("李", "牛奶");
            orderService.AddOrder("王", "书");
            orderService.AddOrder("高", "鸡蛋");
            orderService.AddOrder("赵", "纸片");
        }
        catch (Exception ex)
        {
            MessageBox.Show($"添加示例订单失败: {ex.Message}");
        }
    }
    
    private static void ClearOrdersTable()
    {
        const string sql = "TRUNCATE TABLE orders";
        try
        {
            DatabaseHelper.ExecuteNonQuery(sql);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"清空订单表失败: {ex.Message}");
        }
    }
}