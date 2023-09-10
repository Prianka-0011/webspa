using AutoMapper;
using DatingApp.DTOs;
using DatingApp.Entities;
using DatingApp.Extensions;
using DatingApp.Helpers;
using DatingApp.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DatingApp.Controllers
{
    public class MessageController : BaseController
    {
        private readonly IUserRepository userRepository;
        private readonly IMessageRepository messageRepository;
        private IMapper mapper;
        public MessageController(IUserRepository userRepository, IMessageRepository messageRepository,IMapper mapper)
        {
            this.userRepository = userRepository;
            this.messageRepository = messageRepository;
            this.mapper = mapper;
        }
        [HttpPost]
        public async Task<ActionResult<MessageDto>> CreateMessage(CreateMessageDto createMessageDto) 
        {
            var useanme= User.GetUserName();
            if(useanme == createMessageDto.RecepientUserName.ToLower())
            {
                return BadRequest("Can not send message to your self");
            }
            var sender = await userRepository.GetUserByNameAsync(useanme);
            var recepient = await userRepository.GetUserByNameAsync(createMessageDto.RecepientUserName);
            if(recepient == null)
            {
                return NotFound();
            }
            var message = new Message
            {
                Sender = sender,
                Recepient = recepient,
                SenderUserName = sender.UserName,
                RecepientUserName = recepient.UserName,
                Content = createMessageDto.Content
            };
            messageRepository.AddMessage(message);
            if(await messageRepository.SaveAllAsync())
            {
                return Ok(mapper.Map<MessageDto>(message));
            }
            return BadRequest("Faild to send message");
        }
        [HttpGet]
        public async Task<PagedList<MessageDto>> GetMessagesForUser([FromQuery] MessageParams messageParams)
        {
            messageParams.Username = User.GetUserName();
            var messages = await messageRepository.GetMessagesForUser(messageParams);
            Response.AddPaginationHeader(new PaginationHeader(messages.CurrentPage, messages.PageSize, messages.TotalCount, messages.TotalPage));
            return messages;
        }
      
    }
}
