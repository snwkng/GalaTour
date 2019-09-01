using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GalaTour.Models;

namespace GalaTour
{
    public class SampleData
    {
        public static void Initialize(ExcursionContext context)
        {
            if (!context.Excursions.Any())
            {
                context.Excursions.AddRange(
                    new Excursion
                    {
                        Name = "Экскурсионный тур в Москву",
                        Description = "Однодневный экскурсионный тур в столицу нашей Родины",
                        ImageURL = "/images/crimea.jpg",
                        ExDurationID = 1,
                        ExCityID = 1,
                        ExDateID = 2,
                        ExPriceID = 1
                    },
                    new Excursion
                    {
                        Name = "Сказки Казани",
                        Description = "Незабываемое путешествие в Казань",
                        ImageURL = "/images/crimea.jpg",
                        ExDurationID = 2,
                        ExCityID = 2,
                        ExDateID = 1,
                        ExPriceID = 2
                    },
                    new Excursion
                    {
                        Name = "Санкт-Петербург - Северная столица России",
                        Description = "Посетите роскошные места Сантк-Петербурга, а также Петергоф",
                        ImageURL = "/images/crimea.jpg",
                        ExDurationID = 3,
                        ExCityID = 3,
                        ExDateID = 1,
                        ExPriceID = 3
                    }
                );
                if (!context.ExCityes.Any())
                {
                    context.ExCityes.AddRange(
                        new ExCity
                        {
                            City = "Москва"
                        },
                        new ExCity
                        {
                            City = "Казань"
                        },
                        new ExCity
                        {
                            City = "Санкт-Петербург"
                        }
                    );
                    if (!context.ExDurations.Any())
                    {
                        context.ExDurations.AddRange(
                            new ExDuration
                            {
                                Duration = 1
                            },
                            new ExDuration
                            {
                                Duration = 3
                            },
                            new ExDuration
                            {
                                Duration = 5
                            }
                        );
                        if (!context.ExDates.Any())
                        {
                            context.ExDates.AddRange(
                                new ExDate
                                {
                                    Date = new DateTime(2019, 7, 19)
                                },
                                new ExDate
                                {
                                    Date = new DateTime(2019, 9, 19)
                                },
                                new ExDate
                                {
                                    Date = new DateTime(2019, 10, 19)
                                }
                            );
                            if (!context.ExPrices.Any())
                            {
                                context.ExPrices.AddRange(
                                    new ExPrice
                                    {
                                        Price = 9560
                                    },
                                    new ExPrice
                                    {
                                        Price = 12000
                                    },
                                    new ExPrice
                                    {
                                        Price = 10200
                                    }
                                );
                                context.SaveChanges();
                            }
                        }
                    }
                }
            }
        }
    }
}
