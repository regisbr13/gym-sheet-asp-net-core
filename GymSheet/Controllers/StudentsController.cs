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
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Caching.Memory;

namespace GymSheet.Controllers
{
    public class StudentsController : Controller
    {
        private readonly StudentService _studentService;
        private readonly TeacherService _teacherService;
        private readonly ObjectiveService _objectiveService;
        private readonly IMemoryCache _cache;

        // Tempo de duração do Cache
        private readonly MemoryCacheEntryOptions cacheOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(70));
        // Lista para guardar cache
        private List<Student> list;
        private List<Teacher> teachers;
        private List<Objective> objectives;

        public StudentsController(StudentService studentService, TeacherService teacherService, ObjectiveService objectiveService, IMemoryCache cache)
        {
            _studentService = studentService;
            _teacherService = teacherService;
            _objectiveService = objectiveService;
            _cache = cache;
        }


        // Listar Get:
        public async Task<IActionResult> Index()
        {
            if (!_cache.TryGetValue("student", out list))
            {
                list = await _studentService.FindAllAsync();
                _cache.Set("student", list, cacheOptions);
            }
            else
            {
                list = _cache.Get("student") as List<Student>;
            }
            return View(list);
        }

        // Detalhar Get:
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id nulo" });
            }

            if (!_cache.TryGetValue("student", out list))
            {
                list = await _studentService.FindAllAsync();
                _cache.Set("student", list, cacheOptions);
            }
            else
            {
                list = _cache.Get("student") as List<Student>;
            }

            var obj = list.Find(x => x.Id == id);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não encontrado" });
            }

            return View(obj);
        }

        // Criar Get:
        public async Task<IActionResult> Create()
        {

            if (!_cache.TryGetValue("teacher", out teachers))
            {
                teachers = await _teacherService.FindAllAsync();
                _cache.Set("teacher", teachers, cacheOptions);
            }
            else
            {
                teachers = _cache.Get("teacher") as List<Teacher>;
            }

            if (!_cache.TryGetValue("objective", out objectives))
            {
                objectives = await _objectiveService.FindAllAsync();
                _cache.Set("objective", objectives, cacheOptions);
            }
            else
            {
                objectives = _cache.Get("objective") as List<Objective>;
            }

            ViewBag.teachers = new SelectList(teachers, "Id", "Name");
            ViewBag.objectives = new SelectList(objectives, "Id", "Name");

            return View();
        }

        // Criar Post: 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Student obj)
        {
            if (ModelState.IsValid)
            {
                TempData["confirm"] = obj.Name + " foi cadastrado com sucesso.";
                await _studentService.InsertAsync(obj);
                list = await _studentService.FindAllAsync();
                _cache.Set("student", list, cacheOptions);
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

            if (!_cache.TryGetValue("student", out list))
            {
                list = await _studentService.FindAllAsync();
                _cache.Set("student", list, cacheOptions);
            }
            else
            {
                list = _cache.Get("student") as List<Student>;
            }

            var obj = list.Find(x => x.Id == id);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não encontrado" });
            }

            if (!_cache.TryGetValue("teacher", out teachers))
            {
                teachers = await _teacherService.FindAllAsync();
                _cache.Set("teacher", teachers, cacheOptions);
            }
            else
            {
                teachers = _cache.Get("teacher") as List<Teacher>;
            }

            if (!_cache.TryGetValue("objective", out objectives))
            {
                objectives = await _objectiveService.FindAllAsync();
                _cache.Set("objective", objectives, cacheOptions);
            }
            else
            {
                objectives = _cache.Get("objective") as List<Objective>;
            }

            ViewBag.teachers = new SelectList(teachers, "Id", "Name");
            ViewBag.objectives = new SelectList(objectives, "Id", "Name");

            return View(obj);
        }

        // Editar Post:
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Student obj)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _studentService.UpdateAsync(obj);
                    TempData["confirm"] = obj.Name + " foi editado com sucesso.";
                    list = await _studentService.FindAllAsync();
                    _cache.Set("student", list, cacheOptions);

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

            if (!_cache.TryGetValue("student", out list))
            {
                list = await _studentService.FindAllAsync();
                _cache.Set("student", list, cacheOptions);
            }
            else
            {
                list = _cache.Get("student") as List<Student>;
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
                await _studentService.RemoveAsync(id);
                var obj = (_cache.Get("student") as List<Student>).Find(x => x.Id == id);
                TempData["confirm"] = obj.Name + " foi deletado com sucesso.";
                (_cache.Get("student") as List<Student>).Remove(obj);
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

        // Aluno já existe:
        public async Task<JsonResult> StudentExist(int? Id, string Name)
        {
            if (await _studentService.HasAnyName(Id, Name))
                return Json("aluno já cadastrado");
            return Json(true);
        }
    }
}