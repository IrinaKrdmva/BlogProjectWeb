using BlogProjectWeb.Data;
using BlogProjectWeb.Models.Domain;

namespace BlogProjectWeb.Repositories {
    public class BlogPostRepository : IBlogPostRepository{

        private readonly BlogDbContext blogDbContext;

        public BlogPostRepository(BlogDbContext blogDbContext) {
            this.blogDbContext = blogDbContext;
        }

        public async Task<BlogPost> AddAsync(BlogPost blogPost) {
            await blogDbContext.AddAsync(blogPost);
            await blogDbContext.SaveChangesAsync();
            return blogPost;
        }

        public Task<BlogPost?> DeleteAsync(Guid id) {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<BlogPost>> GetAllAsync() {
            throw new NotImplementedException();
        }

        public Task<BlogPost?> GetAsync(Guid id) {
            throw new NotImplementedException();
        }

        public Task<BlogPost?> UpdateAsync(BlogPost blogPost) {
            throw new NotImplementedException();
        }
    }
}
