using System;
using System.Threading.Tasks;
using Xunit;
using Cn2x.Iryo.UlceraVenosa.Application.Features.AvaliacaoUlcera.Services;
using Microsoft.Extensions.Logging;
using Moq;

namespace Cn2x.Iryo.UlceraVenosa.Tests.Application.Features.AvaliacaoUlcera;

public class FileUploadServiceTests
{
    private readonly Mock<ILogger<FileUploadService>> _loggerMock;
    private readonly FileUploadService _service;

    public FileUploadServiceTests()
    {
        _loggerMock = new Mock<ILogger<FileUploadService>>();
        _service = new FileUploadService(_loggerMock.Object);
    }

    [Fact]
    public async Task ProcessBase64Async_ComImagemJPEGValida_DeveRetornarResultadoCorreto()
    {
        // Arrange
        var imagemJPEG = CriarImagemJPEGTeste();
        var base64String = Convert.ToBase64String(imagemJPEG);

        // Act
        var resultado = await _service.ProcessBase64Async(base64String);

        // Assert
        Assert.NotNull(resultado);
        Assert.Equal(imagemJPEG.Length, resultado.SizeBytes);
        Assert.Equal("image/jpeg", resultado.ContentType);
        Assert.EndsWith(".jpg", resultado.FileName);
        Assert.Equal("Imagem da úlcera", resultado.Description);
        Assert.True(resultado.CaptureDate > DateTime.UtcNow.AddMinutes(-1));
    }

    [Fact]
    public async Task ProcessBase64Async_ComImagemPNGValida_DeveRetornarResultadoCorreto()
    {
        // Arrange
        var imagemPNG = CriarImagemPNGTeste();
        var base64String = Convert.ToBase64String(imagemPNG);

        // Act
        var resultado = await _service.ProcessBase64Async(base64String);

        // Assert
        Assert.NotNull(resultado);
        Assert.Equal(imagemPNG.Length, resultado.SizeBytes);
        Assert.Equal("image/png", resultado.ContentType);
        Assert.EndsWith(".png", resultado.FileName);
    }

    [Fact]
    public async Task ProcessBase64Async_ComBase64ComHeader_DeveRemoverHeaderCorretamente()
    {
        // Arrange
        var imagemJPEG = CriarImagemJPEGTeste();
        var base64String = $"data:image/jpeg;base64,{Convert.ToBase64String(imagemJPEG)}";

        // Act
        var resultado = await _service.ProcessBase64Async(base64String);

        // Assert
        Assert.NotNull(resultado);
        Assert.Equal(imagemJPEG.Length, resultado.SizeBytes);
        Assert.Equal("image/jpeg", resultado.ContentType);
    }

    [Fact]
    public async Task ProcessBase64Async_ComBase64Invalido_DeveLancarExcecao()
    {
        // Arrange
        var base64Invalido = "base64-invalido-!@#";

        // Act & Assert
        var excecao = await Assert.ThrowsAsync<ArgumentException>(
            () => _service.ProcessBase64Async(base64Invalido));
        
        Assert.Contains("Arquivo base64 inválido", excecao.Message);
    }

    [Fact]
    public void IsValidImage_ComJPEGValido_DeveRetornarTrue()
    {
        // Arrange
        var imagemJPEG = CriarImagemJPEGTeste();

        // Act
        var resultado = _service.IsValidImage(imagemJPEG, "image/jpeg");

        // Assert
        Assert.True(resultado);
    }

    [Fact]
    public void IsValidImage_ComPNGValido_DeveRetornarTrue()
    {
        // Arrange
        var imagemPNG = CriarImagemPNGTeste();

        // Act
        var resultado = _service.IsValidImage(imagemPNG, "image/png");

        // Assert
        Assert.True(resultado);
    }

    [Fact]
    public void IsValidImage_ComArquivoMuitoPequeno_DeveRetornarFalse()
    {
        // Arrange
        var arquivoPequeno = new byte[] { 0x89, 0x50, 0x4E, 0x47 }; // Apenas 4 bytes

        // Act
        var resultado = _service.IsValidImage(arquivoPequeno, "image/png");

        // Assert
        Assert.False(resultado);
    }

    [Fact]
    public void IsValidImage_ComTipoNaoSuportado_DeveRetornarFalse()
    {
        // Arrange
        var imagemJPEG = CriarImagemJPEGTeste();

        // Act
        var resultado = _service.IsValidImage(imagemJPEG, "application/pdf");

        // Assert
        Assert.False(resultado);
    }

    private byte[] CriarImagemJPEGTeste()
    {
        // Criar uma imagem JPEG válida de teste (1x1 pixel)
        // Bytes mágicos JPEG: FF D8 + dados mínimos + FF D9
        return new byte[]
        {
            0xFF, 0xD8, // SOI marker
            0xFF, 0xE0, // APP0 marker
            0x00, 0x10, // Length
            0x4A, 0x46, 0x49, 0x46, 0x00, // "JFIF\0"
            0x01, 0x01, // Version 1.1
            0x00, // Units: none
            0x00, 0x01, // Density: 1x1
            0x00, 0x01,
            0x00, 0x00, // No thumbnail
            0xFF, 0xDB, // DQT marker
            0x00, 0x43, // Length
            0x00, // Table info
            // Quantization table (simplificada)
            0x10, 0x0B, 0x09, 0x0B, 0x15, 0x18, 0x18, 0x15,
            0x18, 0x18, 0x18, 0x18, 0x18, 0x18, 0x18, 0x18,
            0x18, 0x18, 0x18, 0x18, 0x18, 0x18, 0x18, 0x18,
            0x18, 0x18, 0x18, 0x18, 0x18, 0x18, 0x18, 0x18,
            0x18, 0x18, 0x18, 0x18, 0x18, 0x18, 0x18, 0x18,
            0x18, 0x18, 0x18, 0x18, 0x18, 0x18, 0x18, 0x18,
            0x18, 0x18, 0x18, 0x18, 0x18, 0x18, 0x18, 0x18,
            0x18, 0x18, 0x18, 0x18, 0x18, 0x18, 0x18, 0x18,
            0xFF, 0xC0, // SOF0 marker
            0x00, 0x11, // Length
            0x08, // Precision
            0x00, 0x01, // Height: 1
            0x00, 0x01, // Width: 1
            0x03, // Components: 3
            0x01, 0x11, 0x00, // Component 1: Y
            0x02, 0x11, 0x01, // Component 2: Cb
            0x03, 0x11, 0x01, // Component 3: Cr
            0xFF, 0xC4, // DHT marker
            0x00, 0x14, // Length
            0x00, // Table info
            // Huffman table (simplificada)
            0x00, 0x01, 0x05, 0x01, 0x01, 0x01, 0x01, 0x01,
            0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07,
            0xFF, 0xDA, // SOS marker
            0x00, 0x0C, // Length
            0x03, // Components: 3
            0x01, 0x00, // Component 1: Y
            0x02, 0x11, // Component 2: Cb
            0x03, 0x11, // Component 3: Cr
            0x00, 0x3F, 0x00, // Spectral selection
            0x00, 0x00, 0x01, 0x05, 0x01, 0x01, 0x01, 0x01, // Dummy data
            0xFF, 0xD9  // EOI marker
        };
    }

    private byte[] CriarImagemPNGTeste()
    {
        // Criar uma imagem PNG válida de teste (1x1 pixel)
        // Bytes mágicos PNG: 89 50 4E 47 0D 0A 1A 0A
        return new byte[]
        {
            0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A, // PNG signature
            0x00, 0x00, 0x00, 0x0D, // IHDR chunk length
            0x49, 0x48, 0x44, 0x52, // "IHDR"
            0x00, 0x00, 0x00, 0x01, // Width: 1
            0x00, 0x00, 0x00, 0x01, // Height: 1
            0x08, // Bit depth
            0x02, // Color type (RGB)
            0x00, // Compression method
            0x00, // Filter method
            0x00, // Interlace method
            0x00, 0x00, 0x00, 0x00, // CRC placeholder
            0x00, 0x00, 0x00, 0x0C, // IDAT chunk length
            0x49, 0x44, 0x41, 0x54, // "IDAT"
            0x08, 0x99, 0x01, 0x01, 0x00, 0x00, 0x00, 0xFF, 0xFF, 0x00, 0x00, 0x00, // Compressed data
            0x00, 0x00, 0x00, 0x00, // CRC placeholder
            0x00, 0x00, 0x00, 0x00, // IEND chunk length
            0x49, 0x45, 0x4E, 0x44, // "IEND"
            0xAE, 0x42, 0x60, 0x82  // CRC
        };
    }
}
