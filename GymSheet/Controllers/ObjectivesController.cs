using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using GymSheet.Models;
using GymSheet.Models.ViewModels;
using GymSheet.Services;
using GymSheet.Services.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace GymSheet.Controllers
{
    [Authorize]
    public class ObjectivesController : Controller
    {
        private readonly ObjectiveService _objectiveService;
        private readonly IMemoryCache _cache;

        // Tempo de duração do Cache
        private readonly MemoryCacheEntryOptions cacheOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(60));
        // Lista para guardar cache
        private List<Objective> list;

        public ObjectivesController(ObjectiveService objectiveService, IMemoryCache cache)
        {
            _objectiveService = objectiveService;
            _cache = cache;
        }

        // Listar Get:
        [HttpGet("Objetivos")]
        public async Task<IActionResult> Index()
        {
            if (!_cache.TryGetValue("objective", out list))
            {
                list = await _objectiveService.FindAllAsync();
                _cache.Set("objective", list, cacheOptions);
            }
            else
            {
                list = _cache.Get("objective") as List<Objective>;
            }
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
        public async Task<IActionResult> Create(Objective obj)
        {
            if (ModelState.IsValid)
            {
                TempData["confirm"] = obj.Name + " foi cadastrado com sucesso.";
                await _objectiveService.InsertAsync(obj);
                list = await _objectiveService.FindAllAsync();
                _cache.Set("objective", list, cacheOptions);
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

            if (!_cache.TryGetValue("objective", out list))
            {
                list = await _objectiveService.FindAllAsync();
                _cache.Set("objective", list, cacheOptions);
            }
            else
            {
                list = _cache.Get("objective") as List<Objective>;
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
        public async Task<IActionResult> Edit(int id, Objective obj)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _objectiveService.UpdateAsync(obj);
                    TempData["confirm"] = obj.Name + " foi editado com sucesso.";
                    list = await _objectiveService.FindAllAsync();
                    _cache.Set("objective", list, cacheOptions);

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

            if (!_cache.TryGetValue("objective", out list))
            {
                list = await _objectiveService.FindAllAsync();
                _cache.Set("objective", list, cacheOptions);
            }
            else
            {
                list = _cache.Get("objective") as List<Objective>;
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
                await _objectiveService.RemoveAsync(id);
                var obj = (_cache.Get("objective") as List<Objective>).Find(x => x.Id == id);
                TempData["confirm"] = obj.Name + " foi deletado com sucesso.";
                (_cache.Get("objective") as List<Objective>).Remove(obj);
                return RedirectToAction(nameof(Index));
            }
            catch (IntegrityException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        // Tratamento de erros:
        [HttpGet("Objetivos/Erro")]
        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };
            return View(viewModel);
        }

        // Objetivo já cadastrado:
        public async Task<JsonResult> ObjectiveExist(int? Id, string Name)
        {
            if (await _objectiveService.HasAnyName(Id, Name))
                return Json("objetivo já cadastrado");
            return Json(true);
        }
    }
}