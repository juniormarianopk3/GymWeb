using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GymWeb.Data;
using GymWeb.Models;
using GymWeb.Models.ViewModels;
using AutoMapper;

namespace GymWeb.Controllers {
    public class TreinosController : Controller {
        private readonly ApplicationDbContext _context;
        public readonly IMapper _mapper;

        public TreinosController(ApplicationDbContext context,  Mapper imapper) {
            _context = context;
            _mapper = imapper;
        }

        // GET: Treinos
        public async Task<IActionResult> Index() {
            return View(await _context.Treinos.Include(p => p.Exercicios).ToListAsync());
        }

        // GET: Treinos/Details/5
        public async Task<IActionResult> Details(int? id) {
            if (id == null || _context.Treinos == null) {
                return NotFound();
            }

            var treino = await _context.Treinos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (treino == null) {
                return NotFound();
            }

            return View(treino);
        }

        // GET: Treinos/Create
        public IActionResult Create() {

            return View();
        }

        // POST: Treinos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Treino treino) {


            if (ModelState.IsValid) {
                _context.Add(treino);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(treino);
        }

        // GET: Treinos/Edit/5
        public async Task<IActionResult> Edit(int? id) {
            if (id == null || _context.Treinos == null) {
                return NotFound();
            }

            var treino = await _context.Treinos.FindAsync(id);
            if (treino == null) {
                return NotFound();
            }

            var ExercicioTreino = from c in _context.Exercicios
                                  select new {
                                      c.Id,
                                      c.Nome,
                                      Checked = ((from ce in _context.ExerciciosTreinos
                                                  where (ce.TreinoId == id) & (ce.ExercicioId == c.Id)
                                                  select ce).Count() > 0)
                                  };

            CreateTreinoViewModel modelTreino = new CreateTreinoViewModel();
            modelTreino.Id = id.Value;
            modelTreino.Nome = treino.Nome;
            modelTreino.Dias = treino.Dias;
            

            var checkExercicio = new List<CheckBoxExercicioViewModel>();

            foreach (var item in ExercicioTreino) {
                checkExercicio.Add(new CheckBoxExercicioViewModel {
                    Id = item.Id,
                    Nome = item.Nome,
                    Checked = item.Checked
                });
            }
            modelTreino.checkBoxExercicios = checkExercicio;



            return View(modelTreino);
        }

        // POST: Treinos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CreateTreinoViewModel treinoModel) {
            

            if (ModelState.IsValid) {

                  var treinoSelecionado = _context.Treinos.Find(treinoModel.Id);


                treinoSelecionado.Nome = treinoModel.Nome;
                treinoSelecionado.Dias = treinoModel.Dias;

                try {
                    foreach (var item in _context.ExerciciosTreinos) {
                        if (item.TreinoId == treinoModel.Id) {
                            _context.Entry(item).State = EntityState.Deleted;
                        }
                    }
                    foreach (var item in treinoModel.checkBoxExercicios) {
                        if (item.Checked) {
                            _context.ExerciciosTreinos.Add(new ExercicioTreino() {
                                Id= item.Id,
                                ExercicioId =item.Id
                                
                            });
                        }
                    }
            
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException) {
                    if (!TreinoExists(treinoModel.Id)) {
                        return NotFound();
                    }
                    else {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(treinoModel);
        }

        // GET: Treinos/Delete/5
        public async Task<IActionResult> Delete(int? id) {
            if (id == null || _context.Treinos == null) {
                return NotFound();
            }

            var treino = await _context.Treinos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (treino == null) {
                return NotFound();
            }

            return View(treino);
        }

        // POST: Treinos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) {
            if (_context.Treinos == null) {
                return Problem("Entity set 'ApplicationDbContext.Treinos'  is null.");
            }
            var treino = await _context.Treinos.FindAsync(id);
            if (treino != null) {
                _context.Treinos.Remove(treino);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TreinoExists(int id) {
            return _context.Treinos.Any(e => e.Id == id);
        }
    }
}
