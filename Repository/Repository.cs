using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPGDataAccess;

namespace Repository
{
    public class Repository
    {
        public DataInstance Instance = new ();
        public IMapper Mapper { get; set; }
    }
}
