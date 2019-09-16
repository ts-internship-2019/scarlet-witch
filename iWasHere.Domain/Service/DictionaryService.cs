using iWasHere.Domain.DTOs;
using iWasHere.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iWasHere.Domain.Service
{
    public class DictionaryService
    {
        private readonly ScarletWitchContext _dbContext;

        public DictionaryService(ScarletWitchContext databaseContext)
        {
            _dbContext = databaseContext;
        }

        public List<DictionaryLandmarkTypeModel> GetDictionaryLandmarkTypeModels()
        {
            List<DictionaryLandmarkTypeModel> dictionaryLandmarkTypeModels = _dbContext.DictionaryLandmarkType.Select(a => new DictionaryLandmarkTypeModel()
            {
                Id = a.LandmarkTypeId,
                Name = a.LandmarkTypeCode
            }).ToList();

            return dictionaryLandmarkTypeModels;
        }


        public List<DictionaryLanguageModel> GetDictionaryLanguageModels()
        {
            List<DictionaryLanguageModel> dictionaryLanguageModels = _dbContext.DictionaryLanguage.Select(a => new DictionaryLanguageModel()
            {
                LanguageId = a.LanguageId,
                LanguageName = a.LanguageName,
                LanguageCode = a.LanguageCode
            }).ToList();

            return dictionaryLanguageModels;
        }
    }
}
