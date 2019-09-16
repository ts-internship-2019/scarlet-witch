using iWasHere.Domain.DTOs;
using iWasHere.Domain.Model;
using iWasHere.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iWasHere.Domain.Service
{
    public class DictionaryService
    {
        private readonly ScarletWitchContext _dbContext2;
        public DictionaryService(ScarletWitchContext dataBaseContext2)
        private readonly ScarletWitchContext _dbContext;

        public DictionaryService(ScarletWitchContext databaseContext)
        {
            _dbContext2 = dataBaseContext2;
            _dbContext = databaseContext;

        }

        public List<DictionaryCountryModel> GetDictionaryCountryModels()
        public List<DictionaryCurrencyModel> GetDictionaryCurrencyModels()
        {
            List<DictionaryCountryModel> dictionaryCountryModels = _dbContext2.DictionaryCountry.Select(b => new DictionaryCountryModel()
            List<DictionaryCurrencyModel> dictionaryCurrencyModels = _scwContext.DictionaryCurrency.Select(b => new DictionaryCurrencyModel()
            {
                CountryId = b.CountryId,
                LanguageId = b.LanguageId,
                CountryName = b.CountryName
                CurrencyId = b.CurrencyId,
                CurrencyName = b.CurrencyName,
                CurrencyCode = b.CurrencyCode,
                CurrencyExchange = Convert.ToDecimal(b.CurrencyExchange)
                
            }).ToList();

            return dictionaryCountryModels;
        
            return dictionaryCurrencyModels;
            
        }
    }
}
