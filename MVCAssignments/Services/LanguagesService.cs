using MVCAssignments.Models;
using MVCAssignments.Models.Data;
using System.Collections.Generic;
using System.Linq;

namespace MVCAssignments.Services
{
    public class LanguagesService : ILanguagesService
    {
        private readonly MVCAssignmentsContext db;

        public LanguagesService(MVCAssignmentsContext db)
        {
            this.db = db;
        }

        public List<Language> Read()
        {
            return db.Languages.ToList();
        }

        public List<Language> FindLanguages(string searchString, bool caseSensitive)
        {
            List<Language> languagesToReturn = new List<Language>();
            List<Language> languages = db.Languages.Where(language =>
                language.Name.Contains(searchString)
                ).ToList();

            if (caseSensitive)
            {
                foreach (var language in languages)
                {
                    if (language.Name.Contains(searchString))
                    {
                        languagesToReturn.Add(language);
                    }
                }
            }
            else
            {
                languagesToReturn = languages;
            }

            return languagesToReturn;
        }

        public Language FindLanguage(int id)
        {
            return db.Languages.Find(id);
        }

        public void CreateLanguage(Language language)
        {
            db.Languages.Add(language);
            db.SaveChanges();
        }

        public void DeleteLanguage(int id)
        {
            db.Languages.Remove(FindLanguage(id));
            db.SaveChanges();
        }
    }
}
