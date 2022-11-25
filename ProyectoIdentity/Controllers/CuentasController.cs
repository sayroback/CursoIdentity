using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProyectoIdentity.Models;

namespace ProyectoIdentity.Controllers
{
  public class CuentasController : Controller
  {
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    public CuentasController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
    {
      userManager = _userManager;
      signInManager = _signInManager;
    }

    public IActionResult Index()
    {
      return View();
    }
    [HttpGet]
    public async Task<IActionResult> Registro()
    {
      RegistroViewModel registroVM = new RegistroViewModel();
      return View(registroVM);
    }
    [HttpPost]
    public async Task<IActionResult> Registro(RegistroViewModel rgViewModel)
    {
      if (ModelState.IsValid)
      {
        var usuario = new AppUsuario
        {
          UserName = rgViewModel.Email,
          Email = rgViewModel.Email,
          Nombre = rgViewModel.Nombre,
          URL = rgViewModel.URL,
          CodigoPais = rgViewModel.CodigoPais,
          Telefono = rgViewModel.Telefono,
          Pais = rgViewModel.Pais,
          Ciudad = rgViewModel.Ciudad,
          Direccion = rgViewModel.Direccion,
          FechaNacimiento = rgViewModel.FechaNacimiento,
          Estado = rgViewModel.Estado
        };
        var resultado = await _userManager.CreateAsync(usuario, rgViewModel.Password);
        if (resultado.Succeeded)
        {
          await _signInManager.SignInAsync(usuario, isPersistent: false);
          return RedirectToAction("Index", "Home");
        }
        ValidarErrores(resultado);
      }
      return View(rgViewModel);
    }
    private void ValidarErrores(IdentityResult resultado)
    {
      foreach (var error in resultado.Errors)
      {
        ModelState.AddModelError(String.Empty, error.Description);
      }
    }
  }
}
