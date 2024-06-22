using Microsoft.EntityFrameworkCore;
using StockApp.Domain.Entities;
using StockApp.Domain.Interfaces;
using StockApp.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockApp.Infra.Data.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _context;

        public EmployeeRepository(ApplicationDbContext context)
        {
            _context = context;
        }   

        public async Task<Employee> GetByIdAsync(int employeeId)
        {
            return await _context.Employees
                .Include(e => e.Evaluations)
                .FirstOrDefaultAsync(e => e.Id == employeeId);
        }

        public async Task AddEvaluationAsync(EmployeeEvaluation evaluation)
        {
            _context.EmployeeEvaluation.Add(evaluation);
            await _context.SaveChangesAsync();  
        }
    }
}
