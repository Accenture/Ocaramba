echo '********************************************Executing tests********************************************'
        
echo '********************************************NUnit tests********************************************'

dotnet vstest .\Ocaramba.Tests.Angular\bin\Debug\netcoreapp3.1\Ocaramba.Tests.Angular.dll .\Ocaramba.Tests.NUnit\bin\Debug\netcoreapp3.1\Ocaramba.Tests.NUnit.dll .\Ocaramba.UnitTests\bin\Debug\netcoreapp3.1\Ocaramba.UnitTests.dll --logger:"trx;LogFileName=Ocaramba.Tests.netcoreapp3.xml"
