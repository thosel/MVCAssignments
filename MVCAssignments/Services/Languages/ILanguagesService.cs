using MVCAssignments.Models;
using System.Collections.Generic;

namespace MVCAssignments.Services
{
    public interface ILanguagesService
    {
        public void CreateLanguage(Language language);

        public List<Language> Read();

        public List<Language> FindLanguages(string searchString, bool caseSensitive);

        public Language FindLanguage(int languageId);

        public Language FindLanguage(string languageName);

        public void UpdateLanguage(Language language);

        public void DeleteLanguage(int languageId);
    }
}
