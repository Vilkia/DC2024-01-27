﻿using AutoMapper;
using FluentValidation;
using Task310.DTO.RequestDTO;
using Task310.DTO.ResponseDTO;
using Task310.Exceptions;
using Task310.Infrastructure.Validators;
using Task310.Models;
using Task310.Repositories.Interfaces;
using Task310.Services.Interfaces;

namespace Task310.Services.Impementations
{
    public class NewsService(INewsRepository newsRepository,
        IMapper mapper, NewsRequestDtoValidator validator) : INewsService
    {
        private readonly INewsRepository _newsRepository = newsRepository;
        private readonly IMapper _mapper = mapper;
        private readonly NewsRequestDtoValidator _validator = validator;

        public async Task<IEnumerable<NewsResponseDto>> GetNewsAsync()
        {
            var news = await _newsRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<NewsResponseDto>>(news);
        }

        public async Task<NewsResponseDto> GetNewsByIdAsync(long id)
        {
            var news = await _newsRepository.GetByIdAsync(id)
                ?? throw new NotFoundException(ErrorMessages.NewsNotFoundMessage(id));
            return _mapper.Map<NewsResponseDto>(news);
        }

        public async Task<NewsResponseDto> CreateNewsAsync(NewsRequestDto news)
        {
            _validator.ValidateAndThrow(news);
            var newsToCreate = _mapper.Map<News>(news);
            newsToCreate.Created = DateTime.UtcNow;
            newsToCreate.Modified = DateTime.UtcNow;
            var createdNews = await _newsRepository.CreateAsync(newsToCreate);
            return _mapper.Map<NewsResponseDto>(createdNews);
        }

        public async Task<NewsResponseDto> UpdateNewsAsync(NewsRequestDto news)
        {
            _validator.ValidateAndThrow(news);
            var newsToUpdate = _mapper.Map<News>(news);
            newsToUpdate.Modified = DateTime.UtcNow;
            var updatedNews = await _newsRepository.UpdateAsync(newsToUpdate)
                ?? throw new NotFoundException(ErrorMessages.NewsNotFoundMessage(news.Id));
            return _mapper.Map<NewsResponseDto>(updatedNews);
        }

        public async Task DeleteNewsAsync(long id)
        {
            if (!await _newsRepository.DeleteAsync(id))
            {
                throw new NotFoundException(ErrorMessages.NewsNotFoundMessage(id));
            }
        }
    }
}
