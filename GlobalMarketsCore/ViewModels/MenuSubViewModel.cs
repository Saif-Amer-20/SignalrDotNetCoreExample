using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GlobalMarketsCore.ViewModels
{
    public class MenuSubViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Link { get; set; }
        public bool Show { get; set; }
        public int MainMenuId { get; set; }
        public string MainMenu { get; set; }
        public int LanguageId { get; set; }
        public string Language { get; set; }
    }
}
