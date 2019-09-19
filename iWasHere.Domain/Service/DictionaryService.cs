﻿using iWasHere.Domain.DTOs;
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

        public IQueryable<DictionaryCountyModel> GetDictionaryCounties()
        {
            IQueryable<DictionaryCountyModel> dictionaryCounties = _dbContext.DictionaryCounty.Select(a => new DictionaryCountyModel()
            {
                CountyId = a.CountyId,
                CountyName = a.CountyName,
                CountryName = _dbContext.DictionaryCountry.Where(c => c.CountryId == a.CountryId).Select(c => c.CountryName).FirstOrDefault().ToString()
            } );
            return dictionaryCounties;
        }

        public IQueryable<DictionaryCountyModel> GetDictionaryCountiesByName(string nameFilter)
        {
            if (nameFilter == null)
            {
                IQueryable<DictionaryCountyModel> dictionaryCounties = _dbContext.DictionaryCounty.Select(a => new DictionaryCountyModel()
                {
                    CountyId = a.CountyId,
                    CountyName = a.CountyName,
                    CountryName = _dbContext.DictionaryCountry.Where(c => c.CountryId == a.CountryId).Select(c => c.CountryName).FirstOrDefault().ToString()
                });
                return dictionaryCounties;
            }
            else
            {
                IQueryable<DictionaryCountyModel> dictionaryCounties = _dbContext.DictionaryCounty.Select(a => new DictionaryCountyModel()
                {
                    CountyId = a.CountyId,
                    CountyName = a.CountyName,
                    CountryName = _dbContext.DictionaryCountry.Where(c => c.CountryId == a.CountryId).Select(c => c.CountryName).FirstOrDefault().ToString()
                }
                ).Where(c => c.CountyName.Contains(nameFilter));
                return dictionaryCounties;
            }
        }

        public List<DictionaryCountyModel> GetDictionaryCountiesByCountry(string countryName)
        {
            List<DictionaryCountyModel> dictionaryCounties = _dbContext.DictionaryCounty.Select(a => new DictionaryCountyModel()
            {
                CountyId = a.CountyId,
                CountyName = a.CountyName,
                CountryName = _dbContext.DictionaryCountry.Where(c => c.CountryId == a.CountryId).Select(c => c.CountryName).FirstOrDefault().ToString()
            }
            ).Where(d => d.CountryName == countryName).ToList();

            return dictionaryCounties;
        }

        public List<DictionaryCountryModel> GetDictionaryCountriesByLanguage(string languageName)
        {
            List<DictionaryCountryModel> dictionaryCountries = _dbContext.DictionaryCountry.Select(a => new DictionaryCountryModel()
            {
                CountryId = a.CountryId,
                CountryName = a.CountryName,
                LanguageName = _dbContext.DictionaryLanguage.Where(c => c.LanguageId == a.LanguageId).Select(c => c.LanguageName).FirstOrDefault().ToString()
            }
            ).Where(d => d.LanguageName == languageName).ToList();

            return dictionaryCountries;
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
        public IQueryable<DictionaryLandmarkTypeModel> GetDictionaryLandmarkTypesFiltered(String landmarkTypeName)
        {
            if (landmarkTypeName == null)
            {
                IQueryable<DictionaryLandmarkTypeModel> dictionaryLandmarkTypes = _dbContext.DictionaryLandmarkType.Select(a => new DictionaryLandmarkTypeModel()
                {
                    LandmarkTypeCode = a.LandmarkTypeCode,
                    Description = a.Description
                });
                return dictionaryLandmarkTypes;
            }
            else
            {
                IQueryable<DictionaryLandmarkTypeModel> dictionaryLandmarkTypes = _dbContext.DictionaryLandmarkType.Select(a => new DictionaryLandmarkTypeModel()
                {
                    LandmarkTypeCode = a.LandmarkTypeCode,
                    Description = a.Description
                }
                ).Where(c => c.LandmarkTypeCode.Contains(landmarkTypeName));
                return dictionaryLandmarkTypes;
            }


        }

        public IQueryable<DictionaryCityModel> GetDictionaryCitiesFiltered(String cityName)
        {
            if (cityName==null)
            {
                IQueryable<DictionaryCityModel> dictionaryCities = _dbContext.DictionaryCity.Select(a => new DictionaryCityModel()
                {
                    Id = a.CityId,
                    Name = a.CityName,
                    County = _dbContext.DictionaryCounty.Where(c => c.CountyId == a.CountyId).Select(c => c.CountyName).FirstOrDefault().ToString()
                });
                return dictionaryCities;
            }
            else
            {
                IQueryable<DictionaryCityModel> dictionaryCities = _dbContext.DictionaryCity.Select(a => new DictionaryCityModel()
                {
                    Id = a.CityId,
                    Name = a.CityName,
                    County = _dbContext.DictionaryCounty.Where(c => c.CountyId == a.CountyId).Select(c => c.CountyName).FirstOrDefault().ToString()
                }
                ).Where(c => c.Name == cityName);
                return dictionaryCities;
            }
         
          
        }

        public void DeleteUsuarios(int id)
        {
            DictionaryCity city = new DictionaryCity() { CityId = id };

        _dbContext.DictionaryCity.Remove(city);
        _dbContext.SaveChanges();


        }

        public void DeleteLanguages(int id)
        {
            DictionaryLanguage language = new DictionaryLanguage() { LanguageId = id };

            _dbContext.DictionaryLanguage.Remove(language);
            _dbContext.SaveChanges();

        }

        public void DeleteCurrency(int id)
        {
            DictionaryCurrency currency = new DictionaryCurrency() { CurrencyId = id };

            _dbContext.DictionaryCurrency.Remove(currency);
            _dbContext.SaveChanges();

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

        public IQueryable<DictionaryCountryModel> GetDictionaryCountriesFiltered(String countryName)
        {
            if (countryName == null)
            {
                IQueryable<DictionaryCountryModel> dictionaryCountries = _dbContext.DictionaryCountry.Select(a => new DictionaryCountryModel()
                {
                    LanguageId = a.LanguageId,
                    CountryId = a.CountryId,
                    CountryName = a.CountryName,
                    //LanguageName = _dbContext.CountryXlanguage.Where(c => c.CountryId == a.CountryId).Where(d => d.LanguageId = c.LanguageId).Select(c => c.LanguageName).FirstOrDefault().ToString()
                });
                return dictionaryCountries;
            }
            else
            {
                IQueryable<DictionaryCountryModel> dictionaryCountry = _dbContext.DictionaryCountry.Select(a => new DictionaryCountryModel()
                {
                    LanguageId = a.LanguageId,
                    CountryId = a.CountryId,
                    CountryName = a.CountryName,
                    //LanguageName = _dbContext.DictionaryLanguage.Where(c => c.LanguageId == a.LanguageId).Select(c => c.LanguageName).FirstOrDefault().ToString()
                }
                ).Where(c => c.CountryName.Contains(countryName));
                return dictionaryCountry;
            }


        }

        public IQueryable<DictionaryCurrencyModel> GetDictionaryCurrencyFiltered(String currencyName)
        {
            if (currencyName == null)
            {
                IQueryable<DictionaryCurrencyModel> dictionaryCurrency = _dbContext.DictionaryCurrency.Select(c => new DictionaryCurrencyModel()
                {
                    CurrencyId = c.CurrencyId,
                    CurrencyCode = c.CurrencyCode,
                    CurrencyName = c.CurrencyName,
                    CurrencyExchange = Convert.ToDecimal(c.CurrencyExchange),
                    CountryName = _dbContext.DictionaryCountry.Where(a => a.CountryId == c.CountryId).Select(a => a.CountryName).FirstOrDefault().ToString()

                });
                return dictionaryCurrency;
            }
            else
            {
                IQueryable<DictionaryCurrencyModel> dictionaryCurrency = _dbContext.DictionaryCurrency.Select(c => new DictionaryCurrencyModel()
                {
                    CurrencyId = c.CurrencyId,
                    CurrencyCode = c.CurrencyCode,
                    CurrencyName = c.CurrencyName,
                    CurrencyExchange = Convert.ToDecimal(c.CurrencyExchange),
                    CountryName = _dbContext.DictionaryCountry.Where(a => a.CountryId == c.CountryId).Select(a => a.CountryName).FirstOrDefault().ToString()

                }
                ).Where(c => c.CurrencyName.Contains(currencyName));
                return dictionaryCurrency;
            }


        }



    }
}
