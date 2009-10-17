using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using JSmith.Mvvm;
using JSmith.Mvvm.View;

namespace MvvmTests
{
    [TestFixture]
    public class MvvmLocatorTests
    {
        [Test]
        public void TestSingleton()
        {
            MvvmLocator<IView> locator = MvvmLocator<IView>.Instance;

            Assert.IsNotNull(locator);

        }//end method

    }//end class

}//end namespace