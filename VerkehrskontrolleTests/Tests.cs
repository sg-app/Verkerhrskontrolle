using Verkehrskontrolle.Controllers;
using Verkehrskontrolle.Interfaces;

namespace VerkehrskontrolleTests
{
    public class Tests
    {




        [Test]
        public void GetFahrerlaubnisByKennzeichenUndFuehrerscheinnummerAsync_ShouldBeTrue()
        {
            //Arrange



            var mok = new Mock<IHalterAbfrageService>();
            var kennzeichen = "R-A-1234";

            mok.Setup(m => m.GetFahrerlaubnisByKennzeichenUndFuehrerscheinnummerAsync(kennzeichen, 1)).ReturnsAsync(true);


            //Act
            var respones = new HalterAbfrageController(mok.Object);

            var result = respones.GetFahrzeugtypErlaubt(kennzeichen, 1);


            //Assert
            Assert.That(result, Is.Not.Null);

        }
    }
}