namespace GlobalMarketsCore.Models
{
    public class ArticleModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ImagePath { get; set; }
        public string Abstract { get; set; }
        public string ArticleCategory { get; set; }
        public string Owner { get; set; }
        public int LanguageId { get; set; }
        public int ArticleCategoryId { get; set; }
    }
}