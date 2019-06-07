using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using GymSheet.Models;
using GymSheet.Models.ViewModels;
using GymSheet.Services;
using GymSheet.Services.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace GymSheet.Controllers
{
    public class SheetsController : Controller
    {
        private readonly SheetService _sheetService;
        private readonly StudentService _studentService;
        private readonly IMemoryCache _cache;

        // Tempo de duração do Cache
        private readonly MemoryCacheEntryOptions cacheOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(60));
        // Lista para guardar cache
        private List<Sheet> list;

        public SheetsController(SheetService sheetService, StudentService studentService, IMemoryCache cache)
        {
            _sheetService = sheetService;
            _studentService = studentService;
            _cache = cache;
        }


        // Listar Get:
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
            TempData["StudentId"] = StudentId;

            return View(list);
        }

        // Detalhar Get:
        public async Task<IActionResult> Details(int? StudentId)
        {
            if (StudentId == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id nulo" });
            }

            if (!_cache.TryGetValue("sheet", out list))
            {
                list = await _sheetService.FindAllAsync(StudentId);
                _cache.Set("sheet", list, cacheOptions);
            }
            else
            {
                list = _cache.Get("sheet") as List<Sheet>;
            }

            var obj = list.Find(x => x.Id == StudentId);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não encontrado" });
            }

            return View(obj);
        }

        // Criar Get:
        public IActionResult Create()
        {
            ViewBag.StudentId = TempData["StudentId"];
            return View();
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
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id nulo" });
            }

            ViewBag.StudentId = TempData["StudentId"];
            if (!_cache.TryGetValue("sheet" + TempData["StudentId"], out list))
            {
                list = await _sheetService.FindAllAsync(int.Parse(TempData["StudentId"].ToString()));
                _cache.Set("sheet" + TempData["StudentId"], list, cacheOptions);
            }
            else
            {
                list = _cache.Get("sheet" + TempData["StudentId"]) as List<Sheet>;
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
        public async Task<IActionResult> Edit(int id, Sheet obj)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _sheetService.UpdateAsync(obj);
                    TempData["confirm"] = obj.Name + " foi editado com sucesso.";
                    list = await _sheetService.FindAllAsync();
                    _cache.Set("sheet" + obj.StudentId, list, cacheOptions);

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
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id nulo" });
            }

            if (!_cache.TryGetValue("sheet" + TempData["StudentId"], out list))
            {
                list = await _sheetService.FindAllAsync();
                _cache.Set("sheet" + TempData["StudentId"], list, cacheOptions);
            }
            else
            {
                list = _cache.Get("sheet" + TempData["StudentId"]) as List<Sheet>;
            }

            var obj = list.Find(x => x.Id == id);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não encontrado" });
            }
            TempData["StudentId"] = obj.StudentId;
            return View(obj);
        }

        // Delete Post: 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _sheetService.RemoveAsync(id);
                var obj = (_cache.Get("sheet" + TempData["StudentId"]) as List<Sheet>).Find(x => x.Id == id);
                TempData["confirm"] = obj.Name + " foi deletado com sucesso.";
                (_cache.Get("sheet" + TempData["StudentId"]) as List<Sheet>).Remove(obj);
                return RedirectToAction(nameof(Index), new { StudentId = obj.StudentId });
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
    }
}