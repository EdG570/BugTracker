using BugTracker.Core.Interfaces;
using BugTracker.Core.Models;
using BugTracker.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.Infrastructure.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly IdentityAppContext _context;

        public CommentRepository(IdentityAppContext context)
        {
            _context = context;
        }

        public async Task<int> Create(Comment entity)
        {
            await _context.Comments.AddAsync(entity);
            await SaveChanges();

            return 1;
        }

        public async Task<int> Delete(int id)
        {
            var comment = await _context.Comments.FindAsync(id);
            _context.Comments.Remove(comment);
            await SaveChanges();

            return 1;
        }

        public async Task<Comment> FindOne(int id)
        {
            return await _context.Comments.FindAsync(id);
        }

        public async Task<IEnumerable<Comment>> GetAll()
        {
            return await _context.Comments.ToListAsync();
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<int> Update(Comment entity)
        {
            var comment = await _context.Comments.FindAsync(entity.Id);
            _context.Entry(comment).CurrentValues.SetValues(entity);
            await SaveChanges();

            return 1;
        }
    }
}
