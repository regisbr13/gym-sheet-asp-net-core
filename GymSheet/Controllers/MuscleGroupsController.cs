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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace GymSheet.Controllers
{
    public class MuscleGroupsController : Controller
    {
        private readonly MuscleGroupService _muscleGroupService;
        private readonly IMemoryCache _cache;

        // Tempo de duração do Cache
        private readonly MemoryCacheEntryOptions cacheOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(60));
        // Lista para guardar cache
        private List<MuscleGroup> list;

        public MuscleGroupsController(MuscleGroupService muscleGroupService, IMemoryCache cache)
        {
            _muscleGroupService = muscleGroupService;
            _cache = cache;
        }

        // Listar Get:
        [Authorize]
        public async Task<IActionResult> Index()
        {
            if (!_cache.TryGetValue("muscleGroup", out list))
            {
                list = await _muscleGroupService.FindAllAsync();
                _cache.Set("muscleGroup", list, cacheOptions);
            }
            else
            {
                list = _cache.Get("muscleGroup") as List<MuscleGroup>;
            }

            HttpContext.Session.SetString("Email", "admin@admin.com");
            var claims = new List<Claim>() { new Claim(ClaimTypes.Email, "admin@admin.com") };
            var user = new ClaimsIdentity(claims, "login");
            ClaimsPrincipal principal = new ClaimsPrincipal(user);
            await HttpContext.SignInAsync(principal);

            return View(list);
        }

        // Criar Get:
        public IActionResult Create()
        {
            return View();
        }

        // Criar Post: 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MuscleGroup obj)
        {
            if (ModelState.IsValid)
            {
                TempData["confirm"] = obj.Name + " foi cadastrado com sucesso.";
                await _muscleGroupService.InsertAsync(obj);
                list = await _muscleGroupService.FindAllAsync();
                _cache.Set("muscleGroup", list, cacheOptions);
                return RedirectToAction(nameof(Index));
            }

            TempData["erro"] = "Erro ao cadastrar.";
            return RedirectToAction(nameof(Index));
        }

        // Editar Get:
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id nulo" });
            }

            if (!_cache.TryGetValue("muscleGroup", out list))
            {
                list = await _muscleGroupService.FindAllAsync();
                _cache.Set("muscleGroup", list, cacheOptions);
            }
            else
            {
                list = _cache.Get("muscleGroup") as List<MuscleGroup>;
            }

            var obj = list.Find(x => x.Id == id);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não encontrado" });
            }

            return View(obj);
        }

        // Editar Post:
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, MuscleGroup obj)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _muscleGroupService.UpdateAsync(obj);
                    TempData["confirm"] = obj.Name + " foi editado com sucesso.";
                    list = await _muscleGroupService.FindAllAsync();
                    _cache.Set("muscleGroup", list, cacheOptions);
                    return RedirectToAction(nameof(Index));
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
            return RedirectToAction(nameof(Index));
        }

        // Delete Get:
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id nulo" });
            }

            if (!_cache.TryGetValue("muscleGroup", out list))
            {
                list = await _muscleGroupService.FindAllAsync();
                _cache.Set("muscleGroup", list, cacheOptions);
            }
            else
            {
                list = _cache.Get("muscleGroup") as List<MuscleGroup>;
            }

            var obj = list.Find(x => x.Id == id);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não encontrado" });
            }

            return View(obj);
        }

        // Delete Post: 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var obj = (_cache.Get("muscleGroup") as List<MuscleGroup>).Find(x => x.Id == id);
                await _muscleGroupService.RemoveAsync(id);
                TempData["confirm"] = obj.Name + " foi deletado com sucesso.";
                list = await _muscleGroupService.FindAllAsync();
                _cache.Set("muscleGroup", list, cacheOptions);
                return RedirectToAction(nameof(Index));
            }
            catch (IntegrityException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
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

        // Grupo Muscular já existe:
        public async Task<JsonResult> MuscleGroupExist(int? Id, string Name)
        {
            if (await _muscleGroupService.HasAnyName(Id, Name))
                return Json("grupo muscular já cadastrado");
            return Json(true);
        }
    }
}