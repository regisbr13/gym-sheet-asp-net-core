using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using GymSheet.Models;
using GymSheet.Models.ViewModels;
using GymSheet.Services;
using GymSheet.Services.Exceptions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace GymSheet.Controllers
{
    public class TeachersController : Controller
    {
        private readonly TeacherService _teacherService;
        private IHostingEnvironment _environment;
        private readonly IMemoryCache _cache;

        // Tempo de duração do Cache
        private readonly MemoryCacheEntryOptions cacheOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(60));
        // Lista para guardar cache
        private List<Teacher> list;

        public TeachersController(TeacherService teacherService, IHostingEnvironment environment, IMemoryCache cache)
        {
            _teacherService = teacherService;
            _environment = environment;
            _cache = cache;
        }

        // Listar Get:
        public async Task<IActionResult> Index()
        {
            if (!_cache.TryGetValue("teacher", out list))
            {
                list = await _teacherService.FindAllAsync();
                _cache.Set("teacher", list, cacheOptions);
            }
            else
            {
                list = _cache.Get("teacher") as List<Teacher>;
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

            if (!_cache.TryGetValue("teacher", out list))
            {
                list = await _teacherService.FindAllAsync();
                _cache.Set("teacher", list, cacheOptions);
            }
            else
            {
                list = _cache.Get("teacher") as List<Teacher>;
            }

            var obj = list.Find(x => x.Id == id);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não encontrado" });
            }

            return View(obj);
        }

        // Criar Get:
        public IActionResult Create()
        {
            return View();
        }

        // Criar Post: 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Teacher obj, IFormFile img)
        {
            if (ModelState.IsValid)
            {
                if(img != null)
                {
                    var path = Path.Combine(_environment.WebRootPath, "images");
                    using(var fs = new FileStream(Path.Combine(path, obj.Id.ToString() + img.FileName), FileMode.Create))
                    {
                        await img.CopyToAsync(fs);
                        obj.ImgPath = "~/images/" + obj.Id.ToString() + img.FileName;
                    }
                }
                else
                {
                    obj.ImgPath = "~/images/avatar-icon.png";
                }
                TempData["confirm"] = obj.Name + " foi cadastrado com sucesso.";
                await _teacherService.InsertAsync(obj);
                list = await _teacherService.FindAllAsync();
                _cache.Set("teacher", list, cacheOptions);
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

            if (!_cache.TryGetValue("teacher", out list))
            {
                list = await _teacherService.FindAllAsync();
                _cache.Set("teacher", list, cacheOptions);
            }
            else
            {
                list = _cache.Get("teacher") as List<Teacher>;
            }

            var obj = list.Find(x => x.Id == id);
            TempData["img"] = obj.ImgPath;
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não encontrado" });
            }

            return View(obj);
        }

        // Editar Post:
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Teacher obj, IFormFile img)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (img != null)
                    {
                        var path = Path.Combine(_environment.WebRootPath, "images");
                        using (var fs = new FileStream(Path.Combine(path, obj.Id.ToString() + img.FileName), FileMode.Create))
                        {
                            await img.CopyToAsync(fs);
                            obj.ImgPath = "~/images/" + obj.Id.ToString() + img.FileName;
                        }
                    }
                    else
                    {
                        obj.ImgPath = TempData["img"].ToString();
                    }

                    await _teacherService.UpdateAsync(obj);
                    TempData["confirm"] = obj.Name + " foi editado com sucesso.";
                    list = await _teacherService.FindAllAsync();
                    _cache.Set("teacher", list, cacheOptions);

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

            if (!_cache.TryGetValue("teacher", out list))
            {
                list = await _teacherService.FindAllAsync();
                _cache.Set("teacher", list, cacheOptions);
            }
            else
            {
                list = _cache.Get("teacher") as List<Teacher>;
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
                await _teacherService.RemoveAsync(id);
                var obj = (_cache.Get("teacher") as List<Teacher>).Find(x => x.Id == id);
                TempData["confirm"] = obj.Name + " foi deletado com sucesso.";
                (_cache.Get("teacher") as List<Teacher>).Remove(obj);
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
    }
}