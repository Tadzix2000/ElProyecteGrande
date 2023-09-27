using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPGDataAccess;
using AutoMapper;

namespace EPGApplication.Repositories.NormalRepositories
{
    public class MainRepository
    {
        public DataInstance Instance;
        public IMapper Mapper { get; set; }
        public MainRepository(DataInstance instance, IMapper mapper)
        {
            Instance = instance;
            Mapper = mapper;
        }
    }
}
