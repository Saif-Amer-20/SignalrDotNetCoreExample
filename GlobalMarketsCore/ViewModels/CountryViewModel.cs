using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GlobalMarketsCore.ViewModels
{
    public class CountryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int LanguageId { get; set; }
        public string LanguageName { get; set; }
    }
}
