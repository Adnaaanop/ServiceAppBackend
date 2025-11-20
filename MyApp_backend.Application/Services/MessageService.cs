using AutoMapper;
using MyApp_backend.Application.DTOs.Message;
using MyApp_backend.Application.Interfaces;
using MyApp_backend.Domain.Entities;
using MyApp_backend.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyApp_backend.Application.Services
{
    public class MessageService : IGenericService<MessageCreateDto, object, MessageResponseDto, Message>
    {
        private readonly IGenericRepository<Message> _repo;
        private readonly IMapper _mapper;
        private readonly ICloudinaryService _cloudinaryService;

        public MessageService(
            IGenericRepository<Message> repo,
            IMapper mapper,
            ICloudinaryService cloudinaryService // Inject Cloudinary service
        )
        {
            _repo = repo;
            _mapper = mapper;
            _cloudinaryService = cloudinaryService;
        }

        public async Task<MessageResponseDto> CreateAsync(MessageCreateDto dto)
        {
            var message = _mapper.Map<Message>(dto);
            message.Timestamp = DateTime.UtcNow;

            // Handle file upload if present
            if (dto.MediaFile != null && dto.MediaFile.Length > 0)
            {
                var url = await _cloudinaryService.UploadImageAsync(dto.MediaFile);
                message.MediaUrl = url;
            }

            var created = await _repo.AddAsync(message);
            return _mapper.Map<MessageResponseDto>(created);
        }

        public async Task<MessageResponseDto?> GetByIdAsync(Guid id)
        {
            var message = await _repo.GetByIdAsync(id);
            if (message == null) return null;
            return _mapper.Map<MessageResponseDto>(message);
        }

        public async Task<IEnumerable<MessageResponseDto>> GetAllAsync()
        {
            var messages = await _repo.GetAllAsync();
            return messages.Select(m => _mapper.Map<MessageResponseDto>(m));
        }

        public Task<MessageResponseDto?> UpdateAsync(Guid id, object dto)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _repo.DeleteAsync(id);
        }

        public async Task<IEnumerable<MessageResponseDto>> GetMessagesByBookingAsync(Guid bookingId)
        {
            var messages = await _repo.FindAsync(m => m.BookingId == bookingId);
            return messages
                   .OrderBy(m => m.Timestamp)
                   .Select(m => _mapper.Map<MessageResponseDto>(m));
        }
    }
}
