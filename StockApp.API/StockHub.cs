﻿using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace StockApp.API.Hubs
{
    public class StockHub : Hub
    {
        public async Task SendStockUpdate(string productId, int newStock)
        {
            await Clients.All.SendAsync("ReceiveStockUpdate", productId, newStock);
        }
    }
}