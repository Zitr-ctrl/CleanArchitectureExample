using Application.Abstractions;
using Domain;
using Data;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace Repository
{
    public class BrandRepository : IRepository<BrandEntity>
    {
        private StoreContext _context;
        public BrandRepository(StoreContext context)
        {
            _context = context;
        }
        public async Task<BrandEntity> GetByIdAsync(int id)
        {
            var brand = await _context.Brands.FirstOrDefaultAsync(b => b.Id == id);
            if (brand == null)
            {
                throw new KeyNotFoundException($"La marca no existe");
            }

            return MapToEntity(brand);
        }

        public async Task<IEnumerable<BrandEntity>> GetAllAsync()
        {
            var brands = await _context.Brands.ToListAsync();
            return brands.Select(MapToEntity);
        }

        public async Task AddAsync(BrandEntity brandEntity)
        {
            var brand = MapToModel(brandEntity);
            await _context.Brands.AddAsync(brand);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(BrandEntity brandEntity)
        {
            if (brandEntity.Id is null)
                throw new ArgumentException("Id requerido", nameof(brandEntity));

            var brand = await _context.Brands.FindAsync(brandEntity.Id);

            if (brand == null)
                throw new KeyNotFoundException($"la marca con el Id = {brandEntity.Id} no existe");

            brand.Name = brandEntity.Name;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var brand = await _context.Brands.FindAsync(id);
            if (brand == null)
                throw new KeyNotFoundException("La marca no existe");

            _context.Brands.Remove(brand);
            await _context.SaveChangesAsync();
        }

        #region Mappers
        private static BrandEntity MapToEntity(Brand model)
        {
            return new BrandEntity(model.Id, model.Name);
        }

        private static Brand MapToModel(BrandEntity entity)
        {
            return new Brand
            {
                Id = entity.Id ?? 0,
                Name = entity.Name

            };
        }
        #endregion

    }
}
