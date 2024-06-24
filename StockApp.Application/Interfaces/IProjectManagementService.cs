using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockApp.Application.DTOs.Projects;

namespace StockApp.Application.Interfaces
{
    public interface IProjectManagementService
    {
        Task<ProjectDto> CreateProjectAsync(CreateProjectDto createProjectDto);
    }
}
