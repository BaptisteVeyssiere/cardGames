// <copyright file="PexAssemblyInfo.cs">Copyright ©  2017</copyright>
using Microsoft.Pex.Framework.Coverage;
using Microsoft.Pex.Framework.Creatable;
using Microsoft.Pex.Framework.Instrumentation;
using Microsoft.Pex.Framework.Settings;
using Microsoft.Pex.Framework.Validation;

// Microsoft.Pex.Framework.Settings
[assembly: PexAssemblySettings(TestFramework = "VisualStudioUnitTest")]

// Microsoft.Pex.Framework.Instrumentation
[assembly: PexAssemblyUnderTest("client")]
[assembly: PexInstrumentAssembly("DotNetty.Codecs")]
[assembly: PexInstrumentAssembly("DotNetty.Codecs.Protobuf")]
[assembly: PexInstrumentAssembly("Google.Protobuf")]
[assembly: PexInstrumentAssembly("DotNetty.Transport")]

// Microsoft.Pex.Framework.Creatable
[assembly: PexCreatableFactoryForDelegates]

// Microsoft.Pex.Framework.Validation
[assembly: PexAllowedContractRequiresFailureAtTypeUnderTestSurface]
[assembly: PexAllowedXmlDocumentedException]

// Microsoft.Pex.Framework.Coverage
[assembly: PexCoverageFilterAssembly(PexCoverageDomain.UserOrTestCode, "DotNetty.Codecs")]
[assembly: PexCoverageFilterAssembly(PexCoverageDomain.UserOrTestCode, "DotNetty.Codecs.Protobuf")]
[assembly: PexCoverageFilterAssembly(PexCoverageDomain.UserOrTestCode, "Google.Protobuf")]
[assembly: PexCoverageFilterAssembly(PexCoverageDomain.UserOrTestCode, "DotNetty.Transport")]

