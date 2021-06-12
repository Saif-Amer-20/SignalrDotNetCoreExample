using System.Collections.Generic;

namespace GlobalMarketsCore.Models
{
    public class HomePageModel
    {
        public ArticleModel Welcome { get; set; }
        public IEnumerable<ArticleModel> Accounts { get; set; }
        public IEnumerable<ArticleModel> Slider { get; set; }
        public IEnumerable<ArticleModel> Tabarts { get; set; }
        public string Country { get; set; }
        public string Prefix { get; set; }
    }
}