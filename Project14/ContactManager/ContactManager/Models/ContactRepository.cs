namespace ContactManager.Models
{
    public class ContactRepository : IContactRepository
    {
        private readonly List<Contact> _contacts;
        private int _nextId = 1;

        public ContactRepository()
        {
            _contacts = new List<Contact>
            {
                new Contact 
                { 
                    ContactId = _nextId++, 
                    FirstName = "John", 
                    LastName = "Doe", 
                    Phone = "555-1234", 
                    Email = "john.doe@example.com",
                    Organization = "Acme Corp"
                },
                new Contact 
                { 
                    ContactId = _nextId++, 
                    FirstName = "Jane", 
                    LastName = "Smith", 
                    Phone = "555-5678", 
                    Email = "jane.smith@example.com",
                    Organization = "Tech Solutions"
                },
                new Contact 
                { 
                    ContactId = _nextId++, 
                    FirstName = "Bob", 
                    LastName = "Johnson", 
                    Phone = "555-9012", 
                    Email = "bob.johnson@example.com",
                    Organization = "Global Industries"
                }
            };
        }

        public IEnumerable<Contact> GetAllContacts()
        {
            return _contacts.OrderBy(c => c.LastName).ThenBy(c => c.FirstName);
        }

        public Contact? GetContactById(int id)
        {
            return _contacts.FirstOrDefault(c => c.ContactId == id);
        }

        public void AddContact(Contact contact)
        {
            contact.ContactId = _nextId++;
            _contacts.Add(contact);
        }

        public void UpdateContact(Contact contact)
        {
            var existingContact = GetContactById(contact.ContactId);
            if (existingContact != null)
            {
                existingContact.FirstName = contact.FirstName;
                existingContact.LastName = contact.LastName;
                existingContact.Phone = contact.Phone;
                existingContact.Email = contact.Email;
                existingContact.Organization = contact.Organization;
            }
        }

        public void DeleteContact(int id)
        {
            var contact = GetContactById(id);
            if (contact != null)
            {
                _contacts.Remove(contact);
            }
        }

        public int Count()
        {
            return _contacts.Count;
        }
    }
}
