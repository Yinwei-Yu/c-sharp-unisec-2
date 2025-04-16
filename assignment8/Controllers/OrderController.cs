// Controllers/OrderController.cs
using Microsoft.AspNetCore.Mvc;
using OrderManagementAPI.Models;
using OrderManagementAPI.Services;
using System.Collections.Generic;

namespace OrderManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly OrderService _orderService;

        public OrderController(OrderService orderService)
        {
            _orderService = orderService;
        }

        // 添加订单
        [HttpPost]
        public IActionResult AddOrder([FromBody] Order order)
        {
            _orderService.AddOrder(order);
            return Ok();
        }

        // 删除订单
        [HttpDelete("{orderId}")]
        public IActionResult DeleteOrder(int orderId)
        {
            _orderService.DeleteOrder(orderId);
            return Ok();
        }

        // 根据订单 ID 查询订单
        [HttpGet("{orderId}")]
        public IActionResult FindAOrderById(int orderId)
        {
            var order = _orderService.FindAOrderById(orderId);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }

        // 根据商品名称查询订单
        [HttpGet("goods/{goodsName}")]
        public IActionResult FindAOrderByGoodsname(string goodsName)
        {
            var orders = _orderService.FindAOrderByGoodsname(goodsName);
            return Ok(orders);
        }

        // 根据客户名称查询订单
        [HttpGet("customer/{customerName}")]
        public IActionResult FindAOrderByCustomer(string customerName)
        {
            var orders = _orderService.FindAOrderByCustomer(customerName);
            return Ok(orders);
        }

        // 查询所有订单
        [HttpGet]
        public IActionResult GetAllOrders()
        {
            var orders = _orderService.GetAllOrders();
            return Ok(orders);
        }

        // 修改订单
        [HttpPut("{orderId}")]
        public IActionResult UpdateOrder(int orderId, [FromBody] Order order)
        {
            _orderService.UpdateOrder(orderId, order);
            return Ok();
        }
    }
}