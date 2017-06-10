using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;

using FamousQuoteQuiz.Data.Repositories;
using FamousQuoteQuiz.Models;

namespace FamousQuoteQuiz.Services
{
    public class ModesService : IModesService
    {
        private IRepository<QuizMode> modesRepository;

        public ModesService(IRepository<QuizMode> modesRepository)
        {
            this.modesRepository = modesRepository;
        }

        public async Task<QuizModeType> GetSelectedMode()
        {
            var selectedMode = await modesRepository.GetAll()
                                                    .Where(x => x.IsSelected)
                                                    .FirstOrDefaultAsync();

            return selectedMode.Type;
        }

        public async Task<int> UpdateMode(QuizModeType newType)
        {
            var selectedMode = await modesRepository.GetAll()
                                                    .Where(x => x.IsSelected)
                                                    .FirstOrDefaultAsync();
            if (selectedMode.Type != newType)
            {
                selectedMode.IsSelected = false;
                var modeToBeSelected = await modesRepository.GetAll()
                                                            .Where(x => x.Type == newType)
                                                            .FirstOrDefaultAsync();
                modeToBeSelected.IsSelected = true;

                return await modesRepository.Update(modeToBeSelected);
            }

            return 0; //Nothing changed, zero rows affected.
        }
    }
}
