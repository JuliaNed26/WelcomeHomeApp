using AutoMapper;
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

        public async Task<DocumentOutDTO> GetAsync(Guid id)
        {
            var foundDocument = await _unitOfWork.DocumentRepository.GetAsync(id).ConfigureAwait(false);
            return _mapper.Map<DocumentOutDTO>(foundDocument);
        }

        public IEnumerable<DocumentOutDTO> GetAll()
        {
            var documents = _unitOfWork.DocumentRepository.GetAll();

            var result = documents.Select(d => _mapper.Map<DocumentOutDTO>(d));

            return result;
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
