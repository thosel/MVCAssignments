using MVCAssignments.Models;
using System.Collections.Generic;

namespace MVCAssignments.Services
{
    public interface ILanguagesService
    {
        public List<Language> Read();

        public List<Language> FindLanguages(string searchString, bool caseSensitive);

        public Language FindLanguage(int id);

        public void CreateLanguage(Language language);

        public void DeleteLanguage(int id);
    }
}
