using AutoMapper;
using Moq;
using WelcomeHome.DAL.Models;
using WelcomeHome.DAL.Repositories;
using WelcomeHome.DAL.UnitOfWork;
using WelcomeHome.Services.DTO;
using WelcomeHome.Services.Exceptions;
using WelcomeHome.Services.Exceptions.ExceptionHandlerMediator;
using WelcomeHome.Services.Services;

namespace WelcomeHome.Services.Tests.Documents
{
    [TestFixture]
    public class DocumentServiceTests
    {
        private DocumentService _documentService;
        private Mock<IDocumentRepository> _mockDocumentRepository;
        private Mock<IUnitOfWork> _mockUnitOfWork;
        private Mock<IMapper> _mockMapper;
        private ExceptionHandlerMediator _exceptionHandlerMediator;
        private static readonly Guid Guid = Guid.NewGuid();
        private static readonly Document Document = new Document
        {
            Id = Guid,
            Name = "document1",
            StepDocuments = new List<StepDocument>()
           {
            new StepDocument { StepId = Guid.NewGuid(), DocumentId = Guid, ToReceive = false },
            new StepDocument { StepId = Guid.NewGuid(), DocumentId = Guid, ToReceive = true }
            }
        };

        [SetUp]
        public void Setup()
        {
            _mockDocumentRepository = new Mock<IDocumentRepository>();
            _mockMapper = new Mock<IMapper>();
            _exceptionHandlerMediator = new ExceptionHandlerMediator();

            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockUnitOfWork.Setup(uow => uow.DocumentRepository).Returns(_mockDocumentRepository.Object);

            _documentService = new DocumentService(_mockUnitOfWork.Object, _mockMapper.Object, _exceptionHandlerMediator);
        }

        [Test]
        public async Task GetAsync_WhenDocumentExists_ReturnsDocumentOutDTO()
        {
            // Arrange
            var documentOutDto = new DocumentOutDTO { Id = Document.Id, Name = Document.Name };

            _mockUnitOfWork.Setup(uow => uow.DocumentRepository.GetByIdAsync(Guid)).ReturnsAsync(Document);
            _mockMapper.Setup(mapper => mapper.Map<DocumentOutDTO>(Document)).Returns(documentOutDto);

            // Act
            var result = await _documentService.GetAsync(Guid);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Id, Is.EqualTo(documentOutDto.Id));
            Assert.That(result.Name, Is.EqualTo(documentOutDto.Name));
        }

        [Test]
        public void GetAsync_WhenDocumentDoesNotExist_ThrowsRecordNotFoundException()
        {
            // Arrange
            Guid nonExistentId = Guid.NewGuid();

            _mockUnitOfWork.Setup(uow => uow.DocumentRepository.GetByIdAsync(nonExistentId)).ReturnsAsync(null as Document);

            // Act & Assert
            Assert.ThrowsAsync<RecordNotFoundException>(async () => await _documentService.GetAsync(nonExistentId));
        }

        [Test]
        public void GetAll_ReturnsAllDocumentsMappedToDocumentOutDTO()
        {
            // Arrange
            var documents = new List<Document>
            {
                Document,
                new Document { Id = Guid.NewGuid(), Name = "document2" },
            };

            var expectedDocumentOutDtos = documents.Select(d => new DocumentOutDTO { Id = d.Id, Name = d.Name }).ToList();

            _mockUnitOfWork.Setup(uow => uow.DocumentRepository.GetAll()).Returns(documents);
            _mockMapper.Setup(mapper => mapper.Map<DocumentOutDTO>(It.IsAny<Document>()))
                       .Returns<Document>(d => new DocumentOutDTO { Id = d.Id, Name = d.Name });

            // Act
            var result = _documentService.GetAll();

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Count(), Is.EqualTo(expectedDocumentOutDtos.Count));

            for (var i = 0; i < expectedDocumentOutDtos.Count; i++)
            {
                Assert.That(result.ElementAt(i).Id, Is.EqualTo(expectedDocumentOutDtos[i].Id));
                Assert.That(result.ElementAt(i).Name, Is.EqualTo(expectedDocumentOutDtos[i].Name));
            }
        }

        [Test]
        public void GetByStepNeeded_ReturnsDocumentsFilteredByStepIdAndToReceiveFlagFalse()
        {
            // Arrange
            var stepId = Guid.NewGuid();
            var documents = new List<Document>
            {
                new Document { Id = Guid.NewGuid(), Name = "Document1", StepDocuments = new List<StepDocument> { new StepDocument { StepId = stepId, ToReceive = false } } },
                new Document { Id = Guid.NewGuid(), Name = "Document2", StepDocuments = new List<StepDocument> { new StepDocument { StepId = stepId, ToReceive = true } } },
                new Document { Id = Guid.NewGuid(), Name = "Document3", StepDocuments = new List<StepDocument> { new StepDocument { StepId = Guid.NewGuid(), ToReceive = false } } },
            };

            var expectedDocuments = documents
                .Where(d => d.StepDocuments.Any(ds => ds.StepId == stepId && ds.ToReceive == false))
                .Select(d => new DocumentOutDTO { Id = d.Id, Name = d.Name }) 
                .ToList();

            _mockUnitOfWork.Setup(uow => uow.DocumentRepository.GetAll()).Returns(documents);
            _mockMapper.Setup(mapper => mapper.Map<DocumentOutDTO>(It.IsAny<Document>()))
                       .Returns<Document>(d => new DocumentOutDTO { Id = d.Id, Name = d.Name }); 

            // Act
            var result = _documentService.GetByStepNeeded(stepId);

            // Assert
            Assert.IsNotNull(result);
            CollectionAssert.AreEqual(
                expectedDocuments.OrderBy(d => d.Id).ToList(),
                result.OrderBy(d => d.Id).ToList()); 
        }

        [Test]
        public void GetByStepReceived_ReturnsDocumentsFilteredByStepIdAndToReceiveFlagTrue()
        {
            // Arrange
            var stepId = Guid.NewGuid();
            var documents = new List<Document>
            {
                new Document { Id = Guid.NewGuid(), Name = "Document1", StepDocuments = new List<StepDocument> { new StepDocument { StepId = stepId, ToReceive = false } } },
                new Document { Id = Guid.NewGuid(), Name = "Document2", StepDocuments = new List<StepDocument> { new StepDocument { StepId = stepId, ToReceive = true } } },
                new Document { Id = Guid.NewGuid(), Name = "Document3", StepDocuments = new List<StepDocument> { new StepDocument { StepId = Guid.NewGuid(), ToReceive = true } } },
            };

            var expectedDocuments = documents
                .Where(d => d.StepDocuments.Any(ds => ds.StepId == stepId && ds.ToReceive == true))
                .Select(d => new DocumentOutDTO { Id = d.Id, Name = d.Name })
                .ToList();

            _mockUnitOfWork.Setup(uow => uow.DocumentRepository.GetAll()).Returns(documents);
            _mockMapper.Setup(mapper => mapper.Map<DocumentOutDTO>(It.IsAny<Document>()))
                       .Returns<Document>(d => new DocumentOutDTO { Id = d.Id, Name = d.Name });

            // Act
            var result = _documentService.GetByStepReceived(stepId);

            // Assert
            Assert.IsNotNull(result);
            CollectionAssert.AreEqual(
                expectedDocuments.OrderBy(d => d.Id).ToList(),
                result.OrderBy(d => d.Id).ToList());
        }

        [Test]
        public async Task AddAsync_WhenDocumentAdded_AddMethodCallsOnceAndMatches()
        {
            // Arrange
            var documentInDto = new DocumentInDTO { Name = "document1" };
            var mappedDocument = new Document { Name = "document1" };

            Document? capturedDocument = null;

            _mockMapper.Setup(mapper => mapper.Map<Document>(documentInDto)).Returns(mappedDocument);
            _mockDocumentRepository.Setup(repo => repo.AddAsync(It.IsAny<Document>()))
                           .Callback<Document>(doc => capturedDocument = doc)
                           .Returns(Task.CompletedTask);


            // Act
            await _documentService.AddAsync(documentInDto);

            // Assert
            _mockDocumentRepository.Verify(repo => repo.AddAsync(It.IsAny<Document>()), Times.Once);

            Assert.IsNotNull(capturedDocument);
            Assert.That(capturedDocument?.Name, Is.EqualTo(mappedDocument.Name));


        }

        [Test]
        public void AddAsync_WhenExceptionThrown_ThrowsException()
        {
            // Arrange
            var documentInDto = new DocumentInDTO { Name = "document1" };
            var exceptionMessage = "test exception message";

            _mockMapper.Setup(mapper => mapper.Map<Document>(documentInDto)).Returns(It.IsAny<Document>());
            _mockDocumentRepository.Setup(repo => repo.AddAsync(It.IsAny<Document>())).ThrowsAsync(new InvalidCastException(exceptionMessage));
            
            // Act & Assert
            var exception = Assert.ThrowsAsync<InvalidCastException>(async () => await _documentService.AddAsync(documentInDto));
            Assert.That(exception?.Message, Is.EqualTo(exceptionMessage));
        }

        [Test]
        public async Task UpdateAsync_WhenDocumentUpdated_CheckUpdateAndPropertyName()
        {
            // Arrange
            var documentOutDto = new DocumentOutDTO { Id = Guid.NewGuid(), Name = "document1" };
            var mappedDocument = new Document { Id = documentOutDto.Id, Name = "document1" };
            var updatedDocument = new Document { Id = documentOutDto.Id, Name = "updatedDocument" };

            _mockMapper.Setup(mapper => mapper.Map<Document>(documentOutDto)).Returns(mappedDocument);

            Document capturedDocument = null;
            _mockDocumentRepository.Setup(repo => repo.UpdateAsync(It.IsAny<Document>()))
                                   .Callback<Document>(doc => capturedDocument = doc)
                                   .Returns(Task.CompletedTask);

            // Act
            await _documentService.UpdateAsync(documentOutDto);

            // Assert
            _mockDocumentRepository.Verify(repo => repo.UpdateAsync(It.IsAny<Document>()), Times.Once);

            Assert.IsNotNull(capturedDocument);
            Assert.That(capturedDocument.Id, Is.EqualTo(documentOutDto.Id));
        }

        [Test]
        public async Task DeleteAsync_WhenDocumentDeleted_DeleteMethodCalledOnceWithCorrectId()
        {
            // Arrange
            var documentId = Guid.NewGuid();

            _mockUnitOfWork.Setup(uow => uow.DocumentRepository.DeleteAsync(documentId))
                           .Returns(Task.CompletedTask);

            // Act
            await _documentService.DeleteAsync(documentId);

            // Assert
            _mockDocumentRepository.Verify(repo => repo.DeleteAsync(documentId), Times.Once);
        }


    }

}
