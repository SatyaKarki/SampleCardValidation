using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Card.Model
{
    public abstract class BaseEntity
    {
        public bool Active { get; set; }
        public long Author { get; set; }
        public DateTime Created { get; set; }
        public long Editor { get; set; }
        public DateTime Modified { get; set; }
    }
}
