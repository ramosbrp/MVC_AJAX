using Microsoft.AspNetCore.Mvc;
using MVC_AJAX.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_AJAX.Controllers
{
    public class CategoriaController : Controller
    {
        private readonly ApplicationDbContext _contexto;
        public CategoriaController(ApplicationDbContext contexto)
        {
            _contexto = contexto;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetCategorias()
        {
            var categoria = _contexto.Categoria.ToList();

            return Json(new
            { Categoria = categoria });
        }

        [HttpGet]
        public IActionResult Editar(int id)
        {
            var categoria = _contexto.Categoria.First(c => c.Id == id);
            return View("Salvar", categoria);
        }

        [HttpGet]
        public IActionResult Salvar()
        {
            return View();
        }

        [HttpPost]
        public JsonResult PostCategorias(Categoria modelo)
        {
            try
            {
                if (modelo.Id == 0)
                    _contexto.Categoria.Add(modelo);
                else
                {
                    var categoria = _contexto.Categoria.First(c => c.Id == modelo.Id);
                    categoria.Nome = modelo.Nome;
                }

                _contexto.SaveChangesAsync();
                return Json(new { Success = true, Message = "Cadastro efetuado com sucesso." });
            }
            catch
            {
                return Json(new { Success = false, Message = "Deu merda." });
            }

        }

        //[HttpPost]
        //public async Task<IActionResult> Salvar(Categoria modelo)
        //{
        //    if (modelo.Id == 0)
        //        _contexto.Categoria.Add(modelo);
        //    else
        //    {
        //        var categoria = _contexto.Categoria.First(c => c.Id == modelo.Id);
        //        categoria.Nome = modelo.Nome;
        //    }

        //    await _contexto.SaveChangesAsync();
        //    return RedirectToAction("Index");
        //}

        public async Task<IActionResult> Deletar(int id)
        {
            var categoria = _contexto.Categoria.First(c => c.Id == id);
            _contexto.Categoria.Remove(categoria);
            await _contexto.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public int add(int number1, int number2)
        {
            return number1 + number2;
        }
    }
}
