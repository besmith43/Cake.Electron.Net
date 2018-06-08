﻿using System;
using Cake.Core;
using Cake.Electron.Net.Commands;
using Cake.Electron.Net.Commands.Settings;
using Cake.Electron.Net.Contracts;
using Cake.Electron.Net.Tests.Mocks;
using Cake.Electron.Net.Utils;
using Moq;
using Xunit;

namespace Cake.Electron.Net.Tests
{
    public class ElectronNetInitializerTests
    {
        private const string CmdBase = "dotnet electronize init";

        private readonly Mock<ICakeContext> _cakeContextMock;

        public ElectronNetInitializerTests()
        {
            _cakeContextMock = new Mock<ICakeContext>(MockBehavior.Strict);
        }

        [Fact]
        public void ElectronNetInit_Should_Throw_ArgumentNullException_If_WorkingDirectory_Is_Null()
        {
            ICakeContext cakeContext = _cakeContextMock.Object;

            Assert.Throws<ArgumentNullException>(() => cakeContext.ElectronNetInit(null, null));
        }

        [Fact]
        public void ElectronNetInit_Should_Execute_Command_By_Given_WorkingDirectory()
        {
            ICakeContext cakeContext = _cakeContextMock.Object;

            string workingDirectory = "./SomeDirectory";

            var processHelperMock = new Mock<IProcessHelper>(MockBehavior.Strict);
            ElectroCakeContext.Current = new TestElectroCakeContext(processHelperMock, null);

            processHelperMock.Setup(helper => helper.CmdExecute(It.Is<string>(s => s == CmdBase), It.Is<string>(s => s == workingDirectory), It.IsAny<bool>(), It.IsAny<bool>())).Returns(1);

            cakeContext.ElectronNetInit(workingDirectory);

            processHelperMock.Verify(helper => helper.CmdExecute(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()));
        }

        [Fact]
        public void ElectronNetInit_Should_Execute_Command_By_Given_WorkingDirectory_And_Path()
        {
            ICakeContext cakeContext = _cakeContextMock.Object;

            string workingDirectory = "./SomeDirectory";
            string path = "./OtherPath";

            var processHelperMock = new Mock<IProcessHelper>(MockBehavior.Strict);
            ElectroCakeContext.Current = new TestElectroCakeContext(processHelperMock, null);
            processHelperMock.Setup(helper => helper.CmdExecute(It.Is<string>(s => s == $"{CmdBase} {path}"), It.Is<string>(s => s == workingDirectory), It.IsAny<bool>(), It.IsAny<bool>())).Returns(1);

            cakeContext.ElectronNetInit(workingDirectory, path);

            processHelperMock.Verify(helper => helper.CmdExecute(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()), Times.Once);
        }

        [Fact]
        public void ElectronNetInit_Should_Throw_ArgumentNullException_If_Settings_Is_Null()
        {
            ICakeContext cakeContext = _cakeContextMock.Object;

            Assert.Throws<ArgumentNullException>(() => cakeContext.ElectronNetInit(null));
        }

        [Fact]
        public void ElectronNetInit_Should_Execute_Command_By_Given_ElectronNetInitSettings()
        {
            ICakeContext cakeContext = _cakeContextMock.Object;

            ElectronNetInitSettings electronNetInitSettings = new ElectronNetInitSettings()
            {
                WorkingDirectory = "./SomeDirectory",
                Path = "./OtherPath"
            };

            var processHelperMock = new Mock<IProcessHelper>(MockBehavior.Strict);
            ElectroCakeContext.Current = new TestElectroCakeContext(processHelperMock, null);
            processHelperMock.Setup(helper => helper.CmdExecute(It.Is<string>(s => s == $"{CmdBase} {electronNetInitSettings.Path}"), It.Is<string>(s => s == electronNetInitSettings.WorkingDirectory), It.IsAny<bool>(), It.IsAny<bool>())).Returns(1);

            cakeContext.ElectronNetInit(electronNetInitSettings);

            processHelperMock.Verify(helper => helper.CmdExecute(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()), Times.Once);
        }
    }
}
