using EPGDataAccess.AddItems;
using EPGDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface ICommentRepository
    {
        public IQueryable<Comment> GetComments();
        public Comment GetComment(int id);
        public IQueryable<Comment> GetResponses(int id);
        public void CreateComment(Comment4Create Data);
        public void UpdateComment(Comment4Create Data, int id);
        public void DeleteComment(int id);
    }
}
