using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GymWeb.Data;
using GymWeb.Models;
using System.Net.Http.Headers;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using Microsoft.Extensions.Options;
using GymWeb.Models.Utils;
using Microsoft.AspNetCore.Hosting;
using GymWeb.Models.ViewModels;
using AutoMapper;

namespace GymWeb.Controllers
{
    public class ExerciciosController : Controller
    {
        private const int V = 1;
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hosting;
        private  ConfigurationImages _myconfiguration { get; set; }
        public readonly IMapper _mapper;


        public ExerciciosController(ApplicationDbContext context, IWebHostEnvironment hosting, IOptions<ConfigurationImages> MyConfiguration, IMapper mapper)
        {
            _context = context;
            _hosting = hosting;
            _myconfiguration = MyConfiguration.Value;
            _mapper = mapper;
        }

        // GET: Exercicios
        public async Task<IActionResult> Index()
        {
              return View(await _context.Exercicios.ToListAsync());
        }

        // GET: Exercicios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Exercicios == null)
            {
                return NotFound();
            }

            var exercicio = await _context.Exercicios
                .FirstOrDefaultAsync(m => m.Id == id);
            if (exercicio == null)
            {
                return NotFound();
            }

            return View(exercicio);
        }

        // GET: Exercicios/Create
        public IActionResult Create()
        {
            ViewBag.AreaCorporal = ExercicioViewModel.GetAreaCorporal().Select(c => new SelectListItem() { Text = c.AreaCorporal, Value = c.AreaCorporal }).ToList();
            return View();
        }

        // POST: Exercicios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ExercicioViewModel model) {
            if (ModelState.IsValid)
            {
                var map = _mapper.Map<Exercicio>(model);

                string ImageExercecio = UploadedFile(model);
                string musculoAtivoImage = UploadedImageMusculoAlvo(model);

                map.Image = ImageExercecio;
                map.ImageMusculoAlvo = musculoAtivoImage;
                
                _context.Add(map);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        // GET: Exercicios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Exercicios == null)
            {
                return NotFound();
            }

            var exercicio = await _context.Exercicios.FindAsync(id);
            
            ExercicioViewModel model = new ExercicioViewModel {


                Id = exercicio.Id,
                Nome = exercicio.Nome,
                AreaCorporal = exercicio.AreaCorporal,
                QntSerie = exercicio.QntSerie,
                Repeticao = exercicio.Repeticao,
                Dicas = exercicio.Dicas,
                Execucao = exercicio.Execucao,
                Preparacao = exercicio.Preparacao,
                MusculoPrimario = exercicio.MusculoPrimario,
                MusculoSecundario = exercicio.MusculoSecundario,
                             
            };

            ViewBag.AreaCorporal = ExercicioViewModel.GetAreaCorporal().Select(c => new SelectListItem() { Text = c.AreaCorporal, Value = c.AreaCorporal }).ToList();

            if (exercicio == null)
            {
                return NotFound();
            }
            return View(model);
        }

        // POST: Exercicios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ExercicioViewModel model)
        {
         
            if (id != model.Id)
            {
                return NotFound();
            }

            var map = _mapper.Map<Exercicio>(model);
            string ImageExercecio = UploadedFile(model);
            string musculoAtivoImage = UploadedImageMusculoAlvo(model);
            map.Image = ImageExercecio;
            map.ImageMusculoAlvo = musculoAtivoImage;

            if (ModelState.IsValid)
            {
        
                try
                {
                    _context.Update(map);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExercicioExists(model.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: Exercicios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Exercicios == null)
            {
                return NotFound();
            }

            var exercicio = await _context.Exercicios
                .FirstOrDefaultAsync(m => m.Id == id);
            if (exercicio == null)
            {
                return NotFound();
            }

            return View(exercicio);
        }

        // POST: Exercicios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Exercicios == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Exercicio'  is null.");
            }
            var exercicio = await _context.Exercicios.FindAsync(id);
            if (exercicio != null)
            {
                _context.Exercicios.Remove(exercicio);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        private string UploadedFile(ExercicioViewModel model) {
            string? nomeUnicoArquivo = null;
            if (model.Image != null) {
                string pastaFotos = Path.Combine(_hosting.WebRootPath, "Images/workout");
                nomeUnicoArquivo = Guid.NewGuid().ToString() + "_" + model.Image.FileName;
                string caminhoArquivo = Path.Combine(pastaFotos, nomeUnicoArquivo);
                using (var fileStream = new FileStream(caminhoArquivo,FileMode.CreateNew)) {
                    model.Image.CopyTo(fileStream);
                }
            }
            return nomeUnicoArquivo;
        }

        private string UploadedImageMusculoAlvo(ExercicioViewModel model) {
            string? nomeUnicoArquivo = null;
            if (model.ImageMusculoAlvo != null) {
                string pastaFotos = Path.Combine(_hosting.WebRootPath, "images/MusculoAtivo");
                nomeUnicoArquivo = Guid.NewGuid().ToString() + "_" + model.ImageMusculoAlvo.FileName;
                string caminhoArquivo = Path.Combine(pastaFotos, nomeUnicoArquivo);
                using (var fileStream = new FileStream(caminhoArquivo, FileMode.CreateNew)) {
                    model.ImageMusculoAlvo.CopyTo(fileStream);
                }
            }
            return nomeUnicoArquivo;
        }

        private bool ExercicioExists(int id)
        {
          return _context.Exercicios.Any(e => e.Id == id);
        }
    }
}
