using Microsoft.EntityFrameworkCore;
using NextgenInovasiTest.DatabaseContext;
using NextgenInovasiTest.Models;

namespace NextgenInovasiTest.BusinessLogic;

public class ItemBusinessLogic : IBusinessLogic<Item>
{
    private readonly ApplicationDbContext _context;

    public ItemBusinessLogic(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Item>> GetAllAsync(bool includeDeleted = false)
    {
        if (!includeDeleted)
        {
            return await _context.Items.ToListAsync();
        }
        return await _context.Items.Where(i => i.isActive == false).ToListAsync();
    }
    public async Task<Item> GetByIdAsync(int id, bool includeDeleted = false)
    {
        return await _context.Items
            .Where(i => i.Id == id && (includeDeleted || !i.isActive))
            .FirstOrDefaultAsync();
    }
    public async Task<Item> AddAsync(Item entity, string userId)
    {
        entity.isActive = true;
        entity.CreatedBy = userId;
        entity.CreatedAt = DateTime.Now;
        await _context.Items.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }
    public async Task<Item> UpdateAsync(Item entity, string userId)
    {
        entity.UpdatedBy = userId;
        _context.Items.Update(entity);
        await _context.SaveChangesAsync();
        return entity;
    }
    public async Task<int> SoftDeleteAsync(int id)
    {
        var item = await _context.Items.FindAsync(id);
        if (item == null)
        {
            return 0;
        }
        item.isActive = true;
        _context.Items.Update(item);
        return await _context.SaveChangesAsync();
    }
    public async Task HardDeleteAsync(int id)
    {
        var item = await _context.Items.FindAsync(id);
        if (item != null)
        {
            _context.Items.Remove(item);
            await _context.SaveChangesAsync();
        }
    }
    public async Task AddBacth(List<Item> entities, string userId)
    {
        foreach (var entity in entities)
        {
            entity.isActive = true;
            entity.CreatedAt = DateTime.Now;
            entity.CreatedBy = userId;
        }
        await _context.Items.AddRangeAsync(entities);
        await _context.SaveChangesAsync();
    }
}
