using Application.Interfaces;
using Syfora_Test.Models;
using System.Xml.Linq;
using XmlData;

namespace Application.DataProviders
{
    public class XmlUserService : IUserService
    {        
        private readonly XmlUserReader _xmlReader;

        public XmlUserService(XmlUserReader xmlReader) 
        {
            _xmlReader = xmlReader;
        }

        public async Task<List<UserDto>> GetAllUsersAsync()
        {
            return await Task.Run(() =>
            {
                var doc = _xmlReader.LoadDocument();
                var users = doc.Root.Elements("User")
                    .Select(x => new UserDto
                    {
                        Id = Guid.Parse(x.Element("Id")?.Value ?? Guid.Empty.ToString()),
                        Login = x.Element("Login")?.Value,
                        FirstName = x.Element("FirstName")?.Value,
                        LastName = x.Element("LastName")?.Value
                    }).ToList();
                return users;
            });
        }

        public async Task<UserDto?> GetUserAsync(Guid id)
        {
            return await Task.Run(() =>
            {
                var doc = _xmlReader.LoadDocument();
                var userElement = doc.Root.Elements("User")
                    .FirstOrDefault(x => Guid.Parse(x.Element("Id")?.Value ?? Guid.Empty.ToString()) == id);
                if (userElement == null)
                    return null;

                return new UserDto
                {
                    Id = id,
                    Login = userElement.Element("Login")?.Value,
                    FirstName = userElement.Element("FirstName")?.Value,
                    LastName = userElement.Element("LastName")?.Value
                };
            });
        }

        public async Task<UserDto> AddUserAsync(UserDto user)
        {
            return await Task.Run(() =>
            {
                var doc = _xmlReader.LoadDocument();
                var id = Guid.NewGuid();
                var newUser = new XElement("User",
                    new XElement("Id", id),
                    new XElement("Login", user.Login),
                    new XElement("FirstName", user.FirstName),
                    new XElement("LastName", user.LastName)
                );

                doc.Root.Add(newUser);
                _xmlReader.SaveDocument(doc);

                user.Id = id;
                return user;
            });
        }

        public async Task UpdateUserAsync(UserDto user)
        {
            await Task.Run(() =>
            {
                var doc = _xmlReader.LoadDocument();

                var userElement = doc.Root.Elements("User")
                    .FirstOrDefault(x => Guid.Parse(x.Element("Id")?.Value ?? Guid.Empty.ToString()) == user.Id);

                userElement.SetElementValue("Login", user.Login);
                userElement.SetElementValue("FirstName", user.FirstName);
                userElement.SetElementValue("LastName", user.LastName);

                _xmlReader.SaveDocument(doc);
            });
        }

        public async Task DeleteUserAsync(Guid id)
        {
            await Task.Run(() =>
            {
                var doc = _xmlReader.LoadDocument();

                var userElement = doc.Root.Elements("User")
                    .FirstOrDefault(x => Guid.Parse(x.Element("Id")?.Value ?? Guid.Empty.ToString()) == id);
                if (userElement != null)
                {
                    userElement.Remove();
                    _xmlReader.SaveDocument(doc);
                }
            });
        }

        public async Task<bool> IsLoginExist(string login)
        {
            if (string.IsNullOrWhiteSpace(login))
                return false;

            return await Task.Run(() =>
            {
                var doc = _xmlReader.LoadDocument();
                return doc.Root.Elements("User")
                    .Any(x => string.Equals(x.Element("Login")?.Value, login, StringComparison.OrdinalIgnoreCase));
            });
        }
    }
}
