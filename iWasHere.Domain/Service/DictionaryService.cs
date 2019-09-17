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

        public List<DictionaryCountyModel> GetDictionaryCounties()
        {
            List<DictionaryCountyModel> dictionaryCounties = _dbContext.DictionaryCounty.Select(a => new DictionaryCountyModel()
            {
                CountyId = a.CountyId,
                CountyName = a.CountyName
            }
            ).ToList();
            return dictionaryCounties;
        }
    }
}