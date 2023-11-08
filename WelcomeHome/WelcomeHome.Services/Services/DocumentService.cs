﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WelcomeHome.DAL.Models;
using WelcomeHome.DAL.UnitOfWork;
using WelcomeHome.Services.DTO;

namespace WelcomeHome.Services.Services
{
    public sealed class DocumentService : IDocumentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DocumentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        //TODO: Add exceptions
        //TODO: Add stedDocument repository
        //TODO: Add getByStep methods

        public async Task<DocumentOutDTO> GetAsync(Guid id)
        {
            var foundDocument = await _unitOfWork.DocumentRepository.GetByIdAsync(id).ConfigureAwait(false);
            return _mapper.Map<DocumentOutDTO>(foundDocument);
        }

        public IEnumerable<DocumentOutDTO> GetAll()
        {
            return _unitOfWork.DocumentRepository.GetAll()
                                                 .Select(d => _mapper.Map<DocumentOutDTO>(d));
        }

        public async Task AddAsync(DocumentInDTO newDocument)
        {
            await _unitOfWork.DocumentRepository.AddAsync(_mapper.Map<Document>(newDocument)).ConfigureAwait(false);
        }

        public async Task UpdateAsync(DocumentInDTO updatedDocument)
        {
            await _unitOfWork.DocumentRepository.UpdateAsync(_mapper.Map<Document>(updatedDocument)).ConfigureAwait(false);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _unitOfWork.DocumentRepository.DeleteAsync(id).ConfigureAwait(false);
        }
    }
}
