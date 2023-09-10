using DatingApp.DTOs;
using DatingApp.Entities;
using DatingApp.Helpers;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DatingApp.Interfaces
{
    public interface IMessageRepository
    {
        void AddMessage(Message message);
        void RemoveMessage(Message message);
        Task<Message> GetMessage();
        Task<PagedList<MessageDto>> GetMessagesForUser(MessageParams messageParams);
        Task<IEnumerable<MessageDto>> GetMessageThreadAsync(string currentUserName, string recepientUserName);
        Task<bool> SaveAllAsync();
    }
}
