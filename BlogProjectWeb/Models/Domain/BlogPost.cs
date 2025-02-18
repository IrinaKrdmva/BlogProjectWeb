namespace BlogProjectWeb.Models.Domain {
    public class BlogPost {
        public Guid Id { get; set; }
        public string Heading { get; set; } //To make nullable property add ? to the type
        public string PageTitle { get; set; }
        public string Content { get; set; }
        public string ShortDescription { get; set; }
        public string FeaturedImageUrl { get; set; }
        public string UrlHandle { get; set; }
        public DateTime PublishedDate { get; set; }
        public string Author { get; set; }
        public bool Visible { get; set; }

        //Many-to-many relationship / Navigation property
        public ICollection<Tag> Tags { get; set; }
    }
}
