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

        public List<DictionaryCountryModel> GetDictionaryCountryModels()
        {
            List<DictionaryCountryModel> dictionaryCountryModels = _dbContext.DictionaryCountry.Select(b => new DictionaryCountryModel()
            {
                CountryId = b.CountryId,
                LanguageId = b.LanguageId,
                CountryName = b.CountryName
            }).ToList();

            return dictionaryCountryModels;
        }

        public IQueryable<DictionaryCityModel> GetDictionaryCities()
        {
            IQueryable<DictionaryCityModel> dictionaryCities = _dbContext.DictionaryCity.Select(a => new DictionaryCityModel()
            {
                Id = a.CityId,
                Name = a.CityName
            }
            );
            return dictionaryCities;
        }

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
        public List<DictionaryCurrencyModel> GetDictionaryCurrencyModels()
        {

            List<DictionaryCurrencyModel> dictionaryCurrencyModels = _dbContext.DictionaryCurrency.Select(b => new DictionaryCurrencyModel()
            {
                CurrencyId = b.CurrencyId,
                CurrencyName = b.CurrencyName,
                CurrencyCode = b.CurrencyCode,
                CurrencyExchange = Convert.ToDecimal(b.CurrencyExchange)
                
            }).ToList();

            return dictionaryCurrencyModels;
            
        }
        public List<DictionaryLandmarkTypeModel> GetDictionaryLandmarkTypeModels()
        {
            List<DictionaryLandmarkTypeModel> dictionaryLandmarkTypeModels = _dbContext.DictionaryLandmarkType.Select(a => new DictionaryLandmarkTypeModel()
            {
                LandmarkTypeCode = a.LandmarkTypeCode,
                Description = a.Description
            }).ToList();

            return dictionaryLandmarkTypeModels;
        }

        public IQueryable<DictionaryCityModel> GetDictionaryCitiesFiltered(String cityName)
        {
            if (cityName==null)
            {
                IQueryable<DictionaryCityModel> dictionaryCities = _dbContext.DictionaryCity.Select(a => new DictionaryCityModel()
                {
                    Id = a.CityId,
                    Name = a.CityName
                });
                return dictionaryCities;
            }
            else
            {
                IQueryable<DictionaryCityModel> dictionaryCities = _dbContext.DictionaryCity.Select(a => new DictionaryCityModel()
                {
                    Id = a.CityId,
                    Name = a.CityName
                }
                ).Where(c => c.Name == cityName);
                return dictionaryCities;
            }
         
          
        }


        public IQueryable<DictionaryLanguageModel> GetDictionaryLanguagesFiltered(String languageName)
        {
            if (languageName == null)
            {
                IQueryable<DictionaryLanguageModel> dictionaryLanguage = _dbContext.DictionaryLanguage.Select(a => new DictionaryLanguageModel()
                {
                    LanguageId = a.LanguageId,
                    LanguageCode=a.LanguageCode,
                    LanguageName=a.LanguageName
                });
                return dictionaryLanguage;
            }
            else
            {
                IQueryable<DictionaryLanguageModel> dictionaryLanguage = _dbContext.DictionaryLanguage.Select(a => new DictionaryLanguageModel()
                {
                    LanguageId = a.LanguageId,
                    LanguageCode = a.LanguageCode,
                    LanguageName = a.LanguageName
                }
                ).Where(c => c.LanguageName.Contains(languageName));
                return dictionaryLanguage;
            }


        }



    }
}
