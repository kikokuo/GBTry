using System;
using System.Collections.Generic;
using System.Text;

namespace GbTry.Machine
{
    public partial class LR35902
    {
        public string O_LD_R_R(byte op, string opname)
        {
            switch (op)
            {
                case 0x40: //LD B,B
                    break;
                case 0x41: //LD B,C
                    gbCPU.BC.lowbyte = gbCPU.BC.highbyte;
                    break;
                case 0x42: //LD B,d
                    gbCPU.BC.lowbyte = gbCPU.DE.lowbyte;
                    break;
                case 0x43: //LD B,e
                    gbCPU.BC.lowbyte = gbCPU.DE.highbyte;
                    break;
                case 0x44: //LD B,h
                    gbCPU.BC.lowbyte = gbCPU.HL.lowbyte;
                    break;
                case 0x45: //LD B,l
                    gbCPU.BC.lowbyte = gbCPU.HL.highbyte;
                    break;
                case 0x46: //LD B,(hl)
                    gbCPU.BC.lowbyte = gbCPU.GetValueFromMemory(gbCPU.HL.word);
                    if (gbCPU.debugflag)
                        opname += " $" + gbCPU.HL.word.ToString("X");
                    gbCPU.IncCycle(4);
                    break;
                case 0x47: //LD B,a
                    gbCPU.BC.lowbyte = gbCPU.AF.lowbyte;
                    break;
                case 0x48: //LD c,b
                    gbCPU.BC.highbyte = gbCPU.BC.lowbyte;
                    break;
                case 0x49://LD c,c
                    break;
                case 0x4a://LD c,d
                    gbCPU.BC.highbyte = gbCPU.DE.lowbyte;
                    break;
                case 0x4b://LD c,e
                    gbCPU.BC.highbyte = gbCPU.DE.highbyte;
                    break;
                case 0x4c://LD c,h
                    gbCPU.BC.highbyte = gbCPU.HL.lowbyte;
                    break;
                case 0x4d://LD c,l
                    gbCPU.BC.highbyte = gbCPU.HL.highbyte;
                    break;
                case 0x4e://LD c,(hl)
                    gbCPU.BC.highbyte = gbCPU.GetValueFromMemory(gbCPU.HL.word);
                    if (gbCPU.debugflag)
                        opname += " $" + gbCPU.HL.word.ToString("X");
                    gbCPU.IncCycle(4);
                    break;
                case 0x4f://LD c,a
                    gbCPU.BC.highbyte = gbCPU.AF.lowbyte;
                    break;
                case 0x50: //LD d,B
                    gbCPU.DE.lowbyte = gbCPU.BC.lowbyte;
                    break;
                case 0x51: //LD d,C
                    gbCPU.DE.lowbyte = gbCPU.BC.highbyte;
                    break;
                case 0x52: //LD d,d
                    break;
                case 0x53: //LD d,e
                    gbCPU.DE.lowbyte = gbCPU.DE.highbyte;
                    break;
                case 0x54: //LD d,h
                    gbCPU.DE.lowbyte = gbCPU.HL.lowbyte;
                    break;
                case 0x55: //LD d,l
                    gbCPU.DE.lowbyte = gbCPU.HL.highbyte;
                    break;
                case 0x56: //LD d,(hl)
                    gbCPU.DE.lowbyte = gbCPU.GetValueFromMemory(gbCPU.HL.word);
                    if (gbCPU.debugflag)
                        opname += " $" + gbCPU.HL.word.ToString("X");
                    gbCPU.IncCycle(4);
                    break;
                case 0x57: //LD d,a
                    gbCPU.DE.lowbyte = gbCPU.AF.lowbyte;
                    break;
                case 0x58: //LD e,b
                    gbCPU.DE.highbyte = gbCPU.BC.lowbyte;
                    break;
                case 0x59://LD e,c
                    gbCPU.DE.highbyte = gbCPU.BC.highbyte;
                    break;
                case 0x5a://LD e,d
                    gbCPU.DE.highbyte = gbCPU.DE.lowbyte;
                    break;
                case 0x5b://LD e,e
                    break;
                case 0x5c://LD e,h
                    gbCPU.DE.highbyte = gbCPU.HL.lowbyte;
                    break;
                case 0x5d://LD e,l
                    gbCPU.DE.highbyte = gbCPU.HL.highbyte;
                    break;
                case 0x5e://LD e,(hl)
                    gbCPU.DE.highbyte = gbCPU.GetValueFromMemory(gbCPU.HL.word);
                    if (gbCPU.debugflag)
                        opname += " $" + gbCPU.HL.word.ToString("X");
                    gbCPU.IncCycle(4);
                    break;
                case 0x5f://LD e,a
                    gbCPU.DE.highbyte = gbCPU.AF.lowbyte;
                    break;
                case 0x60: //LD h,B
                    gbCPU.HL.lowbyte = gbCPU.BC.lowbyte;
                    break;
                case 0x61: //LD h,C
                    gbCPU.HL.lowbyte = gbCPU.BC.highbyte;
                    break;
                case 0x62: //LD h,d
                    gbCPU.HL.lowbyte = gbCPU.DE.lowbyte;
                    break;
                case 0x63: //LD h,e
                    gbCPU.HL.lowbyte = gbCPU.DE.highbyte;
                    break;
                case 0x64: //LD h,h
                    break;
                case 0x65: //LD h,l
                    gbCPU.HL.lowbyte = gbCPU.HL.highbyte;
                    break;
                case 0x66: //LD h,(hl)
                    gbCPU.HL.lowbyte = gbCPU.GetValueFromMemory(gbCPU.HL.word);
                    if (gbCPU.debugflag)
                        opname += " $" + gbCPU.HL.word.ToString("X");
                    gbCPU.IncCycle(4);
                    break;
                case 0x67: //LD h,a
                    gbCPU.HL.lowbyte = gbCPU.AF.lowbyte;
                    break;
                case 0x68: //LD l,b
                    gbCPU.HL.highbyte = gbCPU.BC.lowbyte;
                    break;
                case 0x69://LD l,c
                    gbCPU.HL.highbyte = gbCPU.BC.highbyte;
                    break;
                case 0x6a://LD l,d
                    gbCPU.HL.highbyte = gbCPU.DE.lowbyte;
                    break;
                case 0x6b://LD l,e
                    gbCPU.HL.highbyte = gbCPU.DE.highbyte;
                    break;
                case 0x6c://LD l,h
                    gbCPU.HL.highbyte = gbCPU.HL.lowbyte;
                    break;
                case 0x6d://LD l,l
                    break;
                case 0x6e://LD l,(hl)
                    gbCPU.HL.highbyte = gbCPU.GetValueFromMemory(gbCPU.HL.word);
                    if (gbCPU.debugflag)
                        opname += " $" + gbCPU.HL.word.ToString("X");
                    gbCPU.IncCycle(4);
                    break;
                case 0x6f://LD l,a
                    gbCPU.HL.highbyte = gbCPU.AF.lowbyte;
                    break;
                case 0x70: //LD (hl),B
                    gbCPU.SetValueIntoMemory(gbCPU.HL.word, gbCPU.BC.lowbyte);
                    if (gbCPU.debugflag)
                        opname += "$" + gbCPU.HL.word.ToString("X") + ",B";
                    gbCPU.IncCycle(4);
                    break;
                case 0x71: //LD (hl),C
                    gbCPU.SetValueIntoMemory(gbCPU.HL.word, gbCPU.BC.highbyte);
                    if (gbCPU.debugflag)
                        opname += "$" + gbCPU.HL.word.ToString("X") + ",C";
                    gbCPU.IncCycle(4);
                    break;
                case 0x72: //LD (hl),d
                    gbCPU.SetValueIntoMemory(gbCPU.HL.word, gbCPU.DE.lowbyte);
                    if (gbCPU.debugflag)
                        opname += "$" + gbCPU.HL.word.ToString("X") + ",D";
                    gbCPU.IncCycle(4);
                    break;
                case 0x73: //LD (hl),e
                    gbCPU.SetValueIntoMemory(gbCPU.HL.word, gbCPU.DE.highbyte);
                    if (gbCPU.debugflag)
                        opname += "$" + gbCPU.HL.word.ToString("X") + ",E";
                    gbCPU.IncCycle(4);
                    break;
                case 0x74: //LD (hl),h
                    gbCPU.SetValueIntoMemory(gbCPU.HL.word, gbCPU.HL.lowbyte);
                    if (gbCPU.debugflag)
                        opname += "$" + gbCPU.HL.word.ToString("X") + ",H";
                    gbCPU.IncCycle(4);
                    break;
                case 0x75: //LD (hl),l
                    gbCPU.SetValueIntoMemory(gbCPU.HL.word, gbCPU.HL.highbyte);
                    if (gbCPU.debugflag)
                        opname += "$" + gbCPU.HL.word.ToString("X") + ",L";
                    gbCPU.IncCycle(4);
                    break;
                case 0x76: //Halt
                    gbCPU.Halt = true;
                    break;
                case 0x77: //LD (hl),a
                    gbCPU.SetValueIntoMemory(gbCPU.HL.word, gbCPU.AF.lowbyte);
                    if (gbCPU.debugflag)
                        opname += "$" + gbCPU.HL.word.ToString("X") + ",A";
                    gbCPU.IncCycle(4);
                    break;
                case 0x78: //LD a,b
                    gbCPU.AF.lowbyte = gbCPU.BC.lowbyte;
                    break;
                case 0x79://LD a,c
                    gbCPU.AF.lowbyte = gbCPU.BC.highbyte;
                    break;
                case 0x7a://LD a,d
                    gbCPU.AF.lowbyte = gbCPU.DE.lowbyte;
                    break;
                case 0x7b://LD a,e
                    gbCPU.AF.lowbyte = gbCPU.DE.highbyte;
                    break;
                case 0x7c://LD a,h
                    gbCPU.AF.lowbyte = gbCPU.HL.lowbyte;
                    break;
                case 0x7d://LD a,l
                    gbCPU.AF.lowbyte = gbCPU.HL.highbyte;
                    break;
                case 0x7e://LD a,(hl)
                    gbCPU.AF.lowbyte = gbCPU.GetValueFromMemory(gbCPU.HL.word);
                    if (gbCPU.debugflag)
                        opname += " $" + gbCPU.HL.word.ToString("X");
                    gbCPU.IncCycle(4);
                    break;
                case 0x7f://LD a,a
                    break;
            }
            gbCPU.IncCycle(4);
            return opname;
        }

        public string O_BIT(byte op, string opname)
        {
            byte b = (byte)((op & 0x38) >> 3);
            byte reg = (byte)(op & 7);

            switch (reg)
            {
                case 0:
                    gbCPU.SetFlagZ((byte)((gbCPU.BC.lowbyte >> b) & 1) == 0);
                    break;
                case 1:
                    gbCPU.SetFlagZ((byte)((gbCPU.BC.highbyte >> b) & 1) == 0);
                    break;
                case 2:
                    gbCPU.SetFlagZ((byte)((gbCPU.DE.lowbyte >> b) & 1) == 0);
                    break;
                case 3:
                    gbCPU.SetFlagZ((byte)((gbCPU.DE.highbyte >> b) & 1) == 0);
                    break;
                case 4:
                    gbCPU.SetFlagZ((byte)((gbCPU.HL.lowbyte >> b) & 1) == 0);
                    break;
                case 5:
                    gbCPU.SetFlagZ((byte)((gbCPU.HL.highbyte >> b) & 1) == 0);
                    break;
                case 6:
                    gbCPU.SetFlagZ((byte)((gbCPU.GetValueFromMemory(gbCPU.HL.word) >> b) & 1) == 0);
                    if (gbCPU.debugflag)
                        opname += " $" + gbCPU.HL.word.ToString("X");
                    gbCPU.IncCycle(8);
                    break;
                case 7:
                    gbCPU.SetFlagZ((byte)((gbCPU.AF.lowbyte >> b) & 1) == 0);
                    break;
            }
           
            gbCPU.SetFlagN(false);
            gbCPU.SetFlagH(true);
            gbCPU.IncCycle(8);
            return opname;
        }

        public string O_RES(byte op, string opname)
        {
            byte b = (byte)((op & 0x38) >> 3);
            byte reg = (byte)(op & 7);
            switch (reg)
            {
                case 0:
                    gbCPU.BC.lowbyte = (byte)(gbCPU.BC.lowbyte & ~(1 << b));
                    break;
                case 1:
                    gbCPU.BC.highbyte = (byte)(gbCPU.BC.highbyte & ~(1 << b));
                    break;
                case 2:
                    gbCPU.DE.lowbyte = (byte)(gbCPU.DE.lowbyte & ~(1 << b));
                    break;
                case 3:
                    gbCPU.DE.highbyte = (byte)(gbCPU.DE.highbyte & ~(1 << b));
                    break;
                case 4:
                    gbCPU.HL.lowbyte = (byte)(gbCPU.HL.lowbyte & ~(1 << b));
                    break;
                case 5:
                    gbCPU.HL.highbyte = (byte)(gbCPU.HL.highbyte & ~(1 << b));
                    break;
                case 6:
                    gbCPU.SetValueIntoMemory(gbCPU.HL.word, (byte)(gbCPU.GetValueFromMemory(gbCPU.HL.word) & ~(1 << b)));
                    if (gbCPU.debugflag)
                        opname += " $" + gbCPU.HL.word.ToString("X");
                    gbCPU.IncCycle(8);
                    break;
                case 7:
                    gbCPU.AF.lowbyte = (byte)(gbCPU.AF.lowbyte & ~(1 << b));
                    break;
            }
            gbCPU.IncCycle(8);
            return opname;
        }

        public string O_SET(byte op, string opname)
        {
            byte b = (byte)((op & 0x38) >> 3);
            byte reg = (byte)(op & 7);
            switch (reg)
            {
                case 0:
                    gbCPU.BC.lowbyte = (byte)(gbCPU.BC.lowbyte | (1 << b));
                    break;
                case 1:
                    gbCPU.BC.highbyte = (byte)(gbCPU.BC.highbyte | (1 << b));
                    break;
                case 2:
                    gbCPU.DE.lowbyte = (byte)(gbCPU.DE.lowbyte | (1 << b));
                    break;
                case 3:
                    gbCPU.DE.highbyte = (byte)(gbCPU.DE.highbyte | (1 << b));
                    break;
                case 4:
                    gbCPU.HL.lowbyte = (byte)(gbCPU.HL.lowbyte | (1 << b));
                    break;
                case 5:
                    gbCPU.HL.highbyte = (byte)(gbCPU.HL.highbyte | (1 << b));
                    break;
                case 6:
                    gbCPU.SetValueIntoMemory(gbCPU.HL.word, (byte)(gbCPU.GetValueFromMemory(gbCPU.HL.word) | (1 << b)));
                    if (gbCPU.debugflag)
                        opname += " $" + gbCPU.HL.word.ToString("X");
                    gbCPU.IncCycle(8);
                    break;
                case 7:
                    gbCPU.AF.lowbyte = (byte)(gbCPU.AF.lowbyte | (1 << b));
                    break;
            }
            gbCPU.IncCycle(8);
            return opname;
        }
        public string O_CP(byte op, string opname)
        {
            switch (op)
            {
                case 0xb8:// cp a ,b
                    CalcCP(gbCPU.BC.lowbyte);
                    if (gbCPU.debugflag)
                        opname += "A,B";
                    break;
                case 0xb9:// cp a ,c
                    CalcCP(gbCPU.BC.highbyte);
                    if (gbCPU.debugflag)
                        opname += "A,C";
                    break;
                case 0xba:// cp a ,d
                    CalcCP(gbCPU.DE.lowbyte);
                    if (gbCPU.debugflag)
                        opname += "A,D";
                    break;
                case 0xbb:// cp a ,e
                    CalcCP(gbCPU.DE.highbyte);
                    if (gbCPU.debugflag)
                        opname += "A,E";
                    break;
                case 0xbc:// cp a ,h
                    CalcCP(gbCPU.HL.lowbyte);
                    if (gbCPU.debugflag)
                        opname += "A,H";
                    break;
                case 0xbd:// cp a ,l
                    CalcCP(gbCPU.HL.highbyte);
                    if (gbCPU.debugflag)
                        opname += "A,L";
                    break;
                case 0xbe:// cp a ,(HL)
                    CalcCP(gbCPU.GetValueFromMemory(gbCPU.HL.word));
                    if (gbCPU.debugflag)
                        opname += "A,$" + gbCPU.HL.word.ToString("X");
                    gbCPU.IncCycle(4);
                    break;
                case 0xbf:// cp a ,a
                    CalcCP(gbCPU.AF.lowbyte);
                    if (gbCPU.debugflag)
                        opname += "A,A";
                    break;
                case 0xfe:// cp a ,(d8)
                    {
                        var value = gbCPU.fetch();
                        CalcCP(value);
                        if (gbCPU.debugflag)
                            opname += "A," + value.ToString("X")+"h";
                        gbCPU.IncCycle(4);
                        break;
                    }
            }
            return opname;
        }
        public byte CalcCP(byte Reg)
        {
            ushort var = (ushort)(gbCPU.AF.lowbyte - Reg);
            gbCPU.SetFlagZ(gbCPU.AF.lowbyte == Reg);
            gbCPU.SetFlagC(Reg > gbCPU.AF.lowbyte);
            gbCPU.SetFlagH((Reg&0x0f) > (gbCPU.AF.lowbyte&0x0f));
            gbCPU.SetFlagN(true);
            gbCPU.IncCycle(4);
            return (byte)var;
        }

        public string O_OR(byte op, string opname)
        {
            switch (op)
            {
                case 0xb0:// or a ,b
                    gbCPU.AF.lowbyte = CalcOR(gbCPU.BC.lowbyte);
                    if (gbCPU.debugflag)
                        opname += "A,B";
                    break;
                case 0xb1:// or a ,c
                    gbCPU.AF.lowbyte = CalcOR(gbCPU.BC.highbyte);
                    if (gbCPU.debugflag)
                        opname += "A,C";
                    break;
                case 0xb2:// or a ,d
                    gbCPU.AF.lowbyte = CalcOR(gbCPU.DE.lowbyte);
                    if (gbCPU.debugflag)
                        opname += "A,D";
                    break;
                case 0xb3:// or a ,e
                    gbCPU.AF.lowbyte = CalcOR(gbCPU.DE.highbyte);
                    if (gbCPU.debugflag)
                        opname += "A,E";
                    break;
                case 0xb4:// or a ,h
                    gbCPU.AF.lowbyte = CalcOR(gbCPU.HL.lowbyte);
                    if (gbCPU.debugflag)
                        opname += "A,H";
                    break;
                case 0xb5:// or a ,l
                    gbCPU.AF.lowbyte = CalcOR(gbCPU.HL.highbyte);
                    if (gbCPU.debugflag)
                        opname += "A,L";
                    break;
                case 0xb6:// or a ,(HL)
                    gbCPU.AF.lowbyte = CalcOR(gbCPU.GetValueFromMemory(gbCPU.HL.word));
                    if (gbCPU.debugflag)
                        opname += "A,$" + gbCPU.HL.word.ToString("X");
                    gbCPU.IncCycle(4);
                    break;
                case 0xb7:// or a ,a
                    gbCPU.AF.lowbyte = CalcOR(gbCPU.AF.lowbyte);
                    if (gbCPU.debugflag)
                        opname += "A,A";
                    break;
                case 0xf6:// or a ,d8
                    {
                        var value = gbCPU.fetch();
                        gbCPU.AF.lowbyte = CalcOR(value);
                        if (gbCPU.debugflag)
                            opname += "A,"+ value.ToString("X")+"h";
                        break;
                    }
            }
            return opname;
        }
        public byte CalcOR(byte Reg)
        {
            ushort var = (ushort)(gbCPU.AF.lowbyte | Reg);
            gbCPU.SetFlagZ(var == 0);
            gbCPU.SetFlagC(false);
            gbCPU.SetFlagH(false);
            gbCPU.SetFlagN(false);
            gbCPU.IncCycle(4);
            return (byte)var;
        }

        public string O_XOR(byte op, string opname)
        {
            switch (op)
            {
                case 0xa8:// xor a ,b
                    gbCPU.AF.lowbyte = CalcXOR(gbCPU.BC.lowbyte);
                    if (gbCPU.debugflag)
                        opname += "A,B";
                    break;
                case 0xa9:// xor a ,c
                    gbCPU.AF.lowbyte = CalcXOR(gbCPU.BC.highbyte);
                    if (gbCPU.debugflag)
                        opname += "A,C";
                    break;
                case 0xaa:// xor a ,d
                    gbCPU.AF.lowbyte = CalcXOR(gbCPU.DE.lowbyte);
                    if (gbCPU.debugflag)
                        opname += "A,D";
                    break;
                case 0xab:// xor a ,e
                    gbCPU.AF.lowbyte = CalcXOR(gbCPU.DE.highbyte);
                    if (gbCPU.debugflag)
                        opname += "A,E";
                    break;
                case 0xac:// xor a ,h
                    gbCPU.AF.lowbyte = CalcXOR(gbCPU.HL.lowbyte);
                    if (gbCPU.debugflag)
                        opname += "A,H";
                    break;
                case 0xad:// xor a ,l
                    gbCPU.AF.lowbyte = CalcXOR(gbCPU.HL.highbyte);
                    if (gbCPU.debugflag)
                        opname += "A,L";
                    break;
                case 0xae:// xor a ,(HL)
                    gbCPU.AF.lowbyte = CalcXOR(gbCPU.GetValueFromMemory(gbCPU.HL.word));
                    if (gbCPU.debugflag)
                        opname += "A,$" + gbCPU.HL.word.ToString("X");
                    gbCPU.IncCycle(4);
                    break;
                case 0xaf:// xor a ,a
                    gbCPU.AF.lowbyte = CalcXOR(gbCPU.AF.lowbyte);
                    if (gbCPU.debugflag)
                        opname += "A,A";
                    break;
                case 0xee:// xor a ,d8
                    {
                        var value = gbCPU.fetch();
                        gbCPU.AF.lowbyte = CalcXOR(value);
                        if (gbCPU.debugflag)
                            opname += "A,"+ value.ToString("X")+"h";
                        break;
                    }
            }
            return opname;
        }
        public byte CalcXOR(byte Reg)
        {
            ushort var = (ushort)(gbCPU.AF.lowbyte ^ Reg);
            gbCPU.SetFlagZ(var == 0);
            gbCPU.SetFlagC(false);
            gbCPU.SetFlagH(false);
            gbCPU.SetFlagN(false);
            gbCPU.IncCycle(4);
            return (byte)var;
        }

        public string O_AND(byte op, string opname)
        {
            switch (op)
            {
                case 0xa0:// and a ,b
                    gbCPU.AF.lowbyte = CalcAND(gbCPU.BC.lowbyte);
                    if (gbCPU.debugflag)
                        opname += "A,B";
                    break;
                case 0xa1:// and a ,c
                    gbCPU.AF.lowbyte = CalcAND(gbCPU.BC.highbyte);
                    if (gbCPU.debugflag)
                        opname += "A,C";
                    break;
                case 0xa2:// and a ,d
                    gbCPU.AF.lowbyte = CalcAND(gbCPU.DE.lowbyte);
                    if (gbCPU.debugflag)
                        opname += "A,D";
                    break;
                case 0xa3:// and a ,e
                    gbCPU.AF.lowbyte = CalcAND(gbCPU.DE.highbyte);
                    if (gbCPU.debugflag)
                        opname += "A,E";
                    break;
                case 0xa4:// and a ,h
                    gbCPU.AF.lowbyte = CalcAND(gbCPU.HL.lowbyte);
                    if (gbCPU.debugflag)
                        opname += "A,H";
                    break;
                case 0xa5:// and a ,l
                    gbCPU.AF.lowbyte = CalcAND(gbCPU.HL.highbyte);
                    if (gbCPU.debugflag)
                        opname += "A,L";
                    break;
                case 0xa6:// and a ,(HL)
                    gbCPU.AF.lowbyte = CalcAND(gbCPU.GetValueFromMemory(gbCPU.HL.word));
                    if (gbCPU.debugflag)
                        opname += "A,$" + gbCPU.HL.word.ToString("X");
                    gbCPU.IncCycle(4);
                    break;
                case 0xa7:// and a ,a
                    gbCPU.AF.lowbyte = CalcAND(gbCPU.AF.lowbyte);
                    if (gbCPU.debugflag)
                        opname += "A,A";
                    break;
                case 0xe6:// and a ,d8
                    {
                        var value = gbCPU.fetch();
                        gbCPU.AF.lowbyte = CalcAND(value);
                        if (gbCPU.debugflag)
                            opname += "A,"+ value.ToString("X")+"h";
                        break;
                    }
            }
            return opname;
        }
        public byte CalcAND(byte Reg)
        {
            ushort var = (ushort)(gbCPU.AF.lowbyte & Reg);
            gbCPU.SetFlagZ(var == 0);
            gbCPU.SetFlagC(false);
            gbCPU.SetFlagH(true);
            gbCPU.SetFlagN(false);
            gbCPU.IncCycle(4);
            return (byte)var;
        }

        public string O_SBC(byte op, string opname)
        {
            switch (op)
            {
                case 0x98:// sbc a ,b
                    gbCPU.AF.lowbyte = CalcSBC(gbCPU.BC.lowbyte);
                    if (gbCPU.debugflag)
                        opname += "A,B";
                    break;
                case 0x99:// sbc a ,c
                    gbCPU.AF.lowbyte = CalcSBC(gbCPU.BC.highbyte);
                    if (gbCPU.debugflag)
                        opname += "A,C";
                    break;
                case 0x9a:// sbc a ,d
                    gbCPU.AF.lowbyte = CalcSBC(gbCPU.DE.lowbyte);
                    if (gbCPU.debugflag)
                        opname += "A,D";
                    break;
                case 0x9b:// sbc a ,e
                    gbCPU.AF.lowbyte = CalcSBC(gbCPU.DE.highbyte);
                    if (gbCPU.debugflag)
                        opname += "A,E";
                    break;
                case 0x9c:// sbc a ,h
                    gbCPU.AF.lowbyte = CalcSBC(gbCPU.HL.lowbyte);
                    if (gbCPU.debugflag)
                        opname += "A,H";
                    break;
                case 0x9d:// sbc a ,l
                    gbCPU.AF.lowbyte = CalcSBC(gbCPU.HL.highbyte);
                    if (gbCPU.debugflag)
                        opname += "A,L";
                    break;
                case 0x9e:// sbc a ,(HL)
                    gbCPU.AF.lowbyte = CalcSBC(gbCPU.GetValueFromMemory(gbCPU.HL.word));
                    if (gbCPU.debugflag)
                        opname += "A,$" + gbCPU.HL.word.ToString("X");
                    gbCPU.IncCycle(4);
                    break;
                case 0x9f:// sbc a ,a
                    gbCPU.AF.lowbyte = CalcSBC(gbCPU.AF.lowbyte);
                    if (gbCPU.debugflag)
                        opname += "A,A";
                    break;
                case 0xde:// sbc a ,d8
                    {
                        var value = gbCPU.fetch();
                        gbCPU.AF.lowbyte = CalcSBC(value);
                        if (gbCPU.debugflag)
                            opname += "A,"+value.ToString("X")+"h";
                        break;
                    }
            }
            return opname;
        }
        public byte CalcSBC(byte Reg)
        {
            var c = gbCPU.GetFlagC();
            ushort var = (ushort)(gbCPU.AF.lowbyte - Reg - c);
            gbCPU.SetFlagZ((var & 0x00FF) == 0);
            gbCPU.SetFlagC((ushort)(Reg + c) > gbCPU.AF.lowbyte);
            gbCPU.SetFlagH((gbCPU.AF.lowbyte & 0xF) < ((Reg & 0xF) + c));
            gbCPU.SetFlagN(true);
            gbCPU.IncCycle(4);
            return (byte)var;
        }
        public string O_SUB(byte op, string opname)
        {
            switch (op)
            {
                case 0x90:// sub a ,b
                    gbCPU.AF.lowbyte = CalcSUB(gbCPU.BC.lowbyte);
                    if (gbCPU.debugflag)
                        opname += "A,B";
                    break;
                case 0x91:// sub a ,c
                    gbCPU.AF.lowbyte = CalcSUB(gbCPU.BC.highbyte);
                    if (gbCPU.debugflag)
                        opname += "A,C";
                    break;
                case 0x92:// sub a ,d
                    gbCPU.AF.lowbyte = CalcSUB(gbCPU.DE.lowbyte);
                    if (gbCPU.debugflag)
                        opname += "A,D";
                    break;
                case 0x93:// sub a ,e
                    gbCPU.AF.lowbyte = CalcSUB(gbCPU.DE.highbyte);
                    if (gbCPU.debugflag)
                        opname += "A,E";
                    break;
                case 0x94:// sub a ,h
                    gbCPU.AF.lowbyte = CalcSUB(gbCPU.HL.lowbyte);
                    if (gbCPU.debugflag)
                        opname += "A,H";
                    break;
                case 0x95:// sub a ,l
                    gbCPU.AF.lowbyte = CalcSUB(gbCPU.HL.highbyte);
                    if (gbCPU.debugflag)
                        opname += "A,L";
                    break;
                case 0x96:// sub a ,(HL)
                    gbCPU.AF.lowbyte = CalcSUB(gbCPU.GetValueFromMemory(gbCPU.HL.word));
                    if (gbCPU.debugflag)
                        opname += "A,$" + gbCPU.HL.word.ToString("X");
                    gbCPU.IncCycle(4);
                    break;
                case 0x97:// sub a ,a
                    gbCPU.AF.lowbyte = CalcSUB(gbCPU.AF.lowbyte);
                    if (gbCPU.debugflag)
                        opname += "A,A";
                    break;
                case 0xd6:// sub a ,d8
                    {
                        var value = gbCPU.fetch();
                        gbCPU.AF.lowbyte = CalcSUB(value);
                        if (gbCPU.debugflag)
                            opname += "A,"+ value.ToString("X")+"h";
                        break;
                    }
            }
            return opname;
        }
        public byte CalcSUB(byte Reg)
        {
            ushort var = (ushort)(gbCPU.AF.lowbyte - Reg);
            gbCPU.SetFlagZ((var & 0x00FF) == 0);
            gbCPU.SetFlagC(Reg > gbCPU.AF.lowbyte);
            gbCPU.SetFlagH((gbCPU.AF.lowbyte & 0xF) < (Reg & 0xF));
            gbCPU.SetFlagN(true);
            gbCPU.IncCycle(4);
            return (byte)var;
        }

        public string O_ADC(byte op, string opname)
        {
            switch (op)
            {
                case 0x88:// adc a ,b
                    gbCPU.AF.lowbyte = CalcADC(gbCPU.BC.lowbyte);
                    if (gbCPU.debugflag)
                        opname += "A,B";
                    break;
                case 0x89:// adc a ,c
                    gbCPU.AF.lowbyte = CalcADC(gbCPU.BC.highbyte);
                    if (gbCPU.debugflag)
                        opname += "A,C";
                    break;
                case 0x8a:// adc a ,d
                    gbCPU.AF.lowbyte = CalcADC(gbCPU.DE.lowbyte);
                    if (gbCPU.debugflag)
                        opname += "A,D";
                    break;
                case 0x8b:// adc a ,e
                    gbCPU.AF.lowbyte = CalcADC(gbCPU.DE.highbyte);
                    if (gbCPU.debugflag)
                        opname += "A,E";
                    break;
                case 0x8c:// adc a ,h
                    gbCPU.AF.lowbyte = CalcADC(gbCPU.HL.lowbyte);
                    if (gbCPU.debugflag)
                        opname += "A,H";
                    break;
                case 0x8d:// adc a ,l
                    gbCPU.AF.lowbyte = CalcADC(gbCPU.HL.highbyte);
                    if (gbCPU.debugflag)
                        opname += "A,L";
                    break;
                case 0x8e:// adc a ,(hl)
                    gbCPU.AF.lowbyte = CalcADC(gbCPU.GetValueFromMemory(gbCPU.HL.word));
                    if (gbCPU.debugflag)
                        opname += "A,$" + gbCPU.HL.word.ToString("X");
                    gbCPU.IncCycle(4);
                    break;
                case 0x8f:// adc a ,a
                    gbCPU.AF.lowbyte = CalcADC(gbCPU.AF.lowbyte);
                    if (gbCPU.debugflag)
                        opname += "A,A";
                    break;
                case 0xce:// adc a ,d8
                    {
                        var value = gbCPU.fetch();
                        gbCPU.AF.lowbyte = CalcADC(value);
                        if (gbCPU.debugflag)
                            opname += "A,"+ value.ToString("X")+"h";
                        break;
                    }
            }
            return opname;
        }
        public byte CalcADC(byte Reg)
        {
            var c = gbCPU.GetFlagC();
            int var = gbCPU.AF.lowbyte + Reg + c;
            gbCPU.SetFlagZ((var & 0x00FF) == 0);
            gbCPU.SetFlagC(var > 0xFF);
            gbCPU.SetFlagH(((gbCPU.AF.lowbyte & 0xF) + (Reg & 0xF) + c) > 0xF);
            gbCPU.SetFlagN(false);
            gbCPU.IncCycle(4);
            return (byte)var;
        }


        public string O_ADD(byte op, string opname)
        {
            switch (op)
            {
                case 0x80:// add a ,b
                    gbCPU.AF.lowbyte = CalcADD(gbCPU.BC.lowbyte);
                    if (gbCPU.debugflag)
                        opname += "A,B";
                    break;
                case 0x81:// add a ,c
                    gbCPU.AF.lowbyte = CalcADD(gbCPU.BC.highbyte);
                    if (gbCPU.debugflag)
                        opname += "A,C";
                    break;
                case 0x82:// add a ,d
                    gbCPU.AF.lowbyte = CalcADD(gbCPU.DE.lowbyte);
                    if (gbCPU.debugflag)
                        opname += "A,D";
                    break;
                case 0x83:// add a ,e
                    gbCPU.AF.lowbyte = CalcADD(gbCPU.DE.highbyte);
                    if (gbCPU.debugflag)
                        opname += "A,E";
                    break;
                case 0x84:// add a ,h
                    gbCPU.AF.lowbyte = CalcADD(gbCPU.HL.lowbyte);
                    if (gbCPU.debugflag)
                        opname += "A,H";
                    break;
                case 0x85:// add a ,l
                    gbCPU.AF.lowbyte = CalcADD(gbCPU.HL.highbyte);
                    if (gbCPU.debugflag)
                        opname += "A,L";
                    break;
                case 0x86:// add a ,(HL)
                    gbCPU.AF.lowbyte = CalcADD(gbCPU.GetValueFromMemory(gbCPU.HL.word));
                    if (gbCPU.debugflag)
                        opname += "A,$" + gbCPU.HL.word.ToString("X");
                    gbCPU.IncCycle(4);
                    break;
                case 0x87:// add a ,a
                    gbCPU.AF.lowbyte = CalcADD(gbCPU.AF.lowbyte);
                    if (gbCPU.debugflag)
                        opname += "A,A";
                    break;
                case 0xc6:// add a ,d8
                    {
                        var value = gbCPU.fetch();
                        gbCPU.AF.lowbyte = CalcADD(value);
                        if (gbCPU.debugflag)
                            opname += "A,"+value.ToString("X")+"h";
                        gbCPU.IncCycle(4);
                        break; 
                    }
                case 0xe8:// add sp ,d8
                    {
                        gbCPU.SetFlagN(false);
                        gbCPU.SetFlagZ(false);
                        sbyte value = (sbyte)gbCPU.fetch();
                        int result = value + gbCPU.SP.word;
                        gbCPU.SetFlagC(((gbCPU.SP.word ^ value ^ (result & 0xffff)) & 0x100) == 0x100);
                        gbCPU.SetFlagH(((gbCPU.SP.word ^ value ^ (result & 0xffff)) & 0x10) == 0x10);
                        gbCPU.SP.word = (ushort)result;
                        if (gbCPU.debugflag)
                            opname += "SP," + value.ToString("X") + "h";
                        gbCPU.IncCycle(16);
                        break;
                    }
            }
            return opname;
        }
        public byte CalcADD(byte Reg)
        {
            int var = gbCPU.AF.lowbyte + Reg;
            gbCPU.SetFlagC(var > 0xFF);
            gbCPU.SetFlagZ((var&0x00FF) == 0);
            gbCPU.SetFlagH(((gbCPU.AF.lowbyte & 0xF) + (Reg & 0xF)) > 0xF);
            gbCPU.SetFlagN(false);
            gbCPU.IncCycle(4);
            return (byte)var;
        }
    }
}
