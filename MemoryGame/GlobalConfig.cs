using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryGame {
    public static class GlobalConfig {
        public const string ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=MemoryDatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False;";
        
        private static readonly string _currentDirectory = Environment.CurrentDirectory;
        public static string ImageFilePath = _currentDirectory + @"\cardImages\";    
    }
}
