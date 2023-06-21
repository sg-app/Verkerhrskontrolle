using NUnit.Framework;
using Verkehrskontrolle.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Verkehrskontrolle.Models;
using Moq;
using Verkehrskontrolle.Data;
using Verkehrskontrolle.Interfaces;
using FluentAssertions;
using Moq.EntityFrameworkCore;
using System.Collections.ObjectModel;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Verkehrskontrolle.Services.Tests
{
    [TestFixture()]
    public class HalterAbfrageServiceTests
    {

        //[TestCaseSource]
        [Test()]
        public async Task GetFahrerlaubnisByKennzeichenUndFuehrerscheinnummerAsyncTest_GültigerInput_returnsTrue()
        {
            //Arrange
            var verkehrsKontrolleContext = new Mock<VerkehrskontrolleDbContext>();

            IList<Fahrzeug> fahrzeuge = new List<Fahrzeug>() {
                        new Fahrzeug()
                        {
                            Id = 0,
                            Antrieb = "Front",
                            Fahrzeugtyp = "PKW",
                            Sitze = 2,
                            Leistung = 250,
                            ZulassungDatum = DateTime.Now,
                            TüvDatum = DateTime.Now.AddDays(424),
                            Kennzeichen = "R-AL-12",
                            Halter = null,
                            HalterId = 2
                        },
                        new Fahrzeug()
                        {
                            Id = 1,
                            Antrieb = "Allrad",
                            Fahrzeugtyp = "LKW",
                            Sitze = 3,
                            Leistung = 322,
                            ZulassungDatum = DateTime.Now,
                            TüvDatum = DateTime.Now.AddDays(111),
                            Kennzeichen = "R-BB-22",
                            Halter = null,
                            HalterId = 2
                        },
                        new Fahrzeug()
                        {
                            Id = 2,
                            Antrieb = "",
                            Fahrzeugtyp = "Anhänger",
                            Sitze = 0,
                            Leistung = 0,
                            ZulassungDatum = DateTime.Now,
                            TüvDatum = DateTime.Now.AddDays(12),
                            Kennzeichen = "R-XY-55",
                            Halter = null,
                            HalterId = 2
                        }
            };

            IList<Führerschein> führerscheine = new List<Führerschein>() {
                        new Führerschein()
                        {
                            Id = 0,
                            Gültigkeit = DateTime.Now.AddYears(5),
                            PKWErlaublnis = true,
                            LKWErlaubnis = false,
                            AnhängerErlaubnis = false
                        },
                        new Führerschein()
                        {
                            Id = 1,
                            Gültigkeit = DateTime.Now.AddYears(5),
                            PKWErlaublnis = true,
                            LKWErlaubnis = true,
                            AnhängerErlaubnis = true
                        },
                        new Führerschein()
                        {
                            Id = 2,
                            Gültigkeit = DateTime.Now.AddYears(5),
                            PKWErlaublnis = true,
                            LKWErlaubnis = true,
                            AnhängerErlaubnis = false
                        }
            };

            verkehrsKontrolleContext.Setup(m => m.Fuehrerscheine).ReturnsDbSet(führerscheine);
            verkehrsKontrolleContext.Setup(x => x.Fahrzeuge).ReturnsDbSet(fahrzeuge);


            var sut = new HalterAbfrageService(verkehrsKontrolleContext.Object);

            //Act
            var result = await sut.GetFahrerlaubnisByKennzeichenUndFuehrerscheinnummerAsync(fahrzeuge.First().Kennzeichen, führerscheine.First().Id);

            //Assert
            result.Should().Be(true);

        }

        [Test()]
        public void GetFahrerlaubnisByKennzeichenUndFuehrerscheinnummerAsyncTest_UngültigerInput_returnsFalse()
        {
            //Arrange

            //Act

            //Assert
        }

        [Test()]
        public void GetFahrzeugtypErlaubnisByFuehrerscheinnummerAsync_GültigerInput_returnsCorrectList()
        {
            //Arrange

            //Act

            //Assert
        }
    }
}