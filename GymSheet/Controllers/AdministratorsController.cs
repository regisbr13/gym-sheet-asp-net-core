using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using GymSheet.Models;
using GymSheet.Models.ViewModels;
using GymSheet.Services;
using GymSheet.Services.Exceptions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace GymSheet.Controllers
{
    public class AdministratorsController : Controller
    {
        private readonly AdministratorService _administratorService;

        public AdministratorsController(AdministratorService administratorService)
        {
            _administratorService = administratorService;
        }

        // Registrar Get:
        public IActionResult Register()
        {
            return View();
        }

        // Registrar Post: 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(Administrator obj)
        {
            if (ModelState.IsValid)
            {
                TempData["confirm"] = obj.Name + " foi cadastrado com sucesso.";
                await _administratorService.InsertAsync(obj);

                HttpContext.Session.SetString("AdminId", obj.Id.ToString());
                HttpContext.Session.SetString("Email", obj.Email);
                var claims = new List<Claim> { new Claim(ClaimTypes.Email, obj.Email) };
                var userIdentity = new ClaimsIdentity(claims, "login");
                ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
                await HttpContext.SignInAsync(principal);
                return RedirectToAction("Index", "MuscleGroups");
            }

            TempData["erro"] = "Erro ao cadastrar.";
            return RedirectToAction("Index", "MuscleGroups");
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
                HttpContext.Session.Clear();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(AdministratorViewModel viewModel)
        {
            if (await _administratorService.HasAny(viewModel.Email, viewModel.Password))
            {
                HttpContext.Session.SetString("Email", viewModel.Email);
                var claims = new List<Claim>() { new Claim(ClaimTypes.Email, viewModel.Email) };      
                var user = new ClaimsIdentity(claims, "login");                                      
                ClaimsPrincipal principal = new ClaimsPrincipal(user);                              
                await HttpContext.SignInAsync(principal);                                           
                return RedirectToAction("Index", "MuscleGroups");
            }
            else
            {
            ModelState.AddModelError(string.Empty, "Email ou senha inválidos");
            return View(viewModel);
            }
        }

        // Desautenticar:
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        // Editar Post:
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Administrator obj)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _administratorService.UpdateAsync(obj);
                    TempData["confirm"] = obj.Name + " foi editado com sucesso.";
                    return RedirectToAction(nameof(Informations));
                }
                catch (ApplicationException e)
                {
                    return RedirectToAction(nameof(Error), new { message = e.Message });
                }
            }

            if (id != obj.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Id's não correspondem" });
            }

            TempData["erro"] = "Erro ao editar.";
            return RedirectToAction(nameof(Informations));
        }

        public async Task<IActionResult> Informations()
        {
            var obj = await _administratorService.FindByEmail(HttpContext.Session.GetString("Email"));
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não encontrado" });
            }
            ViewBag.Ip = Request.HttpContext.Connection.RemoteIpAddress.ToString();

            return View(obj);
        }

        // Tratamento de erros:
        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };
            return View(viewModel);
        }
    }
}