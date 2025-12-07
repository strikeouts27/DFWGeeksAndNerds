namespace ContactManager.Models
{
    public interface IContactRepository
    {
        IEnumerable<Contact> GetAllContacts();
        Contact? GetContactById(int id);
        void AddContact(Contact contact);
        void UpdateContact(Contact contact);
        void DeleteContact(int id);
        int Count();
    }
}
