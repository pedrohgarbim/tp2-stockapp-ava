using StockApp.Application.DTOs;
using StockApp.Application.Interfaces;
using StockApp.Domain.Entities;
using StockApp.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockApp.Application.Services
{
    public class EmployeePerformanceEvaluationService : IEmployeePerformanceEvaluationService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeePerformanceEvaluationService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<EmployeeEvaluationDto> EvaluatePerformanceAsync(int employeeId)
        {
            var employee = await _employeeRepository.GetByIdAsync(employeeId);
            if (employee == null)
            {
                return null;
            }

            var evaluationScore = 85;
            var feedback = "Excelente!";

            var evaluation = new EmployeeEvaluation
            {
                EmployeeId = employeeId,
                EvaluationScore = evaluationScore,
                FeedBack = feedback

            };

            await _employeeRepository.AddEvaluationAsync(evaluation);

            return new EmployeeEvaluationDto
            {
                EmployeeId = employeeId,
                EvaluationScore = evaluationScore,
                Feedback = feedback
            };

        }
    }
}
