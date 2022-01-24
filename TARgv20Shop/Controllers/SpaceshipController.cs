using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Targv20Shop.Core.Dtos;
using Targv20Shop.Core.ServiceInterface;
using Targv20Shop.Data;
using Targv20Shop.Models.Spaceship;

namespace Targv20Shop.Controllers
{
    public class SpaceshipController : Controller
    {

        private readonly Targv20ShopDbContext _context;
        private readonly ISpaceshipService _spaceshipService;

        public SpaceshipController
            (
                Targv20ShopDbContext context,
                ISpaceshipService spaceshipService
            )
        {
            _context = context;
            _spaceshipService = spaceshipService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var result = _context.Spaceship
                .Select(x => new SpaceshipListViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Mass = x.Mass,
                    Crew = x.Crew,
                    Prize = x.Prize,
                    Type = x.Type,
                    ConstructedAt = x.ConstructedAt,
                    CreatedAt = x.CreatedAt,
                    ModifiedAt = x.ModifiedAt
                });

            return View(result);
        }

        [HttpGet]
        public IActionResult Add()
        {
            SpaceshipViewModel model = new SpaceshipViewModel();

            return View("Edit", model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(SpaceshipViewModel model)
        {
            var dto = new SpaceshipDto()
            {
                Id = model.Id,
                Crew = model.Crew,
                ConstructedAt = model.ConstructedAt,
                Mass = model.Mass,
                Name = model.Name,
                Prize = model.Prize,
                Type = model.Type,
                CreatedAt = model.CreatedAt,
                ModifiedAt = model.ModifiedAt,
                Files = model.Files,
                Image = model.Image.Select(x => new FileToDatabaseDto
                {
                    Id = x.Id,
                    ImageData = x.ImageData,
                    ImageTitle = x.ImageTitle,
                    SpaceshipId = x.SpaceshipId
                }).ToArray()
            };

            var result = await _spaceshipService.Add(dto);
            if (result == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var spaceship = await _spaceshipService.Edit(id);
            if (spaceship == null)
            {
                return NotFound();
            }

            var photos = await _context.FileToDatabase
                .Where(x => x.SpaceshipId == id)
                .Select(m => new ImagesViewModel
                {
                    ImageData = m.ImageData,
                    Id = m.Id,
                    Image = string.Format("data:image/gif;base64,{0}", Convert.ToBase64String(m.ImageData)),
                    ImageTitle = m.ImageTitle,
                    SpaceshipId = m.Id
                }).ToArrayAsync();

            var model = new SpaceshipViewModel();

            model.Id = spaceship.Id;
            model.Mass = spaceship.Mass;
            model.Name = spaceship.Name;
            model.Prize = spaceship.Prize;
            model.Type = spaceship.Type;
            model.Crew = spaceship.Crew;
            model.ConstructedAt = spaceship.ConstructedAt;
            model.CreatedAt = spaceship.CreatedAt;
            model.ModifiedAt = spaceship.ModifiedAt;
            model.Image.AddRange(photos);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(SpaceshipViewModel model)
        {
            var dto = new SpaceshipDto()
            {
                Id = model.Id,
                Crew = model.Crew,
                Mass = model.Mass,
                Name = model.Name,
                ConstructedAt = model.ConstructedAt,
                Prize = model.Prize,
                Type = model.Type,
                CreatedAt = model.CreatedAt,
                ModifiedAt = model.ModifiedAt,
                Image = model.Image.Select(x=> new FileToDatabaseDto 
                {
                    Id = x.Id,
                    ImageData = x.ImageData,
                    ImageTitle = x.ImageTitle,
                    SpaceshipId = x.SpaceshipId
                })
            };

            var result = await _spaceshipService.Update(dto);

            if (result == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index), model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var spaceship = await _spaceshipService.Delete(id);

            if (spaceship == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }
    }
}