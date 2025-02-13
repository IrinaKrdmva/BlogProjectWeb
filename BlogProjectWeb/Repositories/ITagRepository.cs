using BlogProjectWeb.Models.Domain;

namespace BlogProjectWeb.Repositories {
    public interface ITagRepository {
        //Get all tags
        Task<IEnumerable<Tag>> GetAllAsync();

        //Get one tag (can return null that is why we add the ?)
        Task<Tag?> GetAsync(Guid id);

        //Add tag
        Task<Tag> AddAsync(Tag tag);

        //Update tag
        Task<Tag?> UpdateAsync(Tag tag);

        //Delete tag
        Task<Tag?> DeleteAsync(Guid id);
    }
}
