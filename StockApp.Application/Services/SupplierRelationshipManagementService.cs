using Microsoft.IdentityModel.Tokens;
using StockApp.Application.DTOs;
using StockApp.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockApp.Application.Services
{
    public class SupplierRelationshipManagementService : ISupplierRelationshipManagementService
    {
        private readonly List<SupplierDto> _suppliers;

        public async Task<SupplierDto> EvaluateSupplierAsync(int supplierId)
        {
            var supplier = _suppliers.FirstOrDefault(s => s.Id == supplierId);
            if (supplier == null)
            {
                throw new ArgumentException("Supplier not found.");
            }

            supplier.EvaluationScore = new Random().Next(60,100);

            return await Task.FromResult(supplier);
        }

        public async Task<bool> RenewContractAsync(int supplierId)
        {
            var supplier = _suppliers.FirstOrDefault(s => s.Id == supplierId);
            if (supplier == null)
            {
                throw new ArgumentException("Supplier not found.");
            }

            if (supplier.EvaluationScore >= 75)
            {
                return await Task.FromResult(true);
            }

            return await Task.FromResult(false);
        }
    }
}
