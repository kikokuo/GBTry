using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Input;

namespace GbTry.Machine
{
    [StructLayout(LayoutKind.Explicit)]
    public struct Reg
    {
        [FieldOffset(1)]
        public byte lowbyte;
        [FieldOffset(0)]
        public byte highbyte;
        [FieldOffset(0)]
        public ushort word;
    }

    public struct JoyPad
    { 
        public byte a;
        public byte b;
        public byte select;
        public byte start;
        public byte right;
        public byte left;
        public byte up;
        public byte down;
    }

    public class GbCPU
    {
        // little endain
        public Reg AF, BC, DE, HL;
        public Reg SP, PC;
        public bool Halt = false;
        public bool Running = true;
        public int Cycle = 0;
        public bool bIRQ = true;
        public bool debugflag = false;
        public int Rom_Length = 0;
        private LR35902 Lr35902 = new LR35902();
        public PPU ppu = new PPU();
        public String commandstring;
        private ushort GoToPC = 0;
        private bool SingleStep = false;
        public bool stop = false;
        private ushort IO_P1 = 0xFF00;
        private ushort INTF = 0xFF0F;
        private ushort INTE = 0xFFFF;
        private ushort REG_TIM_DIV = 0xFF04;
        private ushort REG_TIM_TIMA = 0xFF05;
        private ushort REG_TIM_TMA = 0xFF06;
        private ushort REG_TIM_TAC = 0xFF07;
        // MBC type
        private ushort REG_MBC = 0x0147;
        private bool EnableBoot;
        private int timer_internal_div = 0;
        private int timer_internal_cnt = 0;
        private bool OAMEnable = false;
        private byte mbc_mode = 0;
        private byte ram_bank_no = 0;
        private byte rom_bank_no = 0;
        private ushort ram_offs = 0x0000;
        private uint rom_offs = 0x4000;
        private byte mbc = 0;

        public byte[] Memory = new byte[1024*1024];
        public byte[] ram = new byte[0x2000];
        public byte[] rom = new byte[0x100];
        public byte[] vram = new byte[0x2000];
        public byte[] oam = new byte[0x100];
        public byte[] hram = new byte[0x100];
        public byte[] eram = new byte[0x10000];
        public JoyPad keys = new JoyPad();

        public void Init()
        {
            Lr35902.GetGbCPU(this);
            ppu.GetGbCPU(this);

        }
        public void User_Input(int kevvaule, byte data)
        {
            switch(kevvaule)
            {
                case 0x01:
                    if(data == 1)
                        keys.select = 0x01;
                    else
                        keys.select = 0x00;
                    break;
                case 0x02:
                    if (data == 1)
                        keys.start = 0x01;
                    else
                        keys.start = 0x00;
                    break;
                case 0x04:
                    if (data == 1)
                        keys.a = 0x01;
                    else
                        keys.a = 0x00;
                    break;
                case 0x08:
                    if (data == 1)
                        keys.b = 0x01;
                    else
                        keys.b = 0x00;
                    break;
                case 0x10:
                    if (data == 1)
                        keys.left = 0x01;
                    else
                        keys.left = 0x00;
                    break;
                case 0x20:
                    if (data == 1)
                        keys.right = 0x01;
                    else
                        keys.right = 0x00;
                    break;
                case 0x40:
                    if (data == 1)
                        keys.down = 0x01;
                    else
                        keys.down = 0x00;
                    break;
                case 0x80:
                    if (data == 1)
                        keys.up = 0x01;
                    else
                        keys.up = 0x00;
                    break;
            }
        }

        public void EI()
        {
            bIRQ = true;
        }
        public void DI()
        {
            bIRQ = false;
        }

        public ushort POP()
        {
            return  (ushort) (GetValueFromMemory(SP.word++) | (GetValueFromMemory(SP.word++)<<8));
        }

        public void PUSH(ushort value)
        {
            SetValueIntoMemory(--SP.word, (byte)((value >> 8) & 0xFF));
            SetValueIntoMemory(--SP.word, (byte)(value & 0xFF));
        }
        public void SetGotoPC(String address)
        {
            GoToPC = Convert.ToUInt16(address,16);
            Running = true;
        }
        public void SetSingle()
        {
            SingleStep = true;
        }
        public void IncCycle(int count)
        {
            Cycle += count;
        }

        public void SetFlagC(bool set)
        {
            byte s = 0x10;
            if(set)
                AF.highbyte |= s;
            else
                AF.highbyte &= (byte)(~s);
        }
        public void SetFlagH(bool set)
        {
            byte s = 0x20;
            if (set)
                AF.highbyte |= s;
            else
                AF.highbyte &= (byte)(~s);
        }
        public void SetFlagN(bool set)
        {
            byte s = 0x40;
            if (set)
                AF.highbyte |= s;
            else
                AF.highbyte &= (byte)(~s);
        }
        public void SetFlagZ(bool set)
        {
            byte s = 0x80;
            if (set)
                AF.highbyte |= s;
            else
                AF.highbyte &= (byte)(~s);
        }

        public byte GetFlagC()
        {
            return (byte)((AF.highbyte & 0x10) == 0x10 ? 0x01 : 0x00);
        }
        public byte GetFlagH()
        {
            return (byte)((AF.highbyte & 0x20) == 0x20 ? 0x01 : 0x00);
        }
        public byte GetFlagN()
        {
            return (byte)((AF.highbyte & 0x40) == 0x40 ? 0x01 : 0x00);
        }
        public byte GetFlagZ()
        {
            return (byte)((AF.highbyte & 0x80) == 0x80 ? 0x01:0x00);
        }

        public byte GetValueFromMemory(ushort address)
        {
            if (address < 0x4000)
                return Memory[address];
            else if (address < 0x8000)
                return Memory[(uint)(address & 0x3fff) + rom_offs];
            else if (address < 0xa000)
                return vram[address & 0x1fff];
            else if (address < 0xc000)
                return eram[ram_offs + (address & 0x1fff)];
            else
            {
                if (address < 0xfe00) return ram[address & 0x1fff];
                else if (address < 0xff00) return oam[address & 0xff];
                else return hram[address & 0xff];
            }
        }

        public void SetValueIntoMemory(ushort address, byte value)
        {
            if (address < 0x2000) // external ram switch
            {
                /*switch (val)
                {
                    case 2:
                    case 3:
                        ram_on = ((value & 0x0f) == 0x0a) ? 1 : 0;
                        break;
                }*/
                return;
            }
            else if (address < 0x4000) // rom bank select
            {
                if (mbc >= 1 && mbc <= 3)
                {
                    var val1 = value & 0x1f;
                    val1 = (byte)(val1 > 0 ? val1 : 1);
                    rom_bank_no = (byte)((rom_bank_no & 0x60) + val1);
                    rom_offs = (uint)(rom_bank_no) * 0x00004000;
                }
            }
            else if (address < 0x6000)// ram select
            {
                if (mbc >= 1 && mbc <= 3) 
                { 
                    if (mbc_mode != 0)
                    {
                        ram_bank_no = (byte)(value & 3);
                        ram_offs = (ushort)(ram_bank_no * 0x2000);
                    } // ram mode
                    else
                    {
                        rom_bank_no = (byte)(rom_bank_no & 0x1f + ((value & 3) << 5));
                        rom_offs = (uint)(rom_bank_no) * 0x00004000;
                    } // rom mode
                }
            }
            else if (address < 0x8000) //mode switch
            {  
                if (mbc >= 2 && mbc <= 3)
                 mbc_mode = (byte)(value & 1);
            }
            else if (address < 0xa000) // gpu
                vram[address & 0x1fff] = value;
            else if (address < 0xc000) // external ram
                eram[ram_offs + (address & 0x1fff)] = value;
            else
            {
                if (address < 0xfe00) ram[address & 0x1fff] = value;
                else
                if (address < 0xff00) oam[address & 0xff] = value;
                else
                {
                    hram[address & 0xff] = value;
                    if (address == 0xff46) { OAM_RAM(value); }
                }
            }
        }
        private void OAM_RAM(byte value)
        { // OAM 'dma' transfer
            if (value > 0)
            {
                ushort a = (ushort)(value << 8);
                for (byte i = 0; i < 160; i++) { 
                    byte v = GetValueFromMemory((ushort)(a + i)); 
                    SetValueIntoMemory((ushort)(0xfe00 + i), v); 
                } // oam mem space
                SetValueIntoMemory(0xff46,0);
                OAMEnable = true;
            }
        }

        public void Init_Emu(ref byte [] rom)
        {

            Array.Clear(Memory, 0,64*1024);
            Array.Copy(rom,0, Memory, 0, rom.Length);
            Rom_Length = rom.Length;
        }

        public byte fetch()
        {    
            byte val = GetValueFromMemory(PC.word);
             PC.word++;
            return val;
        }

        public unsafe void Tick(int speed)
        {
            if (GoToPC != 0)
            {
                if (PC.word == GoToPC)
                {
                    GoToPC = 0;
                    Running = false;
                }
                else
                {
                    Running = true;
                }
            }
            else
            {
                if (debugflag )
                {
                    Running = false;
                    if (SingleStep)
                    {
                        SingleStep = false;
                        Running = true;
                    }
                }

            }
            if (Running) {
                CheckInput();
                if (!Halt)
                {
                    Cycle = 0;
                    while (speed != 0)
                    {
                        byte opcode = fetch();
                        Lr35902.ExecuteOP(opcode);
                        if (OAMEnable)
                        {
                            Cycle += 671;
                            OAMEnable = false;
                        }
                        speed--;
                    }
                } 
                Timer_step();
                ppu.Render();
                Interrupts();
            }
        }
        public void SetBootImage(bool enableboot)
        {
            EnableBoot = enableboot;
        }

        public void PowerOn(ref UInt32[] g_data)
        {
            PC.word = 0x0100;
            AF.word = 0x01b0;
            BC.word = 0x0013;
            DE.word = 0x00D8;
            HL.word = 0x014D;
            SP.word = 0xFFFE;
            SetValueIntoMemory(0xFF50, 0x01); //boot
            if (EnableBoot) { 
                PC.word = 0x0000;
                AF.word = 0; BC.word = 0; DE.word = 0;
                HL.word = 0; SP.word = 0; 
                SetValueIntoMemory(0xFF50, 0x00); //boot
            }
            SetValueIntoMemory(0xFF40, 0x91); // lcdc register
            SetValueIntoMemory(0xFF41, 0x81); // lcdc register
            SetValueIntoMemory(0xFF44, 0x90); // lcdc register
                                              // init sound regs
            SetValueIntoMemory(0xFFFF, 0x00); // lcdc register
            SetValueIntoMemory(0xFF0F, 0xE1); // lcdc register
            SetValueIntoMemory(0xFF47, 0xfc); 
            SetValueIntoMemory(0xFF48, 0xff);
            SetValueIntoMemory(0xFF49, 0xff);
            SetValueIntoMemory(REG_TIM_TMA, 0);
            SetValueIntoMemory(REG_TIM_TAC, 0);
            SetValueIntoMemory(REG_TIM_TIMA, 0);
            Halt = false;
            Running = true;
            bIRQ = true;
            Cycle = 0;
            timer_internal_div = 0;
            timer_internal_cnt = 0;
            ppu.init(ref g_data);
            mbc = Memory[REG_MBC];
        }
        private  void Timer_step()
        {
             timer_internal_div +=  Cycle;
             while (timer_internal_div >= 256)
             {
                 SetValueIntoMemory(REG_TIM_DIV, (byte)(GetValueFromMemory(REG_TIM_DIV) + 1));
                 timer_internal_div -= 256;
             }

             ushort _tt = 256; // ticks threshold
             var vat = GetValueFromMemory(REG_TIM_TAC);
             if ((vat & 0x4) != 0)
             { // if on
                 switch (vat & 0x3)
                 {
                     case 0: _tt = 1024; break; //  4k
                     case 1: _tt = 16; break; // 256k
                     case 2: _tt = 64; break; // 64k
                     case 3: _tt = 256; break; // 16k
                 }
                 if (Cycle == 0)
                    Cycle += 4;

                 timer_internal_cnt += Cycle;
                 var val = GetValueFromMemory(REG_TIM_TIMA);
                 while (timer_internal_cnt >= _tt)
                 {
                     val++;
                     // actual step
                     if (val == 0)
                     {
                        SetValueIntoMemory(REG_TIM_TIMA, GetValueFromMemory(REG_TIM_TMA));
                        SetValueIntoMemory(INTF, (byte)(GetValueFromMemory(INTF) | 0x4));
                     }
                     else
                     {
                        SetValueIntoMemory(REG_TIM_TIMA, val);
                     }
                     timer_internal_cnt -= _tt;
                 }
             }
        }

        private void CheckInput()
        {
            byte joydata = (byte)((~GetValueFromMemory(IO_P1)) & 0xf0);
            byte val = 0;
            if (((joydata >> 4) & 0x2) !=0) val = (byte)((0x20) | (keys.a << 0) | (keys.b << 1) | (keys.select << 2) | (keys.start << 3));
            if (((joydata >> 4) & 0x1) !=0) val = (byte)((0x10) | (keys.right << 0) | (keys.left << 1) | (keys.up << 2) | (keys.down << 3));
            if (val != 0)
            {   
                SetValueIntoMemory(INTF, (byte)(GetValueFromMemory(INTF) | 0x10));
            }
            SetValueIntoMemory(IO_P1, (byte)~(val));
        }

        private void Interrupts()
        {
            var value = GetValueFromMemory(INTF);
            var inte = GetValueFromMemory(INTE);
            //for (int i = 0; i < 5; i++)
            {
                byte trig = (byte)(value & inte);
                if (trig != 0)
                {
                    Halt = false;
                    if (!bIRQ)
                        return;
                    if ((trig & 0x1) == 0x01)  // vblank
                    {
                        bIRQ = false;
                        SetValueIntoMemory(INTF, (byte)(value & 0xFE));
                        Lr35902.O_RST(0x40, "RST");
                    }
                    else
                    if ((trig & 0x2) == 0x02)
                    { // lcdstat
                        bIRQ = false;
                        SetValueIntoMemory(INTF, (byte)(value & 0xFD));
                        Lr35902.O_RST(0x48, "RST");
                    }
                    else
                    if ((trig & 0x4) == 0x04)
                    { // timer
                        bIRQ = false;
                        SetValueIntoMemory(INTF, (byte)(value & 0xFB));
                        Lr35902.O_RST(0x50, "RST");
                    }
                    else 
                    if ((trig & 0x8) == 0x08)
                    { // serial
                        bIRQ = false;
                        SetValueIntoMemory(INTF, (byte)(value & 0xF7));
                        Lr35902.O_RST(0x58, "RST");
                    }
                    else
                    if ((trig & 0x10) == 0x10)
                    { //  joypad
                        bIRQ = false;
                        SetValueIntoMemory(INTF, (byte)(value & 0xEF));
                        Lr35902.O_RST(0x60, "RST");
                    }
                }
            }
        }
    }
}
