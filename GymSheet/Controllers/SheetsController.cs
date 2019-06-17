using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using GymSheet.Models;
using GymSheet.Models.ViewModels;
using GymSheet.Services;
using GymSheet.Services.Exceptions;
using jsreport.AspNetCore;
using jsreport.Types;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace GymSheet.Controllers
{
    [Authorize]
    public class SheetsController : Controller
    {
        private readonly SheetService _sheetService;
        private readonly StudentService _studentService;
        private readonly ExerciseListService _exerciseListService;
        private readonly IMemoryCache _cache;

        // Tempo de duração do Cache
        private readonly MemoryCacheEntryOptions cacheOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(60));
        // Lista para guardar cache
        private List<Sheet> list;

        public SheetsController(SheetService sheetService, StudentService studentService, ExerciseListService exerciseListService, IMemoryCache cache)
        {
            _sheetService = sheetService;
            _studentService = studentService;
            _exerciseListService = exerciseListService;
            _cache = cache;
        }


        // Listar Get:
        [HttpGet("Fichas")]
        public async Task<IActionResult> Index(int? StudentId)
        {
            if (StudentId == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id nulo" });
            }

            var obj = _sheetService.FindByIdAsync(StudentId);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não encontrado" });
            }

            if (!_cache.TryGetValue("sheet" + StudentId, out list))
            {
                list = await _sheetService.FindAllAsync(StudentId);
                _cache.Set("sheet" + StudentId, list, cacheOptions);
            }
            else
            {
                list = _cache.Get("sheet" + StudentId) as List<Sheet>;
            }

            ViewBag.studentName = (await _studentService.FindByIdAsync(StudentId)).Name;

            return View(list.OrderBy(x => x.Id));
        }

        // Detalhar Get:
        [HttpGet("Fichas/Detalhes")]
        public async Task<IActionResult> Details(int? id, int StudentId)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id nulo" });
            }

            list = await _sheetService.FindAllAsync(StudentId);
            _cache.Set("sheet" + StudentId, list, cacheOptions);

            var obj = (_cache.Get("sheet" + StudentId) as List<Sheet>).Find(x => x.Id == id);

            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não encontrado" });
            }

            return View(obj);
        }

        // Criar Get:
        public IActionResult Create(int StudentId)
        {
            var obj = new Sheet() { StudentId = StudentId };
            return View(obj);
        }

        // Criar Post: 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Sheet obj)
        {
            if (ModelState.IsValid)
            {
                TempData["confirm"] = obj.Name + " foi cadastrado com sucesso.";
                obj.Register = DateTime.Now;
                obj.Validate = DateTime.Now.AddDays(60);
                await _sheetService.InsertAsync(obj);
                list = await _sheetService.FindAllAsync(obj.StudentId);
                _cache.Set("sheet" + obj.StudentId, list, cacheOptions);
                return RedirectToAction(nameof(Index), new { StudentId = obj.StudentId });
            }

            TempData["erro"] = "Erro ao cadastrar.";
            return RedirectToAction(nameof(Index), new { StudentId = obj.StudentId});
        }

        // Editar Get:
        public async Task<IActionResult> Edit(int? id, int StudentId)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id nulo" });
            }

            if (!_cache.TryGetValue("sheet" + StudentId, out list))
            {
                list = await _sheetService.FindAllAsync(StudentId);
                _cache.Set("sheet" + StudentId, list, cacheOptions);
            }
            else
            {
                list = _cache.Get("sheet" + StudentId) as List<Sheet>;
            }

            var obj = list.Find(x => x.Id == id);
            obj.StudentId = StudentId;
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não encontrado" });
            }

            return View(obj);
        }

        // Editar Post:
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Sheet obj)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _sheetService.UpdateAsync(obj);
                    TempData["confirm"] = obj.Name + " foi editado com sucesso.";
                    var sheet = (_cache.Get("sheet" + obj.StudentId) as List<Sheet>).Find(x => x.Id == obj.Id);
                    (_cache.Get("sheet" + obj.StudentId) as List<Sheet>).Remove(sheet);
                    (_cache.Get("sheet" + obj.StudentId) as List<Sheet>).Add(obj);

                    return RedirectToAction(nameof(Index), new { StudentId = obj.StudentId });
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
            return RedirectToAction(nameof(Index), new { StudentId = obj.StudentId });
        }

        // Delete Get:
        public async Task<IActionResult> Delete(int? id, int StudentId)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id nulo" });
            }

            if (!_cache.TryGetValue("sheet" + StudentId, out list))
            {
                list = await _sheetService.FindAllAsync(StudentId);
                _cache.Set("sheet" + StudentId, list, cacheOptions);
            }
            else
            {
                list = _cache.Get("sheet" + StudentId) as List<Sheet>;
            }

            var obj = list.Find(x => x.Id == id);
            obj.StudentId = StudentId;
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não encontrado" });
            }

            return View(obj);
        }

        // Delete Post: 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, int StudentId)
        {
            try
            {
                await _sheetService.RemoveAsync(id);
                var obj = (_cache.Get("sheet" + StudentId) as List<Sheet>).Find(x => x.Id == id);
                TempData["confirm"] = obj.Name + " foi deletado com sucesso.";
                (_cache.Get("sheet" + StudentId) as List<Sheet>).Remove(obj);
                return RedirectToAction(nameof(Index), new { StudentId   = obj.StudentId });
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


        // Ficha já cadastrado:
        public async Task<JsonResult> SheetExist(int? Id, string Name)
        {
            if (await _sheetService.HasAny(Id, Name))
                return Json("ficha já cadastrada");
            return Json(true);
        }

        // Adicionar Exercício à ficha:
        [HttpGet]
        public IActionResult AddExercise(int SheetId, int ExerciseId, int StudentId)
        {
            ExerciseList obj = new ExerciseList() { ExerciseId = ExerciseId, SheetId = SheetId };

            return View(obj);
        }

        [HttpPost]
        public async Task<IActionResult> AddExercise(ExerciseList obj, int StudentId)
        {
            if (await _exerciseListService.HasExerciseList(obj.SheetId, obj.ExerciseId))
            {
                TempData["erro"] = "O exercício já consta na ficha.";
                return RedirectToAction("List", "Exercises", new { SheetId = obj.SheetId });
            }

            if (ModelState.IsValid)
            {
                TempData["confirm"] = "Exercício adicionado com sucesso.";
                await _exerciseListService.InsertAsync(obj);

                return RedirectToAction("List", "Exercises", new { SheetId = obj.SheetId });
            }

            TempData["erro"] = "Erro ao cadastrar.";
            return RedirectToAction("List", "Exercises", new { SheetId = obj.SheetId });
        }

        // Gerar Pdf:
        [HttpGet("Fichas/Pdf")]
        [MiddlewareFilter(typeof(JsReportPipeline))]
        public async Task<IActionResult> Pdf(int SheetId, int StudentId)
        {
            list = await _sheetService.FindAllAsync(StudentId);
            _cache.Set("sheet" + StudentId, list, cacheOptions);

            var obj = (_cache.Get("sheet" + StudentId) as List<Sheet>).Find(x => x.Id == SheetId);

            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não encontrado" });
            }
            HttpContext.JsReportFeature().Recipe(Recipe.ChromePdf);

            return View(obj);
        }
    }
}