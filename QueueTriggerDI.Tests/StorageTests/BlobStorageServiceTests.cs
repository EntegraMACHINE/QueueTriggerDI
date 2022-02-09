using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Specialized;
using Microsoft.Extensions.Logging;
using Moq;
using QueueTriggerDI.Storage.DTO;
using QueueTriggerDI.Storage.Services;
using QueueTriggerDI.Tests.Helpers;
using QueueTriggerDI.Utils.Exceptions;
using System;
using System.Threading;
using Xunit;

namespace QueueTriggerDI.Tests.StorageTests
{
    public class BlobStorageServiceTests
    {
        private readonly BlobStorageService _systemUnderTest;

        private readonly Mock<IBlobClientService> _blobClientServiceMock = new Mock<IBlobClientService>();
        private readonly Mock<ILogger<BlobStorageService>> _loggerMock = new Mock<ILogger<BlobStorageService>>();

        public BlobStorageServiceTests()
        {
            _systemUnderTest = new BlobStorageService(_blobClientServiceMock.Object, _loggerMock.Object);
        }

        [Fact]
        public void UploadBlob_ShouldReturnTrue_WhenBlobUploded()
        {
            //Arrange
            BlobParametersDto blobParametersDto = new BlobParametersDto
            {
                BlobContainerName = "TestContainerName",
                BlobName = "TestClientName"

            };

            Mock<BlobContainerClient> blobContainerClientMock = new Mock<BlobContainerClient>();
            Mock<BlobClient> blobClientMock = new Mock<BlobClient>();

            blobContainerClientMock.Setup(x => x.Name).Returns(blobParametersDto.BlobContainerName);
            blobClientMock.Setup(x => x.Name).Returns(blobParametersDto.BlobName);
            blobClientMock.Setup(x => x.Exists(CancellationToken.None).Value).Returns(true);

            _blobClientServiceMock.Setup(x => x.GetBlobContainerClient(blobParametersDto.BlobContainerName)).Returns(blobContainerClientMock.Object);
            _blobClientServiceMock.Setup(x => x.GetBlobClient(blobContainerClientMock.Object, blobParametersDto.BlobName)).Returns(blobClientMock.Object);

            //Act
            bool response = _systemUnderTest.UploadBlob(blobParametersDto, new GenericContentHelper());

            //Assert
            Assert.True(response);
        }

        [Fact]
        public void UploadBlob_ShouldReturnFalse_WhenParametersAreNullOrEmpty()
        {
            //Arrange
            BlobParametersDto blobParametersDto = new BlobParametersDto
            {
                BlobContainerName = "",
                BlobName = ""

            };

            //Act & Assert
            Assert.Throws<VerifyException>(() => _systemUnderTest.UploadBlob(blobParametersDto, new GenericContentHelper()));
        }

        [Fact]
        public void UploadBlob_ShouldReturnFalse_WhenBlobContainerDoesNotExists()
        {
            //Arrange
            BlobParametersDto blobParametersDto = new BlobParametersDto
            {
                BlobContainerName = "TestContainerName",
                BlobName = "TestClientName"

            };

            _blobClientServiceMock.Setup(x => x.GetBlobContainerClient(blobParametersDto.BlobContainerName)).Returns(() => null);

            //Act
            bool response = _systemUnderTest.UploadBlob(blobParametersDto, new GenericContentHelper());

            //Assert
            Assert.False(response);
        }

        [Fact]
        public void UploadBlob_ShouldReturnFalse_WhenBlobNullOrDoesNotExists()
        {
            //Arrange
            BlobParametersDto blobParametersDto = new BlobParametersDto
            {
                BlobContainerName = "TestContainerName",
                BlobName = "TestClientName"

            };

            Mock<BlobContainerClient> blobContainerClientMock = new Mock<BlobContainerClient>();
            Mock<BlockBlobClient> blobClientMock = new Mock<BlockBlobClient>();

            blobContainerClientMock.Setup(x => x.Name).Returns(blobParametersDto.BlobContainerName);
            blobClientMock.Setup(x => x.Exists(CancellationToken.None).Value).Returns(false);

            _blobClientServiceMock.Setup(x => x.GetBlobContainerClient(blobParametersDto.BlobContainerName)).Returns(blobContainerClientMock.Object);
            _blobClientServiceMock.Setup(x => x.GetBlobClient(blobContainerClientMock.Object, blobParametersDto.BlobName)).Returns(() => null);

            //Act
            bool response = _systemUnderTest.UploadBlob(blobParametersDto, new GenericContentHelper());

            //Assert
            Assert.False(response);
        }

        [Fact]
        public void StageBlock_ShouldreturnTrue_WhenBlobBlockStaged()
        {
            //Arrange
            BlobParametersDto blobParametersDto = new BlobParametersDto
            {
                BlobContainerName = "TestContainerName",
                BlobName = "TestClientName"

            };

            Mock<BlobContainerClient> blobContainerClientMock = new Mock<BlobContainerClient>();
            Mock<BlockBlobClient> blobClientMock = new Mock<BlockBlobClient>();

            blobContainerClientMock.Setup(x => x.Name).Returns(blobParametersDto.BlobContainerName);
            blobClientMock.Setup(x => x.Name).Returns(blobParametersDto.BlobName);
            blobClientMock.Setup(x => x.Exists(CancellationToken.None).Value).Returns(true);

            _blobClientServiceMock.Setup(x => x.GetBlobContainerClient(blobParametersDto.BlobContainerName)).Returns(blobContainerClientMock.Object);
            _blobClientServiceMock.Setup(x => x.GetBlockBlobClient(blobContainerClientMock.Object, blobParametersDto.BlobName)).Returns(blobClientMock.Object);

            //Act
            bool response = _systemUnderTest.StageBlock(blobParametersDto, new GenericContentHelper());

            //Assert
            Assert.True(response);
        }

        [Fact]
        public void StageBlock_ShouldreturnException_WhenParametersAreNullOrEmpty()
        {
            //Arrange
            BlobParametersDto blobParametersDto = new BlobParametersDto
            {
                BlobContainerName = "",
                BlobName = ""

            };

            //Act & Assert
            Assert.Throws<VerifyException>(() => _systemUnderTest.StageBlock(blobParametersDto, new GenericContentHelper()));
        }

        [Fact]
        public void StageBlock_ShouldReturnFalse_WhenBlobContainerDoesNotExists()
        {
            //Arrange
            BlobParametersDto blobParametersDto = new BlobParametersDto
            {
                BlobContainerName = "TestContainerName",
                BlobName = "TestClientName"

            };

            _blobClientServiceMock.Setup(x => x.GetBlobContainerClient(blobParametersDto.BlobContainerName)).Returns(() => null);

            //Act
            bool response = _systemUnderTest.StageBlock(blobParametersDto, new GenericContentHelper());

            //Assert
            Assert.False(response);
        }

        [Fact]
        public void StageBlock_ShouldReturnFalse_WhenBlobNullOrDoesNotExists()
        {
            //Arrange
            BlobParametersDto blobParametersDto = new BlobParametersDto
            {
                BlobContainerName = "TestContainerName",
                BlobName = "TestClientName"

            };

            Mock<BlobContainerClient> blobContainerClientMock = new Mock<BlobContainerClient>();
            Mock<BlockBlobClient> blobClientMock = new Mock<BlockBlobClient>();

            blobContainerClientMock.Setup(x => x.Name).Returns(blobParametersDto.BlobContainerName);
            blobClientMock.Setup(x => x.Exists(CancellationToken.None).Value).Returns(false);

            _blobClientServiceMock.Setup(x => x.GetBlobContainerClient(blobParametersDto.BlobContainerName)).Returns(blobContainerClientMock.Object);
            _blobClientServiceMock.Setup(x => x.GetBlobClient(blobContainerClientMock.Object, blobParametersDto.BlobName)).Returns(() => null);

            //Act
            bool response = _systemUnderTest.StageBlock(blobParametersDto, new GenericContentHelper());

            //Assert
            Assert.False(response);
        }
    }
}
