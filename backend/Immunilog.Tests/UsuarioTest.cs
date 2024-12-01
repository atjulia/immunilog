using Immunilog.Domain.Dto.Usuario;
using Immunilog.Services.Services.Usuario;
using Immunilog.UI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

[TestClass]
public class UsuarioControllerTests
{
    private Mock<IUsuarioService> _mockUsuarioService;
    private UsuarioController _controller;

    [TestInitialize]
    public void Setup()
    {
        _mockUsuarioService = new Mock<IUsuarioService>();
        _controller = new UsuarioController(_mockUsuarioService.Object);
    }

    [TestMethod]
    public async Task GetUsuarios_ReturnsOkResult_WithListOfUsuarios()
    {
        // Arrange
        var usuarios = new List<UsuarioDto>
        {
            new UsuarioDto { Id = Guid.NewGuid(), Nome = "Usuario 1" },
            new UsuarioDto { Id = Guid.NewGuid(), Nome = "Usuario 2" }
        };
        _mockUsuarioService.Setup(service => service.GetListAsync()).ReturnsAsync(usuarios);

        // Act
        var result = await _controller.GetUsuarios();

        // Assert
        var okResult = result.Result as OkObjectResult;
        Assert.IsNotNull(okResult);
        var returnValue = okResult.Value as List<UsuarioDto>;
        Assert.IsNotNull(returnValue);
        Assert.AreEqual(2, returnValue.Count);
    }

    [TestMethod]
    public async Task CreateUsuario_ReturnsOkResult_WithId()
    {
        // Arrange
        var creationUsuarioDto = new CreationUsuarioDto { Nome = "Novo Usuario" };
        var usuarioId = Guid.NewGuid();
        _mockUsuarioService.Setup(service => service.CreateAsync(creationUsuarioDto)).ReturnsAsync(usuarioId);

        // Act
        var result = await _controller.CreateUsuario(creationUsuarioDto);

        // Assert
        var okResult = result as OkObjectResult;
        Assert.IsNotNull(okResult);
        Assert.AreEqual(usuarioId, okResult.Value);
    }

    [TestMethod]
    public async Task CreateUsuario_ReturnsBadRequest_WhenModelStateIsInvalid()
    {
        // Arrange
        _controller.ModelState.AddModelError("Nome", "Campo obrigatório");
        var creationUsuarioDto = new CreationUsuarioDto { Nome = "" };

        // Act
        var result = await _controller.CreateUsuario(creationUsuarioDto);

        // Assert
        Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
    }

    [TestMethod]
    public async Task UpdateUsuario_ReturnsOkResult_WithId()
    {
        // Arrange
        var usuarioDto = new UsuarioDto { Id = Guid.NewGuid(), Nome = "Usuario Atualizado" };
        _mockUsuarioService.Setup(service => service.UpdateAsync(usuarioDto)).ReturnsAsync(true);

        // Act
        var result = await _controller.UpdateUsuario(usuarioDto);

        // Assert
        var okResult = result as OkObjectResult;
        Assert.IsNotNull(okResult);
        Assert.AreEqual(usuarioDto.Id, okResult.Value);
    }

    [TestMethod]
    public async Task UpdateUsuario_ReturnsBadRequest_WhenModelStateIsInvalid()
    {
        // Arrange
        _controller.ModelState.AddModelError("Nome", "Campo obrigatório");
        var usuarioDto = new UsuarioDto { Id = Guid.NewGuid(), Nome = "" };

        // Act
        var result = await _controller.UpdateUsuario(usuarioDto);

        // Assert
        Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
    }

    [TestMethod]
    public async Task GetUsuarioById_ReturnsOkResult_WithUsuario()
    {
        // Arrange
        var usuarioId = Guid.NewGuid();
        var usuarioDto = new UsuarioDto { Id = usuarioId, Nome = "Usuario 1" };
        _mockUsuarioService.Setup(service => service.GetUsuarioById(usuarioId)).ReturnsAsync(usuarioDto);

        // Act
        var result = await _controller.GetUsuarioById(usuarioId);

        // Assert
        var okResult = result as OkObjectResult;
        Assert.IsNotNull(okResult);
        var returnValue = okResult.Value as UsuarioDto;
        Assert.IsNotNull(returnValue);
        Assert.AreEqual(usuarioId, returnValue.Id);
    }

    [TestMethod]
    public async Task DeleteUsuario_ReturnsOkResult()
    {
        // Arrange
        var usuarioId = Guid.NewGuid();
        _mockUsuarioService.Setup(service => service.DeleteAsync(usuarioId)).ReturnsAsync(true);

        // Act
        var result = await _controller.DeleteUsuario(usuarioId);

        // Assert
        Assert.IsInstanceOfType(result, typeof(OkResult));
    }
}
