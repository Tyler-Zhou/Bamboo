using Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICharacterService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="character"></param>
        void Save(Character character);
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        List<CharacterSummary> GetSavedCharacterSummaries();
    }
}
