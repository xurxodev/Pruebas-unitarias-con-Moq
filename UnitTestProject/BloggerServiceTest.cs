using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using UnitTestWithMoqExample;

namespace UnitTestProject
{
    [TestClass]
    public class BloggerServiceTest
    {
        [TestMethod]
        public void TestCreateInstance()
        {
            //creamos objetos dummy
            var mockLog = new Mock<ILog>();
            var mockHtmlValidator = new Mock<IHtmlValidator>();

            BlogService blog = new BlogService(mockHtmlValidator.Object, mockLog.Object);

            Assert.IsNotNull(blog);
        }

        [TestMethod]
        public void ShouldThrowArgumentExceptionTest()
        {
            //creamos objeto dummy
            var mockLog = new Mock<ILog>();
            
            //cuando se llame al método log, escribimos en el output
            mockLog.Setup(l => l.Log(It.IsAny<string>())).Callback<string>(param => System.Diagnostics.Debug.Write(param));

            var mockHtmlValidator = new Mock<IHtmlValidator>();

            //al validar html queremos comprobar como se comporta el blog si no es valido
            mockHtmlValidator.Setup(v => v.IsValid(It.IsAny<string>())).Returns(false);

            BlogService blog = new BlogService(mockHtmlValidator.Object, mockLog.Object);

            bool testOK = false;

            try
            {
                blog.PublicPost("<h1>Titulo");
            }
            catch (ArgumentException ex)
            {
                //el test es correcto
                testOK = true;
            }

            Assert.IsTrue(testOK);
        }

        [TestMethod]
        public void PublishOkTest()
        {
            //creamos objeto dummy
            var mockLog = new Mock<ILog>();

            //cuando se llame al método log, escribimos en el output
            mockLog.Setup(l => l.Log(It.IsAny<string>())).Callback<string>(param => System.Diagnostics.Debug.Write(param));

            var mockHtmlValidator = new Mock<IHtmlValidator>();

            //al validar html queremos comprobar como se comporta el blog si no es valido
            mockHtmlValidator.Setup(v => v.IsValid(It.IsAny<string>())).Returns(true);

            BlogService blog = new BlogService(mockHtmlValidator.Object, mockLog.Object);

            bool testOK = false;

            try
            {
                blog.PublicPost("<h1>Titulo</h1>");
            }
            catch (ArgumentException ex)
            {
                //el test es correcto
                testOK = false;
            }

            //se comprueba que se invoca el método IsValid
            mockHtmlValidator.Verify(validator => validator.IsValid(It.IsAny<string>()));

            Assert.IsTrue(testOK);
        }
    
    }
}
