using Application.Abstractions;
using Domain;
using Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    public class BrandRepository : IRepository<BrandEntity>
    {
        private StoreContext _context;
        public BrandRepository(StoreContext context)
        {
            _context = context;
        }
    }
}
