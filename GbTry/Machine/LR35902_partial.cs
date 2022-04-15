using System;
using System.Collections.Generic;
using System.Text;

namespace GbTry.Machine
{
    public partial class LR35902
    {
        public void Init_CB_exeucteOperation()
        {
            executeCBOP = new Dictionary<byte, DelegateOP>()
            {
                {0x00,new DelegateOP(0x00,"RLC B",CB_RLC)},
                {0x01,new DelegateOP(0x01,"RLC C",CB_RLC)},
                {0x02,new DelegateOP(0x02,"RLC D",CB_RLC)},
                {0x03,new DelegateOP(0x03,"RLC E",CB_RLC)},
                {0x04,new DelegateOP(0x04,"RLC H",CB_RLC)},
                {0x05,new DelegateOP(0x05,"RLC L",CB_RLC)},
                {0x06,new DelegateOP(0x06,"RLC (HL),",CB_RLC)},
                {0x07,new DelegateOP(0x07,"RLC A",CB_RLC)},
                {0x08,new DelegateOP(0x08,"RRC B",CB_RRC)},
                {0x09,new DelegateOP(0x09,"RRC C",CB_RRC)},
                {0x0a,new DelegateOP(0x0a,"RRC D",CB_RRC)},
                {0x0b,new DelegateOP(0x0b,"RRC E",CB_RRC)},
                {0x0c,new DelegateOP(0x0c,"RRC H",CB_RRC)},
                {0x0d,new DelegateOP(0x0d,"RRC L",CB_RRC)},
                {0x0e,new DelegateOP(0x0e,"RRC (HL),",CB_RRC)},
                {0x0f,new DelegateOP(0x0f,"RRC A",CB_RRC)},

                {0x10,new DelegateOP(0x00,"RL  B",CB_RL)},
                {0x11,new DelegateOP(0x01,"RL  C",CB_RL)},
                {0x12,new DelegateOP(0x02,"RL  D",CB_RL)},
                {0x13,new DelegateOP(0x03,"RL  E",CB_RL)},
                {0x14,new DelegateOP(0x04,"RL  H",CB_RL)},
                {0x15,new DelegateOP(0x05,"RL  L",CB_RL)},
                {0x16,new DelegateOP(0x06,"RL  (HL),",CB_RL)},
                {0x17,new DelegateOP(0x07,"RL  A",CB_RL)},
                {0x18,new DelegateOP(0x08,"RR  B",CB_RR)},
                {0x19,new DelegateOP(0x09,"RR  C",CB_RR)},
                {0x1a,new DelegateOP(0x0a,"RR  D",CB_RR)},
                {0x1b,new DelegateOP(0x0b,"RR  E",CB_RR)},
                {0x1c,new DelegateOP(0x0c,"RR  H",CB_RR)},
                {0x1d,new DelegateOP(0x0d,"RR  L",CB_RR)},
                {0x1e,new DelegateOP(0x0e,"RR  (HL),",CB_RR)},
                {0x1f,new DelegateOP(0x0f,"RR  A",CB_RR)},

                {0x20,new DelegateOP(0x00,"SLA B",CB_SLA)},
                {0x21,new DelegateOP(0x01,"SLA C",CB_SLA)},
                {0x22,new DelegateOP(0x02,"SLA D",CB_SLA)},
                {0x23,new DelegateOP(0x03,"SLA E",CB_SLA)},
                {0x24,new DelegateOP(0x04,"SLA H",CB_SLA)},
                {0x25,new DelegateOP(0x05,"SLA L",CB_SLA)},
                {0x26,new DelegateOP(0x06,"SLA (HL),",CB_SLA)},
                {0x27,new DelegateOP(0x07,"SLA A",CB_SLA)},
                {0x28,new DelegateOP(0x08,"SRA B",CB_SRA)},
                {0x29,new DelegateOP(0x09,"SRA C",CB_SRA)},
                {0x2a,new DelegateOP(0x0a,"SRA D",CB_SRA)},
                {0x2b,new DelegateOP(0x0b,"SRA E",CB_SRA)},
                {0x2c,new DelegateOP(0x0c,"SRA H",CB_SRA)},
                {0x2d,new DelegateOP(0x0d,"SRA L",CB_SRA)},
                {0x2e,new DelegateOP(0x0e,"SRA (HL),",CB_SRA)},
                {0x2f,new DelegateOP(0x0f,"SRA A",CB_SRA)},

                {0x30,new DelegateOP(0x00,"SWAP B",CB_SWAP)},
                {0x31,new DelegateOP(0x01,"SWAP C",CB_SWAP)},
                {0x32,new DelegateOP(0x02,"SWAP D",CB_SWAP)},
                {0x33,new DelegateOP(0x03,"SWAP E",CB_SWAP)},
                {0x34,new DelegateOP(0x04,"SWAP H",CB_SWAP)},
                {0x35,new DelegateOP(0x05,"SWAP L",CB_SWAP)},
                {0x36,new DelegateOP(0x06,"SWAP (HL),",CB_SWAP)},
                {0x37,new DelegateOP(0x07,"SWAP A",CB_SWAP)},
                {0x38,new DelegateOP(0x08,"SRL B",CB_SRL)},
                {0x39,new DelegateOP(0x09,"SRL C",CB_SRL)},
                {0x3a,new DelegateOP(0x0a,"SRL D",CB_SRL)},
                {0x3b,new DelegateOP(0x0b,"SRL E",CB_SRL)},
                {0x3c,new DelegateOP(0x0c,"SRL H",CB_SRL)},
                {0x3d,new DelegateOP(0x0d,"SRL L",CB_SRL)},
                {0x3e,new DelegateOP(0x0e,"SRL (HL),",CB_SRL)},
                {0x3f,new DelegateOP(0x0f,"SRL A",CB_SRL)},

                {0x40,new DelegateOP(0x40,"BIT 0,B",O_BIT)},
                {0x41,new DelegateOP(0x41,"BIT 0,C",O_BIT)},
                {0x42,new DelegateOP(0x42,"BIT 0,D",O_BIT)},
                {0x43,new DelegateOP(0x43,"BIT 0,E",O_BIT)},
                {0x44,new DelegateOP(0x44,"BIT 0,H",O_BIT)},
                {0x45,new DelegateOP(0x45,"BIT 0,L",O_BIT)},
                {0x46,new DelegateOP(0x46,"BIT 0,",O_BIT)},
                {0x47,new DelegateOP(0x47,"BIT 0,A",O_BIT)},
                {0x48,new DelegateOP(0x48,"BIT 1,B",O_BIT)},
                {0x49,new DelegateOP(0x49,"BIT 1,C",O_BIT)},
                {0x4A,new DelegateOP(0x4A,"BIT 1,D",O_BIT)},
                {0x4B,new DelegateOP(0x4B,"BIT 1,E",O_BIT)},
                {0x4C,new DelegateOP(0x4C,"BIT 1,H",O_BIT)},
                {0x4D,new DelegateOP(0x4D,"BIT 1,L",O_BIT)},
                {0x4E,new DelegateOP(0x4E,"BIT 1,",O_BIT)},
                {0x4F,new DelegateOP(0x4F,"BIT 1,A",O_BIT)},

                {0x50,new DelegateOP(0x50,"BIT 2,B",O_BIT)},
                {0x51,new DelegateOP(0x51,"BIT 2,C",O_BIT)},
                {0x52,new DelegateOP(0x52,"BIT 2,D",O_BIT)},
                {0x53,new DelegateOP(0x53,"BIT 2,E",O_BIT)},
                {0x54,new DelegateOP(0x54,"BIT 2,H",O_BIT)},
                {0x55,new DelegateOP(0x55,"BIT 2,L",O_BIT)},
                {0x56,new DelegateOP(0x56,"BIT 2,",O_BIT)},
                {0x57,new DelegateOP(0x57,"BIT 2,A",O_BIT)},
                {0x58,new DelegateOP(0x58,"BIT 3,B",O_BIT)},
                {0x59,new DelegateOP(0x59,"BIT 3,C",O_BIT)},
                {0x5A,new DelegateOP(0x5A,"BIT 3,D",O_BIT)},
                {0x5B,new DelegateOP(0x5B,"BIT 3,E",O_BIT)},
                {0x5C,new DelegateOP(0x5C,"BIT 3,H",O_BIT)},
                {0x5D,new DelegateOP(0x5D,"BIT 3,L",O_BIT)},
                {0x5E,new DelegateOP(0x5E,"BIT 3,",O_BIT)},
                {0x5F,new DelegateOP(0x5F,"BIT 3,A",O_BIT)},

                {0x60,new DelegateOP(0x60,"BIT 4,B",O_BIT)},
                {0x61,new DelegateOP(0x61,"BIT 4,C",O_BIT)},
                {0x62,new DelegateOP(0x62,"BIT 4,D",O_BIT)},
                {0x63,new DelegateOP(0x63,"BIT 4,E",O_BIT)},
                {0x64,new DelegateOP(0x64,"BIT 4,H",O_BIT)},
                {0x65,new DelegateOP(0x65,"BIT 4,L",O_BIT)},
                {0x66,new DelegateOP(0x66,"BIT 4,",O_BIT)},
                {0x67,new DelegateOP(0x67,"BIT 4,A",O_BIT)},
                {0x68,new DelegateOP(0x68,"BIT 5,B",O_BIT)},
                {0x69,new DelegateOP(0x69,"BIT 5,C",O_BIT)},
                {0x6A,new DelegateOP(0x6A,"BIT 5,D",O_BIT)},
                {0x6B,new DelegateOP(0x6B,"BIT 5,E",O_BIT)},
                {0x6C,new DelegateOP(0x6C,"BIT 5,H",O_BIT)},
                {0x6D,new DelegateOP(0x6D,"BIT 5,L",O_BIT)},
                {0x6E,new DelegateOP(0x6E,"BIT 5,",O_BIT)},
                {0x6F,new DelegateOP(0x6F,"BIT 5,A",O_BIT)},

                {0x70,new DelegateOP(0x70,"BIT 6,B",O_BIT)},
                {0x71,new DelegateOP(0x71,"BIT 6,C",O_BIT)},
                {0x72,new DelegateOP(0x72,"BIT 6,D",O_BIT)},
                {0x73,new DelegateOP(0x73,"BIT 6,E",O_BIT)},
                {0x74,new DelegateOP(0x74,"BIT 6,H",O_BIT)},
                {0x75,new DelegateOP(0x75,"BIT 6,L",O_BIT)},
                {0x76,new DelegateOP(0x76,"BIT 6,",O_BIT)},
                {0x77,new DelegateOP(0x77,"BIT 6,A",O_BIT)},
                {0x78,new DelegateOP(0x78,"BIT 7,B",O_BIT)},
                {0x79,new DelegateOP(0x79,"BIT 7,C",O_BIT)},
                {0x7A,new DelegateOP(0x7A,"BIT 7,D",O_BIT)},
                {0x7B,new DelegateOP(0x7B,"BIT 7,E",O_BIT)},
                {0x7C,new DelegateOP(0x7C,"BIT 7,H",O_BIT)},
                {0x7D,new DelegateOP(0x7D,"BIT 7,L",O_BIT)},
                {0x7E,new DelegateOP(0x7E,"BIT 7,",O_BIT)},
                {0x7F,new DelegateOP(0x7F,"BIT 7,A",O_BIT)},

                {0x80,new DelegateOP(0x80,"RES 0,B",O_RES)},
                {0x81,new DelegateOP(0x81,"RES 0,C",O_RES)},
                {0x82,new DelegateOP(0x82,"RES 0,D",O_RES)},
                {0x83,new DelegateOP(0x83,"RES 0,E",O_RES)},
                {0x84,new DelegateOP(0x84,"RES 0,H",O_RES)},
                {0x85,new DelegateOP(0x85,"RES 0,L",O_RES)},
                {0x86,new DelegateOP(0x86,"RES 0,",O_RES)},
                {0x87,new DelegateOP(0x87,"RES 0,A",O_RES)},
                {0x88,new DelegateOP(0x88,"RES 1,B",O_RES)},
                {0x89,new DelegateOP(0x89,"RES 1,C",O_RES)},
                {0x8A,new DelegateOP(0x8A,"RES 1,D",O_RES)},
                {0x8B,new DelegateOP(0x8B,"RES 1,E",O_RES)},
                {0x8C,new DelegateOP(0x8C,"RES 1,H",O_RES)},
                {0x8D,new DelegateOP(0x8D,"RES 1,L",O_RES)},
                {0x8E,new DelegateOP(0x8E,"RES 1,",O_RES)},
                {0x8F,new DelegateOP(0x8F,"RES 1,A",O_RES)},

                {0x90,new DelegateOP(0x90,"RES 2,B",O_RES)},
                {0x91,new DelegateOP(0x91,"RES 2,C",O_RES)},
                {0x92,new DelegateOP(0x92,"RES 2,D",O_RES)},
                {0x93,new DelegateOP(0x93,"RES 2,E",O_RES)},
                {0x94,new DelegateOP(0x94,"RES 2,H",O_RES)},
                {0x95,new DelegateOP(0x95,"RES 2,L",O_RES)},
                {0x96,new DelegateOP(0x96,"RES 2,",O_RES)},
                {0x97,new DelegateOP(0x97,"RES 2,A",O_RES)},
                {0x98,new DelegateOP(0x98,"RES 3,B",O_RES)},
                {0x99,new DelegateOP(0x99,"RES 3,C",O_RES)},
                {0x9A,new DelegateOP(0x9A,"RES 3,D",O_RES)},
                {0x9B,new DelegateOP(0x9B,"RES 3,E",O_RES)},
                {0x9C,new DelegateOP(0x9C,"RES 3,H",O_RES)},
                {0x9D,new DelegateOP(0x9D,"RES 3,L",O_RES)},
                {0x9E,new DelegateOP(0x9E,"RES 3,",O_RES)},
                {0x9F,new DelegateOP(0x9F,"RES 3,A",O_RES)},

                {0xa0,new DelegateOP(0xa0,"RES 4,B",O_RES)},
                {0xa1,new DelegateOP(0xa1,"RES 4,C",O_RES)},
                {0xa2,new DelegateOP(0xa2,"RES 4,D",O_RES)},
                {0xa3,new DelegateOP(0xa3,"RES 4,E",O_RES)},
                {0xa4,new DelegateOP(0xa4,"RES 4,H",O_RES)},
                {0xa5,new DelegateOP(0xa5,"RES 4,L",O_RES)},
                {0xa6,new DelegateOP(0xa6,"RES 4,",O_RES)},
                {0xa7,new DelegateOP(0xa7,"RES 4,A",O_RES)},
                {0xa8,new DelegateOP(0xa8,"RES 5,B",O_RES)},
                {0xa9,new DelegateOP(0xa9,"RES 5,C",O_RES)},
                {0xaA,new DelegateOP(0xaA,"RES 5,D",O_RES)},
                {0xaB,new DelegateOP(0xaB,"RES 5,E",O_RES)},
                {0xaC,new DelegateOP(0xaC,"RES 5,H",O_RES)},
                {0xaD,new DelegateOP(0xaD,"RES 5,L",O_RES)},
                {0xaE,new DelegateOP(0xaE,"RES 5,",O_RES)},
                {0xaF,new DelegateOP(0xaF,"RES 5,A",O_RES)},

                {0xb0,new DelegateOP(0xb0,"RES 6,B",O_RES)},
                {0xb1,new DelegateOP(0xb1,"RES 6,C",O_RES)},
                {0xb2,new DelegateOP(0xb2,"RES 6,D",O_RES)},
                {0xb3,new DelegateOP(0xb3,"RES 6,E",O_RES)},
                {0xb4,new DelegateOP(0xb4,"RES 6,H",O_RES)},
                {0xb5,new DelegateOP(0xb5,"RES 6,L",O_RES)},
                {0xb6,new DelegateOP(0xb6,"RES 6,",O_RES)},
                {0xb7,new DelegateOP(0xb7,"RES 6,A",O_RES)},
                {0xb8,new DelegateOP(0xb8,"RES 7,B",O_RES)},
                {0xb9,new DelegateOP(0xb9,"RES 7,C",O_RES)},
                {0xbA,new DelegateOP(0xbA,"RES 7,D",O_RES)},
                {0xbB,new DelegateOP(0xbB,"RES 7,E",O_RES)},
                {0xbC,new DelegateOP(0xbC,"RES 7,H",O_RES)},
                {0xbD,new DelegateOP(0xbD,"RES 7,L",O_RES)},
                {0xbE,new DelegateOP(0xbE,"RES 7,",O_RES)},
                {0xbF,new DelegateOP(0xbF,"RES 7,A",O_RES)},

                {0xc0,new DelegateOP(0xc0,"SET 0,B",O_SET)},
                {0xc1,new DelegateOP(0xc1,"SET 0,C",O_SET)},
                {0xc2,new DelegateOP(0xc2,"SET 0,D",O_SET)},
                {0xc3,new DelegateOP(0xc3,"SET 0,E",O_SET)},
                {0xc4,new DelegateOP(0xc4,"SET 0,H",O_SET)},
                {0xc5,new DelegateOP(0xc5,"SET 0,L",O_SET)},
                {0xc6,new DelegateOP(0xc6,"SET 0,",O_SET)},
                {0xc7,new DelegateOP(0xc7,"SET 0,A",O_SET)},
                {0xc8,new DelegateOP(0xc8,"SET 1,B",O_SET)},
                {0xc9,new DelegateOP(0xc9,"SET 1,C",O_SET)},
                {0xcA,new DelegateOP(0xcA,"SET 1,D",O_SET)},
                {0xcB,new DelegateOP(0xcB,"SET 1,E",O_SET)},
                {0xcC,new DelegateOP(0xcC,"SET 1,H",O_SET)},
                {0xcD,new DelegateOP(0xcD,"SET 1,L",O_SET)},
                {0xcE,new DelegateOP(0xcE,"SET 1,",O_SET)},
                {0xcF,new DelegateOP(0xcF,"SET 1,A",O_SET)},

                {0xd0,new DelegateOP(0xd0,"SET 2,B",O_SET)},
                {0xd1,new DelegateOP(0xd1,"SET 2,C",O_SET)},
                {0xd2,new DelegateOP(0xd2,"SET 2,D",O_SET)},
                {0xd3,new DelegateOP(0xd3,"SET 2,E",O_SET)},
                {0xd4,new DelegateOP(0xd4,"SET 2,H",O_SET)},
                {0xd5,new DelegateOP(0xd5,"SET 2,L",O_SET)},
                {0xd6,new DelegateOP(0xd6,"SET 2,",O_SET)},
                {0xd7,new DelegateOP(0xd7,"SET 2,A",O_SET)},
                {0xd8,new DelegateOP(0xd8,"SET 3,B",O_SET)},
                {0xd9,new DelegateOP(0xd9,"SET 3,C",O_SET)},
                {0xdA,new DelegateOP(0xdA,"SET 3,D",O_SET)},
                {0xdB,new DelegateOP(0xdB,"SET 3,E",O_SET)},
                {0xdC,new DelegateOP(0xdC,"SET 3,H",O_SET)},
                {0xdD,new DelegateOP(0xdD,"SET 3,L",O_SET)},
                {0xdE,new DelegateOP(0xdE,"SET 3,",O_SET)},
                {0xdF,new DelegateOP(0xdF,"SET 3,A",O_SET)},

                {0xe0,new DelegateOP(0xe0,"SET 4,B",O_SET)},
                {0xe1,new DelegateOP(0xe1,"SET 4,C",O_SET)},
                {0xe2,new DelegateOP(0xe2,"SET 4,D",O_SET)},
                {0xe3,new DelegateOP(0xe3,"SET 4,E",O_SET)},
                {0xe4,new DelegateOP(0xe4,"SET 4,H",O_SET)},
                {0xe5,new DelegateOP(0xe5,"SET 4,L",O_SET)},
                {0xe6,new DelegateOP(0xe6,"SET 4,",O_SET)},
                {0xe7,new DelegateOP(0xe7,"SET 4,A",O_SET)},
                {0xe8,new DelegateOP(0xe8,"SET 5,B",O_SET)},
                {0xe9,new DelegateOP(0xe9,"SET 5,C",O_SET)},
                {0xeA,new DelegateOP(0xeA,"SET 5,D",O_SET)},
                {0xeB,new DelegateOP(0xeB,"SET 5,E",O_SET)},
                {0xeC,new DelegateOP(0xeC,"SET 5,H",O_SET)},
                {0xeD,new DelegateOP(0xeD,"SET 5,L",O_SET)},
                {0xeE,new DelegateOP(0xeE,"SET 5,",O_SET)},
                {0xeF,new DelegateOP(0xeF,"SET 5,A",O_SET)},

                {0xf0,new DelegateOP(0xf0,"SET 6,B",O_SET)},
                {0xf1,new DelegateOP(0xf1,"SET 6,C",O_SET)},
                {0xf2,new DelegateOP(0xf2,"SET 6,D",O_SET)},
                {0xf3,new DelegateOP(0xf3,"SET 6,E",O_SET)},
                {0xf4,new DelegateOP(0xf4,"SET 6,H",O_SET)},
                {0xf5,new DelegateOP(0xf5,"SET 6,L",O_SET)},
                {0xf6,new DelegateOP(0xf6,"SET 6,",O_SET)},
                {0xf7,new DelegateOP(0xf7,"SET 6,A",O_SET)},
                {0xf8,new DelegateOP(0xf8,"SET 7,B",O_SET)},
                {0xf9,new DelegateOP(0xf9,"SET 7,C",O_SET)},
                {0xfA,new DelegateOP(0xfA,"SET 7,D",O_SET)},
                {0xfB,new DelegateOP(0xfB,"SET 7,E",O_SET)},
                {0xfC,new DelegateOP(0xfC,"SET 7,H",O_SET)},
                {0xfD,new DelegateOP(0xfD,"SET 7,L",O_SET)},
                {0xfE,new DelegateOP(0xfE,"SET 7,",O_SET)},
                {0xfF,new DelegateOP(0xfF,"SET 7,A",O_SET)},

            };
        }

        public string CB_SRL(byte op, string opname)
        {
            switch (op)
            {
                case 0x08: // SRA B
                    gbCPU.BC.lowbyte = CalcSRL(gbCPU.BC.lowbyte);
                    break;
                case 0x09: // SRA C
                    gbCPU.BC.highbyte = CalcSRL(gbCPU.BC.highbyte);
                    break;
                case 0x0a: // SRA D
                    gbCPU.DE.lowbyte = CalcSRL(gbCPU.DE.lowbyte);
                    break;
                case 0x0b: // SRA E
                    gbCPU.DE.highbyte = CalcSRL(gbCPU.DE.highbyte);
                    break;
                case 0x0c: // SRA H
                    gbCPU.HL.lowbyte = CalcSRL(gbCPU.HL.lowbyte);
                    break;
                case 0x0d: // SRA L
                    gbCPU.HL.highbyte = CalcSRL(gbCPU.HL.highbyte);
                    break;
                case 0x0e: // SRA (HL)
                    byte value = CalcSRL(gbCPU.GetValueFromMemory(gbCPU.HL.word));
                    gbCPU.SetValueIntoMemory(gbCPU.HL.word, value);
                    gbCPU.IncCycle(8);
                    if (gbCPU.debugflag)
                        opname += "$" + gbCPU.HL.word.ToString("X");
                    break;
                case 0x0f: // SRA A
                    gbCPU.AF.lowbyte = CalcSRL(gbCPU.AF.lowbyte);
                    break;
            }
            return opname;
        }

        private byte CalcSRL(byte Reg)
        {
            var Carry = (byte)(Reg & 0x01);
            var retvalue = (byte)(Reg >> 0x01);
            gbCPU.SetFlagZ(retvalue == 0x00);
            gbCPU.SetFlagC(Carry == 0x01);
            gbCPU.SetFlagH(false);
            gbCPU.SetFlagN(false);
            gbCPU.IncCycle(8);
            return retvalue;
        }

        public string CB_SWAP(byte op, string opname)
        {
            switch (op)
            {
                case 0: // SWAP B
                    gbCPU.BC.lowbyte = CalcSWAP(gbCPU.BC.lowbyte);
                    break;
                case 1: // SWAP C
                    gbCPU.BC.highbyte = CalcSWAP(gbCPU.BC.highbyte);
                    break;
                case 2: // SWAP D
                    gbCPU.DE.lowbyte = CalcSWAP(gbCPU.DE.lowbyte);
                    break;
                case 3: // SWAP E
                    gbCPU.DE.highbyte = CalcSWAP(gbCPU.DE.highbyte);
                    break;
                case 4: // SWAP H
                    gbCPU.HL.lowbyte = CalcSWAP(gbCPU.HL.lowbyte);
                    break;
                case 5: // SWAP L
                    gbCPU.HL.highbyte = CalcSWAP(gbCPU.HL.highbyte);
                    break;
                case 6: // SWAP (HL)
                    byte value = CalcSWAP(gbCPU.GetValueFromMemory(gbCPU.HL.word));
                    gbCPU.SetValueIntoMemory(gbCPU.HL.word, value);
                    gbCPU.IncCycle(8);
                    if (gbCPU.debugflag)
                        opname += "$" + gbCPU.HL.word.ToString("X");
                    break;
                case 7: // SWAP A
                    gbCPU.AF.lowbyte = CalcSWAP(gbCPU.AF.lowbyte);
                    break;
            }
            return opname;
        }

        private byte CalcSWAP(byte Reg)
        {
            var retvalue = (byte)(Reg <<0x04 | Reg >> 0x04);
            gbCPU.SetFlagZ(retvalue == 0x00);
            gbCPU.SetFlagC(false);
            gbCPU.SetFlagH(false);
            gbCPU.SetFlagN(false);
            gbCPU.IncCycle(8);
            return retvalue;
        }

        public string CB_SRA(byte op, string opname)
        {
            switch (op)
            {
                case 0x08: // SRA B
                    gbCPU.BC.lowbyte = CalcSRA(gbCPU.BC.lowbyte);
                    break;
                case 0x09: // SRA C
                    gbCPU.BC.highbyte = CalcSRA(gbCPU.BC.highbyte);
                    break;
                case 0x0a: // SRA D
                    gbCPU.DE.lowbyte = CalcSRA(gbCPU.DE.lowbyte);
                    break;
                case 0x0b: // SRA E
                    gbCPU.DE.highbyte = CalcSRA(gbCPU.DE.highbyte);
                    break;
                case 0x0c: // SRA H
                    gbCPU.HL.lowbyte = CalcSRA(gbCPU.HL.lowbyte);
                    break;
                case 0x0d: // SRA L
                    gbCPU.HL.highbyte = CalcSRA(gbCPU.HL.highbyte);
                    break;
                case 0x0e: // SRA (HL)
                    byte value = CalcSRA(gbCPU.GetValueFromMemory(gbCPU.HL.word));
                    gbCPU.SetValueIntoMemory(gbCPU.HL.word, value);
                    gbCPU.IncCycle(8);
                    if (gbCPU.debugflag)
                        opname += "$" + gbCPU.HL.word.ToString("X");
                    break;
                case 0x0f: // SRA A
                    gbCPU.AF.lowbyte = CalcSRA(gbCPU.AF.lowbyte);
                    break;
            }
            return opname;
        }

        private byte CalcSRA(byte Reg)
        {
            var Carry = (byte)(Reg & 0x01);
            var retvalue = (byte)(Reg & 0x80 | Reg >> 0x01);
            gbCPU.SetFlagZ(retvalue == 0x00);
            gbCPU.SetFlagC(Carry == 0x01);
            gbCPU.SetFlagH(false);
            gbCPU.SetFlagN(false);
            gbCPU.IncCycle(8);
            return retvalue;
        }

        public string CB_SLA(byte op, string opname)
        {
            switch (op)
            {
                case 0: // SLA B
                    gbCPU.BC.lowbyte = CalcSLA(gbCPU.BC.lowbyte);
                    break;
                case 1: // SLA C
                    gbCPU.BC.highbyte = CalcSLA(gbCPU.BC.highbyte);
                    break;
                case 2: // SLA D
                    gbCPU.DE.lowbyte = CalcSLA(gbCPU.DE.lowbyte);
                    break;
                case 3: // SLA E
                    gbCPU.DE.highbyte = CalcSLA(gbCPU.DE.highbyte);
                    break;
                case 4: // SLA H
                    gbCPU.HL.lowbyte = CalcSLA(gbCPU.HL.lowbyte);
                    break;
                case 5: // SLA L
                    gbCPU.HL.highbyte = CalcSLA(gbCPU.HL.highbyte);
                    break;
                case 6: // SLA (HL)
                    byte value = CalcSLA(gbCPU.GetValueFromMemory(gbCPU.HL.word));
                    gbCPU.SetValueIntoMemory(gbCPU.HL.word, value);
                    gbCPU.IncCycle(8);
                    if (gbCPU.debugflag)
                        opname += "$" + gbCPU.HL.word.ToString("X");
                    break;
                case 7: // SLA A
                    gbCPU.AF.lowbyte = CalcSLA(gbCPU.AF.lowbyte);
                    break;
            }
            return opname;
        }
        private byte CalcSLA(byte Reg)
        {
            var Carry = (byte)((Reg >> 0x07) & 0x01);
            var retvalue = (byte)(Reg << 0x01);
            gbCPU.SetFlagZ(retvalue == 0x00);
            gbCPU.SetFlagC(Carry == 0x01);
            gbCPU.SetFlagH(false);
            gbCPU.SetFlagN(false);
            gbCPU.IncCycle(8);
            return retvalue;
        }


        public string CB_RLC(byte op, string opname)
        {
            switch (op)
            {
                case 0: // RLC B
                    gbCPU.BC.lowbyte = CalcRLC(gbCPU.BC.lowbyte);
                    break;
                case 1: // RLC C
                    gbCPU.BC.highbyte = CalcRLC(gbCPU.BC.highbyte);
                    break;
                case 2: // RLC D
                    gbCPU.DE.lowbyte = CalcRLC(gbCPU.DE.lowbyte);
                    break;
                case 3: // RLC E
                    gbCPU.DE.highbyte = CalcRLC(gbCPU.DE.highbyte);
                    break;
                case 4: // RLC H
                    gbCPU.HL.lowbyte = CalcRLC(gbCPU.HL.lowbyte);
                    break;
                case 5: // RLC L
                    gbCPU.HL.highbyte = CalcRLC(gbCPU.HL.highbyte);
                    break;
                case 6: // RLC (HL)
                    byte value = CalcRLC(gbCPU.GetValueFromMemory(gbCPU.HL.word));
                    gbCPU.SetValueIntoMemory(gbCPU.HL.word, value);
                    gbCPU.IncCycle(8);
                    if (gbCPU.debugflag)
                        opname += "$" + gbCPU.HL.word.ToString("X");
                    break;
                case 7: // RLC A
                    gbCPU.AF.lowbyte = CalcRLC(gbCPU.AF.lowbyte);
                    break;
            }
            return opname;
        }
        private byte CalcRLC(byte Reg)
        {
            var Carry = (byte)((Reg >> 0x07) & 0x01);
            var retvalue = (byte)(Carry | (Reg << 0x01));
            gbCPU.SetFlagZ(retvalue == 0x00);
            gbCPU.SetFlagC(Carry == 0x01);
            gbCPU.SetFlagH(false);
            gbCPU.SetFlagN(false);
            gbCPU.IncCycle(8);
            return retvalue;
        }

        public string CB_RL(byte op, string opname)
        {
            switch (op)
            {
                case 0x00: // RL B
                    gbCPU.BC.lowbyte = CalcRL(gbCPU.BC.lowbyte);
                    break;
                case 0x01: // RL C
                    gbCPU.BC.highbyte = CalcRL(gbCPU.BC.highbyte);
                    break;
                case 0x02: // RL D
                    gbCPU.DE.lowbyte = CalcRL(gbCPU.DE.lowbyte);
                    break;
                case 0x03: // RL E
                    gbCPU.DE.highbyte = CalcRL(gbCPU.DE.highbyte);
                    break;
                case 0x04: // RL H
                    gbCPU.HL.lowbyte = CalcRL(gbCPU.HL.lowbyte);
                    break;
                case 0x05: // RL L
                    gbCPU.HL.highbyte = CalcRL(gbCPU.HL.highbyte);
                    break;
                case 0x06: // RL (HL)
                    byte value = CalcRL(gbCPU.GetValueFromMemory(gbCPU.HL.word));
                    gbCPU.SetValueIntoMemory(gbCPU.HL.word, value);
                    gbCPU.IncCycle(8);
                    if (gbCPU.debugflag)
                        opname += "$" + gbCPU.HL.word.ToString("X");
                    break;
                case 0x07: // RL A
                    gbCPU.AF.lowbyte = CalcRL(gbCPU.AF.lowbyte);
                    break;
            }
            return opname;
        }
        private byte CalcRL(byte Reg)
        {
            var retvalue = (byte)(gbCPU.GetFlagC() | (Reg << 0x01));
            gbCPU.SetFlagZ(retvalue == 0x00);
            gbCPU.SetFlagC((byte)((Reg >> 0x07) & 0x01) == 0x01);
            gbCPU.SetFlagH(false);
            gbCPU.SetFlagN(false);
            gbCPU.IncCycle(8);
            return retvalue;
        }

        public string CB_RRC(byte op, string opname)
        {
            switch (op)
            {
                case 0x08: // RRC B
                    gbCPU.BC.lowbyte = CalcRRC(gbCPU.BC.lowbyte);
                    break;
                case 0x09: // RRC C
                    gbCPU.BC.highbyte = CalcRRC(gbCPU.BC.highbyte);
                    break;
                case 0x0a: // RRC D
                    gbCPU.DE.lowbyte = CalcRRC(gbCPU.DE.lowbyte);
                    break;
                case 0x0b: // RRC E
                    gbCPU.DE.highbyte = CalcRRC(gbCPU.DE.highbyte);
                    break;
                case 0x0c: // RRC H
                    gbCPU.HL.lowbyte = CalcRRC(gbCPU.HL.lowbyte);
                    break;
                case 0x0d: // RRC L
                    gbCPU.HL.highbyte = CalcRRC(gbCPU.HL.highbyte);
                    break;
                case 0x0e: // RRC (HL)
                    byte value = CalcRRC(gbCPU.GetValueFromMemory(gbCPU.HL.word));
                    gbCPU.SetValueIntoMemory(gbCPU.HL.word, value);
                    gbCPU.IncCycle(8);
                    if (gbCPU.debugflag)
                        opname += "$" + gbCPU.HL.word.ToString("X");
                    break;
                case 0x0f: // RRC A
                    gbCPU.AF.lowbyte = CalcRRC(gbCPU.AF.lowbyte);
                    break;
            }
            return opname;
        }
        private byte CalcRRC(byte Reg)
        {
            var Carry = (byte)(Reg & 0x01);
            var retvalue = (byte)(Carry << 0x07 | (Reg >> 0x01));
            gbCPU.SetFlagZ(retvalue == 0x00);
            gbCPU.SetFlagC(Carry == 0x01);
            gbCPU.SetFlagH(false);
            gbCPU.SetFlagN(false);
            gbCPU.IncCycle(8);
            return retvalue;
        }

        public string CB_RR(byte op, string opname)
        {
            switch (op)
            {
                case 0x08: // RR B
                    gbCPU.BC.lowbyte = CalcRR(gbCPU.BC.lowbyte);
                    break;
                case 0x09: // RR C
                    gbCPU.BC.highbyte = CalcRR(gbCPU.BC.highbyte);
                    break;
                case 0x0a: // RR D
                    gbCPU.DE.lowbyte = CalcRR(gbCPU.DE.lowbyte);
                    break;
                case 0x0b: // RR E
                    gbCPU.DE.highbyte = CalcRR(gbCPU.DE.highbyte);
                    break;
                case 0x0c: // RR H
                    gbCPU.HL.lowbyte = CalcRR(gbCPU.HL.lowbyte);
                    break;
                case 0x0d: // RR L
                    gbCPU.HL.highbyte = CalcRR(gbCPU.HL.highbyte);
                    break;
                case 0x0e: // RR (HL)
                    byte value = CalcRR(gbCPU.GetValueFromMemory(gbCPU.HL.word));
                    gbCPU.SetValueIntoMemory(gbCPU.HL.word, value);
                    gbCPU.IncCycle(8);
                    if (gbCPU.debugflag)
                        opname += "$" + gbCPU.HL.word.ToString("X");
                    break;
                case 0x0f: // RR A
                    gbCPU.AF.lowbyte = CalcRR(gbCPU.AF.lowbyte);
                    break;
            }
            return opname;
        }
        private byte CalcRR(byte Reg)
        {
            var retvalue = (byte)(gbCPU.GetFlagC() << 0x07 | (Reg >> 0x01));
            gbCPU.SetFlagZ(retvalue == 0x00);
            gbCPU.SetFlagC((byte)(Reg & 0x01) == 0x01);
            gbCPU.SetFlagH(false);
            gbCPU.SetFlagN(false);
            gbCPU.IncCycle(8);
            return retvalue;
        }
    }
}
