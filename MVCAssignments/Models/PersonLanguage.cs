namespace MVCAssignments.Models
{
    public class PersonLanguage
    {
        public PersonLanguage() { }

        public PersonLanguage(int personId, int languageId)
        {
            PersonId = personId;
            LanguageId = languageId;
        }

        public int PersonId { get; set; }
        public Person Person { get; set; }

        public int LanguageId { get; set; }
        public Language Language { get; set; }
    }
}
