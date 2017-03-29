using RCIDRepository;
using RCIDRepository.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCIDService
{
    public interface IBirdService
    {
        IEnumerable<BirdSurveyor> GetAllSurveyors();
        IEnumerable<BirdSpecies> GetAllSpecies();
    }
}
