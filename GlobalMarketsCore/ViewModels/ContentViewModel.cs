using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GlobalMarketsCore.ViewModels
{
    public class ContentViewModel
    {
        public int Id { get; set; }
        public string Cont { get; set; }
        public int ContentPlaceId { get; set; }
        public string ContentPlace { get; set; }
        public int WidgetId { get; set; }
        public string Widget { get; set; }
        public int LanguageId { get; set; }
        public string Language { get; set; }
       
    }
}
