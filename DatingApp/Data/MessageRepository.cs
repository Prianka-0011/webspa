using AutoMapper;
using AutoMapper.QueryableExtensions;
using DatingApp.DTOs;
using DatingApp.Entities;
using DatingApp.Helpers;
using DatingApp.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.Data
{
    public class MessageRepository : IMessageRepository
    {
        private readonly  DataContext _context;
        private readonly IMapper mapper;
        public MessageRepository(DataContext context, IMapper mapper)
        {
            this._context = context;
            this.mapper = mapper;
        }
        public void AddMessage(Message message)
        {
           this._context.Add(message);
        }

        public Task<Message> GetMessage()
        {
            throw new System.NotImplementedException();
        }

        public async Task<PagedList<MessageDto>> GetMessagesForUser(MessageParams messageParams)
        {
            var query = _context.Messages.OrderByDescending(x => x.MessageSent).AsQueryable();
            query = messageParams.Container switch
            {
                "Inbox" => query.Where(u=>u.RecepientUserName == messageParams.Username),
                "Outnox" => query.Where(u=>u.SenderUserName == messageParams.Username),
                _=>query.Where(u=>u.RecepientUserName == messageParams.Username && u.DateRead==null)
            };
            var messages = query.ProjectTo<MessageDto>(mapper.ConfigurationProvider);

            return await PagedList<MessageDto>.CreatAsync(messages, messageParams.PageNumber, messageParams.PageSize);
           
        }

        public async Task<IEnumerable<MessageDto>> GetMessageThreadAsync(string currentUserName, string recepientUserName)
        {
            var messages = await _context.Messages.Include(c => c.Sender).ThenInclude(c => c.Photos)
                .Include(c => c.Recepient).ThenInclude(c => c.Photos)
                .Where(m => m.RecepientUserName == currentUserName && m.SenderUserName == recepientUserName ||
                m.SenderUserName == recepientUserName && m.SenderUserName == currentUserName
                ).OrderBy(m => m.MessageSent).ToListAsync();
          var unReadMessages = messages.Where(m => m.DateRead == null && m.RecepientUserName == currentUserName).ToList();
            if (unReadMessages.Any())
            {
                foreach (var message in unReadMessages)
                {
                    message.DateRead = DateTime.Now;

                }
                await _context.SaveChangesAsync();
            }
            return mapper.Map<IEnumerable<MessageDto>>(messages);
        }

        public void RemoveMessage(Message message)
        {
            this._context.Remove(message);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await this._context.SaveChangesAsync()>0;
        }
    }
}
