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
        private readonly ScarletWitchContext _dbContext;

        public DictionaryService(ScarletWitchContext databaseContext)
        {
            _dbContext = databaseContext;

        }
     
        //public List<DictionaryLandmarkTypeModel> GetDictionaryLandmarkTypeModels()
        //{
        //    List<DictionaryLandmarkTypeModel> dictionaryLandmarkTypeModels = _dbContext.DictionaryLandmarkType.Select(a => new DictionaryLandmarkTypeModel()
        //    {
        //        Id = a.DictionaryItemId,
        //        Name = a.DictionaryItemName
        //    }).ToList();

        //    return dictionaryLandmarkTypeModels;
        //}

                public List<DictionaryCityModel> GetDictionaryCities()
        {
            List<DictionaryCityModel> dictionaryCities = _dbContext.DictionaryCity.Select(a => new DictionaryCityModel()
            {
                Id = a.CityId,
                Name = a.CityName
            }
            ).ToList();
            return dictionaryCities;
        }
    }
}
