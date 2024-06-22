using StockApp.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockApp.Application.Services
{
    public class IncrementalBackupService : IBackupService
    {
        private readonly string _backupDirectory = "backups";
        private readonly string _dataDirectory = "data";

        public void PerformBackup()
        {
            if (!Directory.Exists(_backupDirectory))
            {
                Directory.CreateDirectory(_backupDirectory);
            }

            var lastBackupTime = GetLastBackupTime();

            var files = Directory.GetFiles(_backupDirectory);

            foreach (var file in files)
            {
                var lastWriteTime = File.GetLastWriteTime(file);

                if (lastWriteTime > lastBackupTime)
                {
                    var fileName = Path.GetFileName(file);
                    var backupFilePath = Path.Combine(_backupDirectory, fileName);

                    File.Copy(file, backupFilePath, true );
                }
            }

            File.WriteAllText(Path.Combine(_backupDirectory, "last_backup.txt"), DateTime.Now.ToString());
        }

        private DateTime GetLastBackupTime()
        {
            var lastBackupFile = Path.Combine(_backupDirectory, "last_backup.txt");

            if (File.Exists(lastBackupFile))
            {
                var lastBackupTimeString = File.ReadAllText(lastBackupFile);
                return DateTime.Parse(lastBackupTimeString);
            }

            return DateTime.MinValue;
        }
    }
}
