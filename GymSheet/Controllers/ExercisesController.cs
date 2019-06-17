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
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Caching.Memory;

namespace GymSheet.Controllers
{
    [Authorize]
    public class ExercisesController : Controller
    {
        private readonly ExerciseService _exerciseService;
        private readonly MuscleGroupService _muscleGroupService;
        private readonly ExerciseListService _exerciseListService;
        private readonly IMemoryCache _cache;

        // Tempo de duração do Cache
        private readonly MemoryCacheEntryOptions cacheOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(70));
        // Lista para guardar cache
        private List<Exercise> list;
        private List<MuscleGroup> list2;

        public ExercisesController(ExerciseService exerciseService, MuscleGroupService muscleGroupService, ExerciseListService exerciseListService, IMemoryCache cache)
        {
            _exerciseService = exerciseService;
            _muscleGroupService = muscleGroupService;
            _exerciseListService = exerciseListService;
            _cache = cache;
        }


        // Listar Get:
        [HttpGet("Exercicios")]
        public async Task<IActionResult> Index()
        {
            if (!_cache.TryGetValue("exercise", out list))
            {
                list = await _exerciseService.FindAllAsync();
                _cache.Set("exercise", list, cacheOptions);
            }
            else
            {
                list = _cache.Get("exercise") as List<Exercise>;
            }
            return View(list);
        }

        // Criar Get:
        public async Task<IActionResult> Create()
        {
            if (!_cache.TryGetValue("muscleGroup", out list2))
            {
                list2 = await _muscleGroupService.FindAllAsync();
                _cache.Set("muscleGroup", list2, cacheOptions);
            }
            else
            {
                list2 = _cache.Get("muscleGroup") as List<MuscleGroup>;
            }

            ViewBag.MuscleGroupId = new SelectList(list2, "Id", "Name");
            return View();
        }

        // Criar Post: 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Exercise obj)
        {
            if (ModelState.IsValid)
            {
                TempData["confirm"] = obj.Name + " foi cadastrado com sucesso.";
                await _exerciseService.InsertAsync(obj);
                list = await _exerciseService.FindAllAsync();
                _cache.Set("exercise", list, cacheOptions);
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

            if (!_cache.TryGetValue("exercise", out list))
            {
                list = await _exerciseService.FindAllAsync();
                _cache.Set("exercise", list, cacheOptions);
            }
            else
            {
                list = _cache.Get("exercise") as List<Exercise>;
            }

            var obj = list.Find(x => x.Id == id);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não encontrado" });
            }

            if (!_cache.TryGetValue("muscleGroup", out list2))
            {
                list2 = await _muscleGroupService.FindAllAsync();
                _cache.Set("muscleGroup", list2, cacheOptions);
            }
            else
            {
                list2 = _cache.Get("muscleGroup") as List<MuscleGroup>;
            }

            ViewBag.MuscleGroupId = new SelectList(list2, "Id", "Name");

            return View(obj);
        }

        // Editar Post:
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Exercise obj)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _exerciseService.UpdateAsync(obj);
                    TempData["confirm"] = obj.Name + " foi editado com sucesso.";
                    list = await _exerciseService.FindAllAsync();
                    _cache.Set("exercise", list, cacheOptions);

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

            if (!_cache.TryGetValue("exercise", out list))
            {
                list = await _exerciseService.FindAllAsync();
                _cache.Set("exercise", list, cacheOptions);
            }
            else
            {
                list = _cache.Get("exercise") as List<Exercise>;
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
                await _exerciseService.RemoveAsync(id);
                var obj = (_cache.Get("exercise") as List<Exercise>).Find(x => x.Id == id);
                TempData["confirm"] = obj.Name + " foi deletado com sucesso.";
                (_cache.Get("exercise") as List<Exercise>).Remove(obj);
                return RedirectToAction(nameof(Index));
            }
            catch (IntegrityException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        // Tratamento de erros:
        [HttpGet("/Exercicios/Erro")]
        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };
            return View(viewModel);
        }

        // Exercício já existe:
        public async Task<JsonResult> ExerciseExist(int? Id, string Name)
        {
            if (await _exerciseService.HasAny(Id, Name))
                return Json("exercício já cadastrado");
            return Json(true);
        }

        // Listar exercícios para seleção:
        [HttpGet("/Exercicios/Selecionar")]
        public async Task<IActionResult> List()
        {
            var exerciseLists = await _exerciseListService.FindAllAsync();
            var exercises = await _exerciseService.FindAllAsync();
            var viewModel = new ExercisesViewModel() { ExerciseLists = exerciseLists, Exercises = exercises };
            return View(viewModel);
        }
    }
}