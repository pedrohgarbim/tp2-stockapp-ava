using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockApp.Application.DTOs.Projects;
using StockApp.Application.Interfaces;

namespace StockApp.Application.Services
{
    public class ProjectManagementService : IProjectManagementService
    {
        public async Task<ProjectDto> CreateProjectAsync(CreateProjectDto createProjectDto)
        {
            return new ProjectDto
            {
                Id = 1,
                Name = createProjectDto.Name,
                Description = createProjectDto.Description,
                StartDate = createProjectDto.StartDate,
                EndDate = createProjectDto.EndDate,
            };
        }
    }
}
