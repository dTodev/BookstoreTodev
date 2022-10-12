using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Models.Models.Configurations
{
    public class KafkaSettings
    {
        public string BootstrapServers { get; set; }
        public int AutoOffsetReset { get; set; }
        public string GroupId { get; set; }
    }
}
