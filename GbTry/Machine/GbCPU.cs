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
        private bool EnableBoot;
        private int timer_internal_div = 0;
        private int timer_internal_cnt = 0;

        public byte[] Memory = new byte[64*1024];
        public JoyPad keys = new JoyPad();
        // 預設按鍵對映
        public Dictionary<Key, int> button_key_map = new Dictionary<Key, int>();
        public void Init()
        {
            Lr35902.GetGbCPU(this);
            ppu.GetGbCPU(this);

            button_key_map.Add(Key.Z, 0x04); // a
            button_key_map.Add(Key.X, 0x08); // b
            button_key_map.Add(Key.Space, 0x01); // select
            button_key_map.Add(Key.Enter, 0x02); // start
            button_key_map.Add(Key.Up, 0x80); //up
            button_key_map.Add(Key.Down, 0x40); // down
            button_key_map.Add(Key.Left, 0x10); // left
            button_key_map.Add(Key.Right, 0x20); // right
        }
        public void User_Input(Key kevvaule, byte data)
        {
            int index;
            if (button_key_map.TryGetValue(kevvaule, out index))
            {
                switch(index)
                {
                    case 0x01:
                        keys.select = 0x01;
                        break;
                    case 0x02:
                        keys.start = 0x02;
                        break;
                    case 0x04:
                        keys.a = 0x04;
                        break;
                    case 0x08:
                        keys.b = 0x08;
                        break;
                    case 0x10:
                        keys.left = 0x10;
                        break;
                    case 0x20:
                        keys.right = 0x20;
                        break;
                    case 0x40:
                        keys.down = 0x40;
                        break;
                    case 0x80:
                        keys.up = 0x80;
                        break;
                }
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
            return Memory[address];
        }

        public void SetValueIntoMemory(ushort address, byte value)
        {
            Memory[address] = value;
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

        public unsafe void Tick()
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
                Interrupts();
                if (!Halt)
                {
                    Cycle = 0;
                    byte opcode = fetch();
                    Lr35902.ExecuteOP(opcode);
                }
                ppu.Render();
                timer_step();
            }
        }
        public void SetBootImage(bool enableboot)
        {
            EnableBoot = enableboot;
        }

        public void PowerOn(ref UInt32[] g_data)
        {
            PC.word = 0x0100;
            AF.word = 0x1180;
            BC.word = 0x0000;
            DE.word = 0xFF56;
            HL.word = 0x000D;
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
            Halt = false;
            Cycle = 0;
            ppu.init(ref g_data);
        }
        private  void timer_step()
        {
             timer_internal_div +=  Cycle;
             while (timer_internal_div >= 256)
             {
                 var val = GetValueFromMemory(REG_TIM_DIV);
                 val++;
                 SetValueIntoMemory(REG_TIM_DIV,val);
                 timer_internal_div = 0;
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

                timer_internal_cnt = timer_internal_cnt + Cycle;
                 while (timer_internal_cnt >= _tt)
                 {
                     var val = GetValueFromMemory(REG_TIM_TIMA);
                     val++;
                     
                     // actual step
                     if (val == 0)
                     {
                         var vma = GetValueFromMemory(REG_TIM_TMA);
                         SetValueIntoMemory(REG_TIM_TIMA, vma);
                         var intf = GetValueFromMemory(INTF);
                         intf |= 0x4;
                         SetValueIntoMemory(INTF, intf);
                    }
                    else
                    {
                        SetValueIntoMemory(REG_TIM_TIMA, val);
                    }
                     timer_internal_cnt -= _tt;
                 }
             }
        }

        private void Interrupts()
        {
            /*byte joydata = (byte)((~GetValueFromMemory(IO_P1)) & 0xf0);
            byte val = 0;
            if (((joydata >> 4) & 0x2) != 0) val = (byte)((0x20) | (keys.a << 0) | (keys.b << 1) | (keys.select << 2) | (keys.start << 3));
            if (((joydata >> 4) & 0x1) != 0) val = (byte)((0x10) | (keys.right << 0) | (keys.left << 1) | (keys.up << 2) | (keys.down << 3));
            if (val != 0)
            {   
                var value = GetValueFromMemory(INTF); 
                SetValueIntoMemory(INTF, (byte)(value | 0x10));
            }
            SetValueIntoMemory(IO_P1, (byte)~(val));*/


            var value = GetValueFromMemory(INTF);
            byte trig = (byte)(GetValueFromMemory(INTF) & GetValueFromMemory(INTE));
            if (trig != 0)
            {
                Halt = false;
                if (!bIRQ)
                    return;
                if ((trig & 0x1) == 0x01)  // vblank
                {   
                    bIRQ = false;
                    SetValueIntoMemory(INTF, (byte)(value & 0xFE));
                    Lr35902.O_RST(0x40,"RST"); 
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
                } else
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
