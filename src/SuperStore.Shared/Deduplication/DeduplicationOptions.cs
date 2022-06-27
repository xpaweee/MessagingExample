using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperStore.Shared.Deduplication
{
    public class DeduplicationOptions
    {
        public bool Enabled { get; set; }

        public string Interval { get; set; }
        public int MessageEvictionWindowInDays { get; set; }
    }
}
