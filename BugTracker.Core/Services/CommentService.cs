using BugTracker.Core.Interfaces;
using BugTracker.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.Core.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepo;

        public CommentService(ICommentRepository commentRepo)
        {
            _commentRepo = commentRepo;
        }

        public async Task<int> Create(Comment entity)
        {
            return await _commentRepo.Create(entity);
        }

        public async Task<int> Delete(int id)
        {
            return await _commentRepo.Delete(id);
        }

        public async Task<Comment> FindOne(int id)
        {
            return await _commentRepo.FindOne(id);
        }

        public async Task<IEnumerable<Comment>> GetAll()
        {
            return await _commentRepo.GetAll();
        }

        public async Task<int> Update(Comment entity)
        {
            return await _commentRepo.Update(entity);
        }
    }
}
