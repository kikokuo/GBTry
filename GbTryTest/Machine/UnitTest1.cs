using Microsoft.VisualStudio.TestTools.UnitTesting;
using GbTry.Machine;
using System;
using System.Collections.Generic;
using System.Text;

namespace GbTry.Machine.Tests
{
    [TestClass()]
    public class UnitTest1
    {   
        private GbCPU gbCPU = new GbCPU();
        [TestMethod()]
        public void ExecuteRandomOpTest()
        {
            byte[] array = new byte[] { 0x00,0x01,0x33,0x80,0x02,0x03,0x04,0x05,06,01,0xcb,0x00,0xcb,0x01};
            UInt32[] g_bg_data = new UInt32[1];
            gbCPU.Init_Emu(ref array);
            gbCPU.debugflag = true;
            gbCPU.PowerOn(ref g_bg_data);
            Assert.AreEqual(gbCPU.BC.word, 0x0102);
        }
        [TestMethod()]
        public void ExecuteLD16RegTest()
        {
            byte[] array = new byte[] { 0x08, 0x75, 0x80};
            UInt32[] g_bg_data = new UInt32[1];
            gbCPU.Init_Emu(ref array);
            gbCPU.SP.word = 0x1112;
            gbCPU.debugflag = true;
            gbCPU.PowerOn(ref g_bg_data);
            Assert.AreEqual(gbCPU.Memory[0x8075], 0x12);
            Assert.AreEqual(gbCPU.Memory[0x8076], 0x11);
        }
        [TestMethod()]
        public void ExecuteADDHLTest()
        {
            byte[] array = new byte[] { 0x09,0x0A,0x0B,0x0C,0x0D,0x0E};
            UInt32[] g_bg_data = new UInt32[1];
            gbCPU.Init_Emu(ref array);
            gbCPU.HL.word = 0x1000;
            gbCPU.BC.word = 0x2000;
            gbCPU.debugflag = true;
            gbCPU.PowerOn(ref g_bg_data);
            Assert.AreEqual(gbCPU.HL.word, 0x3000);
            Assert.AreEqual(gbCPU.BC.word, 0x00FF);
        }

        [TestMethod()]
        public void ExecuteRRCTest()
        {
            byte[] array = new byte[] { 0xcb, 0x08, 0xcb, 0x09, 0xcb, 0x0a, 0xcb, 0x0b, 0xcb, 0x0c, 0xcb, 0x0d, 0xcb, 0x0e,0xcb,0x0f };
            UInt32[] g_bg_data = new UInt32[1];
            gbCPU.Init_Emu(ref array);
            gbCPU.HL.word = 0x0202;
            gbCPU.BC.word = 0x0202;
            gbCPU.AF.word = 0x0202;
            gbCPU.DE.word = 0x0202;
            gbCPU.debugflag = true;
            gbCPU.PowerOn(ref g_bg_data);
            Assert.AreEqual(gbCPU.HL.word, 0x0101);
            Assert.AreEqual(gbCPU.BC.word, 0x0101);
            Assert.AreEqual(gbCPU.AF.word, 0x0201);
            Assert.AreEqual(gbCPU.DE.word, 0x0101);
        }

        [TestMethod()]
        public void ExecuteRLCTest()
        {
            byte[] array = new byte[] { 0xcb, 0x00, 0xcb, 0x01, 0xcb, 0x02, 0xcb, 0x03, 0xcb, 0x04, 0xcb, 0x05, 0xcb, 0x06, 0xcb, 0x07 };
            UInt32[] g_bg_data = new UInt32[1];
            gbCPU.Init_Emu(ref array);
            gbCPU.HL.word = 0x0101;
            gbCPU.BC.word = 0x0101;
            gbCPU.AF.word = 0x0201;
            gbCPU.DE.word = 0x0101;
            gbCPU.debugflag = true;
            gbCPU.PowerOn(ref g_bg_data);
            Assert.AreEqual(gbCPU.HL.word, 0x0202);
            Assert.AreEqual(gbCPU.BC.word, 0x0202);
            Assert.AreEqual(gbCPU.AF.word, 0x0202);
            Assert.AreEqual(gbCPU.DE.word, 0x0202);
        }
        [TestMethod()]
        public void ExecuteBITTest()
        {
            byte[] array = new byte[] { 0xcb, 0x40,0xcb,0x42 };
            UInt32[] g_bg_data = new UInt32[1];
            gbCPU.Init_Emu(ref array);
            gbCPU.HL.word = 0x0101;
            gbCPU.BC.word = 0x0000;
            gbCPU.AF.word = 0x0201;
            gbCPU.DE.word = 0x0101;
            gbCPU.debugflag = true;
            gbCPU.PowerOn(ref g_bg_data);
            Assert.AreEqual(gbCPU.GetFlagZ(), 0x00);
        }
        [TestMethod()]
        public void ExecuteResTest()
        {
            byte[] array = new byte[] { 0xcb, 0xa8, 0xcb, 0xa9, 0xcb, 0xaa, 0xcb, 0xab, 0xcb, 0xac, 0xcb, 0xad, 0xcb, 0xae, 0xcb, 0xaf };
            UInt32[] g_bg_data = new UInt32[1];
            gbCPU.Init_Emu(ref array);
            gbCPU.HL.word = 0x2020;
            gbCPU.BC.word = 0x2020;
            gbCPU.AF.word = 0x0220;
            gbCPU.DE.word = 0x2020;
            gbCPU.debugflag = true;
            gbCPU.PowerOn(ref g_bg_data);
            Assert.AreEqual(gbCPU.BC.word, 0x0000);
            Assert.AreEqual(gbCPU.DE.word, 0x0000);
            Assert.AreEqual(gbCPU.HL.word, 0x0000);
            Assert.AreEqual(gbCPU.AF.word, 0x0200);
        }
        [TestMethod()]
        public void ExecuteSetTest()
        {
            byte[] array = new byte[] { 0xcb, 0xf8, 0xcb, 0xf9, 0xcb, 0xfa, 0xcb, 0xfb, 0xcb, 0xfc, 0xcb, 0xfd, 0xcb, 0xfe, 0xcb, 0xff };
            UInt32[] g_bg_data = new UInt32[1];
            gbCPU.Init_Emu(ref array);
            gbCPU.HL.word = 0x0000;
            gbCPU.BC.word = 0x0000;
            gbCPU.AF.word = 0x0200;
            gbCPU.DE.word = 0x0000;
            gbCPU.debugflag = true;
            gbCPU.PowerOn(ref g_bg_data);
            Assert.AreEqual(gbCPU.BC.word, 0x8080);
            Assert.AreEqual(gbCPU.DE.word, 0x8080);
            Assert.AreEqual(gbCPU.HL.word, 0x8080);
            Assert.AreEqual(gbCPU.AF.word, 0x0280);
        }
    }
}