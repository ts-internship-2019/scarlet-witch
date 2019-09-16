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
        
        private readonly ScarletWitchContext _scwContext;

        public DictionaryService( ScarletWitchContext scarletWitchContext)
        {          
            _scwContext = scarletWitchContext;
        }

        public List<DictionaryCurrencyModel> GetDictionaryCurrencyModels()
        {
            List<DictionaryCurrencyModel> dictionaryCurrencyModels = _scwContext.DictionaryCurrency.Select(b => new DictionaryCurrencyModel()
            {
                CurrencyId = b.CurrencyId,
                CurrencyName = b.CurrencyName,
                CurrencyCode = b.CurrencyCode,
                CurrencyExchange = Convert.ToDecimal(b.CurrencyExchange)
                
            }).ToList();

        
            return dictionaryCurrencyModels;
            
        }
    }
}
