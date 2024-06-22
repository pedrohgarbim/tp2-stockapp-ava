using Microsoft.AspNetCore.Mvc;
using StockApp.Application.Interfaces;

namespace StockApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BackupController : ControllerBase
    {
        private readonly IBackupService _backupService;

        public BackupController(IBackupService backupService)
        {
            _backupService = backupService;
        }

        [HttpPost("perform-backup")]
        public IActionResult PerformBackUp()
        {
            _backupService.PerformBackup(); 
            return Ok("Backup realizado com sucesso.");
        }
    }
}
