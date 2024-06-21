using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockApp.Application.DTOs;
using StockApp.Domain.Entities;
namespace StockApp.Domain.Interfaces
{
    public interface ISupplierRelationshipManagementService
    {
        Task<SupplierDto> EvaluateSupplierAsync(int supplierId);
        Task<bool> RenewContractAsync(int supplierId);
    }
}


