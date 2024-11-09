using HopsHub.Api.DTOs;
using HopsHub.Api.Exceptions;
using HopsHub.Api.Models;
using HopsHub.Api.Services.Interfaces;
using HopsHub.Api.Helpers;
using HopsHub.Api.Repositories.Interfaces;

namespace HopsHub.Api.Services
{
    public class BrewerService : IBrewerService
    {
        private readonly IRepository<Brewer> _brewerRepository;

        public BrewerService(IRepository<Brewer> brewerRepository)
        {
            _brewerRepository = brewerRepository;
        }

        public async Task<List<Brewer>> GetBrewers()
        {
            return await _brewerRepository.GetAllAsync();
        }

        public async Task<Brewer> PostBrewer(BrewerDTO brewerDTO)
        {
            var brewer = new Brewer
            {
                Name = brewerDTO.Name,
                Url = brewerDTO.Url
            };

            var exist = await _brewerRepository.ExistAsync(b => b.Name.ToLower() == brewer.Name.ToLower());

            if (exist)
            {
                throw new EntityExistsException("Beer already exist");
            }

            await _brewerRepository.AddAsync(brewer);

            await _brewerRepository.SaveAsync();

            return brewer;
        }

        public async Task<Brewer> PutBrewer(UpdateBrewerDTO brewerDTO)
        {
            var brewer = await _brewerRepository.GetByIdAsync(brewerDTO.Id);

            if (brewer == null)
            {
                throw new EntityNotFoundException($"Brewer with id {brewerDTO.Id} not found in database");
            }

            UpdateHelper.UpdateEntity(brewerDTO, brewer);

            _brewerRepository.Update(brewer);

            await _brewerRepository.SaveAsync();

            return brewer;
        }

        public async Task<Brewer> DeleteBrewer(DeleteBrewerDTO brewerDTO)
        {
            var brewer = await _brewerRepository.GetByIdAsync(brewerDTO.Id);

            if (brewer == null)
            {
                throw new EntityNotFoundException($"Beer {brewerDTO.Id} not found in database");
            }

            UpdateHelper.UpdateEntity(brewerDTO, brewer);

            _brewerRepository.Update(brewer);
            await _brewerRepository.SaveAsync();

            return brewer;
        }
    }
}
